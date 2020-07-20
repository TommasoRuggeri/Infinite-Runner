using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObj : MonoBehaviour
{
    public TileSetManager tsManager;

    int layerToCheck;

    private void Start()
    {
        layerToCheck = LayerMask.NameToLayer("Environment");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerToCheck) return;
        tsManager.RemoveObjFromList(other.transform.parent.gameObject);
        Destroy(other.transform.parent.gameObject);
    }
}
