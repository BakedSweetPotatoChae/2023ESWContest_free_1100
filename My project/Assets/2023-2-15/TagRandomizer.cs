using UnityEngine;

public class TagRandomizer : MonoBehaviour {

    public string[] tags = { "tag1", "tag2" };
    public float delay = 1.0f;

    void Start() {
        InvokeRepeating("GenerateTag", delay, delay);
    }

    void GenerateTag() {
        int index = Random.Range(0, tags.Length);
        Debug.Log(tags[index]);
    }
}