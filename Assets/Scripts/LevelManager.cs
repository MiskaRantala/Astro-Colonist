using UnityEngine;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager> {

    
    // A prefab for creating a single tile
    [SerializeField]
    private GameObject tile;

    [SerializeField]
    private Transform map;

    // Change these values to affect map size
    private int mapWidth = 15;
    private int mapHeight = 10;

    private Point mapSize;

    private Stack<Node> path;
    
    public Stack<Node> Path
    {
        get
        {
            if (path == null)
            {
                GeneratePath();
            }

            return new Stack<Node>(new Stack<Node>(path));
        }
    }

    [SerializeField]
    private GameObject portalPrefab;

    [SerializeField]
    private GameObject endPortalPrefab;

    private Point blueSpawn;
    private Point redSpawn;

    public Portal SpawnPortal { get; set; }

    public Dictionary<Point, TileScript> Tiles { get; set; } 

    // Calculates the size and returns it as a float
    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    public Point BlueSpawn
    {
        get
        {
            return blueSpawn;
        }
    }

    // Use this for initialization
    void Start ()
    {
        CreateLevel();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Creates our level
    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        mapSize = new Point(mapWidth, mapHeight);

        Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapHeight; y++) // map height
        {
            for (int x = 0; x < mapWidth; x++) // map width
            {
                PlaceTile(x, y, worldStartPosition);
            }
        }

        SpawnPortals();
    }

    // Places the tiles where we want
    private void PlaceTile(int x, int y, Vector3 worldStartPosition)
    {
        // Creates a new tile and makes a reference to that tile in the NewTile
        TileScript newTile = Instantiate(tile).GetComponent<TileScript>();

        // Uses the new tile variable to change the position of the tile
        newTile.GetComponent<TileScript>().Setup(new Point(x, y), new Vector3(worldStartPosition.x + (TileSize * x), worldStartPosition.y - (TileSize * y), 0), map);

        Tiles.Add(new Point(x, y), newTile);
    }

    public bool InBounds(Point position)
    {
        return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
    }

    private void SpawnPortals()
    {
        blueSpawn = new Point(0, 0);
        // portalPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;
        GameObject tmp = (GameObject) Instantiate(portalPrefab, Tiles[BlueSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
        SpawnPortal = tmp.GetComponent<Portal>();
        SpawnPortal.name = "SpawnPortal";

        redSpawn = new Point(14, 9);
        // endPortalPrefab.GetComponent<SpriteRenderer>().sortingOrder = 1;
        Instantiate(endPortalPrefab, Tiles[redSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

    public void GeneratePath()
    {
        path = AStar.GetPath(BlueSpawn, redSpawn);
    }
}
