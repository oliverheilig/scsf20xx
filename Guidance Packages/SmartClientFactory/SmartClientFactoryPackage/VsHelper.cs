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
using System.Globalization;
using System.ComponentModel;
using EnvDTE;
using System.IO;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell.Interop;

namespace Microsoft.Practices.SmartClientFactory
{
	internal static class VsHelper
	{
		private const uint ROOT = 0xFFFFFFFE;

		public static Guid GetProjectGuid(Project project)
		{
			if (project != null)
			{
				IVsHierarchy vsHier = DteConverter.ToHierarchy(project);
				Guid projectGuid = Guid.Empty;
				vsHier.GetGuidProperty(ROOT, (int)__VSHPROPID.VSHPROPID_ProjectIDGuid, out projectGuid);
				return projectGuid;
			}

			return Guid.Empty;
		}

		public static Project GetProjectFromGuid(DTE dte, Guid guid)
		{
			return FindProject(dte, delegate(Project project)
			{
				return guid == GetProjectGuid(project);
			});
		}

		public static Project FindProject(DTE dte, Predicate<Project> match)
		{
			Project result = null;

			ForEachProject(dte, delegate(Project project)
			{
				if (match(project))
				{
					result = project;
					return true;
				}

				return false;
			});

			return result;
		}

		public static void ForEachProject(DTE vs, Predicate<Project> processAndBreak)
		{
			try
			{
				foreach (Project project in ((Projects)vs.GetObject("CSharpProjects")))
				{
					if (processAndBreak(project)) return;
				}
			}
			catch (Exception)
			{
			}
			try
			{
				foreach (Project project in ((Projects)vs.GetObject("VBProjects")))
				{
					if (processAndBreak(project)) return;
				}
			}
			catch (Exception)
			{
			}
		}

		/// <summary>
		/// Checks the out file if under source control.
		/// </summary>
		/// <param name="vs">The visual studio automation object.</param>
		/// <param name="filePath">The file path.</param>
		public static void EnsureWriteable(DTE vs, string filePath)
		{
			if (File.Exists(filePath))
			{
				if (vs.SourceControl.IsItemUnderSCC(filePath) &&
					!vs.SourceControl.IsItemCheckedOut(filePath))
				{
					bool checkout = vs.SourceControl.CheckOutItem(filePath);
					if (!checkout)
					{
						throw new CheckoutException(string.Format(Properties.Resources.CheckoutException, filePath));
					}
				}
				else
				{
					// perform an extra check if the file is read only.
					if (IsReadOnly(filePath))
					{
						ResetReadOnly(filePath);
					}
				}
			}
		}

		private static bool IsReadOnly(string path)
		{
			return (File.GetAttributes(path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
		}

		private static void ResetReadOnly(string path)
		{
			File.SetAttributes(path, File.GetAttributes(path) ^ FileAttributes.ReadOnly);
		}

		#region GetService

		public static TService GetService<TService>(IServiceProvider serviceProvider)
			where TService : class
		{
			return GetService<TService>(serviceProvider, false);
		}

		public static TService GetService<TService>(GetServiceHandler getServiceMethod)
			where TService : class
		{
			return GetService<TService>(getServiceMethod, false);
		}

		public static TService GetService<TService>(IServiceProvider serviceProvider, bool throwIfMissing)
			where TService : class
		{
			TService serviceInstance = null;
			if (serviceProvider != null)
			{
				serviceInstance = (TService)serviceProvider.GetService(typeof(TService));
			}

			ThrowIfMissing<TService>(throwIfMissing, serviceInstance);

			return serviceInstance;
		}

		public static TService GetService<TService>(GetServiceHandler getServiceMethod, bool throwIfMissing)
			where TService : class
		{
			TService serviceInstance = (TService)getServiceMethod(typeof(TService));
			ThrowIfMissing<TService>(throwIfMissing, serviceInstance);

			return serviceInstance;
		}

		private static void ThrowIfMissing<TService>(bool throwIfMissing, TService serviceInstance)
			where TService : class
		{
			if (throwIfMissing && serviceInstance == null)
			{
				throw new InvalidOperationException(String.Format(
					CultureInfo.CurrentCulture,
					Properties.Resources.ServiceMissing,
					typeof(TService)));
			}
		}

		#endregion

		internal static void DumpProperties(EnvDTE.Properties properties)
		{
			foreach (Property property in properties)
			{
				try
				{
					System.Diagnostics.Debug.WriteLine(String.Format("{0}={1}", property.Name, property.Value));
				}
				catch (Exception)
				{
				}
			}
		}
	}

	public delegate object GetServiceHandler(Type serviceType);
}
