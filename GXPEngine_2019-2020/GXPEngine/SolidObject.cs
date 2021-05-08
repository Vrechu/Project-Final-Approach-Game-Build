using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

abstract class SolidObject : AnimationSprite
{
    /// <summary>
    /// object that can collide with other objects
    /// </summary>
    /// <param name="spriteImage">sprite image file name</param>
    /// <param name="cols">animation sprite column amount</param>
    /// <param name="rows">animation sprite row amount</param>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    public SolidObject(string spriteImage, int cols, int rows, float px, float py) : base(spriteImage, cols, rows)
    {
        SetOrigin(width / 2, height / 2);
        SetXY(px, py);
    }
}

