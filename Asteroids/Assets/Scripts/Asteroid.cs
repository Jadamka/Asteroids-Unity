using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //FIX SOUNDS!!!


    Rigidbody2D rb;

    float screenTop;
    float screenBottom;
    float screenRight;
    float screenLeft;

    public float speed = .5f;

    public bool big;
    public bool medium;
    public bool small;

    public GameObject mediumA;
    public GameObject smallA;

    public int points;
    private GameObject player;

    void Start()
    {
        RandomizeZ();

        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed);

        screenTop = GameObject.Find("Player").GetComponent<Controls>().screenTop;
        screenBottom = GameObject.Find("Player").GetComponent<Controls>().screenBottom;
        screenRight = GameObject.Find("Player").GetComponent<Controls>().screenRight;
        screenLeft = GameObject.Find("Player").GetComponent<Controls>().screenLeft;

        player = GameObject.FindWithTag("Player");
    }

    void RandomizeZ()
    {
        Quaternion randYRotaion = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.rotation = randYRotaion;
    }

    void Update()
    {
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
        if(col.collider.tag == "Bullet" && big)
        {
            Instantiate(mediumA, transform.position, Quaternion.identity);
            Instantiate(mediumA, transform.position, Quaternion.identity);

            player.SendMessage("Score", points);
            Destroy(gameObject);
        }

        if (col.collider.tag == "Bullet" && medium)
        {
            Instantiate(smallA, transform.position, Quaternion.identity);
            Instantiate(smallA, transform.position, Quaternion.identity);

            player.SendMessage("Score", points);
            Destroy(gameObject);
        }

        if (col.collider.tag == "Bullet" && small)
        {
            player.SendMessage("Score", points);
            Destroy(gameObject);
        }
    }
}
