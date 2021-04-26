using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Skull : Sprite
{
    private float _speed = 0;
    private Vec2 _velocity;
    public Skull(float px, float py) : base("circle.png")
    {
        SetOrigin(width / 2, height / 2);
        x = px;
        y = py;
    }

    private void Update()
    {
        UpdateScreenPosition();
    }

    private void UpdateScreenPosition()
    {
        Vec2 _oldPosition = new Vec2(x, y);
        _velocity += MyGame.GravityVector;
        MoveUntilCollision(_velocity.x, _velocity.y, game.FindObjectsOfType<Wall>());
        _speed = new Vec2(_oldPosition.x - x, _oldPosition.y - y).Length();
        _velocity = _velocity.Normalized() * _speed;
    }

    void OnCollision(GameObject other)
    {
        if (other is Wall)
        {
            _velocity = new Vec2(0, 0);
        }
    }
}