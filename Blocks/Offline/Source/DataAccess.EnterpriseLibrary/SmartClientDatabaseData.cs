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
using Microsoft.Practices.EnterpriseLibrary.Data.SqlCe;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration.ContainerModel;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Instrumentation;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary
{
    /// <summary>
    /// Describes a <see cref="SmartClientDatabase"/> instance, aggregating information from a
    /// <see cref="ConnectionStringSettings"/>.
    /// </summary>
    public class SmartClientDatabaseData : SqlCeDatabaseData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmartClientDatabase"/> class with a connection string and a configuration
        /// source.
        ///</summary>
        ///<param name="connectionStringSettings">The <see cref="ConnectionStringSettings"/> for the represented database.</param>
        ///<param name="configurationSource">The <see cref="IConfigurationSource"/> from which additional information can 
        /// be retrieved if necessary.</param>
        public SmartClientDatabaseData(ConnectionStringSettings connectionStringSettings, IConfigurationSource configurationSource)
            : base(connectionStringSettings, configurationSource)
        {
        }

        /// <summary>
        /// Creates a <see cref="TypeRegistration"/> instance describing the database represented by 
        /// this configuration object.
        /// </summary>
        /// <returns>A <see cref="TypeRegistration"/> instance describing a database.</returns>
        public override IEnumerable<Practices.EnterpriseLibrary.Common.Configuration.ContainerModel.TypeRegistration> GetRegistrations()
        {
            yield return new TypeRegistration<Database>(
                () => new SmartClientDatabase(
                        ConnectionString,
                        Container.Resolved<IDataInstrumentationProvider>(Name)))
                        {
                            Name = Name,
                            Lifetime = TypeRegistrationLifetime.Transient
                        };
        }
    }
}
