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
Imports Microsoft.Practices.CompositeUI.Utility

Namespace Commands
	''' <summary>
	''' This is the <see cref="ICommandAdapterMapService"/> default implementation.
	''' </summary>
	Public Class CommandAdapterMapService : Implements ICommandAdapterMapService
		Private map As Dictionary(Of Type, Type) = New Dictionary(Of Type, Type)()

		''' <summary>
		''' Registers an adapter type to be created for the specified invoker type.
		''' </summary>
		''' <param name="invokerType">The invoker type to associate with the specified adapter type. 
		''' This type can be any type.</param>
		''' <param name="adapterType">Any type implementing the <see cref="CommandAdapter"/> interface.</param>
		Public Sub Register(ByVal invokerType As Type, ByVal adapterType As Type) Implements ICommandAdapterMapService.Register
			Guard.ArgumentNotNull(invokerType, "invokerType")
			Guard.ArgumentNotNull(adapterType, "adapterType")

			If (Not GetType(CommandAdapter).IsAssignableFrom(adapterType)) Then
				Throw New AdapterMapServiceException(My.Resources.InvalidAdapter)
			End If

			map(invokerType) = adapterType
		End Sub

		''' <summary>
		''' Creates a new <see cref="CommandAdapter"/> instance for the specified invoker type.
		''' </summary>
		''' <param name="invokerType">The invoker type to create an adapter for.</param>
		''' <returns>A new <see cref="CommandAdapter"/> instance for the registered invoker;
		''' Nothing if there is no adapter registration for the required invoker type.</returns>
		Public Function CreateAdapter(ByVal invokerType As Type) As CommandAdapter Implements ICommandAdapterMapService.CreateAdapter
			Guard.ArgumentNotNull(invokerType, "invokerType")

			Dim adapter As CommandAdapter = Nothing
			If map.ContainsKey(invokerType) Then
				adapter = CType(Activator.CreateInstance(map(invokerType)), CommandAdapter)
			Else
				adapter = FindAssignableAdapter(invokerType)
			End If

			Return adapter
		End Function

		Private Function FindAssignableAdapter(ByVal type As Type) As CommandAdapter
			Dim adapter As CommandAdapter = Nothing
			For Each pair As KeyValuePair(Of Type, Type) In map
				If pair.Key.IsAssignableFrom(type) Then
					adapter = CType(Activator.CreateInstance(pair.Value), CommandAdapter)
					Exit For
				End If
			Next pair
			Return adapter
		End Function


		''' <summary>
		''' Removes the adapter registration for the specified invoker type.
		''' </summary>
		''' <param name="invokerType">The invoker type to remove.</param>
		Public Sub UnRegister(ByVal invokerType As Type) Implements ICommandAdapterMapService.UnRegister
			map.Remove(invokerType)
		End Sub

	End Class
End Namespace