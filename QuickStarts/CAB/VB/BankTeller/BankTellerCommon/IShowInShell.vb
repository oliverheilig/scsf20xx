Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Practices.CompositeUI.SmartParts

Namespace BankTellerCommon
	Public Interface IShowInShell
		Sub Show(ByVal sideBar As IWorkspace, ByVal content As IWorkspace)

	End Interface
End Namespace