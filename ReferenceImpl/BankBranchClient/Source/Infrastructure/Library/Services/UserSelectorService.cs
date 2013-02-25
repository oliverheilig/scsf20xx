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

namespace GlobalBank.Infrastructure.Library.Services
{
	public class UserSelectorService : IUserSelectorService
	{
		private UserData[] _users;

		public UserSelectorService()
		{
			CreateUsers();
		}

		public IUserData SelectUser()
		{
			UserSelectionForm form = new UserSelectionForm(_users);
			return form.SelectUser();
		}


		private void CreateUsers()
		{
			_users = new UserData[3];
			_users[0] = new UserData();
			_users[1] = new UserData();
			_users[2] = new UserData();

			_users[0].Name = "Jerry";
			_users[0].Roles = new string[] { "Greeter" };
			_users[0].Password = "Password1";

			_users[1].Name = "Tom";
			_users[1].Roles = new string[] { "Officer" };
			_users[1].Password = "Password2";

			_users[2].Name = "Spike";
			_users[2].Roles = new string[] { "BranchManager" };
			_users[2].Password = "Password3";
		}
	}
}
