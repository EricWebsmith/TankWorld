'''''''''''''''''''''''''''Weiguang Zhou Oct 15, 2015 UCT'''''''''''''''''''
''' This class Is a translation of the java class from Robert Sedgewick and author Kevin Wayne
''' , Princton University.
''' URL of the original class:
''' http//algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/WeightedQuickUnionUF.java.html
''' ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

Public Class VB_WeightedQuickUnionUF

    Dim parent() As Integer    ' parent[i] = parent Of i
    Dim size() As Integer     ' size[i] = number Of sites In subtree rooted at i
    Dim count As Integer  ' number Of components


    ''' <summary>
    ''' * Initializes an empty union-find data structure with <tt>N</tt> sites
    ''' * <tt>0</tt> through <tt>N-1</tt>. Each site Is initially in its own 
    ''' * component.
    ''' </summary>
    ''' <param name="n">the number of sites</param>
    ''' <exception cref="ArgumentOutOfRangeException">if n&lt;0</exception>
    Public Sub New(n As Integer)
        If n < 0 Then
            Throw New ArgumentOutOfRangeException()
        End If
        count = n
        parent = New Integer(n) {}
        size = New Integer(n) {}
        For i As Integer = 0 To n - 1
            parent(i) = i
            size(i) = 1
        Next
    End Sub

    ''' <summary>
    ''' Returns the number of components.
    ''' </summary>
    ''' <returns>the number of components (between <tt>1</tt> And <tt>N</tt>)</returns>
    Public Function GetCount() As Integer
        Return count
    End Function

    ''' <summary>
    ''' Returns the component identifier for the component containing site <tt>p</tt>.
    ''' </summary>
    ''' <param name="p">the integer representing one object</param>
    ''' <returns>the component identifier for the component containing site <tt>p</tt></returns>
    ''' <exception cref="IndexOutOfRangeException">unless <tt>0 &lt;= p &lt; N</tt></exception>
    Public Function Find(p As Integer) As Integer
        Validate(p)
        While (p <> parent(p))
            p = parent(p)
        End While

        Return p
    End Function

    ''' <summary>
    ''' validate that p Is a valid index
    ''' </summary>
    ''' <param name="p"></param>
    Private Sub Validate(p As Integer)
        If (p < 0 Or p >= parent.Length) Then
            Throw New IndexOutOfRangeException("index " + p + " is not between 0 and " + (parent.Length - 1))
        End If
    End Sub

    ''' <summary>
    ''' Returns true if the the two sites are in the same component.
    ''' </summary>
    ''' <param name="p">the integer representing one site</param>
    ''' <param name="q">the integer representing the other site</param>
    ''' <returns>
    ''' <b>true</b> if the two sites <tt>p</tt> And <tt>q</tt> are in the same component;
    ''' <tt>false</tt> otherwise
    '''</returns>
    ''' <exception cref="IndexOutOfRangeException">unless <tt>0 &lt;= p &lt; N</tt> And <tt>0 &lt;= q &lt; N</tt></exception>
    Public Function Connected(p As Integer, q As Integer) As Boolean
        Validate(p)
        Validate(q)
        Return Find(p) = Find(q)
    End Function

    ''' <summary>
    ''' Merges the component containing site <tt>p</tt> with the 
    '''     * the component containing site <tt>q</tt>.
    ''' </summary>
    ''' <param name="p">the integer representing one site</param>
    ''' <param name="q">the integer representing the other site</param>
    ''' <exception cref="IndexOutOfRangeException">unless both <tt>0 &lt;= p &lt; N</tt> And <tt>0 &lt;= q &lt; N</tt></exception>
    Public Sub Union(p As Integer, q As Integer)
        Validate(p)
        Validate(q)
        Dim rootP = Find(p)
        Dim rootQ = Find(q)
        If (rootP = rootQ) Then Return

        ' make smaller root point to larger one
        If (size(rootP) < size(rootQ)) Then
            parent(rootP) = rootQ
            size(rootQ) = size(rootQ) + size(rootP)
        Else
            parent(rootQ) = rootP
            size(rootP) = size(rootP) + size(rootQ)
        End If

        count = count - 1
    End Sub

End Class
