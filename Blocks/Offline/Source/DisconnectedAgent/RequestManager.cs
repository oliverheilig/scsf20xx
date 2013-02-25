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
using System.Threading;
using Microsoft.Practices.SmartClient.EndpointCatalog;

namespace Microsoft.Practices.SmartClient.DisconnectedAgent
{
	/// <summary>
	/// A <see cref="RequestManager"/> manages the <see cref="Request"/>s, <see cref="IRequestQueue"/>s, and the <see cref="IRequestDispatcher"/>.
	/// </summary>
	public class RequestManager
	{
		private static volatile RequestManager instance;
		private static object syncRoot = new object();
		private static object syncLockObject = new object();
		private static object threadLockObject = new object();
		private IRequestQueue requestQueue;
		private IRequestQueue deadLetterQueue;
		private IRequestDispatcher requestDispatcher;
		private IEndpointCatalog endpointCatalog;
		private IConnectionMonitor connectionMonitor;
		private DispatchRequestThread thread;
		private bool dispatcherRunning;
		private Queue<Command> dispatchCommands;

		/// <summary>
		/// Event fired when the RequestManager has tried to dispatch a Request.
		/// It could be a successful, failing or expired dispatch.
		/// </summary>
		public event EventHandler<RequestDispatchedEventArgs> RequestDispatched;

		/// <summary>
		/// Creates a RequestManager object.
		/// </summary>
		protected RequestManager()
		{
		}

		/// <summary>
		/// This method initializes the RequestManager for request dispatching with all the configurable elements.
		/// </summary>
		/// <param name="theRequestQueue">Queue for the pending requests.</param>
		/// <param name="theDeadLetterQueue">Queue for the failures.</param>
		/// <param name="theConnectionMonitor">IConnectionMonitor for connectivity events and info.</param>
		/// <param name="theEndpointCatalog">Catalog to get the Endpoint especific information for the dispatching.</param>
		public void Initialize(
			IRequestQueue theRequestQueue,
			IRequestQueue theDeadLetterQueue,
			IConnectionMonitor theConnectionMonitor,
			IEndpointCatalog theEndpointCatalog)
		{
			Initialize<RequestDispatcher>(theRequestQueue, theDeadLetterQueue, theConnectionMonitor,
			                              theEndpointCatalog);
		}

		/// <summary>
		/// This method initializes the RequestManager for request dispatching.
		/// </summary>
		/// <typeparam name="TRequestDispatcher">The concrete type of an <see cref="IRequestDispatcher"/>.</typeparam>
		/// <param name="theRequestQueue">Queue for the pending requests.</param>
		/// <param name="theDeadLetterQueue">Queue for the failures.</param>
		/// <param name="theConnectionMonitor">IConnectionMonitor for connectivity events and info.</param>
		/// <param name="theEndpointCatalog">Catalog to get the Endpoint especific information for the dispatching.</param>
		public void Initialize<TRequestDispatcher>(
			IRequestQueue theRequestQueue,
			IRequestQueue theDeadLetterQueue,
			IConnectionMonitor theConnectionMonitor,
			IEndpointCatalog theEndpointCatalog) where TRequestDispatcher : IRequestDispatcher, new()
		{
			Initialize(theRequestQueue, theDeadLetterQueue, theConnectionMonitor, new TRequestDispatcher(),
			           theEndpointCatalog);
		}

