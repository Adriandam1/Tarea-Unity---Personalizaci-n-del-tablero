using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase hace que el objeto rote las armas horizontalmente.
public class RotatorArmas : MonoBehaviour
{

    void Update()
    {
        transform.Rotate (new Vector3 (0, 90, 0) * Time.deltaTime);
        
    }
}
