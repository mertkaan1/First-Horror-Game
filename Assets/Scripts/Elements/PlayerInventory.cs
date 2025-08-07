using System.Collections.Generic;
using UnityEngine;

public static class PlayerInventory
{
    private static HashSet<int> ownedKeys = new HashSet<int>();

    public static void AddKey(int keyId)
    {
        if (!ownedKeys.Contains(keyId))
        {
            ownedKeys.Add(keyId);
            Debug.Log($"Anahtar {keyId} envantere eklendi.");
        }
    }

    public static bool HasKey(int keyId)
    {
        return ownedKeys.Contains(keyId);
    }
}