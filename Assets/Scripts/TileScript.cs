using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This script is used for all tiles in the game
/// </summary>
public class TileScript : MonoBehaviour
{
    /// <summary>
    /// The tiles grid position
    /// </summary>
    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; private set; }

    private Turret myTurret;

    /// <summary>
    /// If the tile already has a turret and is hovered with mouse
    /// </summary>
    private Color32 fullColor = new Color32(255, 188, 188, 255);

    /// <summary>
    /// If the tile is empty and is hovered with mouse
    /// </summary>
    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    private SpriteRenderer spriteRenderer;

    public bool Walkable { get; set }

    public bool Debugging { get; set }

    /// <summary>
    /// The tile's center world position
    /// </summary>
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x));
        }
    }

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    /// <summary>
    /// Sets up the tile, this is an alernative to a constructor
    /// </summary>
    /// <param name="gridPos">The tiles grid position</param>
    /// <param name="worldPos">The tiles world position</param>
    public void Setup(PointEffector2D gridPos, Vector3 worldPos, Transform parent)
    {
        Walkable = true;
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
        LevelManager.instance.Tiles.Add(gridPos, this);
    }

    /// <summary>
    /// Mouseover, this executed when the player mouse over tile
    /// </summary>
    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (IsEmpty && !Debugging) //green
            {
                ColorTile(emptyColor);
            }
            if (!IsEmpty && !Debugging) //red
            {
                ColorTile(fullColor);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTurret();
            }
        }
        else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn == null && Input.GetButtonDown(0))
        {
            if (myTurret != null)
            {
                GameManager.Instance.SelectTurret(myTurret);
            }
            else
            {
                GameManager.Instance.DeselectedTurret();
            }
        }
    }

    private void OnMouseExit()
    {
        if (!Debugging)
        {
            ColorTile(Color.white);
        }
    }

    /// <summary>
    /// Places a turret on the tile
    /// </summary>
    private void PlaceTurret()
    {
        // Creates the turret
        GameObject turret = (GameObject)Instantiate(GameManager.Instance.ClickedBtn.TurretPrefab, transform. ?????????)

        // Set the sorting layer order on the turret
        turret.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        // Sets the tile as transform parent to the turret
        turret.transform.SetParent(transform);

        this.myTurret = turret.transform.GetChild(0).GetComponent<Turret>();

        // Makes sure that it isn't empty
        IsEmpty = false;

        // Sets the color back to white
        ColorTile(Color.white);

        // Buys the tower
        GameManager.Instance.BuyTower();

        Walkable = false;
    }

    /// <summary>
    /// Sets the color on the tile
    /// </summary>
    /// <param name="newColor"></param>
    private void ColorTile(Color newColor)
    {
        // Sets the color on the tile
        spriteRenderer.color = newColor;
    }
}
