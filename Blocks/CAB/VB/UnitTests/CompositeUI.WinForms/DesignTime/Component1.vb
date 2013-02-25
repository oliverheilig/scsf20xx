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
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Text

Namespace DesignTime
	<System.ComponentModel.DesignerCategory("Component")> _
	Partial Friend Class Component1
		Inherits Component

		Public Sub New()
			InitializeComponent()
		End Sub

		Public Sub New(ByVal container As IContainer)
			container.Add(Me)

			InitializeComponent()
		End Sub
	End Class
End Namespace
