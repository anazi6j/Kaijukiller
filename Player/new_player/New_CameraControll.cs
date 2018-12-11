using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace robot
{
      public class New_CameraControll : MonoBehaviour
      {
          public bool LockOn;
          public float followSpeed = 9;
          public float controllerSpeed=7f;

          public Transform target;
          public Enemytarget lockonTarget;
          public Transform lockonTransform;


          Transform pivot;
          Transform camTrans;
          StateManager states;

          public float turnSmoothing = .1f;
          public float minAngle = -35f;
          public float maxAngle = 35f;

          public float SmoothX;
          public float SmoothY;
          public float smoothXvelocity;
          public float smoothYvelocity;
          public float lookAngle;
          public float tiltAngle;
          public bool usedRightAxis;
          public void Init(Transform t)
          {
              target = t;
              camTrans = Camera.main.transform;
              pivot = camTrans.parent;
          }
          //https://youtu.be/e9l2wxkYg2E?t=14m24s
          public void Tick(float d)//毎フレーム毎に呼ばれる
          {
              float h = Input.GetAxis("DS4_RanalogHorizontal");
              float v = Input.GetAxis("DS4_RanalogVertical");
              float targetSpeed = controllerSpeed;

              if (lockonTarget != null)
              {
                  if (lockonTransform == null)
                  {
                      

                      lockonTransform = lockonTarget.GetTarget();
                    states.lockOnTransform = lockonTransform;//ステート側にLockOntransformが参照されてない
                    
                    Debug.Log("カメラ:ロックオン");
                }

                  if (Mathf.Abs(h) > 0.6f)
                  {
                      if (!usedRightAxis)
                      {
                          lockonTransform = lockonTarget.GetTarget((h > 0));
                          states.lockOnTransform = lockonTransform;
                          usedRightAxis = true;
                      }
                  }
              }

              if(usedRightAxis)
              {
                  if(Mathf.Abs(h)<0.6f)
                  {
                      usedRightAxis = false;
                  }
              }





              FollowTarget(d);
              HandleRotations(d, v, h, targetSpeed);


          }
          void HandleRotations(float d,float v,float h,float targetspeed)
          {
              if (turnSmoothing > 0)
              {//http://megumisoft.hatenablog.com/entry/2015/08/29/210310
                  SmoothX = Mathf.SmoothDamp(SmoothX,h, ref smoothXvelocity,turnSmoothing);//smoothXからh（入力された値）にかけてスムーズに値が動く
                  SmoothY = Mathf.SmoothDamp(SmoothY, v, ref smoothYvelocity, turnSmoothing);
              }else
              {
                  SmoothX = h;
                  SmoothY = v;
              }
              tiltAngle -= SmoothY * targetspeed;
              tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
              pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

              if (LockOn && lockonTarget !=null)//ロックオンしたら
              {
                  Vector3 targetDir = lockonTransform.position - transform.position;
                  targetDir.Normalize();//ロックオンした対象とカメラの方向を取得する

                  if (targetDir == Vector3.zero)
                      targetDir = transform.forward;
                  Quaternion targetRot = Quaternion.LookRotation(targetDir);
                  transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, d * 9);//カメラはターゲットの方を滑らかに向く
                  return;
              }

              this.lookAngle += SmoothX * targetspeed; //(DS4左スティックのHorizontal（最大1)*回転スピード）をlookAngleに加算する。具体的に言うとDS4をちょびっと倒すとlookAngleの値がちょびっと加算され、加算された分だけカメラが回転するようになる（0の時は0を加算。つまり回らない）
              transform.rotation =Quaternion.Euler(0,lookAngle,0);//y座標を軸にしてカメラが水平に回転する。角度は


          }
          void FollowTarget(float d)//カメラが撮影対象についてくる
          {
              float speed = d * followSpeed;
              Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, d);
              transform.position = target.position;
          }
          public static New_CameraControll singleton;
          void Awake()
          {
              singleton = this;
          }
      }
  }
