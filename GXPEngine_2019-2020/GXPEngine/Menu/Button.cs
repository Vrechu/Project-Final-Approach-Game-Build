using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GXPEngine;

class Button : AnimationSprite
{
    public static event Action<MyGame.ScreenState> OnButtonClicked;
    private MyGame.ScreenState _nextScreen;

    public Button(string fileName, float px, float py, MyGame.ScreenState nextScreen) : base(fileName, 2, 1)
    {
        SetXY(px, py);
        _nextScreen = nextScreen;
        SetOrigin(width / 2, height / 2);
    }

    private void Update()
    {
        ButtonClick();
    }

    /// <summary>
    /// Changes the sprite if the mouse is over it and invokes the OnButtonClick event.
    /// </summary>
    private void ButtonClick()
    {
        if (HitTestPoint(Input.mouseX, Input.mouseY))
        {
            SetFrame(0);
            if (Input.GetMouseButtonDown(0))
            {
                OnButtonClicked?.Invoke(_nextScreen);
            }
        }
        else
        {
            SetFrame(1);
        }
    }
}

