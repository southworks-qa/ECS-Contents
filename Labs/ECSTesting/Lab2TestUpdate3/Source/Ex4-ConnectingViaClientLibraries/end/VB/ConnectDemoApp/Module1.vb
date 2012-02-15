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

Module Module1

    ' Connection Variables. Please replace the values with those discussed in the lab.

    Public _userName As String = "[USERNAME]"
    Public _password As String = "[PASSWORD]"
    Public _datasource As String = "[SERVER].database.windows.net"
    Public _databaseName As String = "HoLTestDB"

    Sub Main()
        ' Invoke the ADO.NET connection demo
        Console.WriteLine("Starting the ADO.NET Connection Demo..")
        Dim demo1 = New AdoConnectionDemo(_userName, _password, _datasource, _databaseName)
        demo1.ConnectToSQLAzureDemo()
        Console.WriteLine("Demo Complete.. Press any key")
        Console.ReadKey()

        ' Invoke the ODBC connection demo
        Console.WriteLine("Starting the ODBC Connection Demo..")
        Dim demo2 = New OdbcConnectionDemo(_userName, _password, _datasource, _databaseName)
        demo2.ConnectToSQLAzureDemo()
        Console.WriteLine("Demo Complete.. Press any key")
        Console.ReadKey()

        ' Invoke the OleDB connection demo
        Console.WriteLine("Starting the OLEDB Connection Demo..")
        Dim demo3 = New OleDbConnectionDemo(_userName, _password, _datasource, _databaseName)
        demo3.ConnectToSQLAzureDemo()
        Console.WriteLine("Demo Complete.. Press any key")
        Console.ReadKey()

        ' Invoke the Entity Framework connection demo
        Console.WriteLine("Starting the Entity Framework Connection Demo..")
        Dim demo4 = New EFConectionDemo()
        demo4.ConnectToSQLAzureDemo()
        Console.WriteLine("Demo Complete.. Press any key")
        Console.ReadKey()
    End Sub

End Module
