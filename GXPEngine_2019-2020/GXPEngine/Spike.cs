using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Spike : Sprite
{
    public Spike(float px, float py, float pRotation) : base("triangle.png")
    {
        SetOrigin(width / 2, height / 2);
        x = px;
        y = py;
        rotation = pRotation;
    }
}

