Public Class adminpage
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Hide()
        controlzone.TextBox1.Text = ""
        controlzone.TextBox2.Text = ""
        controlzone.TextBox3.Text = ""
        controlzone.TextBox4.Text = ""
        controlzone.TextBox5.Text = ""
        controlzone.TextBox6.Text = ""
        controlzone.Show()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Hide()
        AdminStudentManage.Show()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Hide()
        Adminfaculty.Show()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Hide()
        Admincourse.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
        facultycourseallotment.Show()
    End Sub

    Private Sub adminpage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim currentArea = Screen.FromControl(Me).WorkingArea
        Me.Top = currentArea.Top + CInt((currentArea.Height / 2) - (Me.Height / 2))
        Me.Left = currentArea.Left + CInt((currentArea.Width / 2) - (Me.Width / 2))
    End Sub
End Class