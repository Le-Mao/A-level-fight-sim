Public Class element
    Public id As Integer
    Public name As String
    Public Sub New()
        name = GUI.dmgNames(elements.elementlist.Count)
        id = elements.elementlist.Count
        elements.elementlist.Add(Me)
    End Sub
End Class

Public Class elements
    Public Shared elementlist As List(Of element) = New List(Of element) From {}
    Public Shared nul As element = New element
    Public Shared piercing As element = New element
    Public Shared slashing As element = New element
    Public Shared bludgeoning As element = New element
    Public Shared poison As element = New element
    Public Shared heat As element = New element
    Public Shared cold As element = New element
    Public Shared acid As element = New element
    Public Shared lightning As element = New element
    Public Shared psychic As element = New element
End Class