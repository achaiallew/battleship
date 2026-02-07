using UnityEngine;
using System;
using System.Runtime.Serialization;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    [SerializeField] private float width, height;

    [SerializeField] private Tile tilePrefab;

    [SerializeField] private Transform tilesParent;


    public List<Tile> tiles = new List<Tile>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        int col = 0;
        // Loop Over Width 
        for (float x = -2.25f; x <= width;)
        {
            int row = 0;
            // Loop Over Height
            for (float y = -4.25f; y <= height;)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.transform.SetParent(tilesParent);

                bool isOffset = ((row + col) % 2 != 0);
                spawnedTile.Init(isOffset, row, col);
                tiles.Add(spawnedTile);

                spawnedTile.name = $"Tile {row} {col}";
                
                y += 0.5f;  
                row ++;   
            }

            x += 0.5f;
            col ++;
        }

    }

}

