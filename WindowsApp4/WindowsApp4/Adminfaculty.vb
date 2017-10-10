Imports MySql.Data.MySqlClient

Public Class Adminfaculty

    Dim MySQLConnection As New MySqlConnection("host=localhost; user=root; database=iwp; convert zero datetime=True")
    Dim command As New MySqlCommand
    Dim adapter As New MySqlDataAdapter
    Dim table As New DataTable()

    Private Sub Form9_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim currentArea = Screen.FromControl(Me).WorkingArea
        Me.Top = currentArea.Top + CInt((currentArea.Height / 2) - (Me.Height / 2))
        Me.Left = currentArea.Left + CInt((currentArea.Width / 2) - (Me.Width / 2))

        DataGridView1.ColumnCount = 9
        DataGridView1.Columns(0).Name = "Serial_No"
        DataGridView1.Columns(1).Name = "Teacher_ID"
        DataGridView1.Columns(2).Name = "Teacher_PW"
        DataGridView1.Columns(3).Name = "Teacher_Name"
        DataGridView1.Columns(4).Name = "Teacher_Email"
        DataGridView1.Columns(5).Name = "Teacher_Experience"
        DataGridView1.Columns(6).Name = "Dept_No"
        DataGridView1.Columns(7).Name = "Admin_ID"
        DataGridView1.Columns(8).Name = "Teacher_PhoneNo"

        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView1.Rows.Clear()
    End Sub

    'Add
    Private Sub AddData()

        Dim sql_query As String = "INSERT into teacher values(DEFAULT, @value1, @value2, @value3, @value5, @value0, @value6, @value7, @value8)"

        command = New MySqlCommand(sql_query, MySQLConnection)

        command.Parameters.AddWithValue("@value1", TextBox11.Text.ToString())
        command.Parameters.AddWithValue("@value2", TextBox4.Text.ToString())
        command.Parameters.AddWithValue("@value3", TextBox5.Text.ToString())
        command.Parameters.AddWithValue("@value5", TextBox7.Text.ToString())
        command.Parameters.AddWithValue("@value0", TextBox1.Text.ToString())
        command.Parameters.AddWithValue("@value6", TextBox8.Text.ToString())
        command.Parameters.AddWithValue("@value7", TextBox9.Text.ToString())
        command.Parameters.AddWithValue("@value8", TextBox10.Text.ToString())

        Try
            MySQLConnection.Open()

            If command.ExecuteNonQuery() > 0 Then
                MessageBox.Show("Value Added Successfully")

            End If

            MySQLConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MySQLConnection.Close()
        End Try

    End Sub

    'Retrieve
    Private Sub Retrieve()

        DataGridView1.Rows.Clear()

        Dim sql_query As String = "SELECT * from teacher "
        command = New MySqlCommand(sql_query, MySQLConnection)
        Dim sql_reset As String = "ALTER table teacher drop Serial_No "
        Dim command3 As New MySqlCommand(sql_reset, MySQLConnection)

        Try
            MySQLConnection.Open()
            command3.ExecuteNonQuery()

            command3 = New MySqlCommand("ALTER TABLE `teacher` ADD `Serial_No` INT NOT NULL AUTO_INCREMENT FIRST, ADD UNIQUE (`Serial_No`); ", MySQLConnection)
            command3.ExecuteNonQuery()

            adapter = New MySqlDataAdapter(command)
            table = New DataTable()
            adapter.Fill(table)

            For Each row In table.Rows
                Populate(row(0), row(1), row(2), row(3), row(4), row(5), row(6), row(7), row(8))
            Next

            MySQLConnection.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MySQLConnection.Close()
        End Try

    End Sub

    Private Sub Populate(val1 As Integer, val2 As String, val3 As String, val4 As String, val5 As String, val6 As String, val7 As String, val8 As String, val9 As String)
        Dim row As String() = New String() {val1, val2, val3, val4, val5, val6, val7, val8, val9}
        DataGridView1.Rows.Add(row)
    End Sub

    'Update
    Private Sub UpdateData()

        Dim data As Integer = CInt(DataGridView1.SelectedRows(0).Cells(0).Value)
        Dim sql_query As String = "UPDATE teacher set Teacher_ID = @value1 , Teacher_PW = @value2 , Teacher_Name = @value3 , Teacher_Email = @value5 , Teacher_Experience = @value0, Dept_No = @value6 , Admin_ID = @value7 , Teacher_PhoneNo = @value8 where teacher.Serial_No = @valuedata "

        command = New MySqlCommand(sql_query, MySQLConnection)

        command.Parameters.AddWithValue("@value1", TextBox11.Text.ToString())
        command.Parameters.AddWithValue("@value2", TextBox4.Text.ToString())
        command.Parameters.AddWithValue("@value3", TextBox5.Text.ToString())
        command.Parameters.AddWithValue("@value5", TextBox7.Text.ToString())
        command.Parameters.AddWithValue("@value0", TextBox1.Text.ToString())
        command.Parameters.AddWithValue("@value6", TextBox8.Text.ToString())
        command.Parameters.AddWithValue("@value7", TextBox9.Text.ToString())
        command.Parameters.AddWithValue("@value8", TextBox10.Text.ToString())
        command.Parameters.AddWithValue("@valuedata", data.ToString())

        Try
            MySQLConnection.Open()

            Dim sql_query2 As String = "SET FOREIGN_KEY_CHECKS=0 "
            Dim sql_query3 As String = "SET FOREIGN_KEY_CHECKS=1 "

            Dim command2 As New MySqlCommand(sql_query2, MySQLConnection)
            Dim command3 As New MySqlCommand(sql_query3, MySQLConnection)

            command2.ExecuteNonQuery()

            If command.ExecuteNonQuery() > 0 Then
                MessageBox.Show("Value Updated Successfully")

            End If

            command3.ExecuteNonQuery()

            MySQLConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MySQLConnection.Close()
        End Try
    End Sub

    'Delete
    Private Sub DeleteData()
        MySQLConnection.Open()
        Dim data As Integer = CInt(DataGridView1.SelectedRows(0).Cells(0).Value)
        Dim sql_query As String = "DELETE FROM `teacher` WHERE Serial_No = " + data.ToString() + " "
        command = New MySqlCommand(sql_query, MySQLConnection)
        Try
            If command.ExecuteNonQuery() >= 0 Then
                MessageBox.Show("Value Deleted Successfully")
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        MySQLConnection.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        adminpage.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        DataGridView1.Rows.Clear()
    End Sub

    'Clear
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TextBox11.Text = ""
        TextBox1.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        DeleteData()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        UpdateData()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Retrieve()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        AddData()
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click

    End Sub

End Class