using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MB_JunkCode : MonoBehaviour
{ }



   // [CustomEditor(typeof(Inven))]

    public class InventoryBananaEditor : Editor
    {

        protected Inven me;

        public override void OnInspectorGUI()
        {

            EditorGUILayout.HelpBox("Let's try it", MessageType.None);
            me = (Inven)target;




            me.objectImage = (Sprite)EditorGUILayout.ObjectField("Sprite:", me.objectImage, typeof(Sprite), true);


            //(Sprite)EditorGUILayout.ObjectField( "Sprite",me.objectImage, typeof(Sprite));
            me.message = GUILayout.TextArea(me.message);



            //   GUILayout.Label("This is a Label in a Custom Editor");

            //thisTarget.isLootable = EditorGUILayout.Toggle("Is Lootable", thisTarget.isLootable);
            //thisTarget.animateWhenLooting = EditorGUILayout.Toggle("Animate When Looting", thisTarget.animateWhenLooting);

            //EditorGUILayout.HelpBox("Let's try it", MessageType.Info);

            //GUILayout.Label("Fades Don't Work");
            //EditorGUILayout.BeginFadeGroup(0);

            //GUILayout.FlexibleSpace();



            //me.destroyWhenEmpty = EditorGUILayout.Toggle("Destroy When Empty", thisTarget.destroyWhenEmpty);
            //if (me.destroyWhenEmpty)
            //{
            //    me.delayBeforeDestroying = EditorGUILayout.FloatField("Delay Before Destroying", thisTarget.delayBeforeDestroying);
            //    me.fadeBeforeDestroying = EditorGUILayout.Toggle("Fade Before Destroying", thisTarget.fadeBeforeDestroying);
            //    me.fadeDuration = EditorGUILayout.FloatField("Fade Duration", thisTarget.fadeDuration);
            //}



            // Editor





            if (GUI.changed)
                EditorUtility.SetDirty(target);
        }
    }

    public class InventoryItemEditorPeachJuice : EditorWindow

    {
   //     [MenuItem("MysteryBuilder/Make Me")]
        static void Init()
        {

            if (Selection.activeGameObject != null)
            {


                EditorWindow.GetWindow(typeof(InventoryItemEditorPeachJuice));


            }
            else
            {


                EditorUtility.DisplayDialog("No Object to Collectify", "Select Sprite Gameobject in the Hierarchy to make it an inventory collectable.", "ok boss");
            }
        }


        void OnGUI()
        {

            GUILayout.Label("Inventory Item Editor", EditorStyles.boldLabel);

            GameObject me = Selection.activeGameObject;

            Inven asset = (Inven)ScriptableObject.CreateInstance(typeof(Inven));
            string itemName;

            if (me)
            {
                me.name =
                    EditorGUILayout.TextField("Object Name: ", me.name);
            }

            if (asset)
                Debug.Log("I have a working assset here");


            string message;

            message = EditorGUILayout.TextField("Description of the Object: ", asset.message);
            Debug.Log("m " + message + "a " + asset.message);

            //  this.Repaint();

            // itemName = EditorGUILayout.TextField(itemName);
            /****
    asset.message = EditorGUILayout.TextArea("Description of object when in the Inventory", asset.message);




    Sprite image = me.GetComponent<SpriteRenderer>().sprite;
    if (image != null)
    {
    asset.objectImage = image;
    }
    else
    {
    asset.objectImage = (Sprite)EditorGUILayout.ObjectField("Sprite:", image, typeof(Sprite), true);
    }



        string lookMessage = EditorGUILayout.TextField("lookie");
            me.GetComponent<Inv_Collectable>().lookMessage = lookMessage;

         //   me.GetComponent<Inv_Collectable>().touchMessage = EditorGUILayout.TextField("", lookMessage);
                //GUILayout.TextArea("", me.GetComponent<Inv_Collectable>().touchMessage);

                  ***/
            GUILayout.Label("Optional", EditorStyles.boldLabel);
            /***

    me.GetComponent<Inv_Collectable>().destroyOnCollect = EditorGUILayout.Toggle("Destroy On Collection", true);
    if (!me.GetComponent<Inv_Collectable>().destroyOnCollect) { 
    me.GetComponent<Inv_Collectable>().spriteSwitch = (Sprite)EditorGUILayout.ObjectField("Sprite:", image, typeof(Sprite), true);
    // me.GetComponent<Inv_Collectable>().afterCollectMessage = GUILayout.TextArea("", me.GetComponent<Inv_Collectable>().afterCollectMessage); 
    }


    ****/

            GUILayout.FlexibleSpace();




            Debug.Log("message above button: " + message + "a " + asset.message);


            if (GUILayout.Button("Button"))
            {

                //CreateObject(me, itemName);
                Debug.Log("DOWN BELOW " + message + " c");
                asset.message = message + "but this works? why is message empty";
                Debug.Log("message after:  " + message + "asset after:  " + asset.message);
                string path = "Assets/" + me.name + ".asset";
                AssetDatabase.CreateAsset(asset, path);
                AssetDatabase.SaveAssets();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = asset;

                this.Close();
            }
            if (GUI.changed)
            {
                EditorUtility.SetDirty(me);
            }


        }

        public void CreateObject(GameObject me, string name)
        {

            me.AddComponent<CircleCollider2D>();
            me.GetComponent<CircleCollider2D>().radius += 0.5f;
            me.GetComponent<CircleCollider2D>().isTrigger = true;
            me.AddComponent<Rigidbody2D>();
            me.GetComponent<Rigidbody2D>().gravityScale = 0;
            me.tag = "Look";

            me.AddComponent<Inv_Controller>();
            me.AddComponent<Inv_Collectable>();




            GameObject mom = new GameObject();
            mom.name = name;


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

        }

        public static void CreateAsset(Inven asset, string name)
        {
            string path = "Assets/" + name + ".asset";
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }



    //public class InventoryItemEditor : EditorWindow
    //{

    //    public Inven me;
    //    private int viewIndex = 1;

    //    [MenuItem("MysteryBuilder/Inventory Item Editor %#e")]
    //   
    //    void OnEnable()
    //    {
    //        if (EditorPrefs.HasKey("ObjectPath"))
    //        {
    //            string objectPath = EditorPrefs.GetString("ObjectPath");
    //            me = AssetDatabase.LoadAssetAtPath(objectPath, typeof(Inven)) as Inven;
    //        }
    //        me = AssetDatabase.LoadAssetAtPath("", typeof(Inven)) as Inven;

    //    }

    //    void OnGUI()
    //    {
    //      // GUILayout.BeginHorizontal();
    //        GUILayout.Label("Inventory Item Editor", EditorStyles.boldLabel);
    //        me = AssetDatabase.LoadAssetAtPath("", typeof(Inven)) as Inven;

    //        me.message = GUILayout.TextArea("Description of object when in the Inventory", me.message);





    //        if (GUI.changed)
    //        {
    //            EditorUtility.SetDirty(me);
    //        }
    //    }


    //    void AddItem()
    //    {
    //        Inven newItem = new Inven();
    //        newItem.name = "New Item";

    //    }








