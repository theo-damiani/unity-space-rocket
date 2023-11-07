using System;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 8.5f;
    public float radius = 1f; // Collision radius.
    public event Action OnHitTarget;
    [SerializeField] private Transform asteroidMesh; 
    [SerializeField] private Vector vector;
    float radiusSq;
    Transform target;
    bool aimTarget;

    void OnEnable() {
        radiusSq = radius * radius;
    }

    public void InitAsteroid(Transform target)
    {
        // add target
        aimTarget = false;
        this.target = target;

        // config asteroid's mesh
        asteroidMesh.transform.SetLocalPositionAndRotation(Vector3.zero, UnityEngine.Random.rotation);
		asteroidMesh.transform.localScale = Vector3.one * UnityEngine.Random.Range(0.15f, 0.35f);

        // config vector
        vector.transform.rotation = Quaternion.identity;
        Vector3 direction = target.position - transform.position;
        Vector3 directionNorm = direction.normalized;
        DrawDirectionVector(directionNorm);
    }
    
    void LateUpdate() {
        if (!aimTarget) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, target.transform.position.z-Camera.main.transform.position.z)
            );
            mousePosition.z = target.transform.position.z;
            transform.position = mousePosition;

            Vector3 dirNorm = GetVelocityDirection();
            DrawDirectionVector(dirNorm);
            return;
        }

        // Move towards the target
        Vector3 direction = target.position - transform.position;

        Vector3 directionNorm = direction.normalized;
        DrawDirectionVector(directionNorm);
        transform.Translate(speed * Time.deltaTime * directionNorm, Space.World);

        asteroidMesh.Rotate(Vector3.one*0.1f);

        if ( direction.sqrMagnitude < radiusSq ) {
            // To close to target destroy asteroid.
            // Destroy(gameObject);
            OnHitTarget?.Invoke();
        } 
    }

    public Vector3 GetVelocityDirection()
    {
        if (target)
        {
            Vector3 direction = target.position - transform.position;
            return direction.normalized;
        }
        return Vector3.zero;
    }

    private void DrawDirectionVector(Vector3 directionNormalized)
    {
        vector.transform.localPosition = directionNormalized;
        vector.components.Value = speed * 0.1f * directionNormalized;
        vector.Redraw();
    }

    public void SetAimToTarget(bool value)
    {
        aimTarget = value;
    }
}
