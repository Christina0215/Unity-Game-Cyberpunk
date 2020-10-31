using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeuse;
    public float speed;
    public Text Text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeCount();
        if (timeuse > 3)
            Text.text = "逃离这里！/抓住他！";
            if (timeuse > 5)
        {
            WaterUp();
            Text.text = "";
        }
            
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
