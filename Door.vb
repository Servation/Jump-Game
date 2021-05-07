' Door class takes location to draw and if doors are close are open

Public Class Door
    Public Property x As Integer
    Public Property y As Integer
    Public Property width As Integer
    Public ReadOnly Property xhb As Integer
        Get
            Return x + 50
        End Get
    End Property
    Public ReadOnly Property yhb As Integer
        Get
            Return y + 50
        End Get
    End Property
    Public Property open As Boolean
    Private MainRect As Rectangle


    Public Sub New(MainRect As Rectangle, xLocation As Integer, yLocation As Integer)
        Me.MainRect = MainRect
        _open = False
        _x = xLocation
        _y = yLocation
        _width = 70
    End Sub

    Public Sub show(G As Graphics)
        If open Then
            G.DrawImage(My.Resources.door_openTop, _x, _y, _width, 70)
            G.DrawImage(My.Resources.door_openMid, _x, y + 70, _width, 70)
        Else
            G.DrawImage(My.Resources.door_closedTop, _x, _y, _width, 70)
            G.DrawImage(My.Resources.door_closedMid, _x, y + 70, _width, 70)
        End If

    End Sub
End Class
