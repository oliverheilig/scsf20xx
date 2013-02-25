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
Imports Microsoft.Practices.CompositeUI.Services

Namespace ModuleExposingSameServices
	Public Class TestModule : Inherits ModuleInit
	End Class

	<Service()> _
	Public Class SimpleService
	End Class

	Public Interface ITestService
	End Interface

	<Service(GetType(ITestService))> _
	Public Class TestService : Implements ITestService
	End Class

	<Service()> _
	Public Class NonDefaultCtorService
		Public Sub New(ByVal someArg As String)

		End Sub
	End Class

	<Service()> _
	Public MustInherit Class AbstractService
	End Class
End Namespace
