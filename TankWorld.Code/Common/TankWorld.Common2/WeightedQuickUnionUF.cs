///////////////////Weiguang Zhou Oct 15, 2015 UCT//////////
/// This class is a translation of the java class from Princton University.
/// URL of the original class:
/// http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/WeightedQuickUnionUF.java.html
///////////////////////////////////////////////////////////////////

using System;

namespace TankWorld.Common
{

    /// <summary>
    /// ///////////////////Weiguang Zhou Oct 15, 2015 UCT//////////
    /// This class is a translation of the java class from Princton University.
    /// URL of the original class:
    /// http://algs4.cs.princeton.edu/code/edu/princeton/cs/algs4/WeightedQuickUnionUF.java.html
    /// </summary>
    [Obsolete("Please use the F# code",true)]
    public class WeightedQuickUnionUF
    {
        private int[] parent;   // parent[i] = parent of i
        private int[] size;     // size[i] = number of sites in subtree rooted at i
        private int count;      // number of components

        /// <summary>
        /// * Initializes an empty union-find data structure with <tt>N</tt> sites
        /// * <tt>0</tt> through <tt>N-1</tt>. Each site Is initially in its own 
        /// * component.
        /// </summary>
        /// <param name="n">the number of sites</param>
        /// <exception cref="ArgumentOutOfRangeException">if n&lt;0</exception>
        public WeightedQuickUnionUF(int N)
        {
            count = N;
            parent = new int[N];
            size = new int[N];
            for (int i = 0; i < N; i++)
            {
                parent[i] = i;
                size[i] = 1;
            }
        }

        /// <summary>
        /// Returns the number of components.
        /// </summary>
        /// <returns>the number of components (between <tt>1</tt> And <tt>N</tt>)</returns>
        public int GetCount()
        {
            return count;
        }

        /// <summary>
        /// Returns the component identifier for the component containing site <tt>p</tt>.
        /// </summary>
        /// <param name="p">the integer representing one object</param>
        /// <returns>the component identifier for the component containing site <tt>p</tt></returns>
        /// <exception cref="IndexOutOfRangeException">unless <tt>0 &lt;= p &lt; N</tt></exception>
        public int Find(int p)
        {
            Validate(p);
            while (p != parent[p])
                p = parent[p];
            return p;
        }

        // validate that p is a valid index
        private void Validate(int p)
        {
            int N = parent.Length;
            if (p < 0 || p >= N)
            {
                throw new IndexOutOfRangeException("index " + p + " is not between 0 and " + (N - 1));
            }
        }

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
        public bool Connected(int p, int q)
        {
            return Find(p) == Find(q);
        }

        /// <summary>
        /// Merges the component containing site <tt>p</tt> with the 
        ///     * the component containing site <tt>q</tt>.
        /// </summary>
        /// <param name="p">the integer representing one site</param>
        /// <param name="q">the integer representing the other site</param>
        /// <exception cref="IndexOutOfRangeException">unless both <tt>0 &lt;= p &lt; N</tt> And <tt>0 &lt;= q &lt; N</tt></exception>
        public void Union(int p, int q)
        {
            int rootP = Find(p);
            int rootQ = Find(q);
            if (rootP == rootQ) return;

            // make smaller root point to larger one
            if (size[rootP] < size[rootQ])
            {
                parent[rootP] = rootQ;
                size[rootQ] += size[rootP];
            }
            else
            {
                parent[rootQ] = rootP;
                size[rootP] += size[rootQ];
            }
            count--;
        }
    }
}
