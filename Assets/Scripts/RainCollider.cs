using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RainCollider : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public GameObject rain;
    private Quaternion b = new Quaternion(0, 0, 0, 0);


    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;
        while (i < numCollisionEvents)
        {
            Vector3 pos = collisionEvents[i].intersection;
            GameObject.Instantiate(rain, pos, b);
            i++;
        }
    }
}