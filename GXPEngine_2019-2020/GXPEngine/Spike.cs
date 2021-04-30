using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Spike : SolidObject
{
    public Spike(float px, float py, float pRotation) : base("triangle.png", px, py)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(px, py);
        rotation = pRotation;
    }
}

