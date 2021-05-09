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
    public PlayerAnimations() : base("barry.png", 7, 1)
    {
        SetOrigin(width / 2, height / 2);
    }

    private void Update()
    {
        Animate();
    }
}

