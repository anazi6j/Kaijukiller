using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
     public class AttackActionManager : MonoBehaviour
      {

          public List<Action> actionSlots = new List<Action>();
          StateManager state;
          public void Init(StateManager st)
          {

              state = st;


              UpdateAction();

          }


          public void UpdateAction()//ここを変える必要はない
          {
              Weapon w = state.Ainfo.curAction;
              for (int i = 0; i < w.actions.Count; i++)//PlayerAttackInfoManagerに登録されているactionの数だけ
              {
                  Action a = GetAction(w.actions[i].input);//①入力ボタンとそれに対応して呼び出されるアニメーションを格納するアクションを生成する。
                  a.targetAnim = w.actions[i].targetAnim;//②：GetActionから返された情報を元に生成されたactionのアニメーションを割り当てる
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
                  Action a = GetAction((ActionInput)i);
                  a.targetAnim = null;
              }
          }


          public Action GetActionSlot(StateManager st)
          {
              ActionInput a_input = GetActionInput(st);
              return GetAction(a_input);
          }

          public Action GetAction(ActionInput inp)
          {
              for(int i = 0; i < actionSlots.Count; i++)
              {
                  if (actionSlots[i].input == inp)
                      return actionSlots[i];
              }

              return null;
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

      [System.Serializable]
      public class Action
      {
          public ActionInput input;
          public string targetAnim;
      }
  }
