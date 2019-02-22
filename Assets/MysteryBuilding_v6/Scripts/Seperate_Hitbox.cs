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
using UnityEditor;

[HelpURL("http://example.com/docs/MyComponent.html")]

//[RequireComponent(typeof(Collider2D))]


public class Seperate_Hitbox : MonoBehaviour
{
    public bool nearEnough;

    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Walkable");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        
            if (collision.gameObject.tag == "Player")
                nearEnough = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        nearEnough = false;
     }



}
