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

using System;
using Microsoft.Practices.ObjectBuilder;

namespace Microsoft.Practices.CompositeUI
{
	/// <summary>
	/// Indicates that property or parameter is a dependency on a service and
	/// should be dependency injected when the class is put into a <see cref="WorkItem"/>.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
	public sealed class ServiceDependencyAttribute : OptionalDependencyAttribute
	{
		private Type type;

		/// <summary>
		/// Initializes a new instance of the <see cref="ServiceDependencyAttribute"/> class.
		/// </summary>
		public ServiceDependencyAttribute()
		{
		}

		/// <summary>
		/// Gets or sets the type of the service the property expects.
		/// </summary>
		public Type Type
		{
			get { return type; }
			set { type = value; }
		}

		/// <summary>
		/// See <see cref="ParameterAttribute.CreateParameter"/> for more information.
		/// </summary>
		public override IParameter CreateParameter(Type memberType)
		{
			return new ServiceDependencyParameter(type ?? memberType, Required);
		}

		class ServiceDependencyParameter : IParameter
		{
			Type serviceType;
			bool ensureExists;
			
			public ServiceDependencyParameter(Type serviceType, bool ensureExists)
			{
				this.serviceType = serviceType;
				this.ensureExists = ensureExists;
			}

			public Type GetParameterType(IBuilderContext context)
			{
				return serviceType;
			}

			public object GetValue(IBuilderContext context)
			{
				WorkItem workItem = (WorkItem)context.Locator.Get(new DependencyResolutionLocatorKey(typeof(WorkItem), null));
				return workItem.Services.Get(serviceType, ensureExists);
			}
		}
	}
}