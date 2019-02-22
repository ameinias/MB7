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
 *   This script controls the inventory system list and HUD.
 *   
 *******************************************************************************/
 
 
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEditor;

public class SimpleInv : MonoBehaviour
{


    public List<Inven> items = new List<Inven>();
    public int maxInvantorySlots = 11;
    public bool resetInventoryOnStart = true;
    public List<GameObject> textLinks = new List<GameObject>();
    public GameObject textLinkPrefab;
    public int linkDist = -90;
    public int totalOffsetLink = 500;

    public string banana = "I am a banana";








   // [MenuItem("MysteryBuilder/Make Door ")]
    static void MakeDoor()

    {
        if (Selection.activeGameObject != null)
        {


            GameObject me = Selection.activeGameObject;
            me.AddComponent<CircleCollider2D>();
            me.GetComponent<CircleCollider2D>().radius += 0.5f;
            me.GetComponent<CircleCollider2D>().isTrigger = true;
            me.AddComponent<Rigidbody2D>();
            me.GetComponent<Rigidbody2D>().gravityScale = 0;
            me.tag = "Look";

            me.AddComponent<Inv_Controller>();
            me.AddComponent<Inv_Needed>();




            GameObject mom = new GameObject();
            mom.name = me.name + " door";


            GameObject sister = new GameObject();
            sister.name = "WalkPoint";

            sister.AddComponent<CircleCollider2D>();
            sister.GetComponent<CircleCollider2D>().isTrigger = true;
            sister.AddComponent<Rigidbody2D>();
            sister.GetComponent<Rigidbody2D>().gravityScale = 0;
            sister.AddComponent<Seperate_Hitbox>();
            me.GetComponent<Inv_Needed>().altRadius = sister.transform;
            sister.GetComponent<CircleCollider2D>().radius = me.GetComponent<CircleCollider2D>().radius;
            sister.AddComponent<MakeWalkable>();

            sister.transform.SetParent(mom.transform);
            me.transform.SetParent(mom.transform);


        }
        else
        {

            Debug.Log("Please select an object to make it fancy.");
        }
    }


   // [MenuItem("MysteryBuilder/Make Collectable ")]

    static void MakeCollectable(string lookMessage, Sprite sprite)
    {
        if (Selection.activeGameObject != null)
        {
            //string lookMessage = "It's nice to look at.";

            GameObject me = Selection.activeGameObject;
            me.AddComponent<CircleCollider2D>();
            me.GetComponent<CircleCollider2D>().radius += 0.5f;
            me.GetComponent<CircleCollider2D>().isTrigger = true;
            me.AddComponent<Rigidbody2D>();
            me.GetComponent<Rigidbody2D>().gravityScale = 0;
            me.tag = "Look";

            me.AddComponent<Inv_Controller>();
            me.AddComponent<Inv_Collectable>();




            GameObject mom = new GameObject();
            mom.name = me.name + " coll";


            GameObject sister = new GameObject();
            sister.name = "WalkPoint";

            sister.AddComponent<CircleCollider2D>();
            sister.GetComponent<CircleCollider2D>().isTrigger = true;
            sister.AddComponent<Rigidbody2D>();
            sister.GetComponent<Rigidbody2D>().gravityScale = 0;
            sister.AddComponent<Seperate_Hitbox>();
            me.GetComponent<Inv_Collectable>().altRadius = sister.transform;
            sister.GetComponent<CircleCollider2D>().radius = me.GetComponent<CircleCollider2D>().radius;
            sister.AddComponent<MakeWalkable>();

            sister.transform.SetParent(mom.transform);
            me.transform.SetParent(mom.transform);

            if (!AssetDatabase.IsValidFolder("Assets/Inventory")) { 
            string guid = AssetDatabase.CreateFolder("Assets", "Inventory");
            string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

        }

            Inven asset = (Inven)ScriptableObject.CreateInstance(typeof(Inven));
            string path = "Assets/Inventory/i_" + me.name + ".asset";
            Debug.Log(me.GetComponent<SpriteRenderer>().sprite + " hey ");
            asset.message = lookMessage;
            asset.objectImage = me.GetComponent<SpriteRenderer>().sprite;
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            me.GetComponent<Inv_Collectable>().item = asset;
            me.GetComponent<Inv_Collectable>().lookMessage = lookMessage;

            //   Selection.activeObject = asset;
              Selection.activeObject = me;
            //  Selection.activeTransform = me.transform;
            //Object[] holdMe = new Object[2];
            //holdMe[0] = me; holdMe[1] = asset;
            //Selection.objects = holdMe;



        }
        else
        {

            Debug.Log("Please select an object with a sprite to make it fancy.");
        }
    }




