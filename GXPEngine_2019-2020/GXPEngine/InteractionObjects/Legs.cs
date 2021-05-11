using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Legs : Sprite
{
    /// <summary>
    /// legs powerup
    /// </summary>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    public Legs(float px, float py) : base("Leg_Collectible.png")
    {
        SetXY(px, py);
        SetOrigin(width / 2, height / 2);
    }
}

