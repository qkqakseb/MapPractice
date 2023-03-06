using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainControler : MonoBehaviour
{
    private const string TILE_FRONT_RENDERER_OBJ_NAME = "FrontRenderer";

    private TerrainType terrainType = TerrainType.NONE;

    public bool IsPassable { get; private set; } = false;
    public int TileIdx1D { get; private set; } = -1;
    public Vector2Int TileIdx2D { get; private set; } = default;

    private SpriteRenderer frontRenderer = default;
    private Color defaultColor = default;
    private Color selectedColor = default;
    private Color searchColor = default;
    private Color inactiveColor = default;

    private void Awake()
    {

    }

}
