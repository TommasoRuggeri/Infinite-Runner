using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Token : MonoBehaviour
{
    [SerializeField] float energyGiven;

    int layerToCheck;

    private void Start()
    {
        layerToCheck = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerToCheck) return;
        if (ScreenHoleController.AddEnergy(energyGiven))
            Destroy(gameObject);
    }
}
