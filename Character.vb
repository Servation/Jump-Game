Public Class Character
    Public Property x As Double
    Public Property y As Double
    Public Property speedX As Decimal
    Public Property speedY As Decimal
    Public Property visible As Boolean
    Public Property moving As Boolean
    Public Property maxSpeed As Decimal
    Public Property running As Boolean
    Public Property jumping As Boolean
    Public Property DirectionRight As Boolean
    Public Property frameCount As Integer
    Private normalFrames(10) As Bitmap
    Private jumpFrame As Bitmap
    Private MainRect As Rectangle

    Sub New(MainRect As Rectangle)
        Me.MainRect = MainRect
        _x = 70
        _y = MainRect.Height - (70 + 93)
        _DirectionRight = True
        _visible = True
        _frameCount = 0
        _running = False
        _maxSpeed = 4
        _jumping = False
        Dim img As New Bitmap(My.Resources.p1_spritesheet)
        For i As Integer = 0 To normalFrames.Count - 1
            Dim x As Integer = 0
            Dim y As Integer = 0
            Dim w = 72
            Dim h = 97
            normalFrames(i) = New Bitmap(w, h)
            Dim gr As Graphics = Graphics.FromImage(normalFrames(i))
            Select Case i
                Case 1
                    x = 73
                Case 2
                    x = 146
                Case 3
                    y = 98
                Case 4
                    x = 73
                    y = 98
                Case 5
                    x = 146
                    y = 98
                Case 6
                    x = 219
                Case 7
                    x = 292
                Case 8
                    x = 219
                    y = 98
                Case 9
                    x = 365
            End Select
            gr.DrawImage(img, 0, 0, New RectangleF(x, y, w, h), GraphicsUnit.Pixel)
        Next
        jumpFrame = New Bitmap(67, 94)
        Dim g As Graphics = Graphics.FromImage(jumpFrame)
        g.DrawImage(img, 0, 0, New RectangleF(438, 93, 67, 94), GraphicsUnit.Pixel)
    End Sub

    Public Sub show(G As Graphics)
        Dim flip As Integer = If(DirectionRight, 1, -1)
        Dim flipspace As Integer = If(DirectionRight, 0, 72)
        If running And Not jumping Then
            G.DrawImage(CType(normalFrames(frameCount), Image), CSng(_x + flipspace), CSng(_y + 4), 72 * flip, 97)
        ElseIf Not jumping Then
            G.DrawImage(CType(normalFrames(0), Image), CSng(_x + flipspace), CSng(_y + 4), 72 * flip, 97)
        Else
            flipspace = If(DirectionRight, 0, 67)
            G.DrawImage(CType(jumpFrame, Image), CSng(_x + flipspace), CSng(_y + 4), 67 * flip, 94)
        End If

    End Sub

    Public Sub Update()
        _x += _speedX
        _y += _speedY
        If _x < 0 Then
            _x = 0
            _speedX *= -1
        ElseIf _x > MainRect.Width - 72 Then
            _x = MainRect.Width - 72
            _speedX *= -1
        End If
        If _y < 0 Then
            _y = 0 + 1
            speedY = 0
        ElseIf _y > MainRect.Height - 97 Then
            _y = MainRect.Height - 97
            _speedY = 0
            _jumping = False
        End If
        If _speedX = 0 Then
            _running = False
        End If

        declerate()
    End Sub

    Public Sub declerate()
        If _speedX > 0 And Not jumping Then
            _speedX -= 0.2
        ElseIf _speedX < 0 And Not jumping Then
            _speedX += 0.2
        End If
        _speedY += 0.2

    End Sub
End Class
