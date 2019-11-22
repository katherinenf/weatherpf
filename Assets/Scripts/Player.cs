using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 floorCeilY;
    SpriteRenderer sprite;
    Rigidbody2D rb;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        Vector2 input = new Vector2(1, Input.GetAxisRaw("Vertical"));
        Vector2 delteMove = input * speed * Time.fixedDeltaTime;
        rb.MovePosition(ClampToScreen(rb.position + delteMove));
    }

    Vector2 ClampToScreen(Vector2 pos)
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        pos.x = Mathf.Clamp(pos.x, minScreenBounds.x, maxScreenBounds.x);
        pos.y = Mathf.Clamp(pos.y, minScreenBounds.y  - floorCeilY.x, maxScreenBounds.y - floorCeilY.y);
        return pos;
    }
}
