using UnityEngine;
using System.Collections;
using Assets.Script.Frame;

public enum EventId
{
    EventProgress,
    EventCount,
}

public class EventProgress : XEvent
{
    public float progress;
    public EventProgress(float progress)
        : base((int)EventId.EventProgress, null)
    {
        this.progress = progress;
    }
}
