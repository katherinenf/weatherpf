using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The predefined storm types
public enum StormType
{
    Light,
    Heavy,
    Hurricane
}

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

    // The renderers to fade in and out
    public Renderer[] renderers;

    // The type of storm that what can be watered
    public StormType type;

    // The extents used to find waterables during shape casts
    public float castExtents;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Create horizontal velocity
        rb.AddForce(new Vector2(impulseX, 0), ForceMode2D.Impulse);

        // Animate the storm
        StartCoroutine("PlayStormSequence");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Plane>())
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectAndWaterIslands();
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
        yield return new WaitForSeconds(rainTime);

        // Fade out
        float curfadeTime = 0f;
        do
        {
            curfadeTime += Time.deltaTime;
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
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            transform.position, 
            new Vector2(castExtents * transform.localScale.x, 1f * transform.localScale.y), 
            0f, 
            Vector2.down
        );

        foreach (RaycastHit2D i in hits)
        {
            //if a hotspot is detected, spawn a hurricane
            if (i.transform.gameObject.name == "HotSpot(Clone)")
            {
                GameManager.Instance.StartHurricane();
                Destroy(gameObject);
            }
            // If an island is hit, switch dry island sprite to wet island sprite
            if (!i.rigidbody.GetComponent<Island>())
                continue;

            Waterable[] waterables = i.collider.GetComponentsInChildren<Waterable>();
            foreach (Waterable w in waterables)
            {
                w.OnWatered(type);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.75f);
        Gizmos.DrawLine(
            transform.position - new Vector3(castExtents / 2f * transform.localScale.x, 0, 0),
            transform.position + new Vector3(castExtents / 2f * transform.localScale.x, 0, 0)
        );
    }
}
