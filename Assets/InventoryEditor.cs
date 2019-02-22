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
using UnityEditor;

public class InventoryEditor : EditorWindow
{
    string nameDude;
    string lookMessage = "This could be useful.";
    string touchMessage = "You take the object.";
    Sprite sprite;

    string afterCollectMessage = "You find nothing else of use.";
    bool destroyOnCollect = true;
    Sprite spriteSwitch = null;
    string invMessage;
    bool dontdestroyOnCollect = false;
    bool doInv = false;


    void OnLostFocus()
    {
        this.Focus();
        // Close();  GameObject me = Selection.activeGameObject;
    }

    void OnGUI()
    {


        GameObject me = Selection.activeGameObject;
    

        GUILayout.Label("Inventory Item Editor", EditorStyles.boldLabel);
        GUILayout.Label(" Enter info of the object. You can always change the object details late. For the scene object, select it and change details in the inspector. For the inventory item details, find the asset in the Assets/Inventory folder.", EditorStyles.helpBox);


        me.name = EditorGUILayout.TextField("Name", me.name);
        nameDude = me.name;
        EditorGUILayout.PrefixLabel("LookMessage");
        lookMessage = EditorGUILayout.TextArea(lookMessage, GUILayout.Height(40));

        EditorGUILayout.PrefixLabel("Touch Message");
        touchMessage = EditorGUILayout.TextArea(touchMessage, GUILayout.Height(40));

        GUILayout.Label("Optional");

        //  EditorGUILayout.ToggleLeft("Destroy Object Once Collected",false);
        dontdestroyOnCollect = EditorGUILayout.BeginToggleGroup("Keep Object In Scene After Collection ", dontdestroyOnCollect);
       
        EditorGUILayout.PrefixLabel("After Collect message");
        afterCollectMessage = EditorGUILayout.TextArea(afterCollectMessage, GUILayout.Height(40));

        EditorGUILayout.PrefixLabel("Switch Sprite After Collecting (Optional)");

        spriteSwitch = (Sprite)EditorGUILayout.ObjectField(spriteSwitch, typeof(Sprite), true);

        EditorGUILayout.EndToggleGroup();


        doInv = EditorGUILayout.BeginToggleGroup("Different Examine Message in Inventory", doInv);
        invMessage = EditorGUILayout.TextArea(invMessage, GUILayout.Height(40));

        EditorGUILayout.EndToggleGroup();
       




        GUILayout.FlexibleSpace();



        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }

        if (GUILayout.Button("Inventoralize"))
        {

            if (nameDude == null)
                nameDude = me.name;
            OnClickSave();
            GUIUtility.ExitGUI();
            Close();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(Selection.activeGameObject);
        }



    }

    void OnClickSave()
    {


        if (lookMessage == null)
        {

            

            EditorUtility.DisplayDialog("You need a look message!", "Please type something in the Look message. If you're feeling lazy, the other messages will copy that one.", "ok boss");

        }
        else {

            lookMessage = lookMessage.Trim();

            if (string.IsNullOrEmpty(invMessage))
            {
                invMessage = lookMessage;

            }



            if (string.IsNullOrEmpty(afterCollectMessage))
            {
                afterCollectMessage = lookMessage;

            }

            if (string.IsNullOrEmpty(touchMessage))
            {
                touchMessage = lookMessage;

            }




            MakeCollectable();
            Close();

        }





    }


    void MakeCollectable()
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
        mom.name = nameDude;


        GameObject sister = new GameObject();
        sister.name = "WalkPoint";

        sister.AddComponent<CircleCollider2D>();
        sister.GetComponent<CircleCollider2D>().isTrigger = true;
        sister.AddComponent<Rigidbody2D>();
        sister.GetComponent<Rigidbody2D>().gravityScale = 0;
        sister.AddComponent<Seperate_Hitbox>();
        Inv_Collectable info;
        info = me.GetComponent<Inv_Collectable>();
        sister.GetComponent<CircleCollider2D>().radius = me.GetComponent<CircleCollider2D>().radius;
        sister.AddComponent<MakeWalkable>();

        sister.transform.SetParent(mom.transform);
        me.transform.SetParent(mom.transform);

        if (!AssetDatabase.IsValidFolder("Assets/Inventory"))
        {
            string guid = AssetDatabase.CreateFolder("Assets", "Inventory");
            string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);

        }

        Inven asset = (Inven)ScriptableObject.CreateInstance(typeof(Inven));
        string path = "Assets/Inventory/i_" + nameDude + ".asset";
        asset.message = lookMessage;
        asset.objectImage = me.GetComponent<SpriteRenderer>().sprite;
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();

        // Populate Info
        info.altRadius = sister.transform;
        info.touchMessage = touchMessage;
        info.item = asset;
        info.lookMessage = lookMessage;
        info.destroyOnCollect = destroyOnCollect;


        Selection.activeObject = me;





    }




}


public class LookEditor : EditorWindow
{



    public string nameDude;

    Sprite sprite;

    public string firstLookMessage = "It's very surprising!";

    public string secondLookMessage = "You're getting used to it, now.";


    GUIStyle niceLabel;

    void OnLostFocus()
    {
        this.Focus();
        // Close();  GameObject me = Selection.activeGameObject;
    }

    void OnGUI()
    {




        EditorGUILayout.BeginVertical(niceLabel);
        GameObject me = Selection.activeGameObject;
        GUILayout.Label("Lookable Item Editor", EditorStyles.boldLabel);



        GUILayout.Label(" Enter info of object. You can always change in the inspector later.",EditorStyles.helpBox);
        nameDude = me.name;
        nameDude = EditorGUILayout.TextField("Name", nameDude);

        EditorGUILayout.PrefixLabel("Message First Time Looked At");
        firstLookMessage = EditorGUILayout.TextArea(firstLookMessage, GUILayout.Height(40));

        EditorGUILayout.PrefixLabel("Looked At Message");
        secondLookMessage = EditorGUILayout.TextArea(secondLookMessage, GUILayout.Height(40));







        GUILayout.FlexibleSpace();



        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Cancel"))
        {
            Close();
        }

        if (GUILayout.Button("Make Lookable"))
        {
            MakeCollectable();
            OnClickSave();
            GUIUtility.ExitGUI();
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(Selection.activeGameObject);
        }
        EditorGUILayout.EndVertical();


    }

    void OnClickSave()
    {


        Close();
    }


    void MakeCollectable()
    {



        GameObject me = Selection.activeGameObject;
        me.AddComponent<CircleCollider2D>();
        me.GetComponent<CircleCollider2D>().radius += 0.5f;
        me.GetComponent<CircleCollider2D>().isTrigger = true;
        me.AddComponent<Rigidbody2D>();
        me.GetComponent<Rigidbody2D>().gravityScale = 0;
        me.tag = "Look";

        me.AddComponent<Inv_Controller>().interactive = false;

        Inv_Look info;
        info = me.AddComponent<Inv_Look>();




        EditorUtility.FocusProjectWindow();

        // Populate Info




    info.firstLookMessage = firstLookMessage;
        info.secondLookMessage = secondLookMessage;



        Selection.activeObject = me;






    }




}