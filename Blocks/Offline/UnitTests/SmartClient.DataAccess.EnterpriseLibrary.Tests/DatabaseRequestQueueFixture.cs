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
using System.IO;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.SmartClient.DisconnectedAgent;
using Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.ServiceLocation;

namespace Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests
{
	/// <summary>
	/// Summary description for DatabaseRequestQueueFixture
	/// </summary>
	[TestClass]
	public class DatabaseRequestQueueFixture
	{
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            IServiceLocator mockServiceLocator = new MockServiceLocator(EnterpriseLibraryContainer.Current);
            EnterpriseLibraryContainer.Current = mockServiceLocator;
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Datastore.sdf");
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
        
		[TestMethod]
		public void CreatesTableWhenNotPresent()
		{
			string tableName = "Requests";

			using (SmartClientDatabase database = GetDatabase())
			{
				Assert.IsFalse(database.TableExists(tableName));
				IRequestQueue queue = new DatabaseRequestQueue(database, tableName);

				Assert.IsTrue(database.TableExists(tableName));
			}
		}


		[TestMethod]
		public void UsesDefaultTableNameWhenNotSupplied()
		{
			string tableName = "Requests";
			using (SmartClientDatabase database = GetDatabase())
			{
				Assert.IsFalse(database.TableExists(tableName));
				IRequestQueue queue = new DatabaseRequestQueue(database);

				Assert.IsTrue(database.TableExists(tableName));
			}
		}

		[TestMethod]
		public void AddARequestIncreasesCount()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				int expected = queue.GetCount() + 1;

				Request request = CreateRequest();
				queue.Enqueue(request);

