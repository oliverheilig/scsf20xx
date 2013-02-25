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
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DisconnectedAgent.Tests
{
	[TestClass]
	public class CallParametersSerializerFixture
	{
		[TestMethod]
		public void SerializingNullArrayReturnsEmptyString()
		{
			string xml = CallParametersSerializer.Serialize(null);
			Assert.AreEqual(String.Empty, xml);
		}

		[TestMethod]
		public void SerializingEmptyArrayReturnsEmptyString()
		{
			string xml = CallParametersSerializer.Serialize(new object[0]);
			Assert.AreEqual(String.Empty, xml);
		}

		[TestMethod]
		public void DeserializingEmptyStringReturnsEmptyArray()
		{
			object[] parameters = CallParametersSerializer.Deserialize(String.Empty);
			Assert.IsNotNull(parameters);
			Assert.AreEqual(0, parameters.Length);
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void DeserializingNullStringThrows()
		{
			object[] parameters = CallParametersSerializer.Deserialize(null);
		}

		[TestMethod]
		public void DeserializingSerializedStringRegeneratesSingleInt()
		{
			object[] parameters = new object[] {123};
			string xml = CallParametersSerializer.Serialize(parameters);
			object[] parameters2 = CallParametersSerializer.Deserialize(xml);
			Assert.IsNotNull(parameters2);
			Assert.AreEqual(1, parameters2.Length);
			Assert.AreEqual(123, parameters2[0]);
			Assert.IsTrue(typeof (int).IsAssignableFrom(parameters2[0].GetType()));
		}
	}
}