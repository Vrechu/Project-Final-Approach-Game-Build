using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Spike : SolidObject
{
    /// <summary>
    /// object that damages the player on collision
    /// </summary>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    /// <param name="pRotation">object rotation in degrees</param>
    public Spike(string spriteImage, float px, float py, float pRotation ) : base( spriteImage, 1, 1,px, py)
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(0.95f, 1);
        SetXY(px, py);
        rotation = pRotation;
        AddChild(new DamageHitbox());
    }
}

