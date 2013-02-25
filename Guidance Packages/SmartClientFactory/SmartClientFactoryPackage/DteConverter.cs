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
using System.ComponentModel.Design;
using System.Globalization;
using System.Runtime.InteropServices;

using EnvDTE;

using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Design;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Practices.RecipeFramework.Library;
using Microsoft.Practices.SmartClientFactory.Helpers;

namespace Microsoft.Practices.SmartClientFactory
{
	public static class DteConverter
	{
		#region To Type

		/// <summary>
		/// Retrieves the <see cref="Type"/> corresponding to the first <see cref="CodeClass"/> found in the 
		/// <see cref="ProjectItem.FileCodeModel"/>.
		/// </summary>
		public static Type ToType(ProjectItem item)
		{
			Guard.ArgumentNotNull(item, "item");
			Guard.ArgumentNotNull(item.FileCodeModel, "item.FileCodeModel");

			return ToType(item.FileCodeModel);
		}

		/// <summary>
		/// Retrieves the <see cref="Type"/> corresponding to the first <see cref="CodeClass"/> found in the 
		/// <paramref name="codeModel"/>
		/// </summary>
		public static Type ToType(FileCodeModel codeModel)
		{
			Guard.ArgumentNotNull(codeModel, "codeModel");

			CodeClass cc = CodeModelHelper.FindFirstClass(codeModel.CodeElements);

			return ToType(cc);
		}

		/// <summary>
		/// Retrieves the <see cref="Type"/> corresponding to the <paramref name="codeClass"/>. 
		/// </summary>
		/// <exception cref="InvalidOperationException">An <see cref="ITypeDiscoveryService"/> cannot be 
		/// retrieved for the project the class lives in.</exception>
		/// <exception cref="InvalidOperationException">The <paramref name="codeClass"/> has not been 
		/// compiled into the project still.</exception>
		public static Type ToType(CodeClass codeClass)
		{
			Guard.ArgumentNotNull(codeClass, "codeClass");

			string typeFullName = codeClass.FullName;
			Type returnType = null;
			IServiceProvider provider = ToServiceProvider(codeClass.DTE);

			DynamicTypeService typeService = VsHelper.GetService<DynamicTypeService>(provider, true);

			IVsHierarchy hier = ToHierarchy(codeClass.ProjectItem.ContainingProject);

			ITypeResolutionService typeResolution = typeService.GetTypeResolutionService(hier);

            if(typeResolution==null)
            {
                throw new InvalidOperationException(String.Format(
                    CultureInfo.CurrentCulture,
                    Properties.Resources.ServiceMissing,
                    typeof(ITypeResolutionService)));
            }

            returnType = typeResolution.GetType(typeFullName);
			if (returnType == null)
			{
				throw new InvalidOperationException(String.Format(
					CultureInfo.CurrentCulture,
					Properties.Resources.ClassNotCompiledYet,
					typeFullName,
					codeClass.ProjectItem.get_FileNames(1)));
			}

			return returnType;
		}

		#endregion

		#region To ServiceProvider

		public static IServiceProvider ToServiceProvider(Project project)
		{
			IVsProject3 vsProject = ToVsProject(project);
			Microsoft.VisualStudio.OLE.Interop.IServiceProvider provider;
			ErrorHandler.ThrowOnFailure(vsProject.GetItemContext(VSConstants.VSITEMID_ROOT, out provider));

			return new ServiceProvider(provider);
		}

		public static IServiceProvider ToServiceProvider(DTE dte)
		{
			return ToServiceProvider((object)dte);
		}

		public static IServiceProvider ToServiceProvider(object automationObject)
		{
			return new ServiceProvider(automationObject as Microsoft.VisualStudio.OLE.Interop.IServiceProvider);
		}

		#endregion

		#region To Hierarchy

		/// <summary>
		/// Retrieves the selected hierarchy from the provider.
		/// </summary>
		public static IVsHierarchy ToSelectedHierarchy(IServiceProvider provider)
		{
			Guard.ArgumentNotNull(provider, "provider");

			uint itemId;
			IVsMonitorSelection selectionMonitor = (IVsMonitorSelection)VsHelper.GetService<SVsShellMonitorSelection>(provider, true);
			IntPtr hierarchyPointer = IntPtr.Zero;
			IVsMultiItemSelect multiItemSelect = null;
			IntPtr ppSC = IntPtr.Zero;

			ErrorHandler.ThrowOnFailure(selectionMonitor.GetCurrentSelection(out hierarchyPointer, out itemId, out multiItemSelect, out ppSC));

			return (IVsHierarchy)Marshal.GetObjectForIUnknown(hierarchyPointer);
		}

		/// <summary>
		/// Converts the project into a visual studio hierarchy object.
		/// </summary>
		public static IVsHierarchy ToHierarchy(Project project)
		{
			Guard.ArgumentNotNull(project, "project");

			IServiceProvider provider = ToServiceProvider(project.DTE);
			IVsSolution solution = (IVsSolution)VsHelper.GetService<SVsSolution>(provider, true);

			IVsHierarchy hierarchy;
			ErrorHandler.ThrowOnFailure(solution.GetProjectOfUniqueName(project.UniqueName, out hierarchy));

			return hierarchy;
		}

		#endregion

		#region To Project

		public static IVsProject3 ToVsProject(Project project)
		{
			Guard.ArgumentNotNull(project, "project");

			return ToHierarchy(project) as IVsProject3;
		}

		public static Project ToDteProject(IVsHierarchy hierarchy)
		{
			Guard.ArgumentNotNull(hierarchy, "hierarchy");

			object project = null;
			if (hierarchy.GetProperty(0xfffffffe, -2027, out project) >= 0)
			{
				return (EnvDTE.Project)project;
			}
			else
			{
				throw new ArgumentException(Properties.Resources.VsHierarchyNotProject);
			}
		}

		public static Project ToDteProject(IVsProject project)
		{
			Guard.ArgumentNotNull(project, "project");

			return ToDteProject(project as IVsHierarchy);
		}

		#endregion


	}
}
