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
Imports System.Collections.Generic
Imports Microsoft.Practices.CompositeUI.Services


<TestClass()> _
Public Class ModuleDependencySolverTestFixture
	Private Shared solver As ModuleDependencySolver

	<TestInitialize()> _
	Public Sub Setup()
		solver = New ModuleDependencySolver()
	End Sub

	<TestMethod()> _
	Public Sub ModuleDependencySolverIsAvailable()
		Assert.IsNotNull(solver)
	End Sub

	<TestMethod()> _
	Public Sub CanAddModuleName()
		solver.AddModule("ModuleA")
		Assert.AreEqual(1, solver.ModuleCount)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentNullException))> _
	Public Sub CannotAddNullModuleName()
		solver.AddModule(Nothing)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub CannotAddEmptyModuleName()
		solver.AddModule(String.Empty)
	End Sub

	<TestMethod(), ExpectedException(GetType(ArgumentException))> _
	Public Sub CannotAddDependencyWithoutAddingModule()
		solver.AddDependency("ModuleA", "ModuleB")
	End Sub

	<TestMethod()> _
	Public Sub CanAddModuleDepedency()
		solver.AddModule("ModuleA")
		solver.AddModule("ModuleB")
		solver.AddDependency("ModuleB", "ModuleA")
		Assert.AreEqual(2, solver.ModuleCount)
	End Sub

	<TestMethod()> _
	Public Sub CanSolveAcyclicDependencies()
		solver.AddModule("ModuleA")
		solver.AddModule("ModuleB")
		solver.AddDependency("ModuleB", "ModuleA")
		Dim result As String() = solver.Solve()
		Assert.AreEqual(2, result.Length)
		Assert.AreEqual("ModuleA", result(0))
		Assert.AreEqual("ModuleB", result(1))
	End Sub

	<TestMethod(), ExpectedException(GetType(CyclicDependencyFoundException))> _
	Public Sub FailsWithSimpleCycle()
		solver.AddModule("ModuleB")
		solver.AddDependency("ModuleB", "ModuleB")
		Dim result As String() = solver.Solve()
	End Sub

	<TestMethod()> _
	Public Sub CanSolveForest()
		solver.AddModule("ModuleA")
		solver.AddModule("ModuleB")
		solver.AddModule("ModuleC")
		solver.AddModule("ModuleD")
		solver.AddModule("ModuleE")
		solver.AddModule("ModuleF")
		solver.AddDependency("ModuleC", "ModuleB")
		solver.AddDependency("ModuleB", "ModuleA")
		solver.AddDependency("ModuleE", "ModuleD")
		Dim result As String() = solver.Solve()
		Assert.AreEqual(6, result.Length)
		Dim test As List(Of String) = New List(Of String)(result)
		Assert.IsTrue(test.IndexOf("ModuleA") < test.IndexOf("ModuleB"))
		Assert.IsTrue(test.IndexOf("ModuleB") < test.IndexOf("ModuleC"))
		Assert.IsTrue(test.IndexOf("ModuleD") < test.IndexOf("ModuleE"))
	End Sub

	<TestMethod(), ExpectedException(GetType(CyclicDependencyFoundException))> _
	Public Sub FailsWithComplexCycle()
		solver.AddModule("ModuleA")
		solver.AddModule("ModuleB")
		solver.AddModule("ModuleC")
		solver.AddModule("ModuleD")
		solver.AddModule("ModuleE")
		solver.AddModule("ModuleF")
		solver.AddDependency("ModuleC", "ModuleB")
		solver.AddDependency("ModuleB", "ModuleA")
		solver.AddDependency("ModuleE", "ModuleD")
		solver.AddDependency("ModuleE", "ModuleC")
		solver.AddDependency("ModuleF", "ModuleE")
		solver.AddDependency("ModuleD", "ModuleF")
		solver.AddDependency("ModuleB", "ModuleD")
		solver.Solve()
	End Sub

	<TestMethod(), ExpectedException(GetType(ModuleLoadException))> _
	Public Sub FailsWithMissingModule()
		solver.AddModule("ModuleA")
		solver.AddDependency("ModuleA", "ModuleB")
		solver.Solve()
	End Sub
End Class

