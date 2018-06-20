using UnityEngine;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    // Use this for initialization
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceTower();
        }
    }

    private void PlaceTower()
    {
        Instantiate(GameManager.Instance.TowerPrefab, transform.position, Quaternion.identity);
    }
}