namespace TankWorld.UnionFind

 /// <summary>
 /// * Initializes an empty union-find data structure with <tt>N</tt> sites
 /// * <tt>0</tt> through <tt>N-1</tt>. Each site Is initially in its own 
 /// * component.
 /// </summary>
 /// <param name="n">the number of sites</param>
 /// <exception cref="ArgumentOutOfRangeException">when things go wrong.</exception>
 type WeightedQuickUnion (N) =
     //parent.[i] = parent Of i
     let parent : int[] = Array.init N (fun i -> i) 
     //size.[i] = number Of sites In subtree rooted at i
     let size : int[] = Array.create N 0
     //number Of components
     let mutable count=0
     
     let root i = 
         let mutable q = i
         while (q <> parent.[q]) do q <- parent.[q] 
         q

     //validate that p Is a valid index
     let validate i=
        if i>=N || i<0  then raise(System.IndexOutOfRangeException("index " + i.ToString() + " is not between 0 and " + (parent.Length - 1).ToString()))
        |> ignore

        /// <summary>
        /// Returns the number of components.
        /// </summary>
        /// <returns>the number of components (between <tt>1</tt> And <tt>N</tt>)</returns>
     member t.GetCount() = count

        /// <summary>
        /// Returns true if the the two sites are in the same component.
        /// </summary>
        /// <param name="p">the integer representing one site</param>
        /// <param name="q">the integer representing the other site</param>
        /// <returns>
        /// <b>true</b> if the two sites <tt>p</tt> And <tt>q</tt> are in the same component;
        /// <tt>false</tt> otherwise
        ///</returns>
        /// <exception cref="IndexOutOfRangeException">unless <tt>0 &lt;= p &lt; N</tt> And <tt>0 &lt;= q &lt; N</tt></exception>
     member t.Connected(p, q) =
         validate(p)
         validate(q)
         root(p) = root(q)
 
        /// <summary>
        /// Merges the component containing site <tt>p</tt> with the 
        ///     * the component containing site <tt>q</tt>.
        /// </summary>
        /// <param name="p">the integer representing one site</param>
        /// <param name="q">the integer representing the other site</param>
        /// <exception cref="IndexOutOfRangeException">unless both <tt>0 &lt;= p &lt; N</tt> And <tt>0 &lt;= q &lt; N</tt></exception>
     member t.Union(p, q) =
         validate(p)
         validate(q)
         let i = root(p)
         let j = root(q)
         if size.[i] < size.[j] then parent.[i] <- j; size.[j] <- size.[j] + size.[i]
         else parent.[j] <- i; size.[i] <- size.[i] + size.[j]

