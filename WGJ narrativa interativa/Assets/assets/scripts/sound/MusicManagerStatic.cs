using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerStatic : MonoBehaviour
{
    public int index = 0;
    MusicOnOffManager manager;
    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music Manager");
        foreach (GameObject obj in objs) {
            manager = obj.GetComponent<MusicOnOffManager>();
            if (manager.index == index) {
                break;
            }
        }
    }
    public void turnOn(int i, float time) { manager.turnOn(i, time); }
    public void turnOn(float time) { manager.turnOn(time); }
    public void turnOn() { manager.turnOn(); }
    public void turnOn(int i) { manager.turnOn(i); }
    public void turnOff(int i, float time) { manager.turnOff(i, time); }
    public void turnOff() { manager.turnOff(); }
    public void turnOff(int i) { manager.turnOff(i); }

    public void turnAllOff(float time)
    {
        manager.turnAllOff(time);
    }
    public void turnAllOff()
    {
        manager.turnAllOff();
    }
}
