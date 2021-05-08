using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class InteractionHitbox : Sprite
{    
    public static event Action OnDeath;
    public static event Action OnLegsPickup;
    public static event Action OnGoalReached;
    public static event Action OnPortalInHit;
    public static event Action OnPortalOutHit;

    private float _hitboxOffset = 5; // offset of the hitbox. slighty below the main class to not conflict with the moveuntilcollision
    public InteractionHitbox() : base("square.png")
    {
        SetOrigin(width / 2, height / 2);
        alpha = 0;
        y = _hitboxOffset;
    }

    //sends out different events depending on which interaction object the hitbox collides with 
    private void OnCollision(GameObject other)
    {
        if (other is DamageHitbox)
        {
            OnDeath?.Invoke();
        }
        else if (other is Legs)
        {
            OnLegsPickup?.Invoke();
            other.LateDestroy();
        }
        else if (other is Goal)
        {
            OnGoalReached?.Invoke();
        }
        else if (other is PortalIn)
        {
            OnPortalInHit?.Invoke();
        }
        else if (other is PortalOut)
        {
            OnPortalOutHit?.Invoke();
        }
    }
}

