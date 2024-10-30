Public Class Effect
    Public name As String
    Public duration As UInteger
    Public strength As UInteger
    Public age As UInteger
    Public target As Creature
    Public Sub New(ByVal newName As String, ByVal newStrength As UInteger, ByVal newTarget As Creature, ByVal newDuration As UInteger)
        name = newName
        strength = newStrength
        target = newTarget
        duration = newDuration
        age = 0
    End Sub

    Public Sub tick()
        If age = 0 Then GUI.print({target.name & "(" & target.iid & ") is " & name & "."})
        If duration = age Then
            remove()
        Else
            round()
            age += 1
        End If
    End Sub

    Public Sub remove()
        removed()
        GUI.print({target.name & "(" & target.iid & ") is no longer " & name & "."})
        'target.effectInf.Remove(inf)
        target.effects.Remove(Me)
    End Sub


    Public Overridable Sub start()
    End Sub

    Public Overridable Sub round()
    End Sub

    Public Overridable Sub removed()
    End Sub

    Public Overridable Function cloneTo(ByRef newTarget As Creature)
        Return New Effect(name, strength, newTarget, duration)
    End Function

End Class

Public Class instantDmg
    Inherits Effect
    Public dmgType As element
    Public Sub New(ByVal newStrength As UInteger, ByVal newTarget As Creature, ByVal newDuration As UInteger, ByVal newDmgType As element)
        MyBase.new("instant damage", newStrength, newTarget, newDuration)
        dmgType = newDmgType
    End Sub

    Public Overrides Sub start()
        'start effect here
        target.takeDmg(strength, dmgType)
        target.effects.Remove(Me)
    End Sub

    Public Overrides Function cloneTo(ByRef newTarget As Creature)
        Return New instantDmg(strength, newTarget, duration, dmgType)
    End Function
End Class

Public Class testEffect
    Inherits Effect
    Public Sub New(ByVal newStrength As UInteger, ByVal newTarget As Creature, ByVal newDuration As UInteger)
        MyBase.new("[Test effect]", newStrength, newTarget, newDuration)
    End Sub

    Public Overrides Sub round()
        'tick effect here

    End Sub

    Public Overrides Sub start()
        'start effect here

    End Sub

    Public Overrides Sub removed()
        'effect on removal here

    End Sub

    Public Overrides Function cloneTo(ByRef newTarget As Creature)
        Return New testEffect(strength, newTarget, duration)
    End Function
End Class

Public Class Poison
    Inherits Effect
    Public Sub New(ByVal newStrength As UInteger, ByVal newTarget As Creature, ByVal newDuration As UInteger)
        MyBase.new("poisoned", newStrength, newTarget, newDuration)
    End Sub

    Public Overrides Sub round()
        target.takeDmg(strength, elements.poison)
    End Sub

    Public Overrides Function cloneTo(ByRef newTarget As Creature)
        Return New Poison(strength, newTarget, duration)
    End Function
End Class

Public Class Bleeding
    Inherits Effect
    Public Sub New(ByVal newStrength As UInteger, ByVal newTarget As Creature, ByVal newDuration As UInteger)
        MyBase.new("bleeding", newStrength, newTarget, newDuration)
    End Sub

    Public Overrides Sub round()
        'tick effect here
        target.takeDmg(strength * target.curhp * 0.01, elements.nul)
    End Sub

    Public Overrides Function cloneTo(ByRef newTarget As Creature)
        Return New Bleeding(strength, newTarget, duration)
    End Function
End Class

Public Class OnFire
    Inherits Effect
    Public Sub New(ByVal newStrength As UInteger, ByVal newTarget As Creature, ByVal newDuration As UInteger)
        MyBase.new("on fire", newStrength, newTarget, newDuration)
    End Sub

    Public Overrides Sub round()
        target.takeDmg(strength, elements.heat)
        If age < duration / 2 Then strength += 1
        'tick effect here
    End Sub

    Public Overrides Function cloneTo(ByRef newTarget As Creature)
        Return New OnFire(strength, newTarget, duration)
    End Function

End Class

Public Class Confused
    Inherits Effect
    Public Sub New(ByVal newStrength As UInteger, ByVal newTarget As Creature, ByVal newDuration As UInteger)
        MyBase.new("confused", newStrength, newTarget, newDuration)
    End Sub

    Public Overrides Sub round()
        MyBase.round()
        'tick effect here
        Dim targ As Integer
        Do
            targ = Math.Ceiling(Rnd() * GUI.creatures.Count) - 1
        Loop Until GUI.creatures(targ).curhp > 0
        target.grudge = GUI.creatures(targ)
    End Sub

    Public Overrides Sub removed()
        MyBase.removed()
        target.grudge = Nothing
    End Sub

    Public Overrides Function cloneTo(ByRef newTarget As Creature)
        Return New Confused(strength, newTarget, duration)
    End Function
End Class