   // [MenuItem("MysteryBuilder/Make Lookable ")]
    static void MakeLookable()
    {
        if (Selection.activeGameObject != null)
        {


            GameObject me = Selection.activeGameObject;
            me.AddComponent<CircleCollider2D>().isTrigger = true;
            me.AddComponent<Rigidbody2D>().gravityScale = 0;
            me.tag = "Look";

            me.AddComponent<Inv_Controller>().interactive = false;
            me.AddComponent<Inv_Look>();

        }
        else
        {

            Debug.Log("Please select an object to make it fancy.");
        }
    }



    [MenuItem("MysteryBuilder/InventoryCreator")]
        static void Here()
    {

        if (Selection.activeGameObject != null)
        {


            EditorWindow.GetWindow(typeof(InventoryEditor));


        }
        else
        {


            EditorUtility.DisplayDialog("No Object to Collectify", "Select Sprite Gameobject in the Hierarchy to make it an inventory collectable.", "ok boss");
        }
    }

    [MenuItem("MysteryBuilder/LookCreator")]
    static void Lookit()
    {

        if (Selection.activeGameObject != null)
        {


            EditorWindow.GetWindow(typeof(LookEditor));

      


        }
        else
        {


            EditorUtility.DisplayDialog("No Object to Lookify", "Select Sprite Gameobject in the Hierarchy to make it alookable object.", "ok boss");
        }
    }

    





      //  [MenuItem("MysteryBuilder/Make Me")]
    static void Init()
    {

        if (Selection.activeGameObject != null)
        {


           
            GameObject me = Selection.activeGameObject;
            if (me.GetComponentInChildren<Inv_Collectable>())
            {
                EditorWindow.GetWindow(typeof(InventoryEditor));

                Inven asset = (Inven)ScriptableObject.CreateInstance(typeof(Inven));
                Inv_Collectable thing = me.GetComponentInChildren<Inv_Collectable>();
                string path = "Assets/i_" + thing.name + ".asset";
                Debug.Log(thing.gameObject.GetComponent<SpriteRenderer>().sprite+ " hey ");
                asset.message = thing.lookMessage;
                asset.objectImage = thing.GetComponent<SpriteRenderer>().sprite;
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                EditorUtility.FocusProjectWindow();
                thing.item = asset;
                
                Selection.activeObject = asset;

            }
            else {

                EditorUtility.DisplayDialog("No Collectable Object to Inventorize.", "Please select an GameObject that has been run thorugh the Make Collectable process.", "ok boss");

            }
        }
        else
        {


            EditorUtility.DisplayDialog("No Object to Collectify", "Select Sprite Gameobject in the Hierarchy to make it an inventory collectable.", "ok boss");
        }
    }



    void Start()
    {
        if (resetInventoryOnStart) {
            ClearnInv();
        }
    }



    public bool CheckToAdd() {
        if (items.Count >= maxInvantorySlots)
        {
            Debug.Log("Too many items");
            return false;
        }
        else {
            return true;
        }
    }

    public void addItem(Inven item)
    {


        GameObject linkGO = (GameObject)Instantiate(textLinkPrefab);
        linkGO.GetComponent<Image>().sprite = item.objectImage;
        linkGO.name = item.name;
        linkGO.transform.SetParent(this.transform);
        linkGO.transform.localPosition = new Vector3(items.Count * 1 * linkDist - totalOffsetLink, 0, 0);
        linkGO.transform.localScale = Vector3.one;
        linkGO.GetComponent<Inv_Interact>().message = item.message;
        linkGO.GetComponent<Inv_Interact>().cursor = item.objectImage.texture;
        linkGO.GetComponent<Inv_Interact>().item = item;
        Debug.Log(linkGO.GetComponent<Inv_Interact>().item.name + item.name);
        textLinks.Add(linkGO);
        items.Add(item);
    }


    public bool CheckItem(Inven item)
    {
        if (items.Contains(item))
        { return true; }
        else { return false; }
    }

    public void RemoveItem(Inven item)
    {

        items.Remove(item);

        for (int t = 0; t < textLinks.Count; t++)
        {
            
            if (textLinks[t].name == item.name)
            {
                Debug.Log("found " + item.name +" in textlinks");

                GameObject.Destroy(textLinks[t]);
                    textLinks.RemoveAt(t);


            }
            else {
                Debug.Log("did not find" + item.name);
              
            }
            Debug.Log(t);
        }


        for (int t = 0; t < textLinks.Count; t++)
        {
            textLinks[t].transform.localPosition = new Vector3(t * 1 * linkDist - totalOffsetLink, 0, 0);

        }


    }

    public void ClearnInv() {
        items.Clear();
    }

}







