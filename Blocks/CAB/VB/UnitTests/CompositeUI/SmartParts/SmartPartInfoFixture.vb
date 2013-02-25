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

Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.IO
Imports System.Diagnostics

Namespace Tests.SmartParts
	<TestClass()> _
	Public Class SmartPartInfoFixture
		<TestMethod()> _
		Public Sub CanSetTitleAndDesccription()
			Dim info As SmartPartInfo = New SmartPartInfo()

			info.Title = "Title"
			info.Description = "Description"

			Assert.AreEqual("Title", info.Title)
			Assert.AreEqual("Description", info.Description)
		End Sub

		Public Sub GenerateCodeForSmartPartInfoCompiles()
			Dim attr As DesignerSerializerAttribute = CType(Attribute.GetCustomAttribute(GetType(SmartPartInfo), GetType(DesignerSerializerAttribute)), DesignerSerializerAttribute)
			Dim serializer As CodeDomSerializer = CType(Activator.CreateInstance(Type.GetType(attr.SerializerTypeName)), CodeDomSerializer)

			Dim smartPart As UserControl = New UserControl()

			Dim container As Container = New Container()
			Dim info1 As SmartPartInfo = New SmartPartInfo("foo", "")
			container.Add(info1, "info1")
			Dim info2 As MockSPI = New MockSPI("bar", "")
			container.Add(info2, "info2")

			Dim manager As MockManager = New MockManager(container.Components)
			manager.Services.Add(GetType(IDesignerHost), New MockDesignerHost(smartPart, container))
			manager.Services.Add(GetType(IReferenceService), New MockReferenceService(container.Components))
			manager.Services.Add(GetType(IContainer), container)
			manager.Services.Add(GetType(IDesignerSerializationManager), manager)

			Dim declaration As CodeTypeDeclaration = New CodeTypeDeclaration("UserControl1")
			Dim init As CodeMemberMethod = New CodeMemberMethod()
			init.Name = "InitializeComponent"
			declaration.Members.Add(init)
			' Add empty fields for both SPIs.
			declaration.Members.Add(New CodeMemberField(GetType(ISmartPartInfo), "info1"))
			declaration.Members.Add(New CodeMemberField(GetType(ISmartPartInfo), "info2"))

			manager.Services.Add(GetType(CodeTypeDeclaration), declaration)

			serializer.Serialize(manager, info1)
			serializer.Serialize(manager, info2)

			Dim sw As StringWriter = New StringWriter()

			Dim codeProvider As VisualBasic.VBCodeProvider = New VisualBasic.VBCodeProvider()
			codeProvider.GenerateCodeFromType(declaration, sw, New CodeGeneratorOptions())

			sw.Flush()

			'Console.WriteLine(sw.ToString());

			codeProvider = New VisualBasic.VBCodeProvider()
			Dim results As CompilerResults = codeProvider.CompileAssemblyFromSource(New CompilerParameters(New String() {"System.dll", New Uri(GetType(SmartPartInfo).Assembly.CodeBase).LocalPath}), sw.ToString())

			Assert.IsFalse(results.Errors.HasErrors, ErrorsToString(results.Errors))
		End Sub

		Private Shared Function ErrorsToString(ByVal errors As CompilerErrorCollection) As String
			Dim sb As StringBuilder = New StringBuilder()
			For Each err As CompilerError In errors
				sb.AppendLine(err.ToString())
			Next err

			Return sb.ToString()
		End Function

#Region "Helper classes"

		Private Class MockSPI : Inherits SmartPartInfo
			Public Sub New(ByVal title As String, ByVal description As String)
				MyBase.New(title, description)
			End Sub
		End Class

		Private Class MockManager : Implements IDesignerSerializationManager
			Public Services As Dictionary(Of Type, Object) = New Dictionary(Of Type, Object)()
			Private stack As ContextStack = New ContextStack()
			Private components As ComponentCollection

			Public Sub New(ByVal components As ComponentCollection)
				Me.components = components
			End Sub

			Private Sub HideCompilerWarningsForUnusedInterfaceMethods()
				RaiseEvent ResolveName(Nothing, Nothing)
				RaiseEvent SerializationComplete(Nothing, Nothing)
			End Sub

