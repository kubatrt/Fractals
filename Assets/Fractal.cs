using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	public Mesh		mesh;
	public Material material;
	public float 	maxDepth	= 4;
	[Range(0,1)] 
	public float 	childScale;

	private int depth;

	private static int	objectCounter; 

	private void Start () {
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = material;
		if(depth < maxDepth) {
			StartCoroutine("CreateChild");
		}
		//Debug.Log ("# Object counter: " + (++objectCounter));
	}

	private void Initialize(Fractal parent, Vector3 direction) {
		mesh = parent.mesh;
		material = parent.material;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		transform.parent = parent.transform;
		childScale = parent.childScale;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = direction * (0.5f + 0.5f * childScale);
	}

	private IEnumerator CreateChild()
	{
		yield return new WaitForSeconds(0.2f);
		new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.up);
		yield return new WaitForSeconds(0.2f);
		new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.right);
		yield return new WaitForSeconds(0.2f);
		new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.left);
		yield return new WaitForSeconds(0.2f);
		new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.down);
		yield return new WaitForSeconds(0.2f);
		new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.forward);
		yield return new WaitForSeconds(0.2f);
		new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.back);

	}
}
