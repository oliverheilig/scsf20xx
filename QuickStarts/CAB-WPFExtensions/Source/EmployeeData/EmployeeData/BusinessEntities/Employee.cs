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

namespace Microsoft.Practices.QuickStarts.WPFIntegration.EmployeeData.BusinessEntities
{
    public class Employee
    {
        public Employee()
        {
        }

        public Employee(int id, string name, string position, int age, DateTime employedOn)
        {
            _id = id;
            _name = name;
            _position = position;
            _age = age;
            _employedOn = employedOn;
        }

        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
	
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _position;

        public string Position
        {
            get { return _position; }
            set { _position = value; }
        }

        private int _age;

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        private DateTime _employedOn;

        public DateTime EmployedOn
        {
            get { return _employedOn; }
            set { _employedOn = value; }
        }
	
    }
}
