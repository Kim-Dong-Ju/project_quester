// 전원 장치 관련 이벤트 관리 스크립트
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EventManager : MonoBehaviour
{
    private string text, crossText, wheelText, magneticText, electrolysisText, PlusText;
    private string stepOne, stepTwo, stepThree, stepFour;
    // step마다 할 도움말 Title
    private string detailOne, detailTwo, detailThree, detailFour;
    // step마다 할 도움말 Detail
    public GameObject PowerSupply;
    public GameObject CrookesMagnetic;
    public GameObject CrookesCross;
    public GameObject CrookesWheel;
    public GameObject PlusWireEnd;
    public GameObject MinusWireEnd;
    public GameObject Magnetic;
    public GameObject SelectPanel, RotateBtn, AnotherToolBtn, CurrentToolText, Header, HelpPopup, HelpBtn, BackBtn, RestartBtn; // UI prefab들을 담을 변수들
    public GameObject mainUI; // Canvas
    public Sprite BackImgWhite, ReStartImgWhite; // Images
    private Sprite BackImg, RestartImg;
    Vector2 HelpBtnOriginPos, HelpBtnNewPos;
    GameObject crookesCrossIns, crookeswheelIns, crookesMagIns, magneticIns, plusWireIns, minusWireIns;

    // 이벤트 제어용 boolean 변수들 선언
    private bool PSWireFinish = false, ToolWireFinish = false;
    // 전원 장치에 전선을 꽂았는 지, 도구에 전선을 꽂았는 지
    private bool MagneticSelect = false, CrossSelect = false, WheelSelect = false; // 버튼을 통해 어느 것을 선택했는지 Check용
    private int count = 0, CrookesMagCount = 0, CrookesCrossCount = 0, CrookesWheelCount = 0;

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
        HelpBtnNewPos = new Vector2(-125f, 200f);
        text = CurrentToolText.GetComponent<TMP_Text>().text;
        crossText = "<b>크룩스관 십자입</b>";
        wheelText = "<b>크룩스관 회전차입</b>";
        magneticText = "<b>크룩스관 슬릿입</b>";
        electrolysisText = "<b>전기분해 장치</b>";
        PlusText = "<b> | </b>";
        stepOne = "<b></b>";
    }

    void LateUpdate()
    {
        // if(Restart)
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // }   
        PSWireFinish = PowerSupply.GetComponent<PowerSupply>().GetAllWireConnected();

        if(PSWireFinish && count == 0) // 만약 모든 전선이 연결 되었으면 실행
        {
            // GameObject child = Instantiate(SelectPanel);
            // RectTransform childRt = child.GetComponent<RectTransform>();
            // RectTransform originRt = SelectPanel.GetComponent<RectTransform>();

            // child.transform.SetParent(mainUI.transform); // 선택 판넬 생성
            
            // childRt.anchoredPosition3D = originRt.anchoredPosition3D;
            // childRt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originRt.rect.width);
            // childRt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originRt.rect.height);
            // childRt.localScale = Vector3.one;
            // childRt.localRotation = Quaternion.identity;
            // childRt.sizeDelta = Vector2.zero;
            count++; // 계속 생성되는거 방지용
            SelectWidget();
            // Header.GetComponent<Image>().color = new Color(0f, 1f, 0.8980392f, 1f);
            // CurrentToolText.GetComponent<Text>().color = Color.white;
            // SelectPanel.SetActive(true);
            // // UI 출력
            // Debug.Log(count);
        }


        // if(MagneticSelect) // 크룩스 슬릿입을 선택했을 경우
        // {
        //     // 크룩스 슬릿입 생성
        //     Instantiate(CrookesMagnetic, new Vector3(1.11f, 0.51f, 0.42f), Quaternion.Euler(CrookesMagnetic.transform.localEulerAngles));
        //     Instantiate(Magnetic, new Vector3(0, 0, 0), Quaternion.identity);
        //     Instantiate(MinusWireEnd, new Vector3(0.85f, 0.51f, 0.35f), Quaternion.identity);
        //     Instantiate(PlusWireEnd, new Vector3(0.79f, 0.51f, 0.35f), Quaternion.identity);

        //     MagneticSelect = false; // 중복 생성 등 방지용으로 false로 초기화
        //     CrookesMagCount++;
        // }

        // if(CrossSelect) // 크룩스 십자입을 선택했을 경우
        // {
        //     // 크룩스 슬릿입 생성
        //     Instantiate(CrookesCross, new Vector3(1.11f, 0.51f, 0.42f), Quaternion.Euler(CrookesCross.transform.localEulerAngles));
        //     Instantiate(MinusWireEnd, new Vector3(0.85f, 0.51f, 0.35f), Quaternion.identity);
        //     Instantiate(PlusWireEnd, new Vector3(0.79f, 0.51f, 0.35f), Quaternion.identity);

        //     CrossSelect = false; // 중복 방지를 위해 초기화
        //     CrookesCrossCount++;
        // }

        // if(WheelSelect) // 크룩스 회전차입을 선택했을 경우
        // {
        //     // 크룩스 슬릿입 생성
        //     Instantiate(CrookesWheel, new Vector3(1.11f, 0.51f, 0.42f), Quaternion.Euler(CrookesWheel.transform.localEulerAngles));
        //     Instantiate(MinusWireEnd, new Vector3(0.85f, 0.51f, 0.35f), Quaternion.identity);
        //     Instantiate(PlusWireEnd, new Vector3(0.79f, 0.51f, 0.35f), Quaternion.identity);

        //     WheelSelect = false; // 중복 방지를 위해 초기화
        //     CrookesWheelCount++;
        // }

        // if(OtherTool) // 다른 도구 실험하기를 눌렀을 경우
        // {
        //     if(CrookesCrossCount > 0) // 이전에 이미 크룩스 십자입을 생성했을 경우
        //     {
        //         Destroy(CrookesCross);
        //         Destroy(PlusWireEnd);
        //         Destroy(MinusWireEnd);
        //     }

        //     if(CrookesWheelCount > 0) // 이전에 이미 크룩스 회전차입을 생성했을 경우
        //     {
        //         Destroy(CrookesWheel);
        //         Destroy(PlusWireEnd);
        //         Destroy(MinusWireEnd);
        //     }

        //     if(CrookesMagCount > 0) // 이전에 이미 크룩스 슬릿입을 생성했을 경우
        //     {
        //         Destroy(CrookesMagnetic);
        //         Destroy(Magnetic);
        //         Destroy(PlusWireEnd);
        //         Destroy(MinusWireEnd);
        //     }

        //     count = 0;
        // }
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
        SceneManager.LoadScene(0);
    }

    public void OtherToolButton() // 다른 도구 공부하기 선택했을 경우의 이벤트
    {
        if(CrookesCrossCount > 0) // 이전에 이미 크룩스 십자입을 생성했을 경우
        {
            Destroy(crookesCrossIns);
            Destroy(plusWireIns);
            Destroy(minusWireIns);
        }

        if(CrookesWheelCount > 0) // 이전에 이미 크룩스 회전차입을 생성했을 경우
        {
            Destroy(crookeswheelIns);
            Destroy(plusWireIns);
            Destroy(minusWireIns);
        }

        if(CrookesMagCount > 0) // 이전에 이미 크룩스 슬릿입을 생성했을 경우
        {
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
        count = 0;
    }

    public void MagneticButton() // 크룩스관 슬릿입을 선택했을 경우의 이벤트
    {
        // 현재 창에 띄워진 선택창 제거
        // Destroy(SelectPanel);
        SelectPanel.SetActive(false);
        Header.GetComponent<Image>().color = Color.white;
        BackBtn.GetComponent<Image>().sprite = BackImg;
        RestartBtn.GetComponent<Image>().sprite = RestartImg;
        CurrentToolText.GetComponent<TMP_Text>().color = Color.black;

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
        Header.GetComponent<Image>().color = Color.white;
        BackBtn.GetComponent<Image>().sprite = BackImg;
        RestartBtn.GetComponent<Image>().sprite = RestartImg;
        CurrentToolText.GetComponent<TMP_Text>().color = Color.black;

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
        Header.GetComponent<Image>().color = Color.white;
        BackBtn.GetComponent<Image>().sprite = BackImg;
        RestartBtn.GetComponent<Image>().sprite = RestartImg;
        CurrentToolText.GetComponent<TMP_Text>().color = Color.black;

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
