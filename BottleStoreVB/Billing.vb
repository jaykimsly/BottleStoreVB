Imports System.Data.SqlClient

Public Class Billing
    Private Sub AddBill()
        Try
            Con.Open()
            Dim query = "insert into BillTbl values('" & txtClName.Text & "'," & GrdTotal & ",'" & DateAndTime.Today.Date & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("Bill Saved Sucessfully")
            Con.Close()

        Catch ex As Exception

        End Try
    End Sub
    Private Sub UpdateItem()

        Dim newQty

        If stock - Convert.ToInt32(txtQuantity.Text) <= -1 Then
            MsgBox("Out Of Stock... only " + Convert.ToString(stock) + " beers available")
        Else
            newQty = stock - Convert.ToInt32(txtQuantity.Text)
            Try
                Dim rnum As Integer = BillDGV.Rows.Add()
                i += 1
                Dim total = Convert.ToInt32(txtQuantity.Text) * Convert.ToInt32(txtPrice.Text)
                BillDGV.Rows.Item(rnum).Cells("Column1").Value = i
                BillDGV.Rows.Item(rnum).Cells("Column2").Value = txtItmName.Text
                BillDGV.Rows.Item(rnum).Cells("Column3").Value = txtPrice.Text
                BillDGV.Rows.Item(rnum).Cells("Column4").Value = txtQuantity.Text
                BillDGV.Rows.Item(rnum).Cells("Column5").Value = total
                GrdTotal += total
                Dim tot As String
                tot = "R " + Convert.ToString(GrdTotal)
                TotalLbl.Text = tot
                '====================================================================
                Con.Open()
                Dim query = "Update ItemTbl set ItQty= " & newQty & " where ItId= " & key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("Bill Saved Sucessfully")
                Con.Close()
                displayItem()
            Catch ex As Exception

            End Try
        End If
    End Sub
    Dim i = 0, GrdTotal = 0
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If txtQuantity.Text = "" Then
            MsgBox("Enter the Quantity")
        ElseIf txtPrice.Text = "" Or txtItmName.Text = "" Then
            MsgBox("Select the Item")
        Else



            displayItem()
            UpdateItem()
            Reset()
        End If
    End Sub
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\gugu\Documents\AlcoholSoftwareDB.mdf;Integrated Security=True;Connect Timeout=30")
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
        BDGV.DataSource = ds.Tables(0)
        Con.Close()
    End Sub
    Private Sub Billing_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        displayItem()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub
    Dim key = 0, stock = 0
    Private Sub Reset()
        txtClName.Text = ""
        txtItmName.Text = ""
        txtPrice.Text = ""
        txtQuantity.Text = ""
        ' TotalLbl.Text = "Total"
        key = 0
        stock = 0

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If txtClName.Text = "" Then
            MsgBox("Enter Client name")
        Else
            AddBill()

        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Obj = New Login
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        TotalLbl.Text = "Total"
        BillDGV.Rows.Clear()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Reset()
    End Sub

    Private Sub ItemDGV_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles BDGV.CellMouseClick
        Dim row As DataGridViewRow = BDGV.Rows(e.RowIndex)
        txtItmName.Text = row.Cells(1).Value.ToString
        txtPrice.Text = row.Cells(3).Value.ToString

        If txtItmName.Text = "" Then
            key = 0
        Else
            key = Convert.ToInt32(row.Cells(0).Value.ToString)
            stock = Convert.ToInt32(row.Cells(2).Value.ToString)
        End If

    End Sub
End Class