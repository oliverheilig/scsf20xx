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
''' Defines a set of methods to be implemented by modules deployed in the application.
''' </summary>
Public Interface IModule
	''' <summary>
	''' Allows the module to add services to the root <see cref="WorkItem"/> on startup.
	''' </summary>
	Sub AddServices()

	''' <summary>
	''' Allows the module to be notified that it has been loaded.
	''' </summary>
	Sub Load()

End Interface
