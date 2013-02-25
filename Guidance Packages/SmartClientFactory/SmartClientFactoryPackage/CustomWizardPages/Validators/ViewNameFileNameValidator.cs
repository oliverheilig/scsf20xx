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
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Practices.SmartClientFactory.CustomWizardPages;

namespace Microsoft.Practices.EnterpriseLibrary.Validation.Validators
{
	public class ViewNameFileNameValidator : AndCompositeValidator
	{
		/// <summary>
		/// Creates a new instance of <see cref="FileNameValidator"/>
		/// </summary>
		public ViewNameFileNameValidator()
			: base(new Validator[] {new ViewNameFileNameLengthValidator(), new ReservedSystemWordsFileNameValidator()})
		{
		}

		private class ViewNameFileNameLengthValidator : Validator<string>
		{
			// Due to the generated helper classes, whose names are based on the 
			// View's name, we need to shorten the max length allowed by 30 to be safe
			private const int maxFileNameLength = 80; // 110 - 30
			private const int maxFullFileNameLength = 230; // 260 - 30

			public ViewNameFileNameLengthValidator()
				: base("The view file name is too long", null)
			{
			}

			protected override void DoValidate(string objectToValidate, object currentTarget, string key,
			                                   ValidationResults validationResults)
			{
				ICreateViewPageModel pageModel = currentTarget as ICreateViewPageModel;
				if (pageModel != null)
				{
					string physicalPath = string.Empty;
					if (pageModel.ProjectItem != null)
					{
						physicalPath = pageModel.ProjectItem.ItemPath;
					}
					else
					{
						physicalPath = pageModel.ModuleProject.ProjectPath;
					}
					string moduleViewsProjectPath = (pageModel.CreateViewFolder)
					                                	? Path.Combine(physicalPath, objectToValidate)
					                                	: physicalPath;
					string fileName =
						String.Format(CultureInfo.CurrentCulture, "{0}Presenter.{1}", objectToValidate, pageModel.Language);
					string fullFileName = Path.Combine(moduleViewsProjectPath, fileName);

					if (objectToValidate == null || fullFileName.Length > maxFullFileNameLength ||
					    objectToValidate.Length > maxFileNameLength)
						LogValidationResult(validationResults, MessageTemplate, currentTarget, key);
				}
			}

			protected override string DefaultMessageTemplate
			{
				get { return "The view file name is too long"; }
			}
		}

		private class ReservedSystemWordsFileNameValidator : Validator<string>
		{
			private Regex validNames =
				new Regex(@"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:""><|/]+$",
				          RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

			public ReservedSystemWordsFileNameValidator()
				: base("File name not allowed", null)
			{
			}

			protected override void DoValidate(string objectToValidate, object currentTarget, string key,
			                                   ValidationResults validationResults)
			{
				if (objectToValidate == null || !validNames.IsMatch(objectToValidate))
					LogValidationResult(validationResults, DefaultMessageTemplate, currentTarget, key);
			}

			protected override string DefaultMessageTemplate
			{
				get { return "File name not allowed"; }
			}
		}
	}
}