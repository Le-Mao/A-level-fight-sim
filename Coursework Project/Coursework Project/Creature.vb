Imports Coursework_Project.elements
Public Class Creature
    Public def As cDefinition
    Public Shared count As Integer
    Public pos As position
    Public speed As Integer
    Public curhp As Decimal
    Public id, iid As Integer
    Public effects As List(Of Effect) = New List(Of Effect) From {}
    Public initCounter As Integer
    'Public healTaken As Decimal = 0
    Public name As String
    Public weapon As weapon
    Public offhand As item
    Public items As List(Of item)
    Public grudge As Creature
    'Public ap As Integer
    'Public maxAP As Integer = 5
    Public bonusAction, Action As Boolean
    Public movementLeft As UInteger
    'Public stamina, maxStamina As Integer
    'Public HE_Durations As New List(Of Integer) From {}
    'Public HE_Strengths As New List(Of Integer) From {}

    Public Sub New(ByVal definition As cDefinition, ByVal newName As String)
        def = definition
        name = newName
        curhp = def.maxHp
        weapon = def.natural_weapon
        id = def.id
        'initiative = newInitiative
        'dmg = newDmg
        'maxHp = newMaxhp
        iid = count
        count += 1

    End Sub

    Public Sub print()
        GUI.print({"ID: " & id & vbNewLine & "Current HP: " & curhp & vbNewLine & "IID: " & iid & vbNewLine})
    End Sub

    Public Sub addEffect(ByVal template As Effect)
        Dim ef As Effect = template.cloneTo(Me)
        effects.Add(ef)
        ef.start()
    End Sub

    Public Function takeDmg(ByVal dmg As Integer, ByVal dmgType As element)

        Dim dtIndex As Integer = dmgType.id

        If dmg > 0 Then
            dmg *= 1 - (def.resistances(dtIndex) / 100)
            GUI.print({name & "(" & iid & ") took " & dmg & " " & dmgType.name & " dmg."})
        End If


        curhp = Math.Min(curhp - dmg, def.maxHp)
        If curhp > 0 Then
            Return False
        Else

            Return True 'returns true if dead
        End If
        Return True
    End Function
    Public Function attack(ByRef target As Creature)
        If position.distance(target.pos, pos) < weapon.range + 1 Then
            GUI.print({name & "(" & iid & ") attacks " & target.name & " with " & weapon.name & "."})
            If hits(target.def.baseAC) = True Then
                GUI.print({"Hit!"})
                onhit(target)
                Return True
            Else
                GUI.print({"Miss!"})
                Return False
            End If
        Else
            Return False
        End If
    End Function
    Public Sub onhit(ByVal target As Creature)
        target.takeDmg(def.baseDmg + weapon.dmgAdd, weapon.dmgType) 'deal damage to the target
        For Each Ef As Effect In def.hitEffects 'apply effects from the creaturedef to the target
            target.addEffect(Ef)
        Next
        For Each Ef As Effect In weapon.hiteffects
            target.addEffect(Ef) 'whatever the item says to do on hit
        Next
        weapon.hit(target)
    End Sub
    Public Function hits(ByVal ac As Integer) As Boolean
        Dim atkroll As Integer = Math.Ceiling(Rnd() * 20)
        Select Case atkroll
            Case 20
                Return True
            Case 1
                Return False
        End Select
        If atkroll + def.atkMod < ac Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Sub tick(ByRef enemies As List(Of Creature))
        initCounter += def.initiative
        If initCounter >= 100 Then
            def.behaviour.turn(Me, enemies)
            initCounter -= 100
        End If

        For i = effects.Count - 1 To 0 Step -1
            Try
                If effects.Count > 0 Then effects(i).tick()
            Catch
            End Try
        Next
    End Sub

    Public Sub movetowards(ByVal pos2 As position, Optional ByVal inverted As Boolean = False)
        Dim dx As Integer
        Dim dy As Integer
        Dim dz As Integer
        Dim oPos As New position(pos.x, pos.y, pos.z)
        Dim dir As Integer = 1
        If inverted Then dir = -1

        Do
            dx = Math.Max(1, Math.Abs(pos.x - pos2.x))
            dy = Math.Abs(pos.y - pos2.y)
            dz = Math.Abs(pos.z - pos2.z)

            If dz > dx And dz > dy And def.canFly Then
                If pos.z > pos2.z Then
                    pos.z -= dir
                Else
                    pos.z += dir
                End If
            ElseIf dy > dx Then
                If pos.y > pos2.y Then
                    pos.y -= dir
                Else
                    pos.y += dir
                End If
            ElseIf dx > 1 Then
                If pos.x > pos2.x Then
                    pos.x -= dir
                Else
                    pos.x += dir
                End If
            End If
        Loop Until position.distance(pos, oPos) >= speed Or position.distance(pos, pos2) < 2 Or (Math.Sqrt(dx ^ 2 + dy ^ 2) < 2 And def.canFly = False And dz >= 2)
        GUI.print({name & "(" & iid & ") moved to " & pos.x & "," & pos.y & "," & pos.z & "."})

    End Sub
