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
using System.Security.Authentication;
using System.Threading;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.Services;
using GlobalBank.UnitTest.Library;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
	[TestClass]
	public class SimpleWinFormAuthenticationServiceFixture
	{
		private TestableRootWorkItem workItem;
		private MockUserSelector selector;
		private SimpleWinFormAuthenticationService service;

		[TestInitialize]
		public void Initialize()
		{
			workItem = new TestableRootWorkItem();
			selector = workItem.Services.AddNew<MockUserSelector, IUserSelectorService>();
			service = workItem.Services.AddNew<SimpleWinFormAuthenticationService, IAuthenticationService>();
		}

		[TestMethod]
		public void ThreadIdentityIsSetToSelectedUser()
		{
			selector.Return = selector.Users[1];

			service.Authenticate();

			Assert.AreEqual("User2", Thread.CurrentPrincipal.Identity.Name);
		}

		[TestMethod]
		[ExpectedException(typeof (AuthenticationException))]
		public void FailsIfNoUserIsSelected()
		{
			service.Authenticate();
		}
	}

	class MockUserSelector : IUserSelectorService
	{
		public IUserData[] Users;
		public IUserData Return = null;

		public MockUserSelector()
		{
			Users = new UserData[2];
			Users[0] = new UserData();
			Users[1] = new UserData();

			Users[0].Name = "User1";
			Users[0].Roles = new string[] {"Role1"};
			Users[1].Name = "User2";
			Users[1].Roles = new string[] {"Role2"};
		}

		public IUserData SelectUser()
		{
			return Return;
		}
	}
}