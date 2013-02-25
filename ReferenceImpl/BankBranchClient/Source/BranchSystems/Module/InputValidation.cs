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
using System.Text.RegularExpressions;
using GlobalBank.BranchSystems.Properties;

namespace GlobalBank.BranchSystems.Module
{
	public class InputValidation
	{
		public static bool IsValidNumeralChar(char value)
		{
			Regex rg = new Regex(Resources.ValidNumeralChar);
			return rg.IsMatch(value.ToString());
		}

		public static bool IsValidAlphaChar(char value)
		{
			Regex rg = new Regex(Resources.ValidCharForNames);
			return rg.IsMatch(value.ToString());
		}

		public static bool IsValidGeneralChar(char value)
		{
			Regex rg = new Regex(Resources.ValidGeneralChar);
			return rg.IsMatch(value.ToString());
		}

		public static bool IsValidNumeralDashChar(char value)
		{
			Regex rg = new Regex(Resources.ValidNumeralDashChar);
			return rg.IsMatch(value.ToString());
		}

		public static bool IsValidEmailChar(char value)
		{
			Regex rg = new Regex(Resources.ValidEmailChar);
			return rg.IsMatch(value.ToString());
		}
	}
}