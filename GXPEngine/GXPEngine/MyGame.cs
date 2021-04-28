using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	public enum GravityDirection
    {
		UP, DOWN, LEFT, RIGHT
    }
	public static GravityDirection gravityDirection;

	public static event Action OnGravitySwitch;

	public static float GravityAccaleration = 0.2f; // Rate of acceleration
	public static Vec2 GravityVector = new Vec2();
	private Vec2 _upGravityVec = new Vec2(0, -1).Normalized();
	private Vec2 _downGravityVec = new Vec2(0, 1).Normalized();
	private Vec2 _leftGravityVec = new Vec2(-1, 0).Normalized();
	private Vec2 _rightGravityVec = new Vec2(1, 0).Normalized();

	private int _gravitySwitchCooldownTime = 1; //Cooldown timer in seconds
	private bool canSwitchGravity = true;
	private int oldTime = Time.time;

	public MyGame() : base(1920, 1080, false)		// Create a window that's 800x600 and NOT fullscreen
	{
		SetGravityDirection(GravityDirection.DOWN);

		AddChild(new Skull(width/2, height/2));

		AddChild(new Wall(width / 2, 0, 100, 0.5f));
		AddChild(new Wall(width / 2, height, 100, 0.5f));
		AddChild(new Wall(0, height/2, 0.5f, 100));
		AddChild(new Wall(width, height/2, 0.5f, 100));

		AddChild(new Wall(300, 0, 0.5f, 30));
		AddChild(new Wall(800, height, 0.5f, 30));

		AddChild(new Spike(width - 50, height / 2, 270));

		
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
		GravitySwitchCooldown();
	}

	static void Main()							// Main() is the first method that's called when the program is run
	{
		new MyGame().Start();					// Create a "MyGame" and start it
	}

	/// <summary>
	/// Key imputs for switching gravity direction
	/// </summary>
	private void GravityInputs()
    {
		if (canSwitchGravity)
		{
			if (Input.GetKey(Key.UP))
			{
				SetGravityDirection(GravityDirection.UP);
			}
			else if (Input.GetKey(Key.DOWN))
			{
				SetGravityDirection(GravityDirection.DOWN);
			}
			else if (Input.GetKey(Key.LEFT))
			{
				SetGravityDirection(GravityDirection.LEFT);
			}
			else if (Input.GetKey(Key.RIGHT))
			{
				SetGravityDirection(GravityDirection.RIGHT);
			}
			OnGravitySwitch?.Invoke();
		}
	}

	/// <summary>
	/// Sets the gravity vector in the specified direction
	/// </summary>
	/// <param name="direction">the direction that the gravity should switch to </param>
	public void SetGravityDirection(GravityDirection direction)
    {
		gravityDirection = direction;
		canSwitchGravity = false;
		switch (gravityDirection)
        {
			case GravityDirection.UP:
                {
					GravityVector = _upGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
                }
			case GravityDirection.DOWN:
				{
					GravityVector = _downGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
				}
			case GravityDirection.LEFT:
				{
					GravityVector = _leftGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
				}
			case GravityDirection.RIGHT:
				{
					GravityVector = _rightGravityVec;
					GravityVector = GravityVector * GravityAccaleration;
					break;
				}				
		}
    }

	/// <summary>
	/// Counts down time until you can switch gravity again
	/// </summary>
	private void GravitySwitchCooldown()
    {
		if (!canSwitchGravity)
        {
			oldTime -= Time.deltaTime;
        }

		if (oldTime < 0)
        {
			canSwitchGravity = true;
			oldTime = _gravitySwitchCooldownTime * 1000;
        }
    }
}