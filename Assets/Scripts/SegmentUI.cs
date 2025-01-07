using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SegmentUI : MonoBehaviour
{
    private Image[] _tiles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Image background = GetComponent<Image>();
        _tiles = GetComponentsInChildren<Image>(true).Where(i => i != background).ToArray();
    }

    public void UpdateTiles(List<bool> notes)
    {
        for (int i = 0; i < _tiles.Length; i++)
        {
            _tiles[i].gameObject.SetActive(notes[i]);
        }
    }
}
