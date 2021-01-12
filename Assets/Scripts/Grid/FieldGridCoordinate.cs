using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGridCoordinate
{
    public int X { get; set; }
    public int Z { get; set; }
    public FieldGridCoordinate(int x, int z)
    {
        X = x;
        Z = z;
    }

    public override bool Equals(object obj)
    {
        if(obj == null || !(obj is FieldGridCoordinate))
        {
            return false;
        }
        FieldGridCoordinate tmp = (FieldGridCoordinate)obj;
        return tmp.X == X && tmp.Z == Z;
    }



    public override string ToString()
    {
        return "[" + X + "," + Z + "]";
    }

    public override int GetHashCode()
    {
        int hashCode = 1911744652;
        hashCode = hashCode * -1521134295 + X.GetHashCode();
        hashCode = hashCode * -1521134295 + Z.GetHashCode();
        return hashCode;
    }
}
