using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

    public float power = 0.2f;
    public float duration = 0.2f;
    public float slowDownAmount = 1f;
    private bool _shouldShake;
    private float initialDuration;

    private Vector3 startPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.localPosition;
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update()
    {
        Shake();
    }


    void Shake()
    {
        if(_shouldShake)
        {
            if (duration > 0f)
            {
                
            transform.localPosition = startPosition + Random.insideUnitSphere * power;
            duration -= Time.deltaTime * slowDownAmount;

            }
            else
            {
                _shouldShake = false;
                duration = initialDuration;
                transform.localPosition = startPosition;
            }
        }//if we should shake the camera
    }//Shake

    public bool should_Shake
    {
        get
        {
            return _shouldShake;
        }
        set
        {
            _shouldShake = value;
        }
    }
}
