'===============================================================================
' Microsoft patterns & practices
' CompositeUI Application Block
'===============================================================================
' Copyright © Microsoft Corporation.  All rights reserved.
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
' FITNESS FOR A PARTICULAR PURPOSE.
'===============================================================================



Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports System.ComponentModel.Design.Serialization
Imports System.ComponentModel.Design
Imports System.Collections
Imports System.Collections.Generic
Imports System
Imports System.CodeDom
Imports System.Drawing
Imports System.Reflection
Imports System.Diagnostics

Namespace SmartParts
	<DesignerCategory("Code")> _
	<ToolboxBitmap(GetType(SmartPartInfo), "SmartPartInfo")> _
	<DesignerSerializer(GetType(SmartPartInfo.SmartPartInfoDesignerSerializer), GetType(CodeDomSerializer))> _
	Partial Public Class SmartPartInfo : Inherits Component
		''' <summary>
		''' Adds the SmartPart property to the smart part info at design-time.
		''' </summary>
		Friend Class SmartPartInfoDesigner : Inherits ComponentDesigner
			Private innerSmartPart As String

			Protected Overrides Sub PreFilterProperties(ByVal properties As IDictionary)
				Dim host As IDesignerHost = DirectCast(GetService(GetType(IDesignerHost)), IDesignerHost)
				If TypeOf host.RootComponent Is WorkItem Then
					properties.Add("SmartPart", TypeDescriptor.GetProperties(Me)("SmartPart"))
				End If
			End Sub

			<Description("The smart part this information object describes."), Category("Behavior"), TypeConverter(GetType(SmartPartConverter)), DesignOnly(True), DefaultValue(CType(Nothing, String))> _
			Public Property SmartPart() As String
				Get
					Return innerSmartPart
				End Get
				Set(ByVal value As String)
					innerSmartPart = value
				End Set
			End Property
		End Class

		''' <summary>
		''' Provides a list of the smartparts in the current workitem to select from.
		''' </summary>
		Private Class SmartPartConverter : Inherits TypeConverter
			Public Overloads Overrides Function GetStandardValuesSupported(ByVal context As ITypeDescriptorContext) As Boolean
				Return True
			End Function

			Public Overloads Overrides Function GetStandardValuesExclusive(ByVal context As ITypeDescriptorContext) As Boolean
				Return True
			End Function

			Public Overloads Overrides Function GetStandardValues(ByVal context As ITypeDescriptorContext) As StandardValuesCollection
				Dim smartParts As List(Of String) = New List(Of String)()
				For Each component As IComponent In context.Container.Components
					' Only add those components that have the SmartPart attribute defined, 
					' which are the only ones we care to call RegisterSPI with.
					If Attribute.IsDefined(CObj(component).GetType(), GetType(SmartPartAttribute)) Then
						smartParts.Add(component.Site.Name)
					End If
				Next component

				Return New StandardValuesCollection(smartParts)
			End Function
		End Class

		''' <summary>
		''' Provides custom serialization to register the SPI with the WorkItem.
		''' </summary>
		Friend Class SmartPartInfoDesignerSerializer : Inherits DesignerCodeDomSerializer
			Private Const GetSmartPartInfoMethod As String = "GetSmartPartInfo"
			Private Const AddSmartPartInfosMethod As String = "AddSmartPartInfos"
			Private Const ProviderField As String = "infoProvider"

			Public Overrides Function Serialize(ByVal manager As IDesignerSerializationManager, ByVal value As Object) As Object
				' Get the original CodeDom as emitted by the built-in component serializer.
				Dim baseSerializer As CodeDomSerializer = CType(manager.GetSerializer(GetType(Component), GetType(CodeDomSerializer)), CodeDomSerializer)
				Dim statements As CodeStatementCollection = CType(baseSerializer.Serialize(manager, value), CodeStatementCollection)

				Dim host As IDesignerHost = CType(manager.GetService(GetType(IDesignerHost)), IDesignerHost)

				If TypeOf host.RootComponent Is WorkItem Then
					'GenerateWorkItemCode(manager, value, statements)
				Else
					GenerateSmartPartCode(manager, value, statements)
				End If

				Return statements
			End Function

#Region "Utility code for the function Deserialize"
			Private Function StatementEditor(ByVal invoke As CodeMethodInvokeExpression) As Boolean
				Dim propref As CodePropertyReferenceExpression = TryCast(invoke.Method.TargetObject, CodePropertyReferenceExpression)
				If Not propref Is Nothing Then
					Dim fieldref As CodeFieldReferenceExpression = TryCast(propref.TargetObject, CodeFieldReferenceExpression)
					Return fieldref.FieldName = ProviderField AndAlso TypeOf fieldref.TargetObject Is CodeThisReferenceExpression
				End If

				Return False
			End Function
#End Region

			Public Overrides Function Deserialize(ByVal manager As IDesignerSerializationManager, ByVal codeObject As Object) As Object
				Dim declaration As CodeTypeDeclaration = CType(manager.GetService(GetType(CodeTypeDeclaration)), CodeTypeDeclaration)
				Dim statements As CodeStatementCollection = TryCast(codeObject, CodeStatementCollection)

				If Not statements Is Nothing Then
					' Before deserialization, remove statements we added.
					RemoveFromStatements(Of CodeMethodInvokeExpression)(statements, AddressOf StatementEditor)

					Dim aProviderField As CodeMemberField = FindMember(Of CodeMemberField)(declaration, ProviderField)
					If Not aProviderField Is Nothing Then
						declaration.Members.Remove(aProviderField)
					End If
				End If

				Return MyBase.Deserialize(manager, codeObject)
			End Function

#Region "Serialize helpers"

			Private Shared Sub GenerateSmartPartCode(ByVal manager As IDesignerSerializationManager, ByVal value As Object, ByVal statements As CodeStatementCollection)
				Dim host As IDesignerHost = CType(manager.GetService(GetType(IDesignerHost)), IDesignerHost)
				Dim declaration As CodeTypeDeclaration = CType(manager.GetService(GetType(CodeTypeDeclaration)), CodeTypeDeclaration)

				If host.Container.Components(ProviderField) Is Nothing Then
					Dim provider As IComponent = host.CreateComponent(GetType(SmartPartInfoProvider), ProviderField)
					host.Container.Add(provider, ProviderField)
				End If

				' Make sure this will break a test if we modify the ISmartPartInfoProvider interface.
				Dim interfaceMethod As MethodInfo = GetType(ISmartPartInfoProvider).GetMethod(GetSmartPartInfoMethod)
				Debug.Assert(Not interfaceMethod Is Nothing, "ISmartPartInfoProvider interface definition changed. GetSmartPartInfo method not found.")

				Dim getInfo As CodeMemberMethod = FindMember(Of CodeMemberMethod)(declaration, GetSmartPartInfoMethod)
				If getInfo Is Nothing Then
					getInfo = GenerateGetSmartPartInfo(interfaceMethod)
					declaration.Members.Add(getInfo)
				End If

				AddToProvider(manager, value, statements)
			End Sub

			Private Shared Sub AddToProvider(ByVal manager As IDesignerSerializationManager, ByVal value As Object, ByVal statements As CodeStatementCollection)
				Dim refsvc As IReferenceService = CType(manager.GetService(GetType(IReferenceService)), IReferenceService)

				' this.infoProvider.Items.Add(component);
				statements.Add(New CodeMethodInvokeExpression(New CodePropertyReferenceExpression(New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), ProviderField), "Items"), "Add", New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), refsvc.GetName(value))))
			End Sub

			Private Shared Function GenerateGetSmartPartInfo(ByVal interfaceMethod As MethodInfo) As CodeMemberMethod
				' ISmartPartInfoProvider.GetSmartPartInfo signature:
				' public ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
				Dim methodImpl As CodeMemberMethod = New CodeMemberMethod()
				methodImpl.Attributes = MemberAttributes.Public
				methodImpl.Comments.Add(New CodeCommentStatement("<summary>Generated by a designer. Required inmplementation of ISmartPartInfoProvider"))

				GenerateCastForInterface(methodImpl)

				methodImpl.Name = interfaceMethod.Name
				methodImpl.ReturnType = New CodeTypeReference(interfaceMethod.ReturnType)
				Dim parameters As List(Of CodeExpression) = New List(Of CodeExpression)()

				For Each parameter As ParameterInfo In interfaceMethod.GetParameters()
					methodImpl.Parameters.Add(New CodeParameterDeclarationExpression(parameter.ParameterType, parameter.Name))
					parameters.Add(New CodeArgumentReferenceExpression(parameter.Name))
				Next parameter

				' return this.infoProvider.GetSmartPartInfo(SmartPartInfoType);
				Dim invoke As CodeMethodInvokeExpression = New CodeMethodInvokeExpression(New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), ProviderField), interfaceMethod.Name, parameters.ToArray())

				methodImpl.Statements.Add(New CodeMethodReturnStatement(invoke))

				Return methodImpl
			End Function

			Private Shared Sub GenerateCastForInterface(ByVal methodImpl As CodeMemberMethod)
				methodImpl.Statements.Add(New CodeCommentStatement(My.Resources.SmartPartMustImplementISmartPartInfoProvider))
				' ISmartPartInfoProvider ensureProvider = (ISmartPartInfoProvider)this;
				methodImpl.Statements.Add(New CodeVariableDeclarationStatement(GetType(ISmartPartInfoProvider), "ensureProvider", New CodeThisReferenceExpression()))

			End Sub

			Private Shared Sub GenerateWorkItemCode(ByVal manager As IDesignerSerializationManager, ByVal value As Object, ByVal statements As CodeStatementCollection)
				' See if there's a smartpart assigned at designtime.
				Dim smartPart As String = CStr(TypeDescriptor.GetProperties(value)("SmartPart").GetValue(value))
				If Not smartPart Is Nothing Then
					' Add the registration information to it.
					Dim registerSP As CodeMethodInvokeExpression = New CodeMethodInvokeExpression(New CodeMethodReferenceExpression(New CodeBaseReferenceExpression(), "RegisterSmartPartInfo"), New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), smartPart), New CodeFieldReferenceExpression(New CodeThisReferenceExpression(), manager.GetName(value)))
					statements.Add(registerSP)
				End If
			End Sub

#End Region
		End Class
	End Class
End Namespace
