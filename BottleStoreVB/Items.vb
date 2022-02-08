Imports System.Data.SqlClient

Public Class Items
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gugu\Documents\AlcoholSoftwareDB.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtItmName.Text = "" Or txtQuantity.Text = "" Or txtPrice.Text = "" Or cmbCategories.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "insert into ItemTbl values('" & txtItmName.Text & "'," & txtQuantity.Text & "," & txtPrice.Text & ",'" & cmbCategories.SelectedItem.ToString() & "')"
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Saved Sucessfully")
                Con.Close()
                displayItem()
                Clear()
            Catch ex As Exception

            End Try
        End If
    End Sub
    Private Sub Clear()
        txtItmName.Text = ""
        txtQuantity.Text = ""
        txtPrice.Text = ""
        cmbCategories.SelectedIndex = 0
    End Sub
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Clear()
    End Sub
    Private Sub displayItem()
        Con.Open()
        Dim query = "select * from ItemTbl"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)

        Con.Close()
    End Sub
    Private Sub FilterByCart()
        Con.Open()
        Dim query = "select * from ItemTbl where ItCat = '" & Searchcmb.SelectedItem.ToString() & "'"
        Dim cmd = New SqlCommand(query, Con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        ItemDGV.DataSource = ds.Tables(0)

        Con.Close()
    End Sub
    Dim key = 0
    Private Sub ItemDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles ItemDGV.CellMouseClick
        Dim row As DataGridViewRow = ItemDGV.Rows(e.RowIndex)
        txtItmName.Text = row.Cells(1).Value.ToString
        txtQuantity.Text = row.Cells(2).Value.ToString
        txtPrice.Text = row.Cells(3).Value.ToString
        cmbCategories.SelectedItem = row.Cells(4).Value.ToString

        If txtItmName.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If


    End Sub

    Private Sub Items_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        displayItem()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If key = 0 Then
            MsgBox("Select Item to Delete")
        Else
            Try
                Con.Open()
                Dim query = "Delete from ItemTbl where ItId = " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Deleted Sucessfully")
                Con.Close()
                displayItem()
                Clear()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        If txtItmName.Text = "" Or txtQuantity.Text = "" Or txtPrice.Text = "" Or cmbCategories.Text = "" Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "Update ItemTbl set ItName='" & txtItmName.Text & "', ItQty= " & txtQuantity.Text & ",ItPrices=" & txtPrice.Text & ",ItCat='" & cmbCategories.SelectedItem.ToString & "' where ItId= " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Item Saved Sucessfully")
                Con.Close()
                displayItem()
                Clear()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub ComboBox1_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Searchcmb.SelectionChangeCommitted
        FilterByCart()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        displayItem()
    End Sub
End Class