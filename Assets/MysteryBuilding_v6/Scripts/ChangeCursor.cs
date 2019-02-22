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

public class ChangeCursor : MonoBehaviour {
	public LayerMask walkMask;
	public bool dialog;
	public Texture2D cursorWalk;
	public Texture2D cursorDefault;
	public Texture2D cursorLook;
	public Texture2D cursorInteract;
	 CursorMode cursorMode = CursorMode.Auto;
     Vector2 hotSpot = Vector2.zero;
   public  string activeGuy;
    public GameObject diaBox;
    public Text text;
   public  bool canWalk = false;
    public bool guiIs = false;



    public void Start () {

		useGUILayout = false;
	}




	void Update () {
        if (guiIs) { SetCursorLook(); }
        else
        {
            TheHits(); //update cursor depending on what it's hovering over. 
        }
        TheClicks(); // check if dialog needs to be closed. 
    }



    public void TheClicks() {
       
        if (Input.GetMouseButtonDown(0))
            
        {
          
            if (dialog)
            {
               
                DisableDialog();
            }
           
        }

       

    }

    public void TheHits()
    {
        // Raycast to find what mouse is hovered over. 
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity);
        Debug.DrawRay(worldPoint, Vector3.zero, Color.cyan);



        if (hit.collider != null)
            {
    
                if (!dialog)
                {

                 if (hit.transform.gameObject.tag == "Look")
                {
                    Debug.Log("look");
                    if (hit.transform.gameObject.GetComponent<Inv_Controller>()) {
                        Inv_Controller victim = hit.transform.gameObject.GetComponent<Inv_Controller>();
                        canWalk = false;
                        if (victim.interactive) { Cursor.SetCursor(cursorInteract, hotSpot, cursorMode); }
                        else { Cursor.SetCursor(cursorLook, hotSpot, cursorMode);
                            Debug.Log("other");
                        }

                    }


                }


                else if (walkMask == (walkMask | (1 << hit.transform.gameObject.layer)))



                {
                    Cursor.SetCursor(cursorWalk, hotSpot, cursorMode);
                    canWalk = true;



                }

                else
                {
                    canWalk = false;
                    Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
                    Debug.Log("default");

                }


                    activeGuy = hit.collider.gameObject.name;
                }
                else
                {
                canWalk = false;
                Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
                }
            }
            else
            {
                Cursor.SetCursor(cursorDefault, hotSpot, cursorMode);
            canWalk = false;
        }

        }



    public void SetCursorRemotely(Texture2D icon) {
        Cursor.SetCursor(icon, hotSpot, cursorMode);
    }

    public void SetCursorLook()
    {
        Cursor.SetCursor(cursorLook, hotSpot, cursorMode);
    }

    public void DisableDialog() {
        diaBox.SetActive(false);
        dialog = false;
        
    }

    public void EnableDialog(string str)
    {
        text.text = str;
        diaBox.SetActive(true);
        dialog = true;
     
    }



}
