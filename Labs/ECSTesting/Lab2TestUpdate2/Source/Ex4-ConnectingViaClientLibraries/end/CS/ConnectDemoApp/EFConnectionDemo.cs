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

namespace ConnectDemoApp
{
    public class EFConnectionDemo
    {

        /// <summary>
        /// HolTestDBEntities takes care of handling your transactions for you
        /// leaving you free use Linq to extract information stores up in th cloud
        /// </summary>
        public void ConnectToSQLAzureDemo()
        {
            HolTestDBEntities context = new HolTestDBEntities();

            IQueryable<string> companyNames = from customer in context.Customers
                                              where customer.CustomerID < 20
                                              select customer.CompanyName;
            foreach (var company in companyNames)
            {
                Console.WriteLine(company);
            }
        }
    }
}
