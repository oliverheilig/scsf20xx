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
    public sealed class AddEndpointSectionAction : Microsoft.Practices.RecipeFramework.Action
    {
        #region Input properties

        [Input(Required = true)]
        public XmlDocument XmlDoc
        {
            get { return xmlDoc; }
            set { xmlDoc = value; }
        } XmlDocument xmlDoc;

        [Input(Required = false)]
        public string Endpoint
        {
            get { return endpoint; }
            set { endpoint = value; }
        } string endpoint;

        [Input(Required = false)]
        public string Address
        {
            get { return address; }
            set { address = value; }
        } string address;

        #endregion

        #region Overrides

        public override void Execute()
        {
            if (!string.IsNullOrEmpty(endpoint))
            {
                AddEndpointsSection();
            }
        }

        private void AddEndpointsSection()
        {
            XmlNode endpointsSection = GetOrCreateConfigurationSection("Endpoints", Resources.EndpointsSectionType);
            XmlNode endpointItem = endpointsSection.SelectSingleNode("//EndpointItems/add[@Name='" + Endpoint + "']");
            if (endpointItem == null)
            {
                XmlNode newEndpointItem = XmlDoc.CreateElement("add");

                XmlAttribute xmlEndpointNameAttrib = XmlDoc.CreateAttribute("Name");
                xmlEndpointNameAttrib.Value = Endpoint;
                newEndpointItem.Attributes.Append(xmlEndpointNameAttrib);

                XmlAttribute xmlEndpointAddressAttrib = XmlDoc.CreateAttribute("Address");
                xmlEndpointAddressAttrib.Value = Address;
                newEndpointItem.Attributes.Append(xmlEndpointAddressAttrib);

                XmlNode endpointItemsElement = GetOrCreateElement(endpointsSection, "EndpointItems");
                endpointItemsElement.AppendChild(newEndpointItem);
            }
            else
            {
                XmlAttribute addressAttib = endpointItem.Attributes["Address"];
                if (addressAttib == null)
                {
                    addressAttib = XmlDoc.CreateAttribute("Address");
                    endpointItem.Attributes.Append(addressAttib);
                }
                addressAttib.Value = Address;
            }
        }

        private XmlNode GetOrCreateElement(XmlNode parent, string elementName)
        {
            XmlNode networks = parent.SelectSingleNode(string.Format("//{0}",elementName));
            if (networks == null)
            {
                networks = XmlDoc.CreateElement(elementName);
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
