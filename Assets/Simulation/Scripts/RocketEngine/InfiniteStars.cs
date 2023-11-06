using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteStars : MonoBehaviour {
 
 
    private Transform tx;
    private ParticleSystem.Particle[] points;
 
    public int starsMax = 100;
    public float starSize = 1;
    public float starMaxDistance = 10;
    public float starMaxSpawnSize = 1;
    public float starClipDistance = 1;
    private float starMaxDistanceSqr;
    private float starMaxSpawnSizeSqr;
    private float starClipDistanceSqr;
    private float starSpawnDistance;

    private Rigidbody parentRigidbody;
 
 
    void Start () {
        tx = transform;
        starMaxDistanceSqr = starMaxDistance * starMaxDistance;
        starMaxSpawnSizeSqr = starMaxSpawnSize * starMaxSpawnSize;
        starClipDistanceSqr = starClipDistance * starClipDistance;

        starSpawnDistance = Mathf.Sqrt(starMaxDistanceSqr - starMaxSpawnSizeSqr);

        parentRigidbody = GetComponentInParent<Rigidbody>();
    }
 
 
    private void CreateStarsInSphere() {
        points = new ParticleSystem.Particle[starsMax];
 
        for (int i = 0; i < starsMax; i++) {
            points[i].position = Random.insideUnitSphere * starMaxDistance + tx.position;
            points[i].startColor = new Color(1,1,1, 1);
            points[i].startSize = starSize;
        }
    }

    private void CreateStarsInPlane() {
        Vector3 velocity = parentRigidbody.velocity;

        points = new ParticleSystem.Particle[starsMax];
 
        for (int i = 0; i < starsMax; i++) {
            Vector2 plane = Random.insideUnitCircle;
            Vector3 plane3 = new Vector3(plane.x, plane.y, 0);
            
            points[i].position = RandomTangent(velocity) * Random.Range(0f, starMaxSpawnSize) + tx.position;

            points[i].startColor = new Color(255,1,1, 1);
            points[i].startSize = starSize;
        }
    }
 
 
    // Update is called once per frame
    void Update () {

        if( points == null ) 
        {
            CreateStarsInSphere();
        }
 
        for (int i = 0; i < starsMax; i++) {
 
            if((points[i].position - tx.position).sqrMagnitude > starMaxDistanceSqr) 
            {
                points[i].position = Random.insideUnitSphere.normalized * starMaxDistance + tx.position;
                // Vector3 velocity = GetCurrentVelocity();
                // points[i].position = RandomTangent(velocity) * Random.Range(0f, starMaxSpawnSize) + tx.position + (velocity.normalized * starSpawnDistance);
            }
 
            if ((points[i].position - tx.position).sqrMagnitude <= starClipDistanceSqr) 
            {
                float percent = (points[i].position - tx.position).sqrMagnitude / starClipDistanceSqr;
                points[i].startColor = new Color(1,1,1, percent);
                points[i].startSize = percent * starSize;
            }
 
 
        }
 
 
 
 
        GetComponent<ParticleSystem>().SetParticles ( points, points.Length );
 
    }

    Vector3 RandomTangent(Vector3 vector) {
        return Quaternion.FromToRotation(Vector3.forward, vector) * (Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.forward) * Vector3.right);
    }

    Vector3 GetCurrentVelocity()
    {
        return parentRigidbody.velocity;
    }
}
