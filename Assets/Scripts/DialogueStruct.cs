using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public enum eDialogueType { tutorial, npc, goal, pickup }

[System.Serializable]
public struct sDialogueStruct
{
    public eDialogueType eCurrentDialogueType;
    public float fDialogueWaitTime;
    public string tTextToDisplay;
    public Text tTextComponent;
    public Color textColor;

}
