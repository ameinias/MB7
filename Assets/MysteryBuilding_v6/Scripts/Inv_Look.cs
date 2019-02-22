/*************************
 * 
 *   Tiny Adventure Builder - gblekkenhorst
 *   http://blekkenhorst.ca
 *   
 *   This file last updated:
 *   v1.7 - Feb 2019
 *   
 *   Please enjoy but do not sell or share the whole hit-and-caboodle. 
 *   You may publish games using this code, but please use you're own art.
 *   
 *   
 *   ******/
 
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEditor;

[RequireComponent(typeof(Inv_Controller))]
public class Inv_Look : MonoBehaviour, IPointerClickHandler
{

    Inv_Controller cont;
    ChangeCursor cam;
    [TextArea(2, 5)]
    public string firstLookMessage = "It's very surprising!";
    [TextArea(2, 5)]
    public string secondLookMessage = "You're getting used to it, now.";

    // Use this for initialization
    void Start () {
        cam = Camera.main.GetComponent<ChangeCursor>();
        cont = GetComponent<Inv_Controller>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!cont.seeable)
        {
            
            cam.EnableDialog(secondLookMessage);
        }
        else {
            cam.EnableDialog(firstLookMessage);
            cont.seeable = false;
            cont.used = true;
        }

    }





}
