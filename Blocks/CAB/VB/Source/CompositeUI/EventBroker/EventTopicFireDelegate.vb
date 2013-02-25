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
Imports System.Diagnostics
Namespace EventBroker
	''' <summary>
	''' Represents the signature for the subscription objects to get called from the <see cref="EventTopic"/> during
	''' a firing sequence.
	''' </summary>
	''' <param name="sender">The publisher object firing the topic.</param>
	''' <param name="e">The <see cref="EventArgs"/> data to be passed to the subscribers.</param>
	''' <param name="exceptions">An <see cref="Exception"/> list where a <see cref="Subscription"/> should 
	''' register the exceptions that might occur when executing the subcription code.</param>
	''' <param name="traceSource">The <see cref="TraceSource"/> to use for reporting information.</param>
	Friend Delegate Sub EventTopicFireDelegate(ByVal sender As Object, ByVal e As EventArgs, ByVal exceptions As List(Of Exception), ByVal traceSource As TraceSource)
End Namespace