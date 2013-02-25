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
    public class PurchaseCD
    {
        public const string TabTitle = "Purchase CD";
        public const string AccountComboBoxId = "_accountsComboBox";
        public const string dgRatesTableDataGridView = "_ratesGridView";
        public static readonly string[] RatesTableDataGridViewColumnNames = new string[]{"Amounts", "91 - 149", 
            "150 - 181", "182 - 239", "240 - 299", "300 - 599", "600 - 2147483647"};
        //TODO: Need Clarification regarding LinkLabel Control
        public const string lnkManagerApprovalLink = "_approveLink";
        // public const string lblLinkManagerApproval = "Click here for manager approval...";
        
        #region TextBoxNames

        public const string txtDuration = "_durationTextbox";
        public const string txtAmount   = "_amountTextBox";
        public const string txtCalculatedRate = "_rateTextBox";

        #endregion

        #region ButtonNames

        public const string GetRateButtonName = "_buttonGetRate";
        public const string PurchaseButtonName = "buttonExecute";
        public const string CancelButtonName = "buttonCancel";

        #endregion

        #region LabelNames

        public const string lblDurationName             = @"Duration (days):";
        public const string lblAmountName               = "Amount:";
        public const string lblCalculatedRateName       = "Calculated Rate:";
        public const string lblAccountName              = "Account";
        public const string lblIsufficientFundsMessage  = "There are insufficient funds in the selected account to purchase the desired Certificate of Deposit";
        public const string lblUserNotAuthorizedMessage = "The user currently logged in does not have permissions to do this purchase"; 
        public const string lblRatesTable               = "Rates Table";

        #endregion
        
    }
}
