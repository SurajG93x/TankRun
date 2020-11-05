using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static GameplayManager instance;

    public GameObject[] obstacles;
    public GameObject[] zombies;
    public GameObject[] lanes;

    public float minObstacleDelay = 10f, maxObsDelay = 40f;
    private float halfGroundSize;
    private GameController playerController;

    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<Ground>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

        StartCoroutine("GenerateObstacles");
    }

    // Update is called once per frame

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateObstacles()
    {
        float timer = Random.Range(minObstacleDelay, maxObsDelay) / playerController.speed.z;
        yield return new WaitForSeconds(timer);

        GenerateObstacles(playerController.gameObject.transform.position.z + halfGroundSize);

        StartCoroutine("GenerateObstacles");
    }

    void GenerateObstacles(float zPos)
    {
        int r = Random.Range(0, 10);

        if (0<=r && r < 7)
        {
            int obstaclelane = Random.Range(0, lanes.Length);
            AddObstacle(new Vector3(lanes[obstaclelane].transform.position.x, 0f, zPos), Random.Range(0,obstacles.Length));

            int zombielane = 0;

            if (obstaclelane == 0)
            {
                zombielane = Random.Range(0, 2) == 1 ? 1 : 2;
            }
            else if (obstaclelane == 1)
            {
                zombielane = Random.Range(0, 2) == 1 ? 0 : 2;
            }
            else if (obstaclelane == 2)
            {
                zombielane = Random.Range(0, 2) == 1 ? 1 : 0;
            }

            AddZombies(new Vector3(lanes[zombielane].transform.position.x, 0.15f, zPos));
        }
    }

    void AddObstacle(Vector3 pos, int type)
    {
        GameObject obstacle = Instantiate(obstacles[type], pos, Quaternion.identity);
        bool mirror = Random.Range(0, 2) == 1;

        switch (type)
        {
            case 0:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 1:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -20 : 20, 0f);
                break;
            case 2:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -1 : 1, 0f);
                break;
            case 3:
                obstacle.transform.rotation = Quaternion.Euler(0f, mirror ? -170 : 170, 0f);
                break;
        }

        obstacle.transform.position = pos;
    }

    void AddZombies(Vector3 pos)
    {
        int count = Random.Range(0, 3) + 1;

        for (int i = 0; i < count; i++)
        {
            Vector3 shift = new Vector3(Random.Range(-0.5f, 0.5f), 0f, Random.Range(1, 10) * i);
            Instantiate(zombies[Random.Range(0, zombies.Length)], pos + shift * i, Quaternion.identity);
        }
    }
}
