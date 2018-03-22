using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Skills.Aura
{
    class DmgBufAura : ASkill
    {
        public DmgBufAura()
        {
            Name = "DMGBufAura";
            Description = "This aura add DMG to ally cards";
            Ally = true;

            InputParametrs = new[] {"DMGBuf"};
            InputValues = new[] {"3"};
            Dirs = new[] {Directions.Left, Directions.Right, Directions.Top, Directions.Bottom};
            Type = SkillType.Aura;

            Input = new SkillsInput()
            {
                ParentFunctionName = Name,
                InputParamsNames = InputParametrs,
                InputParamsValues = InputValues,
                Directions = Dirs
            };
        }

        //public struct HPBufBCInput : SkillsInput
        //{
        //    public string parentFunctionName;
        //    public string[] inputParamsNames;
        //    public string[] inputParamsValues;
        //    public Dirs[] directions;

        //    string SkillsInput.ParentFunctionName
        //    {
        //        get { return parentFunctionName; }
        //        set { parentFunctionName = value; }
        //    }

        //    string[] SkillsInput.InputParamsNames
        //    {
        //        get { return inputParamsNames; }
        //        set { inputParamsNames = value; }
        //    }

        //    string[] SkillsInput.InputParamsValues
        //    {
        //        get { return inputParamsValues; }
        //        set { inputParamsValues = value; }
        //    }

        //    Dirs[] SkillsInput.Dirs
        //    {
        //        get { return directions; }
        //        set { directions = value; }
        //    }
        //}

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

    }
}
