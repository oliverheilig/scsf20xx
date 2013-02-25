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
Imports System.Threading

Namespace Utility
	Friend Delegate Sub AcquireIntTimeoutMethod(ByVal millisecondsTimeout As Integer)
	Friend Delegate Sub AcquireTimeSpanTimeoutMethod(ByVal timeout As TimeSpan)
	Friend Delegate Sub ReleaseMethod()

	''' <summary>
	''' Base class for <see cref="ReaderWriterLock"/> helper classes 
	''' <see cref="ReaderLock"/> and <see cref="WriterLock"/>.
	''' </summary>
	''' <seealso cref="ReaderLock"/>
	''' <seealso cref="WriterLock"/>
	Public MustInherit Class LockBase : Implements IDisposable
		Private release As ReleaseMethod
		Private innerTimedOut As Boolean = False

		Friend Sub New(ByVal acquire As AcquireIntTimeoutMethod, ByVal release As ReleaseMethod, ByVal timeout As Integer)
			Me.release = release

			Try
				acquire(timeout)
			Catch e1 As ApplicationException
				innerTimedOut = True
			End Try
		End Sub

		Friend Sub New(ByVal acquire As AcquireTimeSpanTimeoutMethod, ByVal release As ReleaseMethod, ByVal timeout As TimeSpan)
			Me.release = release

			Try
				acquire(timeout)

			Catch e1 As ApplicationException
				innerTimedOut = True
			End Try
		End Sub

		''' <summary>
		''' If a timeout was specified in the constructor, it returns whether the operation 
		''' succeeded or timed out.
		''' </summary>
		Public ReadOnly Property TimedOut() As Boolean
			Get
				Return innerTimedOut
			End Get
		End Property

		''' <summary>
		''' Releases the lock acquired at construction time.
		''' </summary>
		Public Sub Dispose() Implements IDisposable.Dispose
			Dispose(True)
			GC.SuppressFinalize(Me)
		End Sub

		''' <summary>
		''' Called to free resources.
		''' </summary>
		''' <param name="disposing">Should be true when calling from Dispose().</param>
		Protected Overridable Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (Not TimedOut) Then
				release()
			End If
		End Sub

	End Class
End Namespace
