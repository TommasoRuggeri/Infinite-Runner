using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchPlayers : MonoBehaviour
{
    [SerializeField] PlayerController player1;
    [SerializeField] PlayerController player2;
    [SerializeField] ScreenHoleController shc;
    [SerializeField] TileSetManager tsm;
    [SerializeField] Image whiteScreen;
    [SerializeField] float timeBeforeSwitch;
    [SerializeField] float timeBetweenSwitches;
    [SerializeField] float whiteScreenDuration;

    float switchCounter;
    float whiteScreenTimer;
    bool startSwitch = false;
    
    void Update()
    {
        float dt = Time.deltaTime;
        switchCounter += dt;

        if (switchCounter >= timeBetweenSwitches)
        {
            startSwitch = true;
        }
        else if (switchCounter >= timeBetweenSwitches - timeBeforeSwitch && tsm.SpawnStraightTiles == false)
        {
            tsm.SpawnStraightTiles = true;
        }

        if (startSwitch)
        {
            Color color = whiteScreen.color;
            shc.CloseHole = false;
            if (color.a >= 1)
            {
                whiteScreenTimer += dt;
                if (whiteScreenTimer >= whiteScreenDuration)
                {
                    PlayerController.SwitchWith(player1, player2);

                    color.a = whiteScreenTimer = switchCounter = 0f;
                    whiteScreen.color = color;
                    shc.CloseHole = true;
                    startSwitch = false;
                }
            }
            else
            {
                color.a += dt;
                whiteScreen.color = color;
            }
        }
    }
}
