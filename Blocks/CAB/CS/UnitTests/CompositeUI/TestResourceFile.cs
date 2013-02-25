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
using System.IO;
using System.Reflection;

namespace Microsoft.Practices.CompositeUI.Tests
{
	internal class TestResourceFile : IDisposable
	{
		string filename;
		static byte[] buffer = new byte[8000];

		public TestResourceFile(string resourceName)
		{
			filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, resourceName);
			Directory.CreateDirectory(Path.GetDirectoryName(filename));
			resourceName = GetType().Namespace + "." + resourceName.Replace('\\', '.');

			using (Stream resStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			using (FileStream outStream = File.Open(filename, FileMode.Create, FileAccess.Write))
			{
				while (true)
				{
					int read = resStream.Read(buffer, 0, buffer.Length);
					if (read == 0) break;
					outStream.Write(buffer, 0, read);
				}
			}
		}

		public void Dispose()
		{
			try
			{
				File.Delete(filename);
			}
			catch { }
		}
	}


}
