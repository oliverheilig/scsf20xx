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
    public class CustomerResult
    {
        private string firstName;
        private string middleInitial;
        private string lastName;
        private string ssn;
        private string mothersMaiden;
        private string customerLevel;

        public string FirstName
        {
            get
            {
                return firstName;
            }

            set
            {
                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }

            set
            {
                lastName = value;
            }
        }

        public string MiddleInitial
        {
            get
            {
                return middleInitial;
            }

            set
            {
                middleInitial = value;
            }
        }

        public string SSN
        {
            get
            {
                return ssn;
            }

            set
            {
                ssn = value;
            }
        }

        public string MothersMaiden
        {
            get
            {
                return mothersMaiden;
            }

            set
            {
                mothersMaiden = value;
            }
        }

        public string CustomerLevel
        {
            get
            {
                return customerLevel;
            }

            set
            {
                customerLevel = value;
            }
        }        
    }
}