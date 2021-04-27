using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Wall : Sprite
{
    public Wall(float px, float py, float xScale, float yScale) : base("square.png")
    {
        SetOrigin(width / 2, height / 2);
        x = px;
        y = py;

        SetScaleXY(xScale, yScale);

    }
}

