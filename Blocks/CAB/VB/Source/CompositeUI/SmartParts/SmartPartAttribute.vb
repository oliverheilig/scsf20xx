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

Namespace SmartParts
	''' <summary>
	''' This attribute flags an object as a smart part.
	''' The smart part will then be added to the WorkItem.
	''' </summary>
	<AttributeUsage(AttributeTargets.Class)> _
	Public NotInheritable Class SmartPartAttribute : Inherits Attribute
	End Class
End Namespace