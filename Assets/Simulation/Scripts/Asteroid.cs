using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public float speed = 8.5f;
    public float radius = 1f; // Collision radius.
    float radiusSq;
    Transform target;
    bool aimTarget;

    void OnEnable() {
        radiusSq = radius * radius;
    }

    public static Asteroid Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform target) {
        // Spawn object at parent coordinates.
        GameObject go = Instantiate(prefab, target, false);
        // add offset
        go.transform.SetLocalPositionAndRotation(position, rotation);
        Asteroid a = go.GetComponent<Asteroid>();

        if (!a) 
            a = go.AddComponent<Asteroid>();

        // add target
        a.aimTarget = false;
        a.target = target;
        return a;
    }
    
    void LateUpdate() {
        if (!aimTarget) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, target.transform.position.z-Camera.main.transform.position.z)
            );
            mousePosition.z = target.transform.position.z;
            transform.position = mousePosition;
            return;
        }

        // Move towards the target
        Vector3 direction = target.position - transform.position;
        transform.Translate(speed * Time.deltaTime * direction.normalized, Space.World);

        if ( direction.sqrMagnitude < radiusSq ) {
            // To close to target destroy asteroid.
            Destroy(gameObject);
            // Write your own code to spawn an explosion / splat effect.
        } 
    }

    public void SetAimToTarget(bool value)
    {
        aimTarget = value;
    }
}
