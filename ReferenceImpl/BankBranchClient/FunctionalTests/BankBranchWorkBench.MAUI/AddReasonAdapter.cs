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
using System.Collections.Generic;
using System.Text;
using Maui.Core;
using Maui.Core.WinControls;
using Maui.Core.Utilities;
using BankBranchWorkBench.MAUI.Common;

namespace BankBranchWorkBench.MAUI
{
	public class AddReasonAdapter
	{
		#region Screen Controls
		private Window addReasonWindow;
		#endregion

		#region Lifetime
		public AddReasonAdapter()
		{
			addReasonWindow = MAUIUtilities.GetWindow(BankWorkBench.Application, ResourceNames.AddReason.WindowTitle);
		}

		public void CheckPresenceOfControls()
		{
			//TextBoxes
			//MAUIUtilities.GetControl<TextBox>(addReasonWindow,
			//         ResourceNames.AddReason.CustomerIDTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.FirstNameTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.LastNameTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.MiddleInitialTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.StreetTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.CityTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.StateTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.ZipTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.HomeNumberTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.WorkNumberTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.CellNumberTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.SSNTextBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.EMailTextBoxName);
			MAUIUtilities.GetControl<ComboBox>(addReasonWindow,
							 ResourceNames.AddReason.ReasonCodeComboBoxName);
			MAUIUtilities.GetControl<TextBox>(addReasonWindow,
							 ResourceNames.AddReason.DescriptionTextBoxName);

			//Button
			MAUIUtilities.GetControl<Button>(addReasonWindow,
							 ResourceNames.AddReason.OkButtonName);			
			MAUIUtilities.GetControl<Button>(addReasonWindow,
							 ResourceNames.AddReason.CancelButtonName);

			//Label
			
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.FirstNameLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.LastNameLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.MiddleInitialLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.StreetLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.CityLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.StateLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.ZipLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.HomeNumberLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.WorkNumberLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.CellNumberLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.SSNLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.EMailLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
					ResourceNames.AddReason.ReasonCodeLabelText, StringMatchSyntax.ExactMatch,
					"*", StringMatchSyntax.WildCard);
			MAUIUtilities.GetControl<StaticControl>(addReasonWindow,
											ResourceNames.AddReason.DescriptionLabelText, StringMatchSyntax.ExactMatch,
											"*", StringMatchSyntax.WildCard);
		}

		public void Close()
		{
			addReasonWindow.Extended.CloseWindow();
			addReasonWindow = null;
		}

		public bool Closed
		{
			get
			{
				return addReasonWindow == null;
			}
		}

		#endregion

		#region Screen Value Properties
		
		public string FirstName
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.FirstNameTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.FirstNameTextBoxName).Text = value;
			}
		}

		public string LastName
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.LastNameTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.LastNameTextBoxName).Text = value;
			}
		}

		public string MiddleInitial
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.MiddleInitialTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.MiddleInitialTextBoxName).Text = value;
			}
		}

		public string SSN
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.SSNTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.SSNTextBoxName).Text = value;
			}
		}

		public string Street
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.StreetTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.StreetTextBoxName).Text = value;
			}
		}

		public string City
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.CityTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.CityTextBoxName).Text = value;
			}
		}

		public string State
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.StateTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.StateTextBoxName).Text = value;
			}
		}

		public string Zip
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.ZipTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.ZipTextBoxName).Text = value;
			}
		}

		public string EMail
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.EMailTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.EMailTextBoxName).Text = value;
			}
		}

		public string HomeNumber
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.HomeNumberTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.HomeNumberTextBoxName).Text = value;
			}
		}

		public string WorkNumber
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.WorkNumberTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.WorkNumberTextBoxName).Text = value;
			}
		}

		public string CellNumber
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.CellNumberTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.CellNumberTextBoxName).Text = value;
			}
		}

		public string ReasonForVisit
		{
			get
			{
				return MAUIUtilities.GetControl<ComboBox>(addReasonWindow,
						ResourceNames.AddReason.ReasonCodeComboBoxName).Text;
			}

			set
			{
				MAUIUtilities.GetControl<ComboBox>(addReasonWindow,
					ResourceNames.AddReason.ReasonCodeComboBoxName).SelectByText(value, true);
			}
		}

		public string Description
		{
			get
			{
				return MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.DescriptionTextBoxName).Text;
			}
			set
			{
				MAUIUtilities.GetControl<TextBox>(addReasonWindow,
						ResourceNames.AddReason.DescriptionTextBoxName).Text = value;
			}
		}

		private Button QueueForServiceButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(addReasonWindow,
								ResourceNames.AddReason.QueueForServiceButtonName);
			}
		}

		private Button SelfServiceButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(addReasonWindow,
								ResourceNames.AddReason.SelfServiceButtonName);
			}
		}
        private Button OkButton
        {
            get
            {
                return MAUIUtilities.GetControl<Button>(addReasonWindow,
                                 ResourceNames.AddReason.OkButtonName);
            }
        }
		private Button CancelButton
		{
			get
			{
				return MAUIUtilities.GetControl<Button>(addReasonWindow,
								 ResourceNames.AddReason.CancelButtonName);
			}
		}

		public bool IsModal
		{
			get
			{
				return MAUIUtilities.IsModal(BankWorkBench.Application, addReasonWindow);
			}
		}

		public bool IsResizable
		{
			get
			{
				return addReasonWindow.Extended.IsResizable;
			}
		}

		public bool IsQueueForServiceButtonEnabled
		{
			get
			{
				return QueueForServiceButton.IsEnabled;
			}
		}

		public bool IsSelfServiceButtonEnabled
		{
			get
			{
				return SelfServiceButton.IsEnabled;
			}
		}
        public bool IsOkButtonEnabled
        {
            get
            {
                return OkButton.IsEnabled;
            }
        }
		public bool IsCancelButtonEnabled
		{
			get
			{
				return CancelButton.IsEnabled;
			}
		}
		#endregion

		#region Screen Operations
		public void Clear()
		{
			ReasonForVisit = null;
			Description = String.Empty;
		}

		public void QueueForService()
		{
			QueueForServiceButton.Click();
            Sleeper.Delay(500);
			ResetAddReasonWindow();
		}

		public void SelfService()
		{
			SelfServiceButton.Click();
            Sleeper.Delay(500);
            ResetAddReasonWindow();
		}

        public void OkClick()
        {
            OkButton.Click();
            Sleeper.Delay(500);
            ResetAddReasonWindow();
        }
		public void Cancel()
		{
			CancelButton.Click();
            Sleeper.Delay(500);
            ResetAddReasonWindow();
		}


		public void EscPressed()
		{
			addReasonWindow.SendKeys("{ESC}");
            Sleeper.Delay(500);
            ResetAddReasonWindow();
		}

		public void QueueForServiceContextMenuClick()
		{
			Menu contextMenu = new Menu(ContextMenuAccessMethod.ShiftF10);
			contextMenu[ResourceNames.AddReason.QueueForServiceContextMenuText].Execute();
            Sleeper.Delay(500);
            ResetAddReasonWindow();
        }

		public void SelfServiceContextMenuClick()
		{
			Menu contextMenu = new Menu(ContextMenuAccessMethod.ShiftF10);
			contextMenu[ResourceNames.AddReason.SelfServiceContextMenuText].Execute();
            Sleeper.Delay(500);
            ResetAddReasonWindow();
        }

		private void ResetAddReasonWindow()
		{
			addReasonWindow = MAUIUtilities.GetWindow(BankWorkBench.Application,
					ResourceNames.AddReason.WindowTitle, false);
		}
		#endregion

	}
}
