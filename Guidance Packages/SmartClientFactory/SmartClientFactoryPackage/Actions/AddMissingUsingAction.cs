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
using Microsoft.Practices.RecipeFramework;
using EnvDTE;
using Microsoft.Practices.RecipeFramework.Library.Solution;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	public class AddMissingUsingAction : ConfigurableAction
	{
		private string _usingNamespace;
		private object _target;

		[Input(Required = true)]
		public string UsingNamespace
		{
			get { return _usingNamespace; }
			set { _usingNamespace = value; }
		}

		[Input(Required = true)]
		public object Target
		{
			get { return _target; }
			set { _target = value; }
		}


		public override void Execute()
		{
			CodeClass codeClass = _target as CodeClass;
			if (codeClass == null && !ReferenceUtil.HaveAClass(_target, out codeClass)) return;
			if (codeClass != null)
			{
				TextPoint tp = codeClass.StartPoint;
				EditPoint ep = tp.CreateEditPoint();
				ep.StartOfDocument();

				int lastUsing = -1;
                string keyword = string.Empty;
                string patternText = string.Empty;

                switch (codeClass.Language)
                {
                    case CodeModelLanguageConstants.vsCMLanguageCSharp:

                        keyword = "using";
                        patternText = String.Concat(keyword, " {0};");
                        break;

                    case CodeModelLanguageConstants.vsCMLanguageVB:
                        keyword = "Imports";
                        patternText = String.Concat(keyword, " {0}");
                        break;

                    default:
                        throw new NotSupportedException("Language not supported");
                }

                string usingText = String.Format(patternText, _usingNamespace);
                //string usingText = String.Format("using {0}", usingNamespace);

				while (!ep.AtEndOfDocument)
				{
					int length = ep.LineLength;
					string line = ep.GetText(ep.LineLength);
                    //if (line.Contains("using")) return;
                    if (line.Contains(usingText)) return;
					if (line.StartsWith(keyword)) lastUsing = ep.Line;
					ep.LineDown(1);
				}
				ep.StartOfDocument();
				if (lastUsing > 0) ep.LineDown(lastUsing);
				ep.Insert(usingText);
				//ep.Insert(";");
				ep.Insert(Environment.NewLine);
				if (ep.LineLength != 0) ep.Insert(Environment.NewLine);
			}
		}

		public override void Undo()
		{
		}
	}
}
