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

public class MakeWalkable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Walkable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
