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
using System.Collections.Generic;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.Infrastructure.Interface.BusinessEntities;

namespace GlobalBank.BranchSystems.Module.Tests.Mocks
{
	internal class MockCustomerQueueService : ICustomerQueueService
	{
		public List<QueueEntry> Queue = new List<QueueEntry>();
		public bool GetEntriesCalled = false;

		public QueueEntry[] GetEntries()
		{
			GetEntriesCalled = true;
			return Queue.ToArray();
		}

		public QueueEntry RemoveFromQueue(QueueEntry entry)
		{
			Queue.Remove(entry);
			return entry;
		}

		public void AddCustomer(Customer customer, string reason, string description)
		{
			QueueEntry entry = new QueueEntry();
			entry.QueueEntryID = Queue.Count.ToString();
			entry.Person = customer;
			entry.ReasonCode = reason;
			entry.Description = description;
			Queue.Add(entry);
		}

		public void AddWalkin(WalkIn walkIn, string reason, string description)
		{
			QueueEntry entry = new QueueEntry();
			entry.QueueEntryID = Queue.Count.ToString();
			entry.Person = walkIn;
			entry.ReasonCode = reason;
			entry.Description = description;
			Queue.Add(entry);
		}

		public void AssignToServicing(QueueEntry entry, string assignedTo)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public QueueEntry[] GetMyEntries(string name)
		{
            if ( name == "Tom" ) {
                QueueEntry entry = new QueueEntry();
                entry.Description = "Tom";
                Queue.Add(entry);
                return new QueueEntry[] { entry };
            }
            return null;
		}
	}
}
