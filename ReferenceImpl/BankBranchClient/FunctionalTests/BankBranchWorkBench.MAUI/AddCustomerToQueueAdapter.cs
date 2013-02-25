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
    public class AddCustomerToQueueAdapter
    {
        #region Screen Controls
        private Window addCustomerToQueueWindow;
        #endregion

        #region Lifetime
        public AddCustomerToQueueAdapter()
        {
            addCustomerToQueueWindow = MAUIUtilities.GetWindow(BankWorkBench.Application, ResourceNames.AddCustomerToQueue.WindowTitle);
        }

        public void CheckPresenceOfControls()
        {
            //TextBoxes
            //MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
            //         ResourceNames.AddCustomerToQueue.CustomerIDTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.FirstNameTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.LastNameTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.MiddleInitialTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.StreetTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.CityTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.StateTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.ZipTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.HomeNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.WorkNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.CellNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.SSNTextBoxName);
            MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.EMailTextBoxName);

            //Button
            //MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
            //         ResourceNames.AddCustomerToQueue.QueueForServiceButtonName);
            //MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
            //         ResourceNames.AddCustomerToQueue.SelfServiceButtonName);
            MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.OkButtonName);
            MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
                     ResourceNames.AddCustomerToQueue.CancelButtonName);

            //Label
            //MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
            //    ResourceNames.AddCustomerToQueue.CustomerIDLabelName, StringMatchSyntax.ExactMatch,
            //    "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.FirstNameLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.LastNameLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.MiddleInitialLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.StreetLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.CityLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.StateLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.ZipLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.HomeNumberLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.WorkNumberLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.CellNumberLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.SSNLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(addCustomerToQueueWindow,
                ResourceNames.AddCustomerToQueue.EMailLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
        }

        public void Close()
        {
            addCustomerToQueueWindow.Extended.CloseWindow();
            addCustomerToQueueWindow = null;
        }

        public bool Closed
        {
            get
            {
                return addCustomerToQueueWindow == null;
            }
        }

        #endregion

        #region Screen Value Properties
        //public string CustomerID
        //{
        //    get
        //    {
        //        return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
        //            ResourceNames.AddCustomerToQueue.CustomerIDTextBoxName).Text;
        //    }
        //    set
        //    {
        //        MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
        //            ResourceNames.AddCustomerToQueue.CustomerIDTextBoxName).Text = value;
        //    }
        //}

        public string FirstName
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.FirstNameTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.FirstNameTextBoxName).Text = value;
            }
        }

        public string LastName
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.LastNameTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.LastNameTextBoxName).Text = value;
            }
        }

        public string MiddleInitial
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.MiddleInitialTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.MiddleInitialTextBoxName).Text = value;
            }
        }

        public string SSN
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.SSNTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.SSNTextBoxName).Text = value;
            }
        }

        public string Street
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.StreetTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.StreetTextBoxName).Text = value;
            }
        }

        public string City
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.CityTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.CityTextBoxName).Text = value;
            }
        }

        public string State
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.StateTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.StateTextBoxName).Text = value;
            }
        }

        public string Zip
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.ZipTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.ZipTextBoxName).Text = value;
            }
        }

        public string EMail
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.EMailTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.EMailTextBoxName).Text = value;
            }
        }

        public string HomeNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.HomeNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.HomeNumberTextBoxName).Text = value;
            }
        }

        public string WorkNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.WorkNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.WorkNumberTextBoxName).Text = value;
            }
        }

        public string CellNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.CellNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.CellNumberTextBoxName).Text = value;
            }
        }

        public string ReasonForVisit
        {
            get
            {
                return MAUIUtilities.GetControl<ComboBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.ReasonCodeComboBoxName).Text;
            }

            set
            {
                MAUIUtilities.GetControl<ComboBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.ReasonCodeComboBoxName).SelectByText(value, true);
            }
        }

        public string Description
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.DescriptionTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(addCustomerToQueueWindow,
                    ResourceNames.AddCustomerToQueue.DescriptionTextBoxName).Text = value;
            }
        }

        private Button QueueForServiceButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
                        ResourceNames.AddCustomerToQueue.QueueForServiceButtonName);
            }
        }

        private Button SelfServiceButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
                        ResourceNames.AddCustomerToQueue.SelfServiceButtonName);
            }
        }

        private Button CancelButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
                         ResourceNames.AddCustomerToQueue.CancelButtonName);
            }
        }

        private Button OkButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(addCustomerToQueueWindow,
                         ResourceNames.AddCustomerToQueue.OkButtonName);
            }
        }

        public bool IsModal
        {
            get
            {
                return MAUIUtilities.IsModal(BankWorkBench.Application, addCustomerToQueueWindow);
            }
        }

        public bool IsResizable
        {
            get
            {
                return addCustomerToQueueWindow.Extended.IsResizable;
            }
        }

        public bool IsQueueForServiceButtonEnabled
        {
            get
            {
                return QueueForServiceButton.IsEnabled;
            }
        }

        public bool IsSelfServiceButtonEnabled
        {
            get
            {
                return SelfServiceButton.IsEnabled;
            }
        }

        public bool IsCancelButtonEnabled
        {
            get
            {
                return CancelButton.IsEnabled;
            }
        }

        public bool IsOkButtonEnabled
        {
            get
            {
                return OkButton.IsEnabled;
            }
        }
        #endregion

        #region Screen Operations
        public void Clear()
        {
            //CustomerID = String.Empty;
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
            ReasonForVisit = null;
            Description = String.Empty;
        }

        public void QueueForService()
        {
            QueueForServiceButton.Click();
            Sleeper.Delay(500);
            ResetAddCustomerToQueueWindow();
        }
        public void OkClick()
        {
            OkButton.Click();
            Sleeper.Delay(500);
            ResetAddCustomerToQueueWindow();
        }

        public void SelfService()
        {
            SelfServiceButton.Click();
            Sleeper.Delay(500);
            ResetAddCustomerToQueueWindow();
        }

        public void Cancel()
        {
            CancelButton.Click();
            Sleeper.Delay(500);
            ResetAddCustomerToQueueWindow();
        }

        public void EscPressed()
        {
            addCustomerToQueueWindow.SendKeys("{ESC}");
            Sleeper.Delay(500);
            ResetAddCustomerToQueueWindow();
        }

        public void QueueForServiceContextMenuClick()
        {
            Menu contextMenu = new Menu(ContextMenuAccessMethod.ShiftF10);
            contextMenu[ResourceNames.AddCustomerToQueue.QueueForServiceContextMenuText].Execute();
            Sleeper.Delay(500);
            ResetAddCustomerToQueueWindow();
        }

        public void SelfServiceContextMenuClick()
        {
            Menu contextMenu = new Menu(ContextMenuAccessMethod.ShiftF10);
            contextMenu[ResourceNames.AddCustomerToQueue.SelfServiceContextMenuText].Execute();
            Sleeper.Delay(500);
            ResetAddCustomerToQueueWindow();
        }

        private void ResetAddCustomerToQueueWindow()
        {
            addCustomerToQueueWindow = MAUIUtilities.GetWindow(BankWorkBench.Application,
                ResourceNames.AddCustomerToQueue.WindowTitle, false);
        }
        #endregion

    }
}