				Assert.AreEqual(expected, queue.GetCount());
			}
		}

		[TestMethod]
		public void RequestCanBeQueuedWhenOptionalPropertiesNotSet()
		{
			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.MethodName = "Method";
			request.OnlineProxyType = typeof (DatabaseRequestQueueFixture);
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (MockProxyFactory);

			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				Assert.AreEqual(0, queue.GetCount());
				queue.Enqueue(request);
				Assert.AreEqual(1, queue.GetCount());
			}
		}

		[TestMethod]
		public void CanDequeuedWhenOptionalPropertiesNotSet()
		{
			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.MethodName = "Method";
			request.OnlineProxyType = typeof (DatabaseRequestQueueFixture);
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (MockProxyFactory);
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				Assert.AreEqual(0, queue.GetCount());
				queue.Enqueue(request);
				Assert.AreEqual(1, queue.GetCount());

				Request request2 = queue.GetNextRequest();
				Assert.IsNotNull(request2);
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void EnqueingRequestWithMissingMethodNameThrows()
		{
			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.OnlineProxyType = typeof (DatabaseRequestQueueFixture);
			request.Behavior = new OfflineBehavior();

			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				queue.Enqueue(request);
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void EnqueingRequestWithMissingEndpointThrows()
		{
			Request request = new Request();
			request.MethodName = "Method";
			request.OnlineProxyType = typeof (DatabaseRequestQueueFixture);
			request.Behavior = new OfflineBehavior();

			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				queue.Enqueue(request);
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void EnqueingRequestWithMissingOnlineProxyTypeThrows()
		{
			Request request = new Request();
			request.MethodName = "Method";
			request.Endpoint = "Endpoint";
			request.Behavior = new OfflineBehavior();

			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				queue.Enqueue(request);
			}
		}

		[TestMethod]
		[ExpectedException(typeof (ArgumentNullException))]
		public void EnqueingRequestWithMissingBehaviorThrows()
		{
			Request request = new Request();
			request.MethodName = "Method";
			request.Endpoint = "Endpoint";
			request.OnlineProxyType = typeof (DatabaseRequestQueueFixture);
			request.Behavior = null;

			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				queue.Enqueue(request);
			}
		}

		[TestMethod]
		public void GetNextRequestDoesntRemoveItFromQueue()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request = CreateRequest();
				queue.Enqueue(request);

				Assert.AreEqual(1, queue.GetCount());
				Request dequeued = queue.GetNextRequest();

				Assert.AreEqual(1, queue.GetCount());
				Assert.IsTrue(AreEqual(request, dequeued));
			}
		}

		[TestMethod]
		public void RemovingARequestsDecreasesCount()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				Request request = CreateRequest();

				Assert.AreEqual(0, queue.GetCount());
				queue.Enqueue(request);
				Assert.AreEqual(1, queue.GetCount());

				queue.Remove(request);
				Assert.AreEqual(0, queue.GetCount());
			}
		}

		[TestMethod]
		public void CanGetRequestByRequestId()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request1 = CreateRequest();
				request1.Behavior.Tag = "A";

				Request request2 = CreateRequest();
				request2.Behavior.Tag = "B";

				Request request3 = CreateRequest();
				request3.Behavior.Tag = "A";

				queue.Enqueue(request1);
				queue.Enqueue(request2);
				queue.Enqueue(request3);

				Assert.AreEqual(3, queue.GetCount());

				Request req = queue.GetRequest(request2.RequestId);
				queue.Remove(req);

				Assert.IsNotNull(req);
				Assert.IsTrue(AreEqual(request2, req));
				Assert.AreEqual(2, queue.GetCount());
			}
		}

		[TestMethod]
		public void GetRequestByRequestIdReturnsNullIfRequestIdIsNotFound()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request1 = CreateRequest();
				request1.Behavior.Tag = "A";

				Request request2 = CreateRequest();
				request2.Behavior.Tag = "B";

				queue.Enqueue(request1);
				queue.Enqueue(request2);

				Assert.AreEqual(2, queue.GetCount());

				Request req = queue.GetRequest(Guid.Empty);

				Assert.IsNull(req);
				Assert.AreEqual(2, queue.GetCount());
			}
		}

		[TestMethod]
		public void CanGetRequestsIteratorByTag()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request1 = CreateRequest();
				request1.Behavior.Tag = "A";

				Request request2 = CreateRequest();
				request2.Behavior.Tag = "B";

				Request request3 = CreateRequest();
				request3.Behavior.Tag = "A";

				queue.Enqueue(request1);
				queue.Enqueue(request2);
				queue.Enqueue(request3);

                Assert.AreEqual(3, queue.GetCount());

				List<Request> list = new List<Request>();

				foreach (Request req in queue.GetRequests("A"))
				{
					list.Add(req);
					queue.Remove(req);
				}

				Assert.AreEqual(2, list.Count);
				Assert.IsTrue(AreEqual(request1, list[0]));
				Assert.IsTrue(AreEqual(request3, list[1]));
				Assert.AreEqual(1, queue.GetCount());
			}
		}

		[TestMethod]
		public void CanGetRequestsIteratorByPrice()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request1 = CreateRequest();
				request1.Behavior.Stamps = 8;

				Request request2 = CreateRequest();
				request2.Behavior.Stamps = 3;

				Request request3 = CreateRequest();
				request3.Behavior.Stamps = 5;

				queue.Enqueue(request1);
				queue.Enqueue(request2);
				queue.Enqueue(request3);

				Assert.AreEqual(3, queue.GetCount());

				List<Request> list = new List<Request>();

				foreach (Request req in queue.GetRequests(5))
				{
					list.Add(req);
					queue.Remove(req);
				}

				Assert.AreEqual(2, list.Count);
				Assert.IsTrue(AreEqual(request1, list[0]));
				Assert.IsTrue(AreEqual(request3, list[1]));
				Assert.AreEqual(1, queue.GetCount());
			}
		}

		[TestMethod]
		public void RequestsIteratorIsFifo()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request1 = CreateRequest();
				Request request2 = CreateRequest();
				Request request3 = CreateRequest();
				Request request4 = CreateRequest();

				queue.Enqueue(request1);
				queue.Enqueue(request2);
				queue.Enqueue(request3);
				queue.Enqueue(request4);

				Assert.AreEqual(4, queue.GetCount());

				List<Request> list = new List<Request>();

				foreach (Request req in queue.GetRequests())
				{
					list.Add(req);
					queue.Remove(req);
				}

				Assert.AreEqual(4, list.Count);
				Assert.IsTrue(AreEqual(request1, list[0]));
				Assert.IsTrue(AreEqual(request2, list[1]));
				Assert.IsTrue(AreEqual(request3, list[2]));
				Assert.IsTrue(AreEqual(request4, list[3]));
				Assert.AreEqual(0, queue.GetCount());
			}
		}

		[TestMethod]
		public void RequestsByTagAreFifo()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request1 = CreateRequest();
				request1.Behavior.Tag = "A";
				Request request2 = CreateRequest();
				request2.Behavior.Tag = "A";
				Request request3 = CreateRequest();
				request3.Behavior.Tag = "B";
				Request request4 = CreateRequest();
				request4.Behavior.Tag = "A";

				queue.Enqueue(request1);
				queue.Enqueue(request2);
				queue.Enqueue(request3);
				queue.Enqueue(request4);

				Assert.AreEqual(4, queue.GetCount());

				List<Request> list = new List<Request>();

				foreach (Request req in queue.GetRequests("A"))
				{
					list.Add(req);
					queue.Remove(req);
				}

				Assert.AreEqual(3, list.Count);
				Assert.IsTrue(AreEqual(request1, list[0]));
				Assert.IsTrue(AreEqual(request2, list[1]));
				Assert.IsTrue(AreEqual(request4, list[2]));
				Assert.AreEqual(1, queue.GetCount());
			}
		}

		[TestMethod]
		public void RequestsByPriceAreFifo()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(0, queue.GetCount());

				Request request1 = CreateRequest();
				request1.Behavior.Stamps = 7;
				Request request2 = CreateRequest();
				request2.Behavior.Stamps = 6;
				Request request3 = CreateRequest();
				request3.Behavior.Stamps = 1;
				Request request4 = CreateRequest();
				request4.Behavior.Stamps = 5;

				queue.Enqueue(request1);
				queue.Enqueue(request2);
				queue.Enqueue(request3);
				queue.Enqueue(request4);

				Assert.AreEqual(4, queue.GetCount());

				List<Request> list = new List<Request>();

				foreach (Request req in queue.GetRequests(5))
				{
					list.Add(req);
					queue.Remove(req);
				}

				Assert.AreEqual(3, list.Count);
				Assert.IsTrue(AreEqual(request1, list[0]));
				Assert.IsTrue(AreEqual(request2, list[1]));
				Assert.IsTrue(AreEqual(request4, list[2]));
				Assert.AreEqual(1, queue.GetCount());
			}
		}

		[TestMethod]
		public void OnEnqueuedEventIsFiredWhenRequestIsEnqueued()
		{
			bool eventFired = false;

			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				queue.RequestEnqueued += delegate(object sender, RequestEnqueuedEventArgs args) { eventFired = true; };

				Assert.IsFalse(eventFired);
				Request request1 = CreateRequest();
				request1.Behavior.Stamps = 7;
				queue.Enqueue(request1);
				Assert.IsTrue(eventFired);
				Assert.AreEqual(1, queue.GetCount());
			}
		}

		[TestMethod]
		public void OnRequestEnqueuedTheQueuedDateIsFilledInTheRequestByTheQueue()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				Request request1 = CreateRequest();
				request1.Behavior.QueuedDate = null;
				Assert.IsNull(request1.Behavior.QueuedDate);
				DateTime before = DateTime.Now;
				queue.Enqueue(request1);
				DateTime after = DateTime.Now;

				Assert.IsNotNull(request1.Behavior.QueuedDate);
				Assert.IsTrue(before <= request1.Behavior.QueuedDate);
				Assert.IsTrue(after >= request1.Behavior.QueuedDate);
			}
		}

		[TestMethod]
		public void MessagesInQueueSurviveApplicationRestart()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				Request request1 = CreateRequest();
				queue.Enqueue(request1);

				queue = new DatabaseRequestQueue(database, "Requests");

				Assert.AreEqual(1, queue.GetCount());
				Request request2 = queue.GetNextRequest();
				Assert.IsTrue(AreEqual(request1, request2));
			}
		}

		[TestMethod]
		public void CanEnqueueRequestsWithNullParameters()
		{
			string tableName = "Requests";
			using (SmartClientDatabase database = GetDatabase())
			{
				Assert.IsFalse(database.TableExists(tableName));

				IRequestQueue queue = new DatabaseRequestQueue(database, tableName);

				Request req = CreateRequest();
				req.CallParameters = CallParameters.ToArray("Test", null, 123, null);

				// Throws Exception
				queue.Enqueue(req);

				Request result = queue.GetNextRequest();
				Assert.IsTrue(AreEqual(req, result));
			}
		}

		[TestMethod]
		[ExpectedException(typeof (RequestManagerException))]
		public void CannotEnquequeRequestsWithSameId()
		{
			string tableName = "Requests";
			using (SmartClientDatabase database = GetDatabase())
			{
				Request request1 = CreateRequest();
				Request request2 = CreateRequest();
				request2.RequestId = request1.RequestId;

				IRequestQueue queue = new DatabaseRequestQueue(database, tableName);

				queue.Enqueue(request1);
				queue.Enqueue(request2);
			}
		}

		[TestMethod]
		public void CanEnqueueRequestWithEndpointName300CharactersLong()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				Request request1 = CreateRequest();
				String name = new string('s', 300);
				request1.Endpoint = name;

				queue.Enqueue(request1);
				
				Assert.AreEqual(1, queue.GetCount());
				Assert.AreEqual(name, queue.GetNextRequest().Endpoint);
			}
		}

		[TestMethod]
		public void CanEnqueueRequestWithProxyFactoryTypeover255CharactersLong()
		{
			using (SmartClientDatabase database = GetDatabase())
			{
				IRequestQueue queue = new DatabaseRequestQueue(database, "Requests");
				Request request1 = CreateRequest();
				request1.OnlineProxyType = typeof(ClassWithReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName);

				queue.Enqueue(request1);

				Assert.AreEqual(1, queue.GetCount());
			}
		}

		private class ClassWithReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyReallyLongName
		{
			
		}

		private Request CreateRequest()
		{
			Request request = new Request();
			request.Endpoint = "Endpoint";
			request.MethodName = "Method";
			request.OnlineProxyType = typeof (DatabaseRequestQueueFixture);
			//request.RequestId = Guid.NewGuid();
			request.Behavior = new OfflineBehavior();
			request.Behavior.ProxyFactoryType = typeof (WebServiceProxyFactory);
			request.Behavior.ExceptionCallback = new CommandCallback(GetType(), "Exception");
			request.Behavior.ReturnCallback = new CommandCallback(GetType(), "Return");
			request.Behavior.Stamps = 5;
			request.Behavior.Tag = "MockRequest";
			request.Behavior.MaxRetries = 0;
			request.Behavior.Expiration = null;
			request.Behavior.MessageId = Guid.NewGuid();
			request.CallParameters =
				CallParameters.ToArray(1, 2, "Charly", new MockParamClass(7, "seven"), 7.5, new MockParamClass(2, "Two"));
			return request;
		}

		private bool AreEqual(Request a, Request b)
		{
			if (ReferenceEquals(a, b))
				return true;

			return (a.Endpoint == b.Endpoint &&
			        a.MethodName == b.MethodName &&
			        a.OnlineProxyType == b.OnlineProxyType &&
			        a.RequestId == b.RequestId &&
			        AreEqual(a.CallParameters, b.CallParameters) &&
			        AreEqual(a.Behavior, b.Behavior));
		}

		public bool AreEqual(object[] a, object[] b)
		{
			if (ReferenceEquals(a, b)) return true;
			if (a == null && b == null) return true;

			if (a.Length != b.Length)
				return false;

			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] == null)
				{
					if (b[i] != null)
						return false;
				}
				else if (!a[i].Equals(b[i]))
					return false;
			}

			return true;
		}

		private bool AreEqual(OfflineBehavior a, OfflineBehavior b)
		{
			if (ReferenceEquals(a, b)) return true;

			return (AreEqual(a.ExceptionCallback, b.ExceptionCallback) &&
			        a.Expiration == b.Expiration &&
			        a.MaxRetries == b.MaxRetries &&
			        a.MessageId == b.MessageId &&
			        AreEqual(a.ReturnCallback, b.ReturnCallback) &&
			        a.Stamps == b.Stamps &&
			        a.Tag == b.Tag);
		}

		private bool AreEqual(CommandCallback a, CommandCallback b)
		{
			return (a.TargetMethodName == b.TargetMethodName &&
			        a.TargetType == b.TargetType);
		}


		private static SmartClientDatabase GetDatabase()
		{
			const int blockSize = 65536;
			Stream resourceStream =
				Assembly.GetExecutingAssembly().GetManifestResourceStream(
					"Microsoft.Practices.SmartClient.EnterpriseLibrary.Tests.Datastore.sdf");
			using (BinaryReader reader = new BinaryReader(resourceStream))
			{
				FileStream output = new FileStream("Datastore.sdf", FileMode.Create);
				using (BinaryWriter writer = new BinaryWriter(output))
				{
					byte[] bytes = reader.ReadBytes(blockSize);
					while (bytes.Length > 0)
					{
						writer.Write(bytes);
						bytes = reader.ReadBytes(blockSize);
					}
				}
			}
			return (SmartClientDatabase) DatabaseFactory.CreateDatabase();
		}
	}

	public class MockParamClass
	{
		public string name;
		public int id;

		public MockParamClass(int id, string name)
		{
			this.id = id;
			this.name = name;
		}

		public MockParamClass()
		{
		}

		public override bool Equals(object obj)
		{
			MockParamClass other = (MockParamClass) obj;
			return (name == other.name && id == other.id);
		}

		public override int GetHashCode()
		{
			return name.GetHashCode() + id.GetHashCode();
		}
	}
}