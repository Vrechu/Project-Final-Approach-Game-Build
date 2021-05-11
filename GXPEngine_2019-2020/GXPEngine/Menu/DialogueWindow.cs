using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class DialogueWindow : Sprite
{
    /// <summary>
    /// dialogue screen class
    /// </summary>
    /// <param name="screenImage">screen image file name</param>
    /// <param name="nextScreen">next screen to be loaded</param>
    public DialogueWindow(string screenImage, MyGame.ScreenState nextScreen) : base(screenImage)
    {
        AddChild(new Button("Next.png", width / 2, 900, nextScreen));
    }
}
