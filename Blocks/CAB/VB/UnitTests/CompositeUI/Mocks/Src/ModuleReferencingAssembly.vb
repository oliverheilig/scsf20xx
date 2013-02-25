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
Imports System.Text
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.Tests.TestAssembly

<Assembly: ModuleAttribute("TestModule")> 

Namespace ModuleReferencingAssembly
	Public Class TestModule : Inherits ModuleInit

		Public Overrides Sub AddServices()

		End Sub

		Public Overrides Sub Load()
			Dim instance As TestClass = New TestClass()
		End Sub
	End Class
End Namespace
