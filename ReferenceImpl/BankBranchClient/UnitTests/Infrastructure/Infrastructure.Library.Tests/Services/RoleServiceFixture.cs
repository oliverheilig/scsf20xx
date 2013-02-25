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
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.Services;
using GlobalBank.UnitTest.Library;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests.Services
{
	[TestClass]
	public class RoleServiceFixture
	{
		TestableRootWorkItem mockContainer;
		IUserData[] _users;

		[TestInitialize]
		public void Init()
		{
			mockContainer = new TestableRootWorkItem();
			CreateUsers();
		}


		[TestMethod]
		public void GetRolesForUserTest()
		{
			IRoleService roleService = mockContainer.Services.AddNew<SimpleRoleService, IRoleService>();
			string userName = _users[0].Name;

			string[] roles = roleService.GetRolesForUser(userName);
			Assert.AreEqual(roles.Length, _users[0].Roles.Length);
			for (int i = 0; i < roles.Length; i++)
			{
				Assert.AreEqual(roles[i], _users[0].Roles[i]);
			}
		}

		private void CreateUsers()
		{
			_users = new UserData[3];
			_users[0] = new UserData();
			_users[1] = new UserData();
			_users[2] = new UserData();

			_users[0].Name = "Jerry";
			_users[0].Roles = new string[] {"Greeter"};

			_users[1].Name = "Tom";
			_users[1].Roles = new string[] {"Officer"};

			_users[2].Name = "Spike";
			_users[2].Roles = new string[] {"BranchManager"};
		}
	}
}