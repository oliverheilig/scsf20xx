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
using QuickStart.Infrastructure.Interface;

namespace QuickStart.RestaurantModule.DSA.MenuServiceClient
{
	// Generated code for the web service.
	// Use this proxy to make requests to the service when working in an application that is occasionally connected
	public class Callback : CallbackBase
	{
		public static event EventHandler<EventArgs<Restaurant[]>> GetRestaurantsReturn;
		public static event EventHandler<EventArgs<Exception>> GetRestaurantsException;
		public static event EventHandler<EventArgs<MenuItem[]>> GetMenuItemsReturn;
		public static event EventHandler<EventArgs<Exception>> GetMenuItemsException;

		#region GetMenuItems

		public override void OnGetMenuItemsReturn(Request request, object[] parameters, MenuItem[] returnValue)
		{
			if (GetMenuItemsReturn != null)
			{
				GetMenuItemsReturn(this, new EventArgs<MenuItem[]>(returnValue));
			}
		}

		public override OnExceptionAction OnGetMenuItemsException(Request request, Exception ex)
		{
			if (GetMenuItemsException != null)
			{
				GetMenuItemsException(this, new EventArgs<Exception>(ex));
			}
			return OnExceptionAction.Retry;
		}

		#endregion GetMenuItems

		#region GetRestaurants

		public override void OnGetRestaurantsReturn(Request request, object[] parameters, Restaurant[] returnValue)
		{
			if (GetRestaurantsReturn != null)
			{
				GetRestaurantsReturn(this, new EventArgs<Restaurant[]>(returnValue));
			}
		}

		public override OnExceptionAction OnGetRestaurantsException(Request request, Exception ex)
		{
			if (GetRestaurantsException != null)
			{
				GetRestaurantsException(this, new EventArgs<Exception>(ex));
			}
			return OnExceptionAction.Retry;
		}

		#endregion GetRestaurants

	}
}
