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
using System.Threading;
using Microsoft.Practices.SmartClient.DisconnectedAgent;

namespace Microsoft.Practices.SmartClient.ApplicationBlocks.DisconnectedAgent
{
	public class MockRequestQueue : IRequestQueue
	{
		public List<Request> requests = new List<Request>();
		public event EventHandler<RequestEnqueuedEventArgs> RequestEnqueued;
		public event EventHandler RequestReadedFromIteratorByPrice;

		public int DequeueCount = 0;
		public int DequeueCriteriaCount = 0;
		public int CriteriaReceivedPrice;
		public string CriteriaReceivedTag;
		public int ThreadId = 0;

		public int GetCount()
		{
			return requests.Count;
		}

		public Request GetNextRequest()
		{
			ThreadId = Thread.CurrentThread.ManagedThreadId;

			DequeueCount++;
			if (requests.Count > 0)
			{
				Request request = requests[0];
				//requests.RemoveAt(0);
				return request;
			}
			return null;
		}

		public IEnumerable<Request> GetRequests(int stampsEqualOrMoreThan)
		{
			ThreadId = Thread.CurrentThread.ManagedThreadId;

			CriteriaReceivedPrice = stampsEqualOrMoreThan;

			List<Request> tempList = new List<Request>(requests);

			foreach (Request r in tempList)
			{
				if (r.Behavior.Stamps >= stampsEqualOrMoreThan)
				{
					if (RequestReadedFromIteratorByPrice != null)
						RequestReadedFromIteratorByPrice(this, EventArgs.Empty);
					yield return r;
				}
			}
		}

		public void Enqueue(Request request)
		{
			//should validate request has endpoint

			requests.Add(request);
			if (RequestEnqueued != null)
				RequestEnqueued(this, new RequestEnqueuedEventArgs(request));
		}

		public void Remove(Request request)
		{
			requests.Remove(request);
		}

		public IEnumerable<Request> GetRequests(string tag)
		{
			ThreadId = Thread.CurrentThread.ManagedThreadId;

			List<Request> tempList = new List<Request>(requests);

			foreach (Request r in tempList)
			{
				if (r.Behavior.Tag == tag)
					yield return r;
			}
		}

		public IEnumerable<Request> GetRequests()
		{
			ThreadId = Thread.CurrentThread.ManagedThreadId;

			List<Request> tempList = new List<Request>(requests);

			foreach (Request r in tempList)
				yield return r;
		}

		public Request GetRequest(Guid requestId)
		{
			ThreadId = Thread.CurrentThread.ManagedThreadId;

			List<Request> tempList = new List<Request>(requests);

			foreach (Request r in tempList)
			{
				if (r.RequestId == requestId)
					return r;
			}
			return null;
		}
	}
}