using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{
    [SerializeField] float raycastDistance;
    [SerializeField] CharacterController cc;
    [SerializeField] GameObject explosion;

    int layerToCheck;

    private void Start()
    {
        layerToCheck = LayerMask.GetMask("Environment");
    }

    void Update()
    {
        if (Physics.Raycast(cc.transform.position, Vector3.right, raycastDistance, layerToCheck))
        {
            // Do shit;
            Instantiate(explosion, transform.position, transform.rotation);
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }
}