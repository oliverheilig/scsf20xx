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
using Microsoft.Practices.CompositeUI;

public class Module : ModuleInit
{
	private WorkItem _rootWorkItem;

	public Module([ServiceDependency] WorkItem rootWorkItem)
	{
		_rootWorkItem = rootWorkItem;
		Console.WriteLine("Module..ctor()");
	}
	
	public override void AddServices()
	{
		base.AddServices();
		Console.WriteLine("Module.AddServices()");
	}

	public override void Load()
	{
		base.Load();
		Console.WriteLine("Module.Load()");

		_rootWorkItem.WorkItems.AddNew<MyWorkItem>().Activate();
	}
}

public class MyWorkItem : WorkItem
{
	public MyWorkItem()
	{
		Console.WriteLine("MyWorkItem..ctor()");
	}
}

[WorkItemExtension(typeof(MyWorkItem))]
public class WorkItemExt : WorkItemExtension
{
	public WorkItemExt()
	{
		Console.WriteLine("WorkItemExt..ctor()");
	}

	protected override void OnActivated()
	{
		base.OnActivated();
		Console.WriteLine("WorkItemExt.OnActivated()");
	}

	protected override void OnInitialized()
	{
		base.OnInitialized();
		Console.WriteLine("WorkItemExt.OnInitialized()");
	}

	protected override void OnTerminated()
	{
		base.OnTerminated();
		Console.WriteLine("WorkItemExt.OnTerminated()");
	}
}
