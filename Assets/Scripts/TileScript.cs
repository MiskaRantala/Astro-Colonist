using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    public bool IsEmpty { get; private set; }

    // Colors for hovering tiles depending if the tile is available
    private Color32 fullColor = new Color32(255, 118, 118, 255);
    private Color32 emptyColor = new Color32(96, 255, 90, 255);

    private SpriteRenderer spriteRenderer;

    public bool Walkable { get; set; }

    public bool Debugging { get; set; }

    // Use this for initialization
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        Walkable = true;
        IsEmpty = true;
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
    }

    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedBtn != null)
        {
            if (IsEmpty && !Debugging)
            {
                ColorTile(emptyColor);
            }
            if (!IsEmpty && !Debugging)
            {
                ColorTile(fullColor);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
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

    private void PlaceTower()
    {
        GameObject tower = (GameObject) Instantiate(GameManager.Instance.ClickedBtn.TowerPrefab, transform.position, Quaternion.identity);
        tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

        // Create towers under the specific tile in unity hierarchy
        tower.transform.SetParent(transform);

        IsEmpty = false;
        ColorTile(Color.white);

        GameManager.Instance.buyTower();

        Walkable = false;
    }

    private void ColorTile(Color newColor)
    {
        spriteRenderer.color = newColor;
    }
}