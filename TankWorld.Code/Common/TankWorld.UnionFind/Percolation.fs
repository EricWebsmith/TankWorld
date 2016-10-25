(*
Created By Weiguang Zhou - Oct 25, 2015

This class creates a m*n rectangular and checks if two points are connected.
It uses WeightedQuickUnion class.

*)


namespace TankWorld.UnionFind

/// <summary>
/// Creates a rectangular with width * height, the first block is  (0,0)
/// </summary>
/// <param name="width">the width of the blocks, starting from 0</param>
/// <param name="height">the height of the blocks, starting from 0</param>
type Percolation(width, height) =

    let uf = new WeightedQuickUnion(width*height)
    let mutable openSites = [for i in 1..(width*height) -> false]
    
    //in order to use WeightedQuickUnion, we need to transform the two dimension coordinates to one dimension index.
    let oneDimensionIndex x  y  =
        x + y * width

             //validate that p Is a valid index
    let validateWidth x=
        if x>=width || x<0  then raise(System.IndexOutOfRangeException("index " + x.ToString() + " is not between 0 and " + (width - 1).ToString()))
        |> ignore

             //validate that p Is a valid index
    let validateHeight y=
        if y>=height || y<0  then raise(System.IndexOutOfRangeException("index " + y.ToString() + " is not between 0 and " + (height - 1).ToString()))
        |> ignore

    let unionIfOpen (p:int) (q:int) =
        if ( openSites.Item(q)) then uf.Union(p,q)
        |> ignore

    let rec changeItemInner headTemp list item index  =
        match list with
        |[]->[]
        |h::t->if index=0 then headTemp @ [item] @ t else changeItemInner (headTemp@[h]) t item (index-1) 

    let rec changeItem = changeItemInner []

    /// <summary>
    /// true if the site is open
    /// </summary>
    /// <param name="w">the x value</param>
    /// <param name="h">the y value</param>
    /// <returns>true if the site is open</returns>
    member t.isOpen( w, h) = openSites.Item(w + h * width)

    /// <summary>
    /// Open site x, y
    /// </summary>
    /// <param name="x">x</param>
    /// <param name="y">y</param>
    member t.Open( w, h) =
        validateWidth(w)
        validateHeight(h)
        let oneDim=oneDimensionIndex w h
        if not(openSites.Item(oneDim )) then
            //  [for i in 0 .. width*height-1 -> if i=oneDim then true else openSites.Item(i)]
            openSites <- changeItem openSites  true oneDim 
            if not (w = 0) then 
                unionIfOpen oneDim (oneDim - 1)
            if not (w = width-1) then 
                unionIfOpen oneDim (oneDim + 1)
            if not (h = 0) then 
                unionIfOpen oneDim (oneDim - width)
            if not (h = height - 1) then 
                unionIfOpen oneDim (oneDim + width)
        |> ignore

    /// <summary>
    /// true if point(x1, y1) and point(x2, y2) are connected
    /// </summary>
    /// <param name="x1">x value of the first point</param>
    /// <param name="y1">y value of the first point</param>
    /// <param name="x2">x value of the second point</param>
    /// <param name="y2">y value of the second point</param>
    /// <returns>true if point(x1, y1) and point(x2, y2) are connected</returns>
    member t.Connected x1 y1 x2 y2 = 
        uf.Connected(oneDimensionIndex x1 y1, oneDimensionIndex x2 y2)



