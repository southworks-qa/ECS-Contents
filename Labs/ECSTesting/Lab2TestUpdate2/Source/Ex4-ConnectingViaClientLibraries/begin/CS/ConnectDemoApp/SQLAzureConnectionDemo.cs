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
using System.Data;
using System.Data.Common;
using System.Text;

namespace ConnectDemoApp
{
    public abstract class SQLAzureConnectionDemo
    {
        private DbConnection connection = null;

        public SQLAzureConnectionDemo(string userName, string password, string dataSource, string databaseName)
        {
            this.connection = CreateConnection(userName, password, dataSource, databaseName);
        }

        protected abstract DbConnection CreateConnection(string userName, string password, string dataSource, string databaseName);

        protected abstract DbCommand CreateCommand(DbConnection connection);

        /// <summary>
        /// Splits a fully qualified domain name datasource the following format
        /// servername.path.to.server;
        /// </summary>
        /// <param name="dataSource">The fully qualified domain name of your SQL Azure server</param>
        /// <returns>servername</returns>
        protected string GetServerName(string dataSource)
        {
            return dataSource.Split('.')[0];
        }


        public void ConnectToSQLAzureDemo()
        {
            connection.Open();
            DbCommand command = CreateCommand(connection);

            ExecuteCreateDemoTableStatement(command);
            ExecuteInsertTestDataStatement(command);
            ExecuteReadInsertedTestData(command);
            ExecuteDropDemoTable(command);
            connection.Close();
        }

        /// <summary>
        /// Creates a DemoTable two columns, DemoId(int) and DemoName(varchar(20))
        /// </summary>
        /// <param name="command">A command attached to a connection</param>
        private void ExecuteCreateDemoTableStatement(DbCommand command)
        {
            Console.WriteLine("Creating DemoTable..");
            command.CommandText = "CREATE TABLE DemoTable(DemoId int primary key, DemoName varchar(20))";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Inserts 5 new rows into your demo table
        /// </summary>
        /// <param name="command">A command attached to a connection</param>
        private void ExecuteInsertTestDataStatement(DbCommand command)
        {
            Console.WriteLine("Adding some test data..");

            for (int data = 0; data < 5; data++)
            {
                string commandText = string.Format("INSERT INTO DemoTable (DemoId, DemoName) values ({0}, 'Demo {0}')", data);

                Console.WriteLine(commandText);
                command.CommandText = commandText;

                command.ExecuteNonQuery();

            }

            Console.WriteLine("Done..");
            Console.WriteLine("Press any key to read back your data from the cloud..");
            Console.ReadKey();
        }

        /// <summary>
        /// Selects all the values from DemoTable and outputs the results to the console
        /// </summary>
        /// <param name="command">A command attached to a connection</param>
        private void ExecuteReadInsertedTestData(DbCommand command)
        {
            string selectText = "SELECT * FROM DemoTable";

            Console.WriteLine("Reading Data..");
            Console.WriteLine(selectText);

            command.CommandText = selectText;

            ReadData(command.ExecuteReader());
        }

        /// <summary>
        /// Reads data and out puts the results to the console.
        /// </summary>
        /// <param name="reader"></param>
        private void ReadData(IDataReader reader)
        {
            // loop over the results and write them out to the console
            while (reader.Read())
            {
                StringBuilder row = new StringBuilder();
                for (int col = 0; col < reader.FieldCount; col++)
                {
                    row.Append(reader.GetName(col) + ":" + reader.GetValue(col) + "  |  ");
               }
                Console.WriteLine(row.ToString());
            }
            reader.Close();
        }


        /// <summary>
        /// Drops the DemoTable.
        /// </summary>
        /// <param name="command">A command attached to a connection</param>
        private void ExecuteDropDemoTable(DbCommand command)
        {
            Console.WriteLine("Removing DemoTable..");

            command.CommandText = "DROP TABLE DemoTable";
            command.ExecuteNonQuery();
        }

    }
}