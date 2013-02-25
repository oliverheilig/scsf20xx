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
	public class Address
	{
		private AddressType _addressType;
		private string _address1;
		private string _address2;
		private string _city;
		private string _stateProvince;
		private string _countryCode;
		private string _postalZipCode;

		public AddressType AddressType
		{
			get { return _addressType; }
			set { _addressType = value; }
		}

		public string Address1
		{
			get { return _address1; }
			set { _address1 = value; }
		}

		public string Address2
		{
			get { return _address2; }
			set { _address2 = value; }
		}

		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		public string StateProvince
		{
			get { return _stateProvince; }
			set { _stateProvince = value; }
		}

		public string CountryCode
		{
			get { return _countryCode; }
			set { _countryCode = value; }
		}

		public string PostalZipCode
		{
			get { return _postalZipCode; }
			set { _postalZipCode = value; }
		}
	}
}
