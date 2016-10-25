''' <summary>
''' The block Class
''' a map Is consisted With several blocks.
''' a block Is a square area.
''' the position Of a block In a map Is denoted As x And y.
''' </summary>
Public Interface IMapBlock
    Inherits IXY
    Property Passable As Boolean
    Property Distance As Integer
End Interface
