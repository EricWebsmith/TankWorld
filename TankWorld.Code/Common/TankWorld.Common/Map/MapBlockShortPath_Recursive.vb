''' ''''''''''''''''''''''Weiguang Zhou  Oct 14, 2015'''''''''''''''''''''''''
''' This module finds out the shortest path 
''' by iterate every possible path recursively.
''' It is too slow and cannot be used in reality.
''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''


Public Module MapBlockShortPathUtility_Recursive

    ''' <summary>
    ''' This internal class holds parameters shared by mothods.
    ''' Otherwise, the methods will have a long parameter list. 
    ''' </summary>
    Private Class MapBlockShortPath_RecursiveInternal

        Dim blocks As IEnumerable(Of IMapBlock)
        Dim endBlock As IMapBlock
        Dim startBlock As IMapBlock
        Dim shortPathLength As Integer = Integer.MaxValue

        Public Sub New(blocks As IEnumerable(Of IMapBlock), startBlock As IMapBlock, endBlock As IMapBlock)
            Me.blocks = blocks
            Me.startBlock = startBlock
            Me.endBlock = endBlock
        End Sub

        Public Function GetPaths() As IEnumerable(Of MapPath)
            Dim paths = New List(Of MapPath)()
            Dim path = New MapPath()
            Dim list As New List(Of IMapBlock)()
            list.Add(startBlock)
            path.Path = list

            'Go To 4 directions.
            paths = Goto4Directions(path)

            Return paths
        End Function

        ''' <summary>
        ''' Go To 4 directions. Recursively.
        ''' </summary>
        ''' <param name="path"></param>
        ''' <returns></returns>
        Private Function Goto4Directions(ByRef path As MapPath) As IEnumerable(Of MapPath)
            Dim paths = New List(Of MapPath)()
            'Go To 4 directions clockwise. up, right, down, left.
            While True
                'Go up
                paths.AddRange(GotoNeighbouringBlock(path, Direction.Up))
                'Go Right
                paths.AddRange(GotoNeighbouringBlock(path, Direction.Right))
                'Go Down
                paths.AddRange(GotoNeighbouringBlock(path, Direction.Down))
                'Go Left
                paths.AddRange(GotoNeighbouringBlock(path, Direction.Left))
            End While

            Return paths
        End Function

        Function GotoNeighbouringBlock(ByRef path As MapPath, theDirection As Direction) As List(Of MapPath)

            Dim lastBlock = path.Path.Last()
            Dim newX As Integer = 0
            Dim newY As Integer = 0
            If theDirection = Direction.Up Then
                newX = lastBlock.X
                newY = lastBlock.Y - 1
            ElseIf theDirection = Direction.Right
                newX = lastBlock.X + 1
                newY = lastBlock.Y
            ElseIf theDirection = Direction.Down
                newX = lastBlock.X
                newY = lastBlock.Y + 1
            ElseIf theDirection = Direction.Left
                newX = lastBlock.X - 1
                newY = lastBlock.Y
            End If

            Dim newLastQuery = From b In blocks Where b.X = newX And b.Y = newY Select b
            If newLastQuery.Count = 0 Then
                Return New List(Of MapPath)()
            End If
            Dim newLast = newLastQuery.First()
            If Not blocks.Contains(newLast) Then
                Return New List(Of MapPath)()
            End If

            Dim newPath = path.Clone()
            Dim newList = newPath.Path.ToList()
            newList.Add(newLast)
            newPath.Path = newList
            If newPath.Path.Count() > shortPathLength Then
                Return New List(Of MapPath)()
            End If

            Dim newPaths = New List(Of MapPath)()
            If newLast.X = endBlock.X And newLast.Y = endBlock.Y Then

                shortPathLength = newPath.Path.Count()
                newPath.EndPointFound = True
                newPaths.Add(newPath)
                Return newPaths
            End If

            newPaths = Goto4Directions(newPath)

            Return newPaths
        End Function

        Public Function GetShortPaths() As IEnumerable(Of MapPath)
            Dim paths = GetPaths()
            Dim validPaths = (From p In paths Where p.EndPointFound And p.Path.Count() = shortPathLength).ToList()
            Return validPaths
        End Function
    End Class

    ''' <summary>
    ''' Gets the shortest paths.
    ''' </summary>
    ''' <param name="blocks">A map with m*n blocks. Each block is a square.</param>
    ''' <param name="startBlock">The start block.</param>
    ''' <param name="endBlock">The end block.</param>
    ''' <returns>Shortest paths</returns>
    Public Function GetShortPaths(blocks As IEnumerable(Of IMapBlock), startBlock As IMapBlock, endBlock As IMapBlock) As IEnumerable(Of MapPath)
        Dim internalClass As New MapBlockShortPath_RecursiveInternal(blocks, startBlock, endBlock)
        Return internalClass.GetShortPaths()
    End Function

End Module
