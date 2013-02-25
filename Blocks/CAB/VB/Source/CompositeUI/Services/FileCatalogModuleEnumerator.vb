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
Imports Microsoft.Practices.CompositeUI.Configuration
Imports Microsoft.Practices.CompositeUI.Utility
Imports System
Imports System.Globalization
Imports System.Collections.Specialized
Imports Microsoft.Practices.CompositeUI.Configuration.Xsd
Namespace Services
	''' <summary>
	''' This implementation of IModuleEnumerator processes the assemblies specified
	''' in a solution profile.
	''' </summary>
	Public Class FileCatalogModuleEnumerator
		Implements IModuleEnumerator, IConfigurable

		Private innerCatalogFilePath As String = SolutionProfileReader.DefaultCatalogFile

		''' <summary>
		''' Initializes a new instance which will use the <see cref="SolutionProfileReader.DefaultCatalogFile"/> as
		''' the solution profile
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Initializes a new instance that will use the specified file as the solution profile.
		''' </summary>
		''' <param name="catalogFilePath">The path to the solution profile. This file must be
		''' located under the application folder.</param>
		Public Sub New(ByVal catalogFilePath As String)
			Guard.ArgumentNotNullOrEmptyString(catalogFilePath, "catalogFilePath")
			Me.innerCatalogFilePath = catalogFilePath
		End Sub

		''' <summary>
		''' Processes the solution profile and returns the list of modules specified in it.
		''' </summary>
		''' <returns>An array of <see cref="Configuration.ModuleInfo"/> instances.</returns>
		Public Function EnumerateModules() As IModuleInfo() Implements IModuleEnumerator.EnumerateModules
			Try
				Dim reader As SolutionProfileReader = New SolutionProfileReader(innerCatalogFilePath)
				Dim profile As SolutionProfileElement = reader.ReadProfile()
				Return CreateModuleInfos(profile)
			Catch ex As Exception
				Throw New ModuleEnumeratorException(String.Format(CultureInfo.CurrentCulture, My.Resources.ErrorEnumeratingModules, Me), ex)
			End Try
		End Function

		''' <summary>
		''' Gets or sets the file path to the solution profile file.
		''' </summary>
		Public Property CatalogFilePath() As String
			Get
				Return innerCatalogFilePath
			End Get
			Set(ByVal value As String)
				innerCatalogFilePath = Value
			End Set
		End Property

		''' <summary>
		''' Configures the enumerator by passing the path to the solution profile file.
		''' </summary>
		''' <param name="settings">Provide a value with the key 
		''' "filePath" that points to a file that is valid according to the schema defined by SolutionProfile.xsd</param>
		''' <remarks>
		''' If the file path is absent the catalog will be read from 
		''' the <see cref="SolutionProfileReader.DefaultCatalogFile"/>.
		''' </remarks>
		Public Sub Configure(ByVal settings As NameValueCollection) Implements IConfigurable.Configure
			Guard.ArgumentNotNull(settings, "settings")

			If (Not String.IsNullOrEmpty(settings("filePath"))) Then
				innerCatalogFilePath = settings("filePath")
			End If
		End Sub

		Private Shared Function CreateModuleInfos(ByVal solutionProfile As SolutionProfileElement) As IModuleInfo()
			Dim mInfos As ModuleInfo() = New ModuleInfo(solutionProfile.Modules.Length - 1) {}
			Dim i As Integer = 0
			Do While i < solutionProfile.Modules.Length
				Dim xsdModule As ModuleInfoElement = solutionProfile.Modules(i)
				Dim mInfo As ModuleInfo = New ModuleInfo(xsdModule.AssemblyFile)
				mInfo.SetUpdateLocation(xsdModule.UpdateLocation)
				If Not xsdModule.Roles Is Nothing AndAlso xsdModule.Roles.Length > 0 Then
					For Each role As RoleElement In xsdModule.Roles
						mInfo.AddRoles(role.Allow)
					Next role
				End If
				mInfos(i) = mInfo
				i += 1
			Loop
			Return mInfos
		End Function
	End Class
End Namespace