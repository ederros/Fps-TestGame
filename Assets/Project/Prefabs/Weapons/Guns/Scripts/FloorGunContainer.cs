using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGunContainer : GunContainer, IInteractable
{
    public void Interact(EntityBehaviour sender)
    {
        if(sender.MyInventory.AddItem(this.MyGun)>-1)
            Destroy(this.gameObject);
    }

}
