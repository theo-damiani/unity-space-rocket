using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidFactory factory;
    public float collisionSpeed = 3;
    private Asteroid currentAsteroid;
    private Rigidbody rocketRigidbody;

    void Start()
    {
        rocketRigidbody = GetComponent<Rigidbody>();
    }

    public void SpawnNewAsteroid()
    {
        if (currentAsteroid!=null)
        {
            // You can spwan only one asteroid at a time.
            return;
        }

        // Spawn object at parent coordinates.
        currentAsteroid = factory.GetRandom();
        if (currentAsteroid==null)
        {
            return;
        }
        currentAsteroid.transform.parent = transform;
        currentAsteroid.transform.localRotation = Quaternion.identity;
		currentAsteroid.transform.localScale = Vector3.one;
        currentAsteroid.InitAsteroid(transform);
        currentAsteroid.OnHitTarget += ReturnCurrentAsteroid;
        currentAsteroid.gameObject.SetActive(true);
    }

    public void LaunchCurrentAsteroid()
    {
        if (currentAsteroid)
        {
            currentAsteroid.SetAimToTarget(true);
        }
    }

    private void ReturnCurrentAsteroid()
    {
        factory.ReturnObject(currentAsteroid);
        currentAsteroid.OnHitTarget -= ReturnCurrentAsteroid;
        rocketRigidbody.AddForce(currentAsteroid.GetVelocityDirection()*collisionSpeed, ForceMode.VelocityChange);
        currentAsteroid = null;
    }

    void OnDisable()
    {
        if (currentAsteroid)
        {
            currentAsteroid.OnHitTarget -= ReturnCurrentAsteroid;
        }
    }
}
