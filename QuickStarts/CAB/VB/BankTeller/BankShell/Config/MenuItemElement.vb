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
Imports System.Configuration
Imports System.Windows.Forms

Namespace BankShell
	Friend Class MenuItemElement : Inherits ConfigurationElement
		<ConfigurationProperty("commandname", IsRequired:=False)> _
		Public Property CommandName() As String
			Get
				Return CStr(Me("commandname"))
			End Get
			Set(ByVal value As String)
				Me("commandname") = Value
			End Set
		End Property

		<ConfigurationProperty("key", IsRequired:=False)> _
		Public Property Key() As String
			Get
				Return CStr(Me("key"))
			End Get
			Set(ByVal value As String)
				Me("key") = Value
			End Set
		End Property

		<ConfigurationProperty("id", IsRequired:=False, IsKey:=True)> _
		Public Property ID() As Integer
			Get
				Return CInt(Me("id"))
			End Get
			Set(ByVal value As Integer)
				Me("id") = Value
			End Set
		End Property

		<ConfigurationProperty("label", IsRequired:=True)> _
		Public Property Label() As String
			Get
				Return CStr(Me("label"))
			End Get
			Set(ByVal value As String)
				Me("label") = Value
			End Set
		End Property

		<ConfigurationProperty("site", IsRequired:=True)> _
		Public Property Site() As String
			Get
				Return CStr(Me("site"))
			End Get
			Set(ByVal value As String)
				Me("site") = Value
			End Set
		End Property

		<ConfigurationProperty("register", IsRequired:=False)> _
		Public Property Register() As Boolean
			Get
				Return CBool(Me("register"))
			End Get
			Set(ByVal value As Boolean)
				Me("register") = Value
			End Set
		End Property

		<ConfigurationProperty("registrationsite", IsRequired:=False)> _
		Public Property RegistrationSite() As String
			Get
				Return CStr(Me("registrationsite"))
			End Get
			Set(ByVal value As String)
				Me("registrationsite") = Value
			End Set
		End Property

		Public Function ToMenuItem() As ToolStripMenuItem
			Dim result As ToolStripMenuItem = New ToolStripMenuItem()
			result.Text = Label

			If (Not String.IsNullOrEmpty(Key)) Then
				result.ShortcutKeys = CType(System.Enum.Parse(GetType(Keys), Key), Keys)
			End If

			Return result
		End Function
	End Class
End Namespace
