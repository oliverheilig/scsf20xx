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
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using System.Configuration;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Mocks
{
    public class MockServiceLocator : IServiceLocator
    {
        private IServiceLocator entLibServiceLocator;

        public MockServiceLocator(IServiceLocator entLibServiceLocator)
        {
            this.entLibServiceLocator = entLibServiceLocator;
        }

        #region IServiceLocator Members

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public TService GetInstance<TService>(string key)
        {
            return (TService)this.GetInstance(typeof(TService), key);
        }

        public TService GetInstance<TService>()
        {
            return (TService)this.GetInstance(typeof(TService), null);
        }

        public object GetInstance(Type serviceType, string key)
        {
            switch (serviceType.Name)
            {
                case "Database":
                    {
                        string dataBaseStoreConnectionString = ConfigurationManager.ConnectionStrings["TestConnectionString"].ConnectionString;
                        return new SmartClientDatabase(dataBaseStoreConnectionString);
                    }
                default:
                    {
                        return entLibServiceLocator.GetInstance(serviceType, key);
                    }
            }
        }

        public object GetInstance(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IServiceProvider Members

        public object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion        
    }
}
