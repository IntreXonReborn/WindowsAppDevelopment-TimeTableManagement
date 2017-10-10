Imports MySql.Data.MySqlClient

Public Class facultycourseapply

    Dim MySQLConnection As New MySqlConnection("host=localhost; user=root; database=iwp; convert zero datetime=True")

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        facultypage.Show()
    End Sub

    Private Sub facultycourseapply_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim currentArea = Screen.FromControl(Me).WorkingArea
        Me.Top = currentArea.Top + CInt((currentArea.Height / 2) - (Me.Height / 2))
        Me.Left = currentArea.Left + CInt((currentArea.Width / 2) - (Me.Width / 2))

        MySQLConnection.Open()
        DataGridView1.ColumnCount = 4
        DataGridView1.Columns(0).Name = "Course_ID"
        DataGridView1.Columns(1).Name = "Course_Name"
        DataGridView1.Columns(2).Name = "Course_Credit"
        DataGridView1.Columns(3).Name = "Course_Slot"

        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Rows.Clear()

        Dim sql_query As String = "SELECT Course_ID, Course_Name, Course_Credit, Course_Slot from course where Dept_No = @value "
        Dim command As New MySqlCommand(sql_query, MySQLConnection)
        command.Parameters.AddWithValue("@value", facultypage.table.Rows(0)(6).ToString()) 'departmentNo

        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()

        adapter.Fill(table)

        For Each row In table.Rows
            DataGridView1.Rows.Add(New String() {row(0), row(1), row(2), row(3)})
        Next
        MySQLConnection.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim result As Integer = MessageBox.Show("Please confirm your request before proceeding", "WARNING", MessageBoxButtons.OKCancel)
        If result = DialogResult.OK Then
            Dim data As String = DataGridView1.SelectedRows(0).Cells(0).Value.ToString()
            MySQLConnection.Open()
            Dim sql_query As String = "insert into requests values(DEFAULT, @value1, @value2, @value3) "
            Dim command As New MySqlCommand(sql_query, MySQLConnection)

            command.Parameters.AddWithValue("@value1", data) 'course id
            command.Parameters.AddWithValue("@value2", facultypage.username) 'teacher ID
            command.Parameters.AddWithValue("@value3", CInt(facultypage.table.Rows(0)(5).ToString())) 'teacher experience
            Try
                Dim command2 As New MySqlCommand("select * from requests where Course_ID = @value4 and Teacher_ID = @value5 ", MySQLConnection)
                command2.Parameters.AddWithValue("@value4", data)
                command2.Parameters.AddWithValue("@value5", facultypage.username)

                command2.ExecuteNonQuery()
                Dim adapter As New MySqlDataAdapter(command2)
                Dim table As New DataTable()

                adapter.Fill(table)
                If table.Rows.Count > 0 Then
                    MessageBox.Show("You have already requested for this course")
                Else
                    Dim command3 As New MySqlCommand("ALTER table requests drop Serial_No; ALTER TABLE `requests` ADD `Serial_No` INT NOT NULL AUTO_INCREMENT FIRST, ADD UNIQUE (`Serial_No`); ", MySQLConnection)
                    command3.ExecuteNonQuery()

                    command.ExecuteNonQuery()
                    MessageBox.Show("Request sent to administrator")
                End If

            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
            MySQLConnection.Close()
        End If
    End Sub
End Class