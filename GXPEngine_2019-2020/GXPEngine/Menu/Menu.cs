using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Sprite
{
    public Menu() : base("Main_Menu_placeholder_background.png")
    {
        AddChild(new Button("Play_Game.png", width / 2, height / 2, MyGame.ScreenState.COMIC1));
    }
}
