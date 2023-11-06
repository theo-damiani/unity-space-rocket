using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidFactory factory;
    [SerializeField] private ParticleSystem asteroidCollisionEffect;
    [SerializeField] private RectTransform spawnAsteroidButton;
    public FloatReference collisionSpeed;
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

        spawnAsteroidButton.gameObject.SetActive(false);
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
        // Play collision effect
        asteroidCollisionEffect.transform.position = currentAsteroid.transform.position;
        asteroidCollisionEffect.Play();

        // Add collision force to rocket
        rocketRigidbody.AddForce(currentAsteroid.GetVelocityDirection()*collisionSpeed.Value, ForceMode.VelocityChange);

        // Return asteroid to the pool
        factory.ReturnObject(currentAsteroid);
        currentAsteroid.OnHitTarget -= ReturnCurrentAsteroid;
        spawnAsteroidButton.gameObject.SetActive(true);
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
