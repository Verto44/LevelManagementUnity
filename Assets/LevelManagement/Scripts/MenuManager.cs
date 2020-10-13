using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private MainMenu mainMenuPrefab;               //Menu Principal
        [SerializeField] private SettingsMenu settingsMenuPrefab;       //Menu de Configurações
        [SerializeField] private CreditsScreen creditsScreenPrefab;     //Cena de Créditos
        [SerializeField] private GameMenu gameMenuPrefab;               //Menu do jogo
        [SerializeField] private PauseMenu pauseMenuPrefab;             //Menu de pause
        [SerializeField] private WinScreen winScreenPrefab;             //Tela de fase comcluída
        [SerializeField] private LevelSelectMenu levelSelectMenuPrefab;  //Tela de seleção de fase

        [SerializeField]
        private Transform _menuParent;

        private Stack<Menu> _menuStack = new Stack<Menu>();

        private static MenuManager _instance;

        public static MenuManager Instance { get => _instance;}

        public void Awake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                InitializeMenus();

                //Salva o MenuManager para a próxima cena
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if(_instance == this)
            {
                _instance = null;
            }
        }

        private void InitializeMenus()
        {
            if(_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }

            //Persiste todas as instâncias de menus criadas para a proxima cena
            DontDestroyOnLoad(_menuParent);

            //Menu[] menuPrefabs = { mainMenuPrefab, settingsMenuPrefab, creditsScreenPrefab, gameMenuPrefab, pauseMenuPrefab, 
            //winScreenPrefab};

            //System.Type myType = this.GetType();

            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly;
            FieldInfo[] fields = this.GetType().GetFields(myFlags);

            foreach(FieldInfo field in fields)
            {
                Menu prefab = field.GetValue(this) as Menu;

                if(prefab != null)
                {
                    Menu menuInstance = Instantiate(prefab, _menuParent);

                    if(prefab != mainMenuPrefab)
                    {
                        menuInstance.gameObject.SetActive(false);
                    }
                    else
                    {
                        OpenMenu(menuInstance);
                    }
                }
            }
        }

        public void OpenMenu(Menu menuInstance)
        {
            if(menuInstance == null)
            {
                Debug.LogWarning("MENUMANAGER OpenMenu ERROR: invalid menu");
                return;
            }

            if(_menuStack.Count > 0)
            {
                foreach(Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }

            menuInstance.gameObject.SetActive(true);
            
            //add a item on top of stack
            _menuStack.Push(menuInstance);
        }

        public void CloseMenu()
        {
            if(_menuStack.Count == 0)
            {
                Debug.LogWarning("MENUMANAGER CloseMenu ERROR: No menus in stack!");
                return;
            }

            //Return top item on the stack and remove
            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if(_menuStack.Count > 0)
            {
                //Return top item on the stack whithout remove
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
    } 
}
