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
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

/// <summary>
/// Summary description for TestHelper
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FunctionalTestHelper : WebService
{

    public FunctionalTestHelper()
    {
    }

    [WebMethod]
    public void ReInitializeGlobalBank()
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["GlobalBankConnection"].ConnectionString))
        {
            conn.Open();

            SqlCommand cmd = new SqlCommand("sp_InitializeGlobalBankForTesting", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.ExecuteNonQuery();
        }
    }

}

