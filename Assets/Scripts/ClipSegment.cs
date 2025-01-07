using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SegmentUI))]
public class ClipSegment : DraggableSegment, IBeginDragHandler
{
    private Vector2 _startLocation;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _startLocation = transform.position;
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        base.OnEndDrag(eventData);
        transform.position = _startLocation;
    }
}