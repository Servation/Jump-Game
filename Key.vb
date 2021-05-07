' Key Class has 3 different keys that can be used each key has HUD key, key and key hole image

Public Class Key
    Public Property color As String
    Public Property xHUD As Integer
    Public Property yHUD As Integer
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
        _xHUD = xhud
        _yHUD = yhud
        _height = 70
        _width = 70
        _keyVisible = True
        _kHVisible = True
    End Sub

    Public Sub show(G As Graphics)
        Select Case color
            Case "b"
                If keyVisible Then
                    G.DrawImage(My.Resources.hud_keyBlue_disabled, xHUD, yHUD, 44, 40)
                    G.DrawImage(My.Resources.keyBlue, xKey, yKey, 70, 70)
                Else
                    G.DrawImage(My.Resources.hud_keyBlue, xHUD, yHUD, 44, 40)
                End If
                If kHVisible Then
                    G.DrawImage(My.Resources.lock_blue, xKeyH, yKeyH, 70, 70)
                End If
            Case "g"
                If keyVisible Then
                    G.DrawImage(My.Resources.hud_keyGreem_disabled, xHUD, yHUD, 44, 40)
                    G.DrawImage(My.Resources.keyGreen, xKey, yKey, 70, 70)
                Else
                    G.DrawImage(My.Resources.hud_keyGreen, xHUD, yHUD, 44, 40)
                End If
                If kHVisible Then
                    G.DrawImage(My.Resources.lock_green, xKeyH, yKeyH, 70, 70)
                End If
            Case "r"
                If keyVisible Then
                    G.DrawImage(My.Resources.hud_keyRed_disabled, xHUD, yHUD, 44, 40)
                    G.DrawImage(My.Resources.keyRed, xKey, yKey, 70, 70)
                Else
                    G.DrawImage(My.Resources.hud_keyRed, xHUD, yHUD, 44, 40)
                End If
                If kHVisible Then
                    G.DrawImage(My.Resources.lock_red, xKeyH, yKeyH, 70, 70)
                End If
        End Select

    End Sub

End Class
