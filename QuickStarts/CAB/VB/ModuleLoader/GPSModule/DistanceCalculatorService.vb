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
Imports System.Text
Imports Microsoft.Practices.CompositeUI
Imports System.Windows.Forms

Namespace GPSModule
	<Service(GetType(IDistanceCalculatorService), AddOnDemand:=True)> _
	Public Class DistanceCalculatorService
		Implements IDistanceCalculatorService

		Public Sub New()
			MessageBox.Show("This is DistanceCalculatorService being constructed")
		End Sub

		Public Function ComputeDistance(ByVal latitude As Integer, ByVal longitude As Integer) As Integer Implements IDistanceCalculatorService.ComputeDistance
			Return 1234
		End Function
	End Class
End Namespace
