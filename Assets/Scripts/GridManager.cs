using System.Runtime.Serialization;
using UnityEngine;
using System;

public class GridManager : MonoBehaviour
{
 [SerializeField] private float width, height;

 [SerializeField] private Tile tilePrefab;

[SerializeField] private Transform tilesParent;


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
                spawnedTile.name = $"Tile {row} {col}";
                spawnedTile.transform.SetParent(tilesParent);

                bool isOffset = ((row + col) % 2 != 0);
                spawnedTile.Init(isOffset);     

                
                y += 0.5f;  
                row ++;   
            }

            x += 0.5f;
            col ++;
        }

    }
}
