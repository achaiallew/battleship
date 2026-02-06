using UnityEngine;

public class Tile : MonoBehaviour
{
  [SerializeField] private Color baseColour, offsetColour;
  [SerializeField] private SpriteRenderer rendColour;
  [SerializeField] private GameObject highlight;

  public bool isOccupied;
  private SpriteRenderer rend;



  void Start()
  {
    rend = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if (isOccupied)
    {
      string tileName = gameObject.name;

      string[] parts = tileName.Split(' ');

      // parts[0] = "Tile"
      // parts[1] = "0"
      // parts[2] = "0"

      int x = int.Parse(parts[1]);
      int y = int.Parse(parts[2]);
    } 
  }


  public void Init(bool isOffset)
  {
      rendColour.color = isOffset ? offsetColour : baseColour;
      GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Tiles");

  }

  // public void OnBoatDrop(BoatManager boat)
  // {
  //   boat.transform.position = transform.position;
  //   Debug.Log("Boat Dropped");
  // }
}
