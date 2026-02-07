using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RandomPlacement : MonoBehaviour
{
    public List<GameObject> boats;

    public GridManager gridManager;
    public BoatManager boatManager;

    public void RandomBoatPlacement()
    {
        // Destroy Boats 
        GameObject[] boatsOnScene = GameObject.FindGameObjectsWithTag("Boat");
        foreach (GameObject oldboat in boatsOnScene)
        {
            Destroy(oldboat);
        }
 
        foreach (GameObject boat in boats)
        {
            // Choose Random Position
            int randomIndex = Random.Range(0, gridManager.tiles.Count);
            Tile randomTile = gridManager.tiles[randomIndex];

            // Pick random rotation (0, 90, 180, 270)
            int randomRotation = Random.Range(0, 4);
            Quaternion rotation = Quaternion.Euler(0, 0, 90 * randomRotation);

            // Spawn boat TEMPORARILY at tile position
            GameObject boatInstance = Instantiate(boat, randomTile.transform.position, rotation);

        // Snap properly using shadow reference
            boatManager.SnapBoatToGrid(boatInstance.transform);

        }
    }

}
