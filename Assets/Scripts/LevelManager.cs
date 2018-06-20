using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    
    // A prefab for creating a single tile
    [SerializeField]
    private GameObject tile;

    // Change these values to affect map size
    private int mapWidth = 15;
    private int mapHeight = 10;

    // Calculates the size and returns it as a float
    public float TileSize
    {
        get { return tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
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
        Vector3 worldStartPosition = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));

        for (int y = 0; y < mapHeight; y++) // map height
        {
            for (int x = 0; x < mapWidth; x++) // map width
            {
                PlaceTile(x, y, worldStartPosition);
            }
        }
    }

    // Places the tiles where we want
    private void PlaceTile(int x, int y, Vector3 worldStartPosition)
    {
        // Creates a new tile and makes a reference to that tile in the NewTile
        GameObject newTile = Instantiate(tile);

        // Uses the new tile variable to change the position of the tile
        newTile.transform.position = new Vector3(worldStartPosition.x + (TileSize * x), worldStartPosition.y - (TileSize * y), 0);
    }

}
