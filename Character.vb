' Character class allows control character being drawn.

Public Class Character
    Public Property x As Double
    Public Property y As Double
    Public Property xSpeed As Decimal
    Public Property ySpeed As Decimal
    Public Property isVisible As Boolean
    Public Property maxSpeed As Decimal
    Public Property isMoving As Boolean
    Public Property isJumping As Boolean
    Public Property moveRight As Boolean
    Public Property framesCount As Integer
    Private normalFrames(10) As Bitmap
    Private framesJump As Bitmap
    Private MainRect As Rectangle

    Sub New(MainRect As Rectangle)
        Me.MainRect = MainRect
        _x = 70
        _y = MainRect.Height - (70 + 93)
        _moveRight = True
        _isVisible = True
        _framesCount = 0
        _isMoving = False
        _maxSpeed = 4
        _isJumping = False
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
        framesJump = New Bitmap(67, 94)
        Dim g As Graphics = Graphics.FromImage(framesJump)
        g.DrawImage(img, 0, 0, New RectangleF(438, 93, 67, 94), GraphicsUnit.Pixel)
    End Sub

    Public Sub show(G As Graphics)
        Dim flip As Integer = If(moveRight, 1, -1)
        Dim flipspace As Integer = If(moveRight, 0, 72)
        If isMoving And Not isJumping Then
            G.DrawImage(CType(normalFrames(framesCount), Image), CSng(_x + flipspace), CSng(_y + 4), 72 * flip, 97)
        ElseIf Not isJumping Then
            G.DrawImage(CType(normalFrames(0), Image), CSng(_x + flipspace), CSng(_y + 4), 72 * flip, 97)
        Else
            flipspace = If(moveRight, 0, 67)
            G.DrawImage(CType(framesJump, Image), CSng(_x + flipspace), CSng(_y + 4), 67 * flip, 94)
        End If

    End Sub

    Public Sub Update()
        _x += _xSpeed
        _y += _ySpeed
        If _x < 0 Then
            _x = 0
            _xSpeed *= -1
        ElseIf _x > MainRect.Width - 72 Then
            _x = MainRect.Width - 72
            _xSpeed *= -1
        End If
        If _y < 0 Then
            _y = 0 + 1
            ySpeed = 0
        ElseIf _y > MainRect.Height - 97 Then
            _y = MainRect.Height - 97
            _ySpeed = 0
            _isJumping = False
        End If
        If _xSpeed = 0 Then
            _isMoving = False
        End If

        declerate()
    End Sub

    Public Sub declerate()
        If _xSpeed > 0 And Not isJumping Then
            _xSpeed -= 0.2
        ElseIf _xSpeed < 0 And Not isJumping Then
            _xSpeed += 0.2
        End If
        _ySpeed += 0.2

    End Sub
End Class
