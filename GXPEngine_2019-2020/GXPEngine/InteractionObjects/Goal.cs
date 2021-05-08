﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Goal : Sprite
{
    /// <summary>
    /// goal to reach in level
    /// </summary>
    /// <param name="px">objec x position</param>
    /// <param name="py">object y position</param>
    public Goal(float px, float py) : base("goal.png")
    {
        SetXY(px, py);
        SetOrigin(width / 2, height / 2);
    }
}

