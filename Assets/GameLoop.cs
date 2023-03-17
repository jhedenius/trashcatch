using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public TMP_Text hpCounter;
    public TMP_Text pointCounter;

    public static int hp = 10;

    public GameObject player;

    public static int points = 0;

    public static bool GameEnded = false;

    public GameObject trashTemplatePrefab;

    public GameObject gameOver;

    public List<Sprite> trashImages;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOver.SetActive(false);
        StartCoroutine(WaitAndSpawnTrash(2.0f));
    }

    public void SpawnTrash()
    {
        if (GameEnded)
        {
            return;
        }
        var xPos = Random.value * Screen.width;
        var yPos = Screen.height;
        var pos = new Vector3(xPos, yPos, 1);
        Debug.Log("Spawning trash at " + Camera.main.ScreenToWorldPoint(pos));
        
        var trash = GameObject.Instantiate(trashTemplatePrefab, Camera.main.ScreenToWorldPoint(pos), Quaternion.identity);
        var scale = 0.2f + Random.value;
        trash.transform.localScale = new Vector3(scale, scale, 1);
        trash.GetComponent<Rigidbody2D>().gravityScale = (1.0f + (Random.value * 1.0f));
        trash.GetComponent<SpriteRenderer>().sprite = trashImages[(int)(Random.value * trashImages.Count)];
    }

    private IEnumerator WaitAndSpawnTrash(float seconds)
    {
        while (!GameEnded)
        {
            float rnd = Random.value;
            yield return new WaitForSeconds(1.0f + (rnd * seconds));
            SpawnTrash();            
        }
        gameOver.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        hpCounter.text = "HP: " + hp;
        pointCounter.text = "Points: " + points;
        
        if (Input.GetButtonDown("Horizontal"))
        {
            Debug.Log(Input.mousePosition);
        }
        
        float translation = Input.GetAxis("Horizontal") * 10.0f;
        //float rotation = Input.GetAxis("Horizontal") * 100.0f;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        //rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        player.transform.Translate(translation, 0, 0);
    }


    public static void AddPoints(int i)
    {
        points += i;
    }

    public static void SubtractHP(int i)
    {
        hp -= i;
        if (hp <= 0)
        {
            GameEnded = true;
        }
    }
}
