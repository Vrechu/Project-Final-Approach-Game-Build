using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
class PlayerAnimations : AnimationSprite
{
    /// <summary>
    /// player animation sprite
    /// </summary>
    public PlayerAnimations() : base("Player_Spritesheet.png", 11, 1)
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(1f/3f, 1f/3f);
        SetXY(0, -12);
    }

    private void Update()
    {
        Animate();
    }
}

