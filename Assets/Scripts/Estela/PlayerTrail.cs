using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int maxPositions = 20;
    private float distanceThreshold = 0.2f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
    }

    void Update()
    {
        if (Vector3.Distance(lineRenderer.GetPosition(lineRenderer.positionCount - 1), transform.position) > distanceThreshold)
        {
            if (lineRenderer.positionCount < maxPositions)
            {
                lineRenderer.positionCount++;
            }
            lineRenderer.SetPosition(lineRenderer.positionCount - 1, transform.position);
        }
    }
}
