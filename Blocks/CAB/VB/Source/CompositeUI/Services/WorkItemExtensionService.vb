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
Imports System.Globalization
Imports Microsoft.Practices.CompositeUI.Utility
Imports System.Collections.Generic
Imports System.Diagnostics
Imports Microsoft.Practices.ObjectBuilder

Namespace Services
	''' <summary>
	''' Default implementation of the <see cref="IWorkItemExtensionService"/>.
	''' </summary>
	Public Class WorkItemExtensionService
		Implements IWorkItemExtensionService

		Private extensions As ListDictionary(Of Type, Type) = New ListDictionary(Of Type, Type)()
		Private rootExtensions As List(Of Type) = New List(Of Type)()
		Private traceSource As TraceSource

		''' <summary>
		''' Initializes a new instance of <see cref="WorkItemExtensionService"/>.
		''' </summary>
		Public Sub New()
			Me.New(Nothing)
		End Sub

		''' <summary>
		''' Initializes a new instance of <see cref="WorkItemExtensionService"/> with the given <see cref="TraceSource"/>.
		''' </summary>
		<InjectionConstructor()> _
		Public Sub New(<ClassNameTraceSource()> ByVal traceSource As TraceSource)
			Me.traceSource = traceSource
		End Sub

		''' <summary>
		''' See <see cref="IWorkItemExtensionService.RegisteredExtensions"/> for more information.
		''' </summary>
		Public ReadOnly Property RegisteredExtensions() As ReadOnlyDictionary(Of Type, IList(Of Type)) Implements IWorkItemExtensionService.RegisteredExtensions
			Get
				Dim results As Dictionary(Of Type, IList(Of Type)) = New Dictionary(Of Type, IList(Of Type))()

				For Each pair As KeyValuePair(Of Type, List(Of Type)) In extensions
					results(pair.Key) = pair.Value.AsReadOnly()
				Next pair

				Return New ReadOnlyDictionary(Of Type, IList(Of Type))(results)
			End Get
		End Property

		''' <summary>
		''' See <see cref="IWorkItemExtensionService.RegisteredRootExtensions"/> for more information.
		''' </summary>
		Public ReadOnly Property RegisteredRootExtensions() As IList(Of Type) Implements IWorkItemExtensionService.RegisteredRootExtensions
			Get
				Return rootExtensions.AsReadOnly()
			End Get
		End Property

		''' <summary>
		''' See <see cref="IWorkItemExtensionService.RegisterExtension"/> for more information.
		''' </summary>
		Public Sub RegisterExtension(ByVal workItemType As Type, ByVal extensionType As Type) Implements IWorkItemExtensionService.RegisterExtension
			Guard.ArgumentNotNull(workItemType, "workItemType")
			Guard.ArgumentNotNull(extensionType, "extensionType")
			Guard.TypeIsAssignableFromType(workItemType, GetType(WorkItem), "workItemType")
			Guard.TypeIsAssignableFromType(extensionType, GetType(IWorkItemExtension), "extensionType")
			ThrowIfAlreadyAdded(workItemType, extensionType)

			extensions.Add(workItemType, extensionType)

			If Not traceSource Is Nothing Then
				traceSource.TraceInformation(String.Format( _
					CultureInfo.CurrentCulture, _
					My.Resources.WorkItemExtensionRegistered, _
					extensionType, workItemType))
			End If
		End Sub

		Private Sub ThrowIfAlreadyAdded(ByVal workItemType As Type, ByVal extensionType As Type)
			If extensions.ContainsValue(extensionType) AndAlso extensions(workItemType).Contains(extensionType) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, _
				   My.Resources.WorkItemExtensionTypeAlreadyRegistered, extensionType.ToString(), workItemType.ToString()))
			End If
		End Sub

		''' <summary>
		''' See <see cref="IWorkItemExtensionService.RegisterRootExtension"/> for more information.
		''' </summary>
		Public Sub RegisterRootExtension(ByVal extensionType As Type) Implements IWorkItemExtensionService.RegisterRootExtension
			Guard.ArgumentNotNull(extensionType, "extensionType")
			Guard.TypeIsAssignableFromType(extensionType, GetType(IWorkItemExtension), "extensionType")

			rootExtensions.Add(extensionType)

			If Not traceSource Is Nothing Then
				traceSource.TraceInformation(String.Format( _
					CultureInfo.CurrentCulture, _
					My.Resources.WorkItemExtensionRootRegistered, _
					extensionType))
			End If
		End Sub

		''' <summary>
		''' Creates and initializes the extensions for the given workItem.
		''' </summary>
		''' <param name="workItem">The <see cref="WorkItem"/> to add the extensions to and initialize 
		''' the extensions for.</param>
		Public Sub InitializeExtensions(ByVal workItem As WorkItem) Implements IWorkItemExtensionService.InitializeExtensions
			Guard.ArgumentNotNull(workItem, "workItem")

			InnerInitialize(workItem, GetRegisteredExtensions(workItem.GetType()))

			If workItem.Parent Is Nothing Then
				InnerInitialize(workItem, rootExtensions)
			End If
		End Sub

		Private Sub InnerInitialize(ByVal workItem As WorkItem, ByVal extensions As IEnumerable(Of Type))
			For Each extensionType As Type In extensions
				Dim extension As IWorkItemExtension = CType(workItem.Items.AddNew(extensionType), IWorkItemExtension)
				extension.Initialize(workItem)

				If Not traceSource Is Nothing Then
					traceSource.TraceInformation(String.Format(CultureInfo.CurrentCulture, My.Resources.WorkItemExtensionInitialized, extension, workItem))
				End If
			Next extensionType
		End Sub

#Region "Utility code for the function GetRegisteredExtensions"

		Private Class GetRegisteredExtensionsUtilityProvider
			Private workItemTypeField As Type

			Public WriteOnly Property WorkItemType() As Type
				Set(ByVal value As Type)
					workItemTypeField = value
				End Set
			End Property

			Public Function IsAssignableFrom(ByVal key As Type) As Boolean
				Return key.IsAssignableFrom(workItemTypeField)
			End Function
		End Class

#End Region

		Private Function GetRegisteredExtensions(ByVal workItemType As Type) As IEnumerable(Of Type)
			Dim utilityProvider As GetRegisteredExtensionsUtilityProvider = New GetRegisteredExtensionsUtilityProvider()
			utilityProvider.WorkItemType = workItemType
			Return extensions.FindAllValuesByKey(AddressOf utilityProvider.IsAssignableFrom)
		End Function

		Private Class ClassNameTraceSourceAttribute
			Inherits TraceSourceAttribute

			Public Sub New()
				MyBase.New(GetType(WorkItemExtensionService).FullName)
			End Sub
		End Class
	End Class
End Namespace
