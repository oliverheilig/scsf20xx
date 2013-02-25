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
''' A default implementation of the <see cref="IModule"/> interface.
''' </summary>
Public MustInherit Class ModuleInit : Implements IModule
	''' <summary>
	''' See <see cref="IModule.AddServices"/> for more information.
	''' </summary>
	Public Overridable Sub AddServices() Implements IModule.AddServices
	End Sub

	''' <summary>
	''' See <see cref="IModule.Load"/> for more information.
	''' </summary>
	Public Overridable Sub Load() Implements IModule.Load
	End Sub

End Class
