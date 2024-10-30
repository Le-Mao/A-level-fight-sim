Public Class Behaviour
    Overridable Sub turn(ByRef creature As Creature, ByRef enemies As List(Of Creature))
        Console.WriteLine("Base behaviour")
    End Sub

    Function randomTarget(ByRef enemies As List(Of Creature))
        Dim target As Creature
        'Do
        target = enemies(Math.Ceiling(Rnd() * (enemies.Count)) - 1)
        'Loop While target Is Nothing
        Return target
    End Function

    Function randomConsciousTarget(ByRef enemies As List(Of Creature))
        Dim t As Creature
        Do
            t = randomTarget(enemies)
        Loop Until t.curhp > 0
        Return t
    End Function
    Sub useHighestPower(ByRef target As Creature, ByRef creature As Creature)
        Dim maxPriority As Power = creature.def.powers(0)
        For Each Power In creature.def.powers
            If Power.priority > maxPriority.priority Then
                maxPriority = Power
            End If
        Next
        maxPriority.use(target, creature)
    End Sub
    'Sub useHighestPower(ByVal creature As Creature, ByRef target As Creature)
    '    Dim maxStamina As Power
    '    Dim found As Boolean
    '    Do
    '        maxStamina = New emptyPower(0)
    '        found = False
    '        For Each Power In creature.def.powers
    '            If Power.stamina > maxStamina.stamina And Power.ap <= creature.ap And position.distance(creature.pos, target.pos) <= Power.range And creature.stamina >= Power.stamina Then
    '                found = True
    '                maxStamina = Power
    '            End If
    '        Next
    '        If maxStamina.offensive = True Then
    '            maxStamina.use(target, creature)
    '        Else
    '            maxStamina.use(creature, creature)
    '        End If
    '    Loop Until found = False
    'End Sub

    Function flightState(ByVal enemies As List(Of Creature))
        Dim canfly = Function(cre As Creature)
                         Return cre.def.properties.IndexOf(cDefinition.cProperty.Flying) = -1
                     End Function
        Dim state As Integer = -1
        For Each enemy In enemies
            If canfly(enemy) Then
                state = 1
                Exit For
            End If
        Next
        For Each enemy In enemies
            If canfly(enemy) = False Then
                state = 0
                Exit For
            End If
        Next
        Return state
    End Function

    Function remainingEnemies(ByVal enemies As List(Of Creature))
        Dim ret As List(Of Creature) = New List(Of Creature) From {}
        For Each enemy In enemies
            If enemy.curhp > 0 Then ret.Add(enemy)
        Next
        Return ret
    End Function

End Class

Public Class BruteBehavior
    Inherits Behaviour
    Overrides Sub turn(ByRef creature As Creature, ByRef enemies As List(Of Creature))
        'creature.ap = creature.maxAP
        creature.bonusAction = True
        creature.Action = True
        Dim target As Creature
        If creature.grudge Is Nothing Then
            Do
                target = randomTarget(enemies)
            Loop Until target.def.properties.IndexOf(cDefinition.cProperty.Flying) = -1 Or creature.def.properties.IndexOf(cDefinition.cProperty.Flying) <> -1 Or flightState(remainingEnemies(enemies)) = 1
        Else
            target = creature.grudge
        End If

        If position.distance(creature.pos, target.pos) >= 2 Then
            creature.movetowards(target.pos)
        End If

        If creature.def.powers.Count < 1 Then
            creature.attack(target)
        Else
            useHighestPower(target, creature)
        End If
    End Sub
End Class

Public Class ArtilleryBehavior
    Inherits Behaviour
    Public Overrides Sub turn(ByRef creature As Creature, ByRef enemies As List(Of Creature))
        'creature.ap = creature.maxAP
        creature.bonusAction = True
        creature.Action = True
        Dim target As Creature
        If creature.grudge Is Nothing Then            'if the grudge is void or dead then re-target
            creature.grudge = randomConsciousTarget(enemies)
        ElseIf creature.grudge.curhp <= 0 Then
            creature.grudge = randomConsciousTarget(enemies)
        End If

        target = creature.grudge
        If creature.def.powers.Count < 1 Then
            creature.attack(target)
        Else
            useHighestPower(target, creature)
        End If
    End Sub
