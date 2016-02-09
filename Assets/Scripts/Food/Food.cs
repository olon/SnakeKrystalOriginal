using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{
    void Awake()
    {
        GenerateNewFood("RedApple");
    }

    // Need to be called from another class
    public void GenerateNewFood(string foodItem)
    {
        StartCoroutine(CoGenerateNewFood(foodItem));
    }

    // Create an instance of food
    public IEnumerator CoGenerateNewFood(string foodItem)
    {
        GameObject food = (GameObject)Instantiate(Resources.Load("Prefabs/" + foodItem, typeof(GameObject)));

        Vector3 randomV = new Vector3(Random.Range(-16, 16) * 5 + 2.5f, -1, Random.Range(-11, 11) * 5 + 2.5f);

        Collider[] mas = Physics.OverlapSphere(randomV, 4);
        if (mas.Length != 0)
        {
            foreach (var item in mas)
            {
                if (item.GetComponent<Snake>() || item.GetComponent<Tail>() || item.GetComponent<SnakeThirdPersonControll>())
                {
                    StartCoroutine(CoGenerateNewFood(foodItem));
                    yield return null;
                }
            }
            food.transform.position = randomV;
        }
        else
        {
            food.transform.position = randomV;

            yield return null;
        }
    }

}
