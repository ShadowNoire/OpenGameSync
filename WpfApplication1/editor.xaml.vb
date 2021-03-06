﻿Imports System.Drawing
Imports System.Runtime.CompilerServices
Imports System.Windows.Interop
Public Class editor
    Public Property imagesource As ImageSource
    Public Property exepath As String
    Public Property savdir As String
    Public Property szGameName As String
    Public Property isEdit As Boolean = False

    Private Sub save_Click(sender As Object, e As RoutedEventArgs)
        exepath = _ExePathBox.Text
        savdir = _SaveFolderBox.Text
        szGameName = _GameName.Text
        imagesource = _gameicon.Source

        DialogResult = True

    End Sub
    'System.Drawing.Icon.ExtractAssociatedIcon()

    Private Sub editorloaded()
        If Not isEdit Then
            Dim newicon As ImageSource = SystemIcons.Question.ToImageSource
            _gameicon.Source = newicon
        Else
            Try
                _ExePathBox.Text = exepath
                _gameicon.Source = imagesource
                _GameName.Text = szGameName
                _SaveFolderBox.Text = savdir
            Catch ex As Exception
                MessageBox.Show("Error loading settings.")
            End Try
        End If
    End Sub

    Private Sub exebrowse_Click(sender As Object, e As RoutedEventArgs)
        Dim fileDialog As Forms.OpenFileDialog = New Forms.OpenFileDialog()
        fileDialog.CheckFileExists = True
        fileDialog.InitialDirectory = "C:\"
        fileDialog.Filter = "exe files (*.exe)|*.exe"
        Dim result = fileDialog.ShowDialog()
        If System.Windows.Forms.DialogResult.OK Then
            Dim file As String = fileDialog.FileName
            _ExePathBox.Text = file
            _gameicon.Source = System.Drawing.Icon.ExtractAssociatedIcon(file).ToImageSource
        Else
            _ExePathBox.Text = vbNullString
        End If
    End Sub

    Private Sub button_Copy_Click(sender As Object, e As RoutedEventArgs) Handles button_Copy.Click
        Dim dialog As Forms.FolderBrowserDialog = New Forms.FolderBrowserDialog()
        dialog.Description = "Select the folder containing the save game files."
        dialog.ShowNewFolderButton = False
        dialog.SelectedPath = Environment.SpecialFolder.Personal
        Dim result As Forms.DialogResult = dialog.ShowDialog()
        If result = Forms.DialogResult.OK Then
            _SaveFolderBox.Text = dialog.SelectedPath
            _SaveFolderBox.ScrollToEnd()
        End If

    End Sub

    Private Sub exetxt_Changed()
        _ExePathBox.ScrollToEnd()
    End Sub

    Private Sub foldertxt_Changed()
        _SaveFolderBox.ScrollToEnd()
    End Sub

    Private Sub nametxt_changed()
        If _GameName.Text.Count > 0 Then
            _SaveButton.IsEnabled = True
            _SaveButton.ToolTip = "Click to save"
        Else
            _SaveButton.IsEnabled = False
            _SaveButton.ToolTip = "Enter a Friendly name. Ex: ""My Game"""
        End If
    End Sub
End Class


Module IconExtentions
    <Extension()>
    Public Function ToImageSource(ByVal aIcon As Icon) As ImageSource

        Dim imageSource As ImageSource = Imaging.CreateBitmapSourceFromHIcon(
        aIcon.Handle,
        Int32Rect.Empty,
        BitmapSizeOptions.FromEmptyOptions())

        Return imageSource
    End Function
End Module