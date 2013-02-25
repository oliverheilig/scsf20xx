Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports BankTellerModule
Imports Microsoft.Practices.CompositeUI
Imports System.Web
Imports BankTellerCommon

Namespace CustomerMapExtensionModule
	<SmartPart()> _
	Partial Public Class CustomerMap : Inherits UserControl
		Private innerCustomer As Customer
		Private mapLoaded As Boolean = False

		Private Const mapUrlFormat As String = "http://maps.msn.com/home.aspx?strt1={0}&city1={2}&stnm1={3}&zipc1={4}"

		<State(StateConstants.CUSTOMER)> _
		Public WriteOnly Property Customer() As Customer
			Set(ByVal value As Customer)
				innerCustomer = value
			End Set
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub

		Protected Overrides Sub OnVisibleChanged(ByVal e As EventArgs)
			MyBase.OnVisibleChanged(e)

			If Me.Visible = True AndAlso mapLoaded = False Then
				LoadMap()
				mapLoaded = True
			End If
		End Sub

		Private Sub LoadMap()
			If Not innerCustomer Is Nothing Then
				Dim url As String = String.Format(mapUrlFormat, innerCustomer.Address1, innerCustomer.Address2, innerCustomer.City, innerCustomer.State, innerCustomer.ZipCode)
				browser.Navigate(Uri.EscapeUriString(url))
			End If
		End Sub
	End Class
End Namespace
