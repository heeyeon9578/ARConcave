
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveGame : MonoBehaviour
{
    [SerializeField] GameObject image; //바둑판의 "한 칸"
    [SerializeField] GameObject parent;
    [SerializeField] List<Button> images;
    [SerializeField] GameObject black;
    [SerializeField] GameObject white;
    [SerializeField] GameObject gameEndPanel;
    int[] blackCountCol = new int[18];
    int[] whiteCountCol = new int[18];
    int blackCountRow = 0;
    int whiteCountRow = 0;
    int blackCountDia = 0;
    int whiteCountDia = 0;
    int blackCountDia2 = 0;
    int whiteCountDia2 = 0;
    int[,] OmokBoard;
    private bool whatColor= false;
    int count = 0;
    private void Start()
    {
        InitGame(); //게임 환경 초기화
    }
    public void clickBtn(GameObject go)
    {
        if (whatColor == false)
        {
            findOmok(go.name, 400); //black
            go.transform.GetChild(0).gameObject.SetActive(true);
            Destroy(go.transform.GetChild(1).gameObject);
            whatColor = true;
        }
        else
        {
            findOmok(go.name, 500); // white
            go.transform.GetChild(1).gameObject.SetActive(true);
            Destroy(go.transform.GetChild(0).gameObject);
            whatColor = false;
        }
        Destroy(go.GetComponent<Button>());
        check();

    }//오목알을 두기
    public void check() //가로 세로 대각선에 블랙이나 화이트가 5개가 되었는지 체크
    {
        checkRow();
        checkCol();
        checkDia();
        checkDia2();
    }
    public void checkRow() //가로에 블랙이나 화이트가 5개 되었는지 체크
    {
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 14; j++)
            {
                if (OmokBoard[i, j] == 400) //블랙
                {
                    blackCountRow++;
                    if (blackCountRow == 5)
                    {
                        gameEnd("=블랙=이 가로로 5개가 되어서 이겼습니다~!! ^3^");
                    }
                    if (OmokBoard[i, j + 1] != 400)
                    {
                        blackCountRow = 0;

                    }

                }
                else if (OmokBoard[i, j] == 500) //화이트
                {

                    whiteCountRow++;
                    if (whiteCountRow == 5)
                    {
                        gameEnd("=화이트=가 가로로 5개가 되어서 이겼습니다~!! ^3^");
                    }
                    if (OmokBoard[i, j + 1] != 500)
                    {
                        whiteCountRow = 0;
                    }
                }
            }
        }
    }
    public void checkCol()//세로에 블랙이나 화이트가 5개 되었는지 체크
    {
        for (int j = 0; j < 18; j++)
        {
            for (int i = 0; i < 14; i++)
            {

                if (OmokBoard[i, j] == 400)//블랙
                {
                    blackCountCol[j]++;
                    if (blackCountCol[j] == 5)
                    {
                        gameEnd("=블랙=이 세로로 5개가 되어서 이겼습니다~!! >3<");
                    }
                    if (OmokBoard[i + 1, j] != 400)
                    {
                        blackCountCol[j] = 0;

                    }
                }
                else if (OmokBoard[i, j] == 500)//화이트
                {
                    whiteCountCol[j]++;
                    if (whiteCountCol[j] == 5)
                    {
                        gameEnd("=화이트=가 세로로 5개가 되어서 이겼습니다~!! >3<");
                    }
                    if (OmokBoard[i + 1, j] != 500)
                    {
                        whiteCountCol[j] = 0;

                    }
                }
            }
        }

    }
    public void checkDia()//대각선(\)에 블랙이나 화이트가 5개 되었는지 체크
    {
        Debug.Log("checkDia");
        int temp = 0;

        for(int i=0; i<14; i++) {
            
            for (int j = 0; j < 14; j++)
            {
                int d = j;
                for (int r = temp; r < temp+5; r++)
                {
                    if (OmokBoard[r, d] == 400)//블랙
                    {
                        blackCountDia++;
                    }else if(OmokBoard[r, d] == 500)//화이트
                    {
                        whiteCountDia++;
                    }
                    d++;
                }
                if (blackCountDia == 5)
                {
                    gameEnd("=블랙=이 대각선(\\)으로 5개가 되어서 이겼습니다~!! >0<");
                }else if (whiteCountDia==5)
                {
                    gameEnd("=화이트=가 대각선(\\)으로 5개가 되어서 이겼습니다~!! >0<");
                }
                else
                {
                    blackCountDia = 0;
                    whiteCountDia = 0;
                }

            }
            temp++;
        }
    }
    public void checkDia2()//대각선(/)에 블랙이나 화이트가 5개 되었는지 체크
    {
        Debug.Log("checkDia2");
        int temp = 0;

        for (int i = 0; i < 14; i++)
        {

            for (int j = 4; j <18; j++)
            {
                int d = j;
                for (int r = temp; r < temp + 5; r++)
                {
                    if (OmokBoard[r, d] == 400)//블랙
                    {
                        blackCountDia2++;
                    }
                    else if (OmokBoard[r, d] == 500)//화이트
                    {
                        whiteCountDia2++;
                    }
                    d--;
                }
                if (blackCountDia2 == 5)
                {
                    gameEnd("=블랙=이 대각선(/)으로 5개가 되어서 이겼습니다~!! ^0^");
                }
                else if (whiteCountDia2 == 5)
                {
                    gameEnd("=화이트=가 대각선(/)으로 5개가 되어서 이겼습니다~!! ^0^");
                }
                else
                {
                    blackCountDia2 = 0;
                    whiteCountDia2 = 0;
                }

            }
            temp++;
        }
    }
    public void findOmok(string name, int color) //블랙이나 화이트로 설정
    {
        int val = int.Parse(name);
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 18; j++)
            {
               if (OmokBoard[i, j] == val)
                {
                    OmokBoard[i, j] = color;
                    return;
                }
            }
        }
    }
    public void gameEnd(string str)//게임 오버 (누군가가 승리하였을 경우)
    {
        InitGame();
        gameEndPanel = Instantiate(Resources.Load("Canvas", typeof(GameObject))) as GameObject;
        gameEndPanel.GetComponentInChildren<Text>().text = str;
        gameEndPanel.SetActive(true);
    }
    public void InitGame() //게임 초기화 (재시작)
    {
        images.Clear();
        OmokBoard = new int[18, 18];
        for(int i=0; i<parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < 324; i++)
        {
            int temp = i;
            GameObject gobj = Instantiate(image, parent.transform);
            images.Add(gobj.GetComponent<Button>());
            images[temp].onClick.AddListener(() => clickBtn(gobj));
            Instantiate(black, gobj.transform);
            Instantiate(white, gobj.transform);
            gobj.name = temp.ToString();
        }
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                OmokBoard[i, j] = count++;
            }
        }
        count = 0;
        for(int  i=0; i< blackCountCol.Length; i++)
        {
            blackCountCol[i] = 0;
            whiteCountCol[i] = 0;

        }
        blackCountDia = 0;
        whiteCountDia = 0;
        blackCountDia2 = 0;
        whiteCountDia2 = 0;
        whiteCountRow = 0;
        blackCountRow = 0;
    }
}