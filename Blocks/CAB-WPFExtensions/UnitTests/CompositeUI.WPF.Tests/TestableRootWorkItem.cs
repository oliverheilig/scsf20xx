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
using System.Text;
using Microsoft.Practices.CompositeUI.Commands;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeUI.BuilderStrategies;
using Microsoft.Practices.CompositeUI.WinForms;
using Microsoft.Practices.CompositeUI.WPF.Tests.Workspaces;
using Microsoft.Practices.CompositeUI.WPF;
using Microsoft.Practices.CompositeUI.WPF.BuilderStrategies;

namespace Microsoft.Practices.CompositeUI.WPF.Tests
{
    public class TestableRootWorkItem: WorkItem
	{
        public TestableRootWorkItem()
		{
			InitializeRootWorkItem(CreateBuilder());

			Services.AddNew<CommandAdapterMapService, ICommandAdapterMapService>();
			Services.AddNew<ControlActivationService, IControlActivationService>();
            Services.AddNew<MockWPFUIElementAdapter, IWPFUIElementAdapter>();
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

			builder.Strategies.AddNew<WinFormServiceStrategy>(BuilderStage.Initialization);
			builder.Strategies.AddNew<EventBrokerStrategy>(BuilderStage.Initialization);
			builder.Strategies.AddNew<CommandStrategy>(BuilderStage.Initialization);
			builder.Strategies.AddNew<ControlActivationStrategy>(BuilderStage.Initialization);
			builder.Strategies.AddNew<ControlSmartPartStrategy>(BuilderStage.Initialization);
            builder.Strategies.AddNew<ObjectBuiltNotificationStrategy>(BuilderStage.PostInitialization);
            builder.Strategies.AddNew<WPFControlSmartPartStrategy>(BuilderStage.Initialization);

            builder.Policies.SetDefault<ObjectBuiltNotificationPolicy>(new ObjectBuiltNotificationPolicy());
            builder.Policies.SetDefault<ISingletonPolicy>(new SingletonPolicy(true));

			return builder;
		}
	}
}
