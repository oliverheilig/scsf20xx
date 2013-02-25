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
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace QuickStart.RestaurantService.Host.DataContracts
{
    [DataContract]
    public class MenuItem
    {
        protected string identifier;
        protected string number;
        protected string name;
        protected string description;
        protected string imageLocation;
        protected decimal price;
        protected int quantity;
        protected int preparationTime;

        [DataMember]
        public string Identifier
        {
            get { return this.identifier; }
            set { this.identifier = value; }
        }

        [DataMember]
        public string Number
        {
            get { return this.number; }
            set { this.number = value; }
        }

        [DataMember]
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        [DataMember]
        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        [DataMember]
        public string ImageLocation
        {
            get { return this.imageLocation; }
            set { this.imageLocation = value; }
        }

        [DataMember]
        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        [DataMember]
        public int Quantity
        {
            get { return this.quantity; }
            set { this.quantity = value; }
        }

        [DataMember]
        public int PreparationTime
        {
            get { return this.preparationTime; }
            set { this.preparationTime = value; }
        }
    }
}
