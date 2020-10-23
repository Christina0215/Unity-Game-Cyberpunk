using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cullingmask : MonoBehaviour
{
    Camera _camera;
    public int cullingmask;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        _camera.cullingMask = ~(1 << cullingmask);
    }
}
