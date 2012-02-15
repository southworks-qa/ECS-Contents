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
Imports System.Data.OleDb

Public Class OleDbConnectionDemo
    Inherits SQLAzureConnectionDemo

    Public Sub New(ByVal userName As String, ByVal password As String, ByVal dataSource As String, ByVal databaseName As String)
        MyBase.New(userName, password, dataSource, databaseName)
    End Sub

    Protected Overrides Function CreateCommand(ByVal connection As DbConnection) As DbCommand
        Return New OleDbCommand() With {.Connection = TryCast(connection, OleDbConnection)}
    End Function

    Protected Overrides Function CreateConnection(ByVal userName As String, ByVal password As String, ByVal dataSource As String, ByVal databaseName As String) As DbConnection
        Return New OleDbConnection(CreateOleDBConnectionString(userName, password, dataSource, databaseName))
    End Function

    Private Function CreateOleDBConnectionString(ByVal userName As String, ByVal password As String, ByVal dataSource As String, ByVal databaseName As String) As String
        Dim serverName As String = GetServerName(dataSource)

        Dim connectionStringBuilder As OleDbConnectionStringBuilder = New OleDbConnectionStringBuilder With {.Provider = "SQLOLEDB", .DataSource = dataSource}
        connectionStringBuilder("Initial Catalog") = databaseName
        connectionStringBuilder("UId") = userName & "@" & serverName
        connectionStringBuilder("Pwd") = password

        Return connectionStringBuilder.ConnectionString
    End Function

End Class
