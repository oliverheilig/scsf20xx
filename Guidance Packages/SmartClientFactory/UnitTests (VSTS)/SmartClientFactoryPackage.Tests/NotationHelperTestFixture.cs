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
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartClientFactoryPackage.Tests
{
	/// <summary>
	/// Summary description for NotationHelperTestFixture
	/// </summary>
	[TestClass]
	public class NotationHelperTestFixture
	{
		public NotationHelperTestFixture()
		{
		}

		[TestMethod]
		public void ShouldNotChangeStringWhenClassIsNotGeneric()
		{
			string originalClass = typeof (String).AssemblyQualifiedName;
			string translatedClass = NotationHelper.ParseClrNotationToGenericName(originalClass);

			Assert.AreEqual(originalClass, translatedClass);
		}

		[TestMethod]
		public void ShouldChangeStringWhenClassIsGeneric()
		{
			string originalClass = typeof (GenericClass<string>).AssemblyQualifiedName;
			string translatedClass = NotationHelper.ParseClrNotationToGenericName(originalClass);

			Assert.AreNotEqual(originalClass, translatedClass);
			Assert.IsTrue(translatedClass.StartsWith("SmartClientFactoryPackage.Tests.GenericClass<String>"));
		}

		[TestMethod]
		public void ShouldChangeStringToNamedArgumentWhenClassIsGeneric()
		{
			string originalClass = typeof (GenericClass<>).AssemblyQualifiedName;
			string translatedClass = NotationHelper.ParseClrNotationToGenericName(originalClass);

			Assert.AreNotEqual(originalClass, translatedClass);
			Assert.IsTrue(translatedClass.StartsWith("SmartClientFactoryPackage.Tests.GenericClass<T>"));
		}

		[TestMethod]
		public void ShouldChangeStringToNamedArgumentWhenClassIsGenericWith2GenericParameters()
		{
			string originalClass = typeof (GenericClass2<,>).AssemblyQualifiedName;
			string translatedClass = NotationHelper.ParseClrNotationToGenericName(originalClass);

			Assert.AreNotEqual(originalClass, translatedClass);
			Assert.IsTrue(translatedClass.StartsWith("SmartClientFactoryPackage.Tests.GenericClass2<T,G>"));
		}

		[TestMethod]
		public void ShouldChangeStringToNamedArgumentWhenClassIsGenericWith11GenericParameters()
		{
			string originalClass = typeof (GenericClass11<,,,,,,,,,,>).AssemblyQualifiedName;
			string translatedClass = NotationHelper.ParseClrNotationToGenericName(originalClass);

			Assert.AreNotEqual(originalClass, translatedClass);
			Assert.IsTrue(translatedClass.StartsWith("SmartClientFactoryPackage.Tests.GenericClass11<A,B,C,D,E,F,G,H,I,J,K>"));
		}

		[TestMethod]
		public void ShouldChangeNamedArgumentToCLRNotation()
		{
			string genericClass = "SmartClientFactoryPackage.Tests.GenericClass2<T,G>";
			string translatedClass = NotationHelper.ParseGenericNameToCLRNotation(genericClass);

			Assert.AreNotEqual(genericClass, translatedClass);
			Assert.IsTrue(translatedClass.StartsWith("SmartClientFactoryPackage.Tests.GenericClass2`2"));
		}

		[TestMethod]
		public void ShouldNotChangeEmptyToCLRNotation()
		{
			string genericClass = "SmartClientFactoryPackage.Tests.NormalClass";
			string translatedClass = NotationHelper.ParseGenericNameToCLRNotation(genericClass);

			Assert.AreEqual(genericClass, translatedClass);
			Assert.IsTrue(translatedClass.StartsWith("SmartClientFactoryPackage.Tests.NormalClass"));
		}

		[TestMethod]
		public void ShouldChangeNestedGenericsEmptyToCLRNotation()
		{
			string genericClass =
				"SmartClientFactoryPackage.Tests.GenericClass2<SmartClientFactoryPackage.Tests.GenericClass<T>,G>";
			string translatedClass = NotationHelper.ParseGenericNameToCLRNotation(genericClass);

			Assert.AreNotEqual(genericClass, translatedClass);
			Assert.IsTrue(translatedClass.StartsWith("SmartClientFactoryPackage.Tests.GenericClass2`2"));
		}

		[TestMethod]
		public void ShouldRemoveCLRNotationCharacters()
		{
			string clrNotation = typeof (GenericClass<>).FullName;

			Assert.AreEqual("SmartClientFactoryPackage.Tests.GenericClass", NotationHelper.RemoveTrailingCLRChars(clrNotation));
		}
	}

	public class NonGenericClass
	{
	}

	public class GenericClass<T>
	{
	}

	public class GenericClass2<T, G>
	{
	}

	public class GenericClass11<A, B, C, D, E, F, G, H, I, J, K>
	{
	}
}