#Region "IDesignerSerializationManager Members"

			Public Sub AddSerializationProvider(ByVal provider As IDesignerSerializationProvider) Implements IDesignerSerializationManager.AddSerializationProvider
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public ReadOnly Property Context() As ContextStack Implements IDesignerSerializationManager.Context
				Get
					Return stack
				End Get
			End Property

			Public Function CreateInstance(ByVal type As Type, ByVal arguments As System.Collections.ICollection, ByVal name As String, ByVal addToContainer As Boolean) As Object Implements IDesignerSerializationManager.CreateInstance
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function GetInstance(ByVal name As String) As Object Implements IDesignerSerializationManager.GetInstance
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function GetName(ByVal value As Object) As String Implements IDesignerSerializationManager.GetName
				For Each component As IComponent In components
					If components Is value Then
						Return component.Site.Name
					End If
				Next component

				Return Guid.NewGuid().ToString()
			End Function

			Public Function GetSerializer(ByVal objectType As Type, ByVal serializerType As Type) As Object Implements IDesignerSerializationManager.GetSerializer
				Dim attr As DesignerSerializerAttribute = CType(Attribute.GetCustomAttribute(objectType, GetType(DesignerSerializerAttribute)), DesignerSerializerAttribute)
				If attr Is Nothing Then
					If objectType Is GetType(Component) Then
						Dim t As Type = Type.GetType("System.ComponentModel.Design.Serialization.ComponentCodeDomSerializer, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")
						Return Activator.CreateInstance(t)
					End If
					Return Nothing
				Else
					Return CType(Activator.CreateInstance(Type.GetType(attr.SerializerTypeName)), CodeDomSerializer)
				End If
			End Function

			Public Overloads Function [GetType](ByVal typeName As String) As Type Implements IDesignerSerializationManager.GetType
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public ReadOnly Property Properties() As System.ComponentModel.PropertyDescriptorCollection Implements IDesignerSerializationManager.Properties
				Get
					Return TypeDescriptor.GetProperties(GetType(SmartPartInfo))
				End Get
			End Property

			Public Sub RemoveSerializationProvider(ByVal provider As IDesignerSerializationProvider) Implements IDesignerSerializationManager.RemoveSerializationProvider
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub ReportError(ByVal errorInformation As Object) Implements IDesignerSerializationManager.ReportError
				Console.WriteLine(errorInformation)
			End Sub

			Public Event ResolveName As ResolveNameEventHandler Implements IDesignerSerializationManager.ResolveName

			Public Event SerializationComplete As EventHandler Implements IDesignerSerializationManager.SerializationComplete

			Public Sub SetName(ByVal instance As Object, ByVal name As String) Implements IDesignerSerializationManager.SetName
				Throw New Exception("The method or operation is not implemented.")
			End Sub

#End Region

#Region "IServiceProvider Members"

			Public Function GetService(ByVal serviceType As Type) As Object Implements IServiceProvider.GetService
				If (Not Services.ContainsKey(serviceType)) Then
					Debug.Fail("Requested service not found " & serviceType.ToString())
				End If
				Dim service As Object = Nothing
				Services.TryGetValue(serviceType, service)

				Return service
			End Function

#End Region
		End Class

		Private Class MockDesignerHost : Implements IDesignerHost
			Private innerRootComponent As IComponent
			Private innerContainer As IContainer

			Public Sub New(ByVal aRootComponent As IComponent, ByVal aContainer As IContainer)
				Me.innerRootComponent = aRootComponent
				Me.innerContainer = aContainer
			End Sub

			Private Sub HideCompilerWarningsForUnusedInterfaceMethods()
				RaiseEvent Activated(Nothing, Nothing)
				RaiseEvent Deactivated(Nothing, Nothing)
				RaiseEvent LoadComplete(Nothing, Nothing)
				RaiseEvent TransactionClosed(Nothing, Nothing)
				RaiseEvent TransactionClosing(Nothing, Nothing)
				RaiseEvent TransactionOpened(Nothing, Nothing)
				RaiseEvent TransactionOpening(Nothing, Nothing)
			End Sub

