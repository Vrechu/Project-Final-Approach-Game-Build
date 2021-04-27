using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	public  enum GravityDirection
    {
		UP, DOWN, LEFT, RIGHT
    }
	public GravityDirection gravityDirection;
	public static float GravityAccaleration = 0.1f;
	public static Vec2 GravityVector = new Vec2();
	private Vec2 UpGravityVec = new Vec2(0, -1).Normalized();
	private Vec2 DownGravityVec = new Vec2(0, 1).Normalized();
	private Vec2 LeftGravityVec = new Vec2(-1, 0).Normalized();
	private Vec2 RightGravityVec = new Vec2(1, 0).Normalized();

	public MyGame() : base(1920, 1080, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		AddChild(new Skull(width/2, height/2));

		AddChild(new Wall(width / 2, 0, 100, 0.5f));
		AddChild(new Wall(width / 2, height, 100, 0.5f));
		AddChild(new Wall(0, height/2, 0.5f, 100));
		AddChild(new Wall(width, height/2, 0.5f, 100));
	}

    void Update()
	{
		//----------------------------------------------------example-code----------------------------
		if (Input.GetKeyDown(Key.SPACE)) // When space is pressed...
		{
			new Sound("ping.wav").Play(); // ...play a sound
		}
		//------------------------------------------------end-of-example-code-------------------------
		GravityInputs();
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}

	private void GravityInputs()
    {
        if (Input.GetKey(Key.W))
		{
			SetGravityDirection(GravityDirection.UP);
		}
		else if (Input.GetKey(Key.S))
		{
			SetGravityDirection(GravityDirection.DOWN);
		}
		else if (Input.GetKey(Key.A))
		{
			SetGravityDirection(GravityDirection.LEFT);
		}
		else if (Input.GetKey(Key.D))
		{
			SetGravityDirection(GravityDirection.RIGHT);
		}		
	}

	public void SetGravityDirection(GravityDirection direction)
    {
		gravityDirection = direction;
        switch (gravityDirection)
        {
			case GravityDirection.UP:
                {
					GravityVector = UpGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
                }
			case GravityDirection.DOWN:
				{
					GravityVector = DownGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
				}
			case GravityDirection.LEFT:
				{
					GravityVector = LeftGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
				}
			case GravityDirection.RIGHT:
				{
					GravityVector = RightGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
				}
		}
    }
}