using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRopeGrab : MonoBehaviour
{

    //Buat Swing
    [Header("Rope Swing")]
    HingeJoint2D hinge;
    Rigidbody2D rb;
    [SerializeField] LayerMask ropeLayer;
    Rigidbody2D attachedRope;
    Animator animator;
    PlayerMovement player;

    public Transform tanganKanan;
    public Transform tanganKiri;
    public Transform tali;
    bool isHang = false;
    //Rigidbody2D toBeAttachedRope;

    bool isGrabbing = false;

    private void Awake()
    {
        hinge = GetComponentInParent<HingeJoint2D>();
        rb = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerMovement>();
    }

    private void Update()
    {
        GrabRope();
        Swing();
        
    }

    void Swing()
    {
        if (isGrabbing)
        {
            if (Input.GetKey(KeybindSaveSystem.currentKeybind.moveLeft))
            {
                rb.AddRelativeForce(new Vector2(-1,0) * 30);
            }
            if (Input.GetKey(KeybindSaveSystem.currentKeybind.moveRight))
            {
                rb.AddRelativeForce(new Vector2(1, 0) * 30);
            }
        }
    }

    void GrabRope()
    {
        Collider2D[] grabableRope = Physics2D.OverlapCircleAll(transform.position, 1f,ropeLayer);
        Rigidbody2D toBeAttachedRope = null;
        float closestDist = 99f;
        for (int i = 0; i < grabableRope.Length; i++)
        {
            float check = Vector2.Distance(grabableRope[i].transform.position, transform.position);
            if (check < closestDist)
            {
                closestDist = check;
                toBeAttachedRope = grabableRope[i].GetComponent<Rigidbody2D>();
            }
        }

        if (toBeAttachedRope)
        {
            if (Input.GetKey(KeybindSaveSystem.currentKeybind.grab) && !isGrabbing)
            {
                if (toBeAttachedRope != attachedRope)
                {
                    player.attachRope = true;
                    isGrabbing = true;
                    attachedRope = toBeAttachedRope;
                    hinge.connectedBody = attachedRope;
                    hinge.enabled = true;
                    animator.SetTrigger("isHang");
                    tanganKanan.parent = tali.GetChild(0);
                    tanganKiri.parent = tali.GetChild(1);
                    player.isGrabbing = true;
                }
            }

            if (Input.GetKeyUp(KeybindSaveSystem.currentKeybind.grab) && isGrabbing)
            {
                player.attachRope = false;
                isGrabbing = false;
                hinge.enabled = false;
                toBeAttachedRope = null;
                attachedRope = null;
                hinge.connectedBody = null;
                animator.ResetTrigger("isHang");
                animator.SetTrigger("releaseHang");
                tanganKanan.parent = this.transform.parent;
                tanganKiri.parent = this.transform.parent;
                player.isGrabbing = false;
            }

        }
    }

}
