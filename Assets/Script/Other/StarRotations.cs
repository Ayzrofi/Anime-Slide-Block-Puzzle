using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarRotations : MonoBehaviour {
    [SerializeField]
    float speed;
    private void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}
