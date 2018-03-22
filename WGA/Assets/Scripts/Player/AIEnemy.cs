using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIEnemy : MonoBehaviour
{
    private Player possesedPlayer;
    private SkillMaster skillMaster;

    private void Awake()
    {
      
    }

    public void MakeMove()
    {
        var MaxPlacingActionUt = CalculateCardPlacingUtility();
    }

    private CardPlacingAction CalculateCardPlacingUtility()
    {
        var ret = new CardPlacingAction();
        var maxUtility = float.MinValue;
        for (var i = 0; i < possesedPlayer.deck.Count; i++)
        {
            var crdObj = possesedPlayer.deck[i];
       
            var field = Battle.Board;
            var crd = crdObj.GetComponent<Card>();

            for (int m = 0; m < Battle.m; m++)
            {
                for (int n = 0; n < Battle.n; n++)
                {
                    var utility = 0;

                    Battle.Board[m, n] = crd;

                    foreach (var input in crd.Info.BattleCryInput)
                    {
                        skillMaster.ExecuteSkillByInput(crd, input);
                    }

                    foreach (var input in crd.Info.AuraInput)
                    {
                        skillMaster.ExecuteSkillByInput(crd, input);
                    }

                    skillMaster.ApplyBufsToBoard(out field);

                    foreach (var card in field)
                    {
                        if (card)
                        {
                            if (card.Owner == possesedPlayer)
                            {
                                utility += card.Attack + card.Health + card.Shield;
                            }
                            else
                            {
                                utility -= card.Attack + card.Health + card.Shield;
                            }
                        }
                    }

                    if (utility > maxUtility)
                    {
                        maxUtility = utility;

                        ret = new CardPlacingAction
                        {
                            Utillity = utility,
                            CardNum = i,
                            Row = m,
                            Col = n,    
                        };
                    }

                    foreach (var input in crd.Info.BattleCryInput)
                    {
                        skillMaster.ReExecuteSkillByInput(crd, input);
                    }

                    foreach (var input in crd.Info.AuraInput)
                    {
                        skillMaster.ReExecuteSkillByInput(crd, input);
                    }
                }
            }

       
        }

        return ret;
    }

    private MovementAction CalculateMovementutility()
    {
        var ret = new MovementAction();

        return ret;
    }

	// Use this for initialization
	void Start ()
	{
	    possesedPlayer = GetComponentInParent<Player>();
        skillMaster = GameObject.Find("Field").GetComponent<SkillMaster>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private struct MovementAction
    {
        public int Utillity;
        public Directions Direction;
    }

    private struct CardPlacingAction
    {
        public int Utillity;
        public int CardNum;
        public int Row;
        public int Col;
    }

}
