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
Imports System.Collections
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Globalization

''' <summary>
''' Represents a named collection of sites for UI elements.
''' </summary>
Public Class UIExtensionSiteCollection
	Implements ICollection, IEnumerable(Of String), IDisposable

	Private parent As UIExtensionSiteCollection
	Private rootWorkItem As WorkItem
	Private sites As Dictionary(Of String, UIExtensionSite) = New Dictionary(Of String, UIExtensionSite)()
	Dim createdAdapters As List(Of IUIElementAdapter) = New List(Of IUIElementAdapter)()

	''' <summary>
	''' Initializes a new instance of the <see cref="UIExtensionSiteCollection"/> class.
	''' </summary>
	Public Sub New()
		parent = Nothing
		rootWorkItem = Nothing
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="UIExtensionSiteCollection"/> class using the provided
	''' root <see cref="WorkItem"/>.
	''' </summary>
	Public Sub New(ByVal rootWorkItem As WorkItem)
		Me.parent = Nothing
		Me.rootWorkItem = rootWorkItem
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="UIExtensionSiteCollection"/> class using the provided
	''' parent <see cref="UIExtensionSiteCollection"/>.
	''' </summary>
	Public Sub New(ByVal parent As UIExtensionSiteCollection)
		Me.parent = parent
		Me.rootWorkItem = parent.rootWorkItem
	End Sub

	Public Sub New(ByVal rootWorkItem As WorkItem, ByVal parent As UIExtensionSiteCollection)
		Me.parent = parent
		Me.rootWorkItem = rootWorkItem
	End Sub

	''' <summary>
	''' Retrieves an extension site by name.
	''' </summary>
	''' <param name="siteName">The name of the extension site to get.</param>
	''' <returns>The extension site.</returns>
	Default Public ReadOnly Property Item(ByVal siteName As String) As UIExtensionSite
		Get
			If sites.ContainsKey(siteName) Then
				Return sites(siteName)
			End If

			If Not parent Is Nothing Then
				Dim childSite As UIExtensionSite = parent(siteName).Duplicate()
				sites.Add(siteName, childSite)
				Return childSite
			End If

			Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.ExtensionSiteDoesNotExist, siteName), "siteName")
		End Get
	End Property

	''' <summary>
	''' Returns a count of the available extension sites.
	''' </summary>
	Public ReadOnly Property Count() As Integer
		Get
			Dim result As Integer = 0

			For Each siteName As String In Me
				result += 1
			Next siteName

			Return result
		End Get
	End Property

	''' <summary>
	''' Determines whether an extension site is available.
	''' </summary>
	''' <param name="siteName">The site name to find.</param>
	''' <returns>true if the site is available; false otherwise.</returns>
	Public Function Contains(ByVal siteName As String) As Boolean
		If sites.ContainsKey(siteName) Then
			Return True
		End If

		If Not parent Is Nothing Then
			Return parent.Contains(siteName)
		End If

		Return False
	End Function

	''' <summary>
	''' Registers a site for the given UI element by asking the adapter factory to
	''' automatically allocate an adapter based on the element type.
	''' </summary>
	''' <param name="siteName">The site name to register.</param>
	''' <param name="uiElement">The UI element.</param>
	Public Sub RegisterSite(ByVal siteName As String, ByVal uiElement As Object)
		Guard.ArgumentNotNullOrEmptyString(siteName, "siteName")
		Guard.ArgumentNotNull(uiElement, "uiElement")

		Dim factory As IUIElementAdapterFactory = FactoryCatalog.GetFactory(uiElement)
		Dim adapter As IUIElementAdapter = factory.GetAdapter(uiElement)
		createdAdapters.Add(adapter)
		RegisterSite(siteName, adapter)
	End Sub

	''' <summary>
	''' Registers a site using the given adapter.
	''' </summary>
	''' <param name="siteName">The site name to register.</param>
	''' <param name="adapter">The UI element adapter for the site.</param>
	Public Sub RegisterSite(ByVal siteName As String, ByVal adapter As IUIElementAdapter)
		Guard.ArgumentNotNullOrEmptyString(siteName, "siteName")
		Guard.ArgumentNotNull(adapter, "adapter")

		If Contains(siteName) Then
			Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.DuplicateUIExtensionSite, siteName), "siteName")
		End If
		sites.Add(siteName, New UIExtensionSite(adapter))
	End Sub

	''' <summary>
	''' Unregisters a site.
	''' </summary>
	''' <param name="siteName">The site name to unregister.</param>
	Public Sub UnregisterSite(ByVal siteName As String)
		Guard.ArgumentNotNullOrEmptyString(siteName, "siteName")

		If Not parent Is Nothing AndAlso parent.Contains(siteName) Then
			Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.CannotUnregisterSiteRegisteredWithParent, siteName), "siteName")
		End If

		sites.Remove(siteName)
	End Sub

	Private ReadOnly Property FactoryCatalog() As IUIElementAdapterFactoryCatalog
		Get
			Return rootWorkItem.Services.Get(Of IUIElementAdapterFactoryCatalog)()
		End Get
	End Property

	Private Function GetEnumerator() As IEnumerator(Of String) Implements IEnumerable(Of String).GetEnumerator
		Dim seenNames As [Set](Of String) = New [Set](Of String)()
		Dim baseList As List(Of String) = New List(Of String)

		Dim current As UIExtensionSiteCollection = Me
		Do While Not current Is Nothing
			For Each siteName As String In current.sites.Keys
				If (Not seenNames.Contains(siteName)) Then
					seenNames.Add(siteName)
					baseList.Add(siteName)
				End If
			Next siteName
			current = current.parent
		Loop
		Return baseList.GetEnumerator()
	End Function

	Private Function GetEnumeratorBase() As IEnumerator Implements IEnumerable.GetEnumerator
		Return CType(Me, IEnumerable(Of String)).GetEnumerator()
	End Function

	Private Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
		Throw New NotImplementedException()
	End Sub

	Private ReadOnly Property CountBase() As Integer Implements ICollection.Count
		Get
			Return Count
		End Get
	End Property

	Private ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
		Get
			Return False
		End Get
	End Property

	Private ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
		Get
			Throw New NotImplementedException()
		End Get
	End Property

	''' <summary>
	''' See <see cref="IDisposable.Dispose"/> for more information.
	''' </summary>
	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	''' <summary>
	''' See <see cref="IDisposable.Dispose"/> for more information.
	''' </summary>
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		For Each site As UIExtensionSite In sites.Values
			site.Clear()
		Next

		For Each adapter As IUIElementAdapter In createdAdapters

			Dim disp As IDisposable = TryCast(adapter, IDisposable)

			If Not disp Is Nothing Then
				disp.Dispose()
			End If
		Next

		sites.Clear()
		createdAdapters.Clear()
	End Sub

End Class