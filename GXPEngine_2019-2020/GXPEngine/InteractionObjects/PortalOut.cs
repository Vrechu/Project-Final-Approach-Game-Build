using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;

class PortalOut : Sprite
{
    public PortalOut(float px, float py) : base("portal_out.png")
    {
        SetXY(px, py);
        SetOrigin(width / 2, height / 2);
    }
}

