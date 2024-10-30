Imports Coursework_Project.elements


Public Class item
    Public name As String
    Public isWeapon As Boolean = False
End Class


Public Class weapon
    Inherits item
    Public range As Integer = 1
    Public dmgMult As Decimal = 1
    Public dmgAdd As Integer
    Public dmgType As element
    Public Accuracy As Integer
    Public properties As List(Of String) = New List(Of String) From {}
    Public hiteffects As List(Of Effect) = New List(Of Effect) From {}
    Public Sub New(ByVal newName As String, ByVal newDmgType As element)
        name = newName
        dmgType = newDmgType
        isWeapon = True
    End Sub

    Public Overridable Sub hit(ByVal target As Creature)
    End Sub
End Class

Public Class Sword
    Inherits weapon
    Public Sub New(ByVal newName As String, ByVal newDmgType As element)
        MyBase.New(newName, newDmgType)
        Accuracy = 5
    End Sub
End Class
Public Class Dagger
    Inherits Sword
    Public Sub New()
        MyBase.New("dagger", piercing)
        dmgMult = 1
        properties = New List(Of String) From {"thrown", "light", "offhand"}
    End Sub
End Class
Public Class Shortsword
    Inherits Sword
    Public Sub New()
        MyBase.New("shortsword", piercing)
        dmgMult = 1.25
        properties = New List(Of String) From {"light", "offhand"}
    End Sub
End Class
Public Class Longsword
    Inherits Sword
    Public Sub New()
        MyBase.New("longsword", slashing)
        dmgMult = 1.5
    End Sub
End Class
Public Class flametongue
    Inherits Longsword
    Public Overrides Sub hit(ByVal target As Creature)
        target.takeDmg(GUI.xDy(2, 6), heat)
    End Sub

    Public Sub New()
        name = "flametongue"
    End Sub
End Class
Public Class Greatsword
    Inherits Sword
    Public Sub New()
        MyBase.New("greatsword", slashing)
        dmgMult = 1.75
        properties.Add("2-handed")
    End Sub
End Class
Public Class Glaive
    Inherits Sword
    Public Sub New()
        MyBase.New("glaive", slashing)
        dmgMult = 1.5
        properties = New List(Of String) From {"2-handed", "reach"}
        range = 2
    End Sub
End Class

Public Class Axe
    Inherits weapon
    Public Sub New(ByVal newName As String)
        MyBase.New(newName, slashing)
        Accuracy = 2
    End Sub
End Class
Public Class Handaxe
    Inherits Axe
    Public Sub New()
        MyBase.New("handaxe")
        dmgMult = 1.25
        properties = New List(Of String) From {"thrown", "offhand"}
    End Sub
End Class
Public Class Battleaxe
    Inherits Axe
    Public Sub New()
        MyBase.New("battleaxe")
        dmgMult = 1.75
    End Sub
End Class
Public Class Greataxe
    Inherits Axe
    Public Sub New()
        MyBase.New("greataxe")
        dmgMult = 2
        properties = New List(Of String) From {"heavy", "2-handed"}
    End Sub
End Class
Public Class Halberd
    Inherits Axe
    Public Sub New()
        MyBase.New("halberd")
        dmgMult = 1.75
        properties = New List(Of String) From {"heavy", "2-handed", "reach"}
        range = 2
    End Sub
End Class

Public Class Hammer
    Inherits weapon
    Public Sub New(ByVal newName As String)
        MyBase.New(newName, bludgeoning)
        properties = New List(Of String) From {"stunning crits"}
        Accuracy = 1
    End Sub
End Class
Public Class ThrowingHammer
    Inherits Hammer
    Public Sub New()
        MyBase.New("throwing hammer")
        dmgMult = 1.25
        properties.Add("thrown")
        properties.Add("offhand")
    End Sub
End Class
Public Class Warhammer
    Inherits Hammer
    Public Sub New()
        MyBase.New("warhammer")
        dmgMult = 1.75
    End Sub
End Class
Public Class Maul
    Inherits Hammer
    Public Sub New()
        MyBase.New("maul")
        dmgMult = 2
        properties.Add("2-handed")
        properties.Add("heavy")
    End Sub
End Class
Public Class Lucerne
    Inherits Hammer
    Public Sub New()
        MyBase.New("lucerne hammer")
        dmgMult = 1.75
        properties.Add("reach")
        properties.Add("2-handed")
        properties.Add("heavy")
        range = 2
    End Sub
End Class