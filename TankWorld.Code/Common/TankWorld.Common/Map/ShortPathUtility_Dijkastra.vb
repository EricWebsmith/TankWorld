Imports TankWorld.Common

''' ''''''''''''''''''''''Weiguang Zhou  Oct 15, 2015 UTC'''''''''''''''''''''''''
''' This module finds out the shortest path 
''' by iterate every possible path.
''' It is better then the recursive algorithm.
''' It works only when there are less then 100 blocks. 
''' However, it takes minutes when there are more then 150 blocks.
''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Module ShortPathUtility

    ''' <summary>
    ''' This internal class holds parameters shared by mothods.
    ''' Otherwise, the methods will have a long parameter list. 
    ''' </summary>
    Private Class ShortPathInternal

        Dim blocks As IMapBlock(,)
        Dim endBlock As IMapBlock
        Dim startBlock As IMapBlock
        Dim shortPathLength As Integer = Integer.MaxValue

        Public Sub New(blocks As IMapBlock(,), startBlock As IMapBlock, endBlock As IMapBlock)
            Me.blocks = blocks
            Me.startBlock = startBlock
            Me.endBlock = endBlock

            For Each block In Me.blocks
                block.Distance = Integer.MaxValue
            Next
        End Sub

        ''' <summary>
        ''' iterates blocks and mark every block the distance to the distination.
        ''' </summary>
        Sub IterateBlocks()
            endBlock.Distance = 0
            Dim blocksInSearch = New List(Of IMapBlock)()
            blocksInSearch.Add(endBlock)
            'Go To 4 directions.
            While blocksInSearch.Count > 0
                Dim addList = New List(Of IMapBlock)()
                Dim removeList = New List(Of IMapBlock)()
                For Each block As IMapBlock In blocksInSearch
                    Dim addListCount = addList.Count
                    'Go to 4 directions, up, right, down, left.
                    addList.AddWithoutNull(IterateNeighbouringBlock(block, Direction.Right))
                    addList.AddWithoutNull(IterateNeighbouringBlock(block, Direction.Up))
                    addList.AddWithoutNull(IterateNeighbouringBlock(block, Direction.Down))
                    addList.AddWithoutNull(IterateNeighbouringBlock(block, Direction.Left))
                    If addList.Count = addListCount Then
                        removeList.Add(block)
                    End If
                Next
                blocksInSearch.AddRange(addList)
                removeList.ForEach(Sub(item)
                                       blocksInSearch.Remove(item)
                                   End Sub)
            End While


        End Sub

        Function IterateNeighbouringBlock(ByRef block As IMapBlock, theDirection As Direction) As IMapBlock


            Dim newX As Integer = 0
            Dim newY As Integer = 0
            If theDirection = Direction.Up Then
                newX = block.X
                newY = block.Y - 1
            ElseIf theDirection = Direction.Right
                newX = block.X + 1
                newY = block.Y
            ElseIf theDirection = Direction.Down
                newX = block.X
                newY = block.Y + 1
            ElseIf theDirection = Direction.Left
                newX = block.X - 1
                newY = block.Y
            End If

            If newX < 0 Or newX >= blocks.GetLength(0) _
               Or newY < 0 Or newY >= blocks.GetLength(1) Then
                Return Nothing
            End If

            Dim newBlock = blocks(newX, newY)

            If Not newBlock.Passable Then
                Return Nothing
            End If

            If (newBlock.Distance > block.Distance + 1) Then
                newBlock.Distance = block.Distance + 1
            Else
                Return Nothing
            End If

            'The distance is larger than the known shortest distance 
            'between the start block And the end block.
            'Thus, further search is not necessary.
            If newBlock.Distance > shortPathLength Then
                Return Nothing
            End If

            If newBlock.Equals(startBlock) Then
                shortPathLength = newBlock.Distance
            End If

            Return newBlock
        End Function

        Function GetNextSteps(block As IMapBlock) As List(Of IMapBlock)
            Dim nextSteps = New List(Of IMapBlock)
            nextSteps.AddWithoutNull(GetNextStep(block, Direction.Right))
            nextSteps.AddWithoutNull(GetNextStep(block, Direction.Up))
            nextSteps.AddWithoutNull(GetNextStep(block, Direction.Down))
            nextSteps.AddWithoutNull(GetNextStep(block, Direction.Left))
            Return nextSteps
        End Function

        Function GetNextStep(block As IMapBlock, theDirection As Direction) As IMapBlock
            Dim newX As Integer = 0
            Dim newY As Integer = 0
            If theDirection = Direction.Up Then
                newX = block.X
                newY = block.Y - 1
            ElseIf theDirection = Direction.Right
                newX = block.X + 1
                newY = block.Y
            ElseIf theDirection = Direction.Down
                newX = block.X
                newY = block.Y + 1
            ElseIf theDirection = Direction.Left
                newX = block.X - 1
                newY = block.Y
            End If

            If newX < 0 Or newX >= blocks.GetLength(0) _
               Or newY < 0 Or newY >= blocks.GetLength(1) Then
                Return Nothing
            End If

            Dim newBlock = blocks(newX, newY)

            If newBlock.Distance = block.Distance - 1 Then
                Return newBlock
            End If

            Return Nothing
        End Function

        Public Function GetShortPaths() As IEnumerable(Of MapPath)
            IterateBlocks()
            Dim shortestPaths = New List(Of MapPath)()
            If startBlock.Distance = Integer.MaxValue Then
                Return shortestPaths
            End If
            Dim firstPath As New MapPath
            firstPath.Path = New List(Of IMapBlock)()
            firstPath.Path.Add(startBlock)
            Dim paths = New List(Of MapPath)()

            paths.Add(firstPath)


            'Return shortestPaths
            While paths.Count > 0
                Dim newPaths = New List(Of MapPath)
                For Each path In paths
                    'If path.Path.Count = shortPathLength Then
                    '    Continue For
                    'End If
                    Dim nextSteps = GetNextSteps(path.Path.Last())
                    If nextSteps.Count >= 1 Then
                        Dim firstStep = nextSteps.First()

                        nextSteps.Remove(firstStep)


                        For Each blockStep In nextSteps
                            Dim newPath = path.Clone()
                            newPath.Path.Add(blockStep)
                            newPaths.Add(newPath)
                        Next
                        path.Path.Add(firstStep)
                    End If
                Next
                paths.AddRange(newPaths)

                Dim removeList = New List(Of MapPath)
                For Each path In paths
                    If path.Path.Last().Equals(endBlock) Then
                        shortestPaths.Add(path)
                        removeList.Add(path)
                    End If
                Next
                removeList.ForEach(Sub(item)
                                       paths.Remove(item)
                                   End Sub)

            End While
            Return shortestPaths
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
        Dim internalClass As New ShortPathInternal(blocks, startBlock, endBlock)
        Return internalClass.GetShortPaths()
    End Function

End Module
