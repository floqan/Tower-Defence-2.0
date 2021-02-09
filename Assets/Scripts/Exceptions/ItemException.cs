using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemException : Exception
{
    public ItemException() 
    { 
    } 
    
    public ItemException(string message): base(message)
    {
    }

    public ItemException(string message, Exception inner): base(message, inner) 
    { 
    }
}
