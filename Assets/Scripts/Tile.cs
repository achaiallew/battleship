using UnityEngine;


public class Tile : MonoBehaviour
{
  [SerializeField] private Color baseColour, offsetColour;
  [SerializeField] private SpriteRenderer rendColour;
  
  [SerializeField] private GameObject hover;

  public bool isOccupied;
  public int row;
  public int col;

  private int boatCount = 0;

  public Vector2 snapPoint;

  void Awake()
  {
    rendColour = GetComponent<SpriteRenderer>();
    snapPoint = transform.position;
  }


  public void Init(bool isOffset, int r, int c)
  {
    row = r;
    col = c;
    isOccupied = false;

    rendColour.color = isOffset ? offsetColour : baseColour;
    GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Tiles");

  }

  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.GetComponent<ShadowTile>() != null)
    {
      boatCount++;
      HoverOn();
      isOccupied =true;
    }
  }

  private void OnTriggerExit2D(Collider2D other)
  {
    if (other.GetComponent<ShadowTile>() != null)
    {
      boatCount--;
      if (boatCount <= 0)
          HoverOff();
    }
  }

  

  public void HoverOn()
  {
    hover.SetActive(true);
  }

  public void HoverOff()
  {
    hover.SetActive(false);
  }

}
