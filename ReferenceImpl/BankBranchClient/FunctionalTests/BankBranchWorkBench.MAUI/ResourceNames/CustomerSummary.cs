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
using System.Text;

namespace BankBranchWorkBench.MAUI.ResourceNames
{
    class CustomerSummary
    {
        public const string TabTitle = "Summary";

        //TextBoxes 		
        public const string NameTextBoxName = "_nameTextBox";
        public const string AddressTextBoxName = "_addressTextBox";
        public const string SSNTextBoxName = "_ssnTextBox";
        public const string HomeNumberTextBoxName = "_homeNumberTextBox";
        public const string EMailTextBoxName = "_emailTextBox";
        public const string TimeInTextBoxName = "_timeInTextBox";
        public const string ReasonCodeTextBoxName = "_reasonCodeTextBox";
        public const string DescriptionTextBoxName = "_descriptionTextBox";
        public const string StatusTextBoxName = "_statusTextBox";

        //Labels
        public const string NameLabelText = "Name";
        public const string AddressLabelText = "Address";
        public const string SSNLabelText = "SSN";
        public const string HomeNumberLabelText = "Home Phone";
        public const string EMailLabelText = "Email";
        public const string TimeInLabelText = "Time In";
        public const string ReasonCodeLabelText = "Reason";
        public const string DescriptionLabelText = "Description";
        public const string StatusLabelText = "Status";

        //Buttons
        public const string ServiceCompletedButtonName = "Service Completed";

        //List Views
        public const string AlertDataGridViewName = "_alertDataGridView";
		public const string AccountDataGridViewName = "_accountDataGridView";
		public const string CreditCardDataGridViewName = "_creditCardDataGridView";

        public static readonly string[] AlertDataGridViewColumnNames = new string[]{"Alert Type", "Expiration Date"};
        public static readonly string[] AccountDataGridViewColumnNames = new string[]{"Basic Accounts", "Number", 
            "Balance"};
        public static readonly string[] CreditCardDataGridViewColumnNames = new string[]{"Account", "Number", 
            "Payment Due", "Balance"};
    }
}
