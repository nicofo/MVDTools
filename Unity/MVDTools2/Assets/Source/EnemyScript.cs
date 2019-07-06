using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Tooltip("This is the target A.K.A as player")]
    public Transform target;
    public float health;

    [HideInInspector]
    public int level;

    public float damage;

    [ContextMenuItem("Randomize range", "RandomRange")] // randomize range, calls RandomRange function
    public float range;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Reset()
    {
        
    }

    // When the component has suffered any changes
    // Method will be called
    void OnValidate()
    {
        
    }

    // Contextmenu for methods, do custom actions
    [ContextMenu("DOSOMETHING")]
    void DoSomething()
    {
        level = Random.Range(0, 100);
        damage = Random.Range(0, 100);
    }

    void RandomRange()
    {
        Debug.Log("randomizing range");
    }
}
