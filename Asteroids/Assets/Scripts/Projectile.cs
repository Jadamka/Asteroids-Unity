using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    Rigidbody2D rb;

    public float bulletSpeed = 5f;

    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * bulletSpeed);
        screenTop = GameObject.Find("Player").GetComponent<Controls>().screenTop;
        screenBottom = GameObject.Find("Player").GetComponent<Controls>().screenBottom;
        screenRight = GameObject.Find("Player").GetComponent<Controls>().screenRight;
        screenLeft = GameObject.Find("Player").GetComponent<Controls>().screenLeft;
    }

    void Update()
    {
        Destroy(gameObject, 1.3f);

        //Screen wrapping
        Vector2 newPos = transform.position;

        if (transform.position.y > screenTop)
        {
            newPos.y = screenBottom;
        }
        if (transform.position.y < screenBottom)
        {
            newPos.y = screenTop;
        }
        if (transform.position.x > screenRight)
        {
            newPos.x = screenLeft;
        }
        if (transform.position.x < screenLeft)
        {
            newPos.x = screenRight;
        }

        transform.position = newPos;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == "Asteroid")
        {
            Destroy(gameObject);
        }
    }
}
