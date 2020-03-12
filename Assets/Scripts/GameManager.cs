using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public enum AccessType
{
    SET,
    GET
}

public enum Target
{
    SYSTEM,
    STATUS,
    INFO
}

public class GameManager : MonoBehaviour
{
    private static GameManager Instance;

    public static class Command
    {
        public static GameSystem GetSystem(string _systemName) => Instance.GetGameSystem(_systemName);
       

        public static SystemStatus GetSystemStatus(GameSystem system) => Instance.GetSystemStatus(system.systemName);

        public static SystemInfo[] GetSystemInfo() => Instance.GetAllSystemInfo();
        public static SystemInfo[] GetSystemInfo(Status _status) => Instance.GetAllSystemInfo(_status);
     }

    /*GameManager will have a whole collect of different systems and their statuses.*/

    public List<SystemInfo> systemInfoList = new List<SystemInfo>();

    void Awake()
    {
        #region Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    // Start is called before the first frame update
    void Start()
    {
        RegisterSystems();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RegisterSystems()
    {
        GameSystem[] allGameSystems = Resources.FindObjectsOfTypeAll(typeof(GameSystem)) as GameSystem[];

        foreach (GameSystem entry in allGameSystems)
        {
            SystemInfo newSystemInfo = new SystemInfo
            {
                systemName = entry.systemName,
                system = entry,
                systemStatus = new SystemStatus()
            };

            systemInfoList.Add(newSystemInfo);
        }
    }

    /// <summary>
    /// Gain access to the Game System defined in the game.
    /// </summary>
    /// <param name="_systemName">The name of the system.</param>
    /// <returns>A game system.</returns>
    private GameSystem GetGameSystem(string _systemName)
    {
        foreach (SystemInfo info in systemInfoList)
        {
            if (info.systemName == _systemName)
                return info.system;
        }

        return null;
    }

    /// <summary>
    /// Gain access to rather the Game System is Running, Pause, or Stopped.
    /// </summary>
    /// <param name="_systemName"></param>
    /// <returns>A status of a specified game system.</returns>
    private SystemStatus GetSystemStatus(string _systemName)
    {
        foreach (SystemInfo info in systemInfoList)
        {
            if (info.systemName == _systemName)
                return info.systemStatus;
        }

        return null;
    }

    /// <summary>
    /// Let's you retrieve all systems, no matter what their status are.
    /// </summary>
    /// <returns>All system statuses</returns>
    private SystemInfo[] GetAllSystemInfo()
    {
        var systemInfo = from system in systemInfoList select system;

        SystemInfo[] data = systemInfo.ToArray();

        return data;
    }

    /// <summary>
    /// Let's you retrieve all systems will a specific status.
    /// </summary>
    /// <param name="_status"></param>
    /// <returns>Systems with a specify status.</returns>
    private SystemInfo[] GetAllSystemInfo(Status _status)
    {
        var systemInfo = from system in systemInfoList where system.systemStatus.Retrieve() == _status select system;

        SystemInfo[] data = systemInfo.ToArray();

        return data;
    }
}
