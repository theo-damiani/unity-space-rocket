using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    private Asteroid currentAsteroid;

    public void SpawnNewAsteroid()
    {
        currentAsteroid = Asteroid.Spawn(prefab, new Vector3(4,4,0), Quaternion.identity, transform);
    }

    public void LaunchCurrentAsteroid()
    {
        currentAsteroid.SetAimToTarget(true);
    }
}
