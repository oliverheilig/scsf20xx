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
Imports System.IO
Imports Microsoft.Practices.CompositeUI.Configuration
Imports Microsoft.Practices.CompositeUI.Services

Namespace Services
	<TestClass()> _
	Public Class ReflectionModuleEnumeratorFixture
		Private Shared enumerator As ReflectionModuleEnumerator
		Private Shared location As String

		Shared Sub New()
            location = AppDomain.CurrentDomain.BaseDirectory

			ModuleLoaderServiceFixture.CompileFile("Microsoft.Practices.CompositeUI.Tests.ReflectionModule1.vb", ".\Reflection1\Module1.dll")

			ModuleLoaderServiceFixture.CompileFile("Microsoft.Practices.CompositeUI.Tests.ReflectionModule2.vb", ".\Reflection2\Module2.dll")

			ModuleLoaderServiceFixture.CompileFile("Microsoft.Practices.CompositeUI.Tests.ReflectionModule3.vb", ".\Reflection2\Recurse\Module3.dll")
		End Sub

        <TestMethod()> _
        Public Sub BasePathDefaultIsApplicationPath()
            enumerator = New ReflectionModuleEnumerator()
            Assert.AreEqual(location, enumerator.BasePath)
        End Sub

        <TestMethod()> _
        Public Sub EnumeratorFindsModules()
            enumerator = New ReflectionModuleEnumerator()
            enumerator.BasePath = Path.Combine(location, "Reflection1")

            Dim modules As IModuleInfo() = enumerator.EnumerateModules()

            Assert.AreEqual(1, modules.Length)
            Assert.AreEqual(Path.Combine(enumerator.BasePath, "Module1.dll"), modules(0).AssemblyFile)
        End Sub

        <TestMethod()> _
        Public Sub EnumeratorSearchDirectoryRecursively()
            enumerator = New ReflectionModuleEnumerator()
            enumerator.BasePath = Path.Combine(location, "Reflection2")

            Dim modules As IModuleInfo() = enumerator.EnumerateModules()

            Assert.AreEqual(2, modules.Length)
        End Sub


	End Class
End Namespace
