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
Namespace BankTellerModule
	Partial Public Class CustomerDetailView
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
			Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CustomerDetailView))
			Me.customerBindingSource = New System.Windows.Forms.BindingSource(Me.components)
			Me.label4 = New System.Windows.Forms.Label()
			Me.label5 = New System.Windows.Forms.Label()
			Me.txtAddress1 = New System.Windows.Forms.TextBox()
			Me.txtAddress2 = New System.Windows.Forms.TextBox()
			Me.txtCity = New System.Windows.Forms.TextBox()
			Me.label6 = New System.Windows.Forms.Label()
			Me.txtState = New System.Windows.Forms.TextBox()
			Me.label7 = New System.Windows.Forms.Label()
			Me.label8 = New System.Windows.Forms.Label()
			Me.txtZip = New System.Windows.Forms.TextBox()
			Me.label9 = New System.Windows.Forms.Label()
			Me.txtPhone1 = New System.Windows.Forms.TextBox()
			Me.txtPhone2 = New System.Windows.Forms.TextBox()
			Me.txtEmail = New System.Windows.Forms.TextBox()
			Me.label10 = New System.Windows.Forms.Label()
			Me.label11 = New System.Windows.Forms.Label()
			Me.label12 = New System.Windows.Forms.Label()
			Me.detailsCommands = New System.Windows.Forms.ToolStrip()
			Me.showCommentsButton = New System.Windows.Forms.ToolStripButton()
			Me.label1 = New System.Windows.Forms.Label()
			Me.lastNameTextBox = New System.Windows.Forms.TextBox()
			Me.label2 = New System.Windows.Forms.Label()
			Me.firstNameTextBox = New System.Windows.Forms.TextBox()
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.detailsCommands.SuspendLayout()
			Me.SuspendLayout()
			' 
			' customerBindingSource
			' 
			Me.customerBindingSource.DataSource = GetType(BankTellerCommon.Customer)
			' 
			' label4
			' 
			Me.label4.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.label4.BackColor = System.Drawing.Color.LightBlue
			Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label4.Location = New System.Drawing.Point(4, 4)
			Me.label4.Name = "label4"
			Me.label4.Padding = New System.Windows.Forms.Padding(2)
			Me.label4.Size = New System.Drawing.Size(438, 17)
			Me.label4.TabIndex = 5
			Me.label4.Text = "Address"
			' 
			' label5
			' 
			Me.label5.AutoSize = True
			Me.label5.Location = New System.Drawing.Point(14, 60)
			Me.label5.Name = "label5"
			Me.label5.Size = New System.Drawing.Size(48, 13)
			Me.label5.TabIndex = 6
			Me.label5.Text = "Address:"
			' 
			' txtAddress1
			' 
			Me.txtAddress1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "Address1", True))
			Me.txtAddress1.Location = New System.Drawing.Point(69, 57)
			Me.txtAddress1.Name = "txtAddress1"
			Me.txtAddress1.Size = New System.Drawing.Size(370, 20)
			Me.txtAddress1.TabIndex = 7
			' 
			' txtAddress2
			' 
			Me.txtAddress2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "Address2", True))
			Me.txtAddress2.Location = New System.Drawing.Point(69, 83)
			Me.txtAddress2.Name = "txtAddress2"
			Me.txtAddress2.Size = New System.Drawing.Size(370, 20)
			Me.txtAddress2.TabIndex = 8
			' 
			' txtCity
			' 
			Me.txtCity.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "City", True))
			Me.txtCity.Location = New System.Drawing.Point(68, 110)
			Me.txtCity.Name = "txtCity"
			Me.txtCity.Size = New System.Drawing.Size(149, 20)
			Me.txtCity.TabIndex = 9
			' 
			' label6
			' 
			Me.label6.AutoSize = True
			Me.label6.Location = New System.Drawing.Point(35, 113)
			Me.label6.Name = "label6"
			Me.label6.Size = New System.Drawing.Size(27, 13)
			Me.label6.TabIndex = 10
			Me.label6.Text = "City:"
			' 
			' txtState
			' 
			Me.txtState.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "State", True))
			Me.txtState.Location = New System.Drawing.Point(277, 110)
			Me.txtState.Name = "txtState"
			Me.txtState.Size = New System.Drawing.Size(62, 20)
			Me.txtState.TabIndex = 11
			' 
			' label7
			' 
			Me.label7.AutoSize = True
			Me.label7.Location = New System.Drawing.Point(240, 112)
			Me.label7.Name = "label7"
			Me.label7.Size = New System.Drawing.Size(35, 13)
			Me.label7.TabIndex = 12
			Me.label7.Text = "State:"
			' 
			' label8
			' 
			Me.label8.AutoSize = True
			Me.label8.Location = New System.Drawing.Point(345, 113)
			Me.label8.Name = "label8"
			Me.label8.Size = New System.Drawing.Size(25, 13)
			Me.label8.TabIndex = 13
			Me.label8.Text = "Zip:"
			' 
			' txtZip
			' 
			Me.txtZip.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "ZipCode", True))
			Me.txtZip.Location = New System.Drawing.Point(372, 109)
			Me.txtZip.Name = "txtZip"
			Me.txtZip.Size = New System.Drawing.Size(67, 20)
			Me.txtZip.TabIndex = 14
			' 
			' label9
			' 
			Me.label9.Anchor = (CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles))
			Me.label9.BackColor = System.Drawing.Color.LightBlue
			Me.label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.label9.Location = New System.Drawing.Point(3, 142)
			Me.label9.Name = "label9"
			Me.label9.Padding = New System.Windows.Forms.Padding(2)
			Me.label9.Size = New System.Drawing.Size(438, 17)
			Me.label9.TabIndex = 15
			Me.label9.Text = "Contact"
			' 
			' txtPhone1
			' 
			Me.txtPhone1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "Phone1", True))
			Me.txtPhone1.Location = New System.Drawing.Point(69, 163)
			Me.txtPhone1.Name = "txtPhone1"
			Me.txtPhone1.Size = New System.Drawing.Size(148, 20)
			Me.txtPhone1.TabIndex = 16
			' 
			' txtPhone2
			' 
			Me.txtPhone2.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "Phone2", True))
			Me.txtPhone2.Location = New System.Drawing.Point(286, 163)
			Me.txtPhone2.Name = "txtPhone2"
			Me.txtPhone2.Size = New System.Drawing.Size(153, 20)
			Me.txtPhone2.TabIndex = 17
			' 
			' txtEmail
			' 
			Me.txtEmail.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "EmailAddress", True))
			Me.txtEmail.Location = New System.Drawing.Point(69, 189)
			Me.txtEmail.Name = "txtEmail"
			Me.txtEmail.Size = New System.Drawing.Size(370, 20)
			Me.txtEmail.TabIndex = 18
			' 
			' label10
			' 
			Me.label10.AutoSize = True
			Me.label10.Location = New System.Drawing.Point(12, 166)
			Me.label10.Name = "label10"
			Me.label10.Size = New System.Drawing.Size(50, 13)
			Me.label10.TabIndex = 19
			Me.label10.Text = "Phone 1:"
			' 
			' label11
			' 
			Me.label11.AutoSize = True
			Me.label11.Location = New System.Drawing.Point(234, 166)
			Me.label11.Name = "label11"
			Me.label11.Size = New System.Drawing.Size(50, 13)
			Me.label11.TabIndex = 20
			Me.label11.Text = "Phone 2:"
			' 
			' label12
			' 
			Me.label12.AutoSize = True
			Me.label12.Location = New System.Drawing.Point(24, 192)
			Me.label12.Name = "label12"
			Me.label12.Size = New System.Drawing.Size(38, 13)
			Me.label12.TabIndex = 21
			Me.label12.Text = "E-mail:"
			' 
			' detailsCommands
			' 
			Me.detailsCommands.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.showCommentsButton})
			Me.detailsCommands.Location = New System.Drawing.Point(0, 0)
			Me.detailsCommands.Name = "detailsCommands"
			Me.detailsCommands.Size = New System.Drawing.Size(445, 25)
			Me.detailsCommands.TabIndex = 23
			Me.detailsCommands.Text = "toolStrip1"
			' 
			' showCommentsButton
			' 
			Me.showCommentsButton.Image = (CType(resources.GetObject("showCommentsButton.Image"), System.Drawing.Image))
			Me.showCommentsButton.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.showCommentsButton.Name = "showCommentsButton"
			Me.showCommentsButton.Size = New System.Drawing.Size(77, 22)
			Me.showCommentsButton.Text = "Comments"
			'			Me.showCommentsButton.Click += New System.EventHandler(Me.OnShowComments);
			' 
			' label1
			' 
			Me.label1.AutoSize = True
			Me.label1.Location = New System.Drawing.Point(223, 33)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(61, 13)
			Me.label1.TabIndex = 27
			Me.label1.Text = "Last Name:"
			' 
			' lastNameTextBox
			' 
			Me.lastNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "LastName", True))
			Me.lastNameTextBox.Location = New System.Drawing.Point(290, 31)
			Me.lastNameTextBox.Name = "lastNameTextBox"
			Me.lastNameTextBox.Size = New System.Drawing.Size(149, 20)
			Me.lastNameTextBox.TabIndex = 26
			' 
			' label2
			' 
			Me.label2.AutoSize = True
			Me.label2.Location = New System.Drawing.Point(3, 33)
			Me.label2.Name = "label2"
			Me.label2.Size = New System.Drawing.Size(60, 13)
			Me.label2.TabIndex = 25
			Me.label2.Text = "First Name:"
			' 
			' firstNameTextBox
			' 
			Me.firstNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.customerBindingSource, "FirstName", True))
			Me.firstNameTextBox.Location = New System.Drawing.Point(69, 31)
			Me.firstNameTextBox.Name = "firstNameTextBox"
			Me.firstNameTextBox.Size = New System.Drawing.Size(153, 20)
			Me.firstNameTextBox.TabIndex = 24
			' 
			' CustomerDetailView
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0F, 13.0F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.AutoScroll = True
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.lastNameTextBox)
			Me.Controls.Add(Me.label2)
			Me.Controls.Add(Me.firstNameTextBox)
			Me.Controls.Add(Me.detailsCommands)
			Me.Controls.Add(Me.label12)
			Me.Controls.Add(Me.label11)
			Me.Controls.Add(Me.label10)
			Me.Controls.Add(Me.txtEmail)
			Me.Controls.Add(Me.txtPhone2)
			Me.Controls.Add(Me.txtPhone1)
			Me.Controls.Add(Me.label9)
			Me.Controls.Add(Me.txtZip)
			Me.Controls.Add(Me.label8)
			Me.Controls.Add(Me.label7)
			Me.Controls.Add(Me.txtState)
			Me.Controls.Add(Me.label6)
			Me.Controls.Add(Me.txtCity)
			Me.Controls.Add(Me.txtAddress2)
			Me.Controls.Add(Me.txtAddress1)
			Me.Controls.Add(Me.label5)
			Me.Controls.Add(Me.label4)
			Me.Name = "CustomerDetailView"
			Me.Size = New System.Drawing.Size(445, 223)
			CType(Me.customerBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
			Me.detailsCommands.ResumeLayout(False)
			Me.detailsCommands.PerformLayout()
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub

#End Region

		Private label4 As System.Windows.Forms.Label
		Private label5 As System.Windows.Forms.Label
		Private txtAddress1 As System.Windows.Forms.TextBox
		Private txtAddress2 As System.Windows.Forms.TextBox
		Private txtCity As System.Windows.Forms.TextBox
		Private label6 As System.Windows.Forms.Label
		Private txtState As System.Windows.Forms.TextBox
		Private label7 As System.Windows.Forms.Label
		Private label8 As System.Windows.Forms.Label
		Private txtZip As System.Windows.Forms.TextBox
		Private label9 As System.Windows.Forms.Label
		Private txtPhone1 As System.Windows.Forms.TextBox
		Private txtPhone2 As System.Windows.Forms.TextBox
		Private txtEmail As System.Windows.Forms.TextBox
		Private label10 As System.Windows.Forms.Label
		Private label11 As System.Windows.Forms.Label
		Private label12 As System.Windows.Forms.Label
		Private customerBindingSource As System.Windows.Forms.BindingSource
		Private detailsCommands As System.Windows.Forms.ToolStrip
		Private WithEvents showCommentsButton As System.Windows.Forms.ToolStripButton
		Private label1 As System.Windows.Forms.Label
		Private lastNameTextBox As System.Windows.Forms.TextBox
		Private label2 As System.Windows.Forms.Label
		Private firstNameTextBox As System.Windows.Forms.TextBox




	End Class
End Namespace
