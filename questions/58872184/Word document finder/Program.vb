Imports System.Runtime.InteropServices

Module Program
    Sub Main()
        Dim allProcesses = Process.GetProcesses().Where(Function(p) p.ProcessName.Contains("WINWORD"))

        If allProcesses.Count = 0 Then
            Console.WriteLine("Word does not appear to be running.")
            Return
        End If

        Dim windowTitles = ChildWindowManager.GetChildWindowTitles(allProcesses.First().Id)
        For Each title In windowTitles
            If (title.Contains("- Word")) Then
                Console.WriteLine(title)
            End If
        Next
    End Sub

    Class ChildWindowManager

        Delegate Function EnumThreadDelegate(ByVal hWnd As IntPtr, ByVal lParam As IntPtr) As Boolean
        <DllImport("user32.dll")>
        Private Shared Function EnumThreadWindows(ByVal dwThreadId As Integer, ByVal lpfn As EnumThreadDelegate, ByVal lParam As IntPtr) As Boolean
        End Function

        <DllImport("user32.dll")>
        Public Shared Function GetWindowText(ByVal hwnd As Integer, ByVal lpString As System.Text.StringBuilder, ByVal cch As Integer) As Integer
        End Function

        <DllImport("user32.dll")>
        Private Shared Function GetWindowTextLength(ByVal hwnd As IntPtr) As Integer
        End Function

        Private Shared Function EnumerateProcessWindowHandles(ByVal processId As Integer) As List(Of IntPtr)
            Dim windowHandles = New List(Of IntPtr)()

            For Each thread As ProcessThread In Process.GetProcessById(processId).Threads
                EnumThreadWindows(thread.Id, Function(hWnd, lParam)
                                                 windowHandles.Add(hWnd)
                                                 Return True
                                             End Function, IntPtr.Zero)
            Next
            Return windowHandles
        End Function

        Private Shared Function GetWindowTitle(ByVal hWnd As IntPtr) As String
            Dim length As Integer = GetWindowTextLength(hWnd)
            If length = 0 Then Return Nothing

            Dim titleStringBuilder As New Text.StringBuilder("", length)

            GetWindowText(hWnd, titleStringBuilder, titleStringBuilder.Capacity + 1)
            Return titleStringBuilder.ToString()
        End Function

        Public Shared Function GetChildWindowTitles(processId As Integer) As List(Of String)
            Dim windowTitles As New List(Of String)
            For Each handlee In EnumerateProcessWindowHandles(processId)
                Dim windowText = GetWindowTitle(handlee)
                If windowText <> Nothing Then
                    windowTitles.Add(windowText)
                End If
            Next

            Return windowTitles
        End Function

    End Class
End Module
