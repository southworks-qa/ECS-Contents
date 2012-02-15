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

Imports System.Data.Common
Imports System.Text

Public MustInherit Class SQLAzureConnectionDemo
    Private connection As DbConnection = Nothing

    Public Sub New(ByVal userName As String, ByVal password As String, ByVal dataSource As String, ByVal databaseName As String)
        Me.connection = CreateConnection(userName, password, dataSource, databaseName)
    End Sub

    Protected MustOverride Function CreateConnection(ByVal userName As String, ByVal password As String, ByVal dataSource As String, ByVal databaseName As String) As DbConnection

    Protected MustOverride Function CreateCommand(ByVal connection As DbConnection) As DbCommand

    ''' <summary>
    ''' Splits a fully qualified domain name datasource the following format
    ''' servername.path.to.server;
    ''' </summary>
    ''' <param name="dataSource">The fully qualified domain name of your SQL Azure server</param>
    ''' <returns>servername</returns>
    Protected Function GetServerName(ByVal dataSource As String) As String
        Return dataSource.Split("."c)(0)
    End Function


    Public Sub ConnectToSQLAzureDemo()
        connection.Open()
        Dim command As DbCommand = CreateCommand(connection)

        ExecuteCreateDemoTableStatement(command)
        ExecuteInsertTestDataStatement(command)
        ExecuteReadInsertedTestData(command)
        ExecuteDropDemoTable(command)
        connection.Close()
    End Sub

    ''' <summary>
    ''' Creates a DemoTable two columns, DemoId(int) and DemoName(varchar(20))
    ''' </summary>
    ''' <param name="command">A command attached to a connection</param>
    Private Sub ExecuteCreateDemoTableStatement(ByVal command As DbCommand)
        Console.WriteLine("Creating DemoTable..")
        command.CommandText = "CREATE TABLE DemoTable(DemoId int primary key, DemoName varchar(20))"
        command.ExecuteNonQuery()
    End Sub

    ''' <summary>
    ''' Inserts 5 new rows into your demo table
    ''' </summary>
    ''' <param name="command">A command attached to a connection</param>
    Private Sub ExecuteInsertTestDataStatement(ByVal command As DbCommand)
        Console.WriteLine("Adding some test data..")

        For data As Integer = 0 To 4
            Dim commandText As String = String.Format("INSERT INTO DemoTable (DemoId, DemoName) values ({0}, 'Demo {0}')", data)

            Console.WriteLine(commandText)
            command.CommandText = commandText

            command.ExecuteNonQuery()

        Next data

        Console.WriteLine("Done..")
        Console.WriteLine("Press any key to read back your data from the cloud..")
        Console.ReadKey()
    End Sub

    ''' <summary>
    ''' Selects all the values from DemoTable and outputs the results to the console
    ''' </summary>
    ''' <param name="command">A command attached to a connection</param>
    Private Sub ExecuteReadInsertedTestData(ByVal command As DbCommand)
        Dim selectText As String = "SELECT * FROM DemoTable"

        Console.WriteLine("Reading Data..")
        Console.WriteLine(selectText)

        command.CommandText = selectText

        ReadData(command.ExecuteReader())
    End Sub

    ''' <summary>
    ''' Reads data and out puts the results to the console.
    ''' </summary>
    ''' <param name="reader"></param>
    Private Sub ReadData(ByVal reader As IDataReader)
        ' loop over the results and write them out to the console
        Do While reader.Read()
            Dim row As New StringBuilder()
            For col As Integer = 0 To reader.FieldCount - 1
                row.Append(reader.GetName(col) & ":" & reader.GetValue(col) & "  |  ")
            Next col
            Console.WriteLine(row.ToString())
        Loop
        reader.Close()
    End Sub


    ''' <summary>
    ''' Drops the DemoTable.
    ''' </summary>
    ''' <param name="command">A command attached to a connection</param>
    Private Sub ExecuteDropDemoTable(ByVal command As DbCommand)
        Console.WriteLine("Removing DemoTable..")

        command.CommandText = "DROP TABLE DemoTable"
        command.ExecuteNonQuery()
    End Sub

End Class

