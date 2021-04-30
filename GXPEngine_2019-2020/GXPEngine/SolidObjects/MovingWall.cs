﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class MovingWall : SolidObject
{
    private float _speed = 0;
    private Vec2 _gravityVelocity;
    public MovingWall(string SpriteImage, float px, float py) : base(SpriteImage , px, py)
    {
        SetScaleXY(0.95f, 1);
        MyGame.OnGravitySwitch += RotateWall;
    }

    private void OnDestroy()
    {
        MyGame.OnGravitySwitch -= RotateWall;
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
    /// Rotates the skull depending on the gravity direction
    /// </summary>
    private void RotateWall()
    {
        switch (MyGame.gravityDirection)
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


