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
Imports System.Configuration

Namespace Configuration
	''' <summary>
	''' Definition of the configuration section for the block.
	''' </summary>
	Public Class SettingsSection
		Inherits ConfigurationSection

		''' <summary>
		''' The configuration section name for this section.
		''' </summary>
		Public Const SectionName As String = "CompositeUI"

		''' <summary>
		''' List of startup services that will be initialized on the host.
		''' </summary>
		<ConfigurationProperty("services", IsRequired:=True)> _
		Public ReadOnly Property Services() As ServiceElementCollection
			Get
				Return CType(Me("services"), ServiceElementCollection)
			End Get
		End Property

		''' <summary>
		''' Optional visualizer.
		''' </summary>
		<ConfigurationProperty("visualizer", IsRequired:=False)> _
		Public ReadOnly Property Visualizer() As VisualizationElementCollection
			Get
				Return CType(Me("visualizer"), VisualizationElementCollection)
			End Get
		End Property
	End Class
End Namespace