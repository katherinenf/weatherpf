﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Player movement speed
    public Vector2 speed;

    // Additional buffer space that the player cannot move on the vertical axis
    public Vector2 floorCeilY;

    // Maximum fire rate when the button is held or spammed
    public float fireRate;

    // Bullet to spawn for warm fronts
    public GameObject warmFrontPrefab;

    // Bullet to spawn for cold fronts
    public GameObject coldFrontPrefab;

    Rigidbody2D rb;
    float fireCooldown;
    float lastX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastX = transform.position.x;
    }

    void Update()
    {
        // Stop allowing input if the game is over
        if (GameManager.Instance.gameIsOver)
            return;

        // Compute distance traveled
        GameManager.Instance.distanceTraveled += transform.position.x - lastX;
        lastX = transform.position.x;

        // Update the shoot cooldown.
        fireCooldown = Mathf.Max(fireCooldown - Time.deltaTime, 0f);

        // Handle user input
        if (Input.GetMouseButton(0) || Input.GetKeyDown("c")) // Left mouse
        {
            Fire(coldFrontPrefab);
        } 
        else if (Input.GetMouseButton(1) || Input.GetKeyDown("w")) // Right mouse
        {
            Fire(warmFrontPrefab);
        }
    }

    void FixedUpdate()
    {
        // Stop moving if the game is over
        if (GameManager.Instance.gameIsOver)
            return;

        // Get player vertical input
        Vector2 input = new Vector2(1, Input.GetAxisRaw("Vertical"));

        // Calculate the amount to move this frame
        Vector2 delteMove = input * speed * Time.fixedDeltaTime;

        // Update rigidbody position
        rb.MovePosition(ClampToScreen(rb.position + delteMove));
    }

    // Clamps input pos to the screen bounds
    Vector2 ClampToScreen(Vector2 pos)
    {
        Vector3 minScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxScreenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        pos.x = Mathf.Clamp(pos.x, minScreenBounds.x, maxScreenBounds.x);
        pos.y = Mathf.Clamp(pos.y, minScreenBounds.y  - floorCeilY.x, maxScreenBounds.y - floorCeilY.y);
        return pos;
    }

    // Shoot a front
    void Fire(GameObject bulletType)
    {
        // Don't allow shooting when one cooldown
        if (fireCooldown > 0f)
            return;

        // Start the fire cooldown
        fireCooldown = fireRate;

        // Spawn a bullet of the right type slightly ahead of us
        GameObject bullet = Instantiate(bulletType);
        bullet.transform.position = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
    }
}
