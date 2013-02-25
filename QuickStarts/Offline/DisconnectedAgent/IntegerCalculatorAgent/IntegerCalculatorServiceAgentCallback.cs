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
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using System;

namespace Quickstarts.DisconnectedAgent.IntegerCalculatorAgent
{
	public class AddReturnEventArgs : EventArgs
	{
		private Request request;
		private int returnValue;

		public AddReturnEventArgs(Request request, int returnValue)
		{
			this.request = request;
			this.returnValue = returnValue;
		}

		public Request Request
		{
			get { return this.request; }
		}

		public int ReturnValue
		{
			get { return this.returnValue; }
		}
	}

	public class RequestExceptionEventArgs : EventArgs
	{
		private Request request;
		private Exception exception;
		private OnExceptionAction exceptionAction;

		public OnExceptionAction ExceptionAction
		{
			get { return exceptionAction; }
			set { exceptionAction = value; }
		}
	
		public RequestExceptionEventArgs(Request request, Exception exception, OnExceptionAction exceptionAction)
		{
			this.request = request;
			this.exception = exception;
			this.exceptionAction = exceptionAction;
		}

		public Exception Exception
		{
			get { return exception; }
		}
	
		public Request Request
		{
			get { return request; }
		}

		public OnExceptionAction OnExceptionAction
		{
			get { return exceptionAction; }
		}

	}


	// Use this proxy to make requests to the service when working in an application that is occasionally connected
	public class IntegerCalculatorServiceDisconnectedAgentCallback : IntegerCalculatorServiceDisconnectedAgentCallbackBase
	{
		public static event EventHandler<AddReturnEventArgs> AddReturn;
		public static event EventHandler<RequestExceptionEventArgs> RequestException;

		public override void OnAddReturn(Request request, object[] parameters, int returnValue)
		{
			if (AddReturn != null)
			{
				AddReturn(this, new AddReturnEventArgs(request, returnValue));
			}
		}

		public override OnExceptionAction OnAddException(Request request, Exception ex)
		{
			OnExceptionAction action = OnExceptionAction.Dismiss;
			if (RequestException != null)
			{
				RequestExceptionEventArgs args = new RequestExceptionEventArgs(request, ex, action);
				RequestException(this, args);
				action = args.OnExceptionAction;
			}
			return action;
		}
	}
}
