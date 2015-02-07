using UnityEngine;
using System.Collections;

public class Fractal : MonoBehaviour {

	public Mesh[]	meshes;
	public Material material;
	public int 	maxDepth = 4;
	public float maxTwist;
	[Range(0,1)] public float childScale;
	[Range(0,1)] public float spawnProbability;

	private int depth;

	private static Vector3[] childDirections = { 
		Vector3.up, 
		Vector3.right, 
		Vector3.left,
		Vector3.forward,
		Vector3.back
		//Vector3.down
	};
	private static Quaternion[] childOrientations = { 
		Quaternion.identity, 
		Quaternion.Euler(0f,0f,-90f),
		Quaternion.Euler(0f,0f, 90f),
		Quaternion.Euler(90f,0f, 0f),
		Quaternion.Euler(-90f,0f, 0f)
		//Quaternion.Euler(0f, 90f, 0f)
	};
	private Material[,] materials;

	private void InitializeMaterials()
	{
		materials = new Material[maxDepth + 1, 3];
		for(int i=0; i <= maxDepth; ++i) {
			float t = i / (maxDepth - 1f);
			t *= t;
			materials[i, 0] = new Material(material);
			materials[i, 0].color = Color.Lerp(Color.white, Color.yellow, t);
			materials[i, 1] = new Material(material);
			materials[i, 1].color = Color.Lerp(Color.white, Color.cyan, t);
			materials[i, 2] = new Material(material);
			materials[i, 2].color = Color.Lerp(Color.white, Color.green, t);
		}
		materials[maxDepth, 0].color = Color.magenta;
		materials[maxDepth, 1].color = Color.red;
		materials[maxDepth, 2].color = Color.blue;
	}

	private void Update()
	{
		transform.Rotate(0, 30f * Time.deltaTime, 0f);
	}

	private void Start () 
	{
		transform.Rotate(Random.Range(-maxTwist, maxTwist), 0f, 0f);
		if(materials == null) {
			InitializeMaterials();
		}
		gameObject.AddComponent<MeshFilter>().mesh = meshes[Random.Range (0, meshes.Length)];
		gameObject.AddComponent<MeshRenderer>().material = materials[depth, Random.Range(0, 3)];

		if(depth < maxDepth) {
			StartCoroutine("CreateChild");
		}
	}

	private void Initialize(Fractal parent, int childIndex) 
	{
		meshes = parent.meshes;
		materials = parent.materials;
		maxDepth = parent.maxDepth;
		depth = parent.depth + 1;
		maxTwist = parent.maxTwist;
		spawnProbability = parent.spawnProbability;
		transform.parent = parent.transform;
		childScale = parent.childScale;
		transform.localScale = Vector3.one * childScale;
		transform.localPosition = childDirections[childIndex] * (0.5f + 0.5f * childScale);
		transform.localRotation = childOrientations[childIndex];
	}

	private IEnumerator CreateChild()
	{
		for(int i=0; i < childDirections.Length; ++i) {
			if(Random.value < spawnProbability) {
				yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
				new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, i);
			}
		}

		//yield return new WaitForSeconds(0.2f);
		//new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.down);
		//yield return new WaitForSeconds(0.2f);
		//new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.forward);
		//yield return new WaitForSeconds(0.2f);
		//new GameObject("Fractal child").AddComponent<Fractal>().Initialize(this, Vector3.back);

	}
}
