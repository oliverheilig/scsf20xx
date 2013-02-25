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
using System.Globalization;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	///		Serializes a list of parameters into an XML string.
	///		It's called for persist requests into a repository.
	/// </summary>
	public static class CallParametersSerializer
	{
		private static string nullArg = "Null";

		/// <summary>
		///		Serializes a list of parameters into an XML string.
		///		It's called for persist requests into a repository.
		/// </summary>
		/// <param name="parameters">
		///		The list of parameters that need to be serialized for sending the message.
		/// </param>
		/// <returns>string containing the serialized list of parameters.</returns>
		public static string Serialize(object[] parameters)
		{
			if (parameters == null || parameters.Length == 0)
			{
				return string.Empty;
			}

			//1) Build argument types array
			Type[] types = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i] == null)
				{
					types[i] = null;
				}
				else
				{
					types[i] = parameters[i].GetType();
				}
			}

			//2) Start building the xml
			StringWriter textWriter = new StringWriter(CultureInfo.InvariantCulture);
			XmlWriter xmlWriter = XmlWriter.Create(textWriter);
			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("CallParameters");

			//3) Write the types into the xml and create the xml serializers
			List<XmlSerializer> serializers = new List<XmlSerializer>();
			xmlWriter.WriteStartElement("Types");
			foreach (Type type in types)
			{
				xmlWriter.WriteStartElement("Type");
				if (type != null)
				{
					xmlWriter.WriteValue(type.AssemblyQualifiedName);
					serializers.Add(new XmlSerializer(type));
				}
				else
				{
					xmlWriter.WriteValue(nullArg);
					serializers.Add(null);
				}
				xmlWriter.WriteEndElement(); //Type
			}
			xmlWriter.WriteEndElement(); //Types

			//4) Serialize the arguments
			xmlWriter.WriteStartElement("Arguments");
			for (int i = 0; i < parameters.Length; i++)
			{
				xmlWriter.WriteStartElement("Argument");
				if (serializers[i] != null)
				{
					StringWriter innerWriter = new StringWriter(CultureInfo.InvariantCulture);
					serializers[i].Serialize(innerWriter, parameters[i]);
					xmlWriter.WriteValue(innerWriter.ToString());
				}
				else
				{
					xmlWriter.WriteValue(nullArg);
				}

				xmlWriter.WriteEndElement(); //Argument
			}
			xmlWriter.WriteEndElement(); //Arguments

			//5) Close
			xmlWriter.WriteEndElement(); //CallParameters
			xmlWriter.WriteEndDocument();
			xmlWriter.Close();

			//6) Return the xml string
			return textWriter.ToString();
		}

		/// <summary>
		/// Deserialize a list of parameters from a string.
		/// It's called to reconstruct the request from a persistance repository.
		/// </summary>
		/// <param name="serializedParameter">string with the serialized list of parameters.</param>
		/// <returns>object array containing the parameters.</returns>
		public static object[] Deserialize(string serializedParameter)
		{
			Guard.ArgumentNotNull(serializedParameter, "paramString");
			if (string.IsNullOrEmpty(serializedParameter)) return new object[0]; //Empty call parameters

			XmlReader reader = new XmlTextReader(new StringReader(serializedParameter));

			List<Object> argsList = new List<object>();

			reader.ReadStartElement("CallParameters");
			reader.ReadStartElement("Types");

			List<XmlSerializer> serializers = new List<XmlSerializer>();
			while (reader.IsStartElement("Type"))
			{
				string qualifiedName = reader.ReadString();
				if (qualifiedName != nullArg)
				{
					serializers.Add(new XmlSerializer(Type.GetType(qualifiedName)));
				}
				else
				{
					serializers.Add(null);
				}
				reader.ReadEndElement();
			}

			if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "Types")
			{
				reader.ReadEndElement(); //Types
			}

			reader.ReadStartElement("Arguments");
			for (int i = 0; reader.IsStartElement("Argument"); i++)
			{
				string serialized = reader.ReadString();

				if (serializers[i] != null)
				{
					argsList.Add(serializers[i].Deserialize(new StringReader(serialized)));
				}
				else
				{
					argsList.Add(null);
				}

				reader.ReadEndElement();
			}

			if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "Arguments")
			{
				reader.ReadEndElement(); //Arguments
			}

			if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "CallParameters")
			{
				reader.ReadEndElement(); //CallParameters
			}
			reader.Close();

			return argsList.ToArray();
		}
	}
}