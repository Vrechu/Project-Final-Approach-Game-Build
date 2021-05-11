using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class PortalOut : Sprite
{
    /// <summary>
    /// portal out
    /// </summary>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    public PortalOut(float px, float py) : base("Portal_Ingame.png")
    {
        SetXY(px, py);
        SetOrigin(width / 2, height / 2);
    }
}

