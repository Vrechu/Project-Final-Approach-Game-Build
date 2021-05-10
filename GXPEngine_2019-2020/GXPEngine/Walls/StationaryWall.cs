using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class StationaryWall : SolidObject
{
    /// <summary>
    /// wall that does not move with gravity
    /// </summary>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    /// <param name="frame">animation sprite frame</param>
    public StationaryWall(float px, float py, int frame) : base("Tiles.png", 30,16, px, py)
    {
        SetFrame(frame);
    }
}