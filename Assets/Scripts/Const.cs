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
    #endregion

    #region Enum
    public enum LAYER_ACTION_VOTE
    {
        BED = 9,
        KITCHEN = 10,
        GETOUT = 11,
        OBJECTPICK = 12
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
