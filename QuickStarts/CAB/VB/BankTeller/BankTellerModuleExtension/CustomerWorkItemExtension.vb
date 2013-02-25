Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI
Imports BankTellerModule
Imports Microsoft.Practices.CompositeUI.WinForms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.Windows.Forms
Imports BankTellerCommon
Imports Microsoft.Practices.CompositeUI.UIElements

Namespace CustomerMapExtensionModule
	<WorkItemExtension(GetType(CustomerWorkItem))> _
	Public Class CustomerWorkItemExtension : Inherits WorkItemExtension
		Private mapView As CustomerMap

		Protected Overrides Sub OnActivated()
			If mapView Is Nothing Then
				mapView = WorkItem.Items.AddNew(Of CustomerMap)()

				Dim info As TabSmartPartInfo = New TabSmartPartInfo()
				info.Title = "Customer Map"
				info.Description = "Map of the customer location"
				WorkItem.Workspaces(CustomerWorkItem.CUSTOMERDETAIL_TABWORKSPACE).Show(mapView, info)
			End If
		End Sub
	End Class
End Namespace
