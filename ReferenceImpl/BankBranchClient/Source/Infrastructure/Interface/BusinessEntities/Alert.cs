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

namespace GlobalBank.Infrastructure.Interface.BusinessEntities
{
	public class Alert
	{
		private DateTime _expirationDate;
		private DateTime _startDate;
		private string _alertType;
		private string _customerId;
		private int _alertId;

		public DateTime ExpirationDate
		{
			get { return _expirationDate; }
			set { _expirationDate = value; }
		}

		public DateTime StartDate
		{
			get { return _startDate; }
			set { _startDate = value; }
		}

		public string AlertType
		{
			get { return _alertType; }
			set { _alertType = value; }
		}

		public string CustomerId
		{
			get { return _customerId; }
			set { _customerId = value; }
		}

		public int AlertId
		{
			get { return _alertId; }
			set { _alertId = value; }
		}
	}
}
