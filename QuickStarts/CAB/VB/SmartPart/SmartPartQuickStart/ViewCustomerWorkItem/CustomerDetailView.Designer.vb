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
Namespace SmartPartQuickStart.ViewCustomerWorkItem
	Partial Public Class CustomerDetailView
		Inherits SmartPartQuickStart.TitledSmartPart

		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (Not components Is Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Dim lastNameLabel As System.Windows.Forms.Label
			Dim idLabel As System.Windows.Forms.Label
			Dim fullNameLabel As System.Windows.Forms.Label
			Dim firstNameLabel As System.Windows.Forms.Label
			Dim addressLabel As System.Windows.Forms.Label
			Me.customerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.lastNameTextBox = New System.Windows.Forms.TextBox()
			Me.idTextBox = New System.Windows.Forms.TextBox()
			Me.fullNameTextBox = New System.Windows.Forms.TextBox()
			Me.firstNameTextBox = New System.Windows.Forms.TextBox()
			Me.addressTextBox = New System.Windows.Forms.TextBox()
			Me.commentsButton = New System.Windows.Forms.Button()
			lastNameLabel = New System.Windows.Forms.Label()
			idLabel = New System.Windows.Forms.Label()
			fullNameLabel = New System.Windows.Forms.Label()
			firstNameLabel = New System.Windows.Forms.Label()
			addressLabel = New System.Windows.Forms.Label()
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' lastNameLabel
			' 
			lastNameLabel.AutoSize = True
			lastNameLabel.Location = New System.Drawing.Point(27, 154)
			lastNameLabel.Name = "lastNameLabel"
			lastNameLabel.Size = New System.Drawing.Size(57, 13)
			lastNameLabel.TabIndex = 10
			lastNameLabel.Text = "Last Name:"
			' 
			' idLabel
			' 
			idLabel.AutoSize = True
			idLabel.Location = New System.Drawing.Point(27, 127)
			idLabel.Name = "idLabel"
			idLabel.Size = New System.Drawing.Size(15, 13)
			idLabel.TabIndex = 8
			idLabel.Text = "Id:"
			' 
			' fullNameLabel
			' 
			fullNameLabel.AutoSize = True
			fullNameLabel.Location = New System.Drawing.Point(27, 100)
			fullNameLabel.Name = "fullNameLabel"
			fullNameLabel.Size = New System.Drawing.Size(53, 13)
			fullNameLabel.TabIndex = 6
			fullNameLabel.Text = "Full Name:"
			' 
			' firstNameLabel
			' 
			firstNameLabel.AutoSize = True
			firstNameLabel.Location = New System.Drawing.Point(27, 73)
			firstNameLabel.Name = "firstNameLabel"
			firstNameLabel.Size = New System.Drawing.Size(56, 13)
			firstNameLabel.TabIndex = 4
			firstNameLabel.Text = "First Name:"
			' 
			' addressLabel
			' 
			addressLabel.AutoSize = True
			addressLabel.Location = New System.Drawing.Point(27, 46)
			addressLabel.Name = "addressLabel"
			addressLabel.Size = New System.Drawing.Size(44, 13)
			addressLabel.TabIndex = 2
			addressLabel.Text = "Address:"
			' 
			' customerBindingSource
			' 
			Me.customerBindingSource.DataSource = GetType(SmartPartQuickStart.Customer)
			' 
			' lastNameTextBox
			' 
			Me.lastNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "LastName", True))
			Me.lastNameTextBox.Location = New System.Drawing.Point(91, 151)
			Me.lastNameTextBox.Name = "lastNameTextBox"
			Me.lastNameTextBox.Size = New System.Drawing.Size(100, 20)
			Me.lastNameTextBox.TabIndex = 11
			' 
			' idTextBox
			' 
			Me.idTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "Id", True))
			Me.idTextBox.Location = New System.Drawing.Point(91, 124)
			Me.idTextBox.Name = "idTextBox"
			Me.idTextBox.Size = New System.Drawing.Size(100, 20)
			Me.idTextBox.TabIndex = 9
			' 
			' fullNameTextBox
			' 
			Me.fullNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "FullName", True))
			Me.fullNameTextBox.Location = New System.Drawing.Point(91, 97)
			Me.fullNameTextBox.Name = "fullNameTextBox"
			Me.fullNameTextBox.Size = New System.Drawing.Size(100, 20)
			Me.fullNameTextBox.TabIndex = 7
			' 
			' firstNameTextBox
			' 
			Me.firstNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "FirstName", True))
			Me.firstNameTextBox.Location = New System.Drawing.Point(91, 70)
			Me.firstNameTextBox.Name = "firstNameTextBox"
			Me.firstNameTextBox.Size = New System.Drawing.Size(100, 20)
			Me.firstNameTextBox.TabIndex = 5
			' 
			' addressTextBox
			' 
			Me.addressTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "Address", True))
			Me.addressTextBox.Location = New System.Drawing.Point(91, 43)
			Me.addressTextBox.Name = "addressTextBox"
			Me.addressTextBox.Size = New System.Drawing.Size(100, 20)
			Me.addressTextBox.TabIndex = 3
			' 
			' commentsButton
			' 
			Me.commentsButton.Location = New System.Drawing.Point(18, 203)
			Me.commentsButton.Name = "commentsButton"
			Me.commentsButton.Size = New System.Drawing.Size(75, 23)
			Me.commentsButton.TabIndex = 12
			Me.commentsButton.Text = "Comments"
			' 
			' CustomerDetailView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.Controls.Add(Me.commentsButton)
			Me.Controls.Add(Me.lastNameTextBox)
			Me.Controls.Add(lastNameLabel)
			Me.Controls.Add(addressLabel)
			Me.Controls.Add(Me.addressTextBox)
			Me.Controls.Add(firstNameLabel)
			Me.Controls.Add(Me.firstNameTextBox)
			Me.Controls.Add(fullNameLabel)
			Me.Controls.Add(Me.fullNameTextBox)
			Me.Controls.Add(idLabel)
			Me.Controls.Add(Me.idTextBox)
			Me.Name = "CustomerDetailView"
			Me.Size = New System.Drawing.Size(303, 229)
			Me.Title = "Customer Details"
			Me.Controls.SetChildIndex(Me.idTextBox, 0)
			Me.Controls.SetChildIndex(idLabel, 0)
			Me.Controls.SetChildIndex(Me.fullNameTextBox, 0)
			Me.Controls.SetChildIndex(fullNameLabel, 0)
			Me.Controls.SetChildIndex(Me.firstNameTextBox, 0)
			Me.Controls.SetChildIndex(firstNameLabel, 0)
			Me.Controls.SetChildIndex(Me.addressTextBox, 0)
			Me.Controls.SetChildIndex(addressLabel, 0)
			Me.Controls.SetChildIndex(lastNameLabel, 0)
			Me.Controls.SetChildIndex(Me.lastNameTextBox, 0)
			Me.Controls.SetChildIndex(Me.commentsButton, 0)
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private customerBindingSource As System.Windows.Forms.BindingSource
		Private lastNameTextBox As System.Windows.Forms.TextBox
		Private idTextBox As System.Windows.Forms.TextBox
		Private fullNameTextBox As System.Windows.Forms.TextBox
		Private firstNameTextBox As System.Windows.Forms.TextBox
		Private addressTextBox As System.Windows.Forms.TextBox
		Private WithEvents commentsButton As System.Windows.Forms.Button

	End Class
End Namespace
