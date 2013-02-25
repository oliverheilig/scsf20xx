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
Imports Microsoft.Practices.CompositeUI.Utility

''' <summary>
''' Helper class that makes it easier to ensure proper usage of a <see cref="ReaderWriterLock"/> 
''' for writers by providing support for <see cref="IDisposable"/> and the using keyword.
''' </summary>
''' <example>
''' Common usage is as follows:
''' <code>
''' Dim myLock As ReaderWriterLock = New ReaderWriterLock()
''' 
''' ' Inside some method
''' Using New WriterLock(rwLock)
'''		' Do your safe resource write accesses here.
'''	End Using
''' </code>
''' If a timeout is specified, the <see cref="LockBase.TimedOut"/> property should be checked inside the 
''' using statement:
''' <code>
''' Dim myLock As ReaderWriterLock = New ReaderWriterLock()
''' 
''' ' Inside some method
''' Using l As WriterLock = New WriterLock(rwLock, 100)
'''		If l.TimedOut = False Then
'''			' Do your safe resource write accesses here.
'''		Else
'''			Throw New InvalidOperationException("Could not lock the resource.")
'''		End If
'''	End Using
''' </code>
''' </example>
Public Class WriterLock : Inherits LockBase
	''' <summary>
	''' Acquires a reader lock on the rwLock received 
	''' with no timeout specified.
	''' </summary>
	Public Sub New(ByVal rwLock As ReaderWriterLock)
		MyBase.New(New AcquireIntTimeoutMethod(AddressOf rwLock.AcquireWriterLock), New ReleaseMethod(AddressOf rwLock.ReleaseWriterLock), -1)
	End Sub

	''' <summary>
	''' Tries to acquire a reader lock on the rwLock received 
	''' with the timeout specified. 
	''' </summary>
	Public Sub New(ByVal rwLock As ReaderWriterLock, ByVal millisecondsTimeout As Integer)
		MyBase.New(New AcquireIntTimeoutMethod(AddressOf rwLock.AcquireWriterLock), New ReleaseMethod(AddressOf rwLock.ReleaseWriterLock), millisecondsTimeout)
	End Sub

	''' <summary>
	''' Tries to acquire a reader lock on the rwLock received 
	''' with the timeout specified. 
	''' </summary>
	Public Sub New(ByVal rwLock As ReaderWriterLock, ByVal timeout As TimeSpan)
		MyBase.New(New AcquireTimeSpanTimeoutMethod(AddressOf rwLock.AcquireWriterLock), New ReleaseMethod(AddressOf rwLock.ReleaseWriterLock), timeout)
	End Sub
End Class
