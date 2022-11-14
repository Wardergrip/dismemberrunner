using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DismemberManager : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject head;
    public GameObject torso;
    public GameObject leftArmJoint;
    public GameObject leftArm;
    public GameObject rightArmJoint;
    public GameObject rightArm;
    public GameObject leftLeg;
    public GameObject rightLeg;
    public GameObject heart;

    [Header("Heart color")]
    [SerializeField]
    private Color flashingColor;
    private Color originalColor;
    private Material heartMaterial;
    private bool isFlashing = false;

    const string COLOR_PARAMETER = "_Color";

    // Start is called before the first frame update
    void Start()
    {
        Renderer renderer = heart.gameObject.GetComponent<Renderer>();
        heartMaterial = renderer.material;
        originalColor = heartMaterial.GetColor(COLOR_PARAMETER);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Destroy(heartMaterial);
    }

    public bool IsFlashing { get { return isFlashing; } }

    public int AmountOfLimbs 
    { 
        get
        {
            int amount = 0;
            if (leftArm) ++amount;
            if (rightArm) ++amount;
            if (leftLeg) ++amount;
            if (rightLeg) ++amount;
            return amount;
        }
    }
    public float AmountOfLimbsPercentage
    {
        get
        {
            return (AmountOfLimbs / 4.0f);
        }
    }
    public bool HasLegs
    {
        get { return (leftLeg || rightLeg); }
    }
    public bool HasArms
    {
        get { return (leftArm || rightArm); }
    }

    public void SetOriginalColor()
    {
        heartMaterial.SetColor(COLOR_PARAMETER, originalColor);
        isFlashing = false;
    }

    public void SetFlashingColor()
    {
        heartMaterial.SetColor(COLOR_PARAMETER, flashingColor);
        isFlashing = true;
    }

    public void SwitchFlashingColor()
    {
        if (isFlashing)
        {
            SetOriginalColor();
        }
        else
        {
            SetFlashingColor();
        }
    }

    public void DestroyAllLimbs()
    {
        if (leftArm) Destroy(leftArm);
        if (rightArm) Destroy(rightArm);
        if (leftLeg) Destroy(leftLeg);
        if (rightLeg) Destroy(rightLeg);
    }
}
