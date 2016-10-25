Imports System.Runtime.CompilerServices

Public Module DirectionHelper
    Public Function GetDirection(x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer) As Direction
        If x2 > x1 And y1 = y2 Then Return Direction.Right
        If x2 < x1 And y1 = y2 Then Return Direction.Left
        If y2 > y1 And x1 = x2 Then Return Direction.Down
        If y2 < y1 And x1 = x2 Then Return Direction.Up
        Return Direction.Up
        ''Throw New NotImplementedException("Sorry, code for this is not implemented.")
    End Function

    <Extension>
    Public Function GetDirection(currentBlock As IMapBlock, destinationBlock As IMapBlock) As Direction
        Return GetDirection(currentBlock.X, currentBlock.Y, destinationBlock.X, destinationBlock.Y)
    End Function

    <Extension>
    Public Function ToAngle(d As Direction) As Integer
        Select Case d
            Case Direction.Up
                Return 270
            Case Direction.Right
                Return 0
            Case Direction.Down
                Return 90
            Case Direction.Left
                Return 180
        End Select
        Return -1
    End Function

    ''' <summary>
    ''' Get the next step of direction d.
    ''' </summary>
    ''' <param name="d"></param>
    ''' <returns>The stepping function that modifies the x or y value and makes the coordination move towards the given direction d.</returns>
    <Extension>
    Public Function GetSteppingFunc(d As Direction) As Func(Of Integer, Integer, XY)

        Select Case d
            Case Direction.Up
                Return Function(x As Integer, y As Integer)
                           Return New XY(x, y - 1)
                       End Function
            Case Direction.Right
                Return Function(x As Integer, y As Integer)
                           Return New XY(x + 1, y)
                       End Function
            Case Direction.Down
                Return Function(x As Integer, y As Integer)
                           Return New XY(x, y + 1)
                       End Function
            Case Direction.Left
                Return Function(x As Integer, y As Integer)
                           Return New XY(x - 1, y)
                       End Function
            Case Else
                Throw New NotImplementedException("What? Please double check your direction!!!")
        End Select
    End Function

    <Extension>
    Public Function GetIsOpposite(d1 As Direction, d2 As Direction) As Boolean
        If (d1 = Direction.Up And d2 = Direction.Down) Then Return True
        If (d1 = Direction.Down And d2 = Direction.Up) Then Return True
        If (d1 = Direction.Left And d2 = Direction.Right) Then Return True
        If (d1 = Direction.Right And d2 = Direction.Left) Then Return True
        Return False
    End Function

End Module
