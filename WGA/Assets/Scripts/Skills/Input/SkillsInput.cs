using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Directions { Top, Bottom, Left, Right, LeftTop, LeftBottom, RightTop, RightBottom, Map }

public struct SkillsInput
{
    public string ParentFunctionName { get; set; }

    public string[] InputParamsNames { get; set; }

    public string[] InputParamsValues { get; set; }

    public Directions[] Directions { get; set; }
}
