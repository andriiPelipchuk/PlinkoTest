using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Spawner spawner;
    public GameObject play, restart, portal;
    public GameObject heal, explosion, textGameObject;
    public GameObject losePopUp;
    public int healNumber = 1, explosionNumber = 1;

    public int numberSphere;
    public TextMeshProUGUI numberSphereText;


    private Player player;

    private bool _pressAbility, _gameIsPlaying;

    private Color healColor, explosionColor;

    private void Start()
    {
        explosionColor = explosion.GetComponent<Image>().color;
        healColor = heal.GetComponent<Image>().color;
    }

    private void Update()
    {
        if (numberSphere > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit)
                {
                    if (hit.collider.gameObject == play)
                    {

                        PlayGame();
                    }
                    if (_pressAbility )
                    {
                        if (hit.collider.gameObject == portal || hit.collider.gameObject == spawner)
                            return;
                        else
                        {
                            Destroy(hit.collider.gameObject);
                            _pressAbility = false;
                            PlayGame();
                        }
                    }
                }

            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit)
                {
                    if (hit.collider.gameObject == restart)
                    {
                        Restart();
                    }
                    
                }

            }
        }

    }

    private void OnPlayerDied()
    {
        player.PlayerDied -= OnPlayerDied;
        player.PlayerWin -= WinGame;
        _gameIsPlaying = false;
        if (numberSphere == 0)
        {
            GameOver();
            return;
        }
        if(healNumber > 0)
        {
            play.SetActive(true);
            ChangeTransparency(healColor, 100);
            textGameObject.SetActive(true);
        }
        else if (explosionNumber > 0)
        {
            play.SetActive(true);
            ChangeTransparency(explosionColor, 100);
            textGameObject.SetActive(true);
        }
        else
            PlayGame();
    }

    private void PlayGame()
    {
        _gameIsPlaying = true;
         
        spawner.gameObject.SetActive(true);
        portal.SetActive(true);

        textGameObject.SetActive(false);
        play.SetActive(false);

        ChangeTransparency(healColor, 100);
        ChangeTransparency(explosionColor, 100);

        CallSpawner();
    }
    private void CallSpawner()
    {
        numberSphere--;
        numberSphereText.text = numberSphere.ToString();
        player = spawner.SpawnPlayer();
        player.PlayerDied += OnPlayerDied;
        player.PlayerWin += WinGame;
    }
    private void WinGame()
    {
        player.PlayerWin -= WinGame;
        player.PlayerDied -= OnPlayerDied;
    }
    private void GameOver()
    {
        restart.SetActive(true);
        losePopUp.SetActive(true);
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UseHeal()
    {
        if(healNumber > 0 && !_gameIsPlaying)
        {

            healNumber--;
            numberSphere++;
            numberSphereText.text = numberSphere.ToString();
            PlayGame();

        }
        else
        {
            ReportAbsenceAbility();
        }
    }
    
    public void UseExplosion() 
    {
        if(explosionNumber > 0 && !_gameIsPlaying)
        {
            explosionNumber--;
            play.SetActive(false);
            _pressAbility = true;
        }
        else
        {
            ReportAbsenceAbility();
        }
    }

    public void ReportAbsenceAbility()
    {
        var text = textGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        text.text = "You don't have this type of ability left";
    }

    private void ChangeTransparency(Color image, int value)
    {
        image.a = value;
    }
}
