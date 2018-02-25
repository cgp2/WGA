using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Directions { Top, Bottom, Left, Right, Left_Top, Left_Bottom, Right_Top, Right_Bottom, Map }

interface ISkillsInput
{
    string ParentFunctionName
    {
        get;
        set;
    }
    string[] InputParamsNames
    {
        get;
        set;
    }
    Directions[] Directions
    {
        get;
        set;
    }
}
