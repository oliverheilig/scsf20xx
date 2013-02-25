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

using System;
using System.Threading;

namespace Microsoft.Practices.CompositeUI
{
	/// <summary>
	/// Helper class that makes it easier to ensure proper usage of a <see cref="ReaderWriterLock"/> 
	/// for readers by providing support for <see cref="IDisposable"/> and the using keyword.
	/// </summary>
	/// <example>
	/// Common usage is as follows:
	/// <code>
	/// ReaderWriterLock mylock = new ReaderWriterLock();
	/// 
	/// // Inside some method
	/// using (new ReaderLock(rwLock))
	///	{
	///		// Do your safe resource read accesses here.
	///	}
	/// </code>
	/// If a timeout is specified, the <see cref="LockBase.TimedOut"/> property should be checked inside the 
	/// using statement:
	/// <code>
	/// ReaderWriterLock mylock = new ReaderWriterLock();
	/// 
	/// // Inside some method
	/// using (ReaderLock l = new ReaderLock(rwLock, 100))
	///	{
	///		if (l.TimedOut == false)
	///		{
	///			// Do your safe resource read accesses here.
	///		}
	///		else
	///		{
	///			throw new InvalidOperationException("Could not lock the resource.");
	///		}
	///	}
	/// </code>
	/// </example>
	public class ReaderLock : LockBase
	{
		/// <summary>
		/// Acquires a reader lock on the rwLock received 
		/// with no timeout specified.
		/// </summary>
		public ReaderLock(ReaderWriterLock rwLock)
			: base(new AcquireIntTimeoutMethod(rwLock.AcquireReaderLock),
			new ReleaseMethod(rwLock.ReleaseReaderLock), -1) { }

		/// <summary>
		/// Tries to acquire a reader lock on the  rwLock received 
		/// with the timeout specified. 
		/// </summary>
		public ReaderLock(ReaderWriterLock rwLock, int millisecondsTimeout)
			: base(new AcquireIntTimeoutMethod(rwLock.AcquireReaderLock),
			new ReleaseMethod(rwLock.ReleaseReaderLock), millisecondsTimeout) { }

		/// <summary>
		/// Tries to acquire a reader lock on the rwLock received 
		/// with the timeout specified. 
		/// </summary>
		public ReaderLock(ReaderWriterLock rwLock, TimeSpan timeout)
			: base(new AcquireTimeSpanTimeoutMethod(rwLock.AcquireReaderLock),
			new ReleaseMethod(rwLock.ReleaseReaderLock), timeout) { }
	}
}
