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
Imports System.ComponentModel
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI.SmartParts
Imports System.Drawing


''' <summary>
''' Implements a Workspace which shows the smarparts in MDI forms.
''' </summary>
Public Class MdiWorkspace : Inherits WindowWorkspace
	Private innerParentMdiForm As Form

	''' <summary>
	''' Constructor specifying the parent form of the MDI child.
	''' </summary>
	Public Sub New(ByVal parentForm As Form)
		MyBase.New()
		Me.innerParentMdiForm = parentForm
		Me.innerParentMdiForm.IsMdiContainer = True
	End Sub

	''' <summary>
	''' Gets the parent MDI form.
	''' </summary>
	Public ReadOnly Property ParentMdiForm() As Form
		Get
			Return innerParentMdiForm
		End Get
	End Property

	''' <summary>
	''' Shows the form as a child of the specified <see cref="ParentMdiForm"/>.
	''' </summary>
	''' <param name="smartPart">The <see cref="Control"/> to show in the workspace.</param>
	''' <param name="smartPartInfo">The information to use to show the smart part.</param>
	Protected Overrides Sub OnShow(ByVal smartPart As Control, ByVal smartPartInfo As WindowSmartPartInfo)
		Dim mdiChild As Form = Me.GetOrCreateForm(smartPart)
		mdiChild.MdiParent = innerParentMdiForm

		Me.SetWindowProperties(mdiChild, smartPartInfo)
		mdiChild.Show()
		Me.SetWindowLocation(mdiChild, smartPartInfo)
		mdiChild.BringToFront()
	End Sub
End Class
