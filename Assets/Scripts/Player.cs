using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 speed;
    public Vector2 floorCeilY;
    public float fireRate;
    public GameObject warmFrontPrefab;
    public GameObject coldFrontPrefab;
    Rigidbody2D rb;
    float fireCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        fireCooldown = Mathf.Max(fireCooldown - Time.deltaTime, 0f);
        if (Input.GetMouseButton(0))
        {
            Fire(coldFrontPrefab);
        } else if (Input.GetMouseButton(1))
        {
            Fire(warmFrontPrefab);
        }
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

    void Fire(GameObject bulletType)
    {
        if (fireCooldown > 0f)
        {
            return;
        }

        fireCooldown = fireRate;
        GameObject bullet = Instantiate(bulletType);
        bullet.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
    }
}
