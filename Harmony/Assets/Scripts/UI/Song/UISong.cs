using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISong : MonoBehaviour
{
    [SerializeField] private Transform _parent;
    [SerializeField] private GameObject _itemPrefab;

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        ResetItem();
    }

    private void Initialize()
    {
        foreach (var song in SingletonHub.Instance.Get<SoundManager>()._librarySong.SongRthym)
        {
            GameObject itemSpawn = Instantiate(_itemPrefab, _parent);
            itemSpawn.GetComponent<UISongItem>().Initialization(song.SongID, song.DisplayName);
        }
    }

    public void ResetItem()
    {
        foreach (Transform item in _parent)
        {
            Destroy(item.gameObject);
        }
    }
}
