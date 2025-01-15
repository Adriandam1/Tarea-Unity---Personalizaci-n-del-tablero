using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase hace que el objeto rote sobre si mismom usado en cubitos
public class Rotator : MonoBehaviour
{

    void Update()
    {
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
        
    }
}
