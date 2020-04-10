using System;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

using Cakewalk.IoC;

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

[CustomPropertyDrawer(typeof(ShowAsSystemIndicator))]
public class SystemInfoDrawer : PropertyDrawer
{
    GUIStyle currentStyle;
    Color stopped = new Color(1f, 0f, 0f, 0.5f);
    Color paused = new Color(0.5f, 0.5f, 0f, 0.5f);
    Color running = new Color(0f, 1f, 0f, 0.5f);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {


        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        var colorIndicator = new Rect(position.x, position.y, position.width - 220, position.height);
        var systemNameRect = new Rect(position.x + position.width - 215f, position.y, 80f, position.height);
        var systemObjRect = new Rect(position.x + position.width - 150f, position.y, 150f, position.height);

        switch (property.FindPropertyRelative("systemStatus.systemStatus").enumValueIndex)
        {
            case 0:
                {
                    currentStyle = new GUIStyle(GUI.skin.box);
                    currentStyle.normal.background = MakeTex(2, 2, stopped);
                }
                break;
            case 1:
                {
                    currentStyle = new GUIStyle(GUI.skin.box);
                    currentStyle.normal.background = MakeTex(2, 2, paused);
                }
                break;
            case 2:
                {
                    currentStyle = new GUIStyle(GUI.skin.box);
                    currentStyle.normal.background = MakeTex(2, 2, running);
                }
                break;
        }

        GUI.Box(colorIndicator, "", currentStyle);
        EditorGUI.PropertyField(systemNameRect, property.FindPropertyRelative("systemName"), GUIContent.none);
        EditorGUI.PropertyField(systemObjRect, property.FindPropertyRelative("system"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    private Texture2D MakeTex(int width, int height, Color col)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; ++i)
        {
            pix[i] = col;
        }
        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();
        return result;
    }
}



public class GameManager : MonoBehaviour
{
    private static GameManager Instance;
    public static class Command
    {
        public static T GetSystem<T>() where T : GameSystem => Instance.GetGameSystem<T>();
        public static SystemStatus GetSystemStatus(GameSystem system) => Instance.GetSystemStatus(system.systemName);
        public static SystemInfo[] GetSystemInfo() => Instance.GetAllSystemInfo();
        public static SystemInfo[] GetSystemInfo(Status _status) => Instance.GetAllSystemInfo(_status);
    }

    /*GameManager will have a whole collect of different systems and their statuses.*/

    [Header("Resolution")]
    public uint resolutionWidth = 1920;
    public uint resolutionHeight = 1660;

    [Header("Game Systems"), Dependency, ShowAsSystemIndicator]
    public List<SystemInfo> systemInfoList = new List<SystemInfo>();

    DirectoryInfo dirInfo;

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

        Application.targetFrameRate = 60;

        Screen.SetResolution((int)resolutionWidth, (int)resolutionHeight, FullScreenMode.FullScreenWindow, Application.targetFrameRate);

        //I want to also create a folder of Profiles if one exists or not.

        dirInfo = new DirectoryInfo(Application.persistentDataPath + "/Profiles");

        if (!dirInfo.Exists)
            CreateProfileDirectory();
    }

    void RegisterSystems()
    {
        GameSystem[] allGameSystems = FindObjectsOfType<GameSystem>();
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

    void StartUpAllSystems()
    {
        //We're going to turn on all systems defined in the game.
        Command.GetSystem<BattleSystem>().Run();
        Command.GetSystem<CurrencySystem>().Run();
        Command.GetSystem<DeityShrineSystem>().Run();
        Command.GetSystem<HealthSystem>().Run();
        Command.GetSystem<ManaSystem>().Run();
        Command.GetSystem<LevelingSystem>().Run();
        Command.GetSystem<ResurrectionSystem>().Run();
        Command.GetSystem<SPSystem>().Run();
        Command.GetSystem<WeaponSystem>().Run();
        Command.GetSystem<HeavensPlazaSystem>().Run();
    }

    public void Goto(string _scene)
    {
        try
        {
            SceneManager.LoadScene(_scene);
        } catch(IOException e)
        {
            Debug.LogWarning(e.Message);
        }
    }

    /// <summary>
    /// Gain access to the Game System defined in the game.
    /// </summary>
    /// <param name="_systemName">The name of the system.</param>
    /// <returns>A game system.</returns>
    private T GetGameSystem<T>() where T : GameSystem
    {
        foreach (SystemInfo info in systemInfoList)
        {
            if (info.system.GetType() == typeof(T))
                return (T)Convert.ChangeType(info.system, typeof(T));
        }

        Debug.Log(typeof(T) + "isn't an existing system. Why not creating one that derives from 'GameSystem'?");
        return default;
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
        Debug.Log(_systemName + " system does not exist.");
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

    private void CreateProfileDirectory()
    {
        try
        {
            dirInfo.Create();
        }
        catch (IOException e)
        {
            Debug.LogError(e.Message);
        }
    }
}
