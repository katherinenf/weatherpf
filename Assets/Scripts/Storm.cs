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

    //Time before first drop gets low enough to effect flora
    public float timeToGround;

    // Time that this cloud fades out for
    public float fadeOutTime;

    // The rain emitter that spawns the drops
    public ParticleSystem rainEmitter;

    public List<Sprite> drySprites;
    public List<Sprite> wetSprites;

    SpriteRenderer sprite;
    Rigidbody2D rb;
    ParticleSystemRenderer rainRenderer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rainRenderer = rainEmitter.gameObject.GetComponent<ParticleSystemRenderer>();

        // Create horizontal velocity
        rb.AddForce(new Vector2(impulseX, 0), ForceMode2D.Impulse);

        // Animate the storm
        StartCoroutine("PlayStormSequence");

    }

    IEnumerator PlayStormSequence()
    {
        // Scale up during the growth phase
        Vector3 startScale = transform.localScale;
        float curGrowTime = 0f;
        do
        {
            curGrowTime += Time.deltaTime;
            float t = curGrowTime / growTime;

            Color c = sprite.color;
            c.a = Mathf.SmoothStep(0, 1, t);
            sprite.color = c;

            transform.localScale = Vector3.Lerp(startScale, growTarget, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        } while (curGrowTime < growTime);

        // Wait while raining
        rainEmitter.Play();
        float curRainTime = 0f;
        do
        {
            curRainTime += Time.deltaTime;

            if (curRainTime > timeToGround)
            {
                // Find island sections a storm passes over
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, -Vector2.up, 6f);

                // If an island is hit, switch dry island sprite to wet island sprite
                foreach (RaycastHit2D i in hits)
                {
                    
                    int spriteIndex = drySprites.IndexOf(i.transform.GetComponent<SpriteRenderer>().sprite);
                    if (spriteIndex >= 0)
                    {
                        i.transform.GetComponent<SpriteRenderer>().sprite = wetSprites[spriteIndex];
                    }

                }
            }
            yield return null;
        } while (curRainTime < rainTime);
        rainEmitter.Stop();

        // Fade out
        float curfadeTime = 0f;
        do
        {
            curfadeTime += Time.deltaTime;
            Color c = sprite.color;
            c.a = Mathf.SmoothStep(1, 0, curfadeTime / fadeOutTime);
            sprite.color = c;
            rainRenderer.material.color = c;
            yield return null;
        } while (curfadeTime < fadeOutTime);
   

        // Despawn
        Destroy(gameObject);
    }


}
