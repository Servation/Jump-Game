' Spike Class takes the starting position and how many 70x70 blocks of spikes 

Public Class Spike
    Public Property x As Integer
    Public Property y As Integer
    Public Property nSpikes As Integer
    Public Property width As Integer
    Public ReadOnly Property height As Integer

    Private MainRect As Rectangle

    Public Sub New(MainRect As Rectangle, xStart As Integer, yStart As Integer, n As Integer)
        Me.MainRect = MainRect
        _x = xStart
        _y = yStart
        _nSpikes = n
        _width = n * 70
        _height = 24
    End Sub

    Public Sub show(G As Graphics)
        For i As Integer = 0 To nSpikes
            G.DrawImage(My.Resources.spikes, x + (i * 70), y, 70, 70)
        Next
    End Sub
End Class
