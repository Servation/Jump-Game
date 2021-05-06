Public Class Platform
    Public Property x As Integer
    Public Property y As Integer
    Private nB As Integer
    Public Property nBlocks As Integer
        Get
            Return nB
        End Get
        Set(value As Integer)
            nB = value
            w = (nBlocks + 1) * 70
        End Set
    End Property
    Private w As Integer
    Public ReadOnly Property width As Integer
        Get
            Return w
        End Get
    End Property
    Public ReadOnly Property height As Integer

    Private MainRect As Rectangle

    Public Sub New(MainRect As Rectangle)
        Me.MainRect = MainRect
        _x = 0
        _y = 0
        nB = 2
        _height = 70
    End Sub

    Public Sub show(G As Graphics)
        For i As Integer = 0 To nB
            G.DrawImage(My.Resources.block, x + (i * 70), y, 70, 70)
        Next
    End Sub

End Class
