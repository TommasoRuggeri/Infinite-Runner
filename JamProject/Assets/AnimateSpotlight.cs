using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSpotlight : MonoBehaviour
{
    [SerializeField] Animation animationComponent;

    public void PlayAnimation()
    {
        animationComponent.Play();
    }

    public void StopAnimation()
    {
        animationComponent.Stop();
    }
}
