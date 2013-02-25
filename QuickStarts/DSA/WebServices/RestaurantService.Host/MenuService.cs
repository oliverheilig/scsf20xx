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
using System.ServiceModel;
using QuickStart.RestaurantService.Host.DataContracts;
using System.Collections.Generic;

namespace QuickStart.RestaurantService.Host
{
    public class MenuService : IMenuService
    {
        public MenuItem[] GetMenuItems(string restaurantId)
        {
            List<MenuItem> menuItems = new List<MenuItem>(10);
            Random r = new Random();
            for (int i = 1; i <= 10; i++)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Name = menuItem.Description = "Menu Item " + i.ToString();
                menuItem.Identifier = "r" + i.ToString();
                menuItem.Price = (decimal)(r.NextDouble() * 10);
                menuItems.Add(menuItem);
            }
            return menuItems.ToArray();
        }

        public Restaurant[] GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>(10);
            for (int i = 1; i <= 10; i++)
            {
                Restaurant restaurant = new Restaurant();
                restaurant.Name = restaurant.Description = "Restaurant " + i.ToString();
                restaurant.Identifier = "r" + i.ToString();
                restaurants.Add(restaurant);
            }
            return restaurants.ToArray();
        }
    }
}

