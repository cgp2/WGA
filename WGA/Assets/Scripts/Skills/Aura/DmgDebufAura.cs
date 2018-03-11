using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Skills.Aura
{
    class DmgDebufAura : DmgBufAura
    {
        public DmgDebufAura() : base()
        {
            Name = "DMGDebufAura";
            Description = "This aura reduce DMG to enemy cards";
            Ally = false;

            InputParametrs = new[] { "DMGBuf" };
            InputValues = new[] { "-2" };
            Dirs = new[] {Directions.Top};
            Type = SkillType.Aura;

            Input = new SkillsInput()
            {
                ParentFunctionName = Name,
                InputParamsNames = InputParametrs,
                InputParamsValues = InputValues,
                Directions = Dirs
            };
        }


    }
}
