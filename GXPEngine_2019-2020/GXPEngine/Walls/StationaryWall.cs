using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class StationaryWall : SolidObject
{
    public StationaryWall(float px, float py, int frame) : base("Tiles_and_portal.png", 30,16, px, py)
    {
        SetFrame(frame);
    }
}