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

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// Exception to wrap any exception thrown by return callback methods.
	/// </summary>
	[Serializable]
	public class ReturnCallbackException : Exception
	{
		/// <summary>
		/// Creates a ReturnCallbackException object.
		/// </summary>
		public ReturnCallbackException()
			: base()
		{
		}

		/// <summary>
		/// Creates a ReturnCallbackException object.
		/// </summary>
		/// <param name="message">Message for the exception.</param>
		public ReturnCallbackException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Creates a ReturnCallbackException object.
		/// </summary>
		/// <param name="message">Message for the exception.</param>
		/// <param name="innerException">Exception thrown by the return callback method.</param>
		public ReturnCallbackException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Creates a ReturnCallbackException object.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
		/// object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual
		/// information about the source or destination.</param>
		protected ReturnCallbackException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}