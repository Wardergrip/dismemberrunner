using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    private SelfDestructSequence selfDestructSequence;

    [Header("Movement")]
    [SerializeField] public bool allowMovement = true;
    [SerializeField] private bool DEBUG_ForceStill = false;
    [SerializeField] private float baseMovementSpeed = 14;
    [SerializeField] private float leglessMovementSpeed = 12;
    private float currentMovementSpeed;
    [SerializeField] private float armsRotationSpeed = 10;
    private Vector3 armsOriginalRotation;
    bool isArmsRotationComplete = true;
    bool armsRotationPositiveDone = false;

    Vector3 movementDirection;

    Rigidbody rb;

    Collider playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        selfDestructSequence = GetComponent<SelfDestructSequence>();
        currentMovementSpeed = baseMovementSpeed;
        armsOriginalRotation = selfDestructSequence.GetDismemberManager.leftArmJoint.transform.localEulerAngles; 
        playerCollider = player.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!selfDestructSequence.GetDismemberManager.HasLegs)
        {
            currentMovementSpeed = leglessMovementSpeed;
        }

        if (selfDestructSequence.GetDismemberManager.AmountOfLimbs == 0)
        {
            return;
        }
        if (player == null) return;
        movementDirection = player.transform.position - transform.position;
        movementDirection.y = 0;
        if (!isArmsRotationComplete) RotateArmsAnim();
    }

    private void FixedUpdate()
    {
        RotateEnemy();
        if (selfDestructSequence.GetDismemberManager.AmountOfLimbs == 0)
        {
            return;
        }
        if (allowMovement && !DEBUG_ForceStill)
        {
            if (Physics.Raycast(selfDestructSequence.GetDismemberManager.head.transform.position, (player.transform.position - selfDestructSequence.GetDismemberManager.head.transform.position), out RaycastHit hit))
            {
                if (hit.transform.gameObject == player)
                {
                    MoveEnemy();
                }
            }
        }
    }

    private void MoveEnemy()
    {
        rb.AddForce(movementDirection.normalized * currentMovementSpeed,ForceMode.Force);
    }

    private void RotateEnemy()
    {
        rb.MoveRotation(Quaternion.LookRotation(movementDirection.normalized));
    }

    public GameObject Target
    {
        get { return player; }
        set { player = value; }
    }

    #region ArmsRotation Stuff

    public bool IsArmsRotationComplete
    {
        get { return isArmsRotationComplete; }
    }

    public void StartArmsRotation()
    {
        isArmsRotationComplete = false;
    }
    private void RotateArmsAnim()
    {
        if (selfDestructSequence.GetDismemberManager.leftArmJoint.transform.rotation.eulerAngles.z < 17)
        {
            if (armsRotationPositiveDone)
            {
                isArmsRotationComplete = true;
                armsRotationPositiveDone = false;
                ResetArmRotation();
                return;
            }
            armsRotationSpeed = Mathf.Abs(armsRotationSpeed);
        }
        else if (selfDestructSequence.GetDismemberManager.rightArmJoint.transform.rotation.eulerAngles.z > 130)
        {
            armsRotationPositiveDone = true;
            armsRotationSpeed = -Mathf.Abs(armsRotationSpeed);
        }
        RotateArms(armsRotationSpeed);
    }

    private void RotateArms(float rotationSpeed)
    {
        if (selfDestructSequence.GetDismemberManager.leftArmJoint)
        {
            RotateArm(selfDestructSequence.GetDismemberManager.leftArmJoint, rotationSpeed);
        }
        if (selfDestructSequence.GetDismemberManager.rightArmJoint)
        {
            RotateArm(selfDestructSequence.GetDismemberManager.rightArmJoint, rotationSpeed);
        }
    }

    private void RotateArm(GameObject armJoint, float rotationSpeed)
    {
        armJoint.transform.Rotate(0, 0, Time.deltaTime * rotationSpeed, Space.Self);
    }

    private void ResetArmRotation()
    {
        selfDestructSequence.GetDismemberManager.leftArmJoint.transform.localEulerAngles = armsOriginalRotation;
        selfDestructSequence.GetDismemberManager.rightArmJoint.transform.localEulerAngles = armsOriginalRotation;
    }
    #endregion
}
