using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {

    public Tilemap normalTilemap;
    public Tilemap darkTilemap;
    public Tilemap blurredTilemap;
    public Tile darkTile;
    public Tile blurredTile;

	// Use this for initialization
	void Start () {
        darkTilemap.origin = blurredTilemap.origin = normalTilemap.origin;
        darkTilemap.size = blurredTilemap.size = normalTilemap.size;

        foreach( Vector3Int p in darkTilemap.cellBounds.allPositionsWithin)
        {
            darkTilemap.SetTile(p, darkTile);
        }

        foreach (Vector3Int q in blurredTilemap.cellBounds.allPositionsWithin)
        {
            blurredTilemap.SetTile(q, blurredTile);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
