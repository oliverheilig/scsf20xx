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
using Maui.Core;
using Maui.Core.WinControls;
using Maui.Core.Utilities;
using BankBranchWorkBench.MAUI.Common;

namespace BankBranchWorkBench.MAUI
{
    public class FindCustomerAdapter
    {
        #region Screen Controls

        private Window findCustomerWindow;

        #endregion

        #region LifeTime

        public FindCustomerAdapter()
        {
            findCustomerWindow = MAUIUtilities.GetWindow(BankWorkBench.Application, 
                ResourceNames.FindCustomer.WindowTitle);
        }

        public void CheckPresenceOfControls()
        {
            //TextBoxes
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.FirstNameTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.LastNameTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.MiddleInitialTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.StreetTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.CityTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.StateTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.ZipTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.HomeNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.WorkNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.CellNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.SSNTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                     ResourceNames.FindCustomer.EMailTextBoxName);

            //Button
            MAUIUtilities.GetControl<Button>(findCustomerWindow,
                     ResourceNames.FindCustomer.FindButtonName);
            MAUIUtilities.GetControl<Button>(findCustomerWindow,
                     ResourceNames.FindCustomer.CancelButtonName);

            //Label

            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.FirstNameLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.LastNameLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.MiddleInitialLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.StreetLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.CityLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.StateLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.ZipLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.HomeNumberLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.WorkNumberLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.CellNumberLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.SSNLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerWindow, 
                ResourceNames.FindCustomer.EMailLabelText, StringMatchSyntax.ExactMatch, 
                "*", StringMatchSyntax.WildCard);
        }


        public void Close()
        {
            findCustomerWindow.Extended.CloseWindow();
            findCustomerWindow = null;
        }

        public bool Closed
        {
            get
            {
                return findCustomerWindow == null;
            }
        }

        #endregion

        #region Screen Value Properties

        public string FirstName
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow, 
                    ResourceNames.FindCustomer.FirstNameTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow, 
                    ResourceNames.FindCustomer.FirstNameTextBoxName).Text = value;
            }
        }

        public string LastName
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.LastNameTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.LastNameTextBoxName).Text = value;
            }
        }

        public string MiddleInitial
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.MiddleInitialTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.MiddleInitialTextBoxName).Text = value;
            }
        }

        public string SSN
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.SSNTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.SSNTextBoxName).Text = value;
            }
        }

        public string Street
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.StreetTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.StreetTextBoxName).Text = value;
            }
        }

        public string City
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.CityTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.CityTextBoxName).Text = value;
            }
        }

        public string State
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.StateTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.StateTextBoxName).Text = value;
            }
        }

        public string Zip
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.ZipTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.ZipTextBoxName).Text = value;
            }
        }

        public string EMail
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.EMailTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.EMailTextBoxName).Text = value;
            }
        }

        public string HomeNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.HomeNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.HomeNumberTextBoxName).Text = value;
            }
        }

        public string WorkNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.WorkNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.WorkNumberTextBoxName).Text = value;
            }
        }

        public string CellNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.CellNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.CellNumberTextBoxName).Text = value;
            }
        }

        public bool IsModal
        {
            get
            {
                return MAUIUtilities.IsModal(BankWorkBench.Application, findCustomerWindow);
            }
        }

        public bool IsResizable
        {
            get
            {
                return findCustomerWindow.Extended.IsResizable;
            }
        }

        public bool IsFindButtonEnabled
        {
            get
            {
                return FindButton.IsEnabled;
            }
        }

        public bool IsCancelButtonEnabled
        {
            get
            {
                return CancelButton.IsEnabled;
            }
        }

        public bool IsMessageDisplayed
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerWindow,
                    ResourceNames.FindCustomer.ErrorLabelText, StringMatchSyntax.ExactMatch, "*", 
                    StringMatchSyntax.WildCard) != null;
            }
        }

        private Button FindButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(findCustomerWindow,
                        ResourceNames.FindCustomer.FindButtonName);
            }
        }

        private Button CancelButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(findCustomerWindow,
                         ResourceNames.FindCustomer.CancelButtonName);
            }
        }

        #endregion

        #region Screen Operations

        public void Clear()
        {            
            FirstName = String.Empty;
            LastName = String.Empty;
            MiddleInitial = String.Empty;
            Street = String.Empty;
            City = String.Empty;
            State = String.Empty;
            Zip = String.Empty;
            EMail = String.Empty; 
            HomeNumber = String.Empty;
            WorkNumber = String.Empty;
            CellNumber = String.Empty; 
            SSN = String.Empty;  
        }

        public FindCustomerResultsAdapter Find()
        {
            FindButton.Click();
            Sleeper.Delay(500);
            ResetFindCustomerWindow();
            return new FindCustomerResultsAdapter();
        }

        public FindCustomerResultsAdapter EnterPressed()
        {
            findCustomerWindow.SendKeys("{ENTER}");
            Sleeper.Delay(500);
            ResetFindCustomerWindow();
            return new FindCustomerResultsAdapter();
        }

        public void Cancel()
        {
            CancelButton.Click();
            Sleeper.Delay(500);
            ResetFindCustomerWindow();
        }
        
        public void EscPressed()
        {
            findCustomerWindow.SendKeys("{ESC}");
            Sleeper.Delay(500);
            ResetFindCustomerWindow();
        }

        private void ResetFindCustomerWindow()
        {
            findCustomerWindow = MAUIUtilities.GetWindow(BankWorkBench.Application,
                ResourceNames.FindCustomer.WindowTitle, false);
        }

        #endregion

    }
}
