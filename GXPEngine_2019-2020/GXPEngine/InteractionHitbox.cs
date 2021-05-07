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
    private float _hitboxOffset = 5;
    public InteractionHitbox() : base("square.png")
    {
        SetOrigin(width / 2, height / 2);
        alpha = 0;
        y = _hitboxOffset;
    }

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
    }
}

