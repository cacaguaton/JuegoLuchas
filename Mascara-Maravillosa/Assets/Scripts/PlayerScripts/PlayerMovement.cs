using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    //ptivate PlayerAnimation player_Animation;
    private Rigidbody myBody;   //Aqui va el personaje

    public float walk_Speed = 3f;  //Velocidad de movimiento
    public float z_Speed = 1.5f;

    private float rotation_Y = 0f;
    private float rotation_Speed = 15f;



    void Awake()
    {

        myBody = GetComponent<Rigidbody>();
        //player_Anomation = GetComponentInChildren<PlayerAnimation>();

    }


    void Update()
    {

    }

    void FixedUpdate()
    {
        DetectMovement();
    }

    void DetectMovement()
    {
        myBody.linearVelocity = new Vector3(
            Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walk_Speed),
         myBody.linearVelocity.y,
         Input.GetAxisRaw(Axis.VERTICAL_AXIS) * (-z_Speed));


    }

    void RotatePlayer()
    {
        if (Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) > 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotation_Y), 0f);
        }
        else if (Input.GetAxisRaw(Axis.VERTICAL_AXIS) < 0)
        {
            transform.rotation = Quaternion.Euler(0f, -Mathf.Abs(rotation_Y), 0f);
        }
    }//ROTATION

}//class
