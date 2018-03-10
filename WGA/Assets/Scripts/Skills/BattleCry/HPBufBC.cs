using System;

namespace Assets.Scripts.Skills.BattleCry
{
    class HPBufBC : ASkill
    {
        public HPBufBC()
        {
            Name = "HPBufBC";
            Description = "This skill add HP to ally cards";
            Ally = true;

            var inputParams = new[] { "HpBuf" };
            var dirs = new[] { Directions.Left, Directions.Right };
            Type = SkillType.BattleCry;

            Input = new HPBufBCInput
            {
                parentFunctionName = Name,
                inputParamsNames = inputParams,
                directions = dirs
            };
        }

        public struct HPBufBCInput : ISkillsInput
        {
            public string parentFunctionName;
            public string[] inputParamsNames;
            public string[] inputParamsValues;
            public Directions[] directions;

            string ISkillsInput.ParentFunctionName
            {
                get { return parentFunctionName; }
                set { parentFunctionName = value; }
            }
            string[] ISkillsInput.InputParamsNames
            {
                get { return inputParamsNames; }
                set { inputParamsNames = value; }
            }
            string[] ISkillsInput.InputParamsValues
            {
                get { return inputParamsValues; }
                set { inputParamsValues = value; }
            }
            Directions[] ISkillsInput.Directions
            {
                get { return directions; }
                set { directions = value; }
            }
        }

        public override bool ExecuteSkill(ISkillsInput input, int row, int col, int playerID, ref SlotBuff[,] bufMap)
        {
            var t = (HPBufBCInput) input;
            var buffedSlots = GetCardSlotsInDirections(bufMap, input.Directions, row, col);

            var n = Array.IndexOf(input.InputParamsNames, "HpBuf");
            var buf = input.InputParamsValues[n];
            for (var i = 0; i < buffedSlots.Length; i++)
            {
                if (playerID == 0)
                {
                    buffedSlots[i].StaticHPBufPlayer1 += int.Parse(buf);
                }
                else
                {
                    buffedSlots[i].StaticHPBufPlayer2 += int.Parse(buf);
                }              
            }

            return true;
        }
    }
}
