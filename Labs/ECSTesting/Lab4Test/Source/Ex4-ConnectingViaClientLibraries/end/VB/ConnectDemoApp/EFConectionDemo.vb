' ----------------------------------------------------------------------------------
' Microsoft Developer & Platform Evangelism
' 
' Copyright (c) Microsoft Corporation. All rights reserved.
' 
' THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
' EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
' OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
' ----------------------------------------------------------------------------------
' The example companies, organizations, products, domain names,
' e-mail addresses, logos, people, places, and events depicted
' herein are fictitious.  No association with any real company,
' organization, product, domain name, email address, logo, person,
' places, or events is intended or should be inferred.
' ----------------------------------------------------------------------------------

Public Class EFConectionDemo
    ''' <summary>
    ''' HolTestDBEntities takes care of handling your transactions for you
    ''' leaving you free you use Linq to extraxt information stored up in the cloud.
    ''' </summary>
    Public Sub ConnectToSQLAzureDemo()
        Dim context As New HolTestDBEntities()

        ' get all company names
        Dim companyNames As IQueryable(Of String) = From customer In context.Customers _
                                                    Where customer.CustomerID < 20 _
                                                    Select customer.CompanyName

        ' display these all on the console
        For Each company As String In companyNames
            Console.WriteLine(company)
        Next company
    End Sub
End Class
