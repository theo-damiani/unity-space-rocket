using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] private Material materialTrail;
    [SerializeField] private GameObject startPrefab;
    [SerializeField] private BoolReference showPath;
    private TrailRenderer trailRenderer;
    private GameObject startPoint;
    private bool isStartPointSet = false;
    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<TrailRenderer>(out trailRenderer))
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
        }

        trailRenderer.Clear();

        trailRenderer.material = materialTrail;

        // Trail Renderer On:
        trailRenderer.autodestruct = false;
        trailRenderer.enabled = showPath.Value;
        trailRenderer.time = float.PositiveInfinity;
        //trailRenderer.time = 5f;
        trailRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        AnimationCurve curve = new AnimationCurve();
        float curveWidth = 0.1f;
        curve.AddKey(0f, curveWidth);
        curve.AddKey(1f, curveWidth);
        trailRenderer.widthCurve = curve;

        trailRenderer.sortingOrder = 1;
    }

    void Update()
    {
       if (!isStartPointSet && trailRenderer)
        {
            if (trailRenderer.positionCount == 0)
            {
                return;
            }

            Vector3 startPosition = trailRenderer.GetPosition(trailRenderer.positionCount-1);
            startPoint = Instantiate(startPrefab);
            startPoint.transform.localPosition = startPosition;
            isStartPointSet = true;
        } 
    }

    public void OnUpdateShowPath()
    {
        SetPathRenderer(showPath.Value);
    }

    public void SetPathRenderer(bool enable)
    {
        ClearPath();
        if (trailRenderer)
        {
            trailRenderer.enabled = enable;
        }
    }

    public void ClearPath()
    {
        if (trailRenderer)
        {
            trailRenderer.Clear();
        }
        if (startPoint)
        {
            Destroy(startPoint);
            isStartPointSet = false;
        }
    }
}