using UnityEngine;
using System.Collections;

public class WallBoundary : MonoBehaviour {

	public enum ForceDirection
	{
		down, right, left, up
	}
	public ForceDirection forceDirection;

	public float _fBoundaryForce = 1;

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.tag == "Item" || other.tag == "Sacrifice") 
		{
			if (forceDirection == ForceDirection.up) 
			{
				other.GetComponent<Rigidbody2D> ().AddForce(Vector2.up * _fBoundaryForce, ForceMode2D.Impulse);
			}
			else if (forceDirection == ForceDirection.right) 
			{
				other.GetComponent<Rigidbody2D> ().AddForce(Vector2.right * _fBoundaryForce, ForceMode2D.Impulse);
			}
			else if (forceDirection == ForceDirection.down) 
			{
				other.GetComponent<Rigidbody2D> ().AddForce(Vector2.down * _fBoundaryForce, ForceMode2D.Impulse);
			}
			else if (forceDirection == ForceDirection.left) 
			{
				other.GetComponent<Rigidbody2D> ().AddForce(Vector2.left * _fBoundaryForce, ForceMode2D.Impulse);
			}
		}
	}
}
