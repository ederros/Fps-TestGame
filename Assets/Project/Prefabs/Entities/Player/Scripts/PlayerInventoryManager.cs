using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : InventoryManager<GunScriptableObject>
{
    [SerializeField]
    int itemsCount = 1;
    int selection = 0;
    [SerializeField]
    GameObject dropPrefab;

    [SerializeField]
    List<InvCellController> cells = new List<InvCellController>();

    public override int AddItem(GunScriptableObject newItem)
    {
        int result = base.AddItem(newItem);
        if(result>-1&&cells.Count>result){
            cells[result].SetForeground(newItem.Icon);
            itemsCount++;
        }
        return result;
    }
    public override int RemoveItem(GunScriptableObject oldItem)
    {
        int result = base.RemoveItem(oldItem);
        if(result>-1&&cells.Count>result){
            cells[result].SetForeground(null);
            itemsCount--;
        }
        return result;
    }

    void NextSelect(int delta){
        cells[selection].OnUnSelect();
        selection += delta;
        selection = (selection+ items.Count)%items.Count;
        while(items[selection]==null){
            selection += delta;
            selection = (selection+ items.Count)%items.Count;
        }
        cells[selection].OnSelect();
        if(selection < items.Count&&items[selection]!=null){
            PlayerController.Instance.GunContainer.MyGun = items[selection];
        }
    }

    private void Update() {
        if(Input.mouseScrollDelta.y!=0){
            NextSelect((int)Input.mouseScrollDelta.y);
            
        }

        if( Input.GetKeyDown(KeyCode.G)&&itemsCount>1){
            Instantiate(dropPrefab, PlayerController.Instance.transform.position+PlayerController.Instance.transform.forward, Quaternion.identity).GetComponent<FloorGunContainer>().MyGun = items[selection];
            RemoveItem(items[selection]);
            NextSelect(1);
        }
    }
}
