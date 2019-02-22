/*********************************************************************************
 * 
 *   Tiny Adventure Builder - gblekkenhorst
 *   http://blekkenhorst.ca
 *   
 *   This file last updated:
 *   v1.7 - Feb 2019
 *   
 *   Please enjoy but do not sell or share the whole hit-and-caboodle. 
 *   You may publish games using this code, but please use your different art.
 *   
 *   
 *******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

[RequireComponent(typeof(Inv_Controller))]
public class Inv_Collectable : MonoBehaviour, IPointerClickHandler
{

    SimpleInv inv;
    ChangeCursor cam;
    Transform target;
    Inv_Controller cont;

    [Header("Inventory Item")]

    public Inven item;



    [ContextMenuItem("Reset to Standard", "Reset")]
    [TextArea(2, 5)]
    public string lookMessage = "Looks like a thing that I can have.";
    [TextArea(2, 5)]
    public string touchMessage = "It's mine now.";
    public Transform altRadius;


    [Header("Optional Things")]


    public bool destroyOnCollect;
    public Sprite spriteSwitch;
    [TextArea(2, 5)]
    public string afterCollectMessage;






    void Reset()
    {
        lookMessage = "";
        touchMessage = "";
        afterCollectMessage = "";
        destroyOnCollect = true;
        spriteSwitch = null;
        item = null;
    }


 
    void Start()
    {
        cam = Camera.main.GetComponent<ChangeCursor>();
        inv = FindObjectOfType<SimpleInv>();
        target = GameObject.FindWithTag("Target").transform;
        cont = GetComponent<Inv_Controller>();


    }


    public void OnPointerClick(PointerEventData eventData)
    { 

        if (cont.seeable)
        {
            Look(lookMessage);
            cont.seeable = false;
            cont.interactive = true;
        }

        else if (cont.interactive)
        {
            if (NearEnough()) { AttemptCollection(); } else { GetCloser(); }

        }
        else { if (afterCollectMessage != null) { Look(afterCollectMessage); } else { Look(lookMessage); } }

    }

    void AttemptCollection()
    {
        if (inv.CheckToAdd())
        {
            cam.EnableDialog(touchMessage);
            cont.interactive = false;
            cont.used = true;
            inv.addItem(item);
            AfterItem();

        }
        else
        {
            cam.EnableDialog("I can't carry anymore items");
        }
    }

    public bool NearEnough()
    {
        if (altRadius != null)
        {
            bool hit = altRadius.GetComponent<Seperate_Hitbox>().nearEnough;
            if (hit) { return true; }
            else return false;
        }
        return false;
    }

    void GetCloser()
    {
        cam.EnableDialog("Maybe I could do that if I were closer.");

        Vector3 newPos;


        newPos = new Vector3(altRadius.transform.position.x, altRadius.transform.position.y, target.position.z);

        target.position = newPos;

    }




    void Look(string message)
    {

        cam.EnableDialog(message);
    }

    void Touch()
    {


    }

    void AfterItem()
    {

        if (destroyOnCollect)
        { Destroy(gameObject.transform.parent); }
        else if (spriteSwitch != null)
        {
            GetComponent<SpriteRenderer>().sprite = spriteSwitch;
        }
    }

}

