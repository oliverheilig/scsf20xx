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
using Microsoft.CSharp;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	public class UpdateEventFireMethodAction : ConfigurableAction
	{
		private CodeFunction _codeMethod;
		private string _eventTopic;
		private const string _fireMethodPatternCS = "{{\nif ({0} != null)\n{{\n{0}(this, eventArgs);\n}}\n}}";
        private const string _fireMethodPatternVB = "\nRaiseEvent {0}(Me, eventArgs)\nEnd Sub\n";

		[Input(Required = true)]
		public CodeFunction Method
		{
			get { return _codeMethod; }
			set { _codeMethod = value; }
		}

		[Input()]
		public string EventTopic
		{
			get { return _eventTopic; }
			set { _eventTopic = value; }
		}


        public override void Execute()
        {
            _codeMethod.CanOverride = true;
            _codeMethod.Access = vsCMAccess.vsCMAccessProtected;
            if (Utility.IsSealedOrStatic((CodeClass)_codeMethod.Parent))
            {
                _codeMethod.CanOverride = false;
                _codeMethod.Access = vsCMAccess.vsCMAccessPrivate;
            }

            if (_eventTopic != null)
            {
                string _fireMethodPattern = (_codeMethod.Language == CodeModelLanguageConstants.vsCMLanguageCSharp) ? _fireMethodPatternCS : _fireMethodPatternVB;
                string codeText = String.Format(_fireMethodPattern, _eventTopic);
                EditPoint sp = _codeMethod.StartPoint.CreateEditPoint();
                sp.EndOfLine();
                sp.ReplaceText(_codeMethod.EndPoint, codeText, (int)vsEPReplaceTextOptions.vsEPReplaceTextAutoformat);
            }
        }

		public sealed override void Undo()
		{
			EditPoint ep = _codeMethod.StartPoint.CreateEditPoint();
			ep.LineDown(1);
			ep.Delete(_codeMethod.EndPoint);
			ep.Insert("{\r\n}");
		}
	}
}
