using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Player
{
    P1 = 1,
    P2 = -1
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] KeyCode jumpKey;
    [SerializeField] CharacterController cc;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpGravityMult;
    [SerializeField] float fallGravityMult;
    [SerializeField] Animator anim;
    [SerializeField] float raycastDistance;

    Vector3 movement;
    
    float gravity = 9.81f;

    RaycastHit hitInfo;
    bool isGrounded = false;
    int animGrounded;
    LayerMask mask;
    
    // Start is called before the first frame update
    void Start()
    {
        animGrounded = Animator.StringToHash("IsGrounded");
        isGrounded = true;
        mask = LayerMask.NameToLayer("Environment");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            movement.y = jumpForce * (int)player;
        }

        if (movement.y * (int)player <= 0)
        {
            movement.y -= gravity * fallGravityMult * (int)player * Time.deltaTime;
        }
        else if (movement.y * (int)player > 0 && !isGrounded)
        {
            movement.y -= gravity * jumpGravityMult * (int)player * Time.deltaTime;
        }

        if (!isGrounded)
        {
            anim.SetBool(animGrounded, false);
        }
        else
        {
            anim.SetBool(animGrounded, true);
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down * (int)player, out hitInfo, raycastDistance);

        cc.Move(movement * Time.fixedDeltaTime);
    }

    public static void SwitchWith(PlayerController player1, PlayerController player2)
    {
        Player p = player1.player;
        Vector3 mov = player1.movement;
        Vector3 pos = player1.transform.position;
        Quaternion rot = player1.transform.rotation;

        p = player1.player;
        mov = player1.movement;
        player1.cc.enabled = player2.cc.enabled = false;

        player1.player = player2.player;
        player1.movement = player2.movement;
        player1.transform.position = player2.transform.position;
        player1.transform.rotation = player2.transform.rotation;

        player2.player = p;
        player2.movement = mov;
        player2.transform.position = pos;
        player2.transform.rotation = rot;

        player1.cc.enabled = player2.cc.enabled = true;
    }
}