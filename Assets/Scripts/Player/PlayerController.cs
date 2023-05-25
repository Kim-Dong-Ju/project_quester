using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //RaycastHit hit;
    float fMaxDistance = 40f; // Ray Distance(Length)
    GameObject HitObject, dragAnchor;
    // GameObject PlayerObj, CenterObj;
    private Vector2 vCurPos, vLastPos;
    private Vector3 vMovePos;
    private float fMoveSpeed = 0.05f;
   
    Vector3 vFirstPoint, vSecondPoint;
    float xAngle, yAngle, xAngleTemp, yAngleTemp, oldAngle, fLastArea, fNewArea;
   // public bool bIsItemClicked = false;
    
    [HideInInspector]
    public Vector3 camToFloor, offset, vec;
    // private enum cameraMode {
    //     None, Move, Rotate, Zoom // None is None & ItemHandle
    // };
    private enum whichClicked {
        None, PSMain, PSSub, PSToggle, PSWheel, SRed, ERed, SBlack, EBlack,

    };
   // cameraMode CurMode;
    whichClicked CurClick;
    public bool check = true;
    // public bool bSwitchClicked = false;
    // public bool bBtnClicked = false;
    // public bool bWheelClicked = false;
    private float dist;
    private bool dragging = false;
    private Transform toDrag;
    public bool bIsCameraControl = false;
    public float fPerspectZoomSpeed = 0.1f;
    public float fOrthoZoomSpeed = 0.1f;
    
    // Camera Camera;
    void Start()
    {
        // PlayerObj = transform.parent.gameObject;
        vLastPos = transform.position;
        oldAngle = -1.0f;
        xAngle = transform.eulerAngles.x;
        yAngle = transform.eulerAngles.y;
        CurClick = whichClicked.None;
   //     CurMode = cameraMode.None;
        // 현재 카메라가 보고 있는 방향의 회전 각을 xAngle과 yAngle에 저장
    }

    // Update is called once per frame
    // 오브젝터 제어 -> 카메라 제어 순으로 반복할 예정
    void Update()
    {
        if(!bIsCameraControl) LineTraceFunc();
        // 현재 카메라 제어 중이 아닐 경우엔 오브젝트 제어

        else CameraControl();
        
        // 현재 카메라 제어 중이라면 카메라부터 제어

        if(CurClick == whichClicked.None)
            check = true;
        // if(!bIsItemClicked)
        // RotateCamera();

        // if(bSwitchClicked) StartCoroutine(WaitForIt(0.1f));       
        // else check = true;
           // StartCoroutine(WaitForIt(0.05f));
    }

    // 입력 시간 제한용 함수(일단 미사용)
    IEnumerator WaitForIt(float time)
    {
        if(!check) 
        {
            yield return new WaitForSeconds(time);
            check = true;
         //   bSwitchClicked = false;
        }
    }

    // 카메라 제어 함수
    private void CameraControl()
    {
        vMovePos = Vector3.zero; 
        // 터치가 이동한 좌표를 영벡터로 초기 설정

        Touch[] touch = new Touch[3];
        // Touch 구조체 데이터를 배열로 3개 생성
        // (터치 1개 - 카메라 회전, 터치 2개 - 카메라 움직임, 터치 3개 - 카메라 줌인, 줌아웃)

        bIsCameraControl = true;
        // 카메라 컨트롤 시 제어 중이라고 설정

        // Camera Rotate
        if(Input.touchCount == 1)  // 터치 1개 -> 카메라 회전
        {
            touch[0] = Input.GetTouch(0); 
            // 0번 터치(한 손가락 터치) 값 가져옴
            if(touch[0].phase == TouchPhase.Began) // 터치를 눌렀을 때(첫 입력 시)
            {
                vFirstPoint = touch[0].position - touch[0].deltaPosition;
                // touch[0]의 위치 좌표(position)에서 (가장 마지막 프레임에서 발생했던 위치 좌표와 현재 프레임에서 발생한 터치 위치의 차이)(deltaPosition)를 빼서 FirstPosition로 저장 
                
                xAngleTemp = xAngle;
                yAngleTemp = yAngle * -1.0f;
                // 마지막까지 저장되었던 x축 각과 y축 각을 각이 변하는 동안 임시로 저장할 temp 변수에 저장
                // y 각은 negation이 되야 하므로 -1를 곱함.
                // -1을 곱하지 않으면 아래로 터치를 움직이면 카메라가 위로 올라감
                //CurMode = cameraMode.Rotate;
            }

            if(touch[0].phase == TouchPhase.Moved) // 터치를 움직였을 때
            {
                vSecondPoint = touch[0].position - touch[0].deltaPosition;
                // touch[0]의 위치 정보를 vSecondPoint에 저장

                xAngle = xAngleTemp + (vSecondPoint.x - vFirstPoint.x) * 180 / Screen.width;
                yAngle = (yAngleTemp + (vSecondPoint.y - vFirstPoint.y) * 90 * 3f / Screen.height) * -1.0f;
                // x축 각과 y축 각을 최초 입력 시 좌표와 이동할 때의 좌표의 차이(벡터)를 스크린 비율에 맞게 변경 후 최종 저장

                // 이 코드는 안 씀
               // var v2 = vFirstPoint - vSecondPoint;

                // 이 코드도 안 씀
                // var newAngle = Mathf.Atan2(v2.y, v2.x);
                // newAngle = newAngle * 180 / Mathf.PI;

                // 이코드도 안 씀
                // if(oldAngle < 0) oldAngle = newAngle;
                // var deltaAngle = Mathf.DeltaAngle(newAngle, oldAngle);
                // oldAngle = newAngle;

                // if(yAngle < 50f) yAngle = 50f;
                // if(yAngle > 105f) yAngle = 105f;
                // // y축 각을 50에서 105도 사이로 조정

                // 이 아래 코드도 미사용
                // Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                // RaycastHit hit;
                // Vector3 position = new Vector3();
                // // 이 코드도 미사용
                // if(Physics.Raycast(ray, out hit, fMaxDistance))
                // {
                //     position = hit.transform.position;
                // }
                //PlayerObj.transform.RotateAround(position, Vector3.up, -deltaAngle);
                // 여기까지 미사용
                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(yAngle, xAngle, 0.0f), 5.0f * Time.deltaTime);
               // transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
                // 카메라의 부모 오브젝트인 Player의 Rotation을 변경
               // CurMode = cameraMode.Rotate;
            }
            else if(touch[0].phase == TouchPhase.Ended || touch[0].phase == TouchPhase.Canceled)
            {
                vFirstPoint = vSecondPoint;
              //  CurMode = cameraMode.None;
            }
        }

        // Camera Move 
        else if(Input.touchCount == 2) // 터치 2개 -> 카메라 움직이기
        {
            touch[0] = Input.GetTouch(0);
            touch[1] = Input.GetTouch(1);
            // 0번 터치와 1번 터치에 대한 데이터 저장

            if(touch[0].phase == TouchPhase.Began || touch[1].phase == TouchPhase.Began) // 두 터치가 입력될 경우
            {
                vLastPos = ((touch[0].position - touch[0].deltaPosition) + (touch[1].position - touch[1].deltaPosition)) / 2;
                // 두 터치 좌표의 중간 값을 LastPos에 저장
              //  CurMode = cameraMode.Move;
            }

            else if(touch[0].phase == TouchPhase.Moved || touch[1].phase == TouchPhase.Moved) // 터치가 움직였을 때
            {
                vCurPos = ((touch[0].position - touch[0].deltaPosition) + (touch[1].position - touch[1].deltaPosition)) / 2;
                vMovePos = (Vector3)(vLastPos - vCurPos);
                // 움직인 터치의 좌표의 중간값을 CurPos에 저장한 후 LastPos와의 차이를 MovePos에 저장
                
                transform.Translate(vMovePos * Time.deltaTime * fMoveSpeed);
                // 카메라의 부모 오브젝트인 Player의 위치를 변경. 속도는 MoveSpeed로 설정
                
                vLastPos = vCurPos;
                //vLastPos = ((touch[0].position - touch[0].deltaPosition) + (touch[1].position - touch[1].deltaPosition)) / 2;
                // LastPos를 최종 위치로 변경시킴
             //   CurMode = cameraMode.Move;
            }
            
            else if(touch[0].phase == TouchPhase.Ended || touch[0].phase == TouchPhase.Canceled || touch[1].phase == TouchPhase.Ended || touch[1].phase == TouchPhase.Canceled)
            {
                vLastPos = vCurPos;
            }
        }

        // Camera Zoom-In, Zoom-Out
        else if(Input.touchCount == 3) // 터치 3개 -> 카메라 줌인/줌아웃
        {
            touch[0] = Input.GetTouch(0);
            touch[1] = Input.GetTouch(1);
            touch[2] = Input.GetTouch(2);
            // 각 터치 데이터를 저장
            Vector2[] vTouchLastPosArr = new Vector2[3];
            
            if(touch[0].phase == TouchPhase.Began || touch[1].phase == TouchPhase.Began || touch[2].phase == TouchPhase.Began)
            {
                vTouchLastPosArr[0] = touch[0].position - touch[0].deltaPosition;
                vTouchLastPosArr[1] = touch[1].position - touch[1].deltaPosition;
                vTouchLastPosArr[2] = touch[2].position - touch[2].deltaPosition;
                // 각 터치에 대한 위치 좌표를 위치 변수에 저장
                Vector2 LastVectorOne = vTouchLastPosArr[1] - vTouchLastPosArr[0];
                Vector2 LastVectorTwo = vTouchLastPosArr[2] - vTouchLastPosArr[0];
                // touch[0]->touch[1] 벡터(u)와 touch[0]->touch[2](v)의 벡터를 구함
                fLastArea = Mathf.Abs(Vector3.Cross(LastVectorOne, LastVectorTwo).magnitude);
                // 삼각형 넓이를 구하기 위해 두 벡터 사이의 각을 구하기 위해 외적(Cross Product)을 구함
                // 외적: |u*v| = |u||v|sin(theta) // 삼각형 넓이 S = (1/2)|u||v|sin(theta)

             //   CurMode = cameraMode.Zoom;
            }
            else if(touch[0].phase == TouchPhase.Moved || touch[1].phase == TouchPhase.Moved || touch[2].phase == TouchPhase.Moved)
            {
                Vector2 vectorOne = touch[1].position - touch[0].position;
                Vector2 vectorTwo = touch[2].position - touch[0].position;
                // 새로운 touch 값 touch[0]->touch[1] 벡터(u)와 touch[0]->touch[2](v)의 벡터를 구함
                fNewArea = Mathf.Abs(Vector3.Cross(vectorOne, vectorTwo).magnitude);
                // 두 벡터 사이의 외적을 구함

                float deltaMagnitudeDiff = fLastArea - fNewArea;
                // 두 삼각형의 넓이 차이를 구함. 음수가 나오면 손가락을 벌린 상태(줌인)
            

                if(GetComponent<Camera>().orthographic) // if Viewing Projection is Orthographic Mode(2D)
                {
                    GetComponent<Camera>().orthographicSize += (deltaMagnitudeDiff * fOrthoZoomSpeed * Time.deltaTime);
                    GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 5.0f);
                }
                else // if Viewing Projection is Perspective Mode(3D)
                {
                    GetComponent<Camera>().fieldOfView += (deltaMagnitudeDiff * fPerspectZoomSpeed * Time.deltaTime);
                    // Field of View의 값을 변경
                    GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 5.0f, 100.0f);
                    // Field of View 값을 5에서 100 사이로 제한
                }
                fLastArea = fNewArea;
           //     CurMode = cameraMode.Zoom;
            }
            else if(touch[0].phase == TouchPhase.Ended || touch[0].phase == TouchPhase.Canceled || touch[1].phase == TouchPhase.Ended || touch[1].phase == TouchPhase.Canceled || touch[2].phase == TouchPhase.Ended || touch[2].phase == TouchPhase.Canceled)
            {
                fLastArea = fNewArea;
            }
        }

        else 
        {
            bIsCameraControl = false;
       //     CurMode = cameraMode.None;
            // 아무런 터치를 안 했을 때 카메라 제어 중이 아님 설정
        }
    }

    // Ray Cast용 함수(오브젝트와 상호작용하게 할 함수)
    private void LineTraceFunc()
    {
      //  bIsItemClicked = true;
        if(Input.touchCount == 1)
        {
            //check = false;
            Touch touch = Input.GetTouch(0);
             // 0번 터치의 정보(현재 누르고 있는 터치에 대한 정보)

            Vector3 vTouchPosToVec3 = new Vector3(touch.position.x, touch.position.y, 0);
            // 현재 화면을 터치하고 있는 위치를 3차원 공간으로 변형하여 저장

            Vector3 vTouchPos = Camera.main.ScreenToWorldPoint(vTouchPosToVec3);
            // TouchPosToVec3를 현재 카메라가 비추고 있는 화면 내의 좌표값으로 사용할 수 있게 변환

            Ray ray = Camera.main.ScreenPointToRay(vTouchPosToVec3);
            // 카메라로부터 터치한 공간까지 벡터를 Ray로 변환

            RaycastHit hit;
            // Ray Cast(Line Trace) 중 충돌한 object에 대한 정보를 저장할 변수

            // GameObject HitObject;
            // hit 값 중 Game 오브젝트만 저장할 변수

            if(touch.phase == TouchPhase.Began && CurClick == whichClicked.None) // 화면을 눌렀을 때(딱 한 번)
            {
                if(Physics.Raycast(ray, out hit, fMaxDistance))
                // ray를 MaxDistance(40.0f) 만큼 쏴서 충돌한 object를 hit에 저장
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 0.5f);
                    // object와 충돌했을 경우 경로를 빨간색으로 표시(Debug용) 정식 출시 시 삭제 필요

                    HitObject = hit.collider.gameObject;
                    // hit 중 gameOjbect만 저장

                    // LastHitObj = HitObject;
                    // 나중에 touch를 뗐을 때를 위해 마지막에 Hit된 오브젝트 갱신

                    if(HitObject.name == "Wire_Plus_Start") // 오브젝트의 이름이 PlusStart(빨간 핀)일 경우
                    {
                        if(HitObject.GetComponent<RedStartPin>().GetIsConneted()) return;
                        toDrag = HitObject.transform;
                        dist = hit.transform.position.z - Camera.main.transform.position.z;
                        vec = new Vector3(touch.position.x, touch.position.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        offset = toDrag.position - vec;
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        dragging = true;

                       // HitObject.GetComponent<RedStartPin>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                        CurClick = whichClicked.SRed;
                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                    }

                    else if(HitObject.name == "Wire_Minus_Start") // 오브젝트의 이름이 MinusStart(검정 핀)일 경우 
                    {
                        if(HitObject.GetComponent<BlackStartPin>().GetIsConneted()) return;
                        toDrag = HitObject.transform;
                        dist = hit.transform.position.z - Camera.main.transform.position.z;
                        vec = new Vector3(touch.position.x, touch.position.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        offset = toDrag.position - vec;
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        dragging = true;
                      //  HitObject.GetComponent<BlackStartPin>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행
                     //   bIsItemClicked = true;
                        CurClick = whichClicked.SBlack;
                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                    }

                    else if(HitObject.name == "ON_OFF_Sub") // 오브젝트의 이름이 전원 버튼일 경우
                    {
                        HitObject.GetComponent<OnOffSub>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                      //  bBtnClicked = true;
                        CurClick = whichClicked.PSSub;
                        // 버튼을 클릭했다고 설정
                      ///  bIsItemClicked = true;
                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                    }

                    else if(HitObject.name == "ON_OFF_Main") // 오브젝트의 이름이 전원 스위치일 경우
                    {
                        HitObject.GetComponent<OnOffMain>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                        CurClick = whichClicked.PSMain;
                      //  bSwitchClicked = true;
                        // 스위치를 눌렀다고 설정(터치 시간 제한용)(지금 삭제해도 무관할 듯)

                        check = false;
                        // 터치 제한 용

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                     //   bIsItemClicked = true;
                    }

                    else if(HitObject.name == "Reverse_Toggle") // 오브젝트의 이름이 극 전환 스위치일 경우
                    {
                        HitObject.GetComponent<ReverseToggle>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                      //  bSwitchClicked = true;
                        CurClick = whichClicked.PSToggle;
                        // 스위치를 눌렀다고 설정(터치 시간 제한용)(지금 삭제해도 무관할 듯)
                        
                        check = false;
                        // 터치 제한 용

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                      //  bIsItemClicked = true;
                    }
                    
                    else if(HitObject.name == "Ampere_Wheel") // 오브젝트의 이름이 전압 조절기일 경우
                    {
                        HitObject.GetComponent<AmpereWheel>().SetCurPos(vTouchPos);
                        HitObject.GetComponent<AmpereWheel>().SetLastPos();
                        // 현재 터치 위치를 AmpereWheel 스크립트로 전송
                        HitObject.GetComponent<AmpereWheel>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                        CurClick = whichClicked.PSWheel;
                       // bWheelClicked = true;

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                      //  bIsItemClicked = true;
                    }

                    else if(HitObject.name == "TIP_ERed") // 오브젝트의 이름이 빨간 집게일 경우
                    {
                        if(HitObject.GetComponent<RedEndPin>().GetIsConneted()) return;
                        toDrag = HitObject.transform;
                        dist = hit.transform.position.z - Camera.main.transform.position.z;
                        vec = new Vector3(touch.position.x, touch.position.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        offset = toDrag.position - vec;
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                        dragging = true;

                        CurClick = whichClicked.ERed;
                        
                        bIsCameraControl = false;
                        // 카메라 제어 불가
                    }

                    else if(HitObject.name == "TIP_EBlack") // 오브젝트의 이름이 검정 집게일 경우
                    {
                        if(HitObject.GetComponent<BlackEndPin>().GetIsConneted()) return;
                        toDrag = HitObject.transform;
                        dist = hit.transform.position.z - Camera.main.transform.position.z;
                        vec = new Vector3(touch.position.x, touch.position.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        offset = toDrag.position - vec;
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(-90, 0, 0));
                        dragging = true;
                        
                        CurClick = whichClicked.EBlack;

                        bIsCameraControl = false;
                        // 카메라 제어 불가
                    }
                    // hit.collider.gameObject.OnInteract();
                } 
                else
                {
                    Debug.DrawLine(ray.origin, vTouchPos, Color.blue, 0.5f);
                    // 맞은 오브젝트가 없을 때 파란색으로 표시(Debug용) 정식 출시 시 삭제
                }
            }

            else if(touch.phase == TouchPhase.Stationary) // 화면을 꾹 누르고 있을 때
            {
                if(Physics.Raycast(ray, out hit, fMaxDistance)) // Ray를 쏴서 충돌한 객체를 hit에 저장
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 0.5f); 
                    // 맞았을 때 빨간색으로 표시(Debug용) 정식 출시 시 삭제해도 무관

                  //  HitObject = hit.collider.gameObject; 
                    // 맞은 hit의 객체 정보를 HitObject에 저장

                    // LastHitObj = HitObject; 
                    // 터치를 뗐을 때의 트리거를 작동 시키기 위해 LastHitObj에도 저장

                    if(/* HitObject.name == "ON_OFF_Sub" || */ CurClick == whichClicked.PSSub) // 맞은 객체의 이름이 전원 장치의 Sub 버튼일 경우
                    {
                        HitObject.GetComponent<OnOffSub>().OnInteract(); 
                        // 해당 객체의 인터렉트 메서드 실행

                        CurClick = whichClicked.PSSub;
                      //  bBtnClicked = true; 
                        // 현재 버튼을 누를 상태임을 표시

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                        //bIsItemClicked = true; 
                        // 누르는 동안 카메라가 움직이지 못하게 하기 위해 아이템 선택 중임을 표시
                    }
                }
            }

            else if(dragging && touch.phase == TouchPhase.Moved) // 터치를 움직였을 때
            {
                // if(Physics.Raycast(ray, out hit, fMaxDistance, LayerMask.GetMask("Floor"))) // Ray를 쏴서 충돌한 객체를 hit에 저장
                // {
                //     Debug.DrawLine(ray.origin, hit.point, Color.red, 0.5f); 
                    // 맞았을 때 빨간색으로 표시(Debug용) 정식 출시 시 삭제해도 무관

                  //  HitObject = hit.collider.gameObject; 
                    // 맞은 hit의 객체 정보를 HitObject에 저장

                    // LastHitObj = HitObject; 
                    // 터치를 뗐을 때의 트리거를 작동 시키기 위해 LastHitObj에도 저장
                    if(CurClick == whichClicked.SRed) // 현재 선택한 오브젝트의 이름이 빨간 핀일 경우
                    {
                        if(HitObject.GetComponent<RedStartPin>().GetIsConneted()) {
                            CurClick = whichClicked.None;
                            // bIsCameraControl = true;
                            return;
                        }
                        vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        toDrag.position = vec + offset;


                        CurClick = whichClicked.SRed;
                        bIsCameraControl = false;
                        // 카메라 제어 불가
                    }

                    else if(CurClick == whichClicked.SBlack)
                    {
                        if(HitObject.GetComponent<BlackStartPin>().GetIsConneted()) {
                            CurClick = whichClicked.None;
                            // bIsCameraControl = true;
                            return;
                        }
                        vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        toDrag.position = vec + offset;


                        CurClick = whichClicked.SBlack;
                        bIsCameraControl = false;
                        // 카메라 제어 불가
                    }

                    else if(CurClick == whichClicked.PSWheel) // 현재 선택한 오브젝트의 이름이 전압 조절기일 경우
                    {
                        HitObject.GetComponent<AmpereWheel>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행
                        HitObject.GetComponent<AmpereWheel>().SetCurPos(vTouchPos);

                        CurClick = whichClicked.PSWheel;
                      //  bWheelClicked = true;

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                      //  bIsItemClicked = true;
                    }

                    else if(CurClick == whichClicked.ERed) // 현재 선택한 오브젝트의 이름이 빨간 집게일 경우
                    {
                        // // 사용자가 Interaction 했을 때의 스크립트 실행
                        if(HitObject.GetComponent<RedEndPin>().GetIsConneted()) {
                            CurClick = whichClicked.None;
                            // bIsCameraControl = true;
                            return;
                        }
                        vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        toDrag.position = vec + offset;


                        CurClick = whichClicked.ERed;
                        bIsCameraControl = false;
                        // 카메라 제어 불가
                        
                    }

                    else if(CurClick == whichClicked.EBlack) // 현재 선택한 오브젝트의 이름이 검정 집게일 경우
                    {
                        if(HitObject.GetComponent<BlackEndPin>().GetIsConneted()) {
                            CurClick = whichClicked.None;
                            return;
                        }
                        vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, dist);
                        vec = Camera.main.ScreenToWorldPoint(vec);
                        vec.z = 0.3f;
                        toDrag.position = vec + offset;
                        //HitObject.GetComponent<BlackEndPin>().SetCurPos(hit.point);

                        CurClick = whichClicked.EBlack;
                        bIsCameraControl = false;
                        // 카메라 제어 불가
                    }
               // }
            }

            else if(touch.phase == TouchPhase.Ended) // 터치를 뗐을 때
            {
                // switch(CurClick)
                // {
                //     case whichClicked.PSSub:
                //     {
                //         HitObject.GetComponent<OnOffSub>().OffInteract(); // Sub 버튼의 스크립트 실행
                //         CurClick = whichClicked.None;
                //         break;
                //     }

                // }
                if(CurClick == whichClicked.PSSub) // 전원 장치의 Sub 버튼을 눌렀을 때 작동
                {
                    HitObject.GetComponent<OnOffSub>().OffInteract(); // Sub 버튼의 스크립트 실행
                   // bBtnClicked = false; // false로 reset
                }

                else if(CurClick == whichClicked.PSWheel) // 아마 안 쓸 듯. 일단 나둠
                {
                    HitObject.GetComponent<AmpereWheel>().OffInteract();
                  //  bWheelClicked = false;
                }

                else if(dragging && CurClick == whichClicked.SRed) // 마지막까지 터치했던 것이 빨간 핀일 경우
                {
                    if(!HitObject.GetComponent<RedStartPin>().GetIsConneted())
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    
                    dragging = false;
                    HitObject = null;
                }

                else if(dragging && CurClick == whichClicked.SBlack) // 마지막까지 터치했던 것이 검정 핀일 경우
                {
                    if(!HitObject.GetComponent<BlackStartPin>().GetIsConneted())
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    
                    dragging = false;
                    HitObject = null;
                }

                else if(dragging && CurClick == whichClicked.ERed) // 마지막까지 터치했던 것이 빨간 집게일 경우
                {
                    if(!HitObject.GetComponent<RedEndPin>().GetIsConneted())
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    
                    dragging = false;
                    HitObject = null;
                }
                else if(dragging && CurClick == whichClicked.EBlack) // 마지막까지 터치했던 것이 검정 집게일 경우
                {
                    if(!HitObject.GetComponent<BlackEndPin>().GetIsConneted())
                        HitObject.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));

                    dragging = false;
                    HitObject = null;
                    
                }
            }
        }

        else 
        {
           // bIsItemClicked = false;
            CurClick = whichClicked.None;
        //    CurMode = cameraMode.None;
            bIsCameraControl = true;
            // 아무런 터치도 없을 경우 아이템을 클릭하고 있지 않음을 표시
            // 카메라 제어 가능
        }
    }
}
