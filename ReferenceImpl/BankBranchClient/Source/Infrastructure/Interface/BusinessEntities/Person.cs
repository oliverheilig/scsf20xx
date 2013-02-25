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
namespace GlobalBank.Infrastructure.Interface.BusinessEntities
{
	public class Person
	{
		private string _firstName;
		private string _lastName;
		private string _middleInitial;
		private string _socialSecurityNumber;
		private string _motherMaidenName;
		private Address[] _addresses;
		private PhoneNumber[] _phoneNumbers;
		private EmailAddress[] _emailAddresses;
		private string _customerLevel;

		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		public string LastName
		{
			get { return _lastName; }
			set { _lastName = value; }
		}

		public string MiddleInitial
		{
			get { return _middleInitial; }
			set { _middleInitial = value; }
		}

		public string SocialSecurityNumber
		{
			get { return _socialSecurityNumber; }
			set { _socialSecurityNumber = value; }
		}

		public string MotherMaidenName
		{
			get { return _motherMaidenName; }
			set { _motherMaidenName = value; }
		}

		public Address[] Addresses
		{
			get { return _addresses; }
			set { _addresses = value; }
		}

		public PhoneNumber[] PhoneNumbers
		{
			get { return _phoneNumbers; }
			set { _phoneNumbers = value; }
		}

		public EmailAddress[] EmailAddresses
		{
			get { return _emailAddresses; }
			set { _emailAddresses = value; }
		}

		public string CustomerLevel
		{
			get { return _customerLevel; }
			set { _customerLevel = value; }
		}
	}
}
