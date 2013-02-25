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
	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/EmailAddressSearchCriteria")]
	public partial class EmailAddressSearchCriteria
	{

		private string addressField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Address
		{
			get
			{
				return this.addressField;
			}
			set
			{
				this.addressField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/PhoneNumberSearchCriteria")]
	public partial class PhoneNumberSearchCriteria
	{

		private string numberField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Number
		{
			get
			{
				return this.numberField;
			}
			set
			{
				this.numberField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/AddressSearchCriteria")]
	public partial class AddressSearchCriteria
	{

		private string address1Field;

		private string address2Field;

		private string cityField;

		private string stateProvinceField;

		private string countryCodeField;

		private string postalZipCodeField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Address1
		{
			get
			{
				return this.address1Field;
			}
			set
			{
				this.address1Field = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Address2
		{
			get
			{
				return this.address2Field;
			}
			set
			{
				this.address2Field = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string City
		{
			get
			{
				return this.cityField;
			}
			set
			{
				this.cityField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string StateProvince
		{
			get
			{
				return this.stateProvinceField;
			}
			set
			{
				this.stateProvinceField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string CountryCode
		{
			get
			{
				return this.countryCodeField;
			}
			set
			{
				this.countryCodeField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string PostalZipCode
		{
			get
			{
				return this.postalZipCodeField;
			}
			set
			{
				this.postalZipCodeField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/CustomerSearchCriteria")]
	public partial class CustomerSearchCriteria
	{

		private string firstNameField;

		private string lastNameField;

		private string middleInitialField;

		private string sSNumberField;

		private AddressSearchCriteria[] addressesField;

		private PhoneNumberSearchCriteria[] phoneNumbersField;

		private EmailAddressSearchCriteria[] emailAddressesField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string FirstName
		{
			get
			{
				return this.firstNameField;
			}
			set
			{
				this.firstNameField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string LastName
		{
			get
			{
				return this.lastNameField;
			}
			set
			{
				this.lastNameField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string MiddleInitial
		{
			get
			{
				return this.middleInitialField;
			}
			set
			{
				this.middleInitialField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string SSNumber
		{
			get
			{
				return this.sSNumberField;
			}
			set
			{
				this.sSNumberField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/AddressSearchCriteria")]
		public AddressSearchCriteria[] Addresses
		{
			get
			{
				return this.addressesField;
			}
			set
			{
				this.addressesField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/PhoneNumberSearchCriteria")]
		public PhoneNumberSearchCriteria[] PhoneNumbers
		{
			get
			{
				return this.phoneNumbersField;
			}
			set
			{
				this.phoneNumbersField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/EmailAddressSearchCriteria")]
		public EmailAddressSearchCriteria[] EmailAddresses
		{
			get
			{
				return this.emailAddressesField;
			}
			set
			{
				this.emailAddressesField = value;
			}
		}
	}
	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/05/DeleteCustomerResponse")]
	public partial class DeleteCustomerResponse
	{
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/05/DeleteCustomerRequest")]
	public partial class DeleteCustomerRequest
	{

		private string customerIDField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string CustomerID
		{
			get
			{
				return this.customerIDField;
			}
			set
			{
				this.customerIDField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/AccountType")]
	public partial class AccountType
	{
		private int _accountTypeId;
		private string _type;
		private float _fees;
		private int _maxMonthlyTransaction;
		private float _interestRate;

		public float InterestRate
		{
			get { return _interestRate; }
			set { _interestRate = value; }
		}

		public int MaxMonthlyTransaction
		{
			get { return _maxMonthlyTransaction; }
			set { _maxMonthlyTransaction = value; }
		}

		public float Fees
		{
			get { return _fees; }
			set { _fees = value; }
		}

		[XmlElement(IsNullable = true)]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public int AccountTypeId
		{
			get { return _accountTypeId; }
			set { _accountTypeId = value; }
		}
	
	}

		/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Alert")]
	public partial class Alert
	{
		private int _alertId;
		private string _customerId;
		private AlertType _alertTypeReference;
		private DateTime _startDate;
		private DateTime _expirationDate;

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

		[XmlElement(IsNullable = true)]
		public AlertType AlertTypeReference
		{
			get { return _alertTypeReference; }
			set { _alertTypeReference = value; }
		}

		[XmlElement(IsNullable = true)]
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

	
	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/AlertType")]
	public partial class AlertType
	{
		private int _alertTypeId;
		private string _type;
		private string _alertDescription;
		private DateTime _startDate;
		private DateTime _expirationDate;

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

		[XmlElement(IsNullable = true)]
		public string AlertDescription
		{
			get { return _alertDescription; }
			set { _alertDescription = value; }
		}

		[XmlElement(IsNullable = true)]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public int AlertTypeId
		{
			get { return _alertTypeId; }
			set { _alertTypeId = value; }
		}
	
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Walkin")]
	public partial class Walkin
	{

		private string firstNameField;

		private string lastNameField;

		private string middleInitialField;

		private string sSNumberField;

		private string motherMaidenNameField;

		private Address[] addressesField;

		private PhoneNumber[] phoneNumbersField;

		private EmailAddress[] emailAddressesField;

		private Comment[] commentsField;

		private string customerLevelField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string FirstName
		{
			get
			{
				return this.firstNameField;
			}
			set
			{
				this.firstNameField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string LastName
		{
			get
			{
				return this.lastNameField;
			}
			set
			{
				this.lastNameField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string MiddleInitial
		{
			get
			{
				return this.middleInitialField;
			}
			set
			{
				this.middleInitialField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string SSNumber
		{
			get
			{
				return this.sSNumberField;
			}
			set
			{
				this.sSNumberField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string MotherMaidenName
		{
			get
			{
				return this.motherMaidenNameField;
			}
			set
			{
				this.motherMaidenNameField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Address")]
		public Address[] Addresses
		{
			get
			{
				return this.addressesField;
			}
			set
			{
				this.addressesField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/PhoneNumber")]
		public PhoneNumber[] PhoneNumbers
		{
			get
			{
				return this.phoneNumbersField;
			}
			set
			{
				this.phoneNumbersField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/EmailAddress")]
		public EmailAddress[] EmailAddresses
		{
			get
			{
				return this.emailAddressesField;
			}
			set
			{
				this.emailAddressesField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Comment")]
		public Comment[] Comments
		{
			get
			{
				return this.commentsField;
			}
			set
			{
				this.commentsField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string CustomerLevel
		{
			get
			{
				return this.customerLevelField;
			}
			set
			{
				this.customerLevelField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Address")]
	public partial class Address
	{

		private string addressTypeField;

		private string address1Field;

		private string address2Field;

		private string cityField;

		private string stateProvinceField;

		private string countryCodeField;

		private string postalZipCodeField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string AddressType
		{
			get
			{
				return this.addressTypeField;
			}
			set
			{
				this.addressTypeField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Address1
		{
			get
			{
				return this.address1Field;
			}
			set
			{
				this.address1Field = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Address2
		{
			get
			{
				return this.address2Field;
			}
			set
			{
				this.address2Field = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string City
		{
			get
			{
				return this.cityField;
			}
			set
			{
				this.cityField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string StateProvince
		{
			get
			{
				return this.stateProvinceField;
			}
			set
			{
				this.stateProvinceField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string CountryCode
		{
			get
			{
				return this.countryCodeField;
			}
			set
			{
				this.countryCodeField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string PostalZipCode
		{
			get
			{
				return this.postalZipCodeField;
			}
			set
			{
				this.postalZipCodeField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Customer")]
	public partial class Customer
	{

		// Originally not in the contract schema
		private string customerIdField;

		private string firstNameField;

		private string lastNameField;

		private string middleInitialField;

		private string sSNumberField;

		private string motherMaidenNameField;

		private Address[] addressesField;

		private PhoneNumber[] phoneNumbersField;

		private EmailAddress[] emailAddressesField;

		private Comment[] commentsField;

		private string customerLevelField;

		[XmlElement(IsNullable = true)]
		public string CustomerId
		{
			get
			{
				return this.customerIdField;
			}
			set
			{
				this.customerIdField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string FirstName
		{
			get
			{
				return this.firstNameField;
			}
			set
			{
				this.firstNameField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string LastName
		{
			get
			{
				return this.lastNameField;
			}
			set
			{
				this.lastNameField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string MiddleInitial
		{
			get
			{
				return this.middleInitialField;
			}
			set
			{
				this.middleInitialField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string SSNumber
		{
			get
			{
				return this.sSNumberField;
			}
			set
			{
				this.sSNumberField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string MotherMaidenName
		{
			get
			{
				return this.motherMaidenNameField;
			}
			set
			{
				this.motherMaidenNameField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Address")]
		public Address[] Addresses
		{
			get
			{
				return this.addressesField;
			}
			set
			{
				this.addressesField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/PhoneNumber")]
		public PhoneNumber[] PhoneNumbers
		{
			get
			{
				return this.phoneNumbersField;
			}
			set
			{
				this.phoneNumbersField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/EmailAddress")]
		public EmailAddress[] EmailAddresses
		{
			get
			{
				return this.emailAddressesField;
			}
			set
			{
				this.emailAddressesField = value;
			}
		}

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Comment")]
		public Comment[] Comments
		{
			get
			{
				return this.commentsField;
			}
			set
			{
				this.commentsField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string CustomerLevel
		{
			get
			{
				return this.customerLevelField;
			}
			set
			{
				this.customerLevelField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/PhoneNumber")]
	public partial class PhoneNumber
	{

		private string phoneNumber1Field;

		private string phoneTypeField;

		/// <remarks/>
		[XmlElement("PhoneNumber", IsNullable = true)]
		public string PhoneNumber1
		{
			get
			{
				return this.phoneNumber1Field;
			}
			set
			{
				this.phoneNumber1Field = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string PhoneType
		{
			get
			{
				return this.phoneTypeField;
			}
			set
			{
				this.phoneTypeField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/EmailAddress")]
	public partial class EmailAddress
	{

		private string emailAddress1Field;

		private string typeField;

		/// <remarks/>
		[XmlElement("EmailAddress", IsNullable = true)]
		public string EmailAddress1
		{
			get
			{
				return this.emailAddress1Field;
			}
			set
			{
				this.emailAddress1Field = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Type
		{
			get
			{
				return this.typeField;
			}
			set
			{
				this.typeField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Comment")]
	public partial class Comment
	{

		private DateTime dateTimeField;

		private string comment1Field;

		private string authorNameField;

		/// <remarks/>
		public DateTime DateTime
		{
			get
			{
				return this.dateTimeField;
			}
			set
			{
				this.dateTimeField = value;
			}
		}

		/// <remarks/>
		[XmlElement("Comment", IsNullable = true)]
		public string Comment1
		{
			get
			{
				return this.comment1Field;
			}
			set
			{
				this.comment1Field = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string AuthorName
		{
			get
			{
				return this.authorNameField;
			}
			set
			{
				this.authorNameField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/QueueEntryID")]
	public partial class QueueEntryID
	{

		private string idField;

		/// <remarks/>
		public string ID
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/QueueEntry")]
	public partial class QueueEntry
	{

		private QueueEntryID queueEntryIDField;

		private string visitorNameField;

		private Customer customerReferenceField;

		private Walkin walkInReferenceField;

		private DateTime timeInField;

		private string statusField;

		private string reasonCodeField;

		private string descriptionField;

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public QueueEntryID QueueEntryID
		{
			get
			{
				return this.queueEntryIDField;
			}
			set
			{
				this.queueEntryIDField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string VisitorName
		{
			get
			{
				return this.visitorNameField;
			}
			set
			{
				this.visitorNameField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public Customer CustomerReference
		{
			get
			{
				return this.customerReferenceField;
			}
			set
			{
				this.customerReferenceField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public Walkin WalkinReference
		{
			get
			{
				return this.walkInReferenceField;
			}
			set
			{
				this.walkInReferenceField = value;
			}
		}

		/// <remarks/>
		public DateTime TimeIn
		{
			get
			{
				return this.timeInField;
			}
			set
			{
				this.timeInField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string ReasonCode
		{
			get
			{
				return this.reasonCodeField;
			}
			set
			{
				this.reasonCodeField = value;
			}
		}

		/// <remarks/>
		[XmlElement(IsNullable = true)]
		public string Description
		{
			get
			{
				return this.descriptionField;
			}
			set
			{
				this.descriptionField = value;
			}
		}
	}

	/// <remarks/>
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[Serializable()]
	[DebuggerStepThrough()]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/FindCustomerResponse")]
	public partial class FindCustomerResponse
	{

		private Customer[] customersField;

		private bool tooManyResultsField;

		/// <remarks/>
		[XmlArray(IsNullable = true)]
		[XmlArrayItem(Namespace = "http://GlobalBranchServicesDataContracts/2006/04/Customer")]
		public Customer[] Customers
		{
			get
			{
				return this.customersField;
			}
			set
			{
				this.customersField = value;
			}
		}

		/// <remarks/>
		public bool TooManyResults
		{
			get
			{
				return this.tooManyResultsField;
			}
			set
			{
				this.tooManyResultsField = value;
			}
		}
	}
}