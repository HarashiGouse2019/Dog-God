using System;
using System.Collections;
using UnityEngine;

public class SystemException : Exception
{
    public SystemException() { }
    public SystemException(string message) : base(message) { }
    public SystemException(string message, Exception inner) : base(message, inner) { }
}

[System.Serializable]
public abstract class GameSystem : MonoBehaviour
{
    public static GameSystem Instance;

    /*Game System can be any system that handles a defined part of the game;
     the buy of items, the battle system, the resurrection, the leveling system and enchancting system, etc.

    You can Run, Pause, and Stop a system;
     */

    //This system will be very handy with turing itself on and off.
    public string systemName;

    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Run()
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.RUNNING);
    }

    public virtual void Pause()
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.PAUSED);
    }

    public virtual void Stop()
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.STOPPED);
    }

    public virtual void Stop(Exception e)
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.STOPPED);
        Debug.LogException(e);
    }

    protected virtual IEnumerator Main()
    {
        while(true)
        {

            yield return null;
        }
    }
}
