using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnusedManager : MonoBehaviour
{
    public static UnusedManager instance;

    public List<ReuseGO> unusedReuseGOs = new List<ReuseGO>();

    private void Awake()
    {
        instance = this;
    }

    public ReuseGO _GetReuseGO(string id)
    {
        ReuseGO selected = null;

        foreach (var _t in CollectionMarshal.AsSpan(unusedReuseGOs))
        {
            if (id != _t.itemID) continue;
            selected = _t;

            break;
        }

        if (!ReferenceEquals(selected,null))
        {
            unusedReuseGOs.Remove(selected);
        }

        return selected;
    }

    public void _AddToUnusedGO(ReuseGO go)
    {
        go.gameObject.SetActive(false);

        go.transform.parent = transform;

        unusedReuseGOs.Add(go);
    }

    public void _AddToUnusedMonster(ReuseGO go)
    {
        go.gameObject.SetActive(false);

        go.transform.parent = transform;

        PlayerManager.instance.spawnedMonsters.Remove(go);

        unusedReuseGOs.Add(go);
    }

    public PlayerAIController _GetUnusedPlayerAI(string id)
    {
        PlayerAIController selected = null;

        foreach (var _t in CollectionMarshal.AsSpan(unusedReuseGOs))
        {
            if (id != _t.itemID) continue;
            selected = _t.GetComponent<PlayerAIController>();
            break;
        }

        if (!ReferenceEquals(selected,null))
        {
            unusedReuseGOs.Remove(selected.GetComponent<ReuseGO>());
        }

        return selected;
    }

    public void _AddToUnusedPlayerAI(PlayerAIController go)
    {
        go.gameObject.SetActive(false);

        go.transform.parent = transform;

        PlayerManager.instance.spawnedAIPlayers.Remove(go);

        unusedReuseGOs.Add(go.GetComponent<ReuseGO>());
    }

    public PlayerController _GetUnusedPlayer(string id)
    {
        PlayerController selected = null;

        foreach (var _t in CollectionMarshal.AsSpan(unusedReuseGOs))
        {
            if (id != _t.itemID) continue;
            selected = _t.GetComponent<PlayerController>();

            break;
        }

        if (!ReferenceEquals(selected,null))
        {
            unusedReuseGOs.Remove(selected.GetComponent<ReuseGO>());
        }

        Debug.Log("Get unused player");

        return selected;
    }

    public void _AddToUnusedPlayer(PlayerController go)
    {
        go.gameObject.SetActive(false);

        go.transform.parent = transform;

        PlayerManager.instance.spawnedPlayer = null;

        unusedReuseGOs.Add(go.GetComponent<ReuseGO>());
    }
}
