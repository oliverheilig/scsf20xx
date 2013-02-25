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
using System.Configuration;
using System.Reflection;
using DeploymentRepositoryProvider.Properties;

namespace DeploymentRepositoryProvider
{
    /// <summary>
    /// This class acts as a factory for deployment repository providers.
    /// It expects a Visual Studio Settings configuration setting value is set
    /// for the hosting application that will provide the type name and assembly name
    /// for the provider.
    /// </summary>
    public static class DeploymentRepositoryProviderFactory
    {
        /// <summary>
        /// Creates the instance of the provider.
        /// </summary>
        /// <returns>Provider instance.</returns>
        public static IDeploymentRepository CreateInstance()
        {
            string assemblyName;
            string typeName;
            // Extract the assembly name and type name from the configuration setting.
            GetTypeInfo(Settings.Default.DeploymentRepositoryProvider, out assemblyName, out typeName);
            if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(typeName))
            {
                throw new ConfigurationErrorsException("Invalid type information in the configuration for provider.");
            }
            // Dynamically load the assembly using normal resolution.
            Assembly assem = Assembly.Load(assemblyName);
            if (assem == null)
            {
                throw new ConfigurationErrorsException(String.Format("Could not load assembly {0}.", assemblyName));
            }
            // Dynamic instance creation.
            IDeploymentRepository instance = assem.CreateInstance(typeName) as IDeploymentRepository;
            return instance;
        }

        // Helper method to extract the type information from a string.
        // Expects the following format: Fully.Qualified.TypeName, AssemblyName
        private static void GetTypeInfo(string componentTypeInfo, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = null;
            // Configuration entries should be split into 
            // two parts and in the following form:
            // Fully.Qualified.TypeName, AssemblyName
            // Split it into its two parts.
            string[] typeInfo = componentTypeInfo.Split(',');
            if (typeInfo == null || typeInfo.Length < 2)
            {
                return; //invalid type info; ignore
            }
            typeName = typeInfo[0];
            assemblyName = typeInfo[1];
            if (string.IsNullOrEmpty(assemblyName) || string.IsNullOrEmpty(typeName))
            {
                return; //invalid type info; ignore
            }
            assemblyName = assemblyName.Trim();
            typeName = typeName.Trim();
        }
    }
}
