
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveGame : MonoBehaviour
{
    [SerializeField] GameObject image;
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
        InitGame();
    }

//오목알을 두기
    public void clickBtn(GameObject go)
    {

        Debug.Log(go.name);
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

    }
    public void check()
    {
        checkRow();
        checkCol();
        checkDia();
        checkDia2();
    }
    public void checkDia()
    {
        Debug.Log("checkDia");
        int temp = 0;

        for(int i=0; i<14; i++) {
            
            for (int j = 0; j < 14; j++)
            {
                int d = j;
                for (int r = temp; r < temp+5; r++)
                {
                    if (OmokBoard[r, d] == 400)
                    {
                        blackCountDia++;
                    }else if(OmokBoard[r, d] == 500)
                    {
                        whiteCountDia++;
                    }
                    d++;
                }
                if (blackCountDia == 5)
                {
                    gameEnd("checkDia - black win!");
                }else if (whiteCountDia==5)
                {
                    gameEnd("checkDia - white win!");
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
    public void checkDia2()
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
                    if (OmokBoard[r, d] == 400)
                    {
                        blackCountDia2++;
                    }
                    else if (OmokBoard[r, d] == 500)
                    {
                        whiteCountDia2++;
                    }
                    d--;
                }
                if (blackCountDia2 == 5)
                {
                    gameEnd("checkDia2 - black win!");
                }
                else if (whiteCountDia2 == 5)
                {
                    gameEnd("checkDia2 - white win!");
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
    public void findOmok(string name, int color)
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

    public void checkRow()
    {
        Debug.Log("checkFive");

        for (int i = 0; i < 18; i++)
        {
            for (int j = 0; j < 14; j++)
            { 
                if (OmokBoard[i, j] == 400 )
                {
                    blackCountRow++;
                    if (blackCountRow == 5)
                    {
                        gameEnd("checkFive - black win!");
                    }
                    if (OmokBoard[i, j + 1] != 400)
                    {
                        blackCountRow = 0;

                    }

                }
                else if(OmokBoard[i, j] == 500)
                {

                    whiteCountRow++;
                    if (whiteCountRow == 5)
                    {
                        gameEnd("checkFive - white win!");
                    }
                    if (OmokBoard[i, j + 1] != 500)
                    {
                        whiteCountRow = 0;
                    }
                }
            }
        }
    }
    public void checkCol()
    {
        Debug.Log("checkCol");

        for(int j=0; j < 18; j++)
        {
            for (int i = 0; i < 14; i++)
            {

                if (OmokBoard[i, j] == 400)
                {
                    blackCountCol[j]++;
                    if (blackCountCol[j] == 5)
                    {
                        gameEnd("checkCol - black win!");
                    }
                    if (OmokBoard[i + 1, j] != 400)
                    {
                        blackCountCol[j] = 0;

                    }
                }
                else if (OmokBoard[i, j] == 500)
                {
                    whiteCountCol[j]++;
                    if (whiteCountCol[j] == 5)
                    {
                        gameEnd("checkCol - white win!");
                    }
                    if (OmokBoard[i + 1, j] != 500)
                    {
                        whiteCountCol[j] = 0;

                    }
                }
            }
        }
        
    }
    public void gameEnd(string str)
    {
        InitGame();
        gameEndPanel = Instantiate(Resources.Load("Canvas", typeof(GameObject))) as GameObject;
        gameEndPanel.GetComponentInChildren<Text>().text = str;
        gameEndPanel.SetActive(true);
    }

    public void InitGame()
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