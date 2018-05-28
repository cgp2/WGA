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
            Dirs = new[] {Directions.Bottom, Directions.Left, Directions.Right, Directions.Top};
            Type = SkillType.Aura;

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

            var n = Array.IndexOf(input.InputParamsNames, "DMGBuf");
            var buf = input.InputParamsValues[n];
            for (var i = 0; i < buffedSlots.Length; i++)
            {
                if (playerID == 0)
                {
                    if (Ally == true)
                    {
                        buffedSlots[i].FloatingDMGBufPlayer1 -= int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].FloatingDMGBufPlayer2 -= int.Parse(buf);
                    }
                }
                else
                {
                    if (Ally == true)
                    {
                        buffedSlots[i].FloatingDMGBufPlayer2 -= int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].FloatingDMGBufPlayer1 -= int.Parse(buf);
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

            var n = Array.IndexOf(input.InputParamsNames, "DMGBuf");
            var buf = input.InputParamsValues[n];
            for (var i = 0; i < buffedSlots.Length; i++)
            {
                if (playerID == 0)
                {
                    if (Ally == true)
                    {
                        buffedSlots[i].FloatingDMGBufPlayer1 += int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].FloatingDMGBufPlayer2 += int.Parse(buf);
                    }
                }
                else
                {
                    if (Ally == true)
                    {
                        buffedSlots[i].FloatingDMGBufPlayer2 += int.Parse(buf);
                    }
                    else
                    {
                        buffedSlots[i].FloatingDMGBufPlayer1 += int.Parse(buf);
                    }
                }
            }

            ApplyBufToBufMap(buffedSlots, ref bufMap);

            return true;
        }

    }
}
