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
Imports Microsoft.Practices.CompositeUI.Services
Imports Microsoft.Practices.CompositeUI.Utility

''' <summary>
''' Indicates that a class extends <see cref="WorkItem"/> classes. The class
''' must implement <see cref="IWorkItemExtension"/>.
''' </summary>
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=True, Inherited:=True)> _
Public Class WorkItemExtensionAttribute : Inherits Attribute
	Private innerWorkItemType As Type

	''' <summary>
	''' Initializes the attribute with the type of the work item to extend.
	''' </summary>
	Public Sub New(ByVal workItemType As Type)
		Guard.ArgumentNotNull(workItemType, "workItemType")

		Me.innerWorkItemType = workItemType
	End Sub

	''' <summary>
	''' The type of the <see cref="WorkItem"/> to extend.
	''' </summary>
	Public ReadOnly Property WorkItemType() As Type
		Get
			Return innerWorkItemType
		End Get
	End Property
End Class

