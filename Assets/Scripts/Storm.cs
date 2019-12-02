using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : MonoBehaviour
{
    // Initial force to apply on spawn
    public float impulseX;

    // The target scale that this cloud grows to
    public Vector3 growTarget;

    // Time that this cloud grows for
    public float growTime;

    // Time that this cloud rains for
    public float rainTime;

    // Time that this cloud fades out for
    public float fadeOutTime;

    // Dry sprites to assign to the islands
    public List<Sprite> drySprites;

    // Wet sprites to assign to the islands
    public List<Sprite> wetSprites;

    // The renderers to fade in and out
    public Renderer[] renderers;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Create horizontal velocity
        rb.AddForce(new Vector2(impulseX, 0), ForceMode2D.Impulse);

        // Animate the storm
        StartCoroutine("PlayStormSequence");

    }

    IEnumerator PlayStormSequence()
    {
        // Fade in during the growth phase
        transform.localScale = growTarget;
        float curGrowTime = 0f;
        do
        {
            curGrowTime += Time.deltaTime;
            float alpha = Mathf.SmoothStep(0, 1, curGrowTime / growTime);
            foreach (Renderer r in renderers)
            {
                Color c = r.material.color;
                c.a = alpha;
                r.material.color = c;
            }

            yield return null;
        } while (curGrowTime < growTime);

        // Rain and water islands
        float curRainTime = 0f;
        do
        {
            curRainTime += Time.deltaTime;
            DetectAndWaterIslands();
            yield return null;
        } while (curRainTime < rainTime);

        // Fade out
        float curfadeTime = 0f;
        do
        {
            curfadeTime += Time.deltaTime;
            DetectAndWaterIslands();
            float alpha = Mathf.SmoothStep(1, 0, curfadeTime / fadeOutTime);
            foreach (Renderer r in renderers)
            {
                Color c = r.material.color;
                c.a = alpha;
                r.material.color = c;
            }
            yield return null;
        } while (curfadeTime < fadeOutTime);

        // Despawn
        Destroy(gameObject);
    }

    void DetectAndWaterIslands()
    {
        // Find island sections a storm passes over
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, 6f);

        // If an island is hit, switch dry island sprite to wet island sprite
        foreach (RaycastHit2D i in hits)
        {
            if (!i.transform.GetComponent<Island>())
                continue;

            int spriteIndex = drySprites.IndexOf(i.collider.GetComponent<SpriteRenderer>().sprite);
            if (spriteIndex >= 0)
            {
                i.collider.GetComponent<SpriteRenderer>().sprite = wetSprites[spriteIndex];
                
                Flora flora = i.collider.GetComponentInChildren<Flora>();
                if (flora)
                {
                    flora.OnWatered();
                }
            }
        }
    }
}
