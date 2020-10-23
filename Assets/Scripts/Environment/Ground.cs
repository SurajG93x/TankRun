using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{

    public Transform otherBlock;
    public float halfLength = 100f;

    private Transform player;
    private float endOfSet = 10f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveGround();
    }

    void MoveGround()
    {
        if (transform.position.z + halfLength < player.transform.position.z - endOfSet)
        {
            transform.position = new Vector3(otherBlock.position.x, otherBlock.position.y, otherBlock.position.z + halfLength * 2);
        }
    }
}
