using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] private float minY;
    public float speed;

    [SerializeField] private int powerupChance;
    [SerializeField] private GameObject powerUp;
    [SerializeField] private GameObject powerUp2;
    [SerializeField] private GameObject powerUp3;

    private GameObject self;

    private float maxVelocity = 13f;
    private Rigidbody2D rb;
    public bool original = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //If its the main ball, set its reference to the ball manager and set its speed
        if (original)
        {
            rb.velocity = Vector2.down * speed;
            BallManager.instance.mainBall = this.gameObject;
        }

    }

    private void OnEnable()
    {
        self = this.gameObject;
    }

    void Update()
    {
        //If ball goes out of bounds below the stage, destroy it and get rid of a life if it is the main ball
        if(self.transform.position.y < minY)
        {
            //self.transform.position = Vector3.zero;
            Destroy(self);
            rb.velocity = Vector2.down * speed;
            if (original)
            {
                UIManager.instance.lives--;
                UIManager.instance.CheckIfAlive();
                BallManager.instance.DeleteChildren();
            }
            
        }

        //If ball gets too fast, cap it at a certain speed (not really needed since speed scales off 
        //of timescale but is here just in case
        if(rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Destroy a brick if it hits it, balls were made to ignore hitting other balls
        if (other.gameObject.CompareTag("Brick"))
        {
            SpawnPowerup(other.transform.position, Quaternion.Euler(0,0,0));
            Destroy(other.gameObject);            
            UIManager.instance.score += 5;
            UIManager.instance.CheckIfWon();
        }
    }

    private void SpawnPowerup(Vector3 objPos, Quaternion objRot)
    {
        //Determine randomly whether or not a powerup should spawn (30% chance default)
        int randNum = Random.Range(0, 99);
        
        if(randNum < powerupChance)
        {
            int randPowerUp = Random.Range(1, 4);
            //Debug.Log(randPowerUp);

            //Spawn powerup and make it a child to the Ball Manager
            if (randPowerUp == 1)
            {
                var powerUpRef = Instantiate(powerUp, objPos, objRot);
                powerUpRef.transform.parent = BallManager.instance.transform;
            }            
            
            if (randPowerUp == 2)
            {
                var powerUpRef = Instantiate(powerUp2, objPos, objRot);
                powerUpRef.transform.parent = BallManager.instance.transform;
            }

            if (randPowerUp == 3)
            {
                var powerUpRef = Instantiate(powerUp3, objPos, objRot);
                powerUpRef.transform.parent = BallManager.instance.transform;
            }
            
        }
    }

}
