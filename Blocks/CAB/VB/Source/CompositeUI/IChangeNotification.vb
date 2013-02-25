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


''' <summary>
''' Defines an event that is implemented by classes which offer
''' notifications of data changes.
''' </summary>
Public Interface IChangeNotification
	''' <summary>
	''' Notifies subscribers that the state of the element has changed.
	''' </summary>
	Event Changed As EventHandler
End Interface
