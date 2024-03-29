﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    // Movement speed
    public Vector2 speed;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Stop moving if the game is over
        if (GameManager.Instance.gameIsOver)
            return;

        // Calculate the amount to move this frame
        Vector2 delteMove = speed * Time.fixedDeltaTime;

        // Update rigidbody position
        rb.MovePosition(rb.position + delteMove);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            GameManager.Instance.LoseHealth();
        }
        else if (collision.gameObject.GetComponent<Storm>())
        {
            collision.gameObject.GetComponent<Storm>().BreakApart();
        }
    }
}
