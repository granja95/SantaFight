using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace StarterAssets
{

    public class GameManager : MonoBehaviour
{
    public int enemiesAlive = 0;

    public int round = 0;

    public GameObject[] spawnPoints;

    public GameObject spawnPointPlayer;

    public GameObject enemyPrefab;
    public GameObject playerPrefab;

    public GameObject pauseMenu;

    public TextMeshProUGUI roundNum;
    //public TextMeshProUGUI roundsSurvived;
    public GameObject endScreen;
        public GameObject nextRoundUI;

        private bool entrou = false;

        public int damageEnemy = 5;
        public float speed = 2f;
    //public Animator blackScreenAnimator;

    // Start is called before the first frame update
    void Start()
    {
            //gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
            
        if (enemiesAlive == 0 )
        {
            if (round >= 0)
            {
                //nextRoundUI.SetActive(true);
                round++;

                damageEnemy = damageEnemy + 2;
                    speed = speed + speed * 0.2f;
                    FindObjectOfType<AIController>().speedRun = speed;
                FindObjectOfType<DamageCollider>().enemyDamage = damageEnemy;
                //FindObjectOfType<PlayerStats>().currentHealth = 100;
                //FindObjectOfType<PlayerStats>().healthbar.SetMaxHealth(FindObjectOfType<PlayerStats>().maxHealth);
                NextWave(round);
                roundNum.text = "Round: " + round.ToString();
            }
            else if (round == 0)
            {
                round++;
                NextWave(round);
                roundNum.text = "Round: " + round.ToString();
            }
            

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }


    public void NextWave(int round)
    {
        FindObjectOfType<AudioManager>().Play("Round");
        for (int i = 0; i < round; i++)
        {
            GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemySpawned = Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
            enemySpawned.GetComponent<EnemyStats>().gameManager = GetComponent<GameManager>();
            enemiesAlive++;
        }
        
    }

    IEnumerator ExampleCoroutine()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5);

        }

        public void EndGame()
    {
            //Time.timeScale = 0;
            //Cursor.lockState = CursorLockMode.None;
            //endScreen.SetActive(true);
            Debug.Log("FIM");
            //FindObjectOfType<AudioManager>().Play("PlayerDeath");

            Invoke("ReplayGame", 5f);
        //ReplayGame();
        //roundsSurvived.text = round.ToString();
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<ThirdPersonController>().enabled = true;
        FindObjectOfType<PlayerStats>().currentHealth = 100;
        //GameObject playerSpawn = Instantiate(playerPrefab, spawnPointPlayer.transform.position, Quaternion.identity);
        FindObjectOfType<PlayerStats>().alive();
        FindObjectOfType<PlayerStats>().isAlive = true;
         Time.timeScale = 1;
        round = 0;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        //blackScreenAnimator.SetTrigger("FadeIn");
        Invoke("LoadMainMenuScene", .4f);
    }

    void LoadMainMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        AudioListener.volume = 0;
    }

    public void UnPause()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.volume = 1;
    }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
