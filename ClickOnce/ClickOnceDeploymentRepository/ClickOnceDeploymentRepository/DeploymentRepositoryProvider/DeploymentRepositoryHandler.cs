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
using System.IO;
using System.Web;

namespace DeploymentRepositoryProvider
{
    /// <summary>
    /// This HTTP handler retrieves files from arbitrary locations by 
    /// using a deployment repository provider to obtain the file based
    /// on its relative path from the web app as a response stream.
    /// </summary>
    public class DeploymentRepositoryHandler : IHttpHandler 
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            // Get the request path and the root path for the web application.
            string requestPath = context.Request.Url.ToString();
            string rootPath = context.Request.ApplicationPath;
            // Get the relative path of the file request from the root.
            requestPath = requestPath.Substring(requestPath.IndexOf(rootPath) + rootPath.Length + 1);
            requestPath = requestPath.Replace('/', '\\');

            // Get an instance of a repository provider from the factory.
            IDeploymentRepository provider = DeploymentRepositoryProviderFactory.CreateInstance();

            // Get the file stream as a byte array from the provider.
            byte[] bits = provider.GetFile(requestPath);
            context.Response.OutputStream.Write(bits, 0, bits.Length);

            // Set the appropriate MIME content type.
            switch (Path.GetExtension(requestPath).ToLower())
            {
                case ".application" :
                    context.Response.ContentType = "application/x-ms-application";
                    break;
                case ".manifest" :
                    context.Response.ContentType = "application.x-ms-manifest";
                    break;
                case ".deploy" :
                    context.Response.ContentType = "application/octet-stream";
                    break;
            }
        }
    }
}
