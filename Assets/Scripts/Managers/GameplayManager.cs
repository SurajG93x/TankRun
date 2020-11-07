using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private Text scoreText;
    private int scoreVal;

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private Button shootBtn;

    private void Awake()
    {
        MakeInstance();
    }

    void Start()
    {
        halfGroundSize = GameObject.Find("GroundBlock Main").GetComponent<Ground>().halfLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();

        StartCoroutine("GenerateObstacles");

        scoreText = GameObject.Find("Score").GetComponent<Text>();
    }

    public void Update()
    {
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25))
            {
                Debug.Log(hit.collider.gameObject.name);
            }
        }
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

    public void IncreaseScore(int incVal)
    {
        scoreVal += incVal;
        scoreText.text = scoreVal.ToString();
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        shootBtn.gameObject.SetActive(false);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        shootBtn.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        shootBtn.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameoverPanel.SetActive(true);
        shootBtn.gameObject.SetActive(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
        shootBtn.gameObject.SetActive(true);
    }
}
