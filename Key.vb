' Key Class has 3 different keys that can be used each key has HUD key, key and key hole image

Public Class Key
    Public Property color As String
    Public Property xHUDKeys As Integer
    Public Property yHUDKeys As Integer
    Public Property xKey As Integer
    Public Property yKey As Integer
    Public Property keyVisible As Boolean
    Public Property xKeyH As Integer
    Public Property yKeyH As Integer
    Public Property kHVisible As Boolean
    Public ReadOnly Property height As Integer
    Public ReadOnly Property width As Integer
    Private MainRect As Rectangle

    Public Sub New(MainRect As Rectangle, c As String, xhud As Integer, yhud As Integer)
        Me.MainRect = MainRect
        _color = c
        _xHUDKeys = xhud
        _yHUDKeys = yhud
        _height = 70
        _width = 70
        _keyVisible = True
        _kHVisible = True
    End Sub

    Public Sub show(G As Graphics)
        Select Case color
            Case "b"
                If keyVisible Then
                    G.DrawImage(My.Resources.hud_keyBlue_disabled, xHUDKeys, yHUDKeys, 44, 40)
                    G.DrawImage(My.Resources.keyBlue, xKey, yKey, 70, 70)
                Else
                    G.DrawImage(My.Resources.hud_keyBlue, xHUDKeys, yHUDKeys, 44, 40)
                End If
                If kHVisible Then
                    G.DrawImage(My.Resources.lock_blue, xKeyH, yKeyH, 70, 70)
                End If
            Case "g"
                If keyVisible Then
                    G.DrawImage(My.Resources.hud_keyGreem_disabled, xHUDKeys, yHUDKeys, 44, 40)
                    G.DrawImage(My.Resources.keyGreen, xKey, yKey, 70, 70)
                Else
                    G.DrawImage(My.Resources.hud_keyGreen, xHUDKeys, yHUDKeys, 44, 40)
                End If
                If kHVisible Then
                    G.DrawImage(My.Resources.lock_green, xKeyH, yKeyH, 70, 70)
                End If
            Case "r"
                If keyVisible Then
                    G.DrawImage(My.Resources.hud_keyRed_disabled, xHUDKeys, yHUDKeys, 44, 40)
                    G.DrawImage(My.Resources.keyRed, xKey, yKey, 70, 70)
                Else
                    G.DrawImage(My.Resources.hud_keyRed, xHUDKeys, yHUDKeys, 44, 40)
                End If
                If kHVisible Then
                    G.DrawImage(My.Resources.lock_red, xKeyH, yKeyH, 70, 70)
                End If
        End Select

    End Sub

End Class
