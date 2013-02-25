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
using System.ServiceModel;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent.Tests.Mocks
{
	// Define a service contract.
	[ServiceContract]
	public interface IMockService
	{      
	}

	// Service class that implements the service contract.
	public class MockService : IMockService
	{        
    }

    [ServiceContract]
    public interface IMockForReleaseService
    {
        [OperationContract]
        void Foo();

        [OperationContract]
        void FooThrow();
    }

    // Service class that implements the service contract.
    public class MockForReleaseService : IMockForReleaseService
    {
        public void Foo()
        {
        }

        public void FooThrow()
        {
            throw new System.Exception("The method or operation is not implemented.");
        }

    }
}