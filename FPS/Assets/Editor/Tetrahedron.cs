using UnityEditor;
using UnityEngine;

public class Tetrahedron : ScriptableWizard
{
    [SerializeField] private Vector3 _size = new Vector3(1, 1, 1);

    [MenuItem("GameObject/3D Object/Tetrahedron")]
    static void ShowWizard()
    {
        DisplayWizard<Tetrahedron>("Create Tetrahedron", "Create");
    }

    private void OnWizardCreate()
    {
        Mesh mesh = new Mesh();

        Vector3 point0 = new Vector3(0, 0, 0);
        Vector3 point1 = new Vector3(1, 0, 0);
        Vector3 point2 = new Vector3(0.5f, 0, Mathf.Sqrt(0.75f));
        Vector3 point3 = new Vector3(0.5f, Mathf.Sqrt(0.75f), Mathf.Sqrt(0.75f) / 3);

        point0.Scale(_size);
        point1.Scale(_size);
        point2.Scale(_size);
        point3.Scale(_size);

        //Передать список вершин
        mesh.vertices = new Vector3[] { point0, point1, point2, point3 };

        //Передать список треугольников, связанных вершинами
        mesh.triangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,
            2, 1, 3,
            0, 3, 1
        };

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        var gameObject = new GameObject("Tetrahedron");
        var meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        var meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));
    }

    private void OnWizardUpdate()
    {
        if (_size.x <= 0 || _size.y <= 0 || _size.z <= 0)
        {
            isValid = false;

            errorString = "Size cannot be less than zero";
        }
        else
        {
            errorString = null;
            isValid = true;
        }    
    }
}
