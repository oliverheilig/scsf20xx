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
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI

Namespace GPSModule
	Partial Public Class GPSView
		Inherits UserControl

		Private gpsService As IGPSService

		<ServiceDependency()> _
		Public WriteOnly Property GPS() As IGPSService
			Set(ByVal value As IGPSService)
				gpsService = value
			End Set
		End Property

		Private innerWorkItem As WorkItem

		<ServiceDependency()> _
		Public WriteOnly Property WorkItem() As WorkItem
			Set(ByVal value As WorkItem)
				innerWorkItem = value
			End Set
		End Property

		' Note that we didn't added a ServiceDependencyAttribute for the
		' IDistanceCalculatorService, because that way the service would be created
		' when this view is added to the work item. This is just for the purposes of this example.
		Protected ReadOnly Property CalcService() As IDistanceCalculatorService
			Get
				Return innerWorkItem.Services.Get(Of IDistanceCalculatorService)()
			End Get
		End Property

		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub cmdGetDistance_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdGetDistance.Click
			txtDistance.Text = CalcService.ComputeDistance(gpsService.GetLatitude(), gpsService.GetLongitude()).ToString()
		End Sub

		Private Sub cmdGetLatitude_Click(ByVal sender As Object, ByVal e As EventArgs) Handles cmdGetLatitude.Click
			txtLatitude.Text = gpsService.GetLatitude().ToString()
		End Sub
	End Class
End Namespace
