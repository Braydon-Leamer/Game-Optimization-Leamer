using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int size;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private Gradient gradient;

    [SerializeField] private Vector2Int xOffset;
    private void Awake()
    {
        //Generate a bunch of primitives to create the map
        for(int i =0; i < size.x; i++)
        {
            for(int k = 0; k < size.y; k++)
            {
                GameObject newBrick = Instantiate(brickPrefab, transform);
                newBrick.transform.position = transform.position + new Vector3
                    (((float)((size.x - 1)*0.5f-i) * spacing.x) - xOffset.x, k * spacing.y, 0);
                newBrick.GetComponent<SpriteRenderer>().color = gradient.Evaluate((float)k / (size.y - 1));
            }
        }
    }
}
