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
Imports System.Security.Principal

Namespace Services
	''' <summary>
	''' Implementation of <see cref="IAuthenticationService"/> that 
	''' sets the current WindowsPrincipal as the principal policy.
	''' </summary>
	Public Class WindowsPrincipalAuthenticationService
		Implements IAuthenticationService

		''' <summary>
		''' Authenticates the user.
		''' </summary>
		Public Sub Authenticate() Implements IAuthenticationService.Authenticate
			' Set current principal.
			AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal)
		End Sub
	End Class
End Namespace