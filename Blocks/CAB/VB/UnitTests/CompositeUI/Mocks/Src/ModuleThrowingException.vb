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

Namespace ModuleThrowingException
	Public Class TestModule : Inherits ModuleInit
		Public Overrides Sub AddServices()
			Throw New Exception("The method or operation is not implemented.")
		End Sub

		Public Overrides Sub Load()
			Throw New Exception("The method or operation is not implemented.")
		End Sub
	End Class
End Namespace
