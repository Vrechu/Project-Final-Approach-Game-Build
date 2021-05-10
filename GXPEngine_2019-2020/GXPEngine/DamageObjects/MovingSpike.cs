using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class MovingSpike : SolidObject
{
    private float _speed = 0;
    private Vec2 _gravityVelocity;

    private float _teleportCooldownTime = 1; // cooldown time for using a portal in seconds
    private float oldTime = -1;
    private bool canTeleport = true;

    private PortalHitbox _portalHitbox;

    private Sprite _imageSprite;

    /// <summary>
    /// spike that moves with gravity
    /// </summary>
    /// <param name="spriteImage">sprite image filename</param>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    public MovingSpike(string spriteImage, float px, float py) : base("square.png",1, 1, px, py)
    {
        SetScaleXY(0.85f, 0.90f);
        MyGame.OnGravitySwitch += RotateSpike;
        AddChild(new DamageHitbox());
        AddChild(_portalHitbox = new PortalHitbox());
        PortalHitbox.OnPortalInHit += MoveToPortalOut;
        PortalHitbox.OnPortalOutHit += MoveToPortalIn;

        alpha = 0;
        AddChild(_imageSprite = new Sprite("triangle.png"));
        _imageSprite.SetOrigin(_imageSprite.width / 2, _imageSprite.height / 2);
    }

    protected override void OnDestroy()
    {
        MyGame.OnGravitySwitch -= RotateSpike;
        PortalHitbox.OnPortalInHit -= MoveToPortalOut;
        PortalHitbox.OnPortalOutHit -= MoveToPortalIn;
    }

    void Update()
    {
        MoveWall();
    }

    /// <summary>
    /// Moves the object into the gravity direction
    /// </summary>
    private void MoveWall()
    {
        Vec2 _oldPosition = new Vec2(x, y);
        _gravityVelocity += MyGame.GravityVector;
        MoveUntilCollision(_gravityVelocity.x, _gravityVelocity.y, game.FindObjectsOfType<SolidObject>());
        _speed = new Vec2(_oldPosition.x - x, _oldPosition.y - y).Length();
        _gravityVelocity = _gravityVelocity.Normalized() * _speed;
    }

    /// <summary>
    /// Rotates the spike depending on the gravity direction
    /// </summary>
    /// <param name="gravityDirection">direction gravity-affected objects move in</param>
    private void RotateSpike(MyGame.GravityDirection gravityDirection)
    {
        switch (gravityDirection)
        {
            case MyGame.GravityDirection.UP:
                {
                    rotation = 180;
                    _imageSprite.rotation = -180;
                    break;
                }
            case MyGame.GravityDirection.DOWN:
                {
                    rotation = 0;
                    _imageSprite.rotation = 0;
                    break;
                }
            case MyGame.GravityDirection.LEFT:
                {
                    rotation = 90;
                    _imageSprite.rotation = -90;
                    break;
                }
            case MyGame.GravityDirection.RIGHT:
                {
                    rotation = 270;
                    _imageSprite.rotation = -270;
                    break;
                }
        }
    }

    /// <summary>
    /// moves the spike to the portal out
    /// </summary>
    private void MoveToPortalOut(GameObject hitbox)
    {
        if (hitbox == _portalHitbox && canTeleport)
        {
            canTeleport = false;
            SetXY(game.FindObjectOfType<PortalOut>().x, game.FindObjectOfType<PortalOut>().y);
        }
    }

    /// <summary>
    /// moves the spike to portal in
    /// </summary>
    private void MoveToPortalIn(GameObject hitbox)
    {
        if (hitbox == _portalHitbox && canTeleport)
        {
            canTeleport = false;
            SetXY(game.FindObjectOfType<PortalIn>().x, game.FindObjectOfType<PortalIn>().y);
        }
    }

    /// <summary>
    /// counts down the portal timer
    /// </summary>
    private void TeleportCooldown()
    {
        if (!canTeleport)
        {
            oldTime -= Time.deltaTime;
        }

        if (oldTime < 0)
        {
            canTeleport = true;
            oldTime = _teleportCooldownTime * 1000;
        }
    }
}


