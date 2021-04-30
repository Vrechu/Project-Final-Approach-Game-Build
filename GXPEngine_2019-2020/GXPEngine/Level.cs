using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GXPEngine;
using TiledMapParser;

class Level : GameObject
{
    private Map levelData;
    private float _rowLength = 64;
    private float _columnLength = 64;

    public Level(string fileName) : base()
    {
        levelData = MapParser.ReadMap(fileName);
        spawnTiles(levelData);
    }

    private void spawnTiles(Map leveldata)
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

                    case 69:
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
                    case 2:
                        {

                            break;
                        }
                }
            }
        }
    }

    private void PlaceStationaryWall(float _column, float _row)
    {
        AddChild(new StationaryWall("square.png", _column * _columnLength, _row * _rowLength, 1, 1));
    }

    private void PlaceSkull(float _column, float _row)
    {
        AddChild(new Skull(_column * _columnLength, _row * _rowLength));
    }

    private void PlaceSpike(float _column, float _row)
    {
        AddChild(new Spike(_column, _row, 270));
    }

    private void PlaceMovingWall(float _column, float _row)
    {
        AddChild(new MovingWall(_column, _row));
    }
}

