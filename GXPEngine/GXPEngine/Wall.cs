using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Wall : Sprite
{
    public Wall(float px, float py) : base("square.png")
    {
        SetOrigin(width / 2, height / 2);
        x = px;
        y = py;
    }
}

