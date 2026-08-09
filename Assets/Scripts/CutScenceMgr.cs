using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutScenceMgr : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private Image CutScenceImage; // 화면에 띄울 UI Image
    public TextMeshProUGUI dialogueText;
    public GameObject cutscenePanel;

    [Header("컷씬 이미지 리스트 (순서대로 등록)")]
    [SerializeField] private Sprite[] cutsceneSprites; // 인스펙터에서 순서대로 넣을 이미지 배열

    private string[] lines;
    private int currentLine = 0;

    void Start()
    {
        LoadDialogue();
    }

    void Update()
    {
        if (!GameMgr.I.CutSceneActived)
        {
            cutscenePanel.SetActive(false);
        }
        else
        {
            cutscenePanel.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Return) && GameMgr.I.CutSceneActived)
        {
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

            // 첫 번째 대사 및 첫 번째 이미지 로드
            ShowNextDialogue();
        }
        else
        {
            Debug.LogWarning($"[대화 로드 실패] Resources/{fileName}.txt 파일을 찾을 수 없습니다.");
        }
    }

    void ShowNextDialogue()
    {
        // 대사가 없거나 끝까지 읽었을 때
        if (lines == null || currentLine >= lines.Length)
        {
            GameMgr.I.GotoNextScene();
            return;
        }

        // 1. 대사 텍스트 출력
        dialogueText.text = lines[currentLine].Trim();

        // 2. [추가] 컷씬 이미지 순서대로 변경
        // 현재 대사 줄 번호(currentLine)에 맞춰 등록된 스프라이트를 교체합니다.
        if (cutsceneSprites != null && currentLine < cutsceneSprites.Length)
        {
            if (cutsceneSprites[currentLine] != null)
            {
                CutScenceImage.sprite = cutsceneSprites[currentLine];
            }
        }

        // 다음 대사 번호 증가
        currentLine++;
    }
}
