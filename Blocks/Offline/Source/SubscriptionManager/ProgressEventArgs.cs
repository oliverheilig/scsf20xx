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

namespace Microsoft.Practices.SmartClient.Subscriptions
{
	/// <summary>
	///		Used to provide progress reports during background synchronization.
	/// </summary>
	public class ProgressEventArgs : EventArgs
	{
		private int progress;

		/// <summary>
		///		Constructor.
		/// </summary>
		/// <param name="progress">The percent progress, from 0 to 100.</param>
		public ProgressEventArgs(int progress)
		{
			this.progress = progress;
		}

		/// <summary>
		///		Gets the percent complete, from 0 to 100.
		/// </summary>
		public int Progress
		{
			get { return progress; }
		}
	}
}
