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
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.QuickStarts.WPFIntegration.EmployeeData.BusinessEntities;

namespace Microsoft.Practices.QuickStarts.WPFIntegration.EmployeeData.Services
{
    [Service]
    public class EmployeeService
    {
        private List<Employee> _employees = new List<Employee>();

        public EmployeeService()
        {
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            _employees.Add(new Employee(1, "Abbas Syed", "President", 34, DateTime.Now.AddYears(-13)));
            _employees.Add(new Employee(2, "Nadar Issa", "CEO", 56, DateTime.Now.AddYears(-1)));
            _employees.Add(new Employee(3, "Manar Karim", "Sales Manager", 24, DateTime.Now.AddYears(-2)));    
            _employees.Add(new Employee(4, "Mohamed Shammi", "HR Manager", 67, DateTime.Now.AddYears(-4)));    
            _employees.Add(new Employee(5, "Glasson Stuart", "CTO", 32, DateTime.Now.AddYears(-6)));
            _employees.Add(new Employee(6, "Breyer Markus", "Development Manager", 21, DateTime.Now.AddYears(-3)));    
        }

        public Employee GetEmployee(int id)
        {
            return _employees.Find(delegate(Employee match)
            {
                if (match.Id == id) 
                    return true;
                return false;
            });
        }
    }
}
