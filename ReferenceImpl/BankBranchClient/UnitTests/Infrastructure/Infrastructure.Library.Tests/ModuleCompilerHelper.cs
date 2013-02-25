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
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using Microsoft.Practices.CompositeUI;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GlobalBank.Infrastructure.Library.Tests
{
	class ModuleCompilerHelper
	{
		private static Dictionary<string, Assembly> _generatedAssemblies = new Dictionary<string, Assembly>();

		private static string _moduleTemplate = @"
using System;
using System.ComponentModel;
using Microsoft.Practices.CompositeUI;
using System.Diagnostics;

#module#
#dependencies#

namespace TestModules
{
	public class #className#Class : ModuleInit
	{
		public override void AddServices()
		{
			Trace.Write(""#className#.AddServices"");
			Console.WriteLine(""#className#.AddServices"");
		}

		public override void Load()
		{
			Trace.Write(""#className#.Start"");
			Console.WriteLine(""#className#.Start"");
		}
	}
}";

		static ModuleCompilerHelper()
		{
            AppDomain.CurrentDomain.SetData("APPBASE", Environment.CurrentDirectory);
            
            _generatedAssemblies.Add("ModuleExposingServices",
                            CompileFileAndLoadAssembly(
                                    "GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleExposingServices.cs",
									@".\ModuleExposingServices1\ModuleExposingServices.dll"));

			_generatedAssemblies.Add("ModuleExposingSameServices",
                            CompileFileAndLoadAssembly(
									"GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleExposingSameServices.cs",
									@".\ModuleExposingSameServices\ModuleExposingSameServices.dll"));

            CompileFile(@"GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleReferencedAssembly.cs",
									@".\ModuleReferencingAssembly\ModuleReferencedAssembly.dll");

			_generatedAssemblies.Add("ModuleReferencingAssembly",
                            CompileFileAndLoadAssembly(
                                    "GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleReferencingAssembly.cs",
									@".\ModuleReferencingAssembly\ModuleReferencingAssembly.dll",
									@".\ModuleReferencingAssembly\ModuleReferencedAssembly.dll"));

			_generatedAssemblies.Add("ModuleThrowingException",
                            CompileFileAndLoadAssembly(
                                    "GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleThrowingException.cs",
									@".\ModuleThrowingException\ModuleThrowingException.dll"));

			_generatedAssemblies.Add("ModuleExposingOnlyServices",
                            CompileFileAndLoadAssembly(
                                    "GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleExposingOnlyServices.cs",
									@".\ModuleExposingOnlyServices\ModuleExposingOnlyServices.dll"));

			_generatedAssemblies.Add("ModuleExposingDuplicatedServices",
                            CompileFileAndLoadAssembly(
                                    "GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleExposingDuplicatedServices.cs",
									@".\ModuleExposingDuplicatedServices\ModuleExposingDuplicatedServices.dll"));

			_generatedAssemblies.Add("ModuleDependency1",
                            CompileFileAndLoadAssembly("GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleDependency1.cs",
									@".\ModuleDependency1\ModuleDependency1.dll"));

			_generatedAssemblies.Add("ModuleDependency2",
                            CompileFileAndLoadAssembly("GlobalBank.Infrastructure.Library.Tests.Mocks.Src.ModuleDependency2.cs",
									@".\ModuleDependency2\ModuleDependency2.dll"));

			_generatedAssemblies.Add("WorkItemExtension",
                            CompileFileAndLoadAssembly("GlobalBank.Infrastructure.Library.Tests.Mocks.Src.WorkItemExtension.cs",
									@".\WorkItemExtension\WorkItemExtension.dll"));
		}

		public static IDictionary<string, Assembly> GeneratedAssemblies
		{
			get { return _generatedAssemblies; }
		}

		public static string GenerateDynamicModule(string assemblyName, string moduleName, params string[] dependencies)
		{
			string assemblyFile = assemblyName + ".dll";

			if (!Directory.Exists(assemblyName))
				Directory.CreateDirectory(assemblyName);

			string outpath = Path.Combine(assemblyName, assemblyFile);

			if (File.Exists(outpath))
				File.Delete(outpath);

			string moduleCode = _moduleTemplate.Replace("#className#", assemblyName);

			if (moduleName != null && moduleName.Length > 0)
				moduleCode = moduleCode.Replace("#module#", @"[assembly: Module(""" + moduleName + @""")]");
			else
				moduleCode = moduleCode.Replace("#module#", "");

			string depString = String.Empty;

			foreach (string module in dependencies)
				depString += String.Format("[assembly: ModuleDependency(\"{0}\")]\r\n", module);
			
			moduleCode = moduleCode.Replace("#dependencies#", depString);
			CompileCode(moduleCode, outpath);

			return outpath;
		}

        public static Assembly CompileFileAndLoadAssembly(string input, string output, params string[] references)
        {
            return CompileFile(input, output, references).CompiledAssembly;
        }

        private static CompilerResults CompileFile(string input, string output, params string[] references)
		{
			EnsureOutputDirectoryExists(output);

			List<string> referencedAssemblies = new List<string>(references.Length + 3);

			referencedAssemblies.AddRange(references);
			referencedAssemblies.Add("System.dll");
			referencedAssemblies.Add(typeof(IModule).Assembly.CodeBase.Replace(@"file:///", ""));
			referencedAssemblies.Add(typeof(IBuilderAware).Assembly.CodeBase.Replace(@"file:///", ""));

			CSharpCodeProvider codeProvider = new CSharpCodeProvider();
			CompilerParameters cp = new CompilerParameters(referencedAssemblies.ToArray(), output);

			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(input))
			{
				if (stream == null)
					throw new ArgumentException("input");

				StreamReader reader = new StreamReader(stream);
				string source = reader.ReadToEnd();
				CompilerResults results = codeProvider.CompileAssemblyFromSource(cp, source);
				ThrowIfCompilerError(results);
				return results;
			}
		}

		private static Assembly CompileCode(string code, string outputFilename)
		{
			EnsureOutputDirectoryExists(outputFilename);
			CodeCompileUnit unit = new CodeSnippetCompileUnit(code);
			unit.ReferencedAssemblies.Add("System");
			unit.ReferencedAssemblies.Add("Common");

			CompilerResults results = new CSharpCodeProvider().CompileAssemblyFromSource(
					new CompilerParameters(
							new string[] { "System.dll", typeof(IModule).Assembly.CodeBase.Replace(@"file:///", "") },
							outputFilename), code);

			ThrowIfCompilerError(results);
			return results.CompiledAssembly;
		}

		private static void EnsureOutputDirectoryExists(string outputFilename)
		{
			string dir = Path.GetDirectoryName(outputFilename);

			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
		}

		private static void ThrowIfCompilerError(CompilerResults results)
		{
			if (results.Errors.HasErrors)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("Compilation failed.");

				foreach (CompilerError error in results.Errors)
					sb.AppendLine(error.ToString());
				
				Assert.IsFalse(results.Errors.HasErrors, sb.ToString());
			}
		}
	}
}
