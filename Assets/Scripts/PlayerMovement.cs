using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Camera m_Camera;

    [SerializeField] private float speed;
    private float moveX;

    [SerializeField] private float maxX;

    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject ballSpawnPoint;

    private GameObject ballRef;
    private float speedRef;

    private bool held = false;

    public static PlayerMovement instance;

    void Start()
    {
        ballRef = ball;
        //transform.position = new Vector3(0, -4.2f, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        moveX = mousePosition.x;
        //moveX = Input.GetAxis("Horizontal");
        if ((moveX > 0 && transform.position.x < maxX) || (moveX < 0 && transform.position.x > -maxX))
        {
            //transform.position = Vector3.right * moveX * speed * Time.deltaTime;
            //transform.position = Vector3.right * moveX;
            
            //Set the paddle location to where the mouse is and keep it at the bottom of the screen (-4.5f)
            transform.position = new Vector3(moveX, -4.5f, 0);
            
        }

        //Only let player spawn ball if they are alive
        if (UIManager.instance.isAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //spawn a ball at a position above the paddle
                ballRef = Instantiate(ball, ballSpawnPoint.transform.position, Quaternion.identity);
                BallManager.instance.mainBall = ballRef;
                ballRef.transform.position = ballSpawnPoint.transform.position;
                speedRef = ballRef.GetComponent<BallScript>().speed;
                //freeze the ball so it can be moved to where the player wants
                ballRef.GetComponent<BallScript>().speed = 0;
                held = true;
            }

            if (held)
            {
                //drag the ball to where the player wants
                ballRef.transform.position = ballSpawnPoint.transform.position;
            }

            if(Input.GetMouseButtonUp(0))
            {
                //release the ball, one of the setting transform.positions probably isn't needed but just in case :p
                ballRef.transform.position = ballSpawnPoint.transform.position;
                ballRef.GetComponent<Rigidbody2D>().velocity = Vector2.up *speedRef;
                held = false;

                //reset time scale so the player gets a better "reset"
                Time.timeScale = 1f;
                
                //start the game and start tracking time
                UIManager.instance.DisableIntro();
                UIManager.instance.firstBallSpawned = true;
            }
        }
        
    }

    
    public void Enlarge(float scale)
    {
        //Powerup, makes the paddle bigger after picking it up
        this.transform.localScale = new Vector3(transform.localScale.x + scale,
            transform.localScale.y, transform.localScale.z);
    }
}
