using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Skull : SolidObject
{
    public static event Action OnDeath;
    private float _speed = 0;
    private Vec2 _gravityVelocity;
    private Vec2 _WalkingDirection = new Vec2(0, 1);
    private float _walkingSpeed = 5;
    private bool isGrounded = false;
    private bool canWalk = false;

    private Vec2 _startingPosition;
    private float _startingRotation;

    public Skull(float px, float py) : base("triangle.png", px, py, 1, 1)
    {
        SetOrigin(width / 2, height / 2);
        x = px;
        y = py;
        MyGame.OnGravitySwitch += RotateSkull;
        _startingPosition.SetXY(x, y);
        _startingRotation = rotation;
    }

    private void Update()
    {
        MoveSkull();
        CheckIfGounded();
        Walk();
    }

    private void OnDestroy()
    {
        MyGame.OnGravitySwitch -= RotateSkull;
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

    private void OnCollision(GameObject other)
    {
        if (other is Spike)
        {
            Reset();
        }

        else if (other is Legs)
        {
            canWalk = true;
            other.LateDestroy();
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
                MoveUntilCollision(_WalkingDirection.x*-1, _WalkingDirection.y*-1, game.FindObjectsOfType<SolidObject>());
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

    /// <summary>
    /// moves object to starting position and sets velocity to 0
    /// </summary>
    void Reset()
    {
        x = _startingPosition.x;
        y = _startingPosition.y;
        _gravityVelocity.SetXY(0, 0);
        OnDeath?.Invoke();
        rotation = _startingRotation;
    }
}