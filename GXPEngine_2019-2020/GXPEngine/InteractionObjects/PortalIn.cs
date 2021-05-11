using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class PortalIn : Sprite
{
    /// <summary>
    /// portal in
    /// </summary>
    /// <param name="px">object x position</param>
    /// <param name="py">object y positions</param>
    public PortalIn(float px, float py) : base("Portal_Ingame.png")
    {
        SetXY(px, py);
        SetOrigin(width / 2, height / 2);
    }
}

