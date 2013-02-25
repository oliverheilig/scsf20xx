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

namespace GlobalBank.BranchSystems.Module.Tests.Mocks
{
	class MockQueueService : ICustomerQueueService
	{
		public string RemovedQueueEntryID = null;

		public void AddWalkin(WalkIn walkIn, string reason, string description)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public void AddCustomer(Customer customer, string reason, string description)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public QueueEntry[] GetEntries()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public QueueEntry[] GetMyEntries(string name)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public QueueEntry RemoveFromQueue(QueueEntry entry)
		{
			RemovedQueueEntryID = entry.QueueEntryID;
			return null;
		}

		public void AssignToServicing(QueueEntry id, string assignedTo)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}
