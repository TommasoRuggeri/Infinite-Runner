using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public TileSetManager tsManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.parent.gameObject == tsManager.LastObj)
        {
            tsManager.SpawnObject();
        }
    }
}
