using UnityEngine;
using UnityEngine.UI;

public class MainController : MonoBehaviour
{
	private int score = 0;
	[SerializeField] private Text ScoreCountText;

	#region Monobehaviour methods

	private void Start()
	{
		ScoreCountText.text = score.ToString();
		EventManager.AddEventListener(EventManager.Events.PLAYER_BODY_HIT, (object value) =>
		{
			score -= (int)value;
			if (score < 0) score = 0;
			ScoreCountText.text = score.ToString();
		});
		EventManager.AddEventListener(EventManager.Events.PLAYER_FEET_HIT, (value) =>
		{
			score += (int)value;
			ScoreCountText.text = score.ToString();
		});
	} //end method Start

	#endregion
} //end class MainController