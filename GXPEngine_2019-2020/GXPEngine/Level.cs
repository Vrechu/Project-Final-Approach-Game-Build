using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

class Level : GameObject
{
    public static event Action<MyGame.ScreenState> OnLevelFinished;
    public static event Action OnLevelStart;

    private MyGame.ScreenState _nextScreen;
    private Map levelData;
    private float _sideLength = 64;
    private float _widthOffset = 320;
    private float _heightOffset = 130;

    /// <summary>
    /// level creator
    /// </summary>
    /// <param name="levelFileName"> name of the tiled level file</param>
    /// <param name="backgroundFileName">file name of the background image</param>
    /// <param name="nextScreen">next screen to be loaded</param>
    public Level(string levelFileName,string backgroundFileName, MyGame.ScreenState nextScreen) : base()
    {
        PlayerInteractionHitbox.OnGoalReached += NextScreen;
        _nextScreen = nextScreen;
        levelData = MapParser.ReadMap(levelFileName);
        _heightOffset += _sideLength / 2;
        _widthOffset += _sideLength / 2;
        OnLevelStart?.Invoke();
        AddChild(new Sprite(backgroundFileName));
        AddChild(new Sprite("Level_Overlay.png"));
        SpawnTiles(levelData);
        AddChild(new GravityHUD(1550, 785));
    }

    protected override void OnDestroy()
    {
        PlayerInteractionHitbox.OnGoalReached -= NextScreen;
    }

    /// <summary>
    /// invokes the OnLevelFinished event
    /// </summary>
    private void NextScreen()
    {
        OnLevelFinished?.Invoke(_nextScreen);
    }

    /// <summary>
    /// Places tiles in the locations specified by the Tiled map file
    /// </summary>
    /// <param name="leveldata">tiled map data</param>
    private void SpawnTiles(Map leveldata)
    {
        if (leveldata.Layers == null    //nullcheck
            || leveldata.Layers.Length == 0)
        {
            return;
        }
        Layer mainLayer = leveldata.Layers[0];
        short[,] tileNumbers = mainLayer.GetTileArray(); //get arraylist from tiled file

        for (int row = 0; row < mainLayer.Height; row++)
        {
            for (int column = 0; column < mainLayer.Width; column++)
            {
                int tileNumber = tileNumbers[column, row]; //assign row and column numbers

                if (tileNumber > 0 && tileNumber < 240) PlaceStationaryWall(column, row, tileNumber); //place stationary walls
                else switch (tileNumber)
                    {
                        #region  spikes
                        #region spike 1
                        case 245:
                            {
                                PlaceSpike(column, row, "Spike1.png", 0);
                                break;
                            }
                        case 275:
                            {
                                PlaceSpike(column, row, "Spike1.png", 180);
                                break;
                            }
                        case 246:
                            {
                                PlaceSpike(column, row, "Spike1.png", 90);
                                break;
                            }
                        case 276:
                            {
                                PlaceSpike(column, row, "Spike1.png",  270);
                                break;
                            }
                        #endregion

                        #region spike 2
                        case 305:
                            {
                                PlaceSpike(column, row, "Spike2.png", 0);
                                break;
                            }
                        case 335:
                            {
                                PlaceSpike(column, row, "Spike2.png", 180);
                                break;
                            }
                        case 306:
                            {
                                PlaceSpike(column, row, "Spike2.png", 90);
                                break;
                            }
                        case 336:
                            {
                                PlaceSpike(column, row, "Spike2.png", 270);
                                break;
                            }
                        #endregion

                        #region spike 3
                        case 365:
                            {
                                PlaceSpike(column, row, "Spike3.png", 0);
                                break;
                            }
                        case 395:
                            {
                                PlaceSpike(column, row, "Spike3.png", 180);
                                break;
                            }
                        case 366:
                            {
                                PlaceSpike(column, row, "Spike3.png", 90);
                                break;
                            }
                        case 396:
                            {
                                PlaceSpike(column, row, "Spike3.png", 270);
                                break;
                            }
                        #endregion

                        case 331:
                        case 332:
                        case 361:
                            {
                                PlaceMovingSpike(column, row, tileNumber);
                                break;
                            }
                        #endregion

                        #region other
                        case 241:
                            {
                                PlaceSkull(column, row);
                                break;
                            }
                        case 301:
                            {
                                PlaceMovingWall(column, row);
                                break;
                            }
                        case 362:
                            {
                                PlaceLegs(column, row);
                                break;
                            }
                        case 242:
                            {
                                PlaceGoal(column, row);
                                break;
                            }

                        case 243:
                            {
                                PlacePortalIn(column, row);
                                break;
                            }
                        case 244:
                            {
                                PlacePortalOut(column, row);
                                break;
                            }
                            #endregion
                    }
            }
        }
    }

    #region tile placing methods

    /// <summary>
    /// places stationary wall
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    /// <param name="frame">animation sprite frame. same as tiled ID</param>
    private void PlaceStationaryWall(float column, float row, int frame)
    {
        AddChild(new StationaryWall(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset, frame -1));
    }

    /// <summary>
    /// places skull
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    private void PlaceSkull(float column, float row)
    {
        AddChild(new Skull(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    /// <summary>
    /// places spike
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    /// <param name="spriteImage">sprite image filename</param>
    /// <param name="spikeRotation">direction the spike is facing in degrees</param>
    private void PlaceSpike(float column, float row, string spriteImage, int spikeRotation)
    {
        AddChild(new Spike(spriteImage, column * _sideLength + _widthOffset, row * _sideLength + _heightOffset, spikeRotation));
    }

    /// <summary>
    /// places moving wall
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    private void PlaceMovingWall(float column, float row)
    {
        AddChild(new MovingWall("Urn.png", column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    /// <summary>
    /// places legs
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    private void PlaceLegs(float column, float row)
    {
        AddChild(new Legs(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    /// <summary>
    /// places goal
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    private void PlaceGoal(float column, float row)
    {
        AddChild(new Goal(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    /// <summary>
    /// places moving spike
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    /// <param name="frame">tileset frame ID</param>
    private void PlaceMovingSpike(float column, float row, int frame)
    {
        AddChild(new MovingSpike(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset, frame -1));
    }

    /// <summary>
    /// places portal in
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    private void PlacePortalIn(float column, float row)
    {
        AddChild(new PortalIn(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    /// <summary>
    /// places portal out
    /// </summary>
    /// <param name="column">tiled map column</param>
    /// <param name="row">tiled map row</param>
    private void PlacePortalOut(float column, float row)
    {
        AddChild(new PortalOut(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }
    #endregion
}

