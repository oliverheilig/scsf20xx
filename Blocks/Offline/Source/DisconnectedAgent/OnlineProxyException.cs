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
	/// Exception thrown by the online proxy factories to signal that an error has ocurred and 
	/// that the exception must escalate out of the <see cref="RequestDispatcher"/>
	/// </summary>
	[Serializable]
	public class OnlineProxyException : Exception
	{
		/// <summary>
		/// Creates an OnlineProxyException object.
		/// </summary>
		public OnlineProxyException()
		{
		}

		/// <summary>
		/// Creates an OnlineProxyException object.
		/// </summary>
		/// <param name="message">The message describing the <see cref="Exception"/>.</param>
		public OnlineProxyException(string message) : base(message)
		{
		}

		/// <summary>
		/// Creates an OnlineProxyException object.
		/// </summary>
		/// <param name="message">The message describing the <see cref="Exception"/>.</param>
		/// <param name="inner"></param>
		public OnlineProxyException(string message, Exception inner) : base(message, inner)
		{
		}

		/// <summary>
		/// Creates an OnlineProxyException object.
		/// </summary>
		/// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized 
		/// object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="StreamingContext"/> that contains contextual
		/// information about the source or destination.</param>
		protected OnlineProxyException(
			SerializationInfo info,
			StreamingContext context)
			: base(info, context)
		{
		}
	}
}