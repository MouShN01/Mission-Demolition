using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    //Статическое поле, доступное любому другому коду 
    static public bool goalMet = false;

    public AudioSource src;
    public AudioClip winSound;
    public GameObject particle;

    void OnTriggerEnter(Collider other)
    {
        //Когда в область действия триггера попадает что-то, проверить, является ли это что-то снарядом
        if(other.gameObject.tag == "Projectile")
        {
            //Если это снаряд, присвоить полю goalMet значение true
            Goal.goalMet = true;

            src = GameObject.FindGameObjectWithTag("WinSound").GetComponent<AudioSource>();
            src.clip = winSound;
            src.Play();
            GameObject cloneWin = (GameObject)Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(cloneWin, 5.0f);

            // Также изменить альфа-канал цвета-канал, чтобы увеличить непрозрачность
            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.r = 0;
            c.g = 255;
            c.b = 0;
            mat.color = c;
        }
    }
          
}