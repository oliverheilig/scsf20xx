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
using System.Data;
using System.Data.Common;

namespace Microsoft.Practices.TestDataAccess
{
	public class MockParameter : DbParameter
	{
		private ParameterDirection direction;
		private DataRowVersion sourceVersion;
		private string parameterName;
		private string sourceColumn;
		private object value;
		private DbType dbType;
		private bool sourceColumnNullMapping;
		private bool isNullable;
		private int size;

		public override DbType DbType
		{
			get { return dbType; }
			set { dbType = value; }
		}

		public override ParameterDirection Direction
		{
			get { return direction; }
			set { direction = value; }
		}

		public override bool IsNullable
		{
			get { return isNullable; }
			set { isNullable = value; }
		}

		public override string ParameterName
		{
			get { return parameterName; }
			set { parameterName = value; }
		}

		public override void ResetDbType()
		{
		}

		public override int Size
		{
			get { return size; }
			set { size = value; }
		}

		public override string SourceColumn
		{
			get { return sourceColumn; }
			set { sourceColumn = value; }
		}

		public override bool SourceColumnNullMapping
		{
			get { return sourceColumnNullMapping; }
			set { sourceColumnNullMapping = value; }
		}

		public override DataRowVersion SourceVersion
		{
			get { return sourceVersion; }
			set { sourceVersion = value; }
		}

		public override object Value
		{
			get { return value; }
			set { this.value = value; }
		}
	}
}