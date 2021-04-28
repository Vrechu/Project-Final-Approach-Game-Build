using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Skull : Sprite
{
    private float _speed = 0;
    private Vec2 _gravityVelocity;
    private Vec2 _WalkingDirection = new Vec2(0, 1);
    private float _walkingSpeed = 5;
    private bool isGrounded = false;
    private bool canWalk = true;

    public Skull(float px, float py) : base("triangle.png")
    {
        SetOrigin(width / 2, height / 2);
        x = px;
        y = py;
        MyGame.OnGravitySwitch += RotateSkull;
    }

    private void Update()
    {
        MoveSkull();
        Walk();
    }

    private void OnDestroy()
    {
        MyGame.OnGravitySwitch -= RotateSkull;
    }

    /// <summary>
    /// Moves the skull into the gravity direction
    /// </summary>
    private void MoveSkull()
    {
        Vec2 _oldPosition = new Vec2(x, y);
        _gravityVelocity += MyGame.GravityVector;
        MoveUntilCollision(_gravityVelocity.x, _gravityVelocity.y, game.FindObjectsOfType<Wall>());
        _speed = new Vec2(_oldPosition.x - x, _oldPosition.y - y).Length();
        _gravityVelocity = _gravityVelocity.Normalized() * _speed;
        if( _speed == 0)
        {
            isGrounded = true;
        }
        else if( _speed > 0)
        {
            isGrounded = false;
        }
    }

    private void OnCollision(GameObject other)
    {
        if (other is Spike)
        {
            this.LateDestroy();
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
                MoveUntilCollision(_WalkingDirection.x*-1, _WalkingDirection.y*-1, game.FindObjectsOfType<Wall>());
            }
            else if (Input.GetKey(Key.D))
            {
                MoveUntilCollision(_WalkingDirection.x, _WalkingDirection.y, game.FindObjectsOfType<Wall>());
            }
        }
    }

    /// <summary>
    /// Rotates the skull depending on the gravity direction
    /// </summary>
    private void RotateSkull(){
        switch (MyGame.gravityDirection)
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
                    break;/**/
                }
        }
        _WalkingDirection = _WalkingDirection.Normalized() * _walkingSpeed;
    }
}