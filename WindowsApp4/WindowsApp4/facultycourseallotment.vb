Imports MySql.Data.MySqlClient

Public Class facultycourseallotment

    Dim MySQLConnection As New MySqlConnection("host=localhost; user=root; database=iwp; convert zero datetime=True")

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
        adminpage.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ComboAllot()
        Retrieve()
    End Sub

    Private Sub facultycourseallotment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim currentArea = Screen.FromControl(Me).WorkingArea
        Me.Top = currentArea.Top + CInt((currentArea.Height / 2) - (Me.Height / 2))
        Me.Left = currentArea.Left + CInt((currentArea.Width / 2) - (Me.Width / 2))

        DataGridView1.ColumnCount = 4
        DataGridView1.Columns(0).Name = "Serial_No"
        DataGridView1.Columns(1).Name = "Course_ID"
        DataGridView1.Columns(2).Name = "Teacher_ID"
        DataGridView1.Columns(3).Name = "Teacher_Experience"
        DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ComboAllot()
        Retrieve()
    End Sub

    Private Sub ComboAllot()
        MySQLConnection.Open()
        Dim command As New MySqlCommand("select distinct Course_ID from requests order by Course_ID ASC ", MySQLConnection)
        Dim adapter As New MySqlDataAdapter(command)
        Dim table As New DataTable()
        adapter.Fill(table)

        ComboBox1.Items.Clear()

        For Each Row In table.Rows
            ComboBox1.Items.Add(Row(0))
        Next
        MySQLConnection.Close()
    End Sub

    Private Sub Retrieve()
        Try
            DataGridView1.Rows.Clear()
            MySQLConnection.Open()
            Dim command2 As New MySqlCommand("ALTER table requests drop Serial_No; ALTER TABLE `requests` ADD `Serial_No` INT NOT NULL AUTO_INCREMENT FIRST, ADD UNIQUE (`Serial_No`); ", MySQLConnection)
            command2.ExecuteNonQuery()
            Dim command As New MySqlCommand("select * from requests ", MySQLConnection)
            Dim adapter As New MySqlDataAdapter(command)
            Dim table As New DataTable()
            adapter.Fill(table)
            For Each row In table.Rows
                DataGridView1.Rows.Add(New String() {row(0), row(1), row(2), row(3)})
            Next
            MySQLConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MySQLConnection.Close()
        End Try
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Try
            MySQLConnection.Open()

            Dim fixercommand1 As New MySqlCommand("SET FOREIGN_KEY_CHECKS=0 ", MySQLConnection)
            Dim fixercommand2 As New MySqlCommand("SET FOREIGN_KEY_CHECKS=1 ", MySQLConnection)

            fixercommand1.ExecuteNonQuery()

            Dim command As New MySqlCommand("insert into allotted values(DEFAULT, @value1, @value2) ", MySQLConnection)
            Dim courseid As String = DataGridView1.SelectedRows(0).Cells(1).Value.ToString()
            command.Parameters.AddWithValue("@value1", DataGridView1.SelectedRows(0).Cells(2).Value.ToString())
            command.Parameters.AddWithValue("@value2", courseid)
            command.ExecuteNonQuery()

            command = New MySqlCommand("delete from requests where Course_ID = @value3 ", MySQLConnection)
            command.Parameters.AddWithValue("@value3", courseid)
            command.ExecuteNonQuery()

            fixercommand2.ExecuteNonQuery()

            MySQLConnection.Close()
            Retrieve()
            ComboAllot()
            ComboBox1.ResetText()
            MySQLConnection.Open()
            Dim command2 As New MySqlCommand("ALTER table allotted drop Serial_No; ALTER TABLE `allotted` ADD `Serial_No` INT NOT NULL AUTO_INCREMENT FIRST, ADD UNIQUE (`Serial_No`); ", MySQLConnection)
            command2.ExecuteNonQuery()
            MessageBox.Show("Allocation Complete")
            MySQLConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MySQLConnection.Close()
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            MySQLConnection.Open()

            Dim fixercommand1 As New MySqlCommand("SET FOREIGN_KEY_CHECKS=0 ", MySQLConnection)
            Dim fixercommand2 As New MySqlCommand("SET FOREIGN_KEY_CHECKS=1 ", MySQLConnection)
            Dim courseid As String = ComboBox1.GetItemText(ComboBox1.SelectedItem)
            fixercommand1.ExecuteNonQuery()
            Dim needcommand1 As New MySqlCommand("select Teacher_ID from requests where Course_ID = @value0 order by Serial_No limit 1 ", MySQLConnection)
            needcommand1.Parameters.AddWithValue("@value0", courseid)
            Dim adapter1 As New MySqlDataAdapter(needcommand1)
            Dim table1 As New DataTable()
            adapter1.Fill(table1)
            Dim command As New MySqlCommand("insert into allotted values(DEFAULT, @value1, @value2) ", MySQLConnection)
            command.Parameters.AddWithValue("@value1", table1.Rows(0)(0).ToString())
            command.Parameters.AddWithValue("@value2", courseid)
            command.ExecuteNonQuery()

            command = New MySqlCommand("delete from requests where Course_ID = @value3 ", MySQLConnection)
            command.Parameters.AddWithValue("@value3", courseid)
            command.ExecuteNonQuery()

            fixercommand2.ExecuteNonQuery()

            MySQLConnection.Close()
            Retrieve()
            ComboAllot()
            ComboBox1.ResetText()
            MySQLConnection.Open()
            Dim command2 As New MySqlCommand("ALTER table allotted drop Serial_No; ALTER TABLE `allotted` ADD `Serial_No` INT NOT NULL AUTO_INCREMENT FIRST, ADD UNIQUE (`Serial_No`); ", MySQLConnection)
            command2.ExecuteNonQuery()

            MessageBox.Show("Allocation Complete")
            MySQLConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MySQLConnection.Close()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            MySQLConnection.Open()

            Dim fixercommand1 As New MySqlCommand("SET FOREIGN_KEY_CHECKS=0 ", MySQLConnection)
            Dim fixercommand2 As New MySqlCommand("SET FOREIGN_KEY_CHECKS=1 ", MySQLConnection)
            Dim courseid As String = ComboBox1.GetItemText(ComboBox1.SelectedItem)
            fixercommand1.ExecuteNonQuery()
            Dim needcommand1 As New MySqlCommand("select Teacher_ID from requests where Course_ID = @value0 order by Teacher_Experience DESC limit 1 ", MySQLConnection)
            needcommand1.Parameters.AddWithValue("@value0", courseid)
            Dim adapter1 As New MySqlDataAdapter(needcommand1)
            Dim table1 As New DataTable()
            adapter1.Fill(table1)
            Dim command As New MySqlCommand("insert into allotted values(DEFAULT, @value1, @value2) ", MySQLConnection)
            command.Parameters.AddWithValue("@value1", table1.Rows(0)(0).ToString())
            command.Parameters.AddWithValue("@value2", courseid)
            command.ExecuteNonQuery()

            command = New MySqlCommand("delete from requests where Course_ID = @value3 ", MySQLConnection)
            command.Parameters.AddWithValue("@value3", courseid)
            command.ExecuteNonQuery()

            fixercommand2.ExecuteNonQuery()

            MySQLConnection.Close()
            Retrieve()
            ComboAllot()
            ComboBox1.ResetText()
            MySQLConnection.Open()
            Dim command2 As New MySqlCommand("ALTER table allotted drop Serial_No; ALTER TABLE `allotted` ADD `Serial_No` INT NOT NULL AUTO_INCREMENT FIRST, ADD UNIQUE (`Serial_No`); ", MySQLConnection)
            command2.ExecuteNonQuery()

            MessageBox.Show("Allocation Complete")
            MySQLConnection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            MySQLConnection.Close()
        End Try
    End Sub
End Class