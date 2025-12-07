using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 2f;
    public float stopDistance = 1f;
    public float turnSpeed = 3f;
    public Animator myAnimator;
    public Rigidbody rb;
    public GameObject bonesContainer;
    public Rigidbody[] ragdollBodies;
    public Collider[] ragdollColliders;
    public bool isAlive = true;

    void Start()
    {
        ragdollBodies = bonesContainer.GetComponentsInChildren<Rigidbody>();
        foreach (var ab in ragdollBodies)
        {
            ab.isKinematic = true;
        }
        ragdollColliders = bonesContainer.GetComponentsInChildren<Collider>();
        foreach (var cc in ragdollColliders)
        {
            cc.enabled = false;
        }

        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void ActivateRagdoll()
    {
        rb.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;

        foreach (var rbBone in ragdollBodies)
        {
            rbBone.isKinematic = false;
        }
        foreach (var cc in ragdollColliders)
        {
            cc.enabled = true;
        }

        myAnimator.enabled = false;
    }

    void FixedUpdate()
    {
        if (target == null || isAlive == false) return;   // Usually should never happen (the car is missing)

        Vector3 direction = (target.position - transform.position);
        direction.y = 0f;

        if (direction.magnitude < stopDistance)
        {
            myAnimator.SetInteger("zombieAnimState", 2);
            return;
        }

        myAnimator.SetInteger("zombieAnimState", 1);
        direction.Normalize();

        Quaternion targetRot = Quaternion.LookRotation(direction);
        rb.MoveRotation(Quaternion.Slerp(rb.rotation, targetRot, turnSpeed * Time.fixedDeltaTime));

        Vector3 move = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            ActivateRagdoll();
            isAlive = false;
            rb.constraints = RigidbodyConstraints.None;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
    }
}