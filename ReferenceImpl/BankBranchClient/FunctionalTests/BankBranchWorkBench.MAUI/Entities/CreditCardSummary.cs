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
    public class CreditCardSummary
    {
        private string _creditCardNumber;
        private string _creditCardType;
        private DateTime _paymentDueDate;
        private double _balance;

        public string CreditCardNumber
        {
            get
            {
                return _creditCardNumber;
            }

            set
            {
                _creditCardNumber = value;
            }
        }

        public string CreditCardType
        {
            get
            {
                return _creditCardType;
            }

            set
            {
                _creditCardType = value;
            }
        }

        public DateTime PaymentDueDate
        {
            get
            {
                return _paymentDueDate;
            }

            set
            {
                _paymentDueDate = value;
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
