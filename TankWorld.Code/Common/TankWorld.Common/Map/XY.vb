Public Class XY
    Implements IXY

    Dim _x As Integer
    Dim _y As Integer

    Public Sub New(x As Integer, y As Integer)
        _x = x
        _y = y
    End Sub


    Public ReadOnly Property X As Integer Implements IXY.X
        Get
            Return _x
        End Get
    End Property

    Public ReadOnly Property Y As Integer Implements IXY.Y
        Get
            Return _y
        End Get
    End Property
End Class
