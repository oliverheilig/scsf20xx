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

namespace Microsoft.Practices.SmartClientFactory.DSACompatibleTypes
{
	public class OfflineBehavior
	{
		public DateTime? Expiration;
		public int Stamps;
		public int MaxRetries;
		public string Tag;

		public TimeSpan? ExpirationAsTimeSpan
		{
			get
			{
				if (Expiration.HasValue)
				{
					return Expiration.Value.Subtract(DateTime.Now);
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					DateTime dt = DateTime.Now;
					dt.Add(value.Value);
					Expiration = dt;
				}
				else
				{
					Expiration = null;
				}
			}
		}
		
	}
}
