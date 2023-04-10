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
    //Rigidbody2D toBeAttachedRope;

    bool isGrabbing = false;

    private void Awake()
    {
        hinge = GetComponentInParent<HingeJoint2D>();
        rb = GetComponentInParent<Rigidbody2D>();
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
                    isGrabbing = true;
                    attachedRope = toBeAttachedRope;
                    hinge.connectedBody = attachedRope;
                    hinge.enabled = true;

                }
            }

            if (Input.GetKeyUp(KeybindSaveSystem.currentKeybind.grab) && isGrabbing)
            {
                isGrabbing = false;
                hinge.enabled = false;
                toBeAttachedRope = null;
                attachedRope = null;
                hinge.connectedBody = null;
            }

        }
    }

}
