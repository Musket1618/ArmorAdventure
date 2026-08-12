using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScenceMgr : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private Image CutScenceImage;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI nameText;
    public GameObject cutscenePanel;

    [Header("컷씬 이미지 리스트")]
    [SerializeField] private Sprite[] cutsceneSprites;

    private string[] lines;
    private int currentLine = 0;

    // [추가] 대기와 이벤트 처리를 관리할 변수들
    private bool isEventWaiting = false;
    private string pendingEventName = "";

    void Start()
    {
        LoadDialogue();
    }

    void Update()
    {
        if (!GameMgr.I.CutSceneActived)
        {
            cutscenePanel.SetActive(false);
            return;
        }
        else
        {
            cutscenePanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return) && GameMgr.I.CutSceneActived)
        {
            // 이벤트 실행 대기 중일 때 엔터를 누르면 이벤트를 실행하고 대기 상태를 해제합니다.
            if (isEventWaiting)
            {
                TriggerEvent(pendingEventName);
                isEventWaiting = false;
                pendingEventName = "";
                return;
            }

            // 일반 상태일 때는 다음 대사를 불러옵니다.
            ShowNextDialogue();
        }
    }

    public void LoadDialogue()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string fileName = "Dialogue" + currentSceneIndex;

        TextAsset textFile = Resources.Load<TextAsset>(fileName);

        if (textFile != null)
        {
            lines = textFile.text.Replace("\r", "").Split('\n');
            currentLine = 0;
            isEventWaiting = false;
            pendingEventName = "";

            ShowNextDialogue();
        }
        else
        {
            Debug.LogWarning($"[대화 로드 실패] Resources/{fileName}.txt 파일을 찾을 수 없습니다.");
        }
    }

    void ShowNextDialogue()
    {
        if (lines == null || currentLine >= lines.Length)
        {
            GameMgr.I.Nextscene();
            return;
        }

        // 1. 이미지 순서대로 변경
        if (cutsceneSprites != null && currentLine < cutsceneSprites.Length)
        {
            if (cutsceneSprites[currentLine] != null)
            {
                CutScenceImage.sprite = cutsceneSprites[currentLine];
            }
        }

        // 2. '#' 기준으로 [대사]와 [이름 및 명령어] 분리
        string[] pieces = lines[currentLine].Split('#');

        // 대사 출력 (맨 앞 조각)
        dialogueText.text = pieces[0].Trim();

        // 3. '#' 뒤에 글자(이름 or 이벤트)가 포함되어 있다면
        if (pieces.Length > 1)
        {
            string rightPart = pieces[1].Trim(); // 예: "선생님 $ ShowFace" 또는 "주인공"

            // '$' 기호가 포함되어 있다면 (이름과 이벤트가 같이 있는 경우)
            if (rightPart.Contains("$"))
            {
                string[] subPieces = rightPart.Split('$');

                string npcName = subPieces[0].Trim();
                string eventName = subPieces[1].Trim();

                // 이름 표시
                if (nameText != null) nameText.text = npcName;

                // 대사 출력 시 바로 이벤트를 틀지 않고, 다음 엔터 때 실행하도록 보관해 둡니다.
                if (!string.IsNullOrEmpty(eventName))
                {
                    isEventWaiting = true;
                    pendingEventName = eventName;
                }
            }
            else
            {
                // '$' 없이 이름만 적혀있는 경우
                if (nameText != null) nameText.text = rightPart;
            }
        }

        currentLine++;
    }

    // [이벤트 처리 함수] $ 뒤에 적힌 문자열에 맞춰 이벤트를 실행합니다.
    private void TriggerEvent(string eventName)
    {
        switch (eventName)
        {
            case "ShowFace":
                Debug.Log("ShowFace 이벤트 실행");
                break;
        }
    }
}