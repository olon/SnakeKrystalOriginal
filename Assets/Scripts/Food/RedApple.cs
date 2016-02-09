using UnityEngine;

public class RedApple : MonoBehaviour {

private int points = 10;

//Eat Apple
public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Snake>() || other.gameObject.GetComponent<SnakeThirdPersonControll>())
        {
            Destroy(gameObject);

            var Snake = FindObjectOfType<Snake>();
            var Food = FindObjectOfType<Food>();
            var SnakeThirdPerson = FindObjectOfType<SnakeThirdPersonControll>();
            if (Food && Snake)
            {
                Game.Instance.points += points;
                Food.GenerateNewFood("RedApple");
                Snake.AddNewBody();
            }
            else if (Food && SnakeThirdPerson)
            {
                GameThirdPerson.Instance.points += points;
                Food.GenerateNewFood("RedApple");
                SnakeThirdPerson.AddNewBody();
            }
        }
    }
}
