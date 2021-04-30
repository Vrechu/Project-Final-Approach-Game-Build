using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

abstract class SolidObject : Sprite
{
    public SolidObject(string spriteImage, float px, float py) : base(spriteImage)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(px, py);
    }
}

