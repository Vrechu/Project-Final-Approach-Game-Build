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

    public Level(string fileName, MyGame.ScreenState nextScreen) : base()
    {
        InteractionHitbox.OnGoalReached += NextScreen;
        _nextScreen = nextScreen;
        levelData = MapParser.ReadMap(fileName);
        _heightOffset += _sideLength / 2;
        _widthOffset += _sideLength / 2;
        OnLevelStart?.Invoke();
        SpawnTiles(levelData);
    }

    private void OnDestroy()
    {
        InteractionHitbox.OnGoalReached -= NextScreen;
    }

    private void NextScreen()
    {
        OnLevelFinished?.Invoke(_nextScreen);
    }

    private void SpawnTiles(Map leveldata)
    {
        if (leveldata.Layers == null
            || leveldata.Layers.Length == 0) //nullcheck
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
                    case 33:
                        {
                            PlaceStationaryWall(column, row);
                            break;
                        }

                    case 71:
                        {
                            PlaceSkull(column, row);
                            break;
                        }
                    case 70:
                        {
                            PlaceSpike(column, row);
                            break;
                        }
                    case 100:
                        {
                            PlaceMovingWall(column, row);
                            break;
                        }
                    case 101:
                        {
                            PlaceLegs(column, row);
                            break;
                        }
                    case 34:
                        {
                            PlaceGoal(column, row);
                            break;
                        }
                }
            }
        }
    }

    private void PlaceStationaryWall(float column, float row)
    {
        AddChild(new StationaryWall("square.png", column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceSkull(float column, float row)
    {
        AddChild(new Skull(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceSpike(float column, float row)
    {
        AddChild(new Spike(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset, 270));
    }

    private void PlaceMovingWall(float column, float row)
    {
        AddChild(new MovingWall("colors.png", column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceLegs(float column, float row)
    {
        AddChild(new Legs(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }

    private void PlaceGoal(float column, float row)
    {
        AddChild(new Goal(column * _sideLength + _widthOffset, row * _sideLength + _heightOffset));
    }
}

