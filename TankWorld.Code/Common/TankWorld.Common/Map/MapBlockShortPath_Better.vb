Imports TankWorld.Common

''' ''''''''''''''''''''''Weiguang Zhou  Oct 15, 2015 UTC'''''''''''''''''''''''''
''' This module finds out the shortest path 
''' by iterate every possible path.
''' It is better then the recursive algorithm.
''' It works only when there are less then 100 blocks. 
''' However, it takes minutes when there are more then 150 blocks.
''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Module MapBlockShortPathUtility_Better

    ''' <summary>
    ''' This internal class holds parameters shared by mothods.
    ''' Otherwise, the methods will have a long parameter list. 
    ''' </summary>
    Private Class MapBlockShortPathInternal_Better

        Dim blocks As IMapBlock(,)
        Dim endBlock As IMapBlock
        Dim startBlock As IMapBlock
        Dim shortPathLength As Integer = Integer.MaxValue

        Public Sub New(blocks As IMapBlock(,), startBlock As IMapBlock, endBlock As IMapBlock)
            Me.blocks = blocks
            Me.startBlock = startBlock
            Me.endBlock = endBlock
        End Sub

        Function GetPaths() As IEnumerable(Of MapPath)
            Dim paths = New List(Of MapPath)()
            Dim startPath = New MapPath()
            Dim theList As New List(Of IMapBlock)()
            theList.Add(startBlock)
            startPath.Path = theList
            paths.Add(startPath)
            'Go To 4 directions.
            While True
                Dim removeList As New List(Of MapPath)
                Dim addList As New List(Of MapPath)
                Dim notFinishedPaths = (From p In paths Where Not p.DeadEnded _
                                                       And Not p.EndPointFound _
                                                       And p.Path.Count < shortPathLength).ToList()
                For Each path As MapPath In notFinishedPaths
                    Dim addListCount = addList.Count
                    'Go to 4 directions, up, right, down, left.
                    addList.AddWithoutNull(GotoNeighbouringBlock(path, Direction.Right))
                    addList.AddWithoutNull(GotoNeighbouringBlock(path, Direction.Up))
                    addList.AddWithoutNull(GotoNeighbouringBlock(path, Direction.Down))
                    addList.AddWithoutNull(GotoNeighbouringBlock(path, Direction.Left))

                    If addList.Count > addListCount Then
                        removeList.Add(path)
                    Else
                        path.DeadEnded = True
                    End If
                Next
                'Add new paths (the old path and a neighbouring block)
                paths.AddRange(addList)
                'remove old path
                For Each path In removeList
                    paths.Remove(path)
                Next
                'If there is no new path to add, exit.
                If addList.Count = 0 Then
                    Exit While
                End If
            End While

            Return paths
        End Function

        Function GotoNeighbouringBlock(ByRef path As MapPath, theDirection As Direction) As MapPath

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

            If newX < 0 Or newX >= blocks.GetLength(0) _
               Or newY < 0 Or newY >= blocks.GetLength(1) Then
                Return Nothing
            End If

            If Not blocks(newX, newY).Passable Then
                Return Nothing
            End If

            If path.Path.Contains(blocks(newX, newY)) Then
                Return Nothing
            End If


            Dim newPath = path.Clone()
            Dim newList = newPath.Path.ToList()
            newList.Add(blocks(newX, newY))
            newPath.Path = newList
            If newPath.Path.Count() > shortPathLength Then
                Return Nothing
            End If

            If newX = endBlock.X And newY = endBlock.Y Then


                newPath.EndPointFound = True
                shortPathLength = newPath.Path.Count()
            End If

            Return newPath
        End Function

        Public Function GetShortPaths() As IEnumerable(Of MapPath)
            Dim paths = GetPaths()
            Dim pathQueryable = From p In paths Where p.EndPointFound And p.Path.Count() = shortPathLength
            Dim validPaths = pathQueryable.ToList()
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
    Public Function GetShortPaths(blocks As IMapBlock(,), startBlock As IMapBlock, endBlock As IMapBlock) As IEnumerable(Of MapPath)
        Dim internalClass As New MapBlockShortPathInternal_Better(blocks, startBlock, endBlock)
        Return internalClass.GetShortPaths()
    End Function

End Module
