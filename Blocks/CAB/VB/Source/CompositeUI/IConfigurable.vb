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
Imports System.Collections.Specialized

''' <summary>
''' Defines a method implemented by classes that can receive configuration through
''' name/value pairs in the settings file.
''' </summary>
Public Interface IConfigurable
	''' <summary>
	''' Configures the component with the received settings.
	''' </summary>
	Sub Configure(ByVal settings As NameValueCollection)
End Interface
