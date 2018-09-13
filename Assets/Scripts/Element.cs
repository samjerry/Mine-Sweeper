using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour {

	public bool _isMine;

	[SerializeField]private Sprite[] _cellTexture;
	[SerializeField]private Sprite _mineTexture;
	[SerializeField]private GameObject _wScreen; 
	// [SerializeField]private 

	private bool _interactable = true;

	void Start() {
		_wScreen.SetActive(false);

		_isMine = Random.value < 0.15;

		int _x = Mathf.RoundToInt(transform.position.x);
		int _y = Mathf.RoundToInt(transform.position.y);
		Grid.elements [_x, _y] = this; 
	}

	public void loadTexture(int adjecentCount){
		if (_isMine){
			GetComponent<SpriteRenderer>().sprite = _mineTexture;
		} else {
			GetComponent<SpriteRenderer>().sprite = _cellTexture[adjecentCount];
		}
	}

	public bool isCovered(){
		return GetComponent<SpriteRenderer>().sprite.texture.name == "Default";
	}

	private void OnMouseUpAsButton() {

		if (_isMine){
			Grid.uncoverMines();
			Debug.Log("Lost!");
		} else {
			int _x = Mathf.RoundToInt(transform.position.x);
			int _y = Mathf.RoundToInt(transform.position.y);
			loadTexture(Grid.adjecentMines(_x, _y));	

			Grid.FFuncover(_x, _y, new bool[Grid._w, Grid._h]);

			if (Grid.isFinished()){
				_wScreen.SetActive(true);
				Debug.Log("Win!");
			}
		} 
	}
}