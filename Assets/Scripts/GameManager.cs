using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    private Transform spawnPt;
    private PinsMove currentPin;//控制pin运动

    private int score = 0;
    public Text scoreText;//控制分数显示

    private Camera mainCamera;//控制游戏结束动画
    public float lerpSpeed = 3;//动画渐变速度

    private bool isGameOver = false;//判断游戏状态

    public GameObject pinPrefabs;//实例化pin的prefabs

	// Use this for initialization
	void Start () {

        spawnPt = GameObject.Find("SpawnPoint").transform;

        mainCamera = Camera.main;

        SpawnPins();
	}
	 
	// Update is called once per frame
	void Update () {
        if (isGameOver)
        {//如果游戏失败，则后续不可以继续插入针了。
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentPin.InfixPins();
            //调用pinmove脚本中的InfixPins脚本使针插入
            score++;
            //分数+1
            scoreText.text = score.ToString();
            SpawnPins();
            //每次发射完针之后要实例化一个新的针
           
        }

	}

    void SpawnPins()
    {   //实例化pinprefabs，使该脚本update方法中可调用pinmove中定义的方法
        currentPin = GameObject.Instantiate(pinPrefabs, spawnPt.position, pinPrefabs.transform.rotation).GetComponent<PinsMove>();
        //pinprefabs的rotation不为空，因此要跟上第三个参数
        //再调用getcomponent方法获取pin上的脚本pinmove组件
    }

    public void GameOver()
    {//pin相互碰撞时游戏失败
        if (isGameOver)
        {//如果再次调用检测到已经是游戏失败状态则不执行任何操作保证该方法只执行一次。
            return;
        }

        GameObject.Find("Circle").GetComponent<CircleRotate>().enabled = false;
        //如果游戏结束则禁用圆自转的脚本组件。

        StartCoroutine(GameOverAnime());

        isGameOver = true;
    }

    IEnumerator GameOverAnime()
    {
        while (true)
        {//死循环模拟update方法
            mainCamera.backgroundColor = Color.Lerp(mainCamera.backgroundColor,Color.red,lerpSpeed*Time.deltaTime);
            //镜头背景颜色由当前值渐变为红色
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, 4, lerpSpeed * Time.deltaTime);
            //镜头大小由当前值5渐变至4
            if (Mathf.Abs(mainCamera.orthographicSize - 4)<0.01f)
            {//如果镜头大小渐变至4则镜头暂停一下
                break;
            }
            yield return 0;//每一次循环暂停一帧
        }
        yield return new WaitForSeconds(1);//暂停1s
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//重新加载当前场景


    }
}
