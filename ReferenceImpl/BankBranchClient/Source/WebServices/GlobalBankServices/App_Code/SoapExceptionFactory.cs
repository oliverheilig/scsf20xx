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
using System.Web.Services.Protocols;
using System.Xml;

namespace GlobalBankServices
{
	internal class SoapExceptionFactory
	{
		internal static SoapException CreateSoapClientException(Exception clientException)
		{
			// Get the details node for the soap exception
			XmlNode detailsNode = GenerateSoapExceptionDetailsNode(clientException.Message, clientException.GetType().Name);

			// Create the Soap exception   
			SoapException soapEx = new SoapException(clientException.Message, SoapException.ClientFaultCode, clientException.TargetSite.Name, detailsNode);
			return (soapEx);
		}

		private static XmlNode GenerateSoapExceptionDetailsNode(String exceptionMessage, String exceptionTypeName)
		{
			// Build the detail element of the SOAP fault
			XmlDocument xmlDoc = new XmlDocument();
			XmlNode detailsNode = xmlDoc.CreateNode(XmlNodeType.Element, SoapException.DetailElementName.Name, SoapException.DetailElementName.Namespace);

			// Build specific details for the SoapException

			XmlElement exceptionDetails = xmlDoc.CreateElement(exceptionTypeName);
			XmlElement exceptionDetailsChild = xmlDoc.CreateElement("ExceptionMessage");
			exceptionDetailsChild.InnerText = exceptionMessage;
			exceptionDetails.AppendChild(exceptionDetailsChild);

			// Append the child elements to the detail node
			detailsNode.AppendChild(exceptionDetails);
			return (detailsNode);
		}
	}
}
