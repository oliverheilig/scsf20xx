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
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.IO;
using System.Text;
using EnvDTE;
using Microsoft.Practices.Common;
using Microsoft.Practices.ComponentModel;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.SmartClientFactory.Properties;

namespace Microsoft.Practices.SmartClientFactory.ValueProviders
{
    [ServiceDependency(typeof(DTE))]
    [ServiceDependency(typeof(IDictionaryService))]
    public class LibraryFilePathProvider : ValueProvider, IAttributesConfigurable
    {
        public const string LibraryFileAttribute = "LibraryFile";
        public const string LibraryFolderAttribute = "LibFolder";

        private string libraryFileArgument;
        private string libFolderArgument = "Lib";

        public override bool OnBeginRecipe(object currentValue, out object newValue)
        {
            DTE vs = GetService<DTE>();
            string solutionPath = Path.GetDirectoryName(vs.Solution.FullName);
            solutionPath = Path.Combine(solutionPath, libFolderArgument);
            solutionPath = Path.Combine(solutionPath, libraryFileArgument);
            newValue = solutionPath;
            return true;
        }

        #region IAttributesConfigurable Members

        ///<summary>
        ///
        ///            Configures the component using the dictionary of attributes specified 
        ///            in the configuration file.
        ///            
        ///</summary>
        ///
        ///<param name="attributes">The attributes in the configuration element.</param>
        public void Configure(StringDictionary attributes)
        {
            if (!GetKeyValue(attributes, LibraryFileAttribute, ref libraryFileArgument))
            {
                throw new ArgumentNullException(LibraryFileAttribute,
                    string.Format(Resources.RequiredAttributeNotPresent, LibraryFileAttribute));
            }
            GetKeyValue(attributes, LibraryFolderAttribute, ref libFolderArgument);
        }

        #endregion

        private bool GetKeyValue(StringDictionary attributes, string key, ref string value)
        {
            if(attributes.ContainsKey(key))
            {
                value = attributes[key];
                return true;
            }
            return false;
        }
    }
}
