using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] bool destroyByTag = true;
    [SerializeField] string tagName = "";
    void Awake()
    {
        if (destroyByTag)
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);
            if (objs.Length > 1)
            {
                Destroy(this.gameObject);
                return;
            }
        }
        transform.SetParent(null);
        DontDestroyOnLoad(this.gameObject);
    }

}
