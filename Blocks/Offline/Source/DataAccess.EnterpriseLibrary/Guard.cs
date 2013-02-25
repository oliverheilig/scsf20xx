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
using Microsoft.Practices.SmartClient.EnterpriseLibrary.Properties;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary
{
	/// <summary>
	/// Common guard clauses
	/// </summary>
	internal static class Guard
	{
		/// <summary>
		/// Checks an argument to ensure it isn't null
		/// </summary>
		/// <param name="argumentValue">The argument value to check.</param>
		/// <param name="argumentName">The name of the argument.</param>
		public static void ArgumentNotNull(object argumentValue, string argumentName)
		{
			if (argumentValue == null)
				throw new ArgumentNullException(argumentName);
		}

		/// <summary>
		/// Checks a string argument to ensure it isn't null or empty
		/// </summary>
		/// <param name="argumentValue">The argument value to check.</param>
		/// <param name="argumentName">The name of the argument.</param>
		public static void ArgumentNotNullOrEmptyString(string argumentValue, string argumentName)
		{
			ArgumentNotNull(argumentValue, argumentName);

			if (argumentValue.Trim().Length == 0)
			{
				throw new ArgumentException(Resources.StringCannotBeEmpty, argumentName);
			}
		}
	}
}