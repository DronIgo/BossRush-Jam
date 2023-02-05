using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSize : MonoBehaviour
{
    public float size = 60f;
    public void FixSize(float size)
    {
        for (int c = 0; c < transform.childCount; ++c)
        {
            transform.GetChild(c).position = new Vector3(c * size, transform.GetChild(c).position.y, transform.GetChild(c).position.z);
            
        }
    }
}
