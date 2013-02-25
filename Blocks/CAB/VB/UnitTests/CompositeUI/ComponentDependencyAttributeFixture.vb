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
Imports Microsoft.Practices.ObjectBuilder


<TestClass()> _
Public Class ComponentDependencyAttributeFixture
	Private Shared locator As Locator
	Private Shared builder As Builder
	Private Shared container As LifetimeContainer

	<TestInitialize()> _
	Public Sub SetUp()
		locator = New Locator()
		container = New LifetimeContainer()
		locator.Add(GetType(ILifetimeContainer), container)
		builder = New Builder()
		builder.Policies.SetDefault(Of ISingletonPolicy)(New SingletonPolicy(True))
	End Sub

	<TestMethod(), ExpectedException(GetType(DependencyMissingException))> _
	Public Sub AttributeDefaultsToNotCreatingAndRequired()
		Dim obj As MockDefaultDependency = builder.BuildUp(Of MockDefaultDependency)(locator, "foo", Nothing)
	End Sub

	<TestMethod()> _
	Public Sub AttributeDefaultsToNotCreatingWithOptional()
		Dim obj As MockOptionalDependencyDefaults = builder.BuildUp(Of MockOptionalDependencyDefaults)(locator, "foo", Nothing)

		Assert.IsNull(obj.Dependency)
	End Sub

	<TestMethod()> _
	Public Sub DependencyGetsCreatedIfSpecifiedOnAttribute()
		Dim obj As MockRequiredDependencyCreate = builder.BuildUp(Of MockRequiredDependencyCreate)(locator, "foo", Nothing)

		Assert.IsNotNull(obj.Dependency, "Dependency not injected")
	End Sub

	<TestMethod()> _
	Public Sub DependencyGetsCreatedAndReusedIfSpecifiedOnAttribute()
		Dim obj1 As MockRequiredDependencyCreate = builder.BuildUp(Of MockRequiredDependencyCreate)(locator, "foo", Nothing)
		Dim obj2 As MockRequiredDependencyCreate = builder.BuildUp(Of MockRequiredDependencyCreate)(locator, "bar", Nothing)

		' 2 objects + 1 dependency
		Assert.AreEqual(3, container.Count)

		Assert.IsNotNull(obj1.Dependency, "Dependency not injected")
		Assert.IsNotNull(obj2.Dependency, "Dependency not injected")
		Assert.AreSame(obj1.Dependency, obj2.Dependency, "Instance not reused")
	End Sub

	<TestMethod()> _
	Public Sub DependencyOfExplicitTypeIsCreatedIfSpecified()
		Dim obj As MockRequiredDependencyCreateWithType = builder.BuildUp(Of MockRequiredDependencyCreateWithType)(locator, "foo", Nothing)

		Assert.IsNotNull(obj.Dependency, "Dependency not injected")
		Assert.IsTrue(TypeOf obj.Dependency Is Dependency, "Dependency is not of the specified type")
	End Sub

	<TestMethod()> _
	Public Sub NullDependencyDoesNotThrow()
		Dim obj As MockOptionalDependencyDefaults = New MockOptionalDependencyDefaults()

		builder.BuildUp(locator, GetType(MockOptionalDependencyDefaults), "foo", obj)

		Assert.IsNull(obj.Dependency)
	End Sub

#Region "Helper classes"

	Private Class MockDefaultDependency
		Private innerDependency As Object

		<ComponentDependency("dependency")> _
		Public Property Dependency() As Object
			Get
				Return innerDependency
			End Get
			Set(ByVal value As Object)
				innerDependency = value
			End Set
		End Property
	End Class

	Private Class MockOptionalDependencyDefaults
		Private innerDependency As Object

		<ComponentDependency("dependency", Required:=False)> _
		Public Property Dependency() As Object
			Get
				Return innerDependency
			End Get
			Set(ByVal value As Object)
				innerDependency = value
			End Set
		End Property
	End Class

	Private Class MockRequiredDependencyCreate
		Private innerDependency As Object

		<ComponentDependency("dependency", CreateIfNotFound:=True)> _
		Public Property Dependency() As Object
			Get
				Return innerDependency
			End Get
			Set(ByVal value As Object)
				innerDependency = value
			End Set
		End Property
	End Class

	Private Class MockRequiredDependencyCreateWithType
		Private innerDependency As Object

		<ComponentDependency("dependency", CreateIfNotFound:=True, Type:=GetType(Dependency))> _
		Public Property Dependency() As Object
			Get
				Return innerDependency
			End Get
			Set(ByVal value As Object)
				innerDependency = value
			End Set
		End Property
	End Class

	Private Class Dependency
	End Class

#End Region
End Class
