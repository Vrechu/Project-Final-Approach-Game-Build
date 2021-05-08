﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class MovingSpike : SolidObject
{
    private float _speed = 0;
    private Vec2 _gravityVelocity;

    /// <summary>
    /// spike that moves with gravity
    /// </summary>
    /// <param name="SpriteImage">sprite image filename</param>
    /// <param name="px">object x position</param>
    /// <param name="py">object y position</param>
    public MovingSpike(string SpriteImage, float px, float py) : base(SpriteImage,1, 1, px, py)
    {
        SetScaleXY(0.85f, 0.90f);
        MyGame.OnGravitySwitch += RotateSpike;
        AddChild(new DamageHitbox());
    }

    protected override void OnDestroy()
    {
        MyGame.OnGravitySwitch -= RotateSpike;
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
                    break;
                }
            case MyGame.GravityDirection.DOWN:
                {
                    rotation = 0;
                    break;
                }
            case MyGame.GravityDirection.LEFT:
                {
                    rotation = 90;
                    break;
                }
            case MyGame.GravityDirection.RIGHT:
                {
                    rotation = 270;
                    break;
                }
        }
    }
}


