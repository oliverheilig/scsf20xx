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
//===============================================================================
// Microsoft patterns & practices
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Practices.CompositeUI.Utility.Tests
{
	[TestClass]
	public class GuardFixture
	{
		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringNotNullThrowsWithNullString()
		{
			Guard.ArgumentNotNull(null, "Foo");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringNotNullOrEmptyThrowsWithNullString()
		{
			Guard.ArgumentNotNullOrEmptyString(null, "Foo");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void StringNotNullOrEmptyThrowsWithEmptyString()
		{
			Guard.ArgumentNotNullOrEmptyString("", "Foo");
		}

		[TestMethod]
		public void StringNotNullOrEmptyDoesNotThrowWithValidString()
		{
			Guard.ArgumentNotNullOrEmptyString("Foo", "Foo");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void EnumValueIsDefinedThrowIfValueIsUndefined()
		{
			Guard.EnumValueIsDefined(typeof(TestEnum), 2, "argument");
		}

		[TestMethod]
		public void EnumValueIsDefinedDoesNotThrowIfValueIsDefined()
		{
			Guard.EnumValueIsDefined(typeof(TestEnum), 0, "argument");
			Guard.EnumValueIsDefined(typeof(TestEnum), 1, "argument");
			Guard.EnumValueIsDefined(typeof(TestEnum), TestEnum.value1, "argument");
			Guard.EnumValueIsDefined(typeof(TestEnum), TestEnum.value2, "argument");
		}

		[TestMethod]
		public void AssignableTypesDoNotThrow()
		{
			Guard.TypeIsAssignableFromType(typeof(string), typeof(object), "argument");
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void NonAssignableTypesThrow()
		{
			Guard.TypeIsAssignableFromType(typeof(object), typeof(string), "argument");
		}

		enum TestEnum
		{
			value1,
			value2
		}
	}
}
