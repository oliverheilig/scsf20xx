//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Microsoft.Practices.CompositeUI.Tests
{
	[TestClass]
	public class ReaderLockFixture
	{
		[TestMethod]
		public void CanAcquireAndReleaseReaderLock()
		{
			ReaderWriterLock rwLock = new ReaderWriterLock();

			using (new ReaderLock(rwLock))
			{
				Assert.IsTrue(rwLock.IsReaderLockHeld);
			}

			Assert.IsFalse(rwLock.IsReaderLockHeld);
		}

		[TestMethod]
		public void CanAcquireReaderLockWithTimoutMilliseconds()
		{
			ReaderWriterLock rwLock = new ReaderWriterLock();

			using (new ReaderLock(rwLock, 5000))
			{
				Assert.IsTrue(rwLock.IsReaderLockHeld);
			}

			Assert.IsFalse(rwLock.IsReaderLockHeld);

		}

		[TestMethod]
		public void CanAcquireReaderLockWithTimoutTimeSpan()
		{
			ReaderWriterLock rwLock = new ReaderWriterLock();

			using (new ReaderLock(rwLock, TimeSpan.FromSeconds(1)))
			{
				Assert.IsTrue(rwLock.IsReaderLockHeld);
			}

			Assert.IsFalse(rwLock.IsReaderLockHeld);

		}

		[TestMethod]
		public void ReaderTimedOutIsSetIfTimeoutMilliseconds()
		{
			ReaderWriterLock rwLock = new ReaderWriterLock();
			Thread t = new Thread(new ParameterizedThreadStart(AcquireWriterLock));
			t.Start(rwLock);

			Thread.Sleep(20);

			using (ReaderLock l = new ReaderLock(rwLock, 10))
			{
				Assert.IsTrue(l.TimedOut);
			}

			Assert.IsFalse(rwLock.IsReaderLockHeld);
		}

		[TestMethod]
		public void ReaderTimedOutIsSetIfTimeoutTimespan()
		{
			ReaderWriterLock rwLock = new ReaderWriterLock();
			Thread t = new Thread(new ParameterizedThreadStart(AcquireWriterLock));
			t.Start(rwLock);

			Thread.Sleep(20);

			using (ReaderLock l = new ReaderLock(rwLock, TimeSpan.FromMilliseconds(10)))
			{
				Assert.IsTrue(l.TimedOut);
			}

			Assert.IsFalse(rwLock.IsReaderLockHeld);
		}

		private void AcquireReaderLock(object state)
		{
			((ReaderWriterLock)state).AcquireReaderLock(-1);
			Thread.Sleep(100);
		}

		private void AcquireWriterLock(object state)
		{
			((ReaderWriterLock)state).AcquireWriterLock(-1);
			Thread.Sleep(100);
		}
	}
}
