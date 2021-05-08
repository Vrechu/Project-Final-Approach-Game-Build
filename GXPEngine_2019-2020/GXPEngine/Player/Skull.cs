﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Skull : SolidObject
{
    private float _speed = 0;
    private Vec2 _gravityVelocity;
    private Vec2 _WalkingDirection = new Vec2(0, 1);
    private float _walkingSpeed = 5; //speed at which the player walks along surfaces
    private bool isGrounded = false;
    private bool canWalk = false;

    private float _teleportCooldownTime = 1; // cooldown time for using a portal in seconds
    private float oldTime = -1;
    private bool canTeleport = true;

    /// <summary>
    /// player character
    /// </summary>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    public Skull(float px, float py) : base("skull.png",1, 1, px, py)
    {
        SetXY(px, py);
        SetScaleXY(0.95f, 1f);
        AddChild(new InteractionHitbox());

        MyGame.OnGravitySwitch += RotateSkull;
        InteractionHitbox.OnLegsPickup += PickupLegs;
        InteractionHitbox.OnPortalInHit += MoveToPortalOut;
        InteractionHitbox.OnPortalOutHit += MoveToPortalIn;
    }

    protected override void OnDestroy()
    {
        MyGame.OnGravitySwitch -= RotateSkull;
        InteractionHitbox.OnLegsPickup -= PickupLegs;
        InteractionHitbox.OnPortalInHit -= MoveToPortalOut;
        InteractionHitbox.OnPortalOutHit -= MoveToPortalIn;
    }

    private void Update()
    {
        MoveSkull();
        CheckIfGounded();
        Walk();
        TeleportCooldown();
    }


    /// <summary>
    /// Moves the skull into the gravity direction and detects wether the skull is grounded
    /// </summary>
    private void MoveSkull()
    {
        Vec2 _oldPosition = new Vec2(x, y);
        _gravityVelocity += MyGame.GravityVector;
        MoveUntilCollision(_gravityVelocity.x, _gravityVelocity.y, game.FindObjectsOfType<SolidObject>());
        _speed = new Vec2(_oldPosition.x - x, _oldPosition.y - y).Length();
        _gravityVelocity = _gravityVelocity.Normalized() * _speed;
    }

    /// <summary>
    /// checks if the skull is on a walkable surface
    /// </summary>
    private void CheckIfGounded()
    {
        if (_speed == 0)
        {
            isGrounded = true;
        }
        else if (_speed > 0)
        {
            isGrounded = false;
        }
    }

    /// <summary>
    /// Moves the player alongside surfaces
    /// </summary>
    private void Walk()
    {
        if (isGrounded && canWalk)
        {
            if (Input.GetKey(Key.A))
            {
                MoveUntilCollision(_WalkingDirection.x * -1, _WalkingDirection.y * -1, game.FindObjectsOfType<SolidObject>());
            }
            else if (Input.GetKey(Key.D))
            {
                MoveUntilCollision(_WalkingDirection.x, _WalkingDirection.y, game.FindObjectsOfType<SolidObject>());
            }
        }
    }

    /// <summary>
    /// Rotates the skull depending on the gravity direction
    /// </summary>
    /// <param name="gravityDirection">direction gravity-affected objects move in</param>
    private void RotateSkull(MyGame.GravityDirection gravityDirection)
    {
        switch (gravityDirection)
        {
            case MyGame.GravityDirection.UP:
                {
                    rotation = 180;
                    _WalkingDirection.SetAngleDegrees(180);
                    break;
                }
            case MyGame.GravityDirection.DOWN:
                {
                    rotation = 0;
                    _WalkingDirection.SetAngleDegrees(0);
                    break;
                }
            case MyGame.GravityDirection.LEFT:
                {
                    rotation = 90;
                    _WalkingDirection.SetAngleDegrees(90);
                    break;
                }
            case MyGame.GravityDirection.RIGHT:
                {
                    rotation = 270;
                    _WalkingDirection.SetAngleDegrees(270);
                    break;
                }
        }
        _WalkingDirection = _WalkingDirection.Normalized() * _walkingSpeed;
    }

    /// <summary>
    /// sets the canWalk bool to true
    /// </summary>
    private void PickupLegs()
    {
        canWalk = true;
    }

    /// <summary>
    /// moves the player to the portal out
    /// </summary>
    private void MoveToPortalOut()
    {
        if (canTeleport)
        {
            canTeleport = false;
            SetXY(game.FindObjectOfType<PortalOut>().x, game.FindObjectOfType<PortalOut>().y);
        }
    }

    /// <summary>
    /// moves the player to portal in
    /// </summary>
    private void MoveToPortalIn()
    {
        if (canTeleport)
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