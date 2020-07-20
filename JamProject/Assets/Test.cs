using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AnimateSpotlight spotlight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            source.PlayOneShot(source.clip);
            spotlight.PlayAnimation();
        }
    }
}
