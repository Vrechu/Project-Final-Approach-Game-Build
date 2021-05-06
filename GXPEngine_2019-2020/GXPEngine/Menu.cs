using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Sprite
{
    public Menu() : base("square.png")
    {
        AddChild(new Button("Play_Game.png", width / 2, height / 2, MyGame.ScreenState.COMIC1));
    }
}
