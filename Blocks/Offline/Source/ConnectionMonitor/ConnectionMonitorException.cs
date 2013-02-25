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
using System.Runtime.Serialization;

namespace Microsoft.Practices.SmartClient.ConnectionMonitor
{
	/// <summary>
	/// Exception thrown by the <see cref="ConnectionMonitor"/> to report failures.
	/// </summary>
	[Serializable]
	public class ConnectionMonitorException : Exception
	{
		/// <summary>
		/// Initializes a new instance of <see cref="ConnectionMonitorException"/>.
		/// </summary>
		public ConnectionMonitorException()
		{
		}


		/// <summary>
		/// Initializes a new instance of the System.Exception class with a specified
		/// error message.        
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ConnectionMonitorException(string message) : base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the System.Exception class with a specified
		/// error message and a reference to the inner exception that is the cause of
		/// this exception.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="inner">The exception that is the cause of the current exception, 
		/// or a null reference if no inner exception is specified.</param>
		public ConnectionMonitorException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>
		/// Initializes a new instance of the System.Exception class with serialized data.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
		/// object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual
		/// information about the source or destination.</param>
		protected ConnectionMonitorException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
	}
}