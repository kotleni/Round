using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
    private const float RotationSpeed = 0.7f;
    private float _rotation = 0f;

    void Update()
    {
        _rotation += RotationSpeed;
        if(_rotation >= 360f) _rotation = 0f;

        transform.Rotate(new Vector3(0, 0, _rotation));
    }
}
