using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorPrefabLoader : EditorWindow
{
    public int width = 8;
    public float offset = 0;
    public Vector2Int maxGridSize = new Vector2Int(16, 16);

    private List<GameObject> instances;
    private string prefabsPath = "Assets/3DGamekit/Prefabs/Environment";

    private Object sourceMaterial;

    private GUIStyle editorStyle;

    // Add menu named "Prefab tile loader" to the Window menu
    [MenuItem("Tools/Prefab tile loader")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EditorPrefabLoader window = (EditorPrefabLoader)EditorWindow.GetWindow(typeof(EditorPrefabLoader));
        window.title = "Custom tools";
        window.Show();
    }

    // Method used to display the GUI of the menu window
    void OnGUI()
    {
        prefabsPath = EditorGUILayout.TextField("Prefabs path", prefabsPath);
        width = EditorGUILayout.IntField("Grid Width", width);
        offset = EditorGUILayout.FloatField("Grid Offset", offset);
        maxGridSize = EditorGUILayout.Vector2IntField("Maximum grid size", maxGridSize);

        editorStyle = new GUIStyle(GUI.skin.button);
        editorStyle.fontStyle = FontStyle.Bold;
        editorStyle.normal.textColor = Color.blue;

        // Button to trigger prefab placing
        if (GUILayout.Button("Build Tile sets", editorStyle))
        {
            instances = new List<GameObject>();

            RetrievePrefabs(prefabsPath);
            SpawnPrefabs();
        }
		
		sourceMaterial = EditorGUILayout.ObjectField(sourceMaterial, typeof(Object), true);
        if (GUILayout.Button("Update shaders"))
        {
            instances = new List<GameObject>();

            RetrievePrefabs(prefabsPath);
            foreach (GameObject instance in instances)
            {

                updateMaterial(instance);
            }
        }
    }

    public void updateMaterial(GameObject instance)
    {
		// Loop through all selected objects and change its material
    }
	
    public void SpawnPrefabs()
    {
        // Loop through instances list
        // Set instance position along given grid size.
    }

    public void RetrievePrefabs(string path)
    {
        string[] prefabs = AssetDatabase.FindAssets("", new[] { path });

        foreach (string assetGUID in prefabs)
        {
            string prefab_path = AssetDatabase.GUIDToAssetPath(assetGUID);

            // If its a folder, just call the function again with the new path.
            if (AssetDatabase.IsValidFolder(prefab_path))
            {
                RetrievePrefabs(prefab_path);
                Debug.Log("Iterating through folder: " + prefab_path);
                continue;
            }

            // Load the asset at a given path
            // save my prefab source asset into gameobject instances list

            // Discard those prefabs which are bigger than the range provided.

            // Discard also prefabs which don't have extents.
            // ALWAYS WARN THE USER IF A PREFAB IS NOT GOING TO BE DISPLAYED.

        }
    }

    // Get the bounding box of the whole prefab
    public Vector3 GetExtents(GameObject new_tile)
    {
        Vector3 extents = Vector3.zero;
        MeshRenderer renderer = new_tile.GetComponent<MeshRenderer>();

        if (renderer)
            extents = new_tile.GetComponent<MeshRenderer>().bounds.extents;

        // We need to iterate through its children to know the exact size.
        for (int i = 0; i < new_tile.transform.childCount; i++)
        {
            Vector3 ext = GetExtents(new_tile.transform.GetChild(i).gameObject);

            if (ext.x > extents.x) extents.x = ext.x;
            if (ext.y > extents.y) extents.y = ext.y;
            if (ext.z > extents.z) extents.z = ext.z;
        }

        return extents;
    }
}