End Class
Class playerBehaviour
    Inherits Behaviour
    Public Overrides Sub turn(ByRef creature As Creature, ByRef enemies As List(Of Creature))
        'GUI.MoveBtn.Enabled = True
        GUI.btnPower.Enabled = True
        GUI.btnAttack.Enabled = True
    End Sub

    '    Function displayTargets(ByVal question As String, ByVal creature As Creature, ByVal range As Single) As Creature
    '        Dim selected As Integer = 1
    '        Dim length As Integer
    '        Dim targets As List(Of Creature) = New List(Of Creature) From {}
    '        For Each cre As Creature In GUI.creatures
    '            If position.distance(cre.pos, creature.pos) < range Then targets.Add(cre)
    '        Next
    'start:
    '        length = targets.Count
    '        Console.Clear()
    '        Console.ForegroundColor = DC.headercolour
    '        Console.WriteLine(question)
    '        For i = 1 To targets.Count

    '            Dim fc As ConsoleColor = DC.forecolour
    '            If creature.curhp <= 0 Then fc = ConsoleColor.Gray

    '            If selected = i Then
    '                Console.ForegroundColor = DC.backcolour
    '                Console.BackgroundColor = fc
    '            Else
    '                Console.ForegroundColor = fc
    '                Console.BackgroundColor = DC.backcolour
    '            End If
    '            Console.WriteLine(targets(i - 1).name)
    '        Next
    '        Console.ForegroundColor = DC.forecolour
    '        Console.BackgroundColor = DC.backcolour
    '        Select Case Console.ReadKey.Key
    '            Case ConsoleKey.Enter
    '                Return targets(selected - 1)
    '            Case ConsoleKey.UpArrow
    'ups:
    '                If selected = 1 Then
    '                    selected = length
    '                Else
    '                    selected -= 1
    '                End If

    '                Try
    '                    If targets(selected - 1).name = "" Then
    '                        GoTo ups
    '                    End If
    '                Catch
    '                    If targets(length - 1).name = "" Then
    '                        GoTo ups
    '                    End If
    '                End Try
    '            Case ConsoleKey.DownArrow
    'downs:
    '                If selected = length Then
    '                    selected = 1
    '                Else
    '                    selected += 1
    '                End If

    '                Try
    '                    If targets(selected - 1).name = "" Then
    '                        GoTo downs
    '                    End If
    '                Catch
    '                    If targets(1).name = "" Then
    '                        GoTo downs
    '                    End If
    '                End Try
    '            Case ConsoleKey.Backspace
    '                Return Nothing
    '        End Select
    '        GoTo start
    '    End Function
End Class



'Public Class Behaviour
'    Public isMoving As Boolean = False
'    Public moved As Integer = 0
'    Overridable Sub turn(ByRef creature As Creature, ByRef enemies As List(Of Creature))
'        Console.WriteLine("Base behaviour")
'    End Sub

'    Function randomTarget(ByRef enemies As List(Of Creature))
'        Dim target As Creature
'here:
'        Do
'            target = enemies(Math.Ceiling(Rnd() * (enemies.Count)) - 1)
'        Loop Until target.curhp > 0
'        Return target
'    End Function

'    Sub useHighestPower(ByVal creature As Creature, ByRef target As Creature)
'        Dim maxStamina As Power
'        Dim found As Boolean
'        Do
'            maxStamina = New emptyPower(0)
'            found = False
'            For Each Power In creature.powers
'                If position.distance(creature.pos, target.pos) <= Power.range Then
'                    found = True
'                    maxStamina = Power
'                End If
'            Next
'            If maxStamina.offensive = True Then
'                maxStamina.use(target, creature)
'            Else
'                maxStamina.use(creature, creature)
'            End If
'        Loop Until found = False
'    End Sub

'    Function flightState(ByVal enemies As List(Of Creature))
'        Dim state As Integer = -1
'        For Each enemy In enemies
'            If enemy.canFly = True Then
'                state = 1
'                Exit For
'            End If
'        Next
'        For Each enemy In enemies
'            If enemy.canFly = False Then
'                state = 0
'                Exit For
'            End If
'        Next
'        Return state
'    End Function

'    Function remainingEnemies(ByVal enemies As List(Of Creature))
'        Dim ret As List(Of Creature) = New List(Of Creature) From {}
'        For Each enemy In enemies
'            If enemy.curhp > 0 Then ret.Add(enemy)
'        Next
'        Return ret
'    End Function

'End Class
