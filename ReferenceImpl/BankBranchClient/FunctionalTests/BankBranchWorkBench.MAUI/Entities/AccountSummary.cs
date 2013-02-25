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

namespace BankBranchWorkBench.MAUI.Entities
{
    public class AccountSummary
    {
        private string _accountNumber;
        private string _accountType;
        private double _balance;

        public string AccountNumber
        {
            get
            {
                return _accountNumber;
            }

            set
            {
                _accountNumber = value;
            }
        }

        public string AccountType
        {
            get
            {
                return _accountType;
            }

            set
            {
                _accountType = value;
            }
        }

        public double Balance
        {
            get
            {
                return _balance;
            }

            set
            {
                _balance = value;
            }
        }

    }
}
