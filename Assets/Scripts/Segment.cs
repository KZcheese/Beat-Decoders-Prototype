using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Segment : MonoBehaviour
{
    private List<bool> _notes;
    public List<bool> sol;
    private AudioSource _instrument;
    private Image _image;
    private Sprite _emptySegment;

    private void Start()
    {
        _image = GetComponent<Image>();
        _emptySegment = _image.sprite;
    }

    public void SetInstrument(AudioSource audioSource)
    {
        _instrument = audioSource;
        _notes = new List<bool>(sol.Count);
    }
    
    public void PlaySeg(int bpm, double startTime)
    {
        for (int i = 0; i < _notes.Count; i++)
        {
            _instrument.PlayScheduled(startTime + (i * bpm));
        }
    }

    public void SetBeats(Sprite image, List<bool> beats)
    {
        _image.sprite = image;
        _notes = beats;
    }

    public int GetBeatCount()
    {
        return _notes.Count;
    }
}
