// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;


namespace ConnectDemoApp
{
    public class AdoConnectionDemo : SQLAzureConnectionDemo
    {
        public AdoConnectionDemo(string userName, string password, string dataSource, string databaseName)
            : base(userName, password, dataSource, databaseName)
        {
        }

        protected override DbConnection CreateConnection(string userName, string password, string dataSource, string databaseName)
        {
            return new SqlConnection(CreateAdoConnectionString(userName, password, dataSource, databaseName));
        }

        private string CreateAdoConnectionString(string userName, string password, string dataSource, string databaseName)
        {
            // create a new instance of the SQLConnectionStringBuilder
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = dataSource,
                InitialCatalog = databaseName,
                Encrypt = true,
                TrustServerCertificate = false,
                UserID = userName,
                Password = password,

            };
            return connectionStringBuilder.ToString();
        }

        protected override DbCommand CreateCommand(DbConnection connection)
        {
            return new SqlCommand() { Connection = connection as SqlConnection };
        }


    }
}