		/// <summary>
		/// This method initializes the RequestManager for request dispatching with all the configurable elements.
		/// </summary>
		/// <param name="theRequestQueue">Queue for the pending requests.</param>
		/// <param name="theDeadLetterQueue">Queue for the failures.</param>
		/// <param name="theConnectionMonitor">IConnectionMonitor for connectivity events and info.</param>
		/// <param name="theRequestDispatcher">Dispatcher to be used by the manager.</param>
		/// <param name="theEndpointCatalog">Catalog to get the Endpoint especific information for the dispatching.</param>
		private void Initialize(
			IRequestQueue theRequestQueue,
			IRequestQueue theDeadLetterQueue,
			IConnectionMonitor theConnectionMonitor,
			IRequestDispatcher theRequestDispatcher,
			IEndpointCatalog theEndpointCatalog)
		{
			Guard.ArgumentNotNull(theRequestQueue, "requestQueue");
			Guard.ArgumentNotNull(theDeadLetterQueue, "deadLetterQueue");
			Guard.ArgumentNotNull(theConnectionMonitor, "connectionMonitor");
			Guard.ArgumentNotNull(theRequestDispatcher, "requestDispatcher");
			Guard.ArgumentNotNull(theEndpointCatalog, "endpointCatalog");

			requestQueue = theRequestQueue;
			requestDispatcher = theRequestDispatcher;
			endpointCatalog = theEndpointCatalog;
			deadLetterQueue = theDeadLetterQueue;
			connectionMonitor = theConnectionMonitor;
			dispatchCommands = new Queue<Command>();
			dispatcherRunning = false;
		}

		/// <summary>
		/// Gets the <see cref="IEndpointCatalog"/> used by the Request Manager.
		/// </summary>
		public IEndpointCatalog EndpointCatalog
		{
			get { return endpointCatalog; }
		}

		/// <summary>
		/// Starts the automatic dispatch regarding connectivity events and enquing.
		/// </summary>
		public void StartAutomaticDispatch()
		{
			if (dispatcherRunning) return;
			dispatcherRunning = true;
			requestQueue.RequestEnqueued += OnAutomaticDispatch;
			connectionMonitor.ConnectionStatusChanged += OnConnectionDispatch;
			if (connectionMonitor.IsConnected)
			{
				DispatchAllPendingRequestsForConnection();
			}
		}

		/// <summary>
		/// Stops the automatic dispatch.
		/// </summary>
		public void StopAutomaticDispatch()
		{
			if (!dispatcherRunning) return;
			connectionMonitor.ConnectionStatusChanged -= OnConnectionDispatch;
			requestQueue.RequestEnqueued -= OnAutomaticDispatch;
			dispatcherRunning = false;
		}

		/// <summary>
		/// Dispatch all the requests in the pending queue having an endpoint valid address 
		/// regardless of stamp's value and connectivity price.
		/// </summary>
		public void DispatchAllPendingRequestsForConnection()
		{
			DispatchRequests(new Command(this, "GetRequestsForCurrentConnectionPrice"));
		}

		/// <summary>
		/// Dispatch all the requests in the pending queue that match (like) the given tag 
		/// having endpoint valid address regardless of stamp's value.
		/// </summary>
		/// <param name="tag"></param>
		public void DispatchPendingRequestsByTag(string tag)
		{
			DispatchRequests(new Command(this, "GetPendingRequestsByTag", tag));
		}

		/// <summary>
		/// Dispatch all the requests in the pending queue having endpoint valid address regardless of stamp's value.
		/// </summary>
		public void DispatchAllPendingRequests()
		{
			DispatchRequests(new Command(this, "GetPendingRequests"));
		}

		/// <summary>
		/// Dispatch the request if it exists in the pending queue having an endpoint valid address 
		/// regardless of stamp's value.
		/// </summary>
		public void DispatchRequest(Request request)
		{
			Guard.ArgumentNotNull(request, "request");
			DispatchRequests(new Command(this, "GetRequest", request.RequestId));
		}

		/// <summary>
		/// Gets the pending requests queue.
		/// </summary>
		public IRequestQueue RequestQueue
		{
			get { return requestQueue; }
		}

		/// <summary>
		/// Gets the failed requests queue.
		/// </summary>
		public IRequestQueue DeadLetterQueue
		{
			get { return deadLetterQueue; }
		}

		/// <summary>
		/// Gets the <see cref="IConnectionMonitor"/> used for connectivity information.
		/// </summary>
		public IConnectionMonitor ConnectionMonitor
		{
			get { return connectionMonitor; }
		}

