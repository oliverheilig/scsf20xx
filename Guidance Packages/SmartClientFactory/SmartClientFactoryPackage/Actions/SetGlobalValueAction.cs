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
using EnvDTE;
using Microsoft.Practices.RecipeFramework;

namespace Microsoft.Practices.SmartClientFactory.Actions
{
	public sealed class SetGlobalValueAction : ConfigurableAction
	{
		private string _valueName;
		private object _value;
		private object _oldValue = null;
		private bool _persistValue = false;

		[Input(Required = true)]
		public string ValueName
		{
			get { return _valueName; }
			set { _valueName = value; }
		}

		[Input()]
		public bool PersistValue
		{
			get { return _persistValue; }
			set { _persistValue = value; }
		}
	
		[Input(Required = true)]
		public object Value
		{
			get { return _value; }
			set { _value = value; }
		}

		private object SetValue(object value)
		{
			DTE dte = (DTE)GetService(typeof(DTE));
			object oldValue = null;
			if (dte.Solution.Globals.get_VariableExists(_valueName))
			{
				oldValue = dte.Solution.Globals[_valueName];
			}
			dte.Solution.Globals[_valueName] = value.ToString();
			dte.Solution.Globals.set_VariablePersists(_valueName, _persistValue);
			return oldValue;
		}

		public override void Execute()
		{
			_oldValue = SetValue(_value);
		}

		public override void Undo()
		{
			SetValue(_oldValue);
		}
	}
}
