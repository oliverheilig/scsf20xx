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
// Mobile Client Software Factory - July 2006
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
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
using System.IO;
using System.Reflection;

namespace Microsoft.Practices.TestUtilities
{
	/// <summary>
	///	<para>
	///		The Compact Framework doesn't provide this class, which is in System.Configuration
	/// </para>
	/// </summary>
	public class DirectoryUtils
	{
		public static string BaseDirectory
		{
			get
			{
				string name = Assembly.GetExecutingAssembly().GetName().CodeBase;

				// TODO (v-dacazz): the implemention should be replaced with the following 
				// line of code that works on both platforms and doesn't require 
				// string manipulations:
				//return Path.GetDirectoryName(new Uri(name).LocalPath);

				name = Path.GetDirectoryName(name);

				//
				// Support for unit testing on the desktop. CodeBase returns a path with "file:\" at the start
				// under windows, but not under Windows CE. The code that uses this property expects
				// the path without the file:\ at the start.
				//
				if (Environment.OSVersion.Platform != PlatformID.WinCE)
				{
					if (name.StartsWith(@"file:\"))
						name = name.Remove(0, 6);
				}
				return name;
			}
		}
	}
}