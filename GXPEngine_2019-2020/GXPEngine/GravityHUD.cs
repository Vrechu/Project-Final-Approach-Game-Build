using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class GravityHUD : AnimationSprite
{
    /// <summary>
    /// HUD display of current gravity direction
    /// </summary>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    public GravityHUD(float px, float py) : base("Compass.png", 2, 2)
    {
        SetXY(px, py);
        SetHUD(MyGame.GravityDirection.DOWN);
        MyGame.OnGravitySwitch += SetHUD;
    }

    protected override void OnDestroy()
    {
        MyGame.OnGravitySwitch -= SetHUD;
    }

    /// <summary>
    /// sets the gravity HUD indicator to the current gravity direction
    /// </summary>
    /// <param name="gravityDirection">current gravity direction</param>
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
                    SetFrame(2);
                    break;
                }
            case MyGame.GravityDirection.LEFT:
                {
                    SetFrame(1);
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

