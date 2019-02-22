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
using UnityEngine.SceneManagement;
using UnityEditor;

[RequireComponent(typeof(Inv_Controller))]
public class Inv_Needed : MonoBehaviour, IPointerClickHandler
{

    ChangeCursor cam;
    Transform target;
    Inv_Controller cont;
    SimpleInv inv;

    [TextArea(2, 5)]
    public string lookMessage = "You see it.";
    [TextArea(2, 5)]
    public string usedUpMessage = "It's still there.";





    [Header("Require an Object to Interact")]

    public bool needObject;
    public Inven neededObject;
    [TextArea(2, 5)]
    public string needItemMessage = "You'll need a thing to do that.";
    [TextArea(2, 5)]
    public string haveItemMessage = "You have the thing you need!.";

    public bool unLocked = false;
    public Transform altRadius;

    [Header("Load a New Scene")]
    public bool loadScene = false;
    public int sceneToLoad = 1;
    public int waitBeforeSceneLoadTime;

    [Header("Give the Player an Inventory Item")]
    public bool giveObject;
    public Inven objectToGive;
    [TextArea(2, 5)]
    public string gotItemMessage = "It gives you a thing!.";

    [Header("Change Sprite on Unlock")]
    public Sprite spriteSwitch;






  
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
            cam.EnableDialog(lookMessage);
            cont.seeable = false;
            cont.interactive = true;
        }

        else if (cont.interactive)
        {
            if (NearEnough())
            {
                if (needObject)
                {

                    CheckIfHave();
                }

                else
                {
                    Unlocked();
                }
            } else { GetCloser(); }

        }
        else { if (usedUpMessage != null) {  cam.EnableDialog(usedUpMessage); } else { cam.EnableDialog(lookMessage); } }



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


        void CheckIfHave()
    {

        if (!unLocked)
        {
            if (inv.CheckItem(neededObject))
            {

                
                inv.RemoveItem(neededObject);
                unLocked = true;
                needObject = false;


                if (giveObject) {
                    GiveObject();
                    cam.EnableDialog(haveItemMessage + gotItemMessage );
                  Unlocked();

                }
                else
                {
                    cam.EnableDialog(haveItemMessage );
                    Unlocked();
                }
            }
            else
            {
                cam.EnableDialog(needItemMessage);

            }
        }
        else {
           
            Unlocked();
        }



    }

    void GiveObject()
    {
        if (inv.CheckToAdd())
        {
            
            inv.addItem(objectToGive);
            giveObject = false;
        }

    }

    void Unlocked()
    {

        cont.interactive = false;
        cont.used = true;

        if (spriteSwitch != null)
        {
            GetComponent<SpriteRenderer>().sprite = spriteSwitch;
        }

        if (giveObject) {
            cam.EnableDialog(gotItemMessage );
            GiveObject();
        }


       if (loadScene)
        {
            cam.EnableDialog(haveItemMessage);
            StartCoroutine(LoadScene());
        }

        else { cam.EnableDialog(usedUpMessage ); }



    }




    IEnumerator LoadScene()
{
   
    yield return new WaitForSeconds(waitBeforeSceneLoadTime);
        cam.DisableDialog();
        SceneManager.LoadScene(sceneToLoad);

    }
    
}







