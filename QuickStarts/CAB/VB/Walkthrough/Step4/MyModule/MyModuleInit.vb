Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.Services

Namespace MyModule
	Public Class MyModuleInit : Inherits ModuleInit
		Private myCatalogService As IWorkItemTypeCatalogService
		Private innerParentWorkItem As WorkItem

		<ServiceDependency()> _
		Public WriteOnly Property myWorkItemCatalog() As IWorkItemTypeCatalogService
			Set(ByVal value As IWorkItemTypeCatalogService)
				myCatalogService = value
			End Set
		End Property

		<ServiceDependency()> _
		Public WriteOnly Property ParentWorkItem() As WorkItem
			Set(ByVal value As WorkItem)
				innerParentWorkItem = value
			End Set
		End Property

		Public Overrides Sub Load()
			MyBase.Load()
			Dim myWorkItem As MyWorkItem = innerParentWorkItem.WorkItems.AddNew(Of MyWorkItem)()
			myWorkItem.Run(innerParentWorkItem.Workspaces("tabWorkspace1"))
		End Sub
	End Class
End Namespace
