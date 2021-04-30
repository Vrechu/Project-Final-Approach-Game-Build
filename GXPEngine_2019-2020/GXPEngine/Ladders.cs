using System;

using GXPEngine;

public class LadderTop : Sprite
{

    public LadderTop() : base("ladder_top.png")
    {
        SetOrigin(0, 4);

/*        this.x = x;
        this.y = y;*/

        scaleX = 1f;
        scaleY = 1f;

        name = "ladderTop";
    }
}

public class LadderBottom : Sprite
{
    public LadderBottom() : base("ladder.png")
    {
        SetOrigin(0, 0);
/*
        this.x = x;
        this.y = y;*/

        scaleX = 1f;
        scaleY = 1f;

        name = "ladderBottom";
    }
}


