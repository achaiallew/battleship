using UnityEngine;

public class Tile : MonoBehaviour
{
  [SerializeField] private Color baseColour, offsetColour;
  [SerializeField] private SpriteRenderer rendColour;
  [SerializeField] private GameObject highlight;

  public void Init(bool isOffset)
    {
        rendColour.color = isOffset ? offsetColour : baseColour;
        GetComponent<Renderer>().sortingLayerID = SortingLayer.NameToID("Tiles");

    }
}
