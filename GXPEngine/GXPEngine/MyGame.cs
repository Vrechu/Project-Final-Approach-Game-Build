using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	public  enum GravityDirection
    {
		UP, DOWN, LEFT, RIGHT
    }
	public static float GravityAccaleration = 0.1f;
	public static Vec2 GravityVector = new Vec2();
	private Vec2 UpGravityVec = new Vec2(0, -1).Normalized();
	private Vec2 DownGravityVec = new Vec2(0, 1).Normalized();
	private Vec2 LeftGravityVec = new Vec2(-1, 0).Normalized();
	private Vec2 RightGravityVec = new Vec2(1, 0).Normalized();

	public MyGame() : base(1920, 1080, false)		// Create a window that's 800x600 and NOT fullscreen
	{
        //----------------------------------------------------example-code----------------------------
        //create a canvas
        Canvas canvas = new Canvas(width, height);

        //add some content
        canvas.graphics.FillRectangle(new SolidBrush(Color.Red), new Rectangle(0, 0, 400, 300));
        canvas.graphics.FillRectangle(new SolidBrush(Color.Blue), new Rectangle(400, 0, 400, 300));
        canvas.graphics.FillRectangle(new SolidBrush(Color.Yellow), new Rectangle(0, 300, 400, 300));
        canvas.graphics.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(400, 300, 400, 300));

        //add canvas to display list
        AddChild(canvas);
		//------------------------------------------------end-of-example-code-------------------------

		AddChild(new Skull(width/2, height/2));
		AddChild(new Wall(width / 2, 0));
		AddChild(new Wall(width / 2, height));
		AddChild(new Wall(0, height/2));
		AddChild(new Wall(width, height/2));
	}

    void Update()
	{
		//----------------------------------------------------example-code----------------------------
		if (Input.GetKeyDown(Key.SPACE)) // When space is pressed...
		{
			new Sound("ping.wav").Play(); // ...play a sound
		}
		//------------------------------------------------end-of-example-code-------------------------
		SetGravityDirection();
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}

	private void SetGravityDirection()
    {
        if (Input.GetKey(Key.W))
		{
			GravityVector = UpGravityVec;
			GravityVector = GravityVector * GravityAccaleration;
		}
		if (Input.GetKey(Key.S))
		{
			GravityVector = DownGravityVec;
			GravityVector = GravityVector * GravityAccaleration;
		}
		if (Input.GetKey(Key.A))
		{
			GravityVector = LeftGravityVec;
			GravityVector = GravityVector * GravityAccaleration;
		}
		if (Input.GetKey(Key.D))
		{
			GravityVector = RightGravityVec;
			GravityVector = GravityVector * GravityAccaleration;
		}
		
	}
}