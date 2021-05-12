using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Sprite
{
    /// <summary>
    /// menu class
    /// </summary>
    public Menu() : base("MenuScreen.png")
    {
        AddChild(new Button("Play_Game.png", width / 2,450, MyGame.ScreenState.TUTORIAL));
        AddChild(new Button("Help.png", width / 2, 600, MyGame.ScreenState.HELP1));
        AddChild(new Button("Story.png", width / 2, 720, MyGame.ScreenState.STORYCONCEPT));
        AddChild(new Button("Credits.png", width / 2, 840, MyGame.ScreenState.CREDITS));
    }
}
