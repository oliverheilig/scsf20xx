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
using System.Text;
using System.Windows.Forms;
using GlobalBank.Infrastructure.Interface.Constants;
using Microsoft.Practices.ObjectBuilder;

namespace GlobalBank.BranchSystems.Layout
{
	public partial class LayoutView : UserControl
	{
		private LayoutViewPresenter _presenter;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ShellLayoutView"/> class.
		/// </summary>
		public LayoutView()
		{
			InitializeComponent();

			_launchBarWorkspace.Name = WorkspaceNames.LaunchBarWorkspace;
            _branchSystemsWorkspace.Name = WorkspaceNames.BranchSystemsWorkspace;
		}

		/// <summary>
		/// Sets the presenter.
		/// </summary>
		/// <value>The presenter.</value>
		[CreateNew]
		public LayoutViewPresenter Presenter
		{
			set
			{
				_presenter = value;
				_presenter.View = this;
			}
		}

		/// <summary>
		/// Gets the main menu strip.
		/// </summary>
		/// <value>The main menu strip.</value>
		internal MenuStrip MainMenuStrip
		{
			get { return _mainMenuStrip; }
		}

		/// <summary>
		/// Gets the main status strip.
		/// </summary>
		/// <value>The main status strip.</value>
		internal StatusStrip MainStatusStrip
		{
			get { return _mainStatusStrip; }
		}		

		/// <summary>
		/// Close the application.
		/// </summary>
		private void OnFileExit(object sender, EventArgs e)
		{
			_presenter.OnFileExit();
		}

		/// <summary>
		/// Sets the status label.
		/// </summary>
		/// <param name="text">The text.</param>
		public void SetStatusLabel(string text)
		{
			_statusLabel.Text = text;
		}

        public void SetUserPrincipalStatusLabel( string userName, string[] roles )
        {
            StringBuilder sb = new StringBuilder();
            foreach( string role in roles ) {
                sb.AppendFormat("{0}, ", role);
            }
            if (sb.Length > 0)
                sb.Remove(sb.Length - 2, 2);


            _userLabel.Text = userName;
            _rolesLabel.Text = sb.ToString();
        }
    }
}
