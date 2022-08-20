using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    [SerializeField] string tagName = "";
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag(tagName);

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.SetParent(null);
        DontDestroyOnLoad(this.gameObject);
    }

}
