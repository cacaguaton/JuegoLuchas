using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public enum ComboState
{
    NONE,
    PUCH_1,
    PUNCH_2,
    PUNCH_3,
    KICK_1,
    KICK_2
}
public class PlayerAttack : MonoBehaviour
{
    private CharacterAnimation player_Anim;

    private bool activateTimerToReset;

    private float default_Combo_Timer = 0.4f;
    private float current_Combo_Timer;

    private ComboState current_Combo_State;

    void Awake()
    {
        player_Anim = GetComponentInChildren<CharacterAnimation>();
    }

    void Start()
    {
        current_Combo_Timer = default_Combo_Timer;
        current_Combo_State = ComboState.NONE;
        current_Combo_State = (ComboState)1;
    }

    // Update is called once per frame
    void Update()
    {
        ComboAttacks();
    }

    void ComboAttacks()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            current_Combo_State++;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            player_Anim.Kick_1();
        }
    }
}
