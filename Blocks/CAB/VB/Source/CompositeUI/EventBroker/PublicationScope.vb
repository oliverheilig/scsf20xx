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

Namespace EventBroker
	''' <summary>
	''' Defines the scope for a publication of an <see cref="EventTopic"/>.
	''' </summary>
	Public Enum PublicationScope
		''' <summary>
		''' Indicates that the topic should be fired on all the <see cref="WorkItem"/> instances,
		''' regarding where the publication firing occurred.
		''' </summary>
		[Global]
		''' <summary>
		''' Indicates that the topic should be fired only in the <see cref="WorkItem"/> instance where 
		''' the publication firing occurred.
		''' </summary>
		WorkItem
		''' <summary>
		''' Indicates that the topic should be fired in the <see cref="WorkItem"/> instance where 
		''' the publication firing occurred, and in all the <see cref="WorkItem"/> descendants.
		''' </summary>
		Descendants
	End Enum
End Namespace
