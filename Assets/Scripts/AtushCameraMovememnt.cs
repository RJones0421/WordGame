using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtushCameraMovememnt : MonoBehaviour
{
    // Update is called once per frame
    
    void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime*5), Space.World);
    }
}
