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
Imports System.Globalization
Imports Microsoft.Practices.CompositeUI.Utility


Namespace Services

	''' <summary>
	''' Used by the <see cref="ModuleLoaderService"/> to get the load sequence
	''' for the modules to load according to their dependencies.
	''' </summary>
	Public Class ModuleDependencySolver
		Private dependencyMatrix As ListDictionary(Of String, String) = New ListDictionary(Of String, String)()
		Private knownModules As List(Of String) = New List(Of String)()

		''' <summary>
		''' Adds a module to the solver.
		''' </summary>
		''' <param name="name">The name that uniquely identifies the module.</param>
		Public Sub AddModule(ByVal name As String)
			Guard.ArgumentNotNullOrEmptyString(name, "name")

			AddToDependencyMatrix(name)
			AddToKnownModules(name)
		End Sub

		''' <summary>
		''' Adds a module dependency between the modules specified by dependingModule and
		''' dependentModule.
		''' </summary>
		''' <param name="dependingModule">The name of the module with the dependency.</param>
		''' <param name="dependentModule">The name of the module dependingModule
		''' depends on.</param>
		Public Sub AddDependency(ByVal dependingModule As String, ByVal dependentModule As String)
			Guard.ArgumentNotNullOrEmptyString(dependingModule, "dependingModule")
			Guard.ArgumentNotNullOrEmptyString(dependentModule, "dependentModule")

			If (Not knownModules.Contains(dependingModule)) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.DependencyForUnknownModule, dependingModule))
			End If

			AddToDependencyMatrix(dependentModule)
			dependencyMatrix.Add(dependentModule, dependingModule)
		End Sub

		Private Sub AddToDependencyMatrix(ByVal aModule As String)
			If (Not dependencyMatrix.ContainsKey(aModule)) Then
				dependencyMatrix.Add(aModule)
			End If
		End Sub

		Private Sub AddToKnownModules(ByVal aModule As String)
			If (Not knownModules.Contains(aModule)) Then
				knownModules.Add(aModule)
			End If
		End Sub

		''' <summary>
		''' Calculates an ordered vector according to the defined dependencies.
		''' Non-dependant modules appears at the beginning of the resulting array.
		''' </summary>
		''' <returns>The resulting ordered list of modules.</returns>
		''' <exception cref="CyclicDependencyFoundException">This exception is thrown
		''' when a cycle is found in the defined depedency graph.</exception>
		Public Function Solve() As String()
			Dim skip As List(Of String) = New List(Of String)()
			Do While skip.Count < dependencyMatrix.Count
				Dim leaves As List(Of String) = Me.FindLeaves(skip)
				If leaves.Count = 0 AndAlso skip.Count < dependencyMatrix.Count Then
					Throw New CyclicDependencyFoundException()
				End If
				skip.AddRange(leaves)
			Loop
			skip.Reverse()

			If skip.Count > knownModules.Count Then
				Throw New ModuleLoadException(String.Format(CultureInfo.CurrentCulture, My.Resources.DependencyOnMissingModule, FindMissingModules(skip)))
			End If

			Return skip.ToArray()
		End Function

		Private Function FindMissingModules(ByVal skip As List(Of String)) As String
			Dim missingModules As String = ""

			For Each moduleName As String In skip
				If (Not knownModules.Contains(moduleName)) Then
					missingModules &= ", "
					missingModules &= moduleName
				End If
			Next moduleName

			Return missingModules.Substring(2)
		End Function

		''' <summary>
		''' Gets the number of modules added to the solver.
		''' </summary>
		Public ReadOnly Property ModuleCount() As Integer
			Get
				Return dependencyMatrix.Count
			End Get
		End Property

		Private Function FindLeaves(ByVal skip As List(Of String)) As List(Of String)
			Dim result As List(Of String) = New List(Of String)()

			For Each precedent As String In dependencyMatrix.Keys
				If skip.Contains(precedent) Then
					Continue For
				End If

				Dim count As Integer = 0
				For Each dependent As String In dependencyMatrix(precedent)
					If skip.Contains(dependent) Then
						Continue For
					End If
					count += 1
				Next dependent
				If count = 0 Then
					result.Add(precedent)
				End If
			Next precedent
			Return result
		End Function
	End Class

End Namespace