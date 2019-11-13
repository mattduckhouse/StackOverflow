Imports System
Imports System.Data
Imports Newtonsoft.Json

Module Program
    Sub Main(args As String())

        Dim dataSet As DataSet = New DataSet("dataSet")
        dataSet.[Namespace] = "NetFrameWork"
        Dim jsonTable As DataTable = New DataTable()
        Dim keyColumn As DataColumn = New DataColumn("Key")
        Dim valueColumn As DataColumn = New DataColumn("Value")
        jsonTable.Columns.Add(keyColumn)
        jsonTable.Columns.Add(valueColumn)
        dataSet.Tables.Add(jsonTable)

        jsonTable.TableName = "Data"

        For m = 1 To 3
            Dim dataRow2 As DataRow = jsonTable.NewRow
            dataRow2("Key") = "Timestamp"
            dataRow2("Value") = "Value"
            jsonTable.Rows.Add(dataRow2)

            For k = 1 To 3
                Dim dataRow1 As DataRow = jsonTable.NewRow
                dataRow1("Key") = k
                dataRow1("Value") = k
                jsonTable.Rows.Add(dataRow1)
            Next k
        Next m
        dataSet.AcceptChanges()

        Dim holder = New JsonHolder With {
            .DataflowId = 1234,
            .Data = jsonTable
        }

        Dim json = JsonConvert.SerializeObject(holder, Formatting.Indented)

    End Sub


    Public Class JsonHolder
        Public DataflowId As Integer
        Public Data As DataTable
    End Class

End Module
