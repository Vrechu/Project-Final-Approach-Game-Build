using System;									// System contains a lot of default C# libraries 
using System.Drawing;                           // System.Drawing contains a library used for canvas drawing below
using GXPEngine;                                // GXPEngine contains the engine

public class MyGame : Game
{
	public enum ScreenState
	{
		MENU, LEVEL1, LEVEL2, LEVEL3, LEVEL4, LEVEL5, LEVEL6, COMIC1, COMIC2, COMIC3, COMIC4
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

	private float _gravitySwitchCooldownTime = 2f; //Cooldown timer in seconds
	private bool canSwitchGravity = true;
	private float oldTime =Time.time;

	public MyGame() : base(1920, 1080, false)
	{
		Button.OnButtonClicked += SwitchScreen;
		InteractionHitbox.OnDeath += ResetGravity;
		InteractionHitbox.OnDeath += ResetCurrentLevel;
		Level.OnLevelStart += ResetGravity;
		Level.OnLevelFinished += SwitchScreen;

		SwitchScreen(ScreenState.MENU);
		SetGravityDirection(GravityDirection.DOWN);
	}

	protected override void OnDestroy()
	{
		Button.OnButtonClicked -= SwitchScreen;
		InteractionHitbox.OnDeath -= ResetGravity;
		InteractionHitbox.OnDeath -= ResetCurrentLevel;
		Level.OnLevelStart -= ResetGravity;
		Level.OnLevelFinished -= SwitchScreen;
	}

	void Update()
	{
		GravityInputs();
		GravitySwitchCooldown();		
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
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

            #region Levels
            case ScreenState.LEVEL1:
				{
					StartLevel("Tryout3.tmx", ScreenState.LEVEL2);
					break;
				}
			case ScreenState.LEVEL2:
				{
					StartLevel("Tryout3.tmx", ScreenState.COMIC2);
					break;
				}
			case ScreenState.LEVEL3:
				{
					StartLevel("Tryout3.tmx", ScreenState.LEVEL4);
					break;
				}
			case ScreenState.LEVEL4:
				{
					StartLevel("Tryout3.tmx", ScreenState.COMIC3);
					break;
				}
			case ScreenState.LEVEL5:
				{
					StartLevel("Tryout3.tmx", ScreenState.LEVEL6);
					break;
				}
			case ScreenState.LEVEL6:
				{
					StartLevel("Tryout3.tmx", ScreenState.COMIC4);
					break;
				}
            #endregion

            #region comic panels
            case ScreenState.COMIC1:
				{
					StartDialogueWindow("colors.png", ScreenState.LEVEL1);
					break;
				}
			case ScreenState.COMIC2:
				{
					StartDialogueWindow("colors.png", ScreenState.LEVEL3);
					break;
				}
			case ScreenState.COMIC3:
				{
					StartDialogueWindow("colors.png", ScreenState.LEVEL5);
					break;
				}
			case ScreenState.COMIC4:
				{
					StartDialogueWindow("colors.png", ScreenState.MENU);
					break;
				}
                #endregion
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
	/// <param name="levelName"> file name of level</param>
	private void StartLevel(string levelName, ScreenState nextScreen)
    {
        ClosePreviousScreen();
        LateAddChild(new Level(levelName, nextScreen));
    }

	private void StartDialogueWindow(string screenimage, ScreenState nextScreen)
    {
		ClosePreviousScreen();
		LateAddChild(new DialogueWindow(screenimage, nextScreen));
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
	/// Stops and creates the current open screen
	/// </summary>
	private void ResetCurrentLevel()
    {
		SwitchScreen(_screenState);
    }
}