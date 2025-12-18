using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "Data/Song Data")]
public class SOSong : ScriptableObject
{
    public List<SongEntry> SongRthym;
}
