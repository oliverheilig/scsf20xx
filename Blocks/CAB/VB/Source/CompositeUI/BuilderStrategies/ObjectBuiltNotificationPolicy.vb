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
Imports Microsoft.Practices.ObjectBuilder

Namespace BuilderStrategies
	''' <summary>
	''' 
	''' </summary>
	Public Class ObjectBuiltNotificationPolicy : Implements IBuilderPolicy
		''' <summary>
		''' 
		''' </summary>
		''' <param name="item"></param>
		Public Delegate Sub ItemNotification(ByVal item As Object)

		''' <summary>
		''' 
		''' </summary>
		Public AddedDelegates As Dictionary(Of WorkItem, ItemNotification) = New Dictionary(Of WorkItem, ItemNotification)()

		''' <summary>
		''' 
		''' </summary>
		Public RemovedDelegates As Dictionary(Of WorkItem, ItemNotification) = New Dictionary(Of WorkItem, ItemNotification)()
	End Class
End Namespace
