using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

abstract class SolidObject : AnimationSprite
{
    public SolidObject(string spriteImage, int cols, int rows, float px, float py) : base(spriteImage, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(px, py);
    }
}

