using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public TowerBtn ClickedBtn { get; set; }

    private int currency;

    [SerializeField]
    private Text currencyTxt;

    public int Currency
    {
        get
        {
            return currency;
        }

        set
        {
            currency = value;
            currencyTxt.text = "  " + value.ToString() + " <color=yellow>$</color>";
        }
    }
    
    // Use this for initialization
    void Start ()
    {
        Currency = 5;
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleEscape();
	}

    public void pickTower(TowerBtn towerBtn)
    {
        if (Currency >= towerBtn.Price)
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

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Hover.Instance.Deactivate();
        }
    }
}