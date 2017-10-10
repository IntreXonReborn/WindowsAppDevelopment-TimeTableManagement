Imports MySql.Data.MySqlClient

Public Class facultypage

    Dim MySQLConnection As New MySqlConnection("host=localhost; user=root; database=iwp; convert zero datetime=True")
    Dim command As New MySqlCommand
    Dim adapter As New MySqlDataAdapter
    Public table As New DataTable()

    Public username As String

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
        controlzone.TextBox1.Text = ""
        controlzone.TextBox2.Text = ""
        controlzone.TextBox3.Text = ""
        controlzone.TextBox4.Text = ""
        controlzone.TextBox5.Text = ""
        controlzone.TextBox6.Text = ""
        controlzone.Show()
    End Sub

    Private Sub facultypage_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim currentArea = Screen.FromControl(Me).WorkingArea
        Me.Top = currentArea.Top + CInt((currentArea.Height / 2) - (Me.Height / 2))
        Me.Left = currentArea.Left + CInt((currentArea.Width / 2) - (Me.Width / 2))

        MySQLConnection.Open()
        username = controlzone.TextBox5.Text
        Dim sql_query As String = "select * from teacher where Teacher_ID = @value "
        command = New MySqlCommand(sql_query, MySQLConnection)
        command.Parameters.AddWithValue("@value", username)
        adapter = New MySqlDataAdapter(command)

        adapter.Fill(table)

        Label5.Text = table.Rows(0)(1)
        Label13.Text = table.Rows(0)(2)
        Label3.Text = table.Rows(0)(3)
        Label8.Text = table.Rows(0)(4)

        Dim sql_query2 As String = "select Dept_Name from department where Dept_No = @value2 "
        Dim command2 As New MySqlCommand(sql_query2, MySQLConnection)
        command2.Parameters.AddWithValue("@value2", table.Rows(0)(6).ToString())
        Dim adapter2 As New MySqlDataAdapter(command2)
        Dim table2 As New DataTable()
        adapter2.Fill(table2)
        Label10.Text = table.Rows(0)(5).ToString() + " years"
        Label12.Text = table2.Rows(0)(0)
        Label15.Text = table.Rows(0)(8)
        MySQLConnection.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        facultycourseapply.Show()
    End Sub
End Class