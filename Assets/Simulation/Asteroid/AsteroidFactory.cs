using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidFactory : MonoBehaviour
{
    [SerializeField] private Asteroid[] prefabs;
    [SerializeField] private bool expandable = false;
    private int prefabsSize;

    private List<Asteroid> availableAsteroids;
    private List<Asteroid> usedAsteroids;
    private void Awake()
    {
        prefabsSize = prefabs.Length;
        availableAsteroids = new List<Asteroid>();
        usedAsteroids = new List<Asteroid>();

        for (int i = 0; i < prefabs.Length; i++)
        {
            CreateNewAsteroid(i);
        }
    }

    public Asteroid GetRandom () {

        if (availableAsteroids.Count == 0 && !expandable)
        {
            return null;
        }
        else if (availableAsteroids.Count == 0)
        {
            CreateNewAsteroid(Random.Range(0, prefabsSize));
        }

        int randomID = Random.Range(0, availableAsteroids.Count);
        Debug.Log("0 - " + availableAsteroids.Count + " : " + randomID);
        Asteroid a = availableAsteroids[randomID];
        availableAsteroids.RemoveAt(randomID);

        usedAsteroids.Add(a);

		return a;
	}


    public void ReturnObject(Asteroid asteroid)
    {
        asteroid.transform.parent = transform;
        asteroid.gameObject.SetActive(false);
        usedAsteroids.Remove(asteroid);
        availableAsteroids.Add(asteroid);
    }

    private void CreateNewAsteroid(int asteroidID)
    {
        Asteroid asteroid = Instantiate(prefabs[asteroidID]);
        asteroid.transform.parent = transform;
        asteroid.gameObject.SetActive(false);

        availableAsteroids.Add(asteroid);
    }
}
