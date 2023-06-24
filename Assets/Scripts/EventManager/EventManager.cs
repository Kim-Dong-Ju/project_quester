// 전원 장치 관련 이벤트 관리 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class EventManager : MonoBehaviour
{
    private string text, crossText, wheelText, magneticText, electrolysisText, PlusText;
    private string[] stepText = new string[8], detailText = new string[8], detailBold = new string[8];
    // step마다 할 도움말 Title, step마다 할 도움말 Detail
    public GameObject PowerSupply;
    public GameObject CrookesMagnetic;
    public GameObject CrookesCross;
    public GameObject CrookesWheel;
    public GameObject PlusWireEnd;
    public GameObject MinusWireEnd;
    public GameObject Magnetic;
    public GameObject SelectPanel, RotateBtn, AnotherToolBtn, CurrentToolText, Header, HelpPopup, HelpBtn, BackBtn, RestartBtn, HelpText, HelpDetailBold, HelpDetail, HelpImg; // UI prefab들을 담을 변수들
    public GameObject mainUI; // Canvas
    public Sprite BackImgWhite, ReStartImgWhite; // Images
    private Sprite BackImg, RestartImg;
    public Sprite[] HelpImages = new Sprite[9];
    Vector2 HelpBtnOriginPos, HelpBtnNewPos;
    GameObject crookesCrossIns, crookeswheelIns, crookesMagIns, magneticIns, plusWireIns, minusWireIns;

    private bool PSWireFinish = false, SubBtn = false;
    // 재시작 버튼 클릭 시 이벤트, 전원 장치에 전선을 꽂았을 경우 이벤트
    private bool MagneticSelect = false, CrossSelect = false, WheelSelect = false; // 버튼을 통해 어느 것을 선택했는지 Check용
    private int count = 0, CrookesMagCount = 0, CrookesCrossCount = 0, CrookesWheelCount = 0;
    private int idx = 0, curIdx = 0, imgIdx = 0;
    void Start()
    {
        Debug.Log(CurrentToolText.GetComponent<TMP_Text>().text);
        BackImg = BackBtn.GetComponent<Image>().sprite;
        RestartImg = RestartBtn.GetComponent<Image>().sprite;
        SelectPanel.SetActive(false);
        RotateBtn.SetActive(false);
        AnotherToolBtn.SetActive(false);
        HelpPopup.SetActive(false);
        HelpBtnOriginPos = HelpBtn.GetComponent<RectTransform>().anchoredPosition;
        HelpBtnNewPos = new Vector2(-200f, 270f);
        text = CurrentToolText.GetComponent<TMP_Text>().text;
        crossText = "<b>크룩스관 십자입</b>";
        wheelText = "<b>크룩스관 회전차입</b>";
        magneticText = "<b>크룩스관 슬릿입</b>";
        electrolysisText = "<b>전기분해 장치</b>";
        PlusText = "<b> | </b>";
        help();
    }

    void LateUpdate()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("Learn");
        }  
        PSWireFinish = PowerSupply.GetComponent<PowerSupply>().GetAllWireConnected();

        if(PSWireFinish) // 만약 모든 전선이 연결 되었으면 실행
        {
            if(count == 0)
            {
                count++; // 계속 생성되는거 방지용
                SelectWidget();
            }
            HelpImg.GetComponent<Image>().sprite = HelpImages[imgIdx];
            HelpText.GetComponent<TMP_Text>().text = stepText[curIdx];
            HelpDetailBold.GetComponent<TMP_Text>().text = detailBold[curIdx];
            HelpDetail.GetComponent<TMP_Text>().text = detailText[curIdx];

            if(CheckToolWire())
            {
                if(CheckDial())
                {
                    if(SubBtn)
                    {
                        if(CheckMain())
                        {
                            Crookes();
                        }
                        else
                        {
                            imgIdx = 6;
                            curIdx = 4;
                        }
                    }
                    else
                    {
                        imgIdx = 5;
                        curIdx = 3;
                        CheckSubBtn();
                    }
                }
                else
                {
                    imgIdx = 4;
                    curIdx = 2;
                }
            }
        }
    }

    public void help()
    {
        stepText[0] = HelpText.GetComponent<TMP_Text>().text;
        stepText[1] = "2. 전선 연결하기";
        stepText[2] = "3. 전압 조절하기";
        stepText[3] = "4. 순간 동작 버튼으로 시험 작용하기";
        stepText[4] = "5. 동작 스위치로 작동시키기";
        stepText[5] = "6. 관찰하기 및 극 전환시키기";
        stepText[6] = "6. 자석 조정하기 및 극 전환시키기";
        stepText[7] = "6. 관찰하기 및 극 전환시키기";

        detailBold[0] = HelpDetailBold.GetComponent<TMP_Text>().text;
        detailBold[1] = "전선 집게를 드래그하여 크룩스관과 연결하세요.";
        detailBold[2] = "전압 조절 다이얼을 드래그하여 전압을 조절하세요.";
        detailBold[3] = "빨간색 버튼을 눌러 크룩스관과 반응을 확인해보세요.";
        detailBold[4] = "동작 스위치를 눌러 전원을 켜세요";
        detailBold[5] = "크룩스관에서 나타나는 결과를 관찰하세요. 극 전환이 필요하면 레버를 눌러보세요";
        detailBold[6] = "자석을 드래그하여 음극선의 변화를 관찰하세요. 극 전환이 필요하면 레버를 눌러보세요";
        detailBold[7] = "크룩스관에서 나타나는 결과를 관찰하세요. 극 전환이 필요하면 레버를 눌러보세요";

        detailText[0] = HelpDetail.GetComponent<TMP_Text>().text;
        detailText[1] = "빨간색 집게는 (+)극에, 검정색 핀은 (-)극에 연결하세요.";
        detailText[2] = "다이얼을 원 돌리듯 드래그해보세요. 다이얼이 돌아갑니다.";
        detailText[3] = "빨간색 버튼은 순간 전원 버튼으로 전원 장치가 제대로 작동하는지 확인해보는 동작입니다. 꼭 스위치를 누르기 전 눌러보세요.";
        detailText[4] = "빨간 버튼 옆에 있는 검정색 동작 스위치를 눌러 전류를 크룩스관에 주세요.";
        detailText[5] = "극 전환 레버를 누르면 (+)극과 (-)극이 바뀝니다.";
        detailText[6] = "극 전환 레버를 누르면 (+)극과 (-)극이 바뀝니다. 자석이 음극선을 어떻게 변화시키는지 확인해보세요.";
        detailText[7] = "극 전환 레버를 누르면 (+)극과 (-)극이 바뀝니다. 결과가 바뀌기 때문에 극 전환을 시켜보세요.";
        // StreamReader  sr = new StreamReader(Application.dataPath + "/Resources/" + "Help.csv");

        // int i = 0;
        // bool eof = false;
        // while(!eof)
        // {
        //     string dataString = sr.ReadLine();
        //     if(dataString == null)
        //     {
        //         eof = true;
        //         break;
        //     }
        //     var data_value = dataString.Split(',');

        //     if(i == 0)
        //     {
        //         stepText[idx] =  HelpText.GetComponent<TMP_Text>().text;
        //         detailBold[idx] = HelpDetailBold.GetComponent<TMP_Text>().text;
        //         detailText[idx] = HelpDetail.GetComponent<TMP_Text>().text;
        //         i++;
        //         Debug.Log("idx: " + idx +" stepText: " + stepText[idx] + " detailBodx: " + detailBold[idx] + " detaliText: " + detailText[idx++]);
        //         continue;
        //     }


        //     stepText[idx] = data_value[1].ToString();
        //     detailBold[idx] = data_value[2].ToString();
        //     detailText[idx] = data_value[3].ToString();
        //     i++;
        //     Debug.Log("idx: " + idx +" stepText: " + stepText[idx] + " detailBodx: " + detailBold[idx] + " detaliText: " + detailText[idx++]);
        // }
    }

    public bool CheckToolWire()
    {
        bool ToolWireFinish = false;
        // 유저가 크룩스관을 생성했는지 체크
        if(CrookesMagCount > 0)
        {
            ToolWireFinish = crookesMagIns.GetComponent<CrookesMagnetic>().GetIsConneted();
        }
        else if(CrookesCrossCount > 0)
        {
            ToolWireFinish = crookesCrossIns.GetComponent<CrookesCross>().GetIsConnected();
        }
        else if(CrookesWheelCount > 0)
        {
            ToolWireFinish = crookeswheelIns.GetComponent<CrookesPaddle>().GetIsConnected();
        }
        
        return ToolWireFinish;
    }

    public void CheckSubBtn()
    {
        if(SubBtn) return;
        else
        {
            bool powered = PowerSupply.GetComponent<PowerSupply>().GetIsPowered();
            bool mainOn = PowerSupply.GetComponent<PowerSupply>().GetIsMainOn();
            if(powered && !mainOn) SubBtn = true;
        }
    }

    public bool CheckMain()
    {
        bool powered = PowerSupply.GetComponent<PowerSupply>().GetIsPowered();
        bool mainOn = PowerSupply.GetComponent<PowerSupply>().GetIsMainOn();
        return (powered && mainOn);
    }

    public bool CheckDial()
    {
        float Ampere = PowerSupply.GetComponent<PowerSupply>().GetAmpere();
        if(Ampere > 0)
        {
            return true;
        }
        return false;
    }

    void Crookes()
    {
        if(CrookesCrossCount > 0)
        {
            imgIdx = 7;
            curIdx = 5;
        }
        else if(CrookesMagCount > 0)
        {
            imgIdx = 8;
            curIdx = 6;
        }
        else if(CrookesWheelCount > 0)
        {
            imgIdx = 7;
            curIdx = 7;
        }
    }

    public void SelectWidget() // 조건을 만족했을 때 생성시킬 UI
    {
        HelpBtn.SetActive(false);
        StartCoroutine(Widget());
    }

    IEnumerator Widget()
    {
        yield return null; // 한 프레임 이후 실행

        HelpBtn.SetActive(false);
        HelpBtn.GetComponent<RectTransform>().anchoredPosition = HelpBtnOriginPos;
        BackBtn.GetComponent<Image>().sprite = BackImgWhite;
        RestartBtn.GetComponent<Image>().sprite = ReStartImgWhite;
        Header.GetComponent<Image>().color = new Color(0.4039216f, 0.7098039f, 0.7176471f, 1f);
        CurrentToolText.GetComponent<TMP_Text>().color = Color.white;
        CurrentToolText.GetComponent<TMP_Text>().text = "<b>원하는 도구를 선택하세요</b>";
        SelectPanel.SetActive(true);
        // UI 출력
        Debug.Log(count);

        yield return null;
    }

    public void RestartButton() // 재시작 버튼을 눌렀을 경우
    {
        // 현재 씬 다시 재로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }   

    public void BackButton() // 뒤로가기 버튼을 눌렀을 경우
    {
        SceneManager.LoadScene("Learn");
    }

    public void OtherToolButton() // 다른 도구 공부하기 선택했을 경우의 이벤트
    {
        if(CrookesCrossCount > 0) // 이전에 이미 크룩스 십자입을 생성했을 경우
        {
            CrookesCrossCount = 0;
            Destroy(crookesCrossIns);
            Destroy(plusWireIns);
            Destroy(minusWireIns);
        }

        if(CrookesWheelCount > 0) // 이전에 이미 크룩스 회전차입을 생성했을 경우
        {
            CrookesWheelCount = 0;
            Destroy(crookeswheelIns);
            Destroy(plusWireIns);
            Destroy(minusWireIns);
        }

        if(CrookesMagCount > 0) // 이전에 이미 크룩스 슬릿입을 생성했을 경우
        {
            CrookesMagCount = 0;
            Destroy(crookesMagIns);
            Destroy(magneticIns);
            Destroy(plusWireIns);
            Destroy(minusWireIns);
            // Destroy(RotateBtn);
            RotateBtn.SetActive(false);
        }
        // Destroy(AnotherToolBtn); 
        AnotherToolBtn.SetActive(false); // 창에 띄워진 것도 제거

        CurrentToolText.GetComponent<TMP_Text>().text = text;
        curIdx = 0;
        count = 0;
    }

    public void MagneticButton() // 크룩스관 슬릿입을 선택했을 경우의 이벤트
    {
        // 현재 창에 띄워진 선택창 제거
        // Destroy(SelectPanel);
        SelectPanel.SetActive(false);
        imgIdx = 2;
        Header.GetComponent<Image>().color = Color.white;
        BackBtn.GetComponent<Image>().sprite = BackImg;
        RestartBtn.GetComponent<Image>().sprite = RestartImg;
        CurrentToolText.GetComponent<TMP_Text>().color = Color.black;
        HelpImg.GetComponent<Image>().sprite = HelpImages[imgIdx];
        HelpText.GetComponent<TMP_Text>().text = stepText[curIdx];
        HelpDetailBold.GetComponent<TMP_Text>().text = detailBold[curIdx];
        HelpDetail.GetComponent<TMP_Text>().text = detailText[curIdx++];

        // 크룩스 슬릿입 생성
        crookesMagIns = Instantiate(CrookesMagnetic, new Vector3(1.11f, 0.51f, 0.42f), Quaternion.Euler(CrookesMagnetic.transform.localEulerAngles));
        crookesMagIns.GetComponent<CrookesMagnetic>().PowerSupply = this.PowerSupply;
        PowerSupply.GetComponent<PowerSupply>().SetCrookesMagnetic(crookesMagIns);
        magneticIns = Instantiate(Magnetic, new Vector3(0.925f, 0.7938f, 0.289f), Quaternion.Euler(Magnetic.transform.localEulerAngles));
        crookesMagIns.GetComponent<CrookesMagnetic>().SetMagnetic(magneticIns);
        minusWireIns = Instantiate(MinusWireEnd, new Vector3(0.85f, 0.51f, 0.35f), Quaternion.identity);
        plusWireIns = Instantiate(PlusWireEnd, new Vector3(0.79f, 0.51f, 0.35f), Quaternion.identity);
 
        AnotherToolBtn.SetActive(true);
        RotateBtn.SetActive(true);
        HelpBtn.GetComponent<RectTransform>().anchoredPosition = HelpBtnNewPos;
        HelpBtn.SetActive(true);
        CurrentToolText.GetComponent<TMP_Text>().text = text + PlusText + magneticText;
        MagneticSelect = true; // 중복 생성 등 방지용으로 false로 초기화
        CrookesMagCount++;
    }

    public void CrossButton() // 크룩스관 십자입을 선택했을 경우의 이벤트
    {
        // 현재 창에 띄워진 선택창 제거
        // Destroy(SelectPanel);
        SelectPanel.SetActive(false);
        imgIdx = 1;
        Header.GetComponent<Image>().color = Color.white;
        BackBtn.GetComponent<Image>().sprite = BackImg;
        RestartBtn.GetComponent<Image>().sprite = RestartImg;
        CurrentToolText.GetComponent<TMP_Text>().color = Color.black;
        HelpImg.GetComponent<Image>().sprite = HelpImages[imgIdx];
        HelpText.GetComponent<TMP_Text>().text = stepText[curIdx];
        HelpDetailBold.GetComponent<TMP_Text>().text = detailBold[curIdx];
        HelpDetail.GetComponent<TMP_Text>().text = detailText[curIdx++];

        // 크룩스 십자입 생성
        crookesCrossIns = Instantiate(CrookesCross, new Vector3(1.11f, 0.51f, 0.42f), Quaternion.Euler(CrookesCross.transform.localEulerAngles));
        crookesCrossIns.GetComponent<CrookesCross>().PowerSupply = this.PowerSupply;
        PowerSupply.GetComponent<PowerSupply>().SetCrookesCross(crookesCrossIns);
        minusWireIns = Instantiate(MinusWireEnd, new Vector3(0.85f, 0.51f, 0.35f), Quaternion.identity);
        plusWireIns = Instantiate(PlusWireEnd, new Vector3(0.79f, 0.51f, 0.35f), Quaternion.identity);

        // Instantiate(RotateBtn).transform.SetParent(mainUI.transform);
        // Instantiate(AnotherToolBtn).transform.SetParent(mainUI.transform);
        AnotherToolBtn.SetActive(true);
        HelpBtn.SetActive(true);
        
        CurrentToolText.GetComponent<TMP_Text>().text = text + PlusText + crossText;
        CrossSelect = true; // 중복 방지를 위해 초기화
        CrookesCrossCount++;
    }

    public void WheelButton() // 크룩스관 회전차입을 선택했을 경우의 이벤트
    {
        // 현재 창에 띄워진 선택창 제거
        // Destroy(SelectPanel);
        SelectPanel.SetActive(false);
        imgIdx = 3;
        Header.GetComponent<Image>().color = Color.white;
        BackBtn.GetComponent<Image>().sprite = BackImg;
        RestartBtn.GetComponent<Image>().sprite = RestartImg;
        CurrentToolText.GetComponent<TMP_Text>().color = Color.black;
        HelpImg.GetComponent<Image>().sprite = HelpImages[imgIdx];
        HelpText.GetComponent<TMP_Text>().text = stepText[curIdx];
        HelpDetailBold.GetComponent<TMP_Text>().text = detailBold[curIdx];
        HelpDetail.GetComponent<TMP_Text>().text = detailText[curIdx++];

        // 크룩스 회전차입 생성
        crookeswheelIns = Instantiate(CrookesWheel, new Vector3(1.11f, 0.51f, 0.42f), Quaternion.Euler(CrookesWheel.transform.localEulerAngles));
        crookeswheelIns.GetComponent<CrookesPaddle>().PowerSupply = this.PowerSupply;
        PowerSupply.GetComponent<PowerSupply>().SetCrookesWheel(crookeswheelIns);
        minusWireIns = Instantiate(MinusWireEnd, new Vector3(0.85f, 0.51f, 0.35f), Quaternion.identity);
        plusWireIns = Instantiate(PlusWireEnd, new Vector3(0.79f, 0.51f, 0.35f), Quaternion.identity);

        AnotherToolBtn.SetActive(true);
        HelpBtn.SetActive(true);
        CurrentToolText.GetComponent<TMP_Text>().text = text + PlusText + wheelText;
        WheelSelect = true; // 중복 방지를 위해 초기화
        CrookesWheelCount++;
    }

    public void ElectrolysisButton()
    {
        // 현재 창에 띄워진 선택창 제거
        // Destroy(SelectPanel);
        SelectPanel.SetActive(false);
        Header.GetComponent<Image>().color = Color.white;
        BackBtn.GetComponent<Image>().sprite = BackImg;
        RestartBtn.GetComponent<Image>().sprite = RestartImg;
        CurrentToolText.GetComponent<TMP_Text>().color = Color.black;

        CurrentToolText.GetComponent<TMP_Text>().text = text + PlusText + electrolysisText;
        AnotherToolBtn.SetActive(true);
        HelpBtn.SetActive(true);
    }

    public void MagneticRotate()
    {
        magneticIns.transform.Rotate(0, -180, 0);
    }

    public void HelpPop()
    {
        //HelpBtn.SetActive(false);
        HelpBtn.GetComponent<Button>().interactable = false;
        HelpPopup.SetActive(true);
    }

    public void QuitHelpPopup() // 팝업 제어(도움말)
    {
       // HelpBtn.SetActive(true);
        HelpBtn.GetComponent<Button>().interactable = true;
        HelpPopup.SetActive(false);
    }
}
