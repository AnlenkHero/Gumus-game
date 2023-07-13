using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypeWriter : MonoBehaviour
{
	[SerializeField] Text text;
	[SerializeField] TMP_Text tmpProText;
	private string _writer;
	private Coroutine _coroutine;

	[SerializeField] float delayBeforeStart;
	[SerializeField] float timeBtwChars = 0.1f;
	[SerializeField] string leadingChar = "";
	[SerializeField] bool leadingCharBeforeDelay;
	[Space(10)] [SerializeField] private bool startOnEnable;
	
	[Header("Collision-Based")]
	[SerializeField] private bool clearAtStart;
	[SerializeField] private bool startOnCollision;
	enum Options {Clear, Complete}
	[SerializeField] Options collisionExitOptions;
	
	void Awake()
	{
		if(text != null)
		{
			_writer = text.text;
		}
		
		if (tmpProText != null)
		{
			_writer = tmpProText.text;
		}
	}

	void Start()
	{
		if (!clearAtStart ) return;
		if(text != null)
		{
			text.text = "";
		}
		
		if (tmpProText != null)
		{
			tmpProText.text = "";
		}
	}

	private void OnEnable()
	{
		print("On Enable!");
		if(startOnEnable) StartTypewriter();
	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		print("Collision!");
		if (startOnCollision)
		{
			StartTypewriter();
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (collisionExitOptions == Options.Complete)
		{
			if (text != null)
			{
				text.text = _writer;
			}

			if (tmpProText != null)
			{
				tmpProText.text = _writer;
			}
		}
		// clear
		else
		{
			if (text != null)
			{
				text.text = "";
			}

			if (tmpProText != null)
			{
				tmpProText.text = "";
			}
		}
		
		StopAllCoroutines();
	}


	public void StartTypewriter()
	{
		StopAllCoroutines();

		if(text != null)
		{
			text.text = "";

			StartCoroutine(nameof(TypeWriterText));
		}
		
		if (tmpProText != null)
		{
			tmpProText.text = "";

			StartCoroutine(nameof(TypeWriterTMP));
		}
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	IEnumerator TypeWriterText()
	{
		text.text = leadingCharBeforeDelay ? leadingChar : "";

		yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in _writer)
		{
			if (text.text.Length > 0)
			{
				text.text = text.text.Substring(0, text.text.Length - leadingChar.Length);
			}
			text.text += c;
			text.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if(leadingChar != "")
        {
			text.text = text.text.Substring(0, text.text.Length - leadingChar.Length);
		}

		yield return null;
	}

	IEnumerator TypeWriterTMP()
    {
	    tmpProText.text = leadingCharBeforeDelay ? leadingChar : "";

        yield return new WaitForSeconds(delayBeforeStart);

		foreach (char c in _writer)
		{
			if (tmpProText.text.Length > 0)
			{
				tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
			}
			tmpProText.text += c;
			tmpProText.text += leadingChar;
			yield return new WaitForSeconds(timeBtwChars);
		}

		if (leadingChar != "")
		{
			tmpProText.text = tmpProText.text.Substring(0, tmpProText.text.Length - leadingChar.Length);
		}
	}
}
