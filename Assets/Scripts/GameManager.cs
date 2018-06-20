using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    /// <summary>
    /// A property for the towerBtn
    /// </summary>
    public TurretBtn ClickedBtn { get; set; }

    /// <summary>
    /// A reference to the currency text
    /// </summary>
    private int currency;

    private int wave = 0;

    private int lives;

    private bool gameOver = false;

    [SerializeField]
    private Text livesTxt;

    [SerializeField]
    private Text waveTxt;

    [SerializeField]
    private Text currencyTxt;

    [SerializeField]
    private GameObject waveBtn;

    [SerializeField]
    private GameObject gameOverMenu;

    /// <summary>
    /// The current selected turret
    /// </summary>
    private Turret selectedTurret;

    private List<Enemy> activeEnemies = new List<Enemy>();

    /// <summary>
    /// A property for the object pool
    /// </summary>
    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get
        {
            return activeEnemies.Count > 0;
        }
    }

    /// <summary>
    /// Property for accessing the currency
    /// </summary>
    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            this.currency = value;
            this.currencyTxt.text = value.ToString() + "<color=lime>$</color>";
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
            livesTxt.text = lives.ToString();
        }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

	// Use this for initialization
	void Start ()
    {
        Lives = 10;
        Currency = 100;
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleEscape();
	}

    /// <summary>
    /// Pick a turret then a buy button is pressed
    /// </summary>
    /// <param name="turretBtn">The clicked button</param>
    public void PickTurret(TurretBtn turretBtn)
    {
        if (Currency >= turretBtn.Price && !WaveActive)
        {
            //Stores the clicked button
            this.ClickedBtn = turretBtn;

            //Activates the hover icon
            Hover.Instance.Activate(turretBtn.Sprite);
        }
    }

    /// <summary>
    /// Buys a turret
    /// </summary>
    public void BuyTurret()
    {
        if (Currency >= ClickedBtn.Price)
        {
            Currency -= ClickedBtn.Price);
            Hover.Instance.Deactivate();
        }
    }

    public void SelectTurret(Turret turret)
    {
        if (selectedTurret != null)
        {
            selectedTurret.Select();
        }

        selectedTurret = turret;
        selectedTurret.Select();
    }

    public void DeselectTurret()
    {
        if (selectedTurret != null)
        {
            selectedTurret.Select();
        }

        selectedTurret = null;
    }

    /// <summary>
    /// Handles escape presses
    /// </summary>
    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //if we press escape 
        {
            //Deactivate the hover instance
            Hover.Instance.Deactivate();
        }
    }




}
