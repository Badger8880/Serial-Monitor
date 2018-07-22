Imports System.IO.Ports
Public Class Form1

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Gets or sets a value indicating whether to catch calls on the wrong thread that access a control's Handle property when an application is being debugged.
        Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Try
            For Each port As String In SerialPort.GetPortNames()
                ComboBox1.Items.Add(port)
            Next
            'Baud rate selection
            ComboBox1.SelectedIndex = 0
            ComboBox2.SelectedItem = "9600"
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Button1.Text = "Connect" Then
            ' When uses hits connect connect to the serial port of usesr choice
            SerialPort1.BaudRate = Val(ComboBox2.SelectedItem)
            SerialPort1.PortName = ComboBox1.SelectedItem
            Try
                'Opens conmmunication with the serial port and disabld the conection box 
                SerialPort1.Open()
                Button1.Text = "Disconnect"
                TextBox1.Enabled = True
                GroupBox1.Enabled = False
            Catch ex As Exception

            End Try
        Else
            ' closes conmmunication with the serial port and disabld the conection box 
            SerialPort1.Close()
            TextBox1.Enabled = False
            GroupBox1.Enabled = True
            Button1.Text = "Connect"

        End If

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        'sends data to the serial  port
        If e.KeyCode = Keys.Enter Then
            SerialPort1.Write(TextBox1.Text)
            TextBox1.Clear()

        End If
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As SerialDataReceivedEventArgs) Handles SerialPort1.DataReceived
        'reads data from serial port 
        RichTextBox1.Text &= SerialPort1.ReadExisting()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Dim savetxt As New SaveFileDialog
            savetxt.Filter = "txt files (*.txt) |*.txt"
            savetxt.FilterIndex = 2
            savetxt.RestoreDirectory = False
            If savetxt.ShowDialog() = DialogResult.OK Then
                IO.File.WriteAllText(savetxt.FileName, RichTextBox1.Text)
            End If
        Catch fileException As Exception
            Throw fileException
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'Clears serial port
        RichTextBox1.Clear()
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class
