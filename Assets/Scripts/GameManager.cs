using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    private int currency;

    private int wave = 0;

    private int lives;

    private bool gameOver = false;

    private int health = 15;

    [SerializeField]
    private Text livesText;

    [SerializeField]
    private Text waveText;

    [SerializeField]
    private Text currencyTxt;

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject GameOverMenu;

    private List<Monster> activeMonsters = new List<Monster>();

    public ObjectPool Pool { get; set; }

    private Tower selectedTower;

    public bool WaveActive
    {
        get
        {
            return activeMonsters.Count > 0;
        }
    }

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            currency = value;
            currencyTxt.text = "  " + value.ToString();
        }
    }

    public int Lives
    {
        get 
        { 
            return lives; 
        }
        set 
        {
            this.lives = value;

            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }

            livesText.text = lives.ToString();
        }
    }
    
    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Use this for initialization
    void Start ()
    {
        Currency = 50;
        Lives = 10;
        GameOverMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleEscape();
	}

    public void pickTower(TowerBtn towerBtn)
    {
        if (Currency >= towerBtn.Price && !WaveActive)
        {
            this.ClickedBtn = towerBtn;
            Hover.Instance.Activate(towerBtn.Sprite);
        }
    }

    public void buyTower()
    {
        if (Currency >= ClickedBtn.Price)
        {
            // Reduces currency according to the tower's price
            Currency -= ClickedBtn.Price;

            // Remove hover icon
            Hover.Instance.Deactivate();

            ClickedBtn = null;
        }
    }

    public void SelectTower(Tower tower)
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = tower;
        selectedTower.Select();
    }

    public void DeselectTower()
    {
        if (selectedTower != null)
        {
            selectedTower.Select();
        }

        selectedTower = null;
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Hover.Instance.Deactivate();
        }
    }

    public void StartWave()
    {
        wave++;

        waveText.text = string.Format("Wave: <color=yellow>{0}</color>", wave);

        StartCoroutine(SpawnWave());

        waveBtn.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        LevelManager.Instance.GeneratePath();

        for (int i = 0; i < wave; i++)
        {
            int monsterIndex = Random.Range(0, 3);

            string type = string.Empty;

            switch (monsterIndex)
            {
                case 0:
                    type = "HealthBlueMonster";
                    break;
                case 1:
                    type = "HealthOrangeMonster";
                    break;
                case 2:
                    type = "HealthGreenMonster";
                    break;
            }

            Monster monster = Pool.GetObject(type).GetComponent<Monster>();
            monster.Spawn(health);

            if (wave % 3 == 0)
            {
                health += 5;
            }

            activeMonsters.Add(monster);

            yield return new WaitForSeconds(2.5f);
        }
    }

    public void RemoveMonster(Monster monster)
    {
        activeMonsters.Remove(monster);

        if (!WaveActive && !gameOver)
        {
            waveBtn.SetActive(true);
        }
    }

    public void GameOver() 
    {
        if (!gameOver)
        {
            gameOver = true;
            GameOverMenu.SetActive(true);
        }   
    }

    public void Restart() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}