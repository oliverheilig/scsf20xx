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
using System.Collections.Specialized;
using System.Text;
using Maui.Core;
using Maui.Core.WinControls;
using Maui.Core.Utilities;
using BankBranchWorkBench.MAUI.Common;
using BankBranchWorkBench.MAUI.Entities;

namespace BankBranchWorkBench.MAUI
{
    public class CustomerSummaryAdapter
    {
        #region Variables and Properties

        private Window shellWindow;
        private TabControl customerDetailsTabConrol;
        private TabControlTab customerSummaryTab;
        
        public int TabPosition
        {
            get
            {
                return customerSummaryTab.Index;
            }
        }

        private DataGridView AlertDataGridView
        {
            get
            {
				return MAUIUtilities.GetControl<DataGridView>(shellWindow,
                ResourceNames.CustomerSummary.AlertDataGridViewName);
            }
        }

		private DataGridView AccountDataGridView
        {
            get
            {
				return MAUIUtilities.GetControl<DataGridView>(shellWindow,
                ResourceNames.CustomerSummary.AccountDataGridViewName);
            }
        }

		private DataGridView CreditCardDataGridView
        {
            get
            {
				return MAUIUtilities.GetControl<DataGridView>(shellWindow,
                ResourceNames.CustomerSummary.CreditCardDataGridViewName);
            }
        }

