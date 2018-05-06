using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Skills.Active
{
    class DmgToCardAS : ASkill
    {
        public DmgToCardAS()
        {
            Name = "DmgToCard";
            Description = "This skill do some damage (interesting)";
            Ally = false;

            InputParametrs = new[] { "DmgTo" };
            InputValues = new[] { "5" };
            Dirs = new[] { Directions.Map };
            Type = SkillType.Active;

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

            var n = Array.IndexOf(input.InputParamsNames, "DmgTo");
            var buf = input.InputParamsValues[n];
            for (var i = 0; i < buffedSlots.Length; i++)
            {
                if (Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col] != null)
                {
                    if (playerID == 0)
                    {
                        if (Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield >= int.Parse(buf))
                            buffedSlots[i].StaticShieldBufPlayer2 -= int.Parse(buf);
                            
                        if (Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield <= int.Parse(buf))
                        {
                            buffedSlots[i].StaticShieldBufPlayer2 -= Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield;
                            buffedSlots[i].StaticHPBufPlayer2 -= int.Parse(buf) - Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield;
                        }

                    }
                    else
                    {
                        if (Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield >= int.Parse(buf))
                            buffedSlots[i].StaticShieldBufPlayer1 -= int.Parse(buf);
                        if (Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield <= int.Parse(buf))
                        {
                            buffedSlots[i].StaticShieldBufPlayer1 -= Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield;
                            buffedSlots[i].StaticHPBufPlayer1 -= int.Parse(buf) - Battle.Board[buffedSlots[i].Row, buffedSlots[i].Col].Shield;
                        }
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
