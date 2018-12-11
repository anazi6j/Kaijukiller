using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
     public class AttackActionManager : MonoBehaviour
      {
        public int actionIndex;
        public List<Action> actionSlots = new List<Action>();
       
          StateManager state;
          public void Init(StateManager st)
          {

              state = st;


              UpdateAction();

          }


          public void UpdateAction()
          {

            //EmptyAllSlots();

            //StaticFunction.DeepCopyAction(state.Ainfo.curAction, ActionInput.r1, ActionInput.r1,actionSlots);
            //StaticFunction.DeepCopyAction(state.Ainfo.curAction, ActionInput.r2, ActionInput.r2, actionSlots);

        }


        

        
        


        

        /*
        public void DeepCopyStepsList(Action from,Action to)
        {
            to.steps = new List<ActionSteps>();
            for(int i=0;i<from.steps.Count;i++)
            {
                ActionSteps step = new ActionSteps();
                DeepCopyStep(from.steps[i], step);
                to.steps.Add(step);
            }
        }
        */
        //

        public void DeepCopyStep(ActionSteps from,ActionSteps to)
        {
            to.branches = new List<ActionAnim>();

            for(int i=0;i<from.branches.Count;i++)
            {
                ActionAnim a = new ActionAnim();
                a.input = from.branches[i].input;
                a.targetAnim = from.branches[i].targetAnim;
                to.branches.Add(a);
            }

        }



          AttackActionManager()
          {

              for (int i = 0; i < 4; i++)
              {
                  Action a = new Action();
                  a.input = (ActionInput)i;
                  actionSlots.Add(a);
              }

          }

          void EmptyAllSlots()//アクションスロット（アニメーションとそれを呼び出すボタン入力）情報を初期化する
          {
              for (int i = 0; i < 4; i++)
              {
                Action a = StaticFunction.GetAction((ActionInput)i, actionSlots);
                //a.steps = null;
                a.type = ActionType.attack;
              }
          }


          public Action GetActionSlot(StateManager st)
          {
              ActionInput a_input = GetActionInput(st);
            return StaticFunction.GetAction(a_input, actionSlots);
          }

         

          public ActionInput GetActionInput(StateManager st)
          {
              if (st.r1)
                  return ActionInput.r1;

              if (st.r2) {
                  Debug.Log("r2");
                  return ActionInput.r2;
              }
              if (st.l1)
              {
                  Debug.Log("l1");
                  return ActionInput.l1;
              }
              if (st.l2)
                  return ActionInput.l2;

              return ActionInput.r1;
          }
      }
     public enum ActionInput
      {
          r1,l1,r2,l2
      }

    public enum ActionType
    {
        attack,block,parry
    }

      [System.Serializable]
      public class Action
      {
        
          public ActionInput input;
          public ActionType type;
        public string targetAnim;
        
       
        //public List<ActionSteps> steps;

        ActionSteps defaultsteps;
        /*
        public ActionSteps GetActionSteps(ref int indx)
        {
           
            if(steps ==null ||steps.Count == 0)//Actionstepsの動的配列stepsが空っぽだったら
            {
                if (defaultsteps == null)
                {
                    defaultsteps = new ActionSteps();//Actinstepsインスタンスを動的に生成する
                    defaultsteps.branches = new List<ActionAnim>();//ActionAnimインスタンスを動的に生成する
                    ActionAnim aa = new ActionAnim();
                    aa.input = input;//ActionAnimに割り当てられるボタンを決定する
                    aa.targetAnim = targetAnim;//ActionAnimに割り当てられるアニメーションを決定する
                    defaultsteps.branches.Add(aa);//branchにActionAnimの変数aaを追加する（アニメーションとボタンを決定する）
                }
                
            return defaultsteps;
            }
            if (indx > steps.Count - 1)
                indx = 0;
            ActionSteps retval = steps[indx];
            if (indx > steps.Count - 1)
                indx= 0;

            else
                indx++;

            return retval;
        }
        */
      }

    [System.Serializable]
    public class ActionSteps
    {
        public List<ActionAnim> branches = new List<ActionAnim>();
        public ActionAnim GetBranch(ActionInput inp)
        {
            for(int i=0;i<branches.Count;i++)
            {
                if (branches[i].input == inp)
                    return branches[i];
            }
            return branches[0];
        }

    }

    [System.Serializable]
    public class ActionAnim
    {
        public string targetAnim;
        public ActionInput input;
        public float attackpower;
    }

    

       
            /*Attackクラスには、攻撃力を格納するfloat power,アニメーションを格納するstring attackanim
            * 攻撃をはじかれるかどうかのbool parriedなどを格納する*/


        }

       


   
