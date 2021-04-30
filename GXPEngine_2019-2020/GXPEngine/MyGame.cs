using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;								// GXPEngine contains the engine

public class MyGame : Game
{
	public  enum ScreenState
    {
		MENU, LEVEL1
    }
	public ScreenState _screenState;


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

	private int _gravitySwitchCooldownTime = 3; //Cooldown timer in seconds
	private bool canSwitchGravity = true;
	private int oldTime = Time.time;

	public MyGame() : base(1920, 1080, false)
	{
		Button.OnButtonClicked += SwitchScreen;
		SwitchScreen(ScreenState.MENU);
		Skull.OnDeath += Reset;

		SetGravityDirection(GravityDirection.DOWN);
	}

	private void OnDestroy()
    {
		Skull.OnDeath -= Reset;
	}

    void Update()
	{
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

	/// <summary>
	/// returns the gravity to down and resets the cooldown
	/// </summary>
	private void Reset()
    {
		SetGravityDirection(GravityDirection.DOWN);
		canSwitchGravity = false;
    }

	/// <summary>
	/// selects the determined screenstate and switches the window to it
	/// </summary>
	/// <param name="screenState"> screen to switch to</param>
	public void SwitchScreen(ScreenState screenState)
    {
		_screenState = screenState;
        switch (screenState)
        {
			case ScreenState.MENU:
                {
					StartMenu();
					break;
                }
			case ScreenState.LEVEL1:
				{
					StartLevel("Tryout2.tmx");
					break;
				}
		}
    } 

	/// <summary>
	/// closes any window and starts the menu
	/// </summary>
	public void StartMenu()
    {
		ClosePreviousScreen();
		AddChild(new Menu(width/2, height/2));
	}

	/// <summary>
	/// closes any window and starts the selected level
	/// </summary>
	/// <param name="levelName"> file name of level</param>
	public void StartLevel(string levelName)
    {
        ClosePreviousScreen();
        /*AddChild(new Skull(width / 2, height / 2));

        AddChild(new StationaryWall("square.png", width / 2, 0, 100, 0.5f));
        AddChild(new StationaryWall("square.png", width / 2, height, 100, 0.5f));
        AddChild(new StationaryWall("square.png", 0, height / 2, 0.5f, 100));
        AddChild(new StationaryWall("square.png", width, height / 2, 0.5f, 100));

        AddChild(new StationaryWall("square.png", 300, 0, 0.5f, 30));
        AddChild(new StationaryWall("square.png", 800, height, 0.5f, 30));

        AddChild(new Spike(width - 50, height / 2, 270));

        AddChild(new MovingWall(400, height / 2));

        AddChild(new Legs(width - 100, height - 100));*/
        AddChild(new Level(levelName));

    }

    /// <summary>
    /// destroys the current level or menu screen
    /// </summary>
    private void ClosePreviousScreen()
    {
		foreach (Menu menu in FindObjectsOfType<Menu>())
		{
			menu.LateDestroy();
		}
		foreach (Level level in FindObjectsOfType<Level>())
		{
			level.LateDestroy();
		}
	}
		
}