        public string Name
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.NameTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.NameTextBoxName).Text = value;
            }
        }

        public string Address
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.AddressTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.AddressTextBoxName).Text = value;
            }
        }

        public string SSN
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.SSNTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.SSNTextBoxName).Text = value;
            }
        }

        public string HomeNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.HomeNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.HomeNumberTextBoxName).Text = value;
            }
        }

        public string Email
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.EMailTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.EMailTextBoxName).Text = value;
            }
        }

        public string TimeIn
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.TimeInTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.TimeInTextBoxName).Text = value;
            }
        }

        public string ReasonCode
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.ReasonCodeTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.NameTextBoxName).Text = value;
            }
        }

        public string ReasonDescription
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.DescriptionTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.DescriptionTextBoxName).Text = value;
            }
        }

        public string Status
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.StatusTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.StatusTextBoxName).Text = value;
            }
        }

        private Button ServiceCompletedButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(shellWindow,
                ResourceNames.CustomerSummary.ServiceCompletedButtonName);
            }
        }

        #endregion

        #region LifeTime

        public CustomerSummaryAdapter(Window shellWindow, TabControl customerDetailsTabConrol)
        {
            this.shellWindow = shellWindow;
            this.customerDetailsTabConrol = customerDetailsTabConrol;
            customerSummaryTab = new TabControlTab(customerDetailsTabConrol, 
                ResourceNames.CustomerSummary.TabTitle);
        }

        public bool Closed
        {
            get
            {
                return customerSummaryTab == null;
            }
        }

        #endregion

        #region Screen Operations

        public void CheckPresenceOfControls()
        {
            //TextBoxes
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.NameTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.AddressTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.SSNTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.HomeNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.EMailTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.ReasonCodeTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.DescriptionTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                ResourceNames.CustomerSummary.TimeInTextBoxName);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.CustomerSummary.StatusTextBoxName);
            
            //Button
            MAUIUtilities.GetControl<Button>(shellWindow,
                     ResourceNames.CustomerSummary.ServiceCompletedButtonName);
            
            //Labels
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.NameLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.AddressLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.SSNLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.EMailLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.TimeInLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.ReasonCodeLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.DescriptionLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.CustomerSummary.StatusLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);

            //ListViews
            MAUIUtilities.GetControl<ListView>(shellWindow,
                ResourceNames.CustomerSummary.AlertDataGridViewName);
            MAUIUtilities.GetControl<ListView>(shellWindow,
                ResourceNames.CustomerSummary.AccountDataGridViewName);
            MAUIUtilities.GetControl<ListView>(shellWindow,
                ResourceNames.CustomerSummary.CreditCardDataGridViewName);
        }
        
        public bool CheckAlertDataGridViewColumnHeaders()
        {
			return Utilities.CheckDataGridViewColumnHeaders(AlertDataGridView, 
                ResourceNames.CustomerSummary.AlertDataGridViewColumnNames);
        }

        public bool CheckAccountDataGridViewColumnHeaders()
        {
			return Utilities.CheckDataGridViewColumnHeaders(AccountDataGridView,
                ResourceNames.CustomerSummary.AccountDataGridViewColumnNames);
        }

        public bool CheckCreditCardDataGridViewColumnHeaders()
        {
			return Utilities.CheckDataGridViewColumnHeaders(CreditCardDataGridView,
                ResourceNames.CustomerSummary.CreditCardDataGridViewColumnNames);
        }

        public AccountSummary[] GetAccountSummary()
        {
            AccountSummary[] accounts = new AccountSummary[AccountDataGridView.Rows.Count - 1];
            for (int index = 1; index < AccountDataGridView.Rows.Count; index++)
            {
				DataGridViewRow accountRow = AccountDataGridView.Rows[index];

                accounts[index-1] = new AccountSummary();
                accounts[index - 1].AccountType = accountRow.Cells[0].GetValue();
				accounts[index-1].AccountNumber = accountRow.Cells[1].GetValue();
				accounts[index-1].Balance = Convert.ToDouble(accountRow.Cells[2].GetValue());
            }

            return accounts;
        }

		public CreditCardSummary[] GetCreditCardSummary()
		{
			CreditCardSummary[] creditCards = new CreditCardSummary[CreditCardDataGridView.Rows.Count - 1];
			for (int index = 1; index < CreditCardDataGridView.Rows.Count; index++)
			{
				DataGridViewRow creditCardRow = CreditCardDataGridView.Rows[index];

				creditCards[index] = new CreditCardSummary();
				creditCards[index].CreditCardType = creditCardRow.Cells[0].GetValue();
				creditCards[index].CreditCardNumber = creditCardRow.Cells[1].GetValue();
				creditCards[index].PaymentDueDate = Convert.ToDateTime(creditCardRow.Cells[2].GetValue());
				creditCards[index].Balance = Convert.ToDouble(creditCardRow.Cells[3].GetValue());
			}

			return creditCards;
		}

		public AlertSummary[] GetAlertSummary()
		{
			AlertSummary[] alerts = new AlertSummary[AlertDataGridView.Rows.Count - 1];
			for (int index = 1; index < AlertDataGridView.Rows.Count; index++)
			{
				DataGridViewRow alertRow = AlertDataGridView.Rows[index];

				alerts[index] = new AlertSummary();
				alerts[index].AlertType = alertRow.Cells[0].GetValue();
				alerts[index].Description = alertRow.Cells[1].GetValue();
				alerts[index].ExpiryDate = Convert.ToDateTime(alertRow.Cells[2].GetValue());
			}

			return alerts;
		}

        public void ServiceCompleted()
        {
            ServiceCompletedButton.Click();
            ResetCustomerSummaryTab();
        }

        private void ResetCustomerSummaryTab()
        {
            try
            {
                customerSummaryTab = new TabControlTab(customerDetailsTabConrol,
                    ResourceNames.CustomerSummary.TabTitle);
            }
            catch
            {
                customerSummaryTab = null;
            }
        }

        public void SummaryTabClick()
        {
            customerSummaryTab.Click();
            Sleeper.Delay(500);
            //return new CustomerSummaryAdapter(shellWindow, CustomerDetailsTabConrol);
        }

        public CreditCardAdapter CreditCardListClick()
        {
            CreditCardDataGridView.Rows[1].Cells[0].Click(MouseClickType.DoubleClick, MouseFlags.LeftButton);            
            Sleeper.Delay(500);
            return new CreditCardAdapter(shellWindow, customerDetailsTabConrol);
        }
        #endregion
    }
}
