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
Public Class ReaderLockFixture

	<TestMethod()> _
	Public Sub CanAcquireAndReleaseReaderLock()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()

		Using New ReaderLock(rwLock)
			Assert.IsTrue(rwLock.IsReaderLockHeld)
		End Using

		Assert.IsFalse(rwLock.IsReaderLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub CanAcquireReaderLockWithTimoutMilliseconds()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()

		Using (New ReaderLock(rwLock, 5000))
			Assert.IsTrue(rwLock.IsReaderLockHeld)
		End Using

		Assert.IsFalse(rwLock.IsReaderLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub CanAcquireReaderLockWithTimoutTimeSpan()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()

		Using New ReaderLock(rwLock, TimeSpan.FromSeconds(1))
			Assert.IsTrue(rwLock.IsReaderLockHeld)
		End Using

		Assert.IsFalse(rwLock.IsReaderLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub ReaderTimedOutIsSetIfTimeoutMilliseconds()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()
		Dim t As Thread = New Thread(New ParameterizedThreadStart(AddressOf AcquireWriterLock))
		t.Start(rwLock)

		Thread.Sleep(20)

		Using l As ReaderLock = New ReaderLock(rwLock, 10)
			Assert.IsTrue(l.TimedOut)
		End Using

		Assert.IsFalse(rwLock.IsReaderLockHeld)
	End Sub

	<TestMethod()> _
	Public Sub ReaderTimedOutIsSetIfTimeoutTimespan()
		Dim rwLock As ReaderWriterLock = New ReaderWriterLock()
		Dim t As Thread = New Thread(New ParameterizedThreadStart(AddressOf AcquireWriterLock))
		t.Start(rwLock)

		Thread.Sleep(20)

		Using l As ReaderLock = New ReaderLock(rwLock, TimeSpan.FromMilliseconds(10))
			Assert.IsTrue(l.TimedOut)
		End Using

		Assert.IsFalse(rwLock.IsReaderLockHeld)
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
