using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class PortalIn : Sprite
{
    public PortalIn(float px, float py) : base("portal_in.png")
    {
        SetXY(px, py);
        SetOrigin(width / 2, height / 2);
    }
}

