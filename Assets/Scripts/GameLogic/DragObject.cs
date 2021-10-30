using System.Collections;
using UnityEngine;
using UnityEngine.UI;

enum TypeObj
{
	INTERACTABLE,
	NPC
}

class DragObject : MonoBehaviour	
{
	
	[SerializeField] private Material materialOverColor; //Cor para qual o objeto muda quando movimentando.
	[SerializeField] private Material originalMaterial; //Salva sua cor original

	[SerializeField] private bool dragging; //Booleana pra testar se pode movimentar o objeto
	[SerializeField] private float distance; //

	//Propriedades para o Drag
	Vector3 dist;
	float posX;
	float posY;
	Rigidbody rb;

	[SerializeField] private Vector3 screenPos; //Posição do obj na tela.

	private void Start()
	{
		rb = this.gameObject.GetComponent<Rigidbody>();
		originalMaterial = this.gameObject.GetComponent<Renderer>().material;
		dragging = true;
		//pos = Camera.main.WorldToViewportPoint(gameObject.transform.position);
	}
	private void FixedUpdate()
	{
		
	}

#region Inscrição e trancamento nos eventos
	void OnEnable()
	{
		GameManager.OnFall += StopOnFall;
		GameManager.OnRepeat += DoOnRepeat;
	}

	void OnDisable()
	{
		GameManager.OnFall -= StopOnFall;
		GameManager.OnRepeat -= DoOnRepeat;
	}
#endregion
	void StopOnFall()
	{
		dragging = false;
	}

	void DoOnRepeat()
	{
		dragging = true;
	}

	void OnMouseEnter()
	{
		originalMaterial.shader = Shader.Find("Standard");
		originalMaterial.SetFloat("_Metallic", 0.8f);
	}

	void OnMouseExit()
	{
		originalMaterial.shader = Shader.Find("Legacy Shaders/Diffuse Detail");
		
	}

	private void OnMouseUp()
	{
		if(dragging)
		rb.velocity = Vector3.zero;
	}

	private void OnMouseDrag()
	{
		if (dragging)
		{
			Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, dist.z);
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
			//transform.position = worldPos;
			rb.velocity = (worldPos - this.transform.position) * 10;
		}		
	}
	public void OnMouseDown()
	{
		dist = Camera.main.WorldToScreenPoint(transform.position);
		posX = Input.mousePosition.x - dist.x;
		posY = Input.mousePosition.y - dist.y;
	}

	void Update()
	{
		//screenPos = Camera.main.WorldToScreenPoint(transform.position);

		//if (screenPos.x < 0)
		//{
		//	this.transform.position = new Vector3(0, transform.position.y, transform.position.z);
		//}

		//else if (screenPos.x > Screen.width)
		//	this.transform.position = new Vector3(Screen.width, transform.position.y, transform.position.z);
	}
}


