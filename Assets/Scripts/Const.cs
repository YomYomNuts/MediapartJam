using UnityEngine;
using System.Collections;

public class Const
{
    #region Layer
    public static int LAYER_CHARACTER = 8;
    public static int LAYER_BED = 9;
    #endregion

    #region Enum
    public enum LAYER_ACTION_VOTE
    {
        BED = 9,
        KITCHEN = 10,
        GETOUT = 11
    }

    public enum TYPE_VOTE
    {
        NONE = 0,
        MAJORITE,
        ALEATOIRE,
        ALEATOIRE_PONDERE,
        ALEATOIRE_ELECTIVE,
        LENGHT
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
