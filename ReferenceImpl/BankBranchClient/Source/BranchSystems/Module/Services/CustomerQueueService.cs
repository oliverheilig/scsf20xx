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
using System.Web.Services.Protocols;
using GlobalBank.BranchSystems.Interface.Services;
using GlobalBank.BranchSystems.Properties;
using GlobalBank.BranchSystems.ServiceProxies;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;
using Customer=GlobalBank.Infrastructure.Interface.BusinessEntities.Customer;
using QueueEntry=GlobalBank.Infrastructure.Interface.BusinessEntities.QueueEntry;
using Walkin = GlobalBank.Infrastructure.Interface.BusinessEntities.WalkIn;

namespace GlobalBank.BranchSystems.Module.Services
{
	[Service(typeof (ICustomerQueueService))]
	public class CustomerQueueService : ICustomerQueueService
	{
		private ICustomerQueueServiceProxy _customerQueueServiceProxy;
		private IEntityTranslatorService _translator;

		public CustomerQueueService()
			: this(new ServiceProxies.CustomerQueueService(Settings.Default.CustomerQueueWebServiceUrl))
		{
		}

		public CustomerQueueService(ICustomerQueueServiceProxy proxy)
		{
			_customerQueueServiceProxy = proxy;
		}

		[ServiceDependency]
		public IEntityTranslatorService Translator
		{
			get { return _translator; }
			set { _translator = value; }
		}

		public void AddCustomer(Customer customer, string reason, string description)
		{
			AddToQueue(customer, reason, description);
		}

		public void AddWalkin(Walkin walkIn, string reason, string description)
		{
			AddToQueue(walkIn, reason, description);
		}

		private void AddToQueue(Person person, string reason, string description)
		{
			try
			{
				ServiceProxies.QueueEntry entry = new ServiceProxies.QueueEntry();
				if (person is Customer)
					entry.CustomerReference = Translator.Translate<ServiceProxies.Customer>(person);
				else if (person is Walkin)
					entry.WalkinReference = Translator.Translate<ServiceProxies.Walkin>(person);
				entry.ReasonCode = reason;
				entry.Description = description;
				_customerQueueServiceProxy.AddToQueue(entry);
			}
			catch (SoapException ex)
			{
				if (ex.Detail.FirstChild.LocalName == "CustomerQueuedException")
					throw new DuplicateCustomerException();

				throw;
			}
		}

		public QueueEntry[] GetEntries()
		{
			return Translator.Translate<QueueEntry[]>(_customerQueueServiceProxy.GetEntries(null));
		}

		public QueueEntry[] GetMyEntries(string name)
		{
			GetEntriesRequest request = new GetEntriesRequest();
			request.AssignedTo = name;
			return Translator.Translate<QueueEntry[]>(_customerQueueServiceProxy.GetEntries(request));
		}

		public QueueEntry RemoveFromQueue(QueueEntry entry)
		{
			return
				Translator.Translate<QueueEntry>(
					_customerQueueServiceProxy.RemoveFromQueue(Translator.Translate<ServiceProxies.QueueEntry>(entry)));
		}

		public void AssignToServicing(QueueEntry entry, string assignedTo)
		{
			AssignToServicingRequest request = new AssignToServicingRequest();
			request.QueueEntryID = new QueueEntryID();
			request.QueueEntryID.ID = entry.QueueEntryID;
			request.AssignTo = assignedTo;
			_customerQueueServiceProxy.AssignToServicing(request);
		}
	}
}