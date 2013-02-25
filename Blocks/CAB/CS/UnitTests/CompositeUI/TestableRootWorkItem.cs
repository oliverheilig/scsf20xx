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
// CompositeUI Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using Microsoft.Practices.CompositeUI.BuilderStrategies;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.CompositeUI.EventBroker;
using Microsoft.Practices.ObjectBuilder;

namespace Microsoft.Practices.CompositeUI.Tests
{
	public class TestableRootWorkItem : WorkItem
	{
		public TestableRootWorkItem()
		{
			InitializeRootWorkItem(CreateBuilder());

			Services.AddNew<CommandAdapterMapService, ICommandAdapterMapService>();
			Services.AddNew<TraceSourceCatalogService, ITraceSourceCatalogService>();

			TestableAddServices();

			BuildUp();
		}

		protected virtual void TestableAddServices()
		{
		}

		public Builder Builder
		{
			get { return InnerBuilder; }
		}

		public IReadWriteLocator Locator
		{
			get { return InnerLocator; }
		}

		private Builder CreateBuilder()
		{
			Builder builder = new Builder();

			builder.Strategies.AddNew<EventBrokerStrategy>(BuilderStage.Initialization);
			builder.Strategies.AddNew<CommandStrategy>(BuilderStage.Initialization);
            builder.Strategies.AddNew<ObjectBuiltNotificationStrategy>(BuilderStage.PostInitialization);

            builder.Policies.SetDefault<ObjectBuiltNotificationPolicy>(new ObjectBuiltNotificationPolicy());
            builder.Policies.SetDefault<ISingletonPolicy>(new SingletonPolicy(true));

			return builder;
		}
	}
}