End Class

Public Class cDefinition
    Public Shared count As UInteger
    Public resistances(elements.elementlist.Count - 1) As Integer
    Public natural_weapon As weapon
    Public id As Integer
    Public maxHp, atkMod, baseAC, baseDmg As Integer
    Public initiative As UInteger
    Public powers As New List(Of Power) From {}
    Public baseSpeed As UInteger
    Public hitEffects As New List(Of Effect) From {}
    Public defName As String
    Public behaviour As Behaviour
    Public properties As New List(Of cProperty) From {}
    Enum cProperty
        Flying
        Undead
        Construct

    End Enum

    Sub New(ByVal newName As String, ByVal newSpeed As UInteger, ByVal newMHP As UInteger, ByVal newAtk As Integer, ByVal newAC As Integer, ByVal newInit As UInteger, ByRef defaultBehaviour As Behaviour, Optional ByVal newResistances As Integer() = Nothing, Optional ByRef newNW As weapon = Nothing)

        defName = newName
        baseSpeed = newSpeed
        atkMod = newAtk
        baseAC = newAC
        initiative = newInit
        behaviour = defaultBehaviour
        maxHp = newMHP

        If newResistances Is Nothing Then newResistances = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        resistances = newResistances
        If newNW Is Nothing Then newNW = naturalWeapons.slam
        natural_weapon = newNW

        id = count
        count += 1
    End Sub
    Function canFly() As Boolean
        Return Not properties.IndexOf(cProperty.Flying) = -1
    End Function
End Class



Public Class humanoid
    Inherits cDefinition
    Sub New(ByVal newName As String, ByVal newMHP As Integer, ByVal newAC As Integer, ByVal newAtk As Integer, ByVal newDmg As Integer, ByVal defaultBehaviour As Behaviour, ByVal newInitiative As Integer, ByVal newNaturalWeapon As weapon, Optional ByVal newResistances() As Integer = Nothing)
        MyBase.New(newName, 6, newMHP, newAtk, newAC, newInitiative, defaultBehaviour, newResistances)

    End Sub
End Class

Public Class human
    Inherits humanoid
    Sub New(ByVal newName As String, ByVal newAccuracy As Integer, ByVal newDmg As Integer, ByVal defaultBehaviour As Behaviour, ByVal newInitiative As Integer)
        MyBase.New(newName, 100, 12, newAccuracy, newDmg, defaultBehaviour, newInitiative, naturalWeapons.fists)

    End Sub
End Class

Public Class giantWasp
    Inherits cDefinition
    Sub New()
        MyBase.New("giant wasp", 8, 70, 6, 14, 25, GUI.brute, {0, 0, 0, 0, 50, 0, 0, 0, 0, 0}, naturalWeapons.stinger)
        hitEffects = New List(Of Effect) From {New instantDmg(10, Nothing, 1, acid), New Poison(15, Nothing, 4)}
        properties.Add(cProperty.Flying)
    End Sub
End Class