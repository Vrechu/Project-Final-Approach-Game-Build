using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
    #region screen management
    public enum ScreenState
	{
		NULL, MENU, HELP, CREDITS, STORYCONCEPT, 
		TUTORIAL, LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5, LEVEL6
	}
	private ScreenState _screenState;

	private bool canSwitchScreen = true;

	public static event Action<ScreenState> OnScreenSwitch;
    #endregion

    #region gravity management
    public enum GravityDirection
	{
		UP, DOWN, LEFT, RIGHT
	}
	private GravityDirection gravityDirection;

	public static event Action<GravityDirection> OnGravitySwitch;

	public static float GravityAccaleration = 8f; // Rate of acceleration
	public static Vec2 GravityVector = new Vec2();
	private Vec2 _upGravityVec = new Vec2(0, -1).Normalized();
	private Vec2 _downGravityVec = new Vec2(0, 1).Normalized();
	private Vec2 _leftGravityVec = new Vec2(-1, 0).Normalized();
	private Vec2 _rightGravityVec = new Vec2(1, 0).Normalized();

	private float _gravitySwitchCooldownTime = 1f; //Cooldown timer in seconds
	private bool canSwitchGravity = true;
	private float oldTime =Time.time;
    #endregion

    public MyGame() : base(1920, 1080, false)
	{
		Button.OnButtonClicked += SwitchScreen;
		PlayerInteractionHitbox.OnDeath += ResetGravity;
		PlayerInteractionHitbox.OnDeath += ResetCurrentLevel;
		Level.OnLevelStart += ResetGravity;
		Level.OnLevelFinished += SwitchScreen;
		
		/*AddChild(new AudioPlayer());*/
		SwitchScreen(ScreenState.MENU);
		SetGravityDirection(GravityDirection.DOWN);
	}

	protected override void OnDestroy()
	{
		Button.OnButtonClicked -= SwitchScreen;
		PlayerInteractionHitbox.OnDeath -= ResetGravity;
		PlayerInteractionHitbox.OnDeath -= ResetCurrentLevel;
		Level.OnLevelStart -= ResetGravity;
		Level.OnLevelFinished -= SwitchScreen;
	}
	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}

	void Update()
	{
		canSwitchScreen = true;
		ResetImput();
		GravityInputs();
		GravitySwitchCooldown();		
	}


	/// <summary>
	/// selects the determined screenstate and switches the window to it
	/// </summary>
	/// <param name="screenState"> screen to switch to</param>
	public void SwitchScreen(ScreenState screenState)
	{
		if (canSwitchScreen)
		{
			if (_screenState != screenState)
			{
				OnScreenSwitch?.Invoke(screenState);
			}
			canSwitchScreen = false;
			_screenState = screenState;
			switch (screenState)
			{
				#region menu
				case ScreenState.MENU:
					{
						StartMenu();
						break;
					}
				case ScreenState.HELP:
					{
						StartDialogueWindow("Help_Menu_1.1.png", ScreenState.MENU);
						break;
					}
				case ScreenState.CREDITS:
					{
						StartDialogueWindow("Credits_Screen.png", ScreenState.MENU);
						break;
					}
				case ScreenState.STORYCONCEPT:
					{
						StartDialogueWindow("storyboard.png", ScreenState.MENU);
						break;
					}

				#endregion

				#region levels
				case ScreenState.TUTORIAL:
					{
						StartLevel("Tutorial_level.tmx", "Graveyard.png", ScreenState.LEVEL1);
						break;
					}
				case ScreenState.LEVEL1:
					{
						StartLevel("Level 1.tmx", "Graveyard.png", ScreenState.LEVEL2);
						break;
					}
				case ScreenState.LEVEL2:
					{
						StartLevel("Level 2.tmx", "Cave.png", ScreenState.LEVEL3);
						break;
					}
				case ScreenState.LEVEL3:
					{
						StartLevel("Level 3.tmx", "Cave.png", ScreenState.LEVEL4);
						break;
					}
				case ScreenState.LEVEL4:
					{
						StartLevel("Level 4.tmx", "Graveyard.png", ScreenState.LEVEL5);
						break;
					}
				case ScreenState.LEVEL5:
					{
						StartLevel("Level 5.tmx", "Hell.png", ScreenState.LEVEL6);
						break;
					}
				case ScreenState.LEVEL6:
					{
						StartLevel("Level 6.tmx", "Hell.png", ScreenState.CREDITS);
						break;
					}
				#endregion
			}
		}
	}

	/// <summary>
	/// closes any window and starts the menu
	/// </summary>
	private void StartMenu()
    {
		ClosePreviousScreen();
		LateAddChild(new Menu());
	}

	/// <summary>
	/// closes any window and starts the selected level
	/// </summary>
	/// <param name="levelName">file name of level</param>
	/// <param name="background">file name of level background image</param>
	/// <param name="nextScreen">next screen to be loaded</param>
	private void StartLevel(string levelName, string background, ScreenState nextScreen)
    {
        ClosePreviousScreen();
        LateAddChild(new Level(levelName,background, nextScreen));
    }

	/// <summary>
	/// closes any window and starts the selected dialoge window
	/// </summary>
	/// <param name="screenImage">file name of screen image</param>
	/// <param name="nextScreen">next screen to be loaded</param>
	private void StartDialogueWindow(string screenImage, ScreenState nextScreen)
    {
		ClosePreviousScreen();
		LateAddChild(new DialogueWindow(screenImage, nextScreen));
	}

	/// <summary>
	/// destroys the current level or menu screen
	/// </summary>
	private void ClosePreviousScreen()
	{
		foreach (Menu menu in game.FindObjectsOfType<Menu>())
		{
			menu.LateDestroy();
		}
		foreach (Level level in game.FindObjectsOfType<Level>())
		{
			level.LateDestroy();
		}
		foreach (DialogueWindow dialogueWindow in game.FindObjectsOfType<DialogueWindow>())
		{
			dialogueWindow.LateDestroy();
		}
	}

	/// <summary>
	/// resets level on input R
	/// </summary>
	private void ResetImput()
    {
        if (Input.GetKeyDown(Key.R))
        {
			ResetCurrentLevel();
        }
    }

	/// <summary>
	/// Stops and creates the current open screen
	/// </summary>
	private void ResetCurrentLevel()
    {
		SwitchScreen(_screenState);
    }

	/// <summary>
	/// Key imputs for switching gravity direction
	/// </summary>
	private void GravityInputs()
	{
		if (canSwitchGravity)
		{
			if (Input.GetKeyDown(Key.UP))
			{
				SetGravityDirection(GravityDirection.UP);
			}
			else if (Input.GetKeyDown(Key.DOWN))
			{
				SetGravityDirection(GravityDirection.DOWN);
			}
			else if (Input.GetKeyDown(Key.LEFT))
			{
				SetGravityDirection(GravityDirection.LEFT);
			}
			else if (Input.GetKeyDown(Key.RIGHT))
			{
				SetGravityDirection(GravityDirection.RIGHT);
			}
		}
	}

	/// <summary>
	/// Sets the gravity vector in the specified direction
	/// </summary>
	/// <param name="direction">the direction that the gravity should switch to </param>
	private void SetGravityDirection(GravityDirection direction)
	{
		gravityDirection = direction;
		canSwitchGravity = false;
		switch (gravityDirection)
		{
			case GravityDirection.UP:
				{
					GravityVector = _upGravityVec;
					GravityVector *= GravityAccaleration;
					break;
				}
			case GravityDirection.DOWN:
				{
					GravityVector = _downGravityVec;
					GravityVector *= GravityAccaleration;
					break;
				}
			case GravityDirection.LEFT:
				{
					GravityVector = _leftGravityVec;
					GravityVector *= GravityAccaleration;
					break;
				}
			case GravityDirection.RIGHT:
				{
					GravityVector = _rightGravityVec;
					GravityVector *= GravityAccaleration;
					break;
				}
		}
		OnGravitySwitch?.Invoke(gravityDirection);
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
	private void ResetGravity()
	{
		SetGravityDirection(GravityDirection.DOWN);
		canSwitchGravity = false;
	}

}