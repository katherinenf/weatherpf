using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float impulseX;
    public float lifeTime;
    public float scaleRate; // world units/second
    public float fadeInTime;
    public float fadeOutTime;

    SpriteRenderer renderer;
    Rigidbody2D rb;
    float curLifeTime;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // Apply a force that decreased overtime due to linear drag
        rb.AddForce(new Vector2(impulseX, 0), ForceMode2D.Impulse);
    }

    void Update()
    {
        curLifeTime += Time.deltaTime;

        // Scale over lifetime
        transform.localScale *= 1f + scaleRate * Time.deltaTime;

        // Update fade in and out
        Color c = renderer.color;
        c.a = Mathf.Min(curLifeTime / fadeInTime, 1f);
        c.a = Mathf.Min((lifeTime - curLifeTime) / fadeOutTime, c.a);
        renderer.color = c;

        // Destroy this when it's lifetime runs out
        if (curLifeTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
