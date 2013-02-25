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
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
	[TestClass]
	public class EntityTranslatorServiceFixture
	{
		private EntityTranslatorService service;

		[TestInitialize]
		public void Initialize()
		{
			service = new EntityTranslatorService();
		}

		[TestMethod]
		public void CannotTranslateWhenNoTranslatorsAreRegistered()
		{
			Assert.IsFalse(service.CanTranslate(typeof (object), typeof (object)));
		}

		[TestMethod]
		public void CanTranslateWhenAppropiateTranslatorRegistered()
		{
			service.RegisterEntityTranslator(new MockTranslator(true));

			Assert.IsTrue(service.CanTranslate(typeof (object), typeof (object)));
		}

		[TestMethod]
		public void CannotTranslateWhenTranslatorIsRemoved()
		{
			MockTranslator translator = new MockTranslator(true);
			service.RegisterEntityTranslator(translator);
			service.RemoveEntityTranslator(translator);

			Assert.IsFalse(service.CanTranslate(typeof (object), typeof (object)));
		}

		[TestMethod]
		public void CannotTranslateIfNoAppropiateTranslatorRegistered()
		{
			service.RemoveEntityTranslator(new MockTranslator(false));
			Assert.IsFalse(service.CanTranslate(typeof (object), typeof (object)));
		}

		[TestMethod]
		public void DoesNotFailsWhenRemovingUnregisteredTranslator()
		{
			MockTranslator translator = new MockTranslator(true);
			service.RemoveEntityTranslator(translator);
		}

		[TestMethod]
		public void GenericCanTranslate()
		{
			Assert.IsFalse(service.CanTranslate<object, object>());
			service.RegisterEntityTranslator(new MockTranslator(true));
			Assert.IsTrue(service.CanTranslate<object, object>());
		}

		[TestMethod]
		[ExpectedException(typeof (EntityTranslatorException))]
		public void ThrowsWhenNotTranslatorAvailable()
		{
			service.Translate(typeof (object), new object());
		}

		[TestMethod]
		public void ReturnsTranslatedObject()
		{
			MockTranslator translator = new MockTranslator(true);
			service.RegisterEntityTranslator(translator);
			Assert.AreSame(translator.ResultObject, service.Translate(typeof (object), new object()));
		}

		[TestMethod]
		public void GenericTranslate()
		{
			MockTranslator translator = new MockTranslator(true);
			service.RegisterEntityTranslator(translator);
			Assert.AreSame(translator.ResultObject, service.Translate<object>(new object()));
		}

		[TestMethod]
		public void IfCanTranslateObjectCanTranslateArrayOfObjects()
		{
			ObjectTranslator translator = new ObjectTranslator();
			service.RegisterEntityTranslator(translator);
			Assert.IsTrue(service.CanTranslate<object[], object[]>());
		}

		[TestMethod]
		public void TranslateObjectAndArrayOfObjects()
		{
			ObjectTranslator translator = new ObjectTranslator();
			service.RegisterEntityTranslator(translator);

			object[] source = new object[2] {new object(), new object()};
			object[] result = service.Translate<object[]>(source);
			Assert.AreEqual(source.Length, result.Length);
			Assert.IsNotNull(result[0]);
			Assert.IsNotNull(result[1]);
		}

		[TestMethod]
		public void SomeArrayElementsCanBeNull()
		{
			ObjectTranslator translator = new ObjectTranslator();
			service.RegisterEntityTranslator(translator);

			object[] source = new object[3] {new object(), null, new object()};
			object[] result = service.Translate<object[]>(source);
			Assert.AreEqual(source.Length, result.Length);
			Assert.IsNotNull(result[0]);
			Assert.IsNull(result[1]);
			Assert.IsNotNull(result[2]);
		}

		[TestMethod]
		public void TranslatingNullArrayReturnsNullArray()
		{
			ObjectTranslator translator = new ObjectTranslator();
			service.RegisterEntityTranslator(translator);

			object[] source = null;
			object[] result = service.Translate<object[]>(source);

			Assert.IsNull(result);
		}
	}

	class ObjectTranslator : IEntityTranslator
	{
		public bool CanTranslate(Type targetType, Type sourceType)
		{
			return targetType == typeof (object);
		}

		public bool CanTranslate<TTarget, TSource>()
		{
			return CanTranslate(typeof (TTarget), typeof (TSource));
		}

		public object Translate(IEntityTranslatorService service, Type targetType, object source)
		{
			return new object();
		}

		public TTarget Translate<TTarget>(IEntityTranslatorService service, object source)
		{
			return (TTarget) Translate(service, typeof (TTarget), source);
		}
	}

	class MockTranslator : IEntityTranslator
	{
		public bool CanTranslateResult;
		public object ResultObject = new object();

		public MockTranslator(bool canTranslateResult)
		{
			CanTranslateResult = canTranslateResult;
		}

		public bool CanTranslate(Type targetType, Type sourceType)
		{
			return CanTranslateResult;
		}

		public bool CanTranslate<TTarget, TSource>()
		{
			return CanTranslateResult;
		}

		public object Translate(IEntityTranslatorService service, Type targetType, object source)
		{
			return ResultObject;
		}

		public TTarget Translate<TTarget>(IEntityTranslatorService service, object source)
		{
			return (TTarget) ResultObject;
		}
	}
}