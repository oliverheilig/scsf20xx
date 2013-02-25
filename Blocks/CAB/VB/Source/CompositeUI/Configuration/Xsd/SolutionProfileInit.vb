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
Namespace Configuration.Xsd
	''' <summary>
	''' Partial class to initialize solution profile.
	''' </summary>
	Partial Public Class SolutionProfileElement
		''' <summary>
		''' Constructor used to initialize empty moduleinfo.
		''' </summary>
		Public Sub New()
			modulesField = New ModuleInfoElement() {}
		End Sub
	End Class
End Namespace