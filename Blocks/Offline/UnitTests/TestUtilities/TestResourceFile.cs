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
using System.Threading;

namespace Microsoft.Practices.TestUtilities
{
	/// <summary>
	///		This is a helper class that helps with running tests that depend on an external
	///		file. Each time you run tests in VSTS, it creates a new directory in the results
	///		directory. It then copies the assemblies under test to this directory and runs
	///		them from this results directory. This class helps get other files that you need
	///		over to the correct folder.
	/// </summary>
	/// <remarks>
	///		In order to use this class, you need to include the extra files in your project and
	///		then mark them as an embedded resource.
	/// </remarks>
	public class TestResourceFile : IDisposable
	{
		private string filename;
		private static byte[] buffer = new byte[8000];

		public TestResourceFile(Type relativeType, string resourceName)
			: this(relativeType, resourceName, true, resourceName)
		{
		}

		public TestResourceFile(object relativeObject, string resourceName)
			: this(relativeObject.GetType(), resourceName)
		{
		}

		public TestResourceFile(object ns, string resourceName, bool useResourcePath)
			: this(ns.GetType(), resourceName, useResourcePath, resourceName)
		{
		}

		public TestResourceFile(object ns, string resourceName, string outputFileName)
			: this(ns.GetType(), resourceName, true, outputFileName)
		{
		}

		public TestResourceFile(Type relativeType, string resourceName, bool useResourcePath, string outputFileName)
		{
			//
			// Rewrote this test so it uses the base directory calculated by our new class, which
			// has the correct behavior for both the desktop and the CF.
			//
			if (useResourcePath)
			{
				filename = Path.Combine(DirectoryUtils.BaseDirectory, outputFileName);
				string dirName = Path.GetDirectoryName(filename);
				Directory.CreateDirectory(dirName);
			}
			else
				filename = Path.Combine(DirectoryUtils.BaseDirectory, Path.GetFileName(outputFileName));

			//
			// This next line helps with debugging resources.
			//
			//string[] resource = relativeType.Assembly.GetManifestResourceNames();

			resourceName = relativeType.Namespace + "." + resourceName.Replace('\\', '.');

			using (Stream resStream = relativeType.Assembly.GetManifestResourceStream(resourceName))
			{
				if (resStream == null)
				{
					throw new ArgumentException("Could not load resource with name " + resourceName);
				}
				using (FileStream outStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.Write))
				{
                    outStream.Flush();
					while (true)
					{
						int read = resStream.Read(buffer, 0, buffer.Length);
						if (read == 0) break;
						outStream.Write(buffer, 0, read);
					}
				}
			}
		}

		public string Filename
		{
			get { return filename; }
		}

		public void Dispose()
		{
			try
			{				
                //File.Delete(filename);
                //Thread.Sleep(1000);
			}
			catch
			{
			}
		}
	}
}