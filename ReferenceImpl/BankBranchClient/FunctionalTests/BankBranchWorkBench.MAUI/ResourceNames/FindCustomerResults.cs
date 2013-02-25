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

namespace BankBranchWorkBench.MAUI.ResourceNames
{
    public class FindCustomerResults
    {
        public const string WindowTitle = "Find Customer Results";

        //Text Boxes
        public const string FirstNameTextBoxName = "_firstNameTextBox";
        public const string LastNameTextBoxName = "_lastNameTextBox";
        public const string MiddleInitialTextBoxName = "_middleInitialTextBox";
        public const string StreetTextBoxName = "_streetTextBox";
        public const string CityTextBoxName = "_cityTextBox";
        public const string StateTextBoxName = "_stateTextBox";
        public const string ZipTextBoxName = "_zipTextBox";
        public const string HomeNumberTextBoxName = "_homeNumberTextBox";
        public const string WorkNumberTextBoxName = "_workNumberTextBox";
        public const string CellNumberTextBoxName = "_cellNumberTextBox";
        public const string SSNTextBoxName = "_ssnTextBox";
        public const string EMailTextBoxName = "_emailTextBox";

        //List View
        public const string CustomerDataGridViewName = "_customerDataGridView";

        public static readonly string[] CustomerDataGridViewColumnNames = new string[] { "LastName", 
            "MiddleInitial", "FirstName", "MotherMaidenName", "CustomerLevel"};        

        //Context Menu
        public const string AddReasonContextMenuName = "Add Reason";
        public const string QueueForServiceContextMenuName = "Queue for Service";

        //Labels
        public const string FirstNameLabelText = "First &Name";
        public const string LastNameLabelText = "&Last Name";
        public const string MiddleInitialLabelText = "M&I";
        public const string StreetLabelText = "St&reet";
        public const string CityLabelText = "&City";
        public const string StateLabelText = "S&tate";
        public const string ZipLabelText = "&Zip";
        public const string HomeNumberLabelText = "Home &Phone";
        public const string WorkNumberLabelText = "&Work Phone";
        public const string CellNumberLabelText = "&Mobile Phone";
        public const string SSNLabelText = "&SSN";
        public const string EMailLabelText = "&Email Address";

        //Buttons
        public const string QueueForServiceButtonName = "Queue For Service";
        public const string AddReasonButtonName = "Add Reason";
        public const string SelfServiceButtonName = "Self Service";
        public const string CancelButtonName = "Cancel";
    }
}
