using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyColors
{
    public static Color Background_UI_Mechanic_Parts { get { return _background_ui_mechanic_parts; } }
    public static Color Background_UI_Plants { get { return _background_ui_plants; } }
    public static Color Background_UI_Electronic_Parts { get { return _background_ui_electronic_parts; } }

    [SerializeField]
    public static Color _background_ui_plants = new Color32(42, 255,68,255);
    private static Color _background_ui_mechanic_parts = new Color32(173,173,169,255);
    private static Color _background_ui_electronic_parts = new Color32(248,222,20,255);

    public static Color GetBackgroundColorByObjectType(int objectType)
    {
        switch (objectType)
        {
            case 1:
                return Background_UI_Plants;
            case 2:
                return Background_UI_Electronic_Parts;
            case 3:
                return Background_UI_Mechanic_Parts;
            default:
                throw new ItemException("No Background color for object type " + objectType + " found.");
        }
    }
}
