
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveGame : MonoBehaviour
{
    [SerializeField] GameObject image; //바둑판의 "한 칸"
    [SerializeField] GameObject parent; //바둑판
    [SerializeField] List<Button> images; //바둑판들
    [SerializeField] GameObject blackObj; //검은 돌
    [SerializeField] GameObject whiteObj; //하얀 돌
    [SerializeField] GameObject gameEndPanel; //게임 엔딩에 쓰이는 패널

    int black = 400;
    int white = 500;

    int blackCountCol = 0;
    int whiteCountCol =0;
    int blackCountRow = 0;
    int whiteCountRow = 0;
    int blackCountDia = 0;
    int whiteCountDia = 0;
    int blackCountDia2 = 0;
    int whiteCountDia2 = 0;
    int[,] OmokBoard;
    private bool whatColor = false; //하얀 돌/ 검은 돌 순서인지 결정
    private void Start()
    {
        InitGame(); //게임 환경 초기화
    }
    /// <summary>
    /// clickBtn을 스크립트로 각각의 버튼에 붙힌 이유는 파라미터가 클릭한 오브젝트의 정보를 받아와야 하기 때문에
    /// </summary>
    /// <param name="go"></param>
    public void clickBtn(GameObject go)//오목알을 두기
    {
        if (whatColor == false)
        {
            findOmok(go.name, black); //black
            go.transform.GetChild(0).gameObject.SetActive(true); //버튼이 붙은 image의 첫 번째 자식오브젝트 = blackObj
            whatColor = true;
            UIManager.Instance.setColor(white);
        }
        else
        {
            findOmok(go.name, white); // white
            go.transform.GetChild(1).gameObject.SetActive(true);//버튼이 붙은 image의 두 번째 자식오브젝트 = whiteObj
            whatColor = false;
            UIManager.Instance.setColor(black);

        }
        Destroy(go.GetComponent<Button>()); //한 번 눌린 버튼은 해당 게임에서 다시 눌리지 않도록
        check();

    }
    public void check() //가로, 세로, 대각선에 블랙/화이트가 5개가 되었는지 체크
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
            blackCountRow = 0;
            whiteCountRow = 0;
            for (int j = 0; j < 18; j++)
            {
                if (OmokBoard[i, j] == black) //블랙
                {
                    blackCountRow++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountRow:  " + blackCountRow);
                    if (blackCountRow == 5)
                    {
                        gameEnd("=블랙=이 가로로 5개가 되어서 이겼습니다~!! ^3^");
                    }
                    if(j < 17)
                    {
                        if (OmokBoard[i, j + 1] != black)
                        {
                            blackCountRow = 0;

                        }
                    }
                    

                }
                else if (OmokBoard[i, j] == white) //화이트
                {

                    whiteCountRow++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountRow:  " + whiteCountRow);

                    if (whiteCountRow == 5)
                    {
                        gameEnd("=화이트=가 가로로 5개가 되어서 이겼습니다~!! ^3^");
                    }
                    if (j < 17)
                    {
                        if (OmokBoard[i, j + 1] != white)
                        {
                            whiteCountRow = 0;
                        }
                    }

                }
            }
        }
    }
    public void checkCol()//세로에 블랙이나 화이트가 5개 되었는지 체크
    {
        for (int j = 0; j < 18; j++)
        {
            
            for (int i = 0; i < 18; i++)
            {
                

                if (OmokBoard[i, j] == black)//블랙
                {
                    blackCountCol++;
                    Debug.Log("OmokBoard["+i+","+ j+"]:  "+ OmokBoard[i, j]+"   blackCountCol:  " + blackCountCol);
                    if (blackCountCol == 5)
                    {
                        gameEnd("=블랙=이 세로로 5개가 되어서 이겼습니다~!! >3<");
                    }
                    if (i < 17)
                    {
                        if (OmokBoard[i + 1, j] != black)
                        {
                            blackCountCol = 0;

                        }
                    }
                   
                }
                else if (OmokBoard[i, j] == white)//화이트
                {
                    whiteCountCol++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountCol:   " +whiteCountCol);

                    
                    if (whiteCountCol == 5)
                    {
                        gameEnd("=화이트=가 세로로 5개가 되어서 이겼습니다~!! >3<");
                    }
                    if (i < 17)
                    {
                        if (OmokBoard[i + 1, j] != white)
                        {
                            whiteCountCol = 0;

                        }
                    }
                    
                }
            }
            blackCountCol = 0;
            whiteCountCol = 0;
        }

    }
    public void checkDia()//대각선(\)에 블랙이나 화이트가 5개 되었는지 체크
    {
        Debug.Log("checkDia");
        int temp = 0;

        for (int i = 0; i < 14; i++)
        {

            for (int j = 0; j < 14; j++)
            {
                int d = j;
                for (int r = temp; r < temp + 5; r++)
                {
                    if (OmokBoard[r, d] == black)//블랙
                    {
                        blackCountDia++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia:  " + blackCountDia);

                    }
                    else if (OmokBoard[r, d] == white)//화이트
                    {
                        whiteCountDia++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia:  " + whiteCountDia);

                    }
                    d++;
                }
                if (blackCountDia == 5)
                {
                    gameEnd("=블랙=이 대각선(\\)으로 5개가 되어서 이겼습니다~!! >0<");
                }
                else if (whiteCountDia == 5)
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

            for (int j = 4; j < 18; j++)
            {
                int d = j;
                for (int r = temp; r < temp + 5; r++)
                {
                    if (OmokBoard[r, d] == black)//블랙
                    {
                        blackCountDia2++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia2:  " + blackCountDia2);

                    }
                    else if (OmokBoard[r, d] == white)//화이트
                    {
                        whiteCountDia2++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia2:  " + whiteCountDia2);

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
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            Destroy(parent.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < 324; i++)
        {
            int temp = i;
            GameObject gobj = Instantiate(image, parent.transform);
            images.Add(gobj.GetComponent<Button>());
            images[temp].onClick.AddListener(() => clickBtn(gobj));
            Instantiate(blackObj, gobj.transform);
            Instantiate(whiteObj, gobj.transform);
            gobj.name = temp.ToString();
        }

        int count = 0;
        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                OmokBoard[i, j] = count++;
            }
        }
        

        blackCountDia = 0;
        whiteCountDia = 0;
        blackCountDia2 = 0;
        whiteCountDia2 = 0;
        whiteCountRow = 0;
        blackCountRow = 0;
        blackCountCol = 0;
        whiteCountCol = 0;
    }
}