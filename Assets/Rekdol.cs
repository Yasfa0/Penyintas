using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.IK;

public class Rekdol : MonoBehaviour
{
    public Animator animator;
    public sekrip player;
    public Rigidbody2D playerRb;
    public Collider2D playerCollider;
    public List<Collider2D> colliders;
    public List<HingeJoint2D> joints;
    public List<Rigidbody2D> rigidbodies;
    public List<LimbSolver2D> limbSolvers;
    public bool isDead = false;
    // Start is called before the first frame update
    void Update()
    {
        Ragdoll(isDead);
    }

    public void Ragdoll(bool bolehRagdoll)
    {
        animator.enabled = !bolehRagdoll;
        playerRb.simulated = !bolehRagdoll;
        playerCollider.enabled = !bolehRagdoll;
        player.enabled = !bolehRagdoll;

        foreach(var col in colliders)
        {
            col.enabled = bolehRagdoll;
        }

        foreach(var joint in joints)
        {
            joint.enabled = bolehRagdoll;
        }

        foreach(var rb in rigidbodies)
        {
            rb.
                simulated = bolehRagdoll;
        }

        foreach(var solver in limbSolvers)
        {
            solver.enabled = !bolehRagdoll;
        }
    }
}
