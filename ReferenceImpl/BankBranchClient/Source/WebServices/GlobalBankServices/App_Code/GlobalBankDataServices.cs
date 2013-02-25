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
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CustomerAccountDataSetTableAdapters;
using CustomerAlertsDataSetTableAdapters;
using CustomerCreditCardsDataSetTableAdapters;
using GlobalBankServices;
using VisitorQueueTableAdapters;
using WalkinDatasetTableAdapters;

/// <summary>
/// Summary description for GlobalBankDataServices
/// </summary>
internal class GlobalBankDataServices
{
	private const string _defaultSSN = "333-333-333";
	private const string _defaultCustomerLevel = "Default";
	private const string _defaultStatus = "Default";
	private const string _defaulReasonCode = "Default";
	private const string _notAvailable = "N/A";
	private const int CertificateOfDepositAccountType = 3;

	private SqlConnection CreateConnection()
	{
		return new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalBankConnection"].ConnectionString);
	}

	public DataSet FindCustomer(CustomerSearchCriteria criteria)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();
			SqlDataAdapter adapter = new SqlDataAdapter(CreateFindCustomerCommand(criteria, conn));
			DataSet data = new DataSet();
			adapter.Fill(data);
			return data;
		}
	}

	public void QueueCustomer(Customer customer, string reason, string description)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();
			if (IsCustomerInQueue(customer.CustomerId, conn))
			{
				throw new CustomerQueuedException();
			}
			CreateQueueEntry(String.Format("{0} {1}{2}{3}", customer.FirstName, customer.MiddleInitial,
				(customer.MiddleInitial == null || customer.MiddleInitial == String.Empty) ? String.Empty : " ",
				customer.LastName), customer.CustomerId, null, DateTime.Now, _defaultStatus,
				reason, description, conn, null);
		}
	}

	public void QueueWalkin(Walkin walkIn, string reason, string description)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();
			using (SqlTransaction transaction = conn.BeginTransaction())
			{

				string walkInId = CreateWalkin(walkIn, conn, transaction);

				CreateQueueEntry(
					String.Format("{0} {1} {2}", walkIn.FirstName, walkIn.MiddleInitial, walkIn.LastName),
					null, walkInId, DateTime.Now,
					_defaultStatus, reason, description,
					conn, transaction);

				transaction.Commit();
			}
		}
	}

	public QueueEntry[] GetQueue()
	{
		return GetMyEntries(null);
	}

	private string CreateWalkin(Walkin walkIn, SqlConnection conn, SqlTransaction transaction)
	{
		string walkInId = Guid.NewGuid().ToString();
		SqlCommand cmdCreateWalkin = new SqlCommand("sp_CreateWalkin", conn, transaction);
		cmdCreateWalkin.CommandType = CommandType.StoredProcedure;
		cmdCreateWalkin.Parameters.AddWithValue("@WalkinId", walkInId);
		cmdCreateWalkin.Parameters.AddWithValue("@FirstName", walkIn.FirstName);
		cmdCreateWalkin.Parameters.AddWithValue("@LastName", walkIn.LastName);
		cmdCreateWalkin.Parameters.AddWithValue("@MiddleInitial", walkIn.MiddleInitial ?? _notAvailable);
		cmdCreateWalkin.Parameters.AddWithValue("@SSNumber", walkIn.SSNumber ?? _defaultSSN);
		cmdCreateWalkin.Parameters.AddWithValue("@MotherMaidenName", walkIn.MotherMaidenName ?? _notAvailable);
		cmdCreateWalkin.Parameters.AddWithValue("@CustomerLevel", walkIn.CustomerLevel ?? _defaultCustomerLevel);
		cmdCreateWalkin.ExecuteNonQuery();

		if (walkIn.Addresses != null && walkIn.Addresses.Length > 0)
		{
			SqlCommand cmdCreateAddress = GetCreateWalkinAddressCommand(conn, transaction);

			foreach (Address address in walkIn.Addresses)
			{
				cmdCreateAddress.Parameters["@WalkinId"].Value = walkInId;
				cmdCreateAddress.Parameters["@Type"].Value = address.AddressType;
				cmdCreateAddress.Parameters["@Address1"].Value = address.Address1 ?? _notAvailable;
				if (!String.IsNullOrEmpty(address.Address2))
				{
					cmdCreateAddress.Parameters["@Address2"].Value = address.Address2;
				}
				cmdCreateAddress.Parameters["@StateProvince"].Value = address.StateProvince ?? _notAvailable;
				cmdCreateAddress.Parameters["@City"].Value = address.City ?? _notAvailable;
				cmdCreateAddress.Parameters["@Country"].Value = address.CountryCode ?? _notAvailable;
				cmdCreateAddress.Parameters["@PostalZipCode"].Value = address.PostalZipCode ?? _notAvailable;
				cmdCreateAddress.ExecuteNonQuery();
			}
		}

		if (walkIn.EmailAddresses != null && walkIn.EmailAddresses.Length > 0)
		{
			SqlCommand cmdCreateEmailAddress = GetCreateWalkinEmailAddressCommand(conn, transaction);

			foreach (EmailAddress email in walkIn.EmailAddresses)
			{
				cmdCreateEmailAddress.Parameters["@WalkinId"].Value = walkInId;
				cmdCreateEmailAddress.Parameters["@Type"].Value = email.Type ?? "Personal";
				cmdCreateEmailAddress.Parameters["@EmailAddress"].Value = email.EmailAddress1 ?? _notAvailable;
				cmdCreateEmailAddress.ExecuteNonQuery();
			}
		}

		if (walkIn.PhoneNumbers != null && walkIn.PhoneNumbers.Length > 0)
		{
			SqlCommand cmdCreatePhoneNumber = GetCreateWalkinPhoneNumberCommand(conn, transaction);
			foreach (PhoneNumber phone in walkIn.PhoneNumbers)
			{
				cmdCreatePhoneNumber.Parameters["@WalkinId"].Value = walkInId;
				cmdCreatePhoneNumber.Parameters["@Type"].Value = phone.PhoneType;
				cmdCreatePhoneNumber.Parameters["@PhoneNumber"].Value = phone.PhoneNumber1 ?? _notAvailable;
				cmdCreatePhoneNumber.ExecuteNonQuery();
			}
		}

		return walkInId;
	}

	private static SqlCommand GetCreateWalkinPhoneNumberCommand(SqlConnection conn, SqlTransaction transaction)
	{
		SqlCommand cmdCreatePhoneNumber = new SqlCommand("sp_CreateWalkinPhoneNumber", conn, transaction);
		cmdCreatePhoneNumber.CommandType = CommandType.StoredProcedure;
		cmdCreatePhoneNumber.Parameters.Add("@WalkinId", SqlDbType.NVarChar, 32);
		cmdCreatePhoneNumber.Parameters.Add("@Type", SqlDbType.NVarChar, 16);
		cmdCreatePhoneNumber.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 24);
		return cmdCreatePhoneNumber;
	}

	private static SqlCommand GetCreateWalkinEmailAddressCommand(SqlConnection conn, SqlTransaction transaction)
	{
		SqlCommand cmdCreateEmailAddress = new SqlCommand("sp_CreateWalkinEmailAddress", conn, transaction);
		cmdCreateEmailAddress.CommandType = CommandType.StoredProcedure;
		cmdCreateEmailAddress.Parameters.Add("@WalkinId", SqlDbType.NVarChar, 32);
		cmdCreateEmailAddress.Parameters.Add("@Type", SqlDbType.NVarChar, 16);
		cmdCreateEmailAddress.Parameters.Add("@EmailAddress", SqlDbType.NVarChar, 128);
		return cmdCreateEmailAddress;
	}

	private static SqlCommand GetCreateWalkinAddressCommand(SqlConnection conn, SqlTransaction transaction)
	{
		SqlCommand cmdCreateAddress = new SqlCommand("sp_CreateWalkinAddress", conn, transaction);
		cmdCreateAddress.CommandType = CommandType.StoredProcedure;

		cmdCreateAddress.Parameters.Add("@WalkinId", SqlDbType.NVarChar, 32);
		cmdCreateAddress.Parameters.Add("@Type", SqlDbType.NVarChar, 16);
		cmdCreateAddress.Parameters.Add("@Address1", SqlDbType.NVarChar, 128);
		cmdCreateAddress.Parameters.Add("@Address2", SqlDbType.NVarChar, 128);
		cmdCreateAddress.Parameters.Add("@StateProvince", SqlDbType.NVarChar, 64);
		cmdCreateAddress.Parameters.Add("@City", SqlDbType.NVarChar, 64);
		cmdCreateAddress.Parameters.Add("@Country", SqlDbType.NVarChar, 64);
		cmdCreateAddress.Parameters.Add("@PostalZipCode", SqlDbType.NVarChar, 16);
		return cmdCreateAddress;
	}

	private void CreateQueueEntry(string visitorName, string customerId, string walkInId, DateTime timeIn, string status, string reason, string description, SqlConnection conn, SqlTransaction transaction)
	{
		SqlCommand cmdQueueWaklin = new SqlCommand("sp_CreateQueueEntry", conn, transaction);
		cmdQueueWaklin.CommandType = CommandType.StoredProcedure;
		cmdQueueWaklin.Parameters.AddWithValue("@VisitorName", visitorName);
		cmdQueueWaklin.Parameters.AddWithValue("@CustomerId", customerId);
		cmdQueueWaklin.Parameters.AddWithValue("@WalkinId", walkInId);
		cmdQueueWaklin.Parameters.AddWithValue("@TimeIn", timeIn);
		cmdQueueWaklin.Parameters.AddWithValue("@Status", status ?? _defaultStatus);
		cmdQueueWaklin.Parameters.AddWithValue("@ReasonCode", reason ?? _defaulReasonCode);
		cmdQueueWaklin.Parameters.AddWithValue("@Description", description);
		cmdQueueWaklin.ExecuteNonQuery();
	}

	private SqlCommand CreateFindCustomerCommand(CustomerSearchCriteria criteria, SqlConnection conn)
	{

		SqlCommand cmd = new SqlCommand("sp_FindCustomer", conn);
		cmd.CommandType = CommandType.StoredProcedure;

		cmd.Parameters.AddWithValue("@LastName", GetNullOrValue(criteria.LastName));
		cmd.Parameters.AddWithValue("@FirstName", GetNullOrValue(criteria.FirstName));
		cmd.Parameters.AddWithValue("@MiddleInitial", GetNullOrValue(criteria.MiddleInitial));
		cmd.Parameters.AddWithValue("@SSNumber", GetNullOrValue(criteria.SSNumber));

		if (criteria.Addresses != null && criteria.Addresses.Length > 0)
		{
			cmd.Parameters.AddWithValue("@Address1", GetNullOrValue(criteria.Addresses[0].Address1));
			cmd.Parameters.AddWithValue("@Address2", GetNullOrValue(criteria.Addresses[0].Address2));
			cmd.Parameters.AddWithValue("@City", GetNullOrValue(criteria.Addresses[0].City));
			cmd.Parameters.AddWithValue("@StateProvince", GetNullOrValue(criteria.Addresses[0].StateProvince));
			cmd.Parameters.AddWithValue("@PostalZipCode", GetNullOrValue(criteria.Addresses[0].PostalZipCode));
		}
		if (criteria.PhoneNumbers != null && criteria.PhoneNumbers.Length > 0)
		{
			for (int i = 0; i < 3 && i < criteria.PhoneNumbers.Length; i++)
			{
				cmd.Parameters.AddWithValue("@PhoneNumber" + (i + 1).ToString(), GetNullOrValue(criteria.PhoneNumbers[i].Number));
			}
		}
		if (criteria.EmailAddresses != null && criteria.EmailAddresses.Length > 0)
		{
			cmd.Parameters.AddWithValue("@EmailAddress", GetNullOrValue(criteria.EmailAddresses[0].Address));
		}
		return cmd;
	}


	private object GetNullOrValue(string value)
	{
		return value != null ? value.Length > 0 ? value : null : value;
	}

	internal DataSet LookupCustomer(string customerId)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();
			return LookupCustomer(customerId, conn);
		}
	}

	private DataSet LookupCustomer(string customerId, SqlConnection conn)
	{
		SqlCommand cmdLookupCustomer = new SqlCommand("sp_LookupCustomer", conn);
		cmdLookupCustomer.CommandType = CommandType.StoredProcedure;
		cmdLookupCustomer.Parameters.AddWithValue("@CustomerId", customerId);
		SqlDataAdapter adapter = new SqlDataAdapter(cmdLookupCustomer);
		DataSet data = new DataSet();
		adapter.Fill(data);
		return data;
	}

	private bool IsCustomerInQueue(string customerId, SqlConnection conn)
	{
		SqlCommand cmdLookupCustomerInQueue = new SqlCommand("sp_LookupCustomerInQueue", conn);
		cmdLookupCustomerInQueue.CommandType = CommandType.StoredProcedure;
		cmdLookupCustomerInQueue.Parameters.AddWithValue("@CustomerId", customerId);

		using (SqlDataReader reader = cmdLookupCustomerInQueue.ExecuteReader())
		{
			return reader.HasRows;
		}
	}


	internal FindCustomerResponse CreateFindCustomerResponse(DataSet data)
	{
		FindCustomerResponse response = new FindCustomerResponse();
		response.Customers = CreateCustomersFromDataSet(data);
		return response;
	}


	internal Customer[] CreateCustomersFromDataSet(DataSet data)
	{
		Customer[] result = new Customer[data.Tables[0].Rows.Count];
		for (int customerIndex = 0; customerIndex < result.Length; customerIndex++)
		{
			Customer customer = new Customer();
			DataRow customerRow = data.Tables[0].Rows[customerIndex];
			customer.CustomerId = customerRow["CustomerId"].ToString();
			customer.FirstName = customerRow["FirstName"].ToString();
			customer.MiddleInitial = customerRow["MiddleInitial"].ToString();
			customer.CustomerLevel = customerRow["CustomerLevel"].ToString();
			customer.LastName = customerRow["LastName"].ToString();
			customer.MotherMaidenName = customerRow["MotherMaidenName"].ToString();
			customer.SSNumber = customerRow["SSNumber"].ToString();

			string filter = String.Format("CustomerId = '{0}'", customerRow["CustomerId"].ToString());
			DataRow[] addresses = data.Tables[1].Select(filter);
			if (addresses.Length > 0)
			{
				customer.Addresses = new Address[addresses.Length];
				for (int addressIndex = 0; addressIndex < addresses.Length; addressIndex++)
				{
					Address address = new Address();
					DataRow addressRow = addresses[addressIndex];
					address.Address1 = addressRow["Address1"].ToString();
					address.Address2 = addressRow["Address2"].ToString();
					address.AddressType = addressRow["Type"].ToString();
					address.City = addressRow["City"].ToString();
					address.CountryCode = addressRow["Country"].ToString().Trim();
					address.PostalZipCode = addressRow["PostalZipCode"].ToString();
					address.StateProvince = addressRow["StateProvince"].ToString();
					customer.Addresses[addressIndex] = address;
				}
			}

			DataRow[] emails = data.Tables[2].Select(filter);
			if (emails.Length > 0)
			{
				customer.EmailAddresses = new EmailAddress[emails.Length];
				for (int emailIndex = 0; emailIndex < emails.Length; emailIndex++)
				{
					DataRow emailRow = emails[emailIndex];
					EmailAddress email = new EmailAddress();
					email.EmailAddress1 = emailRow["EmailAddress"].ToString();
					email.Type = emailRow["Type"].ToString();
					customer.EmailAddresses[emailIndex] = email;
				}
			}

			DataRow[] phones = data.Tables[3].Select(filter);
			if (phones.Length > 0)
			{
				customer.PhoneNumbers = new PhoneNumber[phones.Length];
				for (int phoneIndex = 0; phoneIndex < phones.Length; phoneIndex++)
				{
					DataRow phoneRow = phones[phoneIndex];
					PhoneNumber phone = new PhoneNumber();
					phone.PhoneNumber1 = phoneRow["PhoneNumber"].ToString();
					phone.PhoneType = phoneRow["Type"].ToString();
					customer.PhoneNumbers[phoneIndex] = phone;
				}
			}

			result[customerIndex] = customer;
		}
		return result;
	}

	internal Alert[] GetCustomerAlerts(string customerId)
	{
		CustomerAlertsDataSet dsAlerts = new CustomerAlertsDataSet();
		AlertTableAdapter ad1 = new AlertTableAdapter();
		AlertTypeTableAdapter ad2 = new AlertTypeTableAdapter();
		ad1.Fill(dsAlerts.Alert, customerId);
		ad2.Fill(dsAlerts.AlertType);

		List<Alert> alerts = new List<Alert>();
		foreach (CustomerAlertsDataSet.AlertRow rowAlert in dsAlerts.Alert)
		{
			Alert alert = new Alert();
			alert.AlertId = rowAlert.AlertId;
			alert.CustomerId = rowAlert.CustomerId;
			alert.ExpirationDate = rowAlert.IsExpirationDateNull() ? DateTime.MaxValue : rowAlert.ExpirationDate;
			alert.StartDate = rowAlert.StartDate;

			CustomerAlertsDataSet.AlertTypeRow rowAlertType = rowAlert.AlertTypeRow;
			alert.AlertTypeReference = new AlertType();
			alert.AlertTypeReference.AlertDescription = rowAlertType.Description;
			alert.AlertTypeReference.AlertTypeId = rowAlertType.AlertTypeId;
			alert.AlertTypeReference.ExpirationDate = rowAlertType.IsExpirationDateNull() ? DateTime.MaxValue : rowAlertType.ExpirationDate;
			alert.AlertTypeReference.StartDate = rowAlertType.StartDate;
			alert.AlertTypeReference.Type = rowAlertType.AlertType;

			alerts.Add(alert);
		}
		return alerts.ToArray();
	}

	internal accountType[] GetCustomerAccounts(string customerId)
	{
		CustomerAccountDataSet dsAccounts = new CustomerAccountDataSet();
		AccountTableAdapter ad1 = new AccountTableAdapter();
		AccountTypeTableAdapter ad2 = new AccountTypeTableAdapter();
		ad1.Fill(dsAccounts.Account, customerId);
		ad2.Fill(dsAccounts.AccountType);

		List<accountType> result = new List<accountType>();
		foreach (CustomerAccountDataSet.AccountRow rowAccount in dsAccounts.Account)
		{
			accountType account = new accountType();

			account.accountNumber = rowAccount.AccountNumber;
			account.balance = rowAccount.Balance;
			account.customerId = rowAccount.CustomerId;
			account.dateOpened = rowAccount.DateOpened;

			account.lastTransactionAt = rowAccount.IsLastTransactionDateNull() ? DateTime.MinValue : rowAccount.LastTransactionDate;

			CustomerAccountDataSet.AccountTypeRow rowAccountType = rowAccount.AccountTypeRow;
			account.accountType1 = new accountTypeType();
			account.accountType1.id = rowAccountType.AccountTypeId;
			account.accountType1.fees = rowAccountType.IsFeesNull() ? 0 : (float)rowAccountType.Fees;
			account.accountType1.interestRate = rowAccountType.IsInterestRateNull() ? 0 : (float)rowAccountType.InterestRate;
			account.accountType1.maxMonthlyTransaction = rowAccountType.IsMaxMonthlyTransactionNull() ? 0 : rowAccountType.MaxMonthlyTransaction;
			account.accountType1.type = rowAccountType.AccountType;

			result.Add(account);
		}

		return result.ToArray();
	}

	internal creditCardType[] GetCustomerCreditCards(string customerId)
	{
		CustomerCreditCardsDataSet dsCreditCard = new CustomerCreditCardsDataSet();
		CreditCardTableAdapter ad1 = new CreditCardTableAdapter();
		CreditCardTypeTableAdapter ad2 = new CreditCardTypeTableAdapter();
		ad1.Fill(dsCreditCard.CreditCard, customerId);
		ad2.Fill(dsCreditCard.CreditCardType);

		List<creditCardType> result = new List<creditCardType>();
		foreach (CustomerCreditCardsDataSet.CreditCardRow rowCreditCard in dsCreditCard.CreditCard)
		{
			creditCardType creditCard = new creditCardType();
			creditCard.availableBalance = rowCreditCard.AvailableBalance;
			creditCard.cardCreditLimit = rowCreditCard.CreditLimit;
			creditCard.accountNumber = rowCreditCard.CreditCardNumber;
			creditCard.customerId = rowCreditCard.CustomerId;
			creditCard.dateOpened = rowCreditCard.DateOpened;
			creditCard.lastPaymentDue = rowCreditCard.IsLastPaymentDateNull() ? DateTime.MinValue : rowCreditCard.LastPaymentDate;
			creditCard.paymentDue = rowCreditCard.IsPaymentDueNull() ? 0 : rowCreditCard.PaymentDue;

			CustomerCreditCardsDataSet.CreditCardTypeRow rowCardType = rowCreditCard.CreditCardTypeRow;
			creditCard.accountType = new creditCardTypeType();
			creditCard.accountType.id = rowCardType.CreditCardTypeId;
			creditCard.accountType.fees = rowCardType.IsFeesNull() ? 0 : (float)rowCardType.Fees;
			creditCard.accountType.interestRate = (float)rowCardType.InterestRate;
			creditCard.accountType.maxCreditLimit = rowCardType.MaxCreditLimit;
			creditCard.accountType.type = rowCardType.CreditCardType;

			result.Add(creditCard);
		}
		return result.ToArray();
	}

	internal void AssignToServicing(int queueId, string assignedTo)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();

			SqlCommand cmd = new SqlCommand("sp_AssignQueueEntry", conn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.AddWithValue("@QueueId", queueId);
			cmd.Parameters.AddWithValue("@AssignedTo", assignedTo);

			cmd.ExecuteNonQuery();
		}
	}

	internal QueueEntry[] GetMyEntries(string name)
	{
		VisitorQueue dsQueue = new VisitorQueue();
		VisitorQueueTableAdapter ad1 = new VisitorQueueTableAdapter();
		ad1.Fill(dsQueue.Queue, name);

		List<QueueEntry> entries = new List<QueueEntry>();
		foreach (VisitorQueue.QueueRow row in dsQueue.Queue)
		{
			entries.Add(CreateQueueEntry(row));
		}

		return entries.ToArray();
	}

	private QueueEntry CreateQueueEntry(VisitorQueue.QueueRow row)
	{
		QueueEntry entry = new QueueEntry();
		entry.Description = row.IsDescriptionNull() ? null : row.Description;
		entry.ReasonCode = row.ReasonCode;
		entry.Status = row.Status;
		entry.TimeIn = row.TimeIn;
		entry.VisitorName = row.VisitorName;
		entry.QueueEntryID = new QueueEntryID();
		entry.QueueEntryID.ID = row.QueueId.ToString();

		if (row.IsCustomerIdNull() == false)
		{
			DataSet ds = LookupCustomer(row.CustomerId);
			Customer[] customers = CreateCustomersFromDataSet(ds);
			entry.CustomerReference = customers[0];
		}
		if (row.IsWalkinIdNull() == false)
		{
			Walkin[] walkIn = LookupWalkin(row.WalkinId);
			if (walkIn != null && walkIn.Length > 0)
				entry.WalkinReference = walkIn[0];
		}
		return entry;
	}

	private Walkin[] LookupWalkin(string walkInId)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();
			WalkinDataset ds = new WalkinDataset();

			new WalkinTableAdapter().Fill(ds.Walkin, walkInId);
			if (ds.Walkin.Rows.Count == 0) return new Walkin[0];

			Walkin walkIn = CreateWalkingFromDataset(walkInId, ds);

			return new Walkin[1] { walkIn };
		}
	}

	private static Walkin CreateWalkingFromDataset(string walkInId, WalkinDataset ds)
	{
		Walkin walkIn = new Walkin();
		WalkinDataset.WalkinRow walkInRow = (WalkinDataset.WalkinRow)ds.Walkin.Rows[0];
		walkIn.FirstName = walkInRow.FirstName;
		walkIn.LastName = walkInRow.LastName;
		walkIn.MiddleInitial = walkInRow.MiddleInitial;
		walkIn.SSNumber = walkInRow.SSNumber;
		walkIn.MotherMaidenName = walkInRow.MotherMaidenName;

		new WalkinAddressTableAdapter().Fill(ds.WalkinAddress, walkInId);
		if (ds.WalkinAddress.Rows.Count > 0)
		{
			List<Address> addresses = new List<Address>();
			foreach (WalkinDataset.WalkinAddressRow addressRow in ds.WalkinAddress.Rows)
			{
				Address address = new Address();
				addresses.Add(address);
				address.Address1 = addressRow.Address1;
				address.Address2 = addressRow.IsAddress2Null() ? null : addressRow.Address2;
				address.AddressType = addressRow.Type;
				address.City = addressRow.City;
				address.CountryCode = addressRow.Country;
				address.PostalZipCode = addressRow.PostalZipCode;
				address.StateProvince = addressRow.StateProvince;
			}
			walkIn.Addresses = addresses.ToArray();
		}

		new WalkinEmailAddressTableAdapter().Fill(ds.WalkinEmailAddress, walkInId);
		if (ds.WalkinEmailAddress.Rows.Count > 0)
		{
			List<EmailAddress> emails = new List<EmailAddress>();
			foreach (WalkinDataset.WalkinEmailAddressRow emailRow in ds.WalkinEmailAddress.Rows)
			{
				EmailAddress email = new EmailAddress();
				emails.Add(email);
				email.EmailAddress1 = emailRow.EmailAddress;
				email.Type = emailRow.Type;
			}
			walkIn.EmailAddresses = emails.ToArray();
		}

		new WalkinPhoneNumberTableAdapter().Fill(ds.WalkinPhoneNumber, walkInId);
		if (ds.WalkinPhoneNumber.Rows.Count > 0)
		{
			List<PhoneNumber> phones = new List<PhoneNumber>();
			foreach (WalkinDataset.WalkinPhoneNumberRow phoneRow in ds.WalkinPhoneNumber.Rows)
			{
				PhoneNumber phone = new PhoneNumber();
				phones.Add(phone);
				phone.PhoneNumber1 = phoneRow.PhoneNumber;
				phoneRow.Type = phoneRow.Type;

			}
			walkIn.PhoneNumbers = phones.ToArray();
		}
		return walkIn;
	}

	private T GetValueOrDefault<T>(object value)
	{
		return value == null ? default(T) : (T)value;
	}

	internal void PurchaseCertificateOfDeposit(string customerId, string accountNumber, decimal amount, int duration, decimal interestRate)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();
			using (SqlTransaction tran = conn.BeginTransaction())
			{
				SqlCommand getBalance = new SqlCommand("sp_GetAccountBalance", conn, tran);
				getBalance.CommandType = CommandType.StoredProcedure;
				getBalance.Parameters.AddWithValue("@AccountNumber", accountNumber);
				getBalance.Parameters.Add("@Balance", SqlDbType.Decimal);
				getBalance.Parameters["@Balance"].Direction = ParameterDirection.Output;
				getBalance.ExecuteNonQuery();

				decimal balance = (decimal)getBalance.Parameters["@Balance"].Value;
				if (balance < amount)
					throw new InsufficientFundsException();

				// update balance
				SqlCommand updateBalance = new SqlCommand("sp_UpdateAccountBalance", conn, tran);
				updateBalance.CommandType = CommandType.StoredProcedure;
				updateBalance.Parameters.AddWithValue("@AccountNumber", accountNumber);
				updateBalance.Parameters.AddWithValue("@Amount", -amount);
				updateBalance.ExecuteNonQuery();

				// create account
				Random rnd = new Random();
				string newAccountNumber = rnd.Next(10000000, 99999999).ToString();
				SqlCommand createAccount = new SqlCommand("sp_CreateAccount", conn, tran);
				createAccount.CommandType = CommandType.StoredProcedure;
				createAccount.Parameters.AddWithValue("@AccountNumber", newAccountNumber);
				createAccount.Parameters.AddWithValue("@CustomerId", customerId);
				createAccount.Parameters.AddWithValue("@AccountTypeId", CertificateOfDepositAccountType);
				createAccount.Parameters.AddWithValue("@Balance", amount);
				createAccount.Parameters.AddWithValue("@DateOpened", DateTime.Now);
				createAccount.Parameters.AddWithValue("@InterestRate", interestRate);
				createAccount.ExecuteNonQuery();

				tran.Commit();
			}
		}
	}

	internal QueueEntry RemoveQueueEntry(string queueEntryId)
	{
		using (SqlConnection conn = CreateConnection())
		{
			conn.Open();
			QueueEntry entry = LookupQueueEntry(queueEntryId, conn);
			if (entry != null)
			{
				SqlCommand removeQueueEntry = new SqlCommand("sp_RemoveQueueEntry", conn);
				removeQueueEntry.Parameters.AddWithValue("@QueueId", queueEntryId);
				removeQueueEntry.CommandType = CommandType.StoredProcedure;
				removeQueueEntry.ExecuteNonQuery();
			}
			return entry;
		}
	}

	private QueueEntry LookupQueueEntry(string queueEntryId, SqlConnection conn)
	{
		SqlCommand lookupQueueEntry = new SqlCommand("sp_LookupQueueEntry", conn);
		lookupQueueEntry.CommandType = CommandType.StoredProcedure;
		lookupQueueEntry.Parameters.AddWithValue("@QueueId", queueEntryId);

		VisitorQueue.QueueDataTable result = new VisitorQueue.QueueDataTable();
		SqlDataAdapter ad = new SqlDataAdapter(lookupQueueEntry);
		ad.Fill(result);

		return CreateQueueEntry((VisitorQueue.QueueRow)result.Rows[0]);
	}
}