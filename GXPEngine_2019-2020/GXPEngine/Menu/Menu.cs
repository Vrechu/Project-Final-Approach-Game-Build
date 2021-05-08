using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Sprite
{
    public Menu() : base("placeholder_menu.png")
    {
        AddChild(new Button("Play_Game.png", width / 2, height / 2, MyGame.ScreenState.COMIC1));
        AddChild(new Button("Play_Game.png", width / 2, height / 3 * 2, MyGame.ScreenState.INTRO));
        AddChild(new Button("Play_Game.png", width / 2, height / 4 * 3, MyGame.ScreenState.CREDITS));
    }
}
