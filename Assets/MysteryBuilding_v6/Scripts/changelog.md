
/*********************************************************************************
 * 
 *   Tiny Adventure Builder - gblekkenhorst
 *   http://blekkenhorst.ca
 *   gillian@blekkenhorst.ca
 *   
 *   This file last updated:
 *   v1.7 - Feb 2019 - Unity 2018.3
 *   
 *   Please enjoy but do not sell or share the whole hit-and-caboodle. 
 *   You may publish games using this code, but please use your different art.
 *   
 *   
 *******************************************************************************/

changelog:

 v1.7 - Feb 2019 - Unity 2018.3.2f1
    - Fixed door unlocking conflict with recieving object
    - Discontinured Unity 5 compatiblity
    - Created menu to auto-create objects
    - Added Mouseover descriptions of items in Inventory
    - Cleaned up a lot of conditionals in ChangeCursor and Inv_Objects
    - Created seperate hit box for Inv_Objects, so they can be looked 
        at from a distance but walked towards the closest walkable place
    - Repaired Player animator
    - Added changeSprite to Inv_Needed and Inv_Collectable.
    - Inv_Collectables now don't need to be destroyed when being collected. 
    - Removed Event triggers on Inv_Objects. 
    - 
 
 v1.6 - Feb 2019 - Unity 2018.3.2f1
    - Inv_Needed can now give you an object when unlocked. 
 
 v1.5 -  Oct 2018 - Unity 2017.2
     - Added load scene to door
     - Made Unity 5 version
