using System.Collections.Generic;
using UnityEngine;

public class InventoryManager<T> : MonoBehaviour
{
    [SerializeField]
    protected List<T> items = new List<T>();
    [SerializeField]
    protected int maxItemCount;
    public bool Contains(T item){
        return items.Contains(item);
    }

    public virtual int AddItem(T newItem){ // returns index of new element, or -1 if occurs error
        int i;
        for(i = 0;i<items.Count;i++){
            if(items[i]!=null) continue;
            items[i] = newItem;
            return i; 
        }
        if(i<maxItemCount){
            items.Add(newItem);
            return i;
        }
        return -1;
    }

    public virtual int RemoveItem(T oldItem){ // returns index of removed element, or -1 if occurs error
        for(int i = 0;i<items.Count;i++){
            if((object)items[i]!=(object)oldItem) continue;
            items[i] = default(T);
            return i; 
        }
        return -1;
    }
}
