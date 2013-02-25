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
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing.Design;
using Microsoft.Practices.SmartClientFactory.Converters;
using Microsoft.Practices.SmartClientFactory.Editors;
using System.Collections.ObjectModel;

namespace Microsoft.Practices.SmartClientFactory
{
	public class MethodBehaviors : List<MethodBehavior>
	{
	}

	public class MethodBehavior
	{
		public MethodBehavior()
		{
		}

		public MethodBehavior(MethodInfo method, bool isOverride)
		{
			this.method = method;
			this.isOverride = isOverride;
		}

		private bool isOverride = true;

		public bool IsOverride
		{
			get { return isOverride; }
			set { isOverride = value; }
		}

		private MethodInfo method;

        [TypeConverter(typeof(ProxyMethodsConverter))]
		public MethodInfo Method
		{
			get { return method; }
			set { method = value; }
		}

		private int? stamps;

		public int? Stamps
		{
			get { return stamps; }
			set { stamps = value; }
		}

		private int? maxRetries;

		public int? MaxRetries
		{
			get { return maxRetries; }
			set { maxRetries = value; }
		}

		private string tag;

		public string Tag
		{
			get { return tag; }
			set { tag = value; }
		}

		private TimeSpan? expiration;

        [Editor(typeof(TimeSpanEditor), typeof(UITypeEditor))]
		public TimeSpan? Expiration
		{
			get { return expiration; }
			set { expiration = value; }
		}
	}
}
