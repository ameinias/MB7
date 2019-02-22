
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

using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(menuName = "Inven")]

public class Inven : ScriptableObject
{

    public Sprite objectImage;
    public string message;

}
