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
using System.IO;
using System.Text;

namespace GlobalBank.Infrastructure.Library.Tests
{
	class ConsoleHelper : IDisposable
	{
		StringBuilder _buffer;
		StringWriter _writer;
		TextWriter _oldConsoleOut;

		public ConsoleHelper()
		{
			_oldConsoleOut = Console.Out;
			_buffer = new StringBuilder();
			_writer = new StringWriter(_buffer);

			Console.SetOut(_writer);
		}

		public void Dispose()
		{
			if (_oldConsoleOut != null)
			{
				Console.SetOut(_oldConsoleOut);
				_oldConsoleOut = null;
			}

			if (_writer != null)
			{
				_writer.Dispose();
				_writer = null;
			}
		}

		public string[] GetOutput()
		{
			return _buffer.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
