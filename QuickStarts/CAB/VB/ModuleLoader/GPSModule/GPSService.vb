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

Namespace GPSModule
	' Declare a service published in the module, along with
	' its contract registration.
	<Service(GetType(IGPSService))> _
	Public Class GPService
		Implements IGPSService

		Public Function GetLatitude() As Integer Implements IGPSService.GetLatitude
			Return 42
		End Function

		Public Function GetLongitude() As Integer Implements IGPSService.GetLongitude
			Return 125
		End Function
	End Class
End Namespace
