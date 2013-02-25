Imports Microsoft.VisualBasic
Imports System
Imports System.Configuration
Imports System.Windows.Forms
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.WinForms

Namespace BankShell
	''' <summary>
	''' This is a temporary implementation that will be replaced with something
	''' richer when we move it into the framework.
	''' </summary>
	Public Class UIElementBuilder
		' Loads the menu items from App.config and put them into the menu strip, hooking
		' up the menu URIs for command dispatching.
		Private Sub New()
		End Sub
		Public Shared Sub LoadFromConfig(ByVal workItem As WorkItem)
			Dim section As ShellItemsSection = CType(ConfigurationManager.GetSection("shellitems"), ShellItemsSection)

			For Each menuItem As MenuItemElement In section.MenuItems
				Dim uiMenuItem As ToolStripMenuItem = menuItem.ToMenuItem()

				workItem.UIExtensionSites(menuItem.Site).Add(uiMenuItem)

				If menuItem.Register = True Then
					workItem.UIExtensionSites.RegisterSite(menuItem.RegistrationSite, uiMenuItem.DropDownItems)
				End If

				If (Not String.IsNullOrEmpty(menuItem.CommandName)) Then
					workItem.Commands(menuItem.CommandName).AddInvoker(uiMenuItem, "Click")
				End If
			Next menuItem
		End Sub
	End Class
End Namespace
