using UnityEngine;

public class TagRandomizer : MonoBehaviour {

    public string[] tags = { "tag1", "tag2" };
    public float delay = 1.0f;

    void Start() {
        InvokeRepeating("GenerateTag", delay, delay);
        // GenerateTag를 딜레이 시간마다 반복 호출
    }

    void GenerateTag() {
        int index = Random.Range(0, tags.Length); // 무작위 선택
        Debug.Log(tags[index]); // 무작위 선택된 태그 출력
    }
}