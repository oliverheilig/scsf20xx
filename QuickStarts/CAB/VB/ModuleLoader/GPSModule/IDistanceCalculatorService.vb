Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace GPSModule
	Public Interface IDistanceCalculatorService
		Function ComputeDistance(ByVal latitude As Integer, ByVal longitude As Integer) As Integer
	End Interface
End Namespace
