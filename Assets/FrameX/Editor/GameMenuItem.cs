using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameMenuItem
{
    [MenuItem("FrameX/打开存档路径")]
    public static void OpenArchivedDirPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
}
