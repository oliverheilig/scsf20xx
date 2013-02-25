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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace GlobalBank.Infrastructure.Library
{
	internal static class XmlValidationHelper
	{
		public static TRootElement DeserializeXml<TRootElement>(string xml, string xsdResourceName, string schemaUri)
		{
			XmlSerializer serializer = new XmlSerializer(typeof (TRootElement));

			using (XmlReader reader = GetValidatingReader(xml, xsdResourceName, schemaUri))
				return (TRootElement) serializer.Deserialize(reader);
		}

		public static XmlReader GetValidatingReader(string xml, string xsdResourceName, string schemaUri)
		{
			Stream stream =
				Assembly.GetExecutingAssembly().GetManifestResourceStream(
					String.Format(CultureInfo.CurrentCulture, "{1}.{0}", xsdResourceName, typeof (XmlValidationHelper).Namespace));
			XmlTextReader schemaReader = new XmlTextReader(stream);
			stream.Dispose();

			XmlReaderSettings settings = new XmlReaderSettings();
			settings.Schemas.Add(schemaUri, schemaReader);
			StringReader xmlStringReader = new StringReader(xml);
			XmlReader catalogReader = XmlReader.Create(xmlStringReader, settings);

			return catalogReader;
		}
	}
}