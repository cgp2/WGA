using System;

namespace Assets.Scripts.Skills.BattleCry
{
    class HPBufBC : ASkill
    {
        public HPBufBC()
        {
            Name = "HPBufBC";
            Description = "This skill add HP to ally cards on card play";
            Ally = true;

            InputParametrs = new[] { "HpBuf" };
            InputValues = new[] {"2"};
            Dirs = new[] { Directions.Self };
            Type = SkillType.BattleCry;

            Input = new SkillsInput()
            {
                ParentFunctionName = Name,
                InputParamsNames = InputParametrs,
                InputParamsValues = InputValues,
                Directions = Dirs
            };
        }

        public override bool ExecuteSkill(SkillsInput input, int row, int col, int playerID, ref SlotBuff[,] bufMap)
        {
            var t = input;
            var buffedSlots = GetCardSlotsInDirections(ref bufMap, input.Directions, playerID, row, col);

            var n = Array.IndexOf(input.InputParamsNames, "HpBuf");
            var buf = input.InputParamsValues[n];
            for (var i = 0; i < buffedSlots.Length; i++)
            {
                if (playerID == 0)
                {
                    if (Ally)
                    {
                        buffedSlots[i].StaticHPBufPlayer1 += int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].StaticHPBufPlayer2 += int.Parse(buf);
                    }
                }
                else
                {
                    if (Ally)
                    {
                        buffedSlots[i].StaticHPBufPlayer2 += int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].StaticHPBufPlayer1 += int.Parse(buf);
                    }
                }              
            }

            ApplyBufToBufMap(buffedSlots, ref bufMap);

            return true;
        }

        public override bool ReExecuteSkill(SkillsInput input, int row, int col, int playerID, ref SlotBuff[,] bufMap)
        {
            var t = input;
            var buffedSlots = GetCardSlotsInDirections(ref bufMap, input.Directions, playerID, row, col);

            var n = Array.IndexOf(input.InputParamsNames, "HpBuf");
            var buf = input.InputParamsValues[n];
            for (var i = 0; i < buffedSlots.Length; i++)
            {
                if (playerID == 0)
                {
                    buffedSlots[i].StaticHPBufPlayer1 -= int.Parse(buf);
                }
                else
                {
                    buffedSlots[i].StaticHPBufPlayer2 -= int.Parse(buf);
                }
            }
            ApplyBufToBufMap(buffedSlots, ref bufMap);

            return true;
        }
    }
}
