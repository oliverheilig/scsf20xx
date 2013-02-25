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
Imports System.Configuration
Imports System.Globalization
Imports Microsoft.Practices.ObjectBuilder

Namespace Configuration
	''' <summary>
	''' Attribute for Dependency Injection of a reference to an <see cref="ApplicationSettingsBase"/>-derived
	''' class that provides the configuration for the component.
	''' </summary>
	<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Parameter, AllowMultiple:=False, Inherited:=True)> _
	Public NotInheritable Class ConfigurationAttribute
		Inherits ParameterAttribute

		Private settingsKey As String

		''' <summary>
		''' Initializes the attribute with no special key for the configuration settings class.
		''' </summary>
		Public Sub New()
		End Sub

		''' <summary>
		''' Initializes the attribute with a key to use to specialize the configuration settings class.
		''' </summary>
		''' <param name="settingsKey">The key used to specialize the configuration settings class.</param>
		Public Sub New(ByVal settingsKey As String)
			Me.settingsKey = settingsKey
		End Sub

		''' <summary>
		''' See <see cref="ParameterAttribute.CreateParameter"/> for more information.
		''' </summary>
		Public Overrides Function CreateParameter(ByVal memberType As Type) As IParameter
			If (Not GetType(ApplicationSettingsBase).IsAssignableFrom(memberType)) Then
				Throw New ArgumentException(String.Format(CultureInfo.CurrentCulture, My.Resources.ConfigurationAttributeOnValueWithIncompatibleType, memberType))
			End If

			Return New ConfigurationParameter(memberType, settingsKey)
		End Function

		Private Class ConfigurationParameter
			Inherits KnownTypeParameter

			Private Shared cache As Dictionary(Of String, ApplicationSettingsBase) = New Dictionary(Of String, ApplicationSettingsBase)()
			Private settingsKey As String

			Public Sub New(ByVal type As Type, ByVal settingsKey As String)
				MyBase.New(type)
				Me.settingsKey = settingsKey
			End Sub

			Public Overrides Function GetValue(ByVal context As IBuilderContext) As Object
				SyncLock cache
					Dim cacheKey As String = type.ToString()

					If Not settingsKey Is Nothing Then
						cacheKey &= "-" & settingsKey
					End If

					Dim settings As ApplicationSettingsBase = cache(cacheKey)

					If settings Is Nothing Then
						settings = CType(context.HeadOfChain.BuildUp(context, type, Nothing, Nothing), ApplicationSettingsBase)

						If Not settingsKey Is Nothing Then
							settings.SettingsKey = settingsKey
						End If

						cache.Add(cacheKey, settings)
					End If

					Return settings
				End SyncLock
			End Function
		End Class
	End Class
End Namespace
