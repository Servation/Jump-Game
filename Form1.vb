Public Class Form1
    Private MainRect As Rectangle
    Private Jumper As Character
    Private Slime(1) As Enemy
    Private logicalSlime As Integer = 1
    Private keysPressed As New HashSet(Of Keys)
    Private doors(3) As Door
    Private spikes As Spike
    Private rKeys As Key
    Private gKeys As Key
    Private bKeys As Key
    Private platforms(5) As Platform
    Private logicalP As Integer = 3
    Private s1 As Boolean = False
    Private s2 As Boolean = False
    Private s3 As Boolean = False
    Private transistion As Boolean = True
    Private counter As Integer = 0

    ' Draws everything to Form1
    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim G As Graphics = e.Graphics
        G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        If s1 Then
            If transistion Then
                transistion = False
            End If
            For i As Integer = 0 To logicalP
                platforms(i).show(G)
            Next
            bKeys.show(G)
            doors(0).show(G)
            For i As Integer = 0 To logicalSlime
                Slime(i).show(G)
            Next
            Jumper.show(G)
        End If
        If s2 Then
            If doors(0).y < Me.Height + 70 Then
                doors(0).y += 10
                doors(0).show(G)
            ElseIf transistion Then
                transistion = False
            End If
            spikes.show(G)
            For i As Integer = 0 To logicalP
                platforms(i).show(G)
            Next
            bKeys.show(G)
            doors(1).show(G)
            For i As Integer = 0 To logicalSlime
                Slime(i).show(G)
            Next
            Jumper.show(G)
        End If
        If s3 Then
            If doors(1).width > 0 Then
                doors(1).width -= 10
                doors(1).x += 5
                doors(1).show(G)
            ElseIf transistion Then
                transistion = False
            End If
            For i As Integer = 0 To logicalP
                platforms(i).show(G)
            Next
            rKeys.show(G)
            gKeys.show(G)
            bKeys.show(G)
            doors(2).show(G)
            For i As Integer = 0 To logicalSlime
                Slime(i).show(G)
            Next
            Jumper.show(G)
            spikes.show(G)
        End If
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Start()
        CopyResourceToDisk()
    End Sub

    ' Starting controls
    Private Sub Start()
        MainRect = DisplayRectangle
        Jumper = New Character(MainRect)
        For i As Integer = 0 To Slime.Count - 1
            Slime(i) = New Enemy(MainRect)
        Next
        spikes = New Spike(MainRect, 140, 780, 5)
        doors(0) = New Door(MainRect, 1332, 640)
        doors(1) = New Door(MainRect, 1340, 56)
        doors(2) = New Door(MainRect, 1360, 444)
        rKeys = New Key(MainRect, "r", 113, 12)
        gKeys = New Key(MainRect, "g", 63, 12)
        bKeys = New Key(MainRect, "b", 12, 12)
        bKeys.xKey = 189
        bKeys.yKey = 306
        bKeys.xKeyH = 666
        bKeys.yKeyH = 640
        For i As Integer = 0 To platforms.Count - 1
            platforms(i) = New Platform(MainRect)
        Next
        logicalP = 3
        logicalSlime = 1
        platforms(0).x = 1134
        platforms(0).y = 472
        platforms(0).nBlocks = 4
        platforms(1).x = 512
        platforms(1).y = 244
        platforms(1).nBlocks = 4
        platforms(2).x = 119
        platforms(2).y = 382
        platforms(2).nBlocks = 2
        platforms(3).x = -10
        platforms(3).y = 780
        platforms(3).nBlocks = 21
        Slime(0).x = platforms(0).x + platforms(0).width / 2
        Slime(0).y = platforms(0).y - Slime(0).height
        Slime(0).xleft = platforms(0).x
        Slime(0).xright = MainRect.Width - Slime(0).width
        Slime(1).x = platforms(3).x + platforms(3).width / 2
        Slime(1).y = platforms(3).y - Slime(0).height
        Slime(1).xleft = platforms(3).x
        Slime(1).xright = MainRect.Width - Slime(1).width
        Slime(0).moveRight = False
        Slime(1).moveRight = True
    End Sub

    ' Movement controls
    Private Sub Movement()
        If Not transistion Then
            If keysPressed.Contains(Keys.A) And Jumper.speedX > -Jumper.maxSpeed And Not Jumper.jumping Then
                Jumper.speedX -= 1
                Jumper.DirectionRight = False
                Jumper.running = True
            End If
            If keysPressed.Contains(Keys.D) And Jumper.speedX < Jumper.maxSpeed And Not Jumper.jumping Then
                Jumper.speedX += 1
                Jumper.DirectionRight = True
                Jumper.running = True
            End If
            If keysPressed.Contains(Keys.Space) And Not Jumper.jumping Then
                Jumper.speedY = -11.5
                Jumper.jumping = True
                PlaySound("jump.wav")
            End If
        End If
    End Sub

    ' Adds and removes keys pressed to Hashset
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        keysPressed.Add(e.KeyCode)
    End Sub

    Private Sub Form1_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        keysPressed.Remove(e.KeyCode)
    End Sub

    ' Timer that controls when and where everything is drawn 
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Movement()
        Jumper.Update()
        If Jumper.running And Not Jumper.frameCount > 9 Then
            If counter Mod 3 = 0 Then
                Jumper.frameCount += 1
            End If
        Else
            Jumper.frameCount = 0
        End If
        If s1 Then
            StageOne()
        End If
        If s2 Then
            StageTwo()
        End If
        If s3 Then
            StageThree()
        End If
        Invalidate()
        counter += 1
    End Sub

    ' Logic for stage one
    Private Sub StageOne()
        If s1 Then
            For i As Integer = 0 To logicalSlime
                If RectsCollision(Slime(i).x, Slime(i).y, Slime(i).width, Slime(i).height, Jumper.x, Jumper.y, 72, 97) Then
                    resetStageOne()
                End If
            Next
            For i As Integer = 0 To logicalP
                resCol(platforms(i).x, platforms(i).y, platforms(i).width, platforms(i).height)
            Next
            If bKeys.keyVisible Then
                If RectsCollision(bKeys.xKey, bKeys.yKey, bKeys.width, bKeys.height, Jumper.x, Jumper.y, 72, 97) Then
                    bKeys.keyVisible = False
                    PlaySound("keypickup.wav")
                End If
            Else
                If RectsCollision(bKeys.xKeyH, bKeys.yKeyH, bKeys.width, bKeys.height, Jumper.x, Jumper.y, 72, 97) And bKeys.keyVisible = False Then
                    If bKeys.kHVisible Then
                        PlaySound("unlock.wav")
                    End If
                    bKeys.kHVisible = False
                    doors(0).open = True
                End If
            End If
            If doors(0).open And RectsCollision(doors(0).xhb, doors(0).yhb, 10, 70, Jumper.x, Jumper.y, 72, 97) And Not Jumper.jumping Then
                transistion = True
                s1 = False
                s2 = True
                Jumper.speedX = -3
                Jumper.DirectionRight = False
                bKeys.xKey = 35
                bKeys.yKey = 704
                bKeys.xKeyH = 1002
                bKeys.yKeyH = 245
                bKeys.keyVisible = True
                bKeys.kHVisible = True
                logicalP = 4
                platforms(0).x = 1204
                platforms(0).y = 195
                platforms(0).nBlocks = 3
                platforms(1).x = 561
                platforms(1).y = 195
                platforms(1).nBlocks = 3
                platforms(2).x = 193
                platforms(2).y = 445
                platforms(2).nBlocks = 0
                platforms(3).x = 0
                platforms(3).y = 780
                platforms(3).nBlocks = 2
                platforms(4).x = 504
                platforms(4).y = 780
                platforms(4).nBlocks = 14
                logicalSlime = 0
                Slime(0).x = platforms(4).x + platforms(4).width / 2
                Slime(0).y = platforms(4).y - Slime(0).height
                Slime(0).xleft = platforms(4).x
                Slime(0).xright = MainRect.Width - Slime(0).width
                Slime(0).moveRight = True
            End If
            For i As Integer = 0 To logicalSlime
                Slime(i).update()
            Next
        End If
    End Sub

    ' Resets stage two
    Public Sub resetStageOne()
        PlaySound("fail.wav")
        Jumper.speedX = 0
        Jumper.DirectionRight = True
        Jumper.x = 70
        Jumper.y = MainRect.Height - (70 + 93)
        Slime(0).x = platforms(0).x + platforms(0).width / 2
        Slime(1).x = platforms(1).x + platforms(1).width / 2
        Slime(0).moveRight = False
        Slime(1).moveRight = True
        bKeys.keyVisible = True
        bKeys.kHVisible = True
        doors(0).open = False
    End Sub

    ' Logic for stage two
    Private Sub StageTwo()
        If s2 Then
            For i As Integer = 0 To logicalSlime
                If RectsCollision(Slime(i).x, Slime(i).y, Slime(i).width, Slime(i).height, Jumper.x, Jumper.y, 72, 97) Then
                    resetStageTwo()
                End If
            Next
            For i As Integer = 0 To logicalP
                resCol(platforms(i).x, platforms(i).y, platforms(i).width, platforms(i).height)
            Next
            If RectsCollision(spikes.x, spikes.y + (70 - 24), spikes.width, spikes.height, Jumper.x, Jumper.y, 72, 97) Then
                resetStageTwo()
            End If
            If bKeys.keyVisible Then
                If RectsCollision(bKeys.xKey, bKeys.yKey, bKeys.width, bKeys.height, Jumper.x, Jumper.y, 72, 97) Then
                    bKeys.keyVisible = False
                    PlaySound("keypickup.wav")
                End If
            Else
                If RectsCollision(bKeys.xKeyH, bKeys.yKeyH, bKeys.width, bKeys.height, Jumper.x, Jumper.y, 72, 97) And bKeys.keyVisible = False Then
                    If bKeys.kHVisible Then
                        PlaySound("unlock.wav")
                    End If
                    bKeys.kHVisible = False
                    doors(1).open = True
                End If
            End If
            If doors(1).open And RectsCollision(doors(1).xhb, doors(1).yhb, 10, 70, Jumper.x, Jumper.y, 72, 97) And Not Jumper.jumping Then
                transistion = True
                s2 = False
                Jumper.speedX = -3
                Jumper.DirectionRight = False
                spikes.x = 0
                spikes.y = 780
                spikes.nSpikes = 22
                spikes.width = 22 * 70
                s3 = True
                bKeys.keyVisible = True
                bKeys.kHVisible = True
                bKeys.xKey = 49
                bKeys.yKey = 168
                bKeys.xKeyH = 861
                bKeys.yKeyH = 168
                rKeys.xKey = 1134
                rKeys.yKey = 630
                rKeys.xKeyH = 561
                rKeys.yKeyH = 630
                gKeys.xKey = 842
                gKeys.yKey = 508
                gKeys.xKeyH = 282
                gKeys.yKeyH = 508
                logicalP = 5
                platforms(0).x = 1274
                platforms(0).y = 195
                platforms(0).nBlocks = 2
                platforms(1).x = 420
                platforms(1).y = 244
                platforms(1).nBlocks = 7
                platforms(2).x = 0
                platforms(2).y = 244
                platforms(2).nBlocks = 2
                platforms(3).x = 210
                platforms(3).y = 584
                platforms(3).nBlocks = 2
                platforms(4).x = 770
                platforms(4).y = 584
                platforms(4).nBlocks = 2
                platforms(5).x = 1344
                platforms(5).y = 584
                platforms(5).nBlocks = 1
                Slime(0).x = platforms(1).x + platforms(1).width / 2
                Slime(0).y = platforms(1).y - Slime(0).height
                Slime(0).xleft = platforms(1).x
                Slime(0).xright = platforms(1).x + platforms(1).width - Slime(0).width
                Slime(0).moveRight = False
            End If
            For i As Integer = 0 To logicalSlime
                Slime(i).update()
            Next
        End If
    End Sub

    ' Resets stage two
    Private Sub resetStageTwo()
        PlaySound("fail.wav")
        Jumper.speedX = 0
        Jumper.DirectionRight = False
        Jumper.x = 1332
        Jumper.y = 640
        bKeys.keyVisible = True
        bKeys.kHVisible = True
        doors(1).open = False
        Slime(0).x = platforms(4).x + platforms(4).width / 2
        Slime(0).moveRight = True
    End Sub

    ' Stage three logic
    Private Sub StageThree()
        If s3 Then
            For i As Integer = 0 To logicalSlime
                If RectsCollision(Slime(i).x, Slime(i).y, Slime(i).width, Slime(i).height, Jumper.x, Jumper.y, 72, 97) Then
                    resetStageThree()
                End If
            Next
            For i As Integer = 0 To logicalP
                resCol(platforms(i).x, platforms(i).y, platforms(i).width, platforms(i).height)
            Next
            If RectsCollision(spikes.x, spikes.y + (70 - 24), spikes.width, spikes.height, Jumper.x, Jumper.y, 72, 97) Then
                resetStageThree()
            End If
            If bKeys.keyVisible Then
                If RectsCollision(bKeys.xKey, bKeys.yKey, bKeys.width, bKeys.height, Jumper.x, Jumper.y, 72, 97) Then
                    bKeys.keyVisible = False
                    PlaySound("keypickup.wav")
                End If
            Else
                If RectsCollision(bKeys.xKeyH, bKeys.yKeyH, bKeys.width, bKeys.height, Jumper.x, Jumper.y, 72, 97) And bKeys.keyVisible = False Then
                    If bKeys.kHVisible Then
                        PlaySound("unlock.wav")
                    End If
                    bKeys.kHVisible = False
                End If
            End If

            If rKeys.keyVisible Then
                If RectsCollision(rKeys.xKey, rKeys.yKey, rKeys.width, rKeys.height, Jumper.x, Jumper.y, 72, 97) Then
                    rKeys.keyVisible = False
                    PlaySound("keypickup.wav")
                End If
            Else
                If RectsCollision(rKeys.xKeyH, rKeys.yKeyH, rKeys.width, rKeys.height, Jumper.x, Jumper.y, 72, 97) And rKeys.keyVisible = False Then
                    If rKeys.kHVisible Then
                        PlaySound("unlock.wav")
                    End If
                    rKeys.kHVisible = False
                End If
            End If

            If gKeys.keyVisible Then
                If RectsCollision(gKeys.xKey, gKeys.yKey, gKeys.width, gKeys.height, Jumper.x, Jumper.y, 72, 97) Then
                    gKeys.keyVisible = False
                    PlaySound("keypickup.wav")
                End If
            Else
                If RectsCollision(gKeys.xKeyH, gKeys.yKeyH, gKeys.width, gKeys.height, Jumper.x, Jumper.y, 72, 97) And gKeys.keyVisible = False Then
                    If gKeys.kHVisible Then
                        PlaySound("unlock.wav")
                    End If
                    gKeys.kHVisible = False
                End If
            End If
            If gKeys.kHVisible = False And rKeys.kHVisible = False And bKeys.kHVisible = False Then
                doors(2).open = True
            End If
            If doors(2).open And RectsCollision(doors(2).xhb, doors(2).yhb, 10, 70, Jumper.x, Jumper.y, 72, 97) And Not Jumper.jumping Then
                s3 = False
                lblTitle.Visible = True
                btnQuit.Visible = True
                btnStart.Visible = True
                Start()
                btnStart.Text = "Play Again"
                PlaySound("finish.wav")
            End If
            For i As Integer = 0 To logicalSlime
                Slime(i).update()
            Next
        End If
    End Sub

    ' Resets stage three
    Private Sub resetStageThree()
        PlaySound("fail.wav")
        Jumper.speedX = -3
        Jumper.DirectionRight = False
        Jumper.x = 1340
        Jumper.y = 56
        Slime(0).x = platforms(1).x + platforms(1).width / 2
        Slime(0).moveRight = False
        gKeys.kHVisible = True
        rKeys.kHVisible = True
        bKeys.kHVisible = True
        gKeys.keyVisible = True
        rKeys.keyVisible = True
        bKeys.keyVisible = True
        doors(2).open = False
    End Sub

    ' Check if there is collision between to rectangles
    Private Function RectsCollision(r1x As Double, r1y As Double, r1w As Double, r1h As Double, r2x As Double, r2y As Double, r2w As Double, r2h As Double) As Boolean
        Return (r1x + r1w >= r2x And r1x <= r2x + r2w And r1y + r1h >= r2y And r1y <= r2y + r2h)
    End Function

    ' Collision system for jumper and platforms
    Private Sub resCol(r1x As Double, r1y As Double, r1w As Double, r1h As Double)
        Dim tempX = 0
        Dim tempY = 0
        If RectsCollision(r1x, r1y, r1w, r1h, Jumper.x, Jumper.y, 72, 97) Then
            Dim top As Boolean = RectsCollision(r1x, r1y, r1w, 1, Jumper.x, Jumper.y, 72, 97)
            Dim left As Boolean = RectsCollision(r1x, r1y, 1, r1h, Jumper.x, Jumper.y, 72, 97)
            Dim bottom As Boolean = RectsCollision(r1x, r1y + r1h - 1, r1w, 1, Jumper.x, Jumper.y, 72, 97)
            Dim right As Boolean = RectsCollision(r1x + r1w - 1, r1y, 1, r1w, Jumper.x, Jumper.y, 72, 97)
            Dim moveup As Double = r1y - 97
            Dim moveleft As Double = r1x - 72
            Dim movedown As Double = r1y + r1h
            Dim moveright As Double = r1x + r1w
            If bottom And right Then
                tempX = r1x + r1w - Jumper.x
                tempY = r1y + r1h - Jumper.y
                If tempX > tempY Then
                    Jumper.y = movedown + 1
                    Jumper.speedY = 0
                Else
                    Jumper.x = moveright
                    Jumper.speedX *= -1
                End If
            ElseIf top And left Then
                tempX = Jumper.x + 72 - r1x
                tempY = Jumper.y + 97 - r1y
                If tempX > tempY Then
                    Jumper.y = moveup
                    Jumper.jumping = False
                    Jumper.speedY = 0
                Else
                    Jumper.x = moveleft
                End If
            ElseIf top And right Then
                tempX = r1x + r1w - Jumper.x
                tempY = Jumper.y + 97 - r1y
                If tempX > tempY Then
                    Jumper.y = moveup
                    Jumper.jumping = False
                    Jumper.speedY = 0
                Else
                    Jumper.x = moveright
                End If
            ElseIf bottom And left Then
                tempX = Jumper.x + 72 - r1x
                tempY = r1y + r1h - Jumper.y
                If tempX > tempY Then
                    Jumper.y = movedown + 1
                    Jumper.speedY = 0
                Else
                    Jumper.x = moveleft
                    Jumper.speedX *= -1
                End If
            Else
                If top Then
                    Jumper.y = moveup
                    Jumper.jumping = False
                    Jumper.speedY = 0
                ElseIf bottom Then
                    Jumper.y = movedown + 1
                    Jumper.speedY = 0
                ElseIf right Then
                    Jumper.x = moveright
                    Jumper.speedX *= -1
                ElseIf left Then
                    Jumper.x = moveleft
                    Jumper.speedX *= -1
                End If
            End If
        End If
    End Sub

    ' Plays a chosen sound
    Private Sub PlaySound(s As String)
        Try
            mciSendString("close myWAV" & s, Nothing, 0, 0)

            Dim fileName1 As String = s
            mciSendString("open " & ChrW(34) & fileName1 & ChrW(34) & " type mpegvideo alias myWAV" & s, Nothing, 0, 0)
            mciSendString("play myWAV" & s, Nothing, 0, 0)

            Dim Volume As Integer = 400
            mciSendString("setaudio myWAV" & s & " volume to " & Volume.ToString, Nothing, 0, 0)

        Catch ex As Exception
            Me.Text = ex.Message
        End Try
    End Sub

    ' Copy sounds to current directory
    Private Sub CopyResourceToDisk()
        If Not IO.File.Exists("jump.wav") Then
            Dim bts(CInt(My.Resources.jump.Length - 1)) As Byte
            My.Resources.jump.Read(bts, 0, bts.Length)
            IO.File.WriteAllBytes("jump.wav", bts)
        End If
        If Not IO.File.Exists("keypickup.wav") Then
            Dim bts(CInt(My.Resources.keypickup.Length - 1)) As Byte
            My.Resources.keypickup.Read(bts, 0, bts.Length)
            IO.File.WriteAllBytes("keypickup.wav", bts)
        End If
        If Not IO.File.Exists("unlock.wav") Then
            Dim bts(CInt(My.Resources.unlock.Length - 1)) As Byte
            My.Resources.unlock.Read(bts, 0, bts.Length)
            IO.File.WriteAllBytes("unlock.wav", bts)
        End If
        If Not IO.File.Exists("button.wav") Then
            Dim bts(CInt(My.Resources.button.Length - 1)) As Byte
            My.Resources.button.Read(bts, 0, bts.Length)
            IO.File.WriteAllBytes("button.wav", bts)
        End If
        If Not IO.File.Exists("fail.wav") Then
            Dim bts(CInt(My.Resources.fail.Length - 1)) As Byte
            My.Resources.fail.Read(bts, 0, bts.Length)
            IO.File.WriteAllBytes("fail.wav", bts)
        End If
        If Not IO.File.Exists("finish.wav") Then
            Dim bts(CInt(My.Resources.finish.Length - 1)) As Byte
            My.Resources.finish.Read(bts, 0, bts.Length)
            IO.File.WriteAllBytes("finish.wav", bts)
        End If
    End Sub

    ' Function to play sounds
    Private Declare Function mciSendString Lib "winmm.dll" Alias "mciSendStringA" _
    (ByVal lpstrCommand As String, ByVal lpstrReturnString As String,
    ByVal uReturnLength As Integer, ByVal hwndCallback As Integer) As Integer

    ' Button controls
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        lblTitle.Visible = False
        btnQuit.Visible = False
        btnStart.Visible = False
        s1 = True
        PlaySound("button.wav")
    End Sub

    Private Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        PlaySound("button.wav")
        Me.Close()
    End Sub

    Private Sub btnStart_MouseHover(sender As Object, e As EventArgs) Handles btnStart.MouseHover
        btnStart.BackColor = Color.LightGray
    End Sub

    Private Sub btnStart_MouseLeave(sender As Object, e As EventArgs) Handles btnStart.MouseLeave
        btnStart.BackColor = Color.Gray
    End Sub

    Private Sub btnQuit_MouseHover(sender As Object, e As EventArgs) Handles btnQuit.MouseHover
        btnQuit.BackColor = Color.LightGray
    End Sub

    Private Sub btnQuit_MouseLeave(sender As Object, e As EventArgs) Handles btnQuit.MouseLeave
        btnQuit.BackColor = Color.Gray
    End Sub
End Class
