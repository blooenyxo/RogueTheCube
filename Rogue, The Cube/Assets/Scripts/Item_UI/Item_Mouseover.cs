﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Item_Mouseover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject tooltip;

    private void Start()
    {
        tooltip.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.SetActive(false);
    }
}
