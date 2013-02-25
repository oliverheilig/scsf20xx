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
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

namespace GlobalBankServices
{
	[GeneratedCode("wsdl", "2.0.50727.42")]
	[WebServiceBinding(Name = "BasicHttpBinding_IAlertsService", Namespace = "http://tempuri.org/")]
	public interface IBasicHttpBinding_IAlertsService
	{
		/// <remarks/>
		[WebMethod()]
		[SoapDocumentMethod("GetCustomerAlerts", RequestNamespace = "http://GlobalBranchServicesServiceContracts/2006/04/IAlertsService", ResponseNamespace = "http://GlobalBranchServicesServiceContracts/2006/04/IAlertsService", Use = SoapBindingUse.Literal, ParameterStyle = SoapParameterStyle.Wrapped)]
		[return: XmlElement(IsNullable = true)]
		Alert[] GetCustomerAlerts([XmlElement(IsNullable = true)] Customer exampleCustomer);
	}
}