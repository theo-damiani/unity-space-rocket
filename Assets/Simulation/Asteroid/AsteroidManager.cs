using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private AsteroidFactory factory;
    private Asteroid currentAsteroid;

    public void SpawnNewAsteroid()
    {
        // currentAsteroid = Asteroid.Spawn(prefab, transform);

        // Spawn object at parent coordinates.
        currentAsteroid = factory.GetRandom();
        currentAsteroid.transform.parent = transform;
        currentAsteroid.transform.localRotation = Quaternion.identity;
		currentAsteroid.transform.localScale = Vector3.one;
        currentAsteroid.InitAsteroid(transform);
        currentAsteroid.OnHitTarget += ReturnCurrentAsteroid;
        currentAsteroid.gameObject.SetActive(true);
    }

    public void LaunchCurrentAsteroid()
    {
        currentAsteroid.SetAimToTarget(true);
    }

    private void ReturnCurrentAsteroid()
    {
        factory.ReturnObject(currentAsteroid);
    }

    void OnDisable()
    {
        if (currentAsteroid)
        {
            currentAsteroid.OnHitTarget -= ReturnCurrentAsteroid;
        }
    }
}
