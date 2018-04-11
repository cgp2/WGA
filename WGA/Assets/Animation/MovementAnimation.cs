using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimation : MonoBehaviour {
    public List<CardAction> actions;
    private bool needToMove = false;
    public enum Acts
    {
        move,
        stop,
        fight,
        destroy
    }
    public int totalTime;
    private int tickCounter;
    public class CardAction
    {
        public int ticks;
        public Directions dir;
        public Acts action;
        public float deltaPosition;
        public float deltaRotation;
        public CardAction(Acts act, Directions _dir, float range, int time = 0, bool rotation = false)
        {
            action = act;
            ticks = time==0 ? GetTimeFromAction(act) : time;
            dir = _dir;
            deltaPosition = range / ticks;
            if (rotation)
                deltaRotation = 180f / ticks;
            else
                deltaRotation = 0;
        }
        public int GetTimeFromAction(Acts act)
        {
            switch (act)
            {
                case Acts.fight:
                    return 30;

                case Acts.move:
                    return 30;

                case Acts.stop:
                    return 75;
                case Acts.destroy:
                    return 15;
                default:
                    return 0;

            }
        }
    }
    private void Awake()
    {
        actions = new List<CardAction>();
        totalTime = 0;
    }
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(needToMove)
        {
            switch(actions[0].action)
            {
                case Acts.move:
                    {
                        switch(actions[0].dir)
                        {
                            case Directions.Top:
                                {
                                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + actions[0].deltaPosition, this.transform.position.z);
                                    break;
                                }
                            case Directions.Bottom:
                                {
                                    this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - actions[0].deltaPosition, this.transform.position.z);
                                    break;
                                }
                            case Directions.Right:
                                {
                                    this.transform.position = new Vector3(this.transform.position.x + actions[0].deltaPosition, this.transform.position.y, this.transform.position.z);
                                    break;
                                }
                            case Directions.Left:
                                {
                                    this.transform.position = new Vector3(this.transform.position.x - actions[0].deltaPosition, this.transform.position.y, this.transform.position.z);
                                    break;
                                }
                        }
                        break;
                    }
                //case Acts.destroy:
                //    {
                //        this.GetComponent<Card>().Destroy(ref Battle.Board, ref GameObject.Find("Field").GetComponent<SkillMaster>().BufMap);
                //        Destroy(gameObject);
                //        break;
                //    }
            }
            if(actions[0].deltaRotation!=0)
            {
                this.transform.Rotate(0, actions[0].deltaRotation, 0);
            }
            tickCounter++;
            if (actions[0].ticks <= tickCounter)
            {
                if (actions[0].action == Acts.fight)
                {
                    if (this.GetComponent<Card>().Health <= 0)
                        Add_Action(Acts.destroy, Directions.Top, 0);
                    Battle.UpdateUI();
                }
                if(actions[0].action==Acts.destroy)
                {
                    this.GetComponent<Card>().Destroy(ref Battle.Board, ref GameObject.Find("Field").GetComponent<SkillMaster>().BufMap);
                    Destroy(gameObject);
                }
                totalTime -= actions[0].ticks;
                actions.RemoveAt(0);
                tickCounter = 0;
            }
            if (actions.Count == 0)
                needToMove = false;
        }
	}
    public void Add_Action(Acts act, Directions dir, float range, int time = 0, bool rotation = false)
    {
        if (time == 0)
        {
            
            actions.Add(new CardAction(act, dir, range, time, rotation));
            

        }
        else
        {
            actions.Add(new CardAction(act, dir, range, time,rotation));
        }
        totalTime += actions[actions.Count - 1].ticks;
        needToMove = true;
    }
   
  
}

