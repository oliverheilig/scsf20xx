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
Imports System.Windows.Forms


''' <summary>
''' Defines a service to deal with the <see cref="WorkItem"/> activation and deactivation based
''' on its contained <see cref="Control"/> state.
''' </summary>
Public Interface IControlActivationService
	''' <summary>
	''' Notifies that a <see cref="Control"/> has been added to the container.
	''' </summary>
	''' <param name="control">The control in which to monitor the OnEnter event.</param>
	Sub ControlAdded(ByVal control As Control)

	''' <summary>
	''' Notifies that a control has been removed from the container.
	''' </summary>
	''' <param name="control">The control being monitored.</param>
	Sub ControlRemoved(ByVal control As Control)
End Interface
