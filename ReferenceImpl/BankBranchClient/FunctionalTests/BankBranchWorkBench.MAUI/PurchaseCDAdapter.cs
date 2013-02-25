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
    public class PurchaseCDAdapter
    {

        #region Variables and Properties

        private Window shellWindow;
        private TabControl customerDetailsTabConrol;
        private TabControlTab purchaseCDTab;

        public int TabPosition
        {
            get
            {
                return purchaseCDTab.Owner.Count;
            }
        }

        private DataGridView RatesTableDataGridView
        {
            get
            {
                return MAUIUtilities.GetControl<DataGridView>(shellWindow,
                ResourceNames.PurchaseCD.dgRatesTableDataGridView);
            }
        }  
        private Button ManagerApprovalLink
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(shellWindow,
                                ResourceNames.PurchaseCD.lnkManagerApprovalLink);
            }
        }
        private ComboBox AccountComboBox
        {
           get
            { 
                return MAUIUtilities.GetControl<ComboBox>(shellWindow,
                                ResourceNames.PurchaseCD.AccountComboBoxId);
            }
        }

        #region Button Controls

        public Button GetRateButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(shellWindow,
                                ResourceNames.PurchaseCD.GetRateButtonName);
            }
        }

        public Button PurchaseButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(shellWindow,
                                ResourceNames.PurchaseCD.PurchaseButtonName);
            }
        }

        public Button CancelButton
		{
		    get
		    {
		        return MAUIUtilities.GetControl<Button>(shellWindow,
		                         ResourceNames.PurchaseCD.CancelButtonName);
		    }
        }

        #endregion

        #region TextBox Controls

        public string DurationTextBox
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                    ResourceNames.PurchaseCD.txtDuration).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                   ResourceNames.PurchaseCD.txtDuration).Text = value;
            }
        }

        public string AmountTextBox
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                    ResourceNames.PurchaseCD.txtAmount).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                   ResourceNames.PurchaseCD.txtAmount).Text = value;
            }
        }

        public string CalculatedRateTextBox
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(shellWindow,
                    ResourceNames.PurchaseCD.txtCalculatedRate).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(shellWindow,
                   ResourceNames.PurchaseCD.txtCalculatedRate).Text = value;
            }

        }

        public string GetAccountComboBoxValueByIndex(int index)
        {
            return AccountComboBox.get_Text(index);
        }

        public void SetAccountComboBoxValue(string value)
        {
            AccountComboBox.SelectByText(value);
        }
        #endregion            

        #endregion

        #region LifeTime

        public PurchaseCDAdapter(Window shellWindow, TabControl customerDetailsTabConrol)
        {
            this.shellWindow = shellWindow;
            this.customerDetailsTabConrol = customerDetailsTabConrol;
            purchaseCDTab = new TabControlTab(customerDetailsTabConrol,
                ResourceNames.PurchaseCD.TabTitle);

        }

        public bool Closed
        {
            get
            {
                return (!purchaseCDTab.Selected);
            }
        }

        public void CheckPresenceOfControls()
        {
           

            #region Button-Controls

            MAUIUtilities.GetControl<Button>(shellWindow,
                    ResourceNames.PurchaseCD.GetRateButtonName);
            MAUIUtilities.GetControl<Button>(shellWindow,
                   ResourceNames.PurchaseCD.PurchaseButtonName);
            MAUIUtilities.GetControl<Button>(shellWindow,
                   ResourceNames.PurchaseCD.CancelButtonName);

            #endregion

            #region Label-Controls

            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.PurchaseCD.lblDurationName, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.PurchaseCD.lblAmountName, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.PurchaseCD.lblAccountName, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(shellWindow,
                ResourceNames.PurchaseCD.lblCalculatedRateName, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            //MAUIUtilities.GetControl<StaticControl>(shellWindow,
            //    ResourceNames.PurchaseCD.lblNotificationMessage, StringMatchSyntax.ExactMatch,
            //    "*", StringMatchSyntax.WildCard); 

            #endregion

            #region  TextBox-Controls
            
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.PurchaseCD.txtDuration);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.PurchaseCD.txtAmount);
            MAUIUtilities.GetControl<TextBox>(shellWindow,
                     ResourceNames.PurchaseCD.txtCalculatedRate);

            #endregion

            MAUIUtilities.GetControl<ListView>(shellWindow,
               ResourceNames.PurchaseCD.dgRatesTableDataGridView);

            MAUIUtilities.GetControl<ComboBox>(shellWindow,
                ResourceNames.PurchaseCD.AccountComboBoxId);
            
            //TODO: Need Clarification regarding LinkLabel Control
            //MAUIUtilities.GetControl<Button>(shellWindow,
            //       ResourceNames.PurchaseCD.lnkManagerApprovalLink);

           
        }

        public bool CheckRatesTableDataGridViewColumnHeaders()
        {
            return Utilities.CheckDataGridViewColumnHeaders(RatesTableDataGridView,
                ResourceNames.PurchaseCD.RatesTableDataGridViewColumnNames);
        }
        
        public bool IsUserNotAuthorizedMessageDisplayed()
        {

            try
            {
                MAUIUtilities.GetControl<StaticControl>(shellWindow,
                    ResourceNames.PurchaseCD.lblUserNotAuthorizedMessage, StringMatchSyntax.ExactMatch,
                    "*", StringMatchSyntax.WildCard);
                MAUIUtilities.GetControl<Button>(shellWindow,
                   ResourceNames.PurchaseCD.lnkManagerApprovalLink);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsInSufficientFundMessageDisplayed()
        {
            
                MAUIUtilities.GetControl<StaticControl>(shellWindow,
                    ResourceNames.PurchaseCD.lblIsufficientFundsMessage, StringMatchSyntax.ExactMatch,
                    "*", StringMatchSyntax.WildCard);
                
                return true;      
           
        }
        
        
        #endregion

        #region Screen Operations

		
        public void Cancel()
        {
            //Sleeper.Delay(500);
            CancelButton.Click();
            Sleeper.Delay(500);
        }

        public void Purchase()
        {
           // Sleeper.Delay(500);
            PurchaseButton.Click();
            Sleeper.Delay(500);
        }

        public void GetRate()
        {
            //Sleeper.Delay(500);
            GetRateButton.Click();
            Sleeper.Delay(500);
        }

        public bool IsCalculatedRateTextBoxEmtpy()
        {
            return String.IsNullOrEmpty(CalculatedRateTextBox);
        }

        public AuthenAdapter ManagerApprovalLinkClick()
        {
           // Sleeper.Delay(500);
            ManagerApprovalLink.Click();
            Sleeper.Delay(500);
            
            return new AuthenAdapter();
        }       
        #endregion

    }
}
