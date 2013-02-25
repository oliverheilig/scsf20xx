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
	''' Provides information about a SmartPart to an <see cref="IWorkspace"/>.
	''' </summary>
	Public Interface ISmartPartInfo
		''' <summary>
		''' Description of this SmartPart.
		''' </summary>
		Property Description() As String

		''' <summary>
		''' Title of this SmartPart.
		''' </summary>
		Property Title() As String
	End Interface
End Namespace