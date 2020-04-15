using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMaskable 
{
    Boolean isMasked { get; }
    void mask();
}
