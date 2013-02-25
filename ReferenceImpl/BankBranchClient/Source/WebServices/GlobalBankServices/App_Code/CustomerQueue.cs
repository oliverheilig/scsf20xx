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
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace GlobalBankServices
{
	public class CustomerQueueService : IBasicHttpBinding_ICustomerQueue
	{
		public void AddToQueue(QueueEntry queueEntry)
		{
			try
			{
				if (queueEntry.CustomerReference != null)
					new GlobalBankDataServices().QueueCustomer(queueEntry.CustomerReference, queueEntry.ReasonCode, queueEntry.Description);
				else if (queueEntry.WalkinReference != null)
					new GlobalBankDataServices().QueueWalkin(queueEntry.WalkinReference, queueEntry.ReasonCode, queueEntry.Description);
			}
			catch (Exception ex)
			{
				throw SoapExceptionFactory.CreateSoapClientException(ex);
			}
		}

		public QueueEntry[] GetEntries(GetEntriesRequest request)
		{
			if (request == null || String.IsNullOrEmpty(request.AssignedTo))
			{
				return new GlobalBankDataServices().GetQueue();
			}
			else
			{
				return new GlobalBankDataServices().GetMyEntries(request.AssignedTo);
			}
		}

		public QueueEntry RemoveFromQueue(QueueEntry queueEntry)
		{
			return new GlobalBankDataServices().RemoveQueueEntry(queueEntry.QueueEntryID.ID);
		}

		public void AssignToServicing(AssignToServicingRequest request)
		{
			GlobalBankDataServices db = new GlobalBankDataServices();
			db.AssignToServicing(int.Parse(request.QueueEntryID.ID), request.AssignTo);
		}
	}

	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/GetEntriesRequest")]
	public class GetEntriesRequest
	{
		private string _assignedTo;

		[XmlElement(IsNullable = true)]
		public string AssignedTo
		{
			get { return _assignedTo; }
			set { _assignedTo = value; }
		}
	
	}


	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/AssignToServicingRequest")]
	public class AssignToServicingRequest
	{
		private QueueEntryID _queueEntryID;
		private string _assignTo;

		public string AssignTo
		{
			get { return _assignTo; }
			set { _assignTo = value; }
		}
	
		public QueueEntryID QueueEntryID
		{
			get { return _queueEntryID; }
			set { _queueEntryID = value; }
		}
	
	}
}
