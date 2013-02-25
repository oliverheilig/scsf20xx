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
using GlobalBank.Infrastructure.Interface.Services;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;

namespace GlobalBank.Infrastructure.Module.Services
{
	public class EnterpriseLibraryCacheService : ICacheService
	{
		private CacheManager _cache;

		public EnterpriseLibraryCacheService()
		{
			_cache = CacheFactory.GetCacheManager() as CacheManager;
		}

		public int Count
		{
			get { return _cache.Count; }
		}

		public object this[string key]
		{
			get { return _cache[key]; }
		}

		public void Add(string key, object value)
		{
			_cache.Add(key, value);
		}

		public void Add(string key, object value, DateTime absolute)
		{
			ICacheItemExpiration expiration = new AbsoluteTime(absolute);
			_cache.Add(key, value, CacheItemPriority.Normal, null, expiration);
		}

		public void Add(string key, object value, TimeSpan relative)
		{
			ICacheItemExpiration expiration = new SlidingTime(relative);
			_cache.Add(key, value, CacheItemPriority.Normal, null, expiration);
		}

		public bool Contains(string key)
		{
			return _cache.Contains(key);
		}

		public void Dispose()
		{
			_cache.Dispose();
		}

		public void Flush()
		{
			_cache.Flush();
		}

		public object GetData(string key)
		{
			return _cache.GetData(key);
		}

		public void Remove(string key)
		{
			_cache.Remove(key);
		}
	}
}