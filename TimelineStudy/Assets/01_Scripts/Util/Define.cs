using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Sound
    {
        SFX,
        BGM,
        MaxCount
    }

    public enum ModeState
    {
        Tracking,
        Content,
    }
    
    public enum Scene
    {
        Unknow,
        Menu,
        Game
    }
    
    public enum DataCodes
    {
        BookData,
        BookCode,
        TrackableImageData,

        Elements,
        FallBall_Experiment,
        DropBalloon_Experiment
    }

    public enum UIEvent
    {
        Click
    }

    public enum Fall
    {
        FreeDrop,
        VerticalDrop
    }
}