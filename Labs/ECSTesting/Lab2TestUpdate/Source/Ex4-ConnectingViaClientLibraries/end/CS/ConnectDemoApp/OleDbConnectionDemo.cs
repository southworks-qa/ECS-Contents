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
using System.Data.OleDb;


namespace ConnectDemoApp
{
    public class OleDbConnectionDemo : SQLAzureConnectionDemo
    {
        public OleDbConnectionDemo(string userName, string password, string dataSource, string databaseName)
            : base(userName, password, dataSource, databaseName)
        {
        }

        protected override DbConnection CreateConnection(string userName, string password, string dataSource, string databaseName)
        {
            return new OleDbConnection(CreateOleDBConnectionString(userName, password, dataSource, databaseName));
        }

        private string CreateOleDBConnectionString(string userName, string password, string dataSource, string databaseName)
        {
            string serverName = GetServerName(dataSource);

            OleDbConnectionStringBuilder connectionStringBuilder = new OleDbConnectionStringBuilder
            {
                Provider = "SQLOLEDB",
                DataSource = dataSource,

            };
            connectionStringBuilder["Initial Catalog"] = databaseName;
            connectionStringBuilder["UId"] = userName + "@" + serverName;
            connectionStringBuilder["Pwd"] = password;

            return connectionStringBuilder.ConnectionString;
        }

        protected override DbCommand CreateCommand(DbConnection connection)
        {
            return new OleDbCommand() { Connection = connection as OleDbConnection };
        }


    }
}
