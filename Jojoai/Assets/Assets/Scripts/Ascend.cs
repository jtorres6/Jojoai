using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascend : MonoBehaviour
{
    public void Ascension()
    {
        GetComponent<Player>().enabled = false;
        GetComponent<God>().enabled = true;
        this.transform.Find("moa").gameObject.SetActive(false);
        this.transform.Find("JojoGod").gameObject.SetActive(true);
    }

    public void Descend()
    {

        GetComponent<Player>().enabled = true;
        GetComponent<God>().enabled = false;
        this.transform.Find("moa").gameObject.SetActive(true);
        this.transform.Find("JojoGod").gameObject.SetActive(false);

    }
}
