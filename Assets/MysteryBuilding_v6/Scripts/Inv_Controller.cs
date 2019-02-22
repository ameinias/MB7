using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inv_Controller : MonoBehaviour
{

    public bool interactive = true;
    public bool seeable = true;
    public bool used = false;


    private void Start()
    {
        gameObject.tag = "Look";
    }

}
