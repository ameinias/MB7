using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.EventSystems;

public class Inv_Interact : MonoBehaviour, IPointerClickHandler

{

    public string message;
    ChangeCursor cam;
    public bool lookedAt = false;
    public Texture2D cursor;
    public Inven item;


    void Start()
    {
        cam = Camera.main.GetComponent<ChangeCursor>();
        gameObject.tag = "Look";

    }

    public void OnPointerClick(PointerEventData eventData)
    {

        cam.EnableDialog(message);
    }

  public  void OnMouseEnter()
    {
        cam.guiIs = true;
    }
    public void OnMouseExit()
    {
        cam.guiIs = false;
    }


}
