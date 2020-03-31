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
    public static GameSystem Command;

    /*Game System can be any system that handles a defined part of the game;
     the buy of items, the battle system, the resurrection, the leveling system and enchancting system, etc.

    You can Run, Pause, and Stop a system;
     */

    //This system will be very handy with turing itself on and off.
    public string systemName;

    public static bool IsRunning { get; private set; }

    void Awake()
    {
        Command = this;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        switch (GameManager.Command.GetSystemStatus(this).systemStatus)
        {
            case Status.STOPPED: Stop(); break;
            case Status.PAUSED: Pause(); break;
            case Status.RUNNING: Run(); break;
            default:  break;
        }
    }

    /// <summary>
    /// All the main functionalities of a system will be put in Main()
    /// </summary>
    protected virtual void Main()
    {
        //This is were our main code is going to go.
    }



    public virtual void Run()
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.RUNNING);
        IsRunning = true;
        StartCoroutine(SystemRoutine());
    }

    public virtual void Pause()
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.PAUSED);
        IsRunning = false;
        StopCoroutine(SystemRoutine());
    }

    public virtual void Stop()
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.STOPPED);
        IsRunning = false;
        StopCoroutine(SystemRoutine());
    }

    public virtual void Stop(Exception _exception)
    {
        GameManager.Command.GetSystemStatus(this).OnStatusChange(Status.STOPPED);
        IsRunning = false;
        StopCoroutine(SystemRoutine());
        throw _exception;
    }

    /// <summary>
    /// The main function of our system. We can pause the process temporarily, stop it
    /// (either meaning that there was something wrong, or that it's done being used completely)
    /// or running it.
    /// </summary>
    /// <returns>null value.</returns>
    protected virtual IEnumerator SystemRoutine()
    {
        while(true)
        {
            try
            {
                Main();
            }
            catch
            {
                //We'll stop the process, and throw an exception.
                Stop(new SystemException());
            }
            yield return null;
        }
    }
}
