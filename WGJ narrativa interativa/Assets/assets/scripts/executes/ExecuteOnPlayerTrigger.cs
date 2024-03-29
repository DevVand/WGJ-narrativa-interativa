﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteOnPlayerTrigger : MonoBehaviour
{
    public bool inside = false;

    public UnityEvent events;
    public UnityEvent exitEvents;
    public UnityEvent stayEvents;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = true;
            events.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = false;
            exitEvents.Invoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inside = true;
            stayEvents.Invoke();
        }
    }
}
