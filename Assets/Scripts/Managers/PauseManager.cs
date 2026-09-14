using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    public static PauseManager instance;

    [Header("UI Pause")]
    public GameObject pausePanel;
    public Button btnPause;
    public Button btnResumir;
    public Button btnMenuPrincipal; // ← novo

    private bool pausado = false;

    void Awake() { instance = this; }

    void Start()
    {
        if (btnMenuPrincipal != null)
            btnMenuPrincipal.onClick.AddListener(VoltarMenuPrincipal);

        pausePanel.SetActive(false);
        btnPause.onClick.AddListener(TogglePause);
        btnResumir.onClick.AddListener(Resumir);
    }

    void Update()
    {
        // ESC no PC também pausa
        if (Input.GetKeyDown(KeyCode.Escape) &&
            GameManager.instance != null &&
            GameManager.instance.JogoRodando())
        {
            TogglePause();
        }
    }

    void VoltarMenuPrincipal()
    {
        pausado = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);

        // Destroi inimigos na tela
        var inimigos = GameObject.FindObjectsByType<Enemy>(FindObjectsInactive.Exclude);
        foreach (var e in inimigos) Destroy(e.gameObject);

        // Volta ao menu
        if (GameManager.instance != null)
            GameManager.instance.VoltarAoMenu();
    }

    public void TogglePause()
    {
        if (!GameManager.instance.JogoRodando() && !pausado) return;
        if (pausado) Resumir();
        else Pausar();
    }

    void Pausar()
    {
        pausado = true;
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
    }

    void Resumir()
    {
        pausado = false;
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
    }

    public bool EstaPausado() => pausado;
}