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
using GlobalBank.BranchSystems.Module.Constants;
using GlobalBank.BranchSystems.Properties;
using GlobalBank.BranchSystems.ServiceProxies;
using GlobalBank.Infrastructure.Interface.BusinessEntities;
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.CompositeUI;

namespace GlobalBank.BranchSystems.Module.Services
{
	[Service(typeof(IQuoteService))]
	public class QuoteService : IQuoteService, IDisposable
	{
		private IQuoteServiceProxy _proxy;
		private IEntityTranslatorService _translator;
		private ICacheService _cache;

		public QuoteService()
			: this(new ServiceProxies.QuoteService(Settings.Default.QuotesWebServiceUrl))
		{
		}

		public QuoteService(IQuoteServiceProxy proxy)
		{
			_proxy = proxy;
		}

		[ServiceDependency]
		public IEntityTranslatorService Translator
		{
			get { return _translator; }
			set { _translator = value; }
		}

		[ServiceDependency]
		public ICacheService Cache
		{
			get { return _cache; }
			set { _cache = value; }
		}

		public Rate[] GetRates()
		{
			Rate[] rates = (Rate[])Cache.GetData(CacheEntryNames.RatesSheet);

			if (rates == null)
			{
				RateTableEntryType[] rateTable = _proxy.GetRates();
				rates = Translator.Translate<Rate[]>(rateTable);

				if (rates != null)
				{
					// TODO: the expiration should be based on the requirement (i.e. refresh the rate sheet every day at 9:00am).                      
					Cache.Add(CacheEntryNames.RatesSheet, rates, TimeSpan.FromMinutes(10.0));
				}
			}
			return rates;
		}

		public Quote GetRate(decimal amount, int duration)
		{
			QuoteRequestType request = new QuoteRequestType();
			request.durationInMonths = duration;
			request.amount = amount;
			QuoteResponseType response = _proxy.GetRate(request);

			return Translator.Translate<Quote>(response);
		}

		~QuoteService()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_proxy is IDisposable)
					((IDisposable)_proxy).Dispose();
				if (_translator is IDisposable)
					((IDisposable)_translator).Dispose();
				if (_cache is IDisposable)
					((IDisposable)_cache).Dispose();
			}
		}
	}
}