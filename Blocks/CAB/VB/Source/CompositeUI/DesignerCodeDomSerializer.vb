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
Imports System
Imports System.ComponentModel.Design.Serialization
Imports System.CodeDom
Imports System.Collections.Generic
Imports System.ComponentModel.Design
Imports System.ComponentModel


''' <summary>
''' Helper base class for custom serializers.
''' </summary>
Friend Class DesignerCodeDomSerializer : Inherits CodeDomSerializer
	Protected Shared Function FindMember(Of TMember As CodeObject)(ByVal declaration As CodeTypeDeclaration, ByVal memberName As String) As TMember
		For Each member As CodeTypeMember In declaration.Members
			Dim method As TMember = TryCast(member, TMember)
			If Not method Is Nothing AndAlso member.Name = memberName Then
				Return method
			End If
		Next member

		Return Nothing
	End Function

	Protected Shared Function FindInitializeComponent(ByVal declaration As CodeTypeDeclaration) As CodeMemberMethod
		Return FindMember(Of CodeMemberMethod)(declaration, "InitializeComponent")
	End Function

	Protected Shared Sub AddStatementToInitializeComponent(ByVal declaration As CodeTypeDeclaration, ByVal statement As CodeStatement)
		Dim init As CodeMemberMethod = FindInitializeComponent(declaration)
		If Not init Is Nothing Then
			init.Statements.Add(statement)
		End If
	End Sub

	Protected Shared Function GetComponentsOnDesignerSurface(ByVal manager As IDesignerSerializationManager, ByVal rootComponent As Object) As List(Of String)
		' Through the designer host we can inspect the components on the surface.
		Dim host As IDesignerHost = CType(manager.GetService(GetType(IDesignerHost)), IDesignerHost)
		Dim names As List(Of String) = New List(Of String)(host.Container.Components.Count)
		For Each component As IComponent In host.Container.Components
			' Beware that the root component also appears on the list.
			If Not component Is rootComponent Then
				names.Add(component.Site.Name)
			End If
		Next component

		Return names
	End Function

	Protected Shared Sub RemoveFromStatements(Of TCodeObject As CodeObject)(ByVal statements As CodeStatementCollection, ByVal shouldRemove As Predicate(Of TCodeObject))
		Dim toRemove As List(Of CodeStatement) = New List(Of CodeStatement)()
		For Each statement As CodeStatement In statements
			Dim typedStatement As TCodeObject = TryCast(statement, TCodeObject)
			Dim expression As CodeExpressionStatement = TryCast(statement, CodeExpressionStatement)
			If typedStatement Is Nothing AndAlso Not expression Is Nothing Then
				typedStatement = TryCast(expression.Expression, TCodeObject)
			End If

			If Not typedStatement Is Nothing AndAlso shouldRemove(typedStatement) Then
				toRemove.Add(statement)
			End If
		Next statement

		For Each statement As CodeStatement In toRemove
			statements.Remove(statement)
		Next statement
	End Sub

	Protected Shared Sub RemoveFromInitializeComponent(Of TCodeObject As CodeObject)(ByVal declaration As CodeTypeDeclaration, ByVal shouldRemove As Predicate(Of TCodeObject))
		Dim initialize As CodeMemberMethod = FindInitializeComponent(declaration)

		If Not initialize Is Nothing Then
			RemoveFromStatements(Of TCodeObject)(initialize.Statements, shouldRemove)
		End If
	End Sub

	Protected Function FindCode(Of TCodeObject As CodeObject)(ByVal statements As CodeStatementCollection, ByVal matchesFilter As Predicate(Of TCodeObject)) As List(Of TCodeObject)
		Dim values As List(Of TCodeObject) = New List(Of TCodeObject)()
		For Each statement As CodeStatement In statements
			Dim typedStatement As TCodeObject = TryCast(statement, TCodeObject)
			Dim expression As CodeExpressionStatement = TryCast(statement, CodeExpressionStatement)
			If typedStatement Is Nothing AndAlso Not expression Is Nothing Then
				typedStatement = TryCast(expression.Expression, TCodeObject)
			End If

			If Not typedStatement Is Nothing AndAlso matchesFilter(typedStatement) Then
				values.Add(typedStatement)
			End If
		Next statement

		Return values
	End Function

	Protected Shared Function FindFirstCode(Of TCodeObject As CodeObject)(ByVal statements As CodeStatementCollection, ByVal matchesFilter As Predicate(Of TCodeObject)) As TCodeObject
		For Each statement As CodeStatement In statements
			Dim typedStatement As TCodeObject = TryCast(statement, TCodeObject)
			Dim expression As CodeExpressionStatement = TryCast(statement, CodeExpressionStatement)
			If typedStatement Is Nothing AndAlso Not expression Is Nothing Then
				typedStatement = TryCast(expression.Expression, TCodeObject)
			End If

			If Not typedStatement Is Nothing AndAlso matchesFilter(typedStatement) Then
				Return typedStatement
			End If
		Next statement

		Return Nothing
	End Function
End Class
