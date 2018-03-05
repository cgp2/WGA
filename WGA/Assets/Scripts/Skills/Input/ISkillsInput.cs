using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Directions { Top, Bottom, Left, Right, LeftTop, LeftBottom, RightTop, RightBottom, Map }

public interface ISkillsInput
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

    string[] InputParamsValues
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
