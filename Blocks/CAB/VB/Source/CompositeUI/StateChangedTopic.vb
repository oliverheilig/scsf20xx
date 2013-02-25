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
Imports System.Globalization


Friend Class StateChangedTopic
	Private Const TOPIC_PREFIX As String = "topic://WorkitemStateChanged/{0}"

	Private Sub New()
	End Sub
	Friend Shared Function BuildStateChangedTopicString(ByVal stateName As String) As String
		Return String.Format(CultureInfo.InvariantCulture, TOPIC_PREFIX, stateName)
	End Function
End Class
