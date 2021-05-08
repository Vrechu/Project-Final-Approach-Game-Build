using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DamageHitbox : Sprite
{
    private float _offset = 2f;
    public DamageHitbox() : base("square.png")
    {
        SetOrigin(width / 2, height / 2);
        alpha = 0;
        SetScaleXY(1, 1 + _offset/64);
        SetXY(1, _offset);
    }
}

