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
Imports System.Diagnostics


Friend Class MockListener
	Inherits TraceListener

	Public WasCalled As Boolean = False

	Public Overloads Overrides Sub Write(ByVal message As String)
		WasCalled = True
	End Sub

	Public Overloads Overrides Sub WriteLine(ByVal message As String)
		WasCalled = True
	End Sub
End Class

