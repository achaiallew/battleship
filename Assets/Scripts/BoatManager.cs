using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class BoatManager : MonoBehaviour
{
  [Header("Input References")]
  public InputActionReference trackingAction;
  public InputActionReference clickingAction;
  public InputActionReference rightclickAction;

  [Header("Camera Reference")]
  public Camera cam;

  [Header("Physics Settings")]
  public LayerMask boatLayer; // Assign the "Boats" layer here in the Inspector

  private Vector2 currentPos;
  private Transform selectedBoat;
  private Vector3 offset;
  private Plane dragPlane;
  private float boatZ; // Store the original Z depth
  private SpriteRenderer rend;

  public GridManager gridManager; // drag GridManager GameObject in inspector

  private void OnEnable()
  {
    trackingAction.action.Enable();
    clickingAction.action.Enable();
    rightclickAction.action.Enable();

    trackingAction.action.performed += OnMousePosition;
    clickingAction.action.performed += OnMousePress;
    clickingAction.action.canceled += OnMouseRelease;
    rightclickAction.action.performed += OnRightClick;
  }

  private void OnDisable()
  {
    trackingAction.action.performed -= OnMousePosition;
    clickingAction.action.performed -= OnMousePress;
    clickingAction.action.canceled -= OnMouseRelease;
    rightclickAction.action.performed -= OnRightClick;

    trackingAction.action.Disable();
    clickingAction.action.Disable();  
    rightclickAction.action.Disable();
  }


private void OnMousePress(InputAction.CallbackContext context)
{
  Ray ray = cam.ScreenPointToRay(currentPos);
  RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, boatLayer);

  if (hit.collider != null)
  {
      selectedBoat = hit.transform;
      boatZ = selectedBoat.position.z; // Remember the original Z

      // Create the plane exactly where the boat is sitting
      dragPlane = new Plane(Vector3.back, new Vector3(0, 0, boatZ));

      // Calculate offset: Where is the boat relative to where I clicked?
      if (dragPlane.Raycast(ray, out float distance))
      {
          Vector3 hitPoint = ray.GetPoint(distance);
          offset = selectedBoat.position - hitPoint;
      }
      
      Cursor.visible = false;
      
      // Get the renderer from the CLICKED object, not this script's object
      rend = selectedBoat.GetComponent<SpriteRenderer>();
      if (rend != null)
      {
          Color temp = rend.color;
          temp.a = 0.5f;
          rend.color = temp;
      }
  }
}

private void OnMousePosition(InputAction.CallbackContext context)
{
  currentPos = context.ReadValue<Vector2>();

  if (selectedBoat != null)
  {
      Ray ray = cam.ScreenPointToRay(currentPos);
      if (dragPlane.Raycast(ray, out float distance))
      {
        Vector3 targetPoint = ray.GetPoint(distance);
        Vector3 finalPos = targetPoint + offset;
        
        // FORCE the Z to stay the same
        finalPos.z = boatZ; 
        
        selectedBoat.position = finalPos;
      }
  }

}
private void OnMouseRelease(InputAction.CallbackContext context)
{
    if (selectedBoat == null)
    {
        Cursor.visible = true;
        return;
    }

    if (rend != null)
    {
        Color temp = rend.color;
        temp.a = 1f;
        rend.color = temp;
    }

    Cursor.visible = true;
    SnapBoatToGrid(selectedBoat);
    selectedBoat = null;
    rend = null;
}


private void OnRightClick(InputAction.CallbackContext context)
  {
    Ray ray = cam.ScreenPointToRay(currentPos);
    RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, boatLayer);

    if (hit.collider != null)
    {
      // Get the transform of the specific boat you just right-clicked
      Transform boatToRotate = hit.transform;
        
      // Rotate it 90 degrees on the Z axis
      boatToRotate.Rotate(0, 0, 90);
    }
  }

  public void SnapBoatToGrid(Transform boat)
  {
     if (boat == null)
        return;

    if (boat.childCount == 0)
        return;

    // Step 4a: Pick reference shadow
    Transform referenceShadow = boat.GetChild(0); // pick first shadow by default
    Tile closestTile = null;
    float minDist = Mathf.Infinity;

    foreach (Tile tile in gridManager.tiles)
    {
      float dist = Vector2.Distance(referenceShadow.position, tile.transform.position);
      if (dist < minDist)
      {
        minDist = dist;
        closestTile = tile;
      }
    }

    // Step 4b: Compute offset
    if (closestTile != null)
    {
      Vector2 offset = (Vector2)closestTile.transform.position - (Vector2)referenceShadow.position;

      // Step 4c: Move the boat parent
      boat.position = (Vector2)boat.position + offset;
    }
  }

}
