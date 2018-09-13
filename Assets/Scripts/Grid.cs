using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour { 

	public static int _w = 10;
	public static int _h = 13;
	
	public static Element[,] elements = new Element[_w, _h];

	public static void uncoverMines(){
		foreach (Element elem in elements){
			if (elem._isMine){
				elem.loadTexture(0);
			}
		}
	}

	public static bool mineAt(int _x, int _y){
		if (_x >= 0 && _y >= 0 && _x < _w && _y < _h){
			return elements[_x, _y]._isMine;
		} 
		return false;
	}

	public static int adjecentMines(int _x, int _y){
		int count = 0;

		if (mineAt(_x, _y+1)) count++;
		if (mineAt(_x, _y-1)) count++;
		if (mineAt(_x+1, _y)) count++;
		if (mineAt(_x-1, _y)) count++;
		if (mineAt(_x+1, _y+1)) count++;
		if (mineAt(_x-1, _y+1)) count++;	
		if (mineAt(_x+1, _y-1)) count++;
		if (mineAt(_x-1, _y-1)) count++;

		return count; 
	}

  	  //-----------------------------------------------------------------\\
     //-------------------Start Flood Fill Algorithm----------------------\\
	//---------------------------------------------------------------------\\

	public static void FFuncover (int _x, int _y, bool[,] visited){
		if (_x >= 0 && _y >= 0 && _y < _h && _x < _w){
			if (visited[_x, _y])
				return;

			elements[_x, _y].loadTexture(adjecentMines(_x, _y));

			if (adjecentMines(_x, _y) > 0){
				return;
			}

			visited[_x, _y] = true;

			FFuncover(_x - 1, _y, visited);
			FFuncover(_x + 1, _y, visited);
			FFuncover(_x, _y - 1, visited);
			FFuncover(_x, _y + 1, visited);
			FFuncover(_x + 1, _y + 1, visited);
			FFuncover(_x + 1, _y - 1, visited);
			FFuncover(_x - 1, _y + 1, visited);
			FFuncover(_x - 1, _y - 1, visited);
		}
	}

	  //-----------------------------------------------------------------\\
     //--------------------End Flood Fill Algorithm-----------------------\\
	//---------------------------------------------------------------------\\

	public static bool isFinished(){
		foreach (Element elem in elements){
			if (elem.isCovered() && !elem._isMine){
				return false;
			}
		}
		return true;
	}
}
