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
using System;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;

namespace GlobalBank.BasicAccounts.Module.Tests.Mocks
{
	public class MockCurrentQueueEntryService : ICurrentQueueEntryService
	{
		private QueueEntry _entry = null;

		public QueueEntry CurrentEntry
		{
			get { return _entry; }
			set { _entry = value; }
		}

		[Obsolete]
		public void SetCurrentEntry(QueueEntry entry)
		{
			_entry = entry;
		}
	}
}
