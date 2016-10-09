using UnityEngine;
using System.Collections;

public class Const
{
    #region Layer
    public static int LAYER_CHARACTER = 8;
    public static int LAYER_BED = 9;
    public static int LAYER_KITCHEN = 10;
    public static int LAYER_GETOUT = 11;
    public static int LAYER_OBJECTPICK = 12;
    public static int LAYER_REPAIRZONE = 13;
    public static int LAYER_FISHINGZONE = 14;
    public static int LAYER_MOVINGBOATZONE = 15;
    public static int LAYER_DETECTFISH = 16;
    public static int LAYER_BANDFISH = 17;
    public static int LAYER_ISLAND = 18;
    #endregion

    #region Enum
    public enum LAYER_ACTION_VOTE
    {
        BED = 9,
        KITCHEN = 10,
        GETOUT = 11,
        OBJECTPICK = 12
    }
    public enum LAYER_ACTION_PICK
    {
        REPAIRZONE = 13,
        FISHINGZONE = 14,
        MOVINGBOATZONE = 15
    }

    public enum TYPE_VOTE
    {
        ABSTENTION = 0,
        MAJORITE,
        ALEATOIRE,
        ALEATOIRE_PONDERE,
        ALEATOIRE_ELECTIVE
    }

    public enum BOOLEAN_VOTE
    {
        ABSTENTION = 0,
        YES,
        NO
    }

    public enum SCREEN
    {
        NONE = 0,
        CHOICE_VOTE,
        CHOICE_MAJORITE,
        CHOICE_ELECTIF,
        RESULT
    }
    #endregion
}
