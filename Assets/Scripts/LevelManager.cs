using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    /// <summary>
    /// A prefab for creating a single tile
    /// </summary>
    [SerializeField]
    private GameObject tile;

    /// <summary>
    /// Calculates the size and returns it as a float
    /// </summary>
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

    /// <summary>
    /// Creates our level
    /// </summary>
    private void CreateLevel()
    {

        for (int x = 0; x < 20; x++) //y position
        {
            for (int y = 0; y < 20; y++) //x position
            {
                PlaceTile(x, y);
            }
        }
    }

    /// <summary>
    /// Places the tiles where we want
    /// </summary>
    private void PlaceTile(int x, int y)
    {
        //Creates a new tile and makes a reference to that tile in the NewTile
        GameObject newTile = Instantiate(tile);

        //Uses the new tile variable to change the position of the tile
        newTile.transform.position = new Vector3(TileSize * x, TileSize * y, 0);
    }

}
