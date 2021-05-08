using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DialogueWindow : Sprite
{
    public DialogueWindow(string screenImage, MyGame.ScreenState nextScreen) : base(screenImage)
    {
        AddChild(new Button("Play_Game.png", width / 2, height / 2, nextScreen));
    }
}
