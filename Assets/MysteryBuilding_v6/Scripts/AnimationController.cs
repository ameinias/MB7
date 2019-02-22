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

using UnityEngine;
using System.Collections;
using Pathfinding.RVO;
using Pathfinding.Util;
using Pathfinding;



[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    public Vector2 targetVelocity;
    private Animator animator;

    public float run = 6f;
    //	public Vector2 targetVelocity;
    public bool canWalk;
    AIPath path;

    void Start()
    {
        animator = GetComponent<Animator>();
        path = GetComponentInParent<AIPath>();
    }

    void FixedUpdate()
    {
        if (canWalk)
        {

            targetVelocity = path.rotDir;
            //				Debug.Log (targetVelocity + path.rotDir);

            if (targetVelocity != Vector2.zero)
            //animator.Play ("idle");
            //	else
            {
                if (Mathf.Abs(targetVelocity.x) < 0.5f)
                {
                    if (targetVelocity.y > 0)
                    {
                        animator.Play("Up");
                        //	Debug.Log ("Up");
                    }
                    else
                    {
                        animator.Play("Down");
                    }
                }
                else
                {
                    if (targetVelocity.x > 0)
                    {
                        animator.Play("Right");
                    }
                    else
                    {
                        animator.Play("Left");
                    }
                }
            }

        }
    }
}






