﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class Menu : Sprite
{
    /// <summary>
    /// menu class
    /// </summary>
    public Menu() : base("Graveyard.png")
    {
        AddChild(new Button("Play_Game.png", width / 2, height / 2, MyGame.ScreenState.TUTORIAL));
        AddChild(new Button("Help.png", width / 2, height / 3 * 2, MyGame.ScreenState.INTRO));
        AddChild(new Button("Credits.png", width / 2, height / 4 * 3, MyGame.ScreenState.CREDITS));
    }
}