#Region "IDesignerHost Members"

			Public Sub Activate() Implements IDesignerHost.Activate
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Event Activated As EventHandler Implements IDesignerHost.Activated

			Public ReadOnly Property Container() As System.ComponentModel.IContainer Implements IDesignerHost.Container
				Get
					Return Me.innerContainer
				End Get
			End Property

			Public Function CreateComponent(ByVal componentClass As Type, ByVal name As String) As System.ComponentModel.IComponent Implements IDesignerHost.CreateComponent
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function CreateComponent(ByVal componentClass As Type) As System.ComponentModel.IComponent Implements IDesignerHost.CreateComponent
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function CreateTransaction(ByVal description As String) As DesignerTransaction Implements IDesignerHost.CreateTransaction
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function CreateTransaction() As DesignerTransaction Implements IDesignerHost.CreateTransaction
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Event Deactivated As EventHandler Implements IDesignerHost.Deactivated

			Public Sub DestroyComponent(ByVal component As System.ComponentModel.IComponent) Implements IDesignerHost.DestroyComponent
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Function GetDesigner(ByVal component As System.ComponentModel.IComponent) As IDesigner Implements IDesignerHost.GetDesigner
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Overloads Function [GetType](ByVal typeName As String) As Type Implements IDesignerHost.GetType
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public ReadOnly Property InTransaction() As Boolean Implements IDesignerHost.InTransaction
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public Event LoadComplete As EventHandler Implements IDesignerHost.LoadComplete

			Public ReadOnly Property Loading() As Boolean Implements IDesignerHost.Loading
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public ReadOnly Property RootComponent() As System.ComponentModel.IComponent Implements IDesignerHost.RootComponent
				Get
					Return innerRootComponent
				End Get
			End Property

			Public ReadOnly Property RootComponentClassName() As String Implements IDesignerHost.RootComponentClassName
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public Event TransactionClosed As DesignerTransactionCloseEventHandler Implements IDesignerHost.TransactionClosed

			Public Event TransactionClosing As DesignerTransactionCloseEventHandler Implements IDesignerHost.TransactionClosing

			Public ReadOnly Property TransactionDescription() As String Implements IDesignerHost.TransactionDescription
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
			End Property

			Public Event TransactionOpened As EventHandler Implements IDesignerHost.TransactionOpened

			Public Event TransactionOpening As EventHandler Implements IDesignerHost.TransactionOpening

#End Region

#Region "IServiceContainer Members"

			Public Sub AddService(ByVal serviceType As Type, ByVal callback As ServiceCreatorCallback, ByVal promote As Boolean) Implements IServiceContainer.AddService
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub AddService(ByVal serviceType As Type, ByVal callback As ServiceCreatorCallback) Implements IServiceContainer.AddService
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub AddService(ByVal serviceType As Type, ByVal serviceInstance As Object, ByVal promote As Boolean) Implements IServiceContainer.AddService
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub AddService(ByVal serviceType As Type, ByVal serviceInstance As Object) Implements IServiceContainer.AddService
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub RemoveService(ByVal serviceType As Type, ByVal promote As Boolean) Implements IServiceContainer.RemoveService
				Throw New Exception("The method or operation is not implemented.")
			End Sub

			Public Sub RemoveService(ByVal serviceType As Type) Implements IServiceContainer.RemoveService
				Throw New Exception("The method or operation is not implemented.")
			End Sub

#End Region

#Region "IServiceProvider Members"

			Public Function GetService(ByVal serviceType As Type) As Object Implements IServiceProvider.GetService
				Throw New Exception("The method or operation is not implemented.")
			End Function

#End Region
		End Class

		Private Class MockReferenceService : Implements IReferenceService
			Private components As ComponentCollection

			Public Sub New(ByVal components As ComponentCollection)
				Me.components = components
			End Sub

#Region "IReferenceService Members"

			Public Function GetComponent(ByVal reference As Object) As IComponent Implements IReferenceService.GetComponent
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function GetName(ByVal reference As Object) As String Implements IReferenceService.GetName
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function GetReference(ByVal name As String) As Object Implements IReferenceService.GetReference
				Return components(name)
			End Function

			Public Function GetReferences(ByVal baseType As Type) As Object() Implements IReferenceService.GetReferences
				Throw New Exception("The method or operation is not implemented.")
			End Function

			Public Function GetReferences() As Object() Implements IReferenceService.GetReferences
				Throw New Exception("The method or operation is not implemented.")
			End Function

#End Region
		End Class

#End Region

	End Class
End Namespace
