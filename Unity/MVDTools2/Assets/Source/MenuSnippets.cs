using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class MenuSnippets
{
    // Method to setup textures properly when selected
    // I want to process all selected texture, set type normal
    [MenuItem("Assets/Process texture")]
    private static void ProcessTexture()
    {
        // Check if it is a texture
        if(Selection.activeObject.GetType() == typeof(Texture2D))
        {
            Texture2D tex = Selection.activeObject as Texture2D;
            string path = AssetDatabase.GetAssetPath(tex); // Give me the path of the asset.
            TextureImporter teximporter = AssetImporter.GetAtPath(path) as TextureImporter; // Get the texture importer from my asset
            {
                teximporter.textureType = TextureImporterType.NormalMap; // Setup the parameters
                teximporter.filterMode = FilterMode.Trilinear;
            }
            AssetDatabase.ImportAsset(path);
        }
        else
        {
            Debug.Log("This is not!!!");
        }
    }

    [MenuItem("CONTEXT/Transform/Randomize")]
    private static void RandomizeTransform(MenuCommand cmd)
    {
        Transform transobj = cmd.context as Transform;
        transobj.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
    }
}
