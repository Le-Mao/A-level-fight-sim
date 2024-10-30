Imports Coursework_Project.elements
Public Class Power
    Public offensive As Boolean = True
    Public lvl As Integer = 1
    Public ap As Integer = 3
    Public stamina As Integer = 0
    Public range As Decimal
    Public priority As Integer

    Public Sub New(ByVal newLvl As Integer, ByVal newstamina As Integer, Optional ByVal newRange As Decimal = 1.8, Optional ByVal newPriority As Integer = Nothing)
        lvl = newLvl
        stamina = newstamina
        range = newRange
        priority = newPriority
    End Sub

    Sub use(ByRef target As Creature, ByRef user As Creature)
        Dim dist = position.distance(user.pos, target.pos)
        If dist <= range Then
            'user.ap -= ap
            'user.stamina -= stamina
            used(target, user)
        End If
    End Sub

    Protected Overridable Sub used(ByRef target As Creature, ByRef user As Creature)
    End Sub
End Class

Public Class emptyPower
    Inherits Power

    Public Sub New(ByVal newLvl As Integer)
        MyBase.New(newLvl, 0)
    End Sub

    Protected Overrides Sub used(ByRef target As Creature, ByRef user As Creature)

    End Sub
End Class

Public Class vampiricStrike
    Inherits Power
    Protected Overrides Sub used(ByRef target As Creature, ByRef user As Creature)
        Dim initialHP As Decimal = target.curhp
        Dim healMult As Decimal = lvl * 0.1 + 0.2
        If user.attack(target) = True Then user.takeDmg((target.curhp - initialHP) * healMult, nul)
    End Sub

    Public Sub New(ByVal newLvl As Integer)
        MyBase.New(newLvl, 20)
    End Sub
End Class

Public Class psychicScreech
    Inherits Power
    Protected Overrides Sub used(ByRef target As Creature, ByRef user As Creature)
        GUI.print({user.name & "(" & user.iid & ") used Psychic Screech on " & target.name & "(" & target.iid & ")."})
        target.takeDmg((lvl + 2) * 5, psychic)
        target.addEffect(New Confused(1, target, 3 + lvl * 2))
    End Sub

    Public Sub New(ByVal newLvl As Integer)
        MyBase.New(newLvl, 20, (newLvl + 2) * 4)
    End Sub
End Class

Public Class bloodLetting
    Inherits Power

    Public Sub New(ByVal newLvl As Integer)
        MyBase.New(newLvl, 5)
    End Sub

    Protected Overrides Sub used(ByRef target As Creature, ByRef user As Creature)
        If user.attack(target) = True Then
            target.addEffect(New Bleeding(lvl, target, lvl + 2))
        End If
    End Sub
End Class

Public Class flurry
    Inherits Power
    Public Sub New(ByVal newLvl As Integer)
        MyBase.New(newLvl, 10)
    End Sub

    Protected Overrides Sub used(ByRef target As Creature, ByRef user As Creature)
        GUI.print({user.name & "(" & user.iid & ") used Flurry of Blows!"})

        Dim originalDmg = user.def.baseDmg
        user.def.baseDmg *= 0.75
        For i = 0 To lvl
            If user.hits(target.def.baseAC) Then user.onhit(target)
        Next
        user.def.baseDmg = originalDmg
    End Sub
End Class