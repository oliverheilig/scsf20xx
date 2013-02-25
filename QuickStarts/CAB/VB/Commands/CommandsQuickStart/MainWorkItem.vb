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

Imports System
Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Windows.Forms
Imports System.Xml.Serialization
Imports Microsoft.Practices.CompositeUI
Imports Microsoft.Practices.CompositeUI.UIElements
Imports Microsoft.Practices.CompositeUI.WinForms
Imports Microsoft.Practices.ObjectBuilder

Public Class MainWorkItem : Inherits WorkItem
	Protected Overrides Sub OnRunStarted()
		MyBase.OnRunStarted()
		Items.AddNew(Of MainController)()
		ProcessCommandMap()
		Activate()
	End Sub

	''' Loads the comand configuration file, and processes
	''' each element in the file. Each element will become a menu
	''' item registered with the UIElement service, attached to the
	''' extension site defined in the XML file. (Note: For this example
	''' they are all added to the extension site named "File" that was added
	''' in CommandsApplication.AfterShellCreated.
	Private Sub ProcessCommandMap()
		Dim map As CommandMap = LoadCommandMap()

		For Each mapping As Mapping In map.Mapping
			Dim item As ToolStripMenuItem = CreateMenuItemForMapping(mapping)
			UIExtensionSites(mapping.Site).Add(item)
			Commands(mapping.CommandName).AddInvoker(item, "Click")
		Next mapping
	End Sub

	''' <summary>
	''' Creates a new ToolStripMenuItem for the given Mapping from the XML file.
	''' </summary>
	''' <param name="mapping"></param>
	''' <returns></returns>
	Private Function CreateMenuItemForMapping(ByVal mapping As Mapping) As ToolStripMenuItem
		Dim item As ToolStripMenuItem = New ToolStripMenuItem()
		item.Text = mapping.Label
		Return item
	End Function

	''' <summary>
	''' Loads the command map from the configuration file.
	''' </summary>
	''' <returns></returns>
	Private Function LoadCommandMap() As CommandMap
		Dim map As CommandMap
		Using stream As Stream = New FileStream("SampleCommandMap.xml", FileMode.Open, FileAccess.Read)
			Dim serializer As XmlSerializer = New XmlSerializer(GetType(CommandMap))
			map = CType(serializer.Deserialize(stream), CommandMap)
		End Using
		Return map
	End Function
End Class
