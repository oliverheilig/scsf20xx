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


Imports Microsoft.VisualStudio.TestTools.UnitTesting








Imports System
Imports System.Threading
Imports Microsoft.Practices.CompositeUI.Utility

<TestClass()> _
Public Class WriterLockFixture
	<TestMethod()> _
	Public Sub CanAcquireAndReleaseWriterLock()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()

		Using New WriterLock(rwLock)
			Assert.IsTrue(rwLock.IsWriterLockHeld)
		End Using

		Assert.IsFalse(rwLock.IsWriterLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub CanAcquireWriterLockWithTimoutMilliseconds()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()

		Using New WriterLock(rwLock, 5000)
			Assert.IsTrue(rwLock.IsWriterLockHeld)
		End Using

		Assert.IsFalse(rwLock.IsWriterLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub CanAcquireWriterLockWithTimoutTimeSpan()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()

		Using New WriterLock(rwLock, TimeSpan.FromSeconds(1))
			Assert.IsTrue(rwLock.IsWriterLockHeld)
		End Using

		Assert.IsFalse(rwLock.IsWriterLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub WriterTimedOutIsSetIfTimeoutMilliseconds()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()
		Dim t As Thread = New Thread(New ParameterizedThreadStart(AddressOf AcquireReaderLock))
		t.Start(rwLock)

		Thread.Sleep(20)

		Using l As WriterLock = New WriterLock(rwLock, 10)
			Assert.IsTrue(l.TimedOut)
		End Using

		Assert.IsFalse(rwLock.IsWriterLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub WriterTimedOutIsSetIfTimeoutTimespan()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()
		Dim t As Thread = New Thread(New ParameterizedThreadStart(AddressOf AcquireReaderLock))
		t.Start(rwLock)

		Thread.Sleep(20)

		Using l As WriterLock = New WriterLock(rwLock, TimeSpan.FromMilliseconds(10))
			Assert.IsTrue(l.TimedOut)
		End Using

		Assert.IsFalse(rwLock.IsWriterLockHeld)
	End Sub

	Private Sub AcquireReaderLock(ByVal state As Object)
		Dim lock As ReaderWriterLock = DirectCast(state, ReaderWriterLock)
		lock.AcquireReaderLock(-1)
		Thread.Sleep(100)
	End Sub

	Private Sub AcquireWriterLock(ByVal state As Object)
		Dim lock As ReaderWriterLock = DirectCast(state, ReaderWriterLock)
		lock.AcquireWriterLock(-1)
		Thread.Sleep(100)
	End Sub

End Class
