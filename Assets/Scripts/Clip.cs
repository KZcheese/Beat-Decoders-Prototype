using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Clip : Draggable
{
    public List<bool> notes;
    private Image _image;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
