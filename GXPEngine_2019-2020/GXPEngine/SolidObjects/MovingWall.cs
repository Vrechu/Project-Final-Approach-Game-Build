using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class MovingWall : SolidObject
{
    private float _speed = 0;
    private Vec2 _gravityVelocity;

    private Vec2 _startingPosition;
    public MovingWall(float px, float py) : base("colors.png", px, py, 1, 1)
    {
        Skull.OnDeath += Reset;
        _startingPosition.SetXY(x, y);
    }

    private void OnDestroy()
    {
        Skull.OnDeath -= Reset;
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
    /// moves object to starting position and sets velocity to 0
    /// </summary>
    void Reset()
    {
        x = _startingPosition.x;
        y = _startingPosition.y;
        _gravityVelocity.SetXY(0, 0);
    }
}


