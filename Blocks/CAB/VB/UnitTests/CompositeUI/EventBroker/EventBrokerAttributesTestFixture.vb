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
Imports System.Reflection
Imports Microsoft.Practices.CompositeUI.EventBroker

Namespace Tests.EventBroker
	<TestClass()> _
	Public Class EventBrokerAttributesTestFixture
		<TestMethod()> _
		Public Sub EventSourceAttributeIsAvailable()
			Dim attr As EventPublicationAttribute = New EventPublicationAttribute("MyEvent")
			Assert.IsNotNull(attr)
			Assert.AreEqual(PublicationScope.Global, attr.Scope)
		End Sub

		<TestMethod()> _
		Public Sub EventNameIsStored()
			Dim attr As EventPublicationAttribute = New EventPublicationAttribute("MyEvent")
			Assert.AreEqual("MyEvent", attr.Topic)
		End Sub

		<TestMethod()> _
		Public Sub EventNameAndWorkItemAreStored()
			Dim attr As EventPublicationAttribute = New EventPublicationAttribute("MyEvent", PublicationScope.WorkItem)
			Assert.AreEqual("MyEvent", attr.Topic)
			Assert.AreEqual(PublicationScope.WorkItem, attr.Scope)
		End Sub

		<TestMethod()> _
		Public Sub AttributesDiscover()
			Dim attributeCount As Integer = 0
			Dim type As Type = GetType(ClassUsingAttributes)

			For Each info As EventInfo In type.GetEvents()
				Dim attrs As EventPublicationAttribute() = CType(info.GetCustomAttributes(GetType(EventPublicationAttribute), True), EventPublicationAttribute())
				For Each attr As EventPublicationAttribute In attrs
					Select Case attr.Topic
						Case "MyGlobalEvent"
							Assert.AreEqual(PublicationScope.Global, attr.Scope)
							Assert.AreEqual("GlobalEvent", info.Name)
						Case "MyLocalEvent"
							Assert.AreEqual(PublicationScope.WorkItem, attr.Scope)
							Assert.AreEqual("LocalEvent", info.Name)
						Case "MyOneEvent", "MyTwoEvent"
							Assert.AreEqual(PublicationScope.Global, attr.Scope)
							Assert.AreEqual("SeveralEvents", info.Name)
						Case Else
							Assert.Fail("Invalid event attribute encountered")
					End Select
					attributeCount += 1
				Next attr
			Next info
			Assert.AreEqual(4, attributeCount)
		End Sub
	End Class

	<TestClass()> _
	Public Class EventHandleAttributeTestFixture
		<TestMethod()> _
		Public Sub EventhandlerAttributeIsAvailable()
			Dim attr As EventSubscriptionAttribute = New EventSubscriptionAttribute("MyEvent")
			Assert.IsNotNull(attr)
		End Sub

		<TestMethod()> _
		Public Sub EventNameIsStored()
			Dim attr As EventSubscriptionAttribute = New EventSubscriptionAttribute("MyEvent")
			Assert.AreEqual("MyEvent", attr.Topic)
		End Sub

	End Class

#Region "Test support classes"

	Public Class ClassUsingAttributes
		<EventPublication("MyGlobalEvent")> _
		Public Event GlobalEvent As EventHandler

		<EventPublication("MyLocalEvent", PublicationScope.WorkItem)> _
		Public Event LocalEvent As EventHandler

		<EventPublication("MyOneEvent"), EventPublication("MyTwoEvent")> _
		Public Event SeveralEvents As EventHandler

		<EventSubscriptionAttribute("MyGlobalEvent")> _
		Public Sub OnGlobalEvent(ByVal sender As Object, ByVal args As EventArgs)
		End Sub

		<EventSubscriptionAttribute("MyLocalEvent")> _
		Public Sub OnLocalEvent(ByVal sender As Object, ByVal args As EventArgs)
		End Sub

		<EventSubscriptionAttribute("MyOneEvent"), EventSubscriptionAttribute("MyTwoEvent")> _
		Public Sub OnSeveralEvents(ByVal sender As Object, ByVal args As EventArgs)
		End Sub

		Private Sub ResolveCompilerWarnings()
			RaiseEvent GlobalEvent(Me, EventArgs.Empty)
			RaiseEvent LocalEvent(Me, EventArgs.Empty)
			RaiseEvent SeveralEvents(Me, EventArgs.Empty)
		End Sub
	End Class

#End Region
End Namespace
