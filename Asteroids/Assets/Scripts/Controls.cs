using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{

    public Rigidbody2D rb = new Rigidbody2D();

    public float rotationSpeed = 1f;

    public float speed = 2f;
    float move;

    public float screenTop;
    public float screenBottom;
    public float screenRight;
    public float screenLeft;

    public GameObject bullet;
    public Transform attackPoint;
    public float timeBetweenShots = 0.5f;
    bool readyToShoot = true;

    private int score;
    public GameObject scoreText;

    public int lives;
    public GameObject livesText;
    Vector2 newPos = new Vector2(0, 0);

    AudioSource tickSource;

    private void Start()
    {
        score = 0;
        lives = 3;

        livesText.GetComponent<Text>().text = "Lives: " + lives;

        tickSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        move = Input.GetAxisRaw("Vertical");

        //Screen wrapping

        Vector2 newPos = transform.position;

        if(transform.position.y > screenTop)
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

        if (Input.GetKeyDown(KeyCode.Space) && readyToShoot)
        {
            Instantiate(bullet, attackPoint.position, attackPoint.rotation);

            readyToShoot = false;

            Invoke(nameof(ResetShot), timeBetweenShots);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddRelativeForce(Vector2.up.normalized * move * speed);
        }

        float rotate = Input.GetAxis("Horizontal") * rotationSpeed;
        transform.Rotate(0, 0, 0 - rotate);
    }

    void ResetShot()
    {
        readyToShoot = true;
    }

    void Score(int points)
    {
        score += points;
        scoreText.GetComponent<Text>().text = "" + score;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.collider.tag == "Asteroid")
        {
            if(lives > 1)
            {
                tickSource.Play();
                lives--;
                livesText.GetComponent<Text>().text = "Lives: " + lives;

                transform.position = newPos;
            }

            else
            {
                tickSource.Play();
                lives--;
                livesText.GetComponent<Text>().text = "Lives: " + lives;

                Destroy(gameObject, .3f);
            }

        }
    }
}