		/// <summary>
		/// Gets the single instance for the RequestManager.
		/// If there is not an instance it creates a new one and returns it.
		/// </summary>
		public static RequestManager Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
						{
							instance = new RequestManager();
						}
					}
				}
				return instance;
			}
		}

		/// <summary>
		/// Wait for the dispatch inner thread finish.
		/// </summary>
		/// <param name="timeout">Milliseconds to wait.</param>
		/// <returns>true if the thread has finished before timeout, otherwise false.</returns>
		public bool Join(int timeout)
		{
			lock (threadLockObject)
			{
				if (thread == null)
				{
					return true;
				}

				return thread.Join(timeout);
			}
		}

		/// <summary>
		/// Gets the automatic dispatch running state of the RequestManager.
		/// </summary>
		public bool AutomaticDispatcherRunning
		{
			get { return dispatcherRunning; }
		}

		/// <summary>
		/// Stops the dispatching thread.
		/// </summary>
		public void StopAutomaticDispatching()
		{
			lock (threadLockObject)
			{
				if (thread != null)
				{
					thread.Stop();
				}
			}
		}

		#region Command Methods

		/// <summary>
		/// Gets the specified request.
		/// </summary>
		/// <param name="requestId">The requested <see cref="Request"/></param>
		/// <returns>A <see cref="Request"/> object.</returns>
		public Request GetRequest(Guid requestId)
		{
			return requestQueue.GetRequest(requestId);
		}

		/// <summary>
		/// Gets an iterator with the requests that contain the given string in the tag.
		/// </summary>
		/// <param name="tag">String to be searched.</param>
		/// <returns>IEnumerable with the matching requests.</returns>
		public IEnumerable<Request> GetPendingRequestsByTag(string tag)
		{
			foreach (Request request in requestQueue.GetRequests(tag))
			{
				yield return request;
			}
		}

		/// <summary>
		/// Gets an iterator with all the requests in the queue.
		/// </summary>
		/// <returns>IEnumerable with all the requests.</returns>
		public IEnumerable<Request> GetPendingRequests()
		{
			foreach (Request request in requestQueue.GetRequests())
			{
				yield return request;
			}
		}

		/// <summary>
		/// Gets an iterator with the requests that have equal or more stamps than the current conenction price.
		/// </summary>
		/// <returns>IEnumerable with the matching requests.</returns>
		public IEnumerable<Request> GetRequestsForCurrentConnectionPrice()
		{
			int? price = null;
			try
			{
				price = connectionMonitor.CurrentConnectionPrice;
			}
			catch
			{
				price = null;
				StopAutomaticDispatching();
			} //Exception getting the currentConnectionPrice seems connectivity issue

			if (price != null)
			{
				foreach (Request request in requestQueue.GetRequests((int) price))
				{
					yield return request;
				}
			}
		}

		#endregion

		/// <summary>
		/// Added for testability support
		/// </summary>
		protected virtual void ResetInstance()
		{
			if (AutomaticDispatcherRunning)
			{
				StopAutomaticDispatch();
			}
			StopAutomaticDispatching();

			DisposeWhenRequired(requestQueue);
			DisposeWhenRequired(requestDispatcher);
			DisposeWhenRequired(endpointCatalog);
			DisposeWhenRequired(deadLetterQueue);
			DisposeWhenRequired(connectionMonitor);

			instance = null;
		}

		private static void DisposeWhenRequired(object target)
		{
			if (target is IDisposable)
			{
				((IDisposable) target).Dispose();
			}
		}

		private void DispatchRequests(Command command)
		{
			lock (syncLockObject)
			{
				dispatchCommands.Enqueue(command);
				if (thread == null)
				{
					thread = new DispatchRequestThread(this, dispatchCommands, syncLockObject);
					thread.Start();
				}
			}
		}

		private void OnAutomaticDispatch(object sender, EventArgs args)
		{
			if (connectionMonitor.IsConnected)
			{
				DispatchAllPendingRequestsForConnection();
			}
		}

		private void OnConnectionDispatch(object sender, EventArgs args)
		{
			if (thread != null)
			{
				//Stop current dispatching
				thread.Stop();
				thread.Join(2000);
			}

			if (connectionMonitor.IsConnected)
			{
				//Continue with new connection status
				DispatchAllPendingRequestsForConnection();
			}
		}

		private class DispatchRequestThread
		{
			private Thread thread;
			private RequestManager manager;
			private Queue<Command> commands;
			private bool stop;
			private object syncLockObject;

			public DispatchRequestThread(
				RequestManager manager, Queue<Command> commands, object syncLockObject)
			{
				this.manager = manager;
				this.commands = commands;
				this.syncLockObject = syncLockObject;
			}

			public void Start()
			{
				thread = new Thread(new ThreadStart(DispatchRequests));
				thread.Start();
			}

			private void DispatchRequests()
			{
				try
				{
					while (stop == false)
					{
						IEnumerable<Request> requests;
						lock (syncLockObject)
						{
							if (commands.Count == 0)
								break;
							requests = GetRequestsFromNextCommand();
						}

						foreach (Request request in requests)
						{
							DispatchSingleRequest(request);
						}

						lock (syncLockObject)
						{
							if (!stop)
								commands.Dequeue();
						}
					}
				}
				finally
				{
					manager.thread = null;
				}
			}

			private void DispatchSingleRequest(Request request)
			{
				//If there is not a connection stops the thread and doesn't remove the command from the queue.
				if (!manager.connectionMonitor.IsConnected)
					stop = true;

				// if the endpoint catalog is configured, and the endpoint for this request is not configured
				// add the request to the dead letter queue
				if ((!(String.IsNullOrEmpty(request.Endpoint))) &&
				    ((manager.endpointCatalog.Count >= 0) && (!manager.endpointCatalog.EndpointExists(request.Endpoint))))
				{
					manager.deadLetterQueue.Enqueue(request);
					manager.requestQueue.Remove(request);
					return;
				}

				if (stop)
					return;

				try
				{
					// if we have an endpoint catalog configured
					// use the first network that the endpoint is configured for
					if (manager.endpointCatalog.Count >= 0)
					{
						// Dispatch the request using the 1st available network
						foreach (
							string network in manager.connectionMonitor.ConnectedNetworks)
						{
							if ((String.IsNullOrEmpty(request.Endpoint)) ||
							    (manager.endpointCatalog.AddressExistsForEndpoint(request.Endpoint, network)))
							{
								DispatchRequestInternal(request, network);
								break;
							}
						}
					}
						// if no endpoints are configured, just dispatch it on the first network available
					else
					{
						DispatchRequestInternal(request, manager.connectionMonitor.ConnectedNetworks[0]);
					}
				}
				catch
				{
					//If there was an exception getting the currentNetwork or Endpoint
					//the connection has been lost between the check and the get / DispatchRequestInternal
					stop = true;
				}
			}

			private void DispatchRequestInternal(Request request, string networkName)
			{
				DispatchResult result = DispatchResult.Expired;

				if (request.Behavior.Expiration == null ||
				    request.Behavior.Expiration >= DateTime.Now)
				{
					try
					{
						result = manager.requestDispatcher.Dispatch(request, networkName);
					}
					catch (Exception)
					{
						//Any Exception should fail the dispatch request
						result = DispatchResult.Failed;
					}

					if (result == DispatchResult.Failed)
					{
						if (!manager.connectionMonitor.IsConnected)
							return;
						manager.deadLetterQueue.Enqueue(request);
					}
				}
				manager.requestQueue.Remove(request);

				if (manager.RequestDispatched != null)
					manager.RequestDispatched(this, new RequestDispatchedEventArgs(request, result));
			}

			private IEnumerable<Request> GetRequestsFromNextCommand()
			{
				if (commands.Peek().CommandName == "GetRequest")
				{
					Request requestByGuid = (Request) commands.Peek().Execute();
					if (requestByGuid == null)
						return new Request[] {};
					else
						return new Request[] {requestByGuid};
				}
				else
					return (IEnumerable<Request>) commands.Peek().Execute();
			}

			public bool Join(int timeout)
			{
				return thread.Join(timeout);
			}

			public void Stop()
			{
				stop = true;
			}
		}
	}
}