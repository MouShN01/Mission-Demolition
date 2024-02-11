using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //����������� ����, ��������� ������ ������� ���� 
    static public bool goalMet = false;
    void OnTriggerEnter(Collider other)
    {
        //����� � ������� �������� �������� �������� ���-��, ���������, �������� �� ��� ���-�� ��������
        if(other.gameObject.tag == "Projectile")
        {
            //���� ��� ������, ��������� ���� goalMet �������� true
            Goal.goalMet = true;
            // ����� �������� �����-����� �����-�����, ����� ��������� ��������������
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.r = 0;
            c.g = 255;
            c.b = 0;
            mat.color = c;
        }
    }
          
}