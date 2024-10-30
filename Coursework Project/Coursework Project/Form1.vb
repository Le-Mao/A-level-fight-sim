Imports Coursework_Project.elements, Microsoft.VisualBasic.Strings

Public Class GUI
    Const DefaultMapSize As Integer = 11
    Public creatures As List(Of Creature) = New List(Of Creature) From {}
    Public brute As New BruteBehavior
    Public artil As New ArtilleryBehavior
    Public dmgNames As List(Of String) = New List(Of String) From {"", "Piercing", "Slashing", "Bludgeoning", "Poison", "Fire", "Cold", "Acid", "Lighting", "Psychic"}
    Public hRogue As human
    Public G_Wasp As Creature
    Public carson As human
    Public hEsmer As humanoid

    Function alldead(ByVal cs As List(Of Creature)) As Boolean
        For i = 0 To cs.Count - 1
            If cs(i).curhp > 0 Then
                Return False
            End If
        Next
        Return True
    End Function
    Sub fight(ByRef side_1 As Creature(), ByRef side_2 As Creature())

        Dim deathreported1(side_1.Length - 1) As Boolean
        Dim deathreported2(side_2.Length - 1) As Boolean

        Dim sides(1) As List(Of Creature)

        sides(1) = New List(Of Creature) From {}
        sides(0) = New List(Of Creature) From {}
        For i = 0 To side_1.Length - 1
            sides(1).Add(side_1(i))
            deathreported1(i) = False
        Next
        For i = 0 To side_2.Length - 1
            sides(0).Add(side_2(i))
            deathreported1(i) = False
        Next


        Do Until alldead(sides(1)) Or alldead(sides(0))
            If alldead(sides(1)) = False Then
                For i = 0 To sides(1).Count - 1
                    If alldead(sides(0)) = False And sides(1)(i).curhp > 0 Then sides(1)(i).tick(sides(0))
                Next
            Else
                Exit Do
            End If

            If alldead(sides(0)) = False Then
                For i = 0 To sides(0).Count - 1
                    If alldead(sides(1)) = False And sides(0)(i).curhp > 0 Then sides(0)(i).tick(sides(1))
                Next
            Else
                Exit Do
            End If


            'For i = 0 To sides(0).Count - 1
            '    If sides(0)(i).curhp <= 0 Then
            '        If deathreported2(i) = False Then print({sides(0)(i).name & "(" & sides(0)(i).iid & ")", " died."}, {drawing.color.White, drawing.color.Red})
            '        deathreported2(i) = True
            '    End If
            'Next
            For Each cre In sides(0)
                If cre.curhp <= 0 Then
                    If deathreported2(sides(0).IndexOf(cre)) = False Then print({cre.name & "(" & cre.iid & ")", " died."})
                    deathreported2(sides(0).IndexOf(cre)) = True
                End If
            Next

            For i = 0 To sides(1).Count - 1
                If sides(1)(i).curhp <= 0 Then
                    If deathreported1(i) = False Then print({sides(1)(i).name & "(" & sides(1)(i).iid & ")", " died."})
                    deathreported1(i) = True
                End If
            Next

            Console.Write(vbNewLine)
        Loop
        outputWinner(sides)
    End Sub

    Sub outputWinner(ByVal sides() As List(Of Creature))
        'Dim temp As String = Nothing
        Dim sing As String = " win!"
        Dim tCount As Integer = 0
        'If alldead(sides(1)) = False Then
        '    For i = 0 To sides(1).Count - 1
        '        If sides(1)(i).curhp > 0 Then
        '            temp &= sides(1)(i).name & "(" & sides(1)(i).iid & "), "
        '            tCount += 1
        '        End If
        '    Next
        'ElseIf alldead(sides(0)) = False Then
        '    For i = 0 To sides(0).Count - 1
        '        If sides(0)(i).curhp > 0 Then
        '            temp &= sides(0)(i).name & "(" & sides(0)(i).iid & "), "
        '            tCount += 1
        '        End If
        '    Next
        'End If
        Dim survivingSide As List(Of Creature)
        If alldead(sides(0)) Then
            survivingSide = sides(1)
        Else
            survivingSide = sides(0)
        End If
        survivingSide = survivingSide.FindAll(Function(cre) cre.curhp > 0)
        tCount = survivingSide.Count 'counts the number of surviving creatures
        If tCount = 1 Then sing = " wins!"
        If tCount > 0 Then
            Dim temp As String = ""
            For Each cre In survivingSide
                temp &= cre.name & ", "
            Next
            Dim arr = temp.Split(",")


            temp = Strings.Left(temp, temp.Length - 2)
            print({temp & sing})
        Else
            print({"There is no winner."})
        End If
    End Sub

    Private Sub UI_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Randomize()
        Me.Show()
        hRogue = New human("Bob the Rogue", 7, 15, brute, 20) With {.powers = New List(Of Power) From {New bloodLetting(3)}}
        carson = New human("Wild Carson", 5, 10, artil, 22) With {.hitEffects = New List(Of Effect) From {New instantDmg(10, Nothing, 1, psychic)},
                                                                  .powers = New List(Of Power) From {New psychicScreech(1)}}

        hEsmer = New humanoid("Esmerelda", 120, 17, 10, 15, brute, 24, naturalWeapons.fists) With {.powers = New List(Of Power) From {New flurry(2)}}

        creatures.Add(New Creature(hRogue, "Bob") With {.weapon = New Dagger, .pos = New position(0, 0, 0)})
        creatures.Add(New Creature(carson, "Carson") With {.pos = New position(10, 0, 0)})
        creatures.Add(New Creature(New giantWasp, "Wasp") With {.pos = New position(10, 5, 10)})
        creatures.Add(New Creature(hEsmer, "Esmerelda") With {.weapon = New flametongue, .offhand = New Shortsword, .pos = New position(0, 10, 0)})
        MapBox.createmap()
        fight({creatures(3), creatures(0)}, {creatures(2), creatures(1)})
        'Console.ReadKey()
        'Console.WriteLine(vbNewLine)
        'For Each creature In creatures
        '    creature.print()
        'Next
        'Console.ReadKey()
    End Sub

    

    Sub print(ByVal text() As String)
        For i = 0 To text.Length - 1
            Log.Text &= text(i) & vbNewLine
        Next
    End Sub

    Public Function includes(ByVal lst, ByVal Eff)
        For i = 0 To lst.Count - 1
            If lst(i) Is Eff Then
                Return True
            End If
        Next
        Return False
    End Function

    Public Function xDy(ByVal x As Integer, ByVal y As Integer)
        Dim total As Integer
        For i = 1 To x
            total += Math.Ceiling(Rnd() * y)
        Next
        Return total
    End Function

End Class
Public Class position
    Public x, y, z As Integer

    Sub New(ByVal newX As Integer, ByVal newY As Integer, ByVal newZ As Integer)
        x = newX
        y = newY
        z = newZ
    End Sub

    Public Shared Function distance(ByVal pos1 As position, ByVal pos2 As position)
        Dim dx As Single = (pos1.x - pos2.x) ^ 2
        Dim dy As Single = (pos1.y - pos2.y) ^ 2
        Dim dz As Single = (pos1.z - pos2.z) ^ 2
        Return Math.Sqrt(dz + dx + dy)
    End Function
End Class

Public Class MapBox
    Inherits TextBox
    'Dim ID As UInteger
    'Shared count As Integer
    Const xOffset As Integer = 10
    Const yOffset As Integer = 150
    Const MaxMapSize As Integer = 31
    'Shared CurrentMapSize As Integer = 11
    Public Shared gridspace(MaxMapSize - 1, MaxMapSize - 1) As MapBox
    Sub New()
        MyBase.New()
        Width = Height
        'ID = count
        'count += 1
        Enabled = False
    End Sub

    Public Shared Sub createmap()
        For x = 0 To MaxMapSize - 1
            For y = 0 To MaxMapSize - 1
                gridspace(x, y) = New MapBox
            Next
        Next
        ResizeMap(11) 'default to 11*11 grid
    End Sub

    Public Shared Sub ResizeMap(ByVal newSize As Integer)
        For Each box As MapBox In gridspace
            box.Hide()
        Next
        For x = 0 To newSize - 1
            For y = 0 To newSize - 1
                Dim box As MapBox = gridspace(x, y)
                box.Left = xOffset + x * box.Width
                box.Top = yOffset + y * box.Height
                box.Show()
                box.BringToFront()

            Next
        Next
    End Sub
End Class