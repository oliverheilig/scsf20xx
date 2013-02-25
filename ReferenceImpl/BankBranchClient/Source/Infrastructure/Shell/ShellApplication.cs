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
using System.Windows.Forms;
using GlobalBank.Infrastructure.Library;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Library.Services;

namespace GlobalBank.Infrastructure.Shell
{
	/// <summary>
	/// Main application entry point class.
	/// Note that the class derives from CAB supplied base class FormShellApplication, and the 
	/// main form will be ShellForm, also created by default by this solution template
	/// </summary>
	public class ShellApplication : SmartClientApplication<WorkItem, ShellForm>
	{
		/// <summary>
		/// Application entry point.
		/// </summary>
		[STAThread]
		static void Main()
		{
#if (DEBUG)
			RunInDebugMode();
#else
			RunInReleaseMode();
#endif
		}

		private static void RunInDebugMode()
		{
			Application.SetCompatibleTextRenderingDefault(false);
			new ShellApplication().Run();
		}

		private static void RunInReleaseMode()
		{
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomainUnhandledException);
			Application.SetCompatibleTextRenderingDefault(false);
			try
			{
				new ShellApplication().Run();
			}
			catch (Exception ex)
			{
				HandleException(ex);
			}
		}

		private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			HandleException(e.ExceptionObject as Exception);
		}

		private static void HandleException(Exception ex)
		{
			if (ex == null)
				return;
			
			ShellNotificationService notifications = new ShellNotificationService();

			if (ExceptionPolicy.HandleException(ex, "Default Policy"))
					notifications.Show(Properties.Resources.UnhandledExceptionMessage, Properties.Resources.UnhandledExceptionTitle,
									MessageBoxButtons.OK, MessageBoxIcon.Error);					

			Environment.Exit(0);
		}		
	}
}