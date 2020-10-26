using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeuse;
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount();
        if (timeuse > 5)
            WaterUp();
    }

    void TimeCount()
    {
        timeuse += Time.deltaTime;
    }
    void WaterUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + speed*Time.deltaTime, transform.position.z);
    }
}
