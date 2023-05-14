using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //RaycastHit hit;
    public bool check = true;
    public bool bSwitchClicked = false;
    public bool bBtnClicked = false;
    public bool bWheelClicked = false;
    float fMaxDistance = 40f; // Ray Distance(Length)
    GameObject LastHitObj;
    GameObject PlayerObj, CenterObj;
    private Vector2 vCurPos, vLastPos;
    private Vector3 vMovePos;
    private float fMoveSpeed = 0.05f;
   
    Vector3 vFirstPoint, vSecondPoint;
    float xAngle, yAngle, xAngleTemp, yAngleTemp, oldAngle;
   // public bool bIsItemClicked = false;
    public bool bIsCameraControl = false;
    
    [HideInInspector]
    public float fPerspectZoomSpeed = 0.1f;
    public float fOrthoZoomSpeed = 0.1f;
    
    // Camera Camera;
    void Start()
    {
        PlayerObj = transform.parent.gameObject;
        xAngle = 0.0f; yAngle = 0.0f; oldAngle = -1.0f;
        //transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
    }

    // Update is called once per frame
    // 오브젝터 제어 -> 카메라 제어 순으로 반복할 예정
    void Update()
    {
        if(!bIsCameraControl) RayTraceFunc();
        // 현재 카메라 제어 중이 아닐 경우엔 오브젝트 제어

        else CameraControl();
        // 현재 카메라 제어 중이라면 카메라부터 제어

        // if(!bIsItemClicked)
        // RotateCamera();

        // if(bSwitchClicked) StartCoroutine(WaitForIt(0.1f));       
        // else check = true;
        check = true;
           // StartCoroutine(WaitForIt(0.05f));
    }

    // 입력 시간 제한용 함수(일단 미사용)
    IEnumerator WaitForIt(float time)
    {
        if(!check) 
        {
            yield return new WaitForSeconds(time);
            check = true;
            bSwitchClicked = false;
        }
    }

    // 카메라 제어 함수
    private void CameraControl()
    {
        vMovePos = Vector3.zero;
        Touch[] touch = new Touch[3];
        bIsCameraControl = true;

        // Camera Rotate
        if(Input.touchCount == 1) 
        {
            touch[0] = Input.GetTouch(0);
            if(touch[0].phase == TouchPhase.Began)
            {
                vFirstPoint = touch[0].position - touch[0].deltaPosition;
                xAngleTemp = xAngle;
                yAngleTemp = yAngle * -1.0f;
            }
            if(touch[0].phase == TouchPhase.Moved)
            {
                vSecondPoint = touch[0].position - touch[0].deltaPosition;
                xAngle = xAngleTemp + (vSecondPoint.x - vFirstPoint.x) * 180 / Screen.width;
                yAngle = (yAngleTemp + (vSecondPoint.y - vFirstPoint.y) * 90 * 3f / Screen.height) * -1.0f;

                var v2 = vFirstPoint - vSecondPoint;

                var newAngle = Mathf.Atan2(v2.y, v2.x);
                newAngle = newAngle * 180 / Mathf.PI;

                if(oldAngle < 0) oldAngle = newAngle;

                var deltaAngle = Mathf.DeltaAngle(newAngle, oldAngle);

                oldAngle = newAngle;

                if(yAngle < 50f) yAngle = 50f;
                if(yAngle > 105f) yAngle = 105f;

                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                RaycastHit hit;
                Vector3 position = new Vector3();

                if(Physics.Raycast(ray, out hit, fMaxDistance))
                {
                    position = hit.transform.position;
                }
                //PlayerObj.transform.RotateAround(position, Vector3.up, -deltaAngle);
                PlayerObj.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);
            }
        }

        // Camera Move 
        else if(Input.touchCount == 2) 
        {
            touch[0] = Input.GetTouch(0);
            touch[1] = Input.GetTouch(1);
            //touch[2] = null;

            if(touch[0].phase == TouchPhase.Began || touch[1].phase == TouchPhase.Began)
            {
                
                vLastPos = ((touch[0].position - touch[0].deltaPosition) + (touch[1].position - touch[1].deltaPosition)) / 2;
            }
            else if(touch[0].phase == TouchPhase.Moved || touch[1].phase == TouchPhase.Moved)
            {
                vCurPos = ((touch[0].position - touch[0].deltaPosition) + (touch[1].position - touch[1].deltaPosition)) / 2;
                vMovePos = (Vector3)(vLastPos - vCurPos);
                
                PlayerObj.transform.Translate(vMovePos * Time.deltaTime * fMoveSpeed);
                vLastPos = ((touch[0].position - touch[0].deltaPosition) + (touch[1].position - touch[1].deltaPosition)) / 2;
            }
        }

        // Camera Zoom-In, Zoom-Out
        else if(Input.touchCount == 3)
        {
            touch[0] = Input.GetTouch(0);
            touch[1] = Input.GetTouch(1);
            touch[2] = Input.GetTouch(2);

            Vector2[] vTouchLastPosArr = new Vector2[3];
            vTouchLastPosArr[0] = touch[0].position - touch[0].deltaPosition;
            vTouchLastPosArr[1] = touch[1].position - touch[1].deltaPosition;
            vTouchLastPosArr[2] = touch[2].position - touch[2].deltaPosition;

            Vector2 LastVectorOne = vTouchLastPosArr[1] - vTouchLastPosArr[0];
            Vector2 LastVectorTwo = vTouchLastPosArr[2] - vTouchLastPosArr[0];
            float fLastArea = Mathf.Abs(Vector3.Cross(LastVectorOne, LastVectorTwo).magnitude);

            Vector2 vectorOne = touch[1].position - touch[0].position;
            Vector2 vectorTwo = touch[2].position - touch[0].position;
            float fNewArea = Mathf.Abs(Vector3.Cross(vectorOne, vectorTwo).magnitude);

            float deltaMagnitudeDiff = fLastArea - fNewArea;
           

            if(GetComponent<Camera>().orthographic) // if Viewing Projection is Orthographic Mode
            {
                GetComponent<Camera>().orthographicSize += (deltaMagnitudeDiff * fOrthoZoomSpeed * Time.deltaTime);
                GetComponent<Camera>().orthographicSize = Mathf.Max(GetComponent<Camera>().orthographicSize, 5.0f);
            }
            else // if Viewing Projection is Perspective Mode
            {
                GetComponent<Camera>().fieldOfView += (deltaMagnitudeDiff * fPerspectZoomSpeed * Time.deltaTime);
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 5.0f, 100.0f);
            }
        /*
            if(touch[0].phase == TouchPhase.Began || touch[1].phase == TouchPhase.Began || touch[2].phase == TouchPhase.Began)
            {
                Vector2[] vTouchLastPosArr = new Vectot2[3];
                vTouchLastPosArr[0] = touch[0].position - touch[0].deltaPosition;
                vTouchLastPosArr[1] = touch[1].position - touch[1].deltaPosition;
                vTouchLastPosArr[2] = touch[2].position - touch[2].deltaPosition;
            }
            else if(touch[0].phase == TouchPhase.Moved || touch[1].phase == TouchPhase.Moved || touch[2].phase == TouchPhase.Moved)
            {
                Vector2 LastVectorOne = vTouchLastPosArr[1] - vTouchLastPosArr[0];
                Vector2 LastVectorTwo = vTouchLastPosArr[2] - vTouchLastPosArr[0];
                float fLastArea = Mathf.Abs(Vector3.Cross(LastVectorOne, LastVectorTwo).magnitude);

                Vector2 vectorOne = touch[1].position - touch[0].position;
                Vector2 vectorTwo = touch[2].position - touch[0].position;
            }
        */
        }
        else 
        {
            bIsCameraControl = false;
            // 아무런 터치를 안 했을 때 카메라 제어 중이 아님 설정
        }
    }

    // Ray Trace용 함수(오브젝트와 상호작용하게 할 함수)
    private void RayTraceFunc()
    {
      //  bIsItemClicked = true;
        if(Input.touchCount == 1 && check)
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
            // Ray Cast(Ray Trace) 중 충돌한 object에 대한 정보를 저장할 변수

            GameObject HitObject;
            // hit 값 중 Game 오브젝트만 저장할 변수

            if(touch.phase == TouchPhase.Began) // 화면을 눌렀을 때(딱 한 번)
            {
                if(Physics.Raycast(ray, out hit, fMaxDistance))
                // ray를 MaxDistance(40.0f) 만큼 쏴서 충돌한 object를 hit에 저장
                {
                    Debug.DrawLine(ray.origin, hit.point, Color.red, 0.5f);
                    // object와 충돌했을 경우 경로를 빨간색으로 표시(Debug용) 정식 출시 시 삭제 필요

                    HitObject = hit.collider.gameObject;
                    // hit 중 gameOjbect만 저장

                    LastHitObj = HitObject;
                    // 나중에 touch를 뗐을 때를 위해 마지막에 Hit된 오브젝트 갱신

                    if(HitObject.name == "Wire_Plus_Start") // 오브젝트의 이름이 PlusStart(빨간 핀)일 경우
                    {
                        HitObject.GetComponent<RedPin>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                    }

                    else if(HitObject.name == "Wire_Minus_Start") // 오브젝트의 이름이 MinusStart(검정 핀)일 경우 
                    {
                        HitObject.GetComponent<BlackPin>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행
                     //   bIsItemClicked = true;
                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                    }

                    else if(HitObject.name == "ON_OFF_Sub") // 오브젝트의 이름이 전원 버튼일 경우
                    {
                        HitObject.GetComponent<OnOffSub>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                        bBtnClicked = true;
                        // 버튼을 클릭했다고 설정
                      ///  bIsItemClicked = true;
                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                    }

                    else if(HitObject.name == "ON_OFF_Main") // 오브젝트의 이름이 전원 스위치일 경우
                    {
                        HitObject.GetComponent<OnOffMain>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                        bSwitchClicked = true;
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

                        bSwitchClicked = true;
                        // 스위치를 눌렀다고 설정(터치 시간 제한용)(지금 삭제해도 무관할 듯)
                        
                        check = false;
                        // 터치 제한 용

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                      //  bIsItemClicked = true;
                    }
                    
                    else if(HitObject.name == "Ampere_Wheel") // 오브젝트의 이름이 전압 조절기일 경우
                    {
                        HitObject.GetComponent<AmpereWheel>().OnInteract();
                        // 사용자가 Interaction을 했을 때의 스크립트 실행

                        bWheelClicked = true;

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                      //  bIsItemClicked = true;
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

                    HitObject = hit.collider.gameObject; 
                    // 맞은 hit의 객체 정보를 HitObject에 저장

                    LastHitObj = HitObject; 
                    // 터치를 뗐을 때의 트리거를 작동 시키기 위해 LastHitObj에도 저장

                    if(HitObject.name == "ON_OFF_Sub") // 맞은 객체의 이름이 전원 장치의 Sub 버튼일 경우
                    {
                        HitObject.GetComponent<OnOffSub>().OnInteract(); 
                        // 해당 객체의 인터렉트 메서드 실행

                        bBtnClicked = true; 
                        // 현재 버튼을 누를 상태임을 표시

                        bIsCameraControl = false;
                        // 카메라 제어 불가능으로 설정
                        //bIsItemClicked = true; 
                        // 누르는 동안 카메라가 움직이지 못하게 하기 위해 아이템 선택 중임을 표시
                    }
                }
            }

            else if(touch.phase == TouchPhase.Ended) // 터치를 뗐을 때
            {
                if(bBtnClicked) // 전원 장치의 Sub 버튼을 눌렀을 때 작동
                {
                    LastHitObj.GetComponent<OnOffSub>().OffInteract(); // Sub 버튼의 스크립트 실행
                    bBtnClicked = false; // false로 reset
                }
                else if(bWheelClicked) // 아마 안 쓸 듯. 일단 나둠
                {
                    LastHitObj.GetComponent<AmpereWheel>().OffInteract();
                    bWheelClicked = false;
                }
            }
        }

        else 
        {
           // bIsItemClicked = false;
            bIsCameraControl = true;
            // 아무런 터치도 없을 경우 아이템을 클릭하고 있지 않음을 표시
            // 카메라 제어 가능
        }
    }
}
