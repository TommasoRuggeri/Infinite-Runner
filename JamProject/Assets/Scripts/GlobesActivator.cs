using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobesActivator : MonoBehaviour
{
    float timer;
    float time_to_spawn_a_globe;
    public float MinTime, MaxTime;
    [SerializeField] TileSetManager TsManager;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        time_to_spawn_a_globe = Random.Range(MinTime, MaxTime);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= time_to_spawn_a_globe)
        {
            time_to_spawn_a_globe = Random.Range(MinTime, MaxTime);
            TsManager.LastObj.GetComponent<GlobesSpawner>().RandomSpawn();
            timer = 0;
        }
    }
}
