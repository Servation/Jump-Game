' Enemy class controls where slimes are being drawn, gives where and size of slimes

Public Class Enemy
    Public Property x As Integer
    Public Property y As Integer
    Public Property xleft As Integer
    Public Property xright As Integer
    Public ReadOnly Property height As Integer
    Public ReadOnly Property width As Integer
    Public Property moveRight As Boolean
    Private FrameCounter As Integer = 0
    Private MainRect As Rectangle

    Public Sub New(MainRect As Rectangle)
        Me.MainRect = MainRect
        _height = 28 * 1.5
        _width = 50 * 1.5
    End Sub

    Public Sub show(G As Graphics)
        Dim flip As Integer = If(Not moveRight, 1, -1)
        Dim flipspace As Integer = If(Not moveRight, 0, 50)
        If FrameCounter < 30 Then
            G.DrawImage(My.Resources.slimeWalk1, x + flipspace, y, width * flip, height)
        Else
            G.DrawImage(My.Resources.slimeWalk2, x + flipspace, y, width * flip, height)
            If FrameCounter = 60 Then
                FrameCounter = 0
            End If
        End If
        FrameCounter += 1
    End Sub

    Public Sub update()
        If x >= xright Then
            moveRight = False
        ElseIf x <= xleft Then
            moveRight = True
        End If
        If moveRight Then
            x += 2
        Else
            x -= 2
        End If
    End Sub

End Class
