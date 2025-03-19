using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUIFollow : MonoBehaviour
{
    public Transform cam;


    void Update()
    {
        if (cam != null)
        {
            transform.LookAt(cam);

            transform.Rotate(0, 180, 0);

        }
    }

}
