using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Assets.Scripts.Skills
{
    class HPBufBC : ASkill
    { 

        public HPBufBC()
        {
            Name = "HP_Buf";
            description = "This skill add HP to ally cards";
            Ally = true;

            string[] inputParams = new string[] { "HpBuf", "DMGBuf" };
            Directions[] dirs = new Directions[] { Directions.Left, Directions.Right };
            Type = SkillType.BattleCry;

            HPBufBCInput input = new HPBufBCInput();
            input.parentFunctionName = Name;
            input.inputParamsNames = inputParams;
            input.directions = dirs;

            Input = input;
        }

        struct HPBufBCInput : ISkillsInput
        {
            public string parentFunctionName;
            public string[] inputParamsNames;
            public Directions[] directions;

            string ISkillsInput.ParentFunctionName
            {
                get { return parentFunctionName; }
                set { parentFunctionName = value; }
            }
            string[] ISkillsInput.InputParamsNames
            {
                get {
                    return inputParamsNames;
                }
                set
                {
                    inputParamsNames = value;
                }
            }
            Directions[] ISkillsInput.Directions
            {
                get
                {
                    return directions;
                }
                set
                {
                    directions = value;
                }
            }
        }


        public override bool ExecuteSkill(ISkillsInput input)
        { 
            throw new NotImplementedException();
        }
    }
}
