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
Imports System.Reflection
Imports Microsoft.Practices.CompositeUI.Configuration
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Services
	''' <summary>
	''' Service that performs module loading.
	''' </summary>
	Public Interface IModuleLoaderService
		''' <summary>
		''' Returns a list of the loaded modules.
		''' </summary>
		ReadOnly Property LoadedModules() As IList(Of LoadedModuleInfo)

		''' <summary>
		''' Loads the specified list of modules.
		''' </summary>
		''' <param name="workItem">The <see cref="WorkItem"/> that will host the modules.</param>
		''' <param name="modules">The list of modules to load.</param>
		Sub Load(ByVal workItem As WorkItem, ByVal ParamArray modules As IModuleInfo())

		''' <summary>
		''' Loads assemblies as modules.
		''' </summary>
		''' <param name="workItem">The <see cref="WorkItem"/> that will host the modules.</param>
		''' <param name="assemblies">The list of assemblies to load as modules.</param>
		Sub Load(ByVal workItem As WorkItem, ByVal ParamArray assemblies As System.Reflection.Assembly())

		''' <summary>
		''' The event that is fired when a module has been loaded by the service.
		''' </summary>
		Event ModuleLoaded As EventHandler(Of DataEventArgs(Of LoadedModuleInfo))
	End Interface
End Namespace