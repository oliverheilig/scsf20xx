Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Text

Namespace BankTellerCommon
	Public Class UIExtensionConstants
		Public Shared ReadOnly CUSTOMERCONTEXT As String = "CustomerContext"

		Public Shared ReadOnly FILE As String = "File"

		Public Shared ReadOnly MAINMENU As String = "MainMenu"

		Public Shared ReadOnly FILEDROPDOWN As String = "FileDropDown"

		Public Shared ReadOnly MAINSTATUS As String = "MainStatus"

		Public Shared ReadOnly QUEUE As String = "Queue"

		Public Shared ReadOnly CUSTOMER As String = "Customer"
	End Class

	''' <summary>
	''' Used for creating and handling commands.
	''' These are constants because they are used in attributes.
	''' </summary>
	Public Class CommandConstants
		Public Const ACCEPT_CUSTOMER As String = "QueueAcceptCustomer"

		Public Const EDIT_CUSTOMER As String = "EditCustomer"

		Public Const CUSTOMER_MOUSEOVER As String = "CustomerMouseOver"
	End Class

	''' <summary>
	''' Used for handling State.
	''' These are constants because they are used in attributes.
	''' </summary>
	Public Class StateConstants
		Public Const CUSTOMER As String = "Customer2"
	End Class

	Public Class WorkspacesConstants
		Public Shared ReadOnly SHELL_SIDEBAR As String = "sideBarWorkspace"
		Public Shared ReadOnly SHELL_CONTENT As String = "contentWorkspace"
	End Class
End Namespace
