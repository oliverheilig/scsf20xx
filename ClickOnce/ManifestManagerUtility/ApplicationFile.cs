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
using System.ComponentModel;

namespace ClickOnceUtils
{
	/// <summary>
	/// Business entity class for the items in the application manifest file collection
	/// </summary>
	public class ApplicationFile : INotifyPropertyChanged, ISupportInitialize
	{
		#region Protected Member variables
		
		protected bool _isDirty = false;
		protected bool _initialized = true;
		
		#endregion Protected Member variables

		#region Private Member variables
		
		private string _fileName = string.Empty;
		private string _relativePath = string.Empty;
		private bool _dataFile = false;
		private bool _entryPoint = false;

		#endregion Private Member variables

		#region Public properties

		public bool IsDirty
		{
			get
			{
				return _isDirty;
			}
		}


		public string FileName
		{
			get
			{
				return _fileName;
			}
			set
			{
				bool changed = CheckPropertyChanged(_fileName, value);
				_fileName = value;

				if (changed)
				{
					FirePropertyChanged("FileName");
				}
			}
		}

		public string RelativePath
		{
			get
			{
				return _relativePath;
			}
			set
			{
				bool changed = CheckPropertyChanged(_relativePath, value);
				_relativePath = value;
				if (changed)
				{
					FirePropertyChanged("RelativePath");
				}
			}
		}

		public bool DataFile
		{
			get
			{
				return _dataFile;
			}
			set
			{
				bool changed = _dataFile.Equals(value);
				_dataFile = value;
				if (changed)
				{
					FirePropertyChanged("DataFile");
				}
			}
		}

		public bool EntryPoint
		{
			get
			{
				return _entryPoint;
			}
			set
			{
				bool changed = _entryPoint.Equals(value);
				_entryPoint = value;
				if (changed)
				{
					FirePropertyChanged("EntryPoint");
				}
			}
		}
		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region ISupportInitialize Members

		public void BeginInit()
		{
			_initialized = false;
		}

		public void EndInit()
		{
			_initialized = true;
		}

		#endregion

		#region Helper Methods
		protected virtual bool CheckPropertyChanged<T>(T member, T value) where T : class
		{
			if (member == null && value == null)
			{
				return false;
			}

			if (member != null)
			{
				return !member.Equals(value);
			}
			else if (value != null)
			{
				return !value.Equals(member);
			}
			else
			{
				return false;
			}
		}

		private void FirePropertyChanged(string propName)
		{
			if (_initialized)
			{
				_isDirty = true;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs(propName));
				}
			}
		}


		#endregion
	}
}
