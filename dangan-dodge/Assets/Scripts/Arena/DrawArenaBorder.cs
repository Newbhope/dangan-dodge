using UnityEngine;

public class DrawArenaBorder : MonoBehaviour
{

    void Start()
    {
        Bounds bounds = GetComponent<SpriteRenderer>().bounds;
        LineRenderer lineRenderer = GetComponent<LineRenderer>();

        Vector3 topLeft = transform.position - transform.right * bounds.extents.x + transform.up * bounds.extents.y;
        Vector3 topRight = transform.position + transform.right * bounds.extents.x + transform.up * bounds.extents.y;
        Vector3 bottomLeft = transform.position - transform.right * bounds.extents.x - transform.up * bounds.extents.y;
        Vector3 bottomRight = transform.position + transform.right * bounds.extents.x - transform.up * bounds.extents.y;

        Vector3[] corners = { topLeft, topRight, bottomRight, bottomLeft, topLeft };
        lineRenderer.SetPositions(corners);
    }
}
