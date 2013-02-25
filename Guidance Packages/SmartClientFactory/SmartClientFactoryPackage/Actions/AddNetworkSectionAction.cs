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
using System.Text;
using System.Collections.Generic;
using Microsoft.Practices.RecipeFramework;
using Microsoft.Practices.SmartClientFactory.Properties;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    public sealed class AddNetworkSectionAction : Microsoft.Practices.RecipeFramework.Action
	{
		#region Input properties

		[Input(Required = true)]
		public XmlDocument XmlDoc
		{
			get { return xmlDoc; }
			set { xmlDoc = value; }
		} XmlDocument xmlDoc;

		#endregion

		#region Overrides

		public override void Execute()
		{
            AddNetworkSection();
		}

        private void AddNetworkSection()
        {
            XmlNode connectionMonitorSection = GetOrCreateConfigurationSection("ConnectionMonitor", Resources.ConnectionSettingsSectionType);
            XmlNode internetNetwork = connectionMonitorSection.SelectSingleNode("//Networks/add[@Name='Internet']");
            if (internetNetwork == null)
            {
                XmlNode newNetworkSection = XmlDoc.CreateElement("add");

                XmlAttribute xmlNetworkNameAttrib = XmlDoc.CreateAttribute("Name");
                xmlNetworkNameAttrib.Value = Resources.NetworkName;
                newNetworkSection.Attributes.Append(xmlNetworkNameAttrib);

                XmlAttribute xmlNetworkAddressAttrib = XmlDoc.CreateAttribute("Address");
                xmlNetworkAddressAttrib.Value = Resources.NetworkAddress;
                newNetworkSection.Attributes.Append(xmlNetworkAddressAttrib);

                XmlNode networksElement = GetOrCreateElement(connectionMonitorSection, "Networks");
                networksElement.AppendChild(newNetworkSection);
            }
        }

        private XmlNode GetOrCreateElement(XmlNode parent, string elementName)
        {
            XmlNode networks = parent.SelectSingleNode("//Networks");
            if (networks == null)
            {
                networks = XmlDoc.CreateElement("Networks");
                parent.AppendChild(networks);
            }
            return networks;
        }

        private XmlNode GetOrCreateConfigurationSection(string sectionName, string sectionType)
        {
            XmlNode section = this.XmlDoc.SelectSingleNode("//configuration/configSections/section[@name='" + sectionName + "']");
            if (section == null)
            {
                XmlNode newSection = XmlDoc.CreateElement("section");

                XmlAttribute xmlNameAttrib = XmlDoc.CreateAttribute("name");
                xmlNameAttrib.Value = sectionName;
                newSection.Attributes.Append(xmlNameAttrib);

                XmlAttribute xmlTypeAttrib = XmlDoc.CreateAttribute("type");
                xmlTypeAttrib.Value = sectionType;
                newSection.Attributes.Append(xmlTypeAttrib);

                XmlNode sections = this.XmlDoc.SelectSingleNode("//configuration/configSections");
                sections.AppendChild(newSection);
            }

            XmlNode configurationSection = this.XmlDoc.SelectSingleNode("//configuration/" + sectionName);
            if (configurationSection == null)
            {
                configurationSection = XmlDoc.CreateElement(sectionName);
                XmlNode configuration = XmlDoc.SelectSingleNode("//configuration");
                configuration.AppendChild(configurationSection);
            }
            return configurationSection;
        }

		public override void Undo()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		#endregion
	}
}
