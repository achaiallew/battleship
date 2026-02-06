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
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 pos = rb.position;

        pos.x = Mathf.Clamp(pos.x, minX + boatWidth, maxX - boatWidth);
        pos.y = Mathf.Clamp(pos.y, minY + boatHeight, maxY - boatHeight);

        rb.position = pos;
    }
}
