using UnityEditor;
using System.Collections;
using UnityEngine;

public class TextureCounter : EditorWindow
{
    private string _stringValue;

    private Vector2 _scrollPosition;

    [MenuItem("Window/Textur Counter")]
    public static void Init()
    {
        var window = EditorWindow.GetWindow<TextureCounter>("Textur Counter");

        DontDestroyOnLoad(window);
    }

    private void OnGUI()
    {
        using (var verticalArea = new EditorGUILayout.VerticalScope())
        {
            var buttonClicked = GUILayout.Button("Click me!");
            if (buttonClicked)
                Debug.Log("Click");

            _stringValue = EditorGUILayout.TextField(_stringValue);
        }

        using (var scrollView = new EditorGUILayout.ScrollViewScope(_scrollPosition))
        {
            _scrollPosition = scrollView.scrollPosition;
            GUILayout.Label("These");
            GUILayout.Label("Labels");
            GUILayout.Label("Will be shown");
            GUILayout.Label("On top of each other");
        }
    }
}
