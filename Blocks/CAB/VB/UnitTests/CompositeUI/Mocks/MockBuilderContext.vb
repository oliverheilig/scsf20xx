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
Imports Microsoft.Practices.CompositeUI.Utility
Imports Microsoft.Practices.ObjectBuilder

Public Class MockBuilderContext
	Implements IBuilderContext

	Private innerLocator As IReadWriteLocator = New Locator()
	Private chain As BuilderStrategyChain = New BuilderStrategyChain()
	Private innerPolicies As PolicyList = New PolicyList()

	Public Sub New(ByVal ParamArray strategies As IBuilderStrategy())
		Guard.ArgumentNotNull(strategies, "strategies")

		For Each strategy As IBuilderStrategy In strategies
			chain.Add(strategy)
		Next strategy
	End Sub

	Public ReadOnly Property HeadOfChain() As IBuilderStrategy Implements IBuilderContext.HeadOfChain
		Get
			Return chain.Head
		End Get
	End Property

	Public Function GetNextInChain(ByVal currentStrategy As IBuilderStrategy) As IBuilderStrategy Implements IBuilderContext.GetNextInChain
		Return chain.GetNext(currentStrategy)
	End Function

	Public ReadOnly Property Locator() As IReadWriteLocator Implements IBuilderContext.Locator
		Get
			Return innerLocator
		End Get
	End Property

	Public ReadOnly Property Policies() As PolicyList Implements IBuilderContext.Policies
		Get
			Return innerPolicies
		End Get
	End Property
End Class
