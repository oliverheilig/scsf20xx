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
    public class FindCustomerResultsAdapter
    {
        #region Screen Controls

        private Window findCustomerResultsWindow;

        #endregion

        #region LifeTime

        public FindCustomerResultsAdapter()
        {
            findCustomerResultsWindow = MAUIUtilities.GetWindow(BankWorkBench.Application,
                ResourceNames.FindCustomerResults.WindowTitle);
        }

        public void Close()
        {
            findCustomerResultsWindow.Extended.CloseWindow();
            findCustomerResultsWindow = null;
        }

        public bool Closed
        {
            get
            {
                return findCustomerResultsWindow == null;
            }
        }

        #endregion

        #region Screen Value Properties

        public string FirstName
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.FirstNameTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.FirstNameTextBoxName).Text = value;
            }
        }

        public string LastName
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.LastNameTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.LastNameTextBoxName).Text = value;
            }
        }

        public string MiddleInitial
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.MiddleInitialTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.MiddleInitialTextBoxName).Text = value;
            }
        }

        public string SSN
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.SSNTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.SSNTextBoxName).Text = value;
            }
        }

        public string Street
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StreetTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StreetTextBoxName).Text = value;
            }
        }

        public string City
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CityTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CityTextBoxName).Text = value;
            }
        }

        public string State
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StateTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StateTextBoxName).Text = value;
            }
        }

        public string Zip
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.ZipTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.ZipTextBoxName).Text = value;
            }
        }

        public string EMail
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.EMailTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.EMailTextBoxName).Text = value;
            }
        }

        public string HomeNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.HomeNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.HomeNumberTextBoxName).Text = value;
            }
        }

        public string WorkNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.WorkNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.HomeNumberTextBoxName).Text = value;
            }
        }

        public string CellNumber
        {
            get
            {
                return MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CellNumberTextBoxName).Text;
            }
            set
            {
                MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CellNumberTextBoxName).Text = value;
            }
        }

        public bool IsModal
        {
            get
            {
                return MAUIUtilities.IsModal(BankWorkBench.Application, findCustomerResultsWindow);
            }
        }

        public int RecordCount
        {
            get
            {
                return CustomerDataGridView.Rows.Count;
            }
        }

        private DataGridView CustomerDataGridView
        {
            get
            {
                return MAUIUtilities.GetControl<DataGridView>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CustomerDataGridViewName);
            }
        }

        private Button QueueForServiceButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.QueueForServiceButtonName);
            }
        }

        private Button AddReasonButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.AddReasonButtonName);
            }
        }
        private Button SelfServiceButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.SelfServiceButtonName);
            }
        }


        private Button CancelButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CancelButtonName);
            }
        }

        #endregion

        #region Screen Operations

        public void CheckPresenceOfControls()
        {
            //TextBoxes
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.FirstNameTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.LastNameTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.MiddleInitialTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StreetTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CityTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StateTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.ZipTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.HomeNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.WorkNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CellNumberTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.SSNTextBoxName);
            MAUIUtilities.GetControl<TextBox>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.EMailTextBoxName);

            //List View
            MAUIUtilities.GetControl<DataGridView>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CustomerDataGridViewName);

            //Buttons
            MAUIUtilities.GetControl<Button>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.QueueForServiceButtonName);
            //MAUIUtilities.GetControl<Button>(findCustomerResultsWindow,
            //    ResourceNames.FindCustomerResults.SelfServiceButtonName);
            MAUIUtilities.GetControl<Button>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CancelButtonName);

            //Labels
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.FirstNameLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.LastNameLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.MiddleInitialLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StreetLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CityLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.StateLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.ZipLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.HomeNumberLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.WorkNumberLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.CellNumberLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.SSNLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
            MAUIUtilities.GetControl<StaticControl>(findCustomerResultsWindow,
                ResourceNames.FindCustomerResults.EMailLabelText, StringMatchSyntax.ExactMatch,
                "*", StringMatchSyntax.WildCard);
        }

        public bool CheckCustomerDataGridViewColumnHeaders()
        {
            DataGridView customerResultsDataGridView = CustomerDataGridView;

            for(int index = 1;index < customerResultsDataGridView.ColumnHeaders.Count; index++)
            {
                if (customerResultsDataGridView.ColumnHeaders[index].Name !=
                    ResourceNames.FindCustomerResults.CustomerDataGridViewColumnNames[index - 1])
                {
                    return false;
                }                
            }

            return true;
        }

        public void SelectCustomerDataGridRow(int dataGridRowId)
        {
            CustomerDataGridView.Rows[dataGridRowId].Click();
        }

        public void Cancel()
        {
            CancelButton.Click();
            Sleeper.Delay(500);
            ResetFindCustomerResultsWindow();
        }
        
        public void EscPressed()
        {
            findCustomerResultsWindow.SendKeys("{ESC}");
            Sleeper.Delay(500);
            ResetFindCustomerResultsWindow();
        }

        public CustomerResult[] GetResults()
        {
            DataGridView customerGridView = CustomerDataGridView;
            CustomerResult[] result = new CustomerResult[customerGridView.Rows.Count - 1];
            for (int index = 0; index < customerGridView.Rows.Count - 1; index++)
            {
                DataGridViewRow customerDataGridRow = customerGridView.Rows[index + 1];
                
                result[index] = new CustomerResult();
                result[index].LastName = customerDataGridRow.Cells[0].GetValue();
                result[index].MiddleInitial = customerDataGridRow.Cells[1].GetValue();
                result[index].FirstName = customerDataGridRow.Cells[2].GetValue();
                result[index].MothersMaiden = customerDataGridRow.Cells[3].GetValue();
                result[index].CustomerLevel = customerDataGridRow.Cells[4].GetValue();
               // result[index].SSN = customerDataGridRow.Cells[5].GetValue();
            }

            return result;
        }

        public AddReasonAdapter QueueForService()
        {
            QueueForServiceButton.Click();
            Sleeper.Delay(500);
            ResetFindCustomerResultsWindow();
            return new AddReasonAdapter();
        }

        public void SelfService()
        {
            SelfServiceButton.Click();
            Sleeper.Delay(500);
            ResetFindCustomerResultsWindow();
        }

        public AddReasonAdapter AddReason()
        {
            AddReasonButton.Click();
            Sleeper.Delay(500);
            ResetFindCustomerResultsWindow();
            return new AddReasonAdapter();
        }

        public StringCollection GetContextMenuOptionOnDataGridView()
        {
            SelectCustomerDataGridRow(0);
            Menu contextMenu = new Menu(ContextMenuAccessMethod.ShiftF10);

            StringCollection contextMenuOptions = new StringCollection();

            foreach(MenuItem menuOption in contextMenu.MenuItems)
            {
                contextMenuOptions.Add(menuOption.Text);
            }

            return contextMenuOptions;
        }

        public AddReasonAdapter AddReasonContextMenuClick()
        {
            Menu contextMenu = new Menu(ContextMenuAccessMethod.ShiftF10);
            contextMenu[ResourceNames.FindCustomerResults.AddReasonContextMenuName].Execute();
            Sleeper.Delay(500);
            return new AddReasonAdapter();
        }

        public void QueueForServiceContextMenuClick()
        {
            Menu contextMenu = new Menu(ContextMenuAccessMethod.ShiftF10);
            contextMenu[ResourceNames.FindCustomerResults.QueueForServiceContextMenuName].Execute();
            Sleeper.Delay(500);
            ResetFindCustomerResultsWindow();
        }

        private void ResetFindCustomerResultsWindow()
        {
            Sleeper.Delay(500);
            findCustomerResultsWindow = MAUIUtilities.GetWindow(BankWorkBench.Application,
                ResourceNames.FindCustomerResults.WindowTitle, false);
        }

        #endregion
    }
}
