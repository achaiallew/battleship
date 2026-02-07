using UnityEngine;
using System;
using UnityEngine.Jobs;

public class Boundries : MonoBehaviour
{
    private float maxX = 2.5f;
    private float maxY = 1.2f;
    private float minX = -2.5f;
    private float minY = -4.5f;

    private float boatWidth;
    private float boatHeight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boatWidth = transform.GetComponent<SpriteRenderer>().bounds.size.x / 2;
        boatHeight = transform.GetComponent<SpriteRenderer>().bounds.size.y / 2;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float clampY = transform.position.y;
        float clampX = transform.position.x;
  

        // 2. Clamp X (Left and Right)
        if (clampX - boatWidth <= minX) clampX = minX + boatWidth;
        else if (clampX + boatWidth >= maxX) clampX = maxX - boatWidth;

        // 3. Clamp Y (Top and Bottom)
        if (clampY - boatHeight <= minY) clampY = minY + boatHeight;
        else if (clampY + boatHeight >= maxY) clampY = maxY - boatHeight;

        // 4. Apply both at once
        transform.position = new Vector3(clampX, clampY, 0);
        
    }      
}
