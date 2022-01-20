using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
    [SerializeField] private Material _selectMaterial;
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Camera _camera;

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if (Physics.Raycast(ray, out rayHit))
        {
            var selection = rayHit.transform;
            var selectionRenderer = selection.GetComponent<Renderer>();

            if (selectionRenderer != null)
            {
                selectionRenderer.material = _selectMaterial;
            }
            
        }

    }
}
