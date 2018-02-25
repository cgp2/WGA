using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

public abstract class Abstract_Skills
{
    public List<string> SkillsNames;
    public List<ISkillsInput> SkillsInput;

    public Abstract_Skills()
    {

    }

    public static void ExecuteSkillByName(string name)
    {
        
    }

    private void _executeSkillByName(string name)
    {
        MethodInfo mi = this.GetType().GetMethod("aa");
        mi.Invoke(this, new object[] {
          SkillsInput[0]
        });
    }

    public static void GetSkillInputByName(string name)
    {

    }
}

