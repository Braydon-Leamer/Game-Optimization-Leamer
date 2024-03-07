using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public static BallManager instance;

    public GameObject mainBall;
    private void Awake()
    {
        instance = this;
    }
    public void DeleteChildren()
    {
        //Delete all of the spawned game objects that have been created
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
            
        }
    }

    public void SpeedUp()
    {
        //Speed up the game (literally)
        Time.timeScale += .25f;
    }
}
