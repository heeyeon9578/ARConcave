
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConcaveGame : MonoBehaviour
{
    [SerializeField] GameObject image; //�ٵ����� "�� ĭ"
    [SerializeField] GameObject parent; //�ٵ���
    [SerializeField] List<Button> images; //�ٵ��ǵ�
    [SerializeField] GameObject blackObj; //���� ��
    [SerializeField] GameObject whiteObj; //�Ͼ� ��
    [SerializeField] GameObject gameEndPanel; //���� ������ ���̴� �г�

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
    private bool whatColor = false; //�Ͼ� ��/ ���� �� �������� ����
    private void Start()
    {
        InitGame(); //���� ȯ�� �ʱ�ȭ
    }
    /// <summary>
    /// clickBtn�� ��ũ��Ʈ�� ������ ��ư�� ���� ������ �Ķ���Ͱ� Ŭ���� ������Ʈ�� ������ �޾ƿ;� �ϱ� ������
    /// </summary>
    /// <param name="go"></param>
    public void clickBtn(GameObject go)//������� �α�
    {
        if (whatColor == false)
        {
            findOmok(go.name, black); //black
            go.transform.GetChild(0).gameObject.SetActive(true); //��ư�� ���� image�� ù ��° �ڽĿ�����Ʈ = blackObj
            whatColor = true;
            UIManager.Instance.setColor(white);
        }
        else
        {
            findOmok(go.name, white); // white
            go.transform.GetChild(1).gameObject.SetActive(true);//��ư�� ���� image�� �� ��° �ڽĿ�����Ʈ = whiteObj
            whatColor = false;
            UIManager.Instance.setColor(black);

        }
        Destroy(go.GetComponent<Button>()); //�� �� ���� ��ư�� �ش� ���ӿ��� �ٽ� ������ �ʵ���
        check();

    }
    public void check() //����, ����, �밢���� ��/ȭ��Ʈ�� 5���� �Ǿ����� üũ
    {
        checkRow();
        checkCol();
        checkDia();
        checkDia2();
    }
    public void checkRow() //���ο� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
    {
        for (int i = 0; i < 18; i++)
        {
            blackCountRow = 0;
            whiteCountRow = 0;
            for (int j = 0; j < 18; j++)
            {
                if (OmokBoard[i, j] == black) //��
                {
                    blackCountRow++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountRow:  " + blackCountRow);
                    if (blackCountRow == 5)
                    {
                        gameEnd("=��=�� ���η� 5���� �Ǿ �̰���ϴ�~!! ^3^");
                    }
                    if(j < 17)
                    {
                        if (OmokBoard[i, j + 1] != black)
                        {
                            blackCountRow = 0;

                        }
                    }
                    

                }
                else if (OmokBoard[i, j] == white) //ȭ��Ʈ
                {

                    whiteCountRow++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountRow:  " + whiteCountRow);

                    if (whiteCountRow == 5)
                    {
                        gameEnd("=ȭ��Ʈ=�� ���η� 5���� �Ǿ �̰���ϴ�~!! ^3^");
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
    public void checkCol()//���ο� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
    {
        for (int j = 0; j < 18; j++)
        {
            
            for (int i = 0; i < 18; i++)
            {
                

                if (OmokBoard[i, j] == black)//��
                {
                    blackCountCol++;
                    Debug.Log("OmokBoard["+i+","+ j+"]:  "+ OmokBoard[i, j]+"   blackCountCol:  " + blackCountCol);
                    if (blackCountCol == 5)
                    {
                        gameEnd("=��=�� ���η� 5���� �Ǿ �̰���ϴ�~!! >3<");
                    }
                    if (i < 17)
                    {
                        if (OmokBoard[i + 1, j] != black)
                        {
                            blackCountCol = 0;

                        }
                    }
                   
                }
                else if (OmokBoard[i, j] == white)//ȭ��Ʈ
                {
                    whiteCountCol++;
                    Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountCol:   " +whiteCountCol);

                    
                    if (whiteCountCol == 5)
                    {
                        gameEnd("=ȭ��Ʈ=�� ���η� 5���� �Ǿ �̰���ϴ�~!! >3<");
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
    public void checkDia()//�밢��(\)�� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
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
                    if (OmokBoard[r, d] == black)//��
                    {
                        blackCountDia++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia:  " + blackCountDia);

                    }
                    else if (OmokBoard[r, d] == white)//ȭ��Ʈ
                    {
                        whiteCountDia++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia:  " + whiteCountDia);

                    }
                    d++;
                }
                if (blackCountDia == 5)
                {
                    gameEnd("=��=�� �밢��(\\)���� 5���� �Ǿ �̰���ϴ�~!! >0<");
                }
                else if (whiteCountDia == 5)
                {
                    gameEnd("=ȭ��Ʈ=�� �밢��(\\)���� 5���� �Ǿ �̰���ϴ�~!! >0<");
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
    public void checkDia2()//�밢��(/)�� ���̳� ȭ��Ʈ�� 5�� �Ǿ����� üũ
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
                    if (OmokBoard[r, d] == black)//��
                    {
                        blackCountDia2++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   blackCountDia2:  " + blackCountDia2);

                    }
                    else if (OmokBoard[r, d] == white)//ȭ��Ʈ
                    {
                        whiteCountDia2++;
                        Debug.Log("OmokBoard[" + i + "," + j + "]:  " + OmokBoard[i, j] + "   whiteCountDia2:  " + whiteCountDia2);

                    }
                    d--;
                }
                if (blackCountDia2 == 5)
                {
                    gameEnd("=��=�� �밢��(/)���� 5���� �Ǿ �̰���ϴ�~!! ^0^");
                }
                else if (whiteCountDia2 == 5)
                {
                    gameEnd("=ȭ��Ʈ=�� �밢��(/)���� 5���� �Ǿ �̰���ϴ�~!! ^0^");
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
    public void findOmok(string name, int color) //���̳� ȭ��Ʈ�� ����
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
    public void gameEnd(string str)//���� ���� (�������� �¸��Ͽ��� ���)
    {
        InitGame();
        gameEndPanel = Instantiate(Resources.Load("Canvas", typeof(GameObject))) as GameObject;
        gameEndPanel.GetComponentInChildren<Text>().text = str;
        gameEndPanel.SetActive(true);
    }
    public void InitGame() //���� �ʱ�ȭ (�����)
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