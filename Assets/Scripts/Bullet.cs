using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Initial force to apply on spawn
    public float impulseX;

    // The time until this bullet despawns (seconds)
    public float lifeTime;

    // There rate that the front grows (world units/second)
    public float scaleRate;

    // Alpha lerp in duration
    public float fadeInTime;

    // Alpha lerp out duration
    public float fadeOutTime;

    // The sprite renderer to fade
    public SpriteRenderer renderer;

    // An emitter to release before destroying to keep particles alive
    public ParticleSystemRenderer emitter;

    Rigidbody2D rb;
    float curLifeTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Apply a force that decreases overtime due to linear drag
        rb.AddForce(new Vector2(impulseX, 0), ForceMode2D.Impulse);
    }

    void Update()
    {
        // Increment lifetime
        curLifeTime += Time.deltaTime;

        // Scale over lifetime
        transform.localScale *= 1f + scaleRate * Time.deltaTime;

        // Update fade in and out
        Color c = renderer.color;
        c.a = Mathf.Min(curLifeTime / fadeInTime, 1f);
        c.a = Mathf.Min((lifeTime - curLifeTime) / fadeOutTime, c.a);
        renderer.color = c;
        emitter.material.color = c;

        // Destroy this when it's lifetime runs out
        if (curLifeTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }
}
