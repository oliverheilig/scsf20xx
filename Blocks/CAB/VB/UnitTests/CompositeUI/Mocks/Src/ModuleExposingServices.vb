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

Namespace ModuleExposingServices
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

	<Service(AddOnDemand:=True)> _
	Public Class OnDemandService
		Public Shared ServiceCreated As Boolean = False

		Public Sub New()
			ServiceCreated = True
		End Sub
	End Class

	<Service(AddOnDemand:=True)> _
	Public Class OnDemandService2
		Public Shared ServiceCreated As Boolean = False

		Public Sub New()
			ServiceCreated = True
		End Sub
	End Class
End Namespace
