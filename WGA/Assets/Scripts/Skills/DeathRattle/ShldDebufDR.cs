using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Skills.DeathRattle
{
    class ShldDebufDR : ASkill
    {
        public ShldDebufDR()
        {
            Name = "SHLDDebufDR";
            Description = "This skill reduce SHLD to eneme cards on card death";
            Ally = false;

            InputParametrs = new[] { "ShldBuf" };
            InputValues = new[] { "-1" };
            Dirs = new[] { Directions.Map};
            Type = SkillType.DeathRattle;

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

            var n = Array.IndexOf(input.InputParamsNames, "ShldBuf");
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
                        buffedSlots[i].StaticShieldBufPlayer2 += int.Parse(buf);
                    }
                }
                else
                {
                    if (Ally)
                    {
                        buffedSlots[i].StaticShieldBufPlayer2 += int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].StaticShieldBufPlayer1 += int.Parse(buf);
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

            var n = Array.IndexOf(input.InputParamsNames, "ShldBuf");
            var buf = input.InputParamsValues[n];
            for (var i = 0; i < buffedSlots.Length; i++)
            {
                if (playerID == 0)
                {
                    if (Ally)
                    {
                        buffedSlots[i].StaticHPBufPlayer1 -= int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].StaticShieldBufPlayer2 -= int.Parse(buf);
                    }
                }
                else
                {
                    if (Ally)
                    {
                        buffedSlots[i].StaticShieldBufPlayer2 -= int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].StaticShieldBufPlayer1 -= int.Parse(buf);
                    }
                }
            }

            ApplyBufToBufMap(buffedSlots, ref bufMap);

            return true;
        }
    }
}
