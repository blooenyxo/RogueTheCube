using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Drop : MonoBehaviour, IDropHandler {

    public virtual void OnDrop (PointerEventData eventData) {
        if (transform.childCount == 0)
            eventData.pointerDrag.GetComponent<Item_Drag> ().parent = this.transform;
    }
}