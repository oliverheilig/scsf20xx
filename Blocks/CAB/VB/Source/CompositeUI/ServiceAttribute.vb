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


''' <summary>
''' Indicates that a class should be automatically registered as a service into the
''' application's root <see cref="WorkItem"/>.
''' </summary>
<AttributeUsage(AttributeTargets.Class, AllowMultiple:=False)> _
Public NotInheritable Class ServiceAttribute : Inherits Attribute
	Private innerRegisterAs As Type
	Private innerAddOnDemand As Boolean

	''' <summary>
	''' Initializes a new instance of the <see cref="ServiceAttribute"/> class. The
	''' registered type will the concrete type of the service.
	''' </summary>
	Public Sub New()
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="ServiceAttribute"/> class using
	''' the provided type as the registration type for the service.
	''' </summary>
	''' <param name="registerAs">The registration type for the service.</param>
	Public Sub New(ByVal registerAsValue As Type)
		Me.innerregisterAs = registerAsValue
	End Sub

	''' <summary>
	''' The service type implemented by the attributed class. This is the type 
	''' that will be used to register the service with the <see cref="WorkItem"/>.
	''' </summary>
	Public ReadOnly Property RegisterAs() As Type
		Get
			Return innerRegisterAs
		End Get
	End Property

	''' <summary>
	''' Specifies that the service instance will be created and added to the container
	''' upon request of that service (delayed creation).
	''' </summary>
	Public Property AddOnDemand() As Boolean
		Get
			Return Me.innerAddOnDemand
		End Get
		Set(ByVal value As Boolean)
			Me.innerAddOnDemand = value
		End Set
	End Property
End Class
