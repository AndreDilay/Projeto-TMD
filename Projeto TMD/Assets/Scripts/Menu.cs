using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Menu: MonoBehaviour {

	[SerializeField] Button botaoJogar,botaoOpcoes,botaoSair;
	[Space(20)]
	[SerializeField] Slider barraVolume;
	[SerializeField] Toggle caixaModoJanela;
	[SerializeField] Dropdown resolucoes, qualidades;
	[SerializeField] Button botaoVoltar, botaoSalvarPref;
	[Space(20)]
    [SerializeField] Text textoVol;
    [SerializeField] string nomeCenaJogo = "CENA1";
	private string nomeDaCena;
	private float volume;
	private int qualidadeGrafica, modoJanelaAtivo, resolucaoSalveIndex;
	private bool telaCheiaAtivada;
	private Resolution[] resolucoesSuportadas;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
		resolucoesSuportadas = Screen.resolutions;
	}

	void Start () {
		Opcoes (false);
		ChecarResolucoes ();
		AjustarQualidades ();
		//
		if (PlayerPrefs.HasKey ("RESOLUCAO")) {
			int numResoluc = PlayerPrefs.GetInt ("RESOLUCAO");
			if (resolucoesSuportadas.Length <= numResoluc) {
				PlayerPrefs.DeleteKey ("RESOLUCAO");
			}
		}
		//
		nomeDaCena = SceneManager.GetActiveScene ().name;
		Cursor.visible = true;
		Time.timeScale = 1;
		//
		barraVolume.minValue = 0;
		barraVolume.maxValue = 1;

		//=============== SAVES===========//
		if (PlayerPrefs.HasKey ("VOLUME")) {
			volume = PlayerPrefs.GetFloat ("VOLUME");
			barraVolume.value = volume;
		} else {
			PlayerPrefs.SetFloat ("VOLUME", 1);
			barraVolume.value = 1;
		}
		//=============MODO JANELA===========//
		if (PlayerPrefs.HasKey ("modoJanela")) {
			modoJanelaAtivo = PlayerPrefs.GetInt ("modoJanela");
			if (modoJanelaAtivo == 1) {
				Screen.fullScreen = false;
				caixaModoJanela.isOn = true;
			} else {
				Screen.fullScreen = true;
				caixaModoJanela.isOn = false;
			}
		} else {
			modoJanelaAtivo = 0;
			PlayerPrefs.SetInt ("modoJanela", modoJanelaAtivo);
			caixaModoJanela.isOn = false;
			Screen.fullScreen = true;
		}
		//========RESOLUCOES========//
		if (modoJanelaAtivo == 1) {
			telaCheiaAtivada = false;
		} else {
			telaCheiaAtivada = true;
		}
		if (PlayerPrefs.HasKey ("RESOLUCAO")) {
			resolucaoSalveIndex = PlayerPrefs.GetInt ("RESOLUCAO");
			Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width,resolucoesSuportadas[resolucaoSalveIndex].height,telaCheiaAtivada);
			resolucoes.value = resolucaoSalveIndex;
		} else {
			resolucaoSalveIndex = (resolucoesSuportadas.Length -1);
			Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width,resolucoesSuportadas[resolucaoSalveIndex].height,telaCheiaAtivada);
			PlayerPrefs.SetInt ("RESOLUCAO", resolucaoSalveIndex);
			resolucoes.value = resolucaoSalveIndex;
		}
		//=========QUALIDADES=========//
		if (PlayerPrefs.HasKey ("qualidadeGrafica")) {
			qualidadeGrafica = PlayerPrefs.GetInt ("qualidadeGrafica");
			QualitySettings.SetQualityLevel(qualidadeGrafica);
			qualidades.value = qualidadeGrafica;
		} else {
			QualitySettings.SetQualityLevel((QualitySettings.names.Length-1));
			qualidadeGrafica = (QualitySettings.names.Length-1);
			PlayerPrefs.SetInt ("qualidadeGrafica", qualidadeGrafica);
			qualidades.value = qualidadeGrafica;
		}

		// =========SETAR BOTOES==========//
		botaoJogar.onClick = new Button.ButtonClickedEvent();
		botaoOpcoes.onClick = new Button.ButtonClickedEvent();
		botaoSair.onClick = new Button.ButtonClickedEvent();
		botaoVoltar.onClick = new Button.ButtonClickedEvent();
		botaoSalvarPref.onClick = new Button.ButtonClickedEvent();
		botaoJogar.onClick.AddListener(() => Jogar());
		botaoOpcoes.onClick.AddListener(() => Opcoes(true));
		botaoSair.onClick.AddListener(() => Sair());
		botaoVoltar.onClick.AddListener(() => Opcoes(false));
		botaoSalvarPref.onClick.AddListener(() => SalvarPreferencias());
	}
	//=========VOIDS DE CHECAGEM==========//
	private void ChecarResolucoes(){
		Resolution[] resolucoesSuportadas = Screen.resolutions;
		resolucoes.options.Clear ();
		for(int y = 0; y < resolucoesSuportadas.Length; y++){
			resolucoes.options.Add(new Dropdown.OptionData() { text = resolucoesSuportadas[y].width + "x" + resolucoesSuportadas[y].height });
		}
		resolucoes.captionText.text = "Resolucao";
	}
	private void AjustarQualidades(){
		string[] nomes = QualitySettings.names;
		qualidades.options.Clear ();
		for(int y = 0; y < nomes.Length; y++){
			qualidades.options.Add(new Dropdown.OptionData() { text = nomes[y] });
		}
		qualidades.captionText.text = "Qualidade";
	}
	private void Opcoes(bool ativarOP){
		botaoJogar.gameObject.SetActive (!ativarOP);
		botaoOpcoes.gameObject.SetActive (!ativarOP);
		botaoSair.gameObject.SetActive (!ativarOP);
		//
		textoVol.gameObject.SetActive (ativarOP);
		barraVolume.gameObject.SetActive (ativarOP);
		caixaModoJanela.gameObject.SetActive (ativarOP);
		resolucoes.gameObject.SetActive (ativarOP);
		qualidades.gameObject.SetActive (ativarOP);
		botaoVoltar.gameObject.SetActive (ativarOP);
		botaoSalvarPref.gameObject.SetActive (ativarOP);
	}
	//=========VOIDS DE SALVAMENTO==========//
	private void SalvarPreferencias(){
		if (caixaModoJanela.isOn == true) {
			modoJanelaAtivo = 1;
			telaCheiaAtivada = false;
		} else {
			modoJanelaAtivo = 0;
			telaCheiaAtivada = true;
		}
		PlayerPrefs.SetFloat ("VOLUME", barraVolume.value);
		PlayerPrefs.SetInt ("qualidadeGrafica", qualidades.value);
		PlayerPrefs.SetInt ("modoJanela", modoJanelaAtivo);
		PlayerPrefs.SetInt ("RESOLUCAO", resolucoes.value);
		resolucaoSalveIndex = resolucoes.value;
		AplicarPreferencias ();
	}
	private void AplicarPreferencias(){
		volume = PlayerPrefs.GetFloat ("VOLUME");
		QualitySettings.SetQualityLevel(PlayerPrefs.GetInt ("qualidadeGrafica"));
		Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width,resolucoesSuportadas[resolucaoSalveIndex].height,telaCheiaAtivada);
	}
	//===========VOIDS NORMAIS=========//
	void Update(){
		if (SceneManager.GetActiveScene ().name != nomeDaCena) {
			AudioListener.volume = volume;
			Destroy (gameObject);
		}
	}
	private void Jogar(){
		SceneManager.LoadScene (nomeCenaJogo);
	}
	private void Sair(){
		Application.Quit ();
	}
}