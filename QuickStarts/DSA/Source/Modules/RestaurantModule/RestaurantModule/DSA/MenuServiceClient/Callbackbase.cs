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
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using QuickStart.RestaurantModule.RestaurantService;
using System;

namespace QuickStart.RestaurantModule.DSA.MenuServiceClient
{
	// Generated code for the web service.
	// Use this proxy to make requests to the service when working in an application that is occasionally connected
	public abstract class CallbackBase
	{
		#region GetMenuItems

		public abstract void OnGetMenuItemsReturn(Request request, object[] parameters, MenuItem[] returnValue);

		public abstract OnExceptionAction OnGetMenuItemsException(Request request, Exception ex);

		#endregion GetMenuItems

		#region GetRestaurants

		public abstract void OnGetRestaurantsReturn(Request request, object[] parameters, Restaurant[] returnValue);

		public abstract OnExceptionAction OnGetRestaurantsException(Request request, Exception ex);

		#endregion GetRestaurants

	}
}
