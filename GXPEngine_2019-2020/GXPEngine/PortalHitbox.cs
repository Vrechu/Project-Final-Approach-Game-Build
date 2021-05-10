using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class PortalHitbox : Sprite
{
    public static event Action<GameObject> OnPortalInHit;
    public static event Action<GameObject> OnPortalOutHit;

    private float _offset = 2f; // offset of the hitbox. slighty below the main class to not conflict with the moveUntilCollision

    /// <summary>
    /// hitbox for interacting with portals
    /// </summary>
    public PortalHitbox() : base("square.png")
    {
        SetOrigin(width / 2, height / 2);
        alpha = 0;
        SetScaleXY(1, 1 + _offset / 64);
        SetXY(1, _offset);
    }

    //sends out different events depending on which portal is hit
    private void OnCollision(GameObject other)
    {
        if (other is PortalIn)
        {
            OnPortalInHit?.Invoke(this);
        }
        else if (other is PortalOut)
        {
            OnPortalOutHit?.Invoke(this);
        }
    }
}

