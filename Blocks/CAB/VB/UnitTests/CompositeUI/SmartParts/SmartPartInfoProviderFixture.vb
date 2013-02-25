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

Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace Tests.SmartParts
	<TestClass()> _
	Public Class SmartPartInfoProviderFixture
		Private Shared provider As SmartPartInfoProvider

		<TestInitialize()> _
		Public Sub SetUp()
			provider = New SmartPartInfoProvider()
		End Sub

		<TestMethod()> _
		Public Sub CanRegisterSPI()
			provider.Items.Add(New SmartPartInfo())
		End Sub

		<TestMethod()> _
		Public Sub CanRetrieveSpecificSPI()
			Dim spi As MockSPI = New MockSPI()
			provider.Items.Add(New SmartPartInfo())
			provider.Items.Add(spi)

			Dim result As MockSPI = CType(provider.GetSmartPartInfo(GetType(MockSPI)), MockSPI)

			Assert.AreSame(spi, result)
		End Sub

		Private Class MockSPI : Implements ISmartPartInfo
#Region "ISmartPartInfo Members"

			Public Property Description() As String Implements ISmartPartInfo.Description
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
				Set(ByVal value As String)
					Throw New Exception("The method or operation is not implemented.")
				End Set
			End Property

			Public Property Title() As String Implements ISmartPartInfo.Title
				Get
					Throw New Exception("The method or operation is not implemented.")
				End Get
				Set(ByVal value As String)
					Throw New Exception("The method or operation is not implemented.")
				End Set
			End Property

#End Region
		End Class
	End Class
End Namespace
