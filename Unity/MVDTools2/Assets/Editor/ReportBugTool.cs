using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public struct Report
{
    public string objectName;
    public string componentName;
    public string scene;
    public string title;
    public string description;


    public Report(string objName, string compName, string sceneName)
    {
        objectName = objName;
        componentName = compName;
        scene = sceneName;
        description = "\n\n\n";
        title = "";
    }
}

public class ReportBugEditor : EditorWindow
{
    // Editor version
    public static string version = "0.1a";

    public static Object contex;

    public static Report currentReport;

    [MenuItem("CONTEXT/Component/Report bug!!")]
    static void Init(MenuCommand cmd)
    {
        contex = cmd.context;
        // Get existing open window or if none, make a new one:
        ReportBugEditor window = (ReportBugEditor)EditorWindow.GetWindow(typeof(ReportBugEditor));
        window.Show();
        currentReport = new Report(cmd.context.name, cmd.context.GetType().ToString(), "");

    }

    void OnDestroy()
    {
    }




    // Main UI function to display the window
    void OnGUI()
    {
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Report bug", EditorStyles.largeLabel);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // name
            GUILayout.BeginHorizontal();
            GUILayout.Label("Object:", EditorStyles.boldLabel);
            GUILayout.Label(currentReport.objectName);
            GUILayout.EndHorizontal();

            // component
            GUILayout.BeginHorizontal();
            GUILayout.Label("Component:", EditorStyles.boldLabel);
            GUILayout.Label(currentReport.componentName);
            GUILayout.EndHorizontal();
        }
        DisplaySeparator(50);
        {
            // title
            currentReport.title = EditorGUILayout.TextField("Title", currentReport.title);

            // description
            GUILayout.Label("Description");
            currentReport.description = EditorGUILayout.TextArea(currentReport.description);
            GUILayout.Space(3);

            if (GUILayout.Button("Report"))
            {
                Debug.Log(currentReport.title);
                Debug.Log(Path.Combine(Application.dataPath, "Bugs"));
                Debug.Log(System.DateTime.Now.ToLongDateString() + "-" + System.DateTime.Now.ToLongTimeString());
                WriteBug();
                this.Close();
            }
        }
    }

    static void DisplaySeparator(int width)
    {
        string line = string.Empty;
        for (int i = 0; i < width; i++) line += "_";

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField(line);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }
    static void WriteBug()
    {

        //File.WriteAllText(Applicati, "dsfmdsklgnjslfgfldskngdlfskgn");
        Directory.CreateDirectory(" Bugs");
        string path = "Bugs/" + System.DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ".json";


        // If the directory already exists, this method does nothing.
        string[] content =
        {
            "{",
            "\"object\": \"" + currentReport.objectName + "\",",
            "\"component\": \"" + currentReport.componentName + "\",",
            "\"title\": \"" + currentReport.title + "\",",
            "\"description\": \"" + currentReport.description.Replace("\n", "\\n") + "\"",
            "}"
        };
        File.WriteAllLines(path, content);
        
        
    }
}