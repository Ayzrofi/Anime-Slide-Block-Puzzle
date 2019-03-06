using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScroll : MonoBehaviour {

    //public Transform MenuScrollGameObject;
    public RectTransform menu;

    Vector3 pos;

    bool moved;
    [ContextMenu("Move")]
   
    public void Move()
    {
        pos = menu.anchoredPosition;
        pos.x -= 600;
        menu.anchoredPosition = pos;
    }
}
