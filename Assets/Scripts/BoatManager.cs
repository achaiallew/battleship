using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class BoatManager : MonoBehaviour
{
  private bool boatSelected;
  public int boatNumber;

  private Collider2D col;
  private Vector3 startPosition;
  private Vector3 currentPosition;

  private SpriteRenderer rend;


  void Start()
  {
    rend = GetComponent<SpriteRenderer>();
  }

  void Update()
  {
    if (Mouse.current.leftButton.wasPressedThisFrame)
    {
      SelectBoat();
    }

    if (Mouse.current.leftButton.wasReleasedThisFrame)
    {
      boatSelected = false;
      Cursor.visible = true;

      Color temp = rend.color;
      temp.a = 1f;
      rend.color = temp;

      // col.enabled = false;
      // Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
      // col.enabled = true;

      // if (hitCollider != null && hitCollider.TryGetComponent(out ITileDropArea titleDropArea))
      // {
      //   titleDropArea.OnBoatDrop(this);
      // }
      // else
      // {
      //   transform.position = startPosition;
      // }

    }

    if (boatSelected)
    {
      Vector2 mousePos = Mouse.current.position.ReadValue();
      Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
      worldPos.z = 0f;

      transform.position = worldPos;
    }

    if (Mouse.current.rightButton.wasPressedThisFrame)
    {
      RotateBoat();
    }
  }

  void SelectBoat()
  {
    Vector2 mousePos = Mouse.current.position.ReadValue();
    Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

    RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

    if (hit.collider != null && hit.collider.gameObject == gameObject)
    {
      boatSelected = true;
      Cursor.visible = false;
      
      Color temp = rend.color;
      temp.a = 0.5f;
      rend.color = temp;
    }
  }

  void RotateBoat()
  {
    Vector2 mousePos = Mouse.current.position.ReadValue();
    Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

    RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

    if (hit.collider != null && hit.collider.gameObject == gameObject)
    {
      transform.Rotate(0, 0, 90);
    }
  }

}
