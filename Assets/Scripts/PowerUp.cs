using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float speed;
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private int ballSpawn;
    [SerializeField] private int powerUpType;

    private Vector2 spawnAngle = new Vector2(0.9f, 0.9f);
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 selfPos = this.transform.position;
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
            for(int i = 0; i < ballSpawn; i++)
            {
                if(powerUpType == 1)
                {
                    //Attempt to spawn the balls going different ways and make them a child to the Ball Manager
                    if(i%2 == 0)
                    {
                        GameObject ballRef = Instantiate(ballPrefab, selfPos, Quaternion.Euler(0, 0, 45));
                        ballRef.GetComponent<Rigidbody2D>().velocity = spawnAngle * (speed + (1/0.8f));
                        ballRef.transform.parent = BallManager.instance.transform;
                    }
                    else
                    {
                        GameObject ballRef = Instantiate(ballPrefab, selfPos, Quaternion.Euler(0, 0, -45));
                        ballRef.GetComponent<Rigidbody2D>().velocity = spawnAngle * (-speed - (1/0.8f));
                        ballRef.transform.parent = BallManager.instance.transform;
                    }
                }

          
            }

            if(powerUpType == 2)
            {
                //Calls the SpeedUp powerup 
                BallManager.instance.SpeedUp();
            }

            else if( powerUpType == 3)
            {
                //Calls the Enlarge powerup 
                PlayerMovement.instance.Enlarge(.25f);
            }
        }
    }
}
