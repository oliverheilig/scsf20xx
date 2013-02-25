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
using Microsoft.Practices.RecipeFramework.Library.CodeModel;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
    public class AddAttributeNoQuotesAction : ConfigurableAction
	{
		private CodeAttribute _attribute;
		private string _attributeValue;
		private CodeElementEx _codeElement;
		private string _name;
		private object _position;

        public AddAttributeNoQuotesAction()
		{
			this._attributeValue = "";
			this._position = 0;
		}

		// Methods
		public override void Execute()
		{
			this.Attribute = this._codeElement.AddAttribute(this.AttributeName, this.AttributeValue, this.Position);
		}

		public override void Undo()
		{
		}

		// Properties
		[Output]
		public CodeAttribute Attribute
		{
			get { return _attribute; }
			set { _attribute = value; }
		}

		[Input(Required = true)]
		public string AttributeName
		{
			get { return _name; }
			set { _name = value; }
		}

		[Input(Required = false)]
		public string AttributeValue
		{
			get { return _attributeValue; }
			set { _attributeValue = value; }
		}

		[Input(Required = true)]
		public CodeElement CodeElement
		{
			get { return _codeElement.RealObject; }
			set { _codeElement = new CodeElementEx(value); }
		}

		[Input(Required = false)]
		public object Position
		{
			get { return _position; }
			set { _position = value; }
		}
	}
}
