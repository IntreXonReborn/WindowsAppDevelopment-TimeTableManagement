Imports MySql.Data.MySqlClient

Public Class controlzone

    Dim MySQLConnection As New MySqlConnection("host=localhost; user=root; database=iwp")

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim command As New MySqlCommand("select * from admin where Admin_ID= @username and Admin_PW= @password", MySQLConnection)

        command.Parameters.AddWithValue("@username", TextBox1.Text.ToString())
        command.Parameters.AddWithValue("@password", TextBox2.Text.ToString())

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        If table.Rows.Count() <= 0 Then
            MessageBox.Show("Invalid Credentials. Please try again.")
        Else
            adminpage.Show()
            Me.Hide()

        End If


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim command As New MySqlCommand("select * from student where Student_ID= @username and Student_PW= @password", MySQLConnection)

        command.Parameters.AddWithValue("@username", TextBox3.Text.ToString())
        command.Parameters.AddWithValue("@password", TextBox4.Text.ToString())

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        If table.Rows.Count() <= 0 Then
            MessageBox.Show("Invalid Credentials. Please try again.")
        Else
            Form5.Show()
            Me.Hide()

        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox3.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim command As New MySqlCommand("select * from teacher where Teacher_ID= @username and Teacher_PW= @password", MySQLConnection)

        command.Parameters.AddWithValue("@username", TextBox5.Text.ToString())
        command.Parameters.AddWithValue("@password", TextBox6.Text.ToString())

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        If table.Rows.Count() <= 0 Then
            MessageBox.Show("Invalid Credentials. Please try again.")
        Else
            Me.Hide()
            facultypage.Show()
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        TextBox5.Text = ""
        TextBox6.Text = ""
    End Sub

    Private Sub controlzone_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim currentArea = Screen.FromControl(Me).WorkingArea
        Me.Top = currentArea.Top + CInt((currentArea.Height / 2) - (Me.Height / 2))
        Me.Left = currentArea.Left + CInt((currentArea.Width / 2) - (Me.Width / 2))
    End Sub
End Class
