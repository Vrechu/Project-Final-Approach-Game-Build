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
    private float _widthOffset = 200;
    private float _heightOffset = 100;

    public Level(string levelFileName,string backgroundFileName, MyGame.ScreenState nextScreen) : base()
    {
        InteractionHitbox.OnGoalReached += NextScreen;
        _nextScreen = nextScreen;
        levelData = MapParser.ReadMap(levelFileName);
        _heightOffset += _sideLength / 2;
        _widthOffset += _sideLength / 2;
        OnLevelStart?.Invoke();
        AddChild(new Sprite(backgroundFileName));
        SpawnTiles(levelData);
        AddChild(new GravityHUD(game.width/2, 800));
    }

    protected override void OnDestroy()
    {
        InteractionHitbox.OnGoalReached -= NextScreen;
    }

    private void NextScreen()
    {
        OnLevelFinished?.Invoke(_nextScreen);
    }

    /// <summary>
    /// Places tiles in the locations specified by the Tiled map file
    /// </summary>
    /// <param name="leveldata"></param>
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
                switch (tileNumber)
                {
                    #region stationary walls
                    case 33:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 34:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 35:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 36:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 37:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 38:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 63:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 93:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 123:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 153:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 183:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 184:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 185:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 186:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 187:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 188:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 68:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 98:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 128:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    case 158:
                        {
                            PlaceStationaryWall(column, row, tileNumber);
                            break;
                        }
                    #endregion

                    #region stationary spikes
                    case 243:
                        {
                            PlaceSpike(column, row, 0);
                            break;
                        }
                    case 244:
                        {
                            PlaceSpike(column, row, 180);
                            break;
                        }
                    case 245:
                        {
                            PlaceSpike(column, row, 270);
                            break;
                        }
                    case 246:
                        {
                            PlaceSpike(column, row, 90);
                            break;
                        }
                    #endregion

                    case 215:
                        {
                            PlaceSkull(column, row);
                            break;
                        }
                    case 218:
                        {
                            PlaceMovingWall(column, row);
                            break;
                        }
                    case 217:
                        {
                            PlaceLegs(column, row);
                            break;
                        }
                    case 216:
                        {
                            PlaceGoal(column, row);
                            break;
                        }
                    case 247:
                        {
                            PlaceMovingSpike(column, row);
                            break;
                        }

                    case 213:
                        {
                            PlacePortalIn(column, row);
                            break;
                        }
                    case 214:
                        {
                            PlacePortalOut(column, row);
                            break;
                        }
                }
            }
        }
    }

    private void PlaceStationaryWall(float column, float row, int frame)
    {
        AddChild(new StationaryWall(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset, frame -1));
    }

    private void PlaceSkull(float column, float row)
    {
        AddChild(new Skull(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceSpike(float column, float row, int spikeRotation)
    {
        AddChild(new Spike(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset, spikeRotation));
    }

    private void PlaceMovingWall(float column, float row)
    {
        AddChild(new MovingWall("moving_wall.png", column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceLegs(float column, float row)
    {
        AddChild(new Legs(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceGoal(float column, float row)
    {
        AddChild(new Goal(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceMovingSpike(float column, float row)
    {
        AddChild(new MovingSpike("moving_spikes.png", column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlacePortalIn(float column, float row)
    {
        AddChild(new PortalIn(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlacePortalOut(float column, float row)
    {
        AddChild(new PortalOut(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }
        
}

