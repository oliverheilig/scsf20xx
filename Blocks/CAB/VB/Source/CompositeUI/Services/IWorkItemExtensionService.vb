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
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Services
	''' <summary>
	''' Service that manages the <see cref="IWorkItemExtension"/>s registration.
	''' </summary>
	Public Interface IWorkItemExtensionService
		''' <summary>
		''' Gets a dictionary of the registered <see cref="WorkItem"/> extensions, where
		''' the key is the <see cref="WorkItem"/> type, and the value is a list of the
		''' types of classes that offer extensions for that <see cref="WorkItem"/>.
		''' </summary>
		ReadOnly Property RegisteredExtensions() As ReadOnlyDictionary(Of Type, IList(Of Type))

		''' <summary>
		''' Gets a list of the registered root <see cref="WorkItem"/> extension class types.
		''' </summary>
		ReadOnly Property RegisteredRootExtensions() As IList(Of Type)

		''' <summary>
		''' Creates and initializes the extensions for the given <see cref="WorkItem"/>.
		''' </summary>
		''' <param name="workItem">The <see cref="WorkItem"/> to add the extensions to and initialize 
		''' the extensions for.</param>
		Sub InitializeExtensions(ByVal workItem As WorkItem)

		''' <summary>
		''' Registers a type as an extension to a specific <see cref="WorkItem"/> type.
		''' </summary>
		''' <param name="workItemType">The type of <see cref="WorkItem"/> to be extended.</param>
		''' <param name="extensionType">The type of the class to perform the extension.</param>
		Sub RegisterExtension(ByVal workItemType As Type, ByVal extensionType As Type)

		''' <summary>
		''' Registers a type as an extension to the root <see cref="WorkItem"/>.
		''' </summary>
		''' <param name="extensionType">The type of the class to perform the extension.</param>
		Sub RegisterRootExtension(ByVal extensionType As Type)
	End Interface
End Namespace