using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using TankWorld.Core;

namespace TankWorld.Test.Common
{
    [TestClass]
    public class ShortPathTest
    {
        /// <summary>
        /// Short Path in a Clear map(no bricks). 
        /// The result should be a strait line from Block[0,3] to Block[9,3]
        /// </summary>
        [TestMethod]
        public void ShortPath1_Clear()
        {
            Map theMap = new Map(10, 10);
            theMap.CreateFlagAndTanks();
            //expected result
            List<Block> expectedPath = new List<Block>();
            for (int i = 0; i <= 9; i++)
            {
                expectedPath.Add(theMap.Blocks[i, 3]);
            }

            List<Block> clearBlocks = new List<Block>();
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            //TankWorld.Common.WeightedQuickUnionUF uf = new TankWorld.Common.WeightedQuickUnionUF(theMap.Blocks.Length);
            //TankWorld.Common.Percolation_VB mapPercolation = new TankWorld.Common.Percolation_VB(10, 10);
            TankWorld.UnionFind.Percolation mapPercolation = new UnionFind.Percolation(10, 10);
            foreach (var block in theMap.Blocks)
            {
                if (block.Passable)
                {
                    mapPercolation.Open(block.X, block.Y);
                }
            }
            sw.Stop();
            Debug.Print(sw.Elapsed.ToString());
            bool isConnected = mapPercolation.Connected(0, 3, 9, 3);
            Assert.IsTrue(isConnected, "The two points should be connected.");
            var actualPath = TankWorld.Common.ShortPathUtility.GetShortPaths(theMap.Blocks, theMap.Blocks[0, 3], theMap.Blocks[9, 3]).First().Path.ToList();

            Assert.AreEqual(actualPath.Count, expectedPath.Count, string.Format("The actual path and the expected path are not of the same length. Actual Path:{0}, Expected Path:{1}", actualPath.Count(), expectedPath.Count()));
            for (int i = 0; i < actualPath.Count; i++)
            {
                if (actualPath[i].X != expectedPath[i].X || actualPath[i].Y != expectedPath[i].Y)
                {
                    Assert.Fail("The actual and expected paths are different!");
                }

            }
        }

        /// <summary>
        /// Short Path in a Clear map(no bricks). 
        /// The result should be a strait line from Block[0,3] to Block[9,3]
        /// </summary>
        [TestMethod]
        public void ShortPath1_Random_SmockTest()
        {
            TankWorld.Core.Map theMap = new Map(24, 14);
            theMap.CreateFlagAndTanks();
            theMap.CreateRandomBricks();

            bool isConnected = theMap.IsConnected(theMap.Blocks[0, 3], theMap.Blocks[9, 3]);
            var actualPaths = TankWorld.Common.ShortPathUtility.GetShortPaths(theMap.Blocks, theMap.Blocks[0, 3], theMap.Blocks[9, 3]).ToList();

            bool hasShortPaths = actualPaths.Count > 0;
            Assert.AreEqual(isConnected, hasShortPaths,
               string.Format("Two points must be connected and have a short path at the same time."
               + " Connected:{0}, HasShortPaths:{1}", isConnected, hasShortPaths
               ));

            //Assert.AreEqual(actualPath.Count, expectedPath.Count, string.Format("The actual path and the expected path are not of the same length. Actual Path:{0}, Expected Path:{1}", actualPath.Count(), expectedPath.Count()));
            //for (int i = 0; i < actualPath.Count; i++)
            //{
            //    if (actualPath[i].X != expectedPath[i].X || actualPath[i].Y != expectedPath[i].Y)
            //    {
            //        Assert.Fail("The actual and expected paths are different!");
            //    }

            //}
        }

        /// <summary>
        /// Inter-test between UnionFind(has a path) and Dijkstra(find the path)
        /// if there is a path, we should be able to find it and vice versa.
        /// </summary>
        [TestMethod]
        public void UnionFind_Dijkstra_Test()
        {
            TankWorld.Core.Map theMap = new Map(10, 10);
            theMap.CreateFlagAndTanks();
            theMap.CreateRandomBricks();

            bool isConnected = theMap.IsConnected(theMap.Blocks[0, 5], theMap.Blocks[9, 5]);
            Debug.Print("\tConnected:"+isConnected.ToString());
            var actualPaths = TankWorld.Common.ShortPathUtility.GetShortPaths(theMap.Blocks, theMap.Blocks[0, 5], theMap.Blocks[9, 5]).ToList();
            Debug.Print("\tPaths:"+actualPaths.Count);
            bool hasShortPaths = actualPaths.Count > 0;
            Assert.AreEqual(isConnected, hasShortPaths,
               string.Format("Two points must be connected and have a short path at the same time."
               + " Connected:{0}, HasShortPaths:{1}", isConnected, hasShortPaths
               ));
        }

        /// <summary>
        /// Calls UnionFind_Dijkstra_Test 1000 times.
        /// </summary>
        [TestMethod]
        public void UnionFind_Dijkstra_Test1000()
        {
            Debug.Print("Test starts.");
            Debug.Print("Test starts.");
            for (int i = 0; i < 1000; i++)
            {
                Debug.Print("Loop "+i.ToString());
                UnionFind_Dijkstra_Test();
            }
        }
    }
}
