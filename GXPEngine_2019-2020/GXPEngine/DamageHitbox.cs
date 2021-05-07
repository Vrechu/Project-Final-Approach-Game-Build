using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DamageHitbox : Sprite
{
    public DamageHitbox() : base("square.png")
    {
        SetOrigin(width / 2, height / 2);
        alpha = 0;
        SetScaleXY(1, 1.2f);
    }
}

