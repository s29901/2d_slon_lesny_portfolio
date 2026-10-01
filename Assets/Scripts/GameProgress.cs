using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Everything the game remembers: where the player stood in each scene,
/// whether the skeleton has been completed, and which bones have been dug up.
///
/// Kept in PlayerPrefs, so it survives both scene changes and quitting the game.
/// </summary>
public static class GameProgress
{
    private const string PuzzleKey = "progress.puzzleCompleted";
    private const string BonesKey = "progress.collectedBones";
    private const string SceneListKey = "progress.scenesWithPosition";

    // ---------------------------------------------------------------- puzzle

    public static bool PuzzleCompleted
    {
        get { return PlayerPrefs.GetInt(PuzzleKey, 0) == 1; }
        set { PlayerPrefs.SetInt(PuzzleKey, value ? 1 : 0); PlayerPrefs.Save(); }
    }

    // ----------------------------------------------------------------- bones

    private static int CollectedBonesMask
    {
        get { return PlayerPrefs.GetInt(BonesKey, 0); }
        set { PlayerPrefs.SetInt(BonesKey, value); PlayerPrefs.Save(); }
    }

    public static bool IsBoneCollected(int index)
    {
        if (index < 0 || index >= 32) return false;

        return (CollectedBonesMask & (1 << index)) != 0;
    }

    public static void MarkBoneCollected(int index)
    {
        if (index < 0 || index >= 32) return;

        CollectedBonesMask |= 1 << index;
    }

    public static int CollectedBoneCount
    {
        get
        {
            int bits = CollectedBonesMask;
            int count = 0;

            while (bits != 0)
            {
                count += bits & 1;
                bits >>= 1;
            }

            return count;
        }
    }

    // -------------------------------------------------------------- position

    private static string PositionKey(string scene, string part)
    {
        return "progress.pos." + scene + "." + part;
    }

    public static bool HasPosition(string scene)
    {
        return !string.IsNullOrEmpty(scene) && PlayerPrefs.GetInt(PositionKey(scene, "has"), 0) == 1;
    }

    public static Vector2 GetPosition(string scene)
    {
        return new Vector2(PlayerPrefs.GetFloat(PositionKey(scene, "x"), 0f),
                           PlayerPrefs.GetFloat(PositionKey(scene, "y"), 0f));
    }

    public static void SavePosition(string scene, Vector2 position)
    {
        if (string.IsNullOrEmpty(scene)) return;

        PlayerPrefs.SetFloat(PositionKey(scene, "x"), position.x);
        PlayerPrefs.SetFloat(PositionKey(scene, "y"), position.y);
        PlayerPrefs.SetInt(PositionKey(scene, "has"), 1);
        RememberSceneName(scene);
        PlayerPrefs.Save();
    }

    private static void RememberSceneName(string scene)
    {
        List<string> scenes = new List<string>(SavedScenes());
        if (scenes.Contains(scene)) return;

        scenes.Add(scene);
        PlayerPrefs.SetString(SceneListKey, string.Join(",", scenes));
    }

    private static string[] SavedScenes()
    {
        string stored = PlayerPrefs.GetString(SceneListKey, "");

        return string.IsNullOrEmpty(stored) ? new string[0] : stored.Split(',');
    }

    // ----------------------------------------------------------------- reset

    /// <summary>
    /// Wipes the saved game. Volume settings are left alone — they are the
    /// player's preference, not progress.
    /// </summary>
    public static void Clear()
    {
        PlayerPrefs.DeleteKey(PuzzleKey);
        PlayerPrefs.DeleteKey(BonesKey);

        foreach (string scene in SavedScenes())
        {
            PlayerPrefs.DeleteKey(PositionKey(scene, "x"));
            PlayerPrefs.DeleteKey(PositionKey(scene, "y"));
            PlayerPrefs.DeleteKey(PositionKey(scene, "has"));
        }

        PlayerPrefs.DeleteKey(SceneListKey);
        PlayerPrefs.Save();
    }

    /// <summary>True when there is a game worth continuing.</summary>
    public static bool HasSave
    {
        get { return PuzzleCompleted || CollectedBonesMask != 0 || SavedScenes().Length > 0; }
    }
}
