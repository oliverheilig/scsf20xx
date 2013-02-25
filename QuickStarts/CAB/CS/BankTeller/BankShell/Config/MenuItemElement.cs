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

using System;
using System.Configuration;
using System.Windows.Forms;

namespace BankShell
{
	class MenuItemElement : ConfigurationElement
	{
		[ConfigurationProperty("commandname", IsRequired = false)]
		public string CommandName
		{
			get
			{
				return (string)this["commandname"];
			}
			set
			{
				this["commandname"] = value;
			}
		}

		[ConfigurationProperty("key", IsRequired = false)]
		public string Key
		{
			get
			{
				return (string)this["key"];
			}
			set
			{
				this["key"] = value;
			}
		}

		[ConfigurationProperty("id", IsRequired = false, IsKey = true)]
		public int ID
		{
			get
			{
				return (int)this["id"];
			}
			set
			{
				this["id"] = value;
			}
		}

		[ConfigurationProperty("label", IsRequired = true)]
		public string Label
		{
			get
			{
				return (string)this["label"];
			}
			set
			{
				this["label"] = value;
			}
		}

		[ConfigurationProperty("site", IsRequired = true)]
		public string Site
		{
			get
			{
				return (string)this["site"];
			}
			set
			{
				this["site"] = value;
			}
		}

		[ConfigurationProperty("register", IsRequired = false)]
		public bool Register
		{
			get
			{
				return (bool)this["register"];
			}
			set
			{
				this["register"] = value;
			}
		}

		[ConfigurationProperty("registrationsite", IsRequired = false)]
		public string RegistrationSite
		{
			get
			{
				return (string)this["registrationsite"];
			}
			set
			{
				this["registrationsite"] = value;
			}
		}

		public ToolStripMenuItem ToMenuItem()
		{
			ToolStripMenuItem result = new ToolStripMenuItem();
			result.Text = Label;
			
			if (!String.IsNullOrEmpty(Key))
				result.ShortcutKeys = (Keys)Enum.Parse(typeof(Keys), Key);

			return result;
		}
	}
}
