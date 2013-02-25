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
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using GlobalBank.Infrastructure.Interface.Services;
using GlobalBank.Infrastructure.Module.Services;
using GlobalBank.UnitTest.Library;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.CompositeUI.Services;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Module.Tests.Services
{
	[TestClass]
	public class ImpersonationServiceFixture
	{
		TestableRootWorkItem mockContainer;

		[TestInitialize]
		public void Init()
		{
			mockContainer = new TestableRootWorkItem();

			GenericIdentity identity = new GenericIdentity("CurrentUser");
			GenericPrincipal principal = new GenericPrincipal(identity, new string[] {"Role1", "Role2"});
			Thread.CurrentPrincipal = principal;
		}

		[TestMethod]
		public void AuthenticationServiceIsInjectedImpersonationService()
		{
			MockAuthenticationService authService =
				mockContainer.Services.AddNew<MockAuthenticationService, IAuthenticationService>();
			IImpersonationService impersonator =
				mockContainer.Services.AddNew<GenericPrincipalImpersonationService, IImpersonationService>();

			Assert.AreEqual(impersonator.AuthenticationService, authService);
		}

		[TestMethod]
		public void ImpersonateChangesIdentity()
		{
			string currentUser = Thread.CurrentPrincipal.Identity.Name;
			string impersonatedUsername = "ImpersonatedUser";

			MockAuthenticationService authService =
				mockContainer.Services.AddNew<MockAuthenticationService, IAuthenticationService>();
			authService.Identity = impersonatedUsername;

			IImpersonationService impersonator =
				mockContainer.Services.AddNew<GenericPrincipalImpersonationService, IImpersonationService>();
			IImpersonationContext context = impersonator.Impersonate();

			// user was impersonated
			Assert.IsTrue(Thread.CurrentPrincipal.Identity.IsAuthenticated);
			Assert.AreEqual(impersonatedUsername, Thread.CurrentPrincipal.Identity.Name);

			context.Undo();

			// previous principal
			Assert.IsTrue(Thread.CurrentPrincipal.Identity.IsAuthenticated);
			Assert.AreEqual(currentUser, Thread.CurrentPrincipal.Identity.Name);
		}

		[TestMethod]
		[ExpectedException(typeof (AuthenticationException))]
		public void ImpersonateFailsIfAuthenticationFails()
		{
			string currentUser = Thread.CurrentPrincipal.Identity.Name;
			string userName = "NotExists";

			MockAuthenticationService authService =
				mockContainer.Services.AddNew<MockAuthenticationService, IAuthenticationService>();
			authService.Identity = userName;

			IImpersonationService impersonator =
				mockContainer.Services.AddNew<GenericPrincipalImpersonationService, IImpersonationService>();
			// fails
			impersonator.Impersonate();
		}

		[TestMethod]
		public void ImpersonationContextCallsUndoOnDispose()
		{
			string currentUser = Thread.CurrentPrincipal.Identity.Name;
			string impersonatedUsername = "ImpersonatedUser";

			MockAuthenticationService authService =
				mockContainer.Services.AddNew<MockAuthenticationService, IAuthenticationService>();
			authService.Identity = impersonatedUsername;

			IImpersonationService impersonator =
				mockContainer.Services.AddNew<GenericPrincipalImpersonationService, IImpersonationService>();
			using (IImpersonationContext context = impersonator.Impersonate())
			{
				Assert.IsTrue(Thread.CurrentPrincipal.Identity.IsAuthenticated);
				Assert.AreEqual(impersonatedUsername, Thread.CurrentPrincipal.Identity.Name);
			}

			Assert.IsTrue(Thread.CurrentPrincipal.Identity.IsAuthenticated);
			Assert.AreEqual(currentUser, Thread.CurrentPrincipal.Identity.Name);
		}

		[TestMethod]
		public void ImpersonationServiceSupportsWindowsPrincipalImpersonation()
		{
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);
			Thread.CurrentPrincipal = principal;

			string currentUser = Thread.CurrentPrincipal.Identity.Name;
			string impersonatedUsername = "DOMAIN\\TestUser";

			WindowsAuthenticationService authService =
				mockContainer.Services.AddNew<WindowsAuthenticationService, IAuthenticationService>();
			authService.Identity = impersonatedUsername;

			IImpersonationService impersonator =
				mockContainer.Services.AddNew<WindowsImpersonationService, IImpersonationService>();
			using (IImpersonationContext context = impersonator.Impersonate())
			{
				Assert.IsTrue(Thread.CurrentPrincipal.Identity.IsAuthenticated);
				Assert.AreEqual(impersonatedUsername, Thread.CurrentPrincipal.Identity.Name);
			}

			Assert.IsTrue(Thread.CurrentPrincipal.Identity.IsAuthenticated);
			Assert.AreEqual(currentUser, Thread.CurrentPrincipal.Identity.Name);
		}
	}

	#region Mocks

	public class MockAuthenticationService : IAuthenticationService
	{
		public string Identity = "";
		public string[] Roles;

		#region IAuthenticationService Members

		public void Authenticate()
		{
			if (Identity == "NotExists")
				throw new AuthenticationException();

			GenericIdentity identity = new GenericIdentity(Identity);
			GenericPrincipal principal = new GenericPrincipal(identity, Roles);
			Thread.CurrentPrincipal = principal;
		}

		#endregion
	}

	class WindowsImpersonationService : IImpersonationService
	{
		private WindowsAuthenticationService _authService = null;

		[InjectionConstructor]
		public WindowsImpersonationService(
			[ServiceDependency] IAuthenticationService authService)
		{
			_authService = authService as WindowsAuthenticationService;
		}

		#region IImpersonationService Members

		public IAuthenticationService AuthenticationService
		{
			get { return _authService; }
		}

		public IImpersonationContext Impersonate()
		{
			IImpersonationContext context = new WindowsImpersonationContext();
			AuthenticationService.Authenticate();

			context.State = _authService.context;

			return context;
		}

		#endregion
	}

	class WindowsImpersonationContext : IImpersonationContext
	{
		#region IImpersonationContext Members

		private System.Security.Principal.WindowsImpersonationContext _context;

		public object State
		{
			get { return _context; }
			set { _context = (System.Security.Principal.WindowsImpersonationContext) value; }
		}

		public void Undo()
		{
			_context.Undo();

			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			WindowsPrincipal principal = new WindowsPrincipal(identity);
			Thread.CurrentPrincipal = principal;
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			Dispose(true);
		}

		#endregion

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				Undo();
			}
		}
	}

	class WindowsAuthenticationService : IAuthenticationService
	{
		#region IAuthenticationService Members

		public System.Security.Principal.WindowsImpersonationContext context;

		public string Identity = "";

		public void Authenticate()
		{
			// this might use new WindowsIdentity(upn) or LogonUser
			WindowsIdentity identity = WindowsIdentity.GetCurrent();
			context = identity.Impersonate();

			// simulate a WindowsPrincipal
			GenericPrincipal principal = new GenericPrincipal(new GenericIdentity(Identity), new string[] {});
			Thread.CurrentPrincipal = principal;
		}

		#endregion
	}

	#endregion
}