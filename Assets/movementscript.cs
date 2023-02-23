using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementscript : MonoBehaviour
{
    public float movespeed = 3;
    [HideInInspector] public Vector3 dir;
    float hzInput, VInput;
    CharacterController controller;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    Vector3 spherePos;


    [SerializeField] float gravity  = -9.81f;
    Vector3 Velocity;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionAndMove();
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        VInput = Input.GetAxis("Vertical");
        dir = transform.forward * VInput + transform.right * hzInput;
        controller.Move(dir * movespeed * Time.deltaTime);
    }

    bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y - groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask)) return true;
        return false;
    }


    void Gravity() {
        if (!IsGrounded()) Velocity.y += gravity * Time.deltaTime;
        else if (Velocity.y < 0) Velocity.y = -2;

        controller.Move(Velocity * Time.deltaTime);
            }


    private void onDrawGizmozs()

    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, controller.radius - 0.05f);
    }
}
