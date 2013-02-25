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
	public class QueueEntry
	{
		private string _queueEntryID;
		private string _visitorName;
		private Person _person;
		private DateTime _timeIn;
		private string _status;
		private string _reasonCode;
		private string _description;

		public string QueueEntryID
		{
			get { return _queueEntryID; }
			set { _queueEntryID = value; }
		}

		public string VisitorName
		{
			get { return _visitorName; }
			set { _visitorName = value; }
		}

		public Person Person
		{
			get { return _person; }
			set { _person = value; }
		}

		public DateTime TimeIn
		{
			get { return _timeIn; }
			set { _timeIn = value; }
		}

		public string Status
		{
			get { return _status; }
			set { _status = value; }
		}

		public string ReasonCode
		{
			get { return _reasonCode; }
			set { _reasonCode = value; }
		}

		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}
	}
}
