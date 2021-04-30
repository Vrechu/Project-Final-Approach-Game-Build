using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class InteractionHitbox : Sprite
{
    private Skull _skull;

    public static event Action OnDeath;
    public static event Action OnLegsPickup;
    public static event Action OnGoalReached;
    public InteractionHitbox(Skull skull) : base("square.png")
    {
        _skull = skull;
        SetOrigin(width / 2, height / 2);
        SetScaleXY(1, 1.1f);
        alpha = 0;
    }

    private void OnCollision(GameObject other)
    {
        if (other is Spike)
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
    }
}

