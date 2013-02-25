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
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.Practices.SmartClient.EnterpriseLibrary;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Mocks;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests
{
	/// <summary>
    /// Summary description for DatabaseRequestManagerIntializerFixture
	/// </summary>
    [TestClass]
    public class DatabaseRequestManagerIntializerFixture
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            IServiceLocator mockServiceLocator = new MockServiceLocator(EnterpriseLibraryContainer.Current);
            EnterpriseLibraryContainer.Current = mockServiceLocator;            
        }

        [ClassCleanup]
        public static void ClassCleanup()
        { 
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),"Datastore.sdf");
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        [TestMethod]
        public void ShouldInitalizeRequestManager()
        {
            using (SmartClientDatabase database = GetDatabase())
            {
                RequestManager requestManager = DatabaseRequestManagerIntializer.Initialize();

                Assert.IsNotNull(requestManager);
                Assert.IsNotNull(requestManager.RequestQueue);
                Assert.IsNotNull(requestManager.DeadLetterQueue);
                Assert.IsNotNull(requestManager.EndpointCatalog);
            }
        }

        [TestMethod]
        public void ShouldInitalizeManagerRequestManager()
        {
            using (SmartClientDatabase database = GetDatabase())
            {
                RequestManager requestManager = DatabaseRequestManagerIntializer.Initialize("TestConnectionString");

                Assert.IsNotNull(requestManager);
                Assert.IsNotNull(requestManager.RequestQueue);
                Assert.IsNotNull(requestManager.DeadLetterQueue);
                Assert.IsNotNull(requestManager.EndpointCatalog);
            }
        }

        private static SmartClientDatabase GetDatabase()
        {
            const int blockSize = 65536;
            Stream resourceStream =
                Assembly.GetExecutingAssembly().GetManifestResourceStream(
                    "Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Datastore.sdf");
            using (BinaryReader reader = new BinaryReader(resourceStream))
            {
                FileStream output = new FileStream("Datastore.sdf", FileMode.Create);
                using (BinaryWriter writer = new BinaryWriter(output))
                {
                    byte[] bytes = reader.ReadBytes(blockSize);
                    while (bytes.Length > 0)
                    {
                        writer.Write(bytes);
                        bytes = reader.ReadBytes(blockSize);
                    }
                }
            }
            return (SmartClientDatabase)DatabaseFactory.CreateDatabase();
        }
    }
}