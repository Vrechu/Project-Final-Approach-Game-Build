using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Sprite
{
    public Menu(float px, float py) : base("square.png")
    {
        SetXY(px, py);
        AddChild(new Button("Play_Game.png", width / 2, height / 2, MyGame.ScreenState.LEVEL1));
    }
}
