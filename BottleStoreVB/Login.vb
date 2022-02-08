Public Class Login

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Application.Exit()
    End Sub

    Private Sub lblSeller_Click(sender As Object, e As EventArgs) Handles lblSeller.Click
        Dim Obj = New Billing
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If txtBUsername.Text = "" Or txtBPassword.Text = "" Then
            MsgBox("Enter Username and Password")
        ElseIf txtBUsername.Text = "Konka" And txtBPassword.Text = "DuMo" Then
            Dim obj = New Items
            obj.Show()
            Me.Hide()
        Else
            MsgBox("Wrong Username or Password")
            txtBUsername.Text = ""
            txtBPassword.Text = ""
        End If
    End Sub
End Class