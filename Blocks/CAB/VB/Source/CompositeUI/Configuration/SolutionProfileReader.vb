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
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports System.Security.Principal
Imports System.Threading
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.Serialization
Imports Microsoft.Practices.CompositeUI.Configuration.Xsd
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Configuration
	''' <summary>
	''' Reads the <see cref="SolutionProfileElement"/> from a file.
	''' </summary>
	Public Class SolutionProfileReader
		Private innerCatalogFilePath As String = DefaultCatalogFile

		''' <summary>
		''' The default profile to use if no profile is specified.
		''' </summary>
		Public Const DefaultCatalogFile As String = "ProfileCatalog.xml"

		''' <summary>
		''' Initializes a new instance which will use the <see cref="DefaultCatalogFile"/> as
		''' the solution profile.
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Initializes a new instance that will use the specified file as the solution profile.
		''' </summary>
		''' <param name="catalogFilePath">The path to the solution profile. This file must be
		''' located under the application folder.</param>
		Public Sub New(ByVal catalogFilePath As String)
			Me.innerCatalogFilePath = GetFullPathOrThrowIfInvalid(catalogFilePath)
		End Sub

		''' <summary>
		''' Gets the solution profile file path.
		''' </summary>
		Public ReadOnly Property CatalogFilePath() As String
			Get
				Return innerCatalogFilePath
			End Get
		End Property

		''' <summary>
		''' Reads the solution profile form the specified file.
		''' </summary>
		''' <returns>An instance of <see cref="SolutionProfileElement"/>.</returns>
		Public Function ReadProfile() As SolutionProfileElement
			Dim solution As SolutionProfileElement = New SolutionProfileElement()
			If File.Exists(innerCatalogFilePath) Then
				Try
					Dim serializer As XmlSerializer = New XmlSerializer(GetType(SolutionProfileElement))
					Using reader As XmlReader = GetValidatingReader()
						solution = CType(serializer.Deserialize(reader), SolutionProfileElement)
						ProcessProfileRoles(solution)
						Return solution
					End Using
				Catch ex As Exception
					Throw New SolutionProfileReaderException(String.Format(CultureInfo.CurrentCulture, _
								My.Resources.ErrorReadingProfile, innerCatalogFilePath), ex)
				End Try
			Else
				ThrowIfCatalogPathNotDefault()
			End If
			Return solution
		End Function

		Private Function GetValidatingReader() As XmlReader
			Dim stream As Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.Practices.CompositeUI.SolutionProfile.xsd")
			Dim schemaReader As XmlTextReader = New XmlTextReader(stream)
			stream.Dispose()

			Dim settings As XmlReaderSettings = New XmlReaderSettings()
			settings.Schemas.Add("http://schemas.microsoft.com/pag/cab-profile", schemaReader)
			Dim catalogReader As XmlReader = XmlReader.Create(innerCatalogFilePath, settings)

			Return catalogReader
		End Function

		''' <summary>
		''' Processes the solution profile comparing the roles the user has against
		''' the roles specified on the solution profile. The list of modules in the 
		''' solution profile is modified eliminating the modules the user has not
		''' access to according his/her roles.
		''' </summary>
		''' <param name="profile">The solution profile specifying the modules and
		''' the roles needed to use that modules.</param>
		Private Shared Sub ProcessProfileRoles(ByVal profile As SolutionProfileElement)
			Dim principal As IPrincipal = Thread.CurrentPrincipal

			Dim permittedModules As List(Of ModuleInfoElement) = New List(Of ModuleInfoElement)()
			If Not profile.Modules Is Nothing Then
				For Each modInfo As ModuleInfoElement In profile.Modules
					If Not modInfo.Roles Is Nothing Then
						If principal.Identity.IsAuthenticated Then
							For Each role As RoleElement In modInfo.Roles
								If principal.IsInRole(role.Allow) Then
									permittedModules.Add(modInfo)
									Exit For
								End If
							Next role
						End If
					Else
						permittedModules.Add(modInfo)
					End If
				Next modInfo
				profile.Modules = permittedModules.ToArray()
			End If
		End Sub

		Private Sub ThrowIfCatalogPathNotDefault()
			Dim defaultFullPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DefaultCatalogFile)
			If String.Compare(innerCatalogFilePath, DefaultCatalogFile, True, CultureInfo.CurrentCulture) = 0 _
				OrElse String.Compare(innerCatalogFilePath, defaultFullPath, True, CultureInfo.CurrentCulture) = 0 Then

				Return
			End If

			Throw New SolutionProfileReaderException(String.Format(CultureInfo.CurrentCulture, My.Resources.SolutionProfileNotFound, innerCatalogFilePath))
		End Sub

		Private Function GetFullPathOrThrowIfInvalid(ByVal catalogFilePath As String) As String
			' Only change the default if we have a non-empty value.
			Guard.ArgumentNotNullOrEmptyString(catalogFilePath, "catalogFilePath")

			Dim fullPath As String = catalogFilePath

			If (Not Path.IsPathRooted(fullPath)) Then
				fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fullPath)
			End If

			' Simplify relative path movements.
			fullPath = New FileInfo(fullPath).FullName

			If (Not fullPath.StartsWith(AppDomain.CurrentDomain.BaseDirectory, True, CultureInfo.CurrentCulture)) Then
				Throw New SolutionProfileReaderException(String.Format(CultureInfo.CurrentCulture, _
							My.Resources.InvalidSolutionProfilePath, catalogFilePath, AppDomain.CurrentDomain.BaseDirectory))
			End If

			Return fullPath
		End Function

	End Class
End Namespace