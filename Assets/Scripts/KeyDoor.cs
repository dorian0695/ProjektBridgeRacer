using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyDoor : MonoBehaviour
{
    [SerializeField] private Brick.BrickType keyType;
    [SerializeField] private GameObject doorWing;
    [SerializeField] private Collider blocker;
    public static int howManyTimes = 0;
    public GameObject completeLevelUI;
    public void Update()
    {
        if (howManyTimes == 2)
        {
            StartCoroutine(ExampleCoroutine());
        }
    }
    IEnumerator ExampleCoroutine()
    {
        completeLevelUI.SetActive(true);
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("SampleScene");
    }

        public Brick.BrickType GetKeyType()
    {
        return keyType;
    }

    public void OpenDoor(Transform transform)
    {
        //gameObject.SetActive(false);
        if (GetKeyType() == Brick.BrickType.Yellow)
        {
            doorWing.transform.rotation = Quaternion.Euler(0, -180, 0);
            howManyTimes++;
        }
        else
        {
            doorWing.transform.rotation = Quaternion.Euler(0, -90, 0);
            howManyTimes++;

        }
        //blocker.transform.rotation = Quaternion.Euler(0, -90, 0);
        // blocker.transform.position = doorWing.transform.position;
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Key key = other.GetComponent<Key>();
        if (key != null)
        {
            AddKey(key.GetKeyType());
            Destroy(key.gameObject);
        }
    }*/
}
