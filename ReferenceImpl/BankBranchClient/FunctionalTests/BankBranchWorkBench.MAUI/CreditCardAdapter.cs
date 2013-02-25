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
    public class CreditCardAdapter
    {

        #region Variables and Properties

        private Window shellWindow;
        private TabControl customerDetailsTabConrol;
        private TabControlTab creditCardTab;

        public int TabPosition
        {
            get
            {
                return creditCardTab.Owner.Count;
            }
        }            

        #region Button Controls

        public Button CancelButton
		{
		    get
		    {
		        return MAUIUtilities.GetControl<Button>(shellWindow,
		                         ResourceNames.CreditCard.CancelButtonName);
		    }
        }

        #endregion
       

        #endregion

        #region LifeTime

        public CreditCardAdapter(Window shellWindow, TabControl customerDetailsTabConrol)
        {
            this.shellWindow = shellWindow;
            this.customerDetailsTabConrol = customerDetailsTabConrol;
            creditCardTab = new TabControlTab(customerDetailsTabConrol,
                ResourceNames.CreditCard.TabTitle + "2341");
        }

        public bool Closed
        {
            get
            {
                return (!creditCardTab.Selected);
            }
        }        
        
        #endregion

        #region Screen Operations
		
        public void Cancel()
        {
            //Sleeper.Delay(500);
            CancelButton.Click();
            Sleeper.Delay(500);
        }
       
        #endregion

    }
}
