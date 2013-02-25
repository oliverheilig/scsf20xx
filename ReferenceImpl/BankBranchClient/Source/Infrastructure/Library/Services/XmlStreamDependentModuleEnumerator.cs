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
using System.Xml;
using GlobalBank.Infrastructure.Library.Properties;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Configuration;
using Microsoft.Practices.CompositeUI.Services;

namespace GlobalBank.Infrastructure.Library.Services
{
	/// <summary>
	/// This implementation of IModuleEnumerator processes the assemblies specified
	/// in a solution profile.
	/// </summary>
	public class XmlStreamDependentModuleEnumerator : IModuleEnumerator
	{
		private IModuleInfoStore _moduleInfoStore;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:XmlStreamDependentModuleEnumerator"/> class.
		/// </summary>
		public XmlStreamDependentModuleEnumerator()
		{
		}

		[ServiceDependency]
		public IModuleInfoStore ModuleInfoStore
		{
			get { return _moduleInfoStore; }
			set { _moduleInfoStore = value; }
		}

		/// <summary>
		/// Gets an array of <see cref="T:Microsoft.Practices.CompositeUI.Configuration.IModuleInfo"/>
		/// enumerated from the source the enumerator is processing.
		/// </summary>
		/// <returns>
		/// An array of <see cref="T:Microsoft.Practices.CompositeUI.Configuration.IModuleInfo"/> instances.
		/// </returns>
		public IModuleInfo[] EnumerateModules()
		{
			string xml = _moduleInfoStore.GetModuleListXml();

			if (String.IsNullOrEmpty(xml))
				return new DependentModuleInfo[0];

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			switch (doc.FirstChild.NamespaceURI)
			{
				case SolutionProfileV1Parser.Namespace:
					return new SolutionProfileV1Parser().Parse(xml);

				case SolutionProfileV2Parser.Namespace:
					return new SolutionProfileV2Parser().Parse(xml);

				default:
					throw new InvalidOperationException(Resources.InvalidSolutionProfileSchema);
			}
		}
	}
}