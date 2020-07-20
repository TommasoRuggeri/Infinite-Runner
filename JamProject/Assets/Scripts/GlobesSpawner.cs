using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobesSpawner : MonoBehaviour
{
    List<Transform> globes;
    // Start is called before the first frame update
    void Start()
    {
        globes = new List<Transform>();

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.CompareTag("Token"))
            {
                globes.Add(child);
            }
        }
    }

    public void RandomSpawn()
    {
        int index = Random.Range(0, globes.Count);
        //Debug.Log(index);
        globes[index].gameObject.SetActive(true);
    }
}
