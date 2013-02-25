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
Imports System.ComponentModel
Imports System.Runtime.Serialization
Imports System.Security.Permissions
Imports System.Diagnostics


''' <summary>
''' Provides a dictionary of information which provides notification when items change
''' in the collection. It is serialized with the <see cref="WorkItem"/> when the
''' <see cref="WorkItem"/> is saved and loaded.
''' </summary>
<Serializable()> _
Public Class State
	Inherits StateElement
	Implements ISerializable

	Private innerHasChanged As Boolean
	Private stateID As String
	Private innerTraceSource As TraceSource = Nothing

	''' <summary>
	''' Sets the <see cref="TraceSource"/> to use for tracing messages.
	''' </summary>
	<ClassNameTraceSourceAttribute()> _
	Public WriteOnly Property TraceSource() As TraceSource
		Set(ByVal value As TraceSource)
			innerTraceSource = value
		End Set
	End Property

	''' <summary>
	''' Initializes a new instance of the <see cref="State"/> class using a random ID.
	''' </summary>
	Public Sub New()
		Me.New(Guid.NewGuid().ToString())
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="State"/> class using the provided ID.
	''' </summary>
	''' <param name="id">The ID for the state.</param>
	Public Sub New(ByVal id As String)
		Me.stateID = id
	End Sub

	''' <summary>
	''' Initializes a new instance of the <see cref="State"/> class using the provided
	''' serialization information.
	''' </summary>
	''' <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
	''' <param name="context">The destination (see <see cref="StreamingContext"/>) for this serialization. </param>
	Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
		MyBase.New(info, context)
		Me.stateID = CStr(info.GetValue("id", GetType(String)))
	End Sub

	''' <summary>
	''' Populates a System.Runtime.Serialization.SerializationInfo with the data
	''' needed to serialize the target object.
	''' </summary>
	''' <param name="info">The System.Runtime.Serialization.SerializationInfo to populate with data.</param>
	''' <param name="context">The destination <see cref="StreamingContext"/>
	''' for this serialization.</param>
	<SecurityPermission(SecurityAction.Demand, Flags:=SecurityPermissionFlag.SerializationFormatter)> _
	Public Overrides Sub GetObjectData(ByVal info As SerializationInfo, ByVal context As StreamingContext) 'Implements ISerializable.GetObjectData
		MyBase.GetObjectData(info, context)
		info.AddValue("id", Me.stateID)
	End Sub

	''' <summary>
	''' Gets and sets the value of an element in the state.
	''' </summary>
	Default Public Shadows Property Item(ByVal key As String) As Object
		Get
			Return MyBase.Item(key)
		End Get
		Set(ByVal value As Object)
			MyBase.Item(key) = value
		End Set
	End Property

	''' <summary>
	''' Resets the <see cref="HasChanges"/> flag.
	''' </summary>
	Public Sub AcceptChanges()
		innerHasChanged = False
	End Sub

	''' <summary>
	''' Gets whether the state has changed since it was initially created or 
	''' the last call to <see cref="AcceptChanges"/>.
	''' </summary>
	<DefaultValue(False)> _
	Public ReadOnly Property HasChanges() As Boolean
		Get
			Return innerHasChanged
		End Get
	End Property

	''' <summary>
	''' Gets the state ID.
	''' </summary>
	Public ReadOnly Property ID() As String
		Get
			Return Me.stateID
		End Get
	End Property

	''' <summary>
	''' Raises the <see cref="StateElement.StateChanged"/> event and sets the <see cref="HasChanges"/> flag to true.
	''' </summary>
	Protected Overrides Sub OnStateChanged(ByVal e As StateChangedEventArgs)
		If Not innerTraceSource Is Nothing Then
			innerTraceSource.TraceInformation(My.Resources.TraceStateChangedMessage, stateID, e.Key)
		End If
		MyBase.OnStateChanged(e)
		innerHasChanged = True
	End Sub

	Private Class ClassNameTraceSourceAttribute
		Inherits TraceSourceAttribute

		Public Sub New()
			MyBase.New(GetType(State).FullName)
		End Sub
	End Class

End Class
