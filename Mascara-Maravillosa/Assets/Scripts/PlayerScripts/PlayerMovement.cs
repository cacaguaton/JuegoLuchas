using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //ptivate PlayerAnimation player_Animation;
    private Rigidbody myBody;   //Aqui va el personaje

    private float walk_Speed = 3f;  //Velocidad de movimiento
    public float z_Speed = 1.5f;

    private float rotation_Y = -90f;
    private float rotation_Speed = 15f;

    void Awake()
    {

        myBody = GetComponent<Rigidbody>();
        //player_Anomation = GetComponentInChildren<PlayerAnimation>();

    }


    void Update()
    {


    }
    
    void DetectMovement()
    {
        myBody.linearVelocity = new Vector3(Input.GetAxisRaw(Axis.HORIZONTAL_AXIS) * (-walk_Speed),
         myBody.linearVelocity.y, Input.GetAxisRaw(Axis.VERTICAL_AXIS)*(-z_Speed));
    }
}//class
