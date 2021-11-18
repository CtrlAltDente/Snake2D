using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
	public class SliderValueManager : MonoBehaviour
	{
		[SerializeField]
		private Text _valueLabel;
		private Slider _slider;

		private void Start()
		{
			_slider = GetComponent<Slider>();
			ChangeValueInLabel();
		}

		public void ChangeValueInLabel()
		{
			_valueLabel.text = _slider.value.ToString();
		}

		public float GetValue()
		{
			return _slider.value;
		}
	}
}