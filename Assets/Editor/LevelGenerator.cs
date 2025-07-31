using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class LevelGenerator : EditorWindow
{
    private int gridSize = 10;
    private int gridLength = 20;
    private GameObject tilePrefab;
    private Transform gridParent;
    private int sectionSpacing = 10;
    private List<GameObject> gridTiles;
    [MenuItem("Tools/Level Generator")]
    public static void ShowWindow()
    {
        GetWindow<LevelGenerator>("Level Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Grid Settings", EditorStyles.boldLabel);
        gridSize = EditorGUILayout.IntField("Grid Size", gridSize);
        GUILayout.Space(sectionSpacing);

        GUILayout.Label("Grid Length", EditorStyles.boldLabel);
        gridLength = EditorGUILayout.IntField("Grid Length", gridLength);
        GUILayout.Space(sectionSpacing);


        GUILayout.Label("Tile Prefab", EditorStyles.boldLabel);
        tilePrefab = (GameObject)EditorGUILayout.ObjectField("Tile Prefab", tilePrefab, typeof(GameObject), false);
        
        GUILayout.Space(sectionSpacing);
        GUILayout.Label("Grid Parent", EditorStyles.boldLabel);
        gridParent = (Transform) EditorGUILayout.ObjectField("Grid Parent", gridParent, typeof(Transform), true);

        



        GUILayout.Space(sectionSpacing);
        if (GUILayout.Button("Generate Grid"))
        {
            GenerateGrid();
        }

        GUILayout.Space(sectionSpacing);
        if (GUILayout.Button("Clear Grid"))
        {
              ClearGrid();
        }
    }
            

    private void GenerateGrid()
    {
        if(tilePrefab == null)
        {
            Debug.LogError("Tile Prefab is not set.");
            return;
        }
        gridTiles = new List<GameObject>() ;
        for (int length = 0; length < gridSize; length++)
        {
            for(int width = 0; width < gridLength; width++)
            {
                Vector3 position = new Vector3(length, 0, width);
                GameObject gridTile = ((GameObject)PrefabUtility.InstantiatePrefab(tilePrefab, gridParent));
                gridTile.transform.position=position;
                gridTiles.Add(gridTile);

            }
        }
        
    }

    private void ClearGrid()
    {
        
        
            foreach (GameObject tile in gridTiles)
            {
                
                
                    DestroyImmediate(tile);
                
            }
        
        
    }

}
