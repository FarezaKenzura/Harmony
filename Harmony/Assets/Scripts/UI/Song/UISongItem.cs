using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISongItem : MonoBehaviour
{
    [SerializeField] private TMP_Text _titleText;
    private string _songID;

    public void Initialization(string id, string displayName)
    {
        _songID = id;
        _titleText.text = displayName;
    }

    public void ClickSong()
    {
        GameDataController.SelectedSongName = _songID;
        SceneManager.LoadScene("Gameplay");
    }
}
