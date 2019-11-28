using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherFront : MonoBehaviour
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

    // Seconds to wait before spawning the cloud
    public float stormSpawnDelay;

    // The storm prefab to spawn
    public GameObject stormPrefab;

    // The sprite renderer to fade
    public SpriteRenderer sprite;

    // An emitter to release before destroying to keep particles alive
    public ParticleSystemRenderer emitter;

    Rigidbody2D rb;
    float curLifeTime;
    bool stormSpawned;

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

        if (!stormSpawned && curLifeTime >= stormSpawnDelay)
        {
            SpawnStorm();
        }

        // Scale over lifetime
        transform.localScale *= 1f + scaleRate * Time.deltaTime;

        // Update fade in and out
        Color c = sprite.color;
        c.a = Mathf.Min(curLifeTime / fadeInTime, 1f);
        c.a = Mathf.Min((lifeTime - curLifeTime) / fadeOutTime, c.a);
        sprite.color = c;
        emitter.material.color = c;

        // Destroy this when it's lifetime runs out
        if (curLifeTime >= lifeTime)
        {
            Destroy(this.gameObject);
        }
    }

    void SpawnStorm()
    {
        stormSpawned = true;
        GameObject storm = Instantiate(stormPrefab);
        storm.transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);
    }
}
