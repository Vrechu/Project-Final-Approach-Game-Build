using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class GravityHUD : AnimationSprite
{
    public GravityHUD(float px, float py) : base("placeholder_hud.png", 2, 2)
    {
        SetXY(px, py);
        SetHUD(MyGame.GravityDirection.DOWN);
        MyGame.OnGravitySwitch += SetHUD;
    }

    protected override void OnDestroy()
    {
        MyGame.OnGravitySwitch -= SetHUD;
    }

    private void SetHUD(MyGame.GravityDirection gravityDirection)
    {
        switch (gravityDirection)
        {
            case MyGame.GravityDirection.UP:
                {
                    SetFrame(0);
                    break;
                }
            case MyGame.GravityDirection.DOWN:
                {
                    SetFrame(1);
                    break;
                }
            case MyGame.GravityDirection.LEFT:
                {
                    SetFrame(2);
                    break;
                }
            case MyGame.GravityDirection.RIGHT:
                {
                    SetFrame(3);
                    break;
                }
        }
    }
}

