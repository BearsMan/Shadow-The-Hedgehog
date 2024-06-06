using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperForm : MonoBehaviour
{
    public SkinnedMeshRenderer objectMaterialRender1;
    public SkinnedMeshRenderer objectMaterialRender2;
    public Material superForm;
    public WeaponSystem weaponController;
    public PickUpItem energyBlast;
    public bool isFlying = false;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlying)
        {
            isFlying = false;
            FlyController();
        }
    }
    public void ChangeToSuperForm()
    {
        objectMaterialRender1.material = superForm;
        objectMaterialRender2.material = superForm;
        weaponController.AddWeapons(energyBlast);
        GameManager.instance.isInSuperForm = true; // When not playing the game scene, revert back to normal form.
    }
    private void FlyController()
    {
        body.constraints = RigidbodyConstraints.FreezePositionY;
        // currentStates = States.flying;
        // animController.currentAnim = animController.currentAnim;
        ChangeToSuperForm();
    }
}
