using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace CustomIngameMenu
{
    public class Loader
    {
        private static GameObject _obj;

        public static void Init()
        {
            Load();
        }

        public static void Load()
        {
            GameObject existing = GameObject.Find("CheatMenu_RuntimeHost");
            if (existing != null)
            {
                _obj = existing;
                CheatMenu menu = _obj.GetComponent<CheatMenu>();
                if (menu != null) menu.isOpen = true;
                return;
            }

            if (_obj != null)
            {
                CheatMenu menu = _obj.GetComponent<CheatMenu>();
                if (menu != null) menu.isOpen = true;
                return;
            }

            _obj = new GameObject("CheatMenu_RuntimeHost");
            _obj.AddComponent<CheatMenu>();
            UnityEngine.Object.DontDestroyOnLoad(_obj);
        }

        public static void Unload()
        {
            GameObject existing = GameObject.Find("CheatMenu_RuntimeHost");
            if (existing != null)
            {
                UnityEngine.Object.Destroy(existing);
            }
            if (_obj != null)
            {
                UnityEngine.Object.Destroy(_obj);
                _obj = null;
            }
        }
    }

    public class CheatMenu : MonoBehaviour
    {
        public bool isOpen = true;
        public int langIndex = 0;

        private Rect winRect = new Rect(100f, 100f, 640f, 580f);
        private int currentTab = 0;

        public bool untouchable = false;
        public bool noDamage = false;
        public bool unlimitedEnergy = false;
        public bool invisibility = false;
        public bool superGrab = false;
        public float grabForceMultiplier = 1.0f;
        public bool brightMap = false;
        private Light fullbrightLight = null;

        public bool infiniteJumps = false;
        public float walkSpeedMultiplier = 1.0f;
        public float sprintSpeedMultiplier = 1.0f;
        public float crouchSpeedMultiplier = 1.0f;

        public bool infiniteMoney = false;
        public bool unbreakableItems = false;
        private bool prevUnbreakableItems = false;
        public bool infiniteItemBattery = true;
        public bool infiniteStationCharge = true;
        public bool autoChargeHeldItem = true;

        public bool unlockShopItems = false;
        private bool prevUnlockShopItems = false;
        public bool freeShop = false;
        public bool freeExtraction = false;

        public bool espValuables = false;
        public bool espEnemies = true;
        public bool espExtraction = true;
        public bool espExtractionOnlyLocked = true;
        public bool espExtractionOnlyIncomplete = true;
        public bool espShowDistance = true;
        public bool espShowPrice = true;
        public bool espShowHealthBar = true;
        public bool espShowEnemyName = true;
        public bool espBoxValuables = true;
        public bool espBoxEnemies = true;
        public bool espGoldBadgeHighValue = true;
        public bool espOnlyGoldItems = false;
        public float espValuablesMinPrice = 0f;
        public float espMaxDistance = 150f;

        private static Type roundDirectorType;
        private static FieldInfo roundDirectorInstFld;
        private static FieldInfo rdCurrentHaulFld;
        private static FieldInfo rdHaulGoalFld;

        private static Type extractionPointType;
        private static FieldInfo epHaulGoalFld;
        private static FieldInfo epIsLockedFld;
        private static MethodInfo epUpdateLockMethod;
        private static FieldInfo epShopStationFld;
        private static FieldInfo epIsShopFld;
        private static FieldInfo epCurrentStateFld;
        private static FieldInfo epExtractionSurplusCompletedFld;
        private static FieldInfo epIsCompletedRightAwayFld;

        private static Type shopManagerType;
        private static FieldInfo shopManagerInstFld;
        private static FieldInfo smTotalCostFld;
        private static FieldInfo smItemMultiplierFld;

        private static Type itemDeactivatedUntilLevelType;
        private static FieldInfo levelToActivateFld;

        private static Type impactDetectorType;
        private static FieldInfo idIsIndestructibleFld;
        private static FieldInfo idDestroyDisableFld;
        private static FieldInfo idFragilityFld;
        private static FieldInfo idDurabilityFld;

        private static Type semiFuncType;
        private static MethodInfo sfStatSetRunCurrency;

        private static Type cameraAimType;
        private static PropertyInfo cameraAimInstProp;
        private static FieldInfo cameraAimInstFld;
        private static FieldInfo aimOverrideAimStopFld;
        private static FieldInfo aimOverrideAimStopTimerFld;

        private static Type inputManagerType;
        private static FieldInfo inputManagerInstFld;
        private static FieldInfo imDisableControlsExceptTimerFld;
        private static FieldInfo imDisableAimingTimerFld;

        private static Type cursorType;
        private static PropertyInfo cursorInst;
        private static PropertyInfo cursorUsersList;
        private static Type controllerType;
        private static PropertyInfo controllerInst;
        private static FieldInfo controllerBlocker;
        private static Type kolkataPlayerType;
        private static PropertyInfo kolkataPlayerLocal;
        private static Type playerInteractorType;
        private static PropertyInfo playerInteractorLocal;
        private static Type photonPlayerControllerType;
        private static FieldInfo pcOriginalMoveSpeedFld;
        private static FieldInfo pcOriginalSprintSpeedFld;
        private static FieldInfo pcOriginalCrouchSpeedFld;
        private static Type metaManagerType;
        private static FieldInfo metaManagerInstanceFld;
        private static Type valuableDirectorType;
        private static FieldInfo valuableDirectorInstanceFld;
        private static FieldInfo valuableListFld;
        private static FieldInfo dollarValueCurrentFld;
        private static Type renderTextureMainType;
        private static FieldInfo renderTextureMainInstanceFld;
        private static FieldInfo renderTextureWidthFld;
        private static FieldInfo renderTextureHeightFld;
        private static Type cameraUtilsType;
        private static FieldInfo cameraUtilsInstanceFld;
        private static FieldInfo cameraUtilsMainCameraFld;
        private static Type enemyDirectorType;
        private static FieldInfo enemyDirectorInstanceFld;
        private static FieldInfo enemiesSpawnedFld;
        private static Type physGrabObjectType;
        private static FieldInfo physMidPointFld;
        private static FieldInfo physBoundingBoxFld;
        private static Type playerAvatarType;
        private static FieldInfo playerAvatarInstFld;
        private static FieldInfo playerHealthFld;
        private static FieldInfo physGrabberFld;
        private static FieldInfo deadSetFld;
        private static FieldInfo playerTumbleFld;
        private static Type playerHealthType;
        private static FieldInfo healthFld;
        private static FieldInfo maxHealthFld;
        private static FieldInfo healthGodModeFld;
        private static FieldInfo invincibleTimerFld;
        private static Type physGrabberType;
        private static FieldInfo grabStrengthFld;
        private static FieldInfo throwStrengthFld;
        private static FieldInfo grabRangeFld;
        private static FieldInfo grabReleaseDistFld;
        private static FieldInfo forceMaxFld;
        private static FieldInfo overrideGrabStrengthFld;
        private static FieldInfo overrideGrabStrengthTimerFld;
        private static FieldInfo overrideAlwaysGrabbableTimerFld;
        private static FieldInfo grabDisableTimerFld;
        private static Type chargingStationType;
        private static FieldInfo chargingStationInstFld;
        private static FieldInfo chargeTotalFld;
        private static FieldInfo chargeFloatFld;
        private static FieldInfo chargeIntFld;
        private static FieldInfo chargeSegmentCurrentFld;
        private static FieldInfo chargeScaleTargetFld;
        private static Type itemBatteryType;
        private static FieldInfo batteryLifeFld;
        private static FieldInfo batteryLifeIntFld;
        private static FieldInfo batteryBarsFld;
        private static FieldInfo drainRateFld;

        // Performance & Optimization Caches
        private static bool reflectionScanned = false;
        private static float reflectionScanTimer = 0f;
        private static int reflectionScanCount = 0;
        private static Camera _cachedCam = null;
        private static float _camCacheTimer = 0f;
        private static Component cachedLocalAvatar = null;
        private static float localAvatarCacheTimer = 0f;
        private static readonly Dictionary<string, string> itemNameCache = new Dictionary<string, string>();
        private static readonly List<Component> cachedExtractionPoints = new List<Component>();
        private static float batteryScanTimer = 0f;
        private static readonly GUIStyle emptyWindow = new GUIStyle();
        private static readonly string[] TabIcons = new string[] { "👤 ", "⚡ ", "👁 ", "🎒 ", "🎭 ", "🛒 ", "⚙ " };
        private static float hackTickTimer = 0f;
        private static float slowScanTimer = 0f;
        private static float cachedBaseMove = 0f;
        private static float cachedBaseSprint = 0f;
        private static float cachedBaseCrouch = 0f;
        private static Type statsMgrType;
        private static FieldInfo statsMgrInstFld;
        private static FieldInfo statsMgrRunStatsFld;
        private static Type currencyUiType;
        private static FieldInfo currencyUiInstFld;
        private static MethodInfo currencyUiFetchMethod;
        private static readonly object[] moneyArgs = new object[1];
        private static int lastAppliedMoney = -1;

        // Pre-cached Reflection Handles for Zero-Allocation Loops
        private static FieldInfo photonPcInstFld;
        private static FieldInfo photonPcMoveSpeedFld;
        private static FieldInfo photonPcSprintSpeedFld;
        private static FieldInfo photonPcCrouchSpeedFld;
        private static FieldInfo photonPcEnergyCurFld;
        private static FieldInfo photonPcEnergyStartFld;
        private static FieldInfo photonPcDebugEnergyFld;
        private static FieldInfo photonPcJumpCdFld;
        private static FieldInfo photonPcJumpBufferFld;
        private static FieldInfo photonPcJumpExtraFld;
        private static FieldInfo photonPcAvatarScriptFld;

        private static Type enemyType;
        private static FieldInfo enemyNameFld;
        private static FieldInfo enemyHealthCompFld;
        private static FieldInfo healthCurrentFld;
        private static FieldInfo healthMaxFld;
        private static FieldInfo epEnemyFld;
        private static FieldInfo epSpawnedFld;
        private static FieldInfo enCenterTransformFld;
        private static FieldInfo epPlatformFld;

        private static FieldInfo enemyDirectorDebugNoVisionFld;
        private static FieldInfo playerAvatarSteamIDFld;
        private static FieldInfo roundDirectorEpCurFld;
        private static FieldInfo roundDirectorEpListFld;

        private Texture2D texBackground;
        private Texture2D texSidebar;
        private Texture2D texHeader;
        private Texture2D texCard;
        private Texture2D texCardBorder;
        private Texture2D texAccent;
        private Texture2D texAccentCyan;
        private Texture2D texToggleOn;
        private Texture2D texToggleOff;
        private Texture2D texBtnNormal;
        private Texture2D texBtnHover;
        private Texture2D texWhite;
        private Texture2D texDarkBadge;

        private GUIStyle styleTitle;
        private GUIStyle styleSubtitle;
        private GUIStyle styleTabActive;
        private GUIStyle styleTabInactive;
        private GUIStyle styleHeader;
        private GUIStyle styleLabelText;
        private GUIStyle styleToggleOn;
        private GUIStyle styleToggleOff;
        private GUIStyle styleStepperBtn;
        private GUIStyle styleStepperVal;
        private GUIStyle styleActionBtn;
        private GUIStyle styleCloseBtn;
        private GUIStyle espLabelNormal;
        private GUIStyle espLabelGold;
        private GUIStyle espEnemyStyle;
        private GUIStyle espHpStyle;
        private GUIStyle espExtractionStyle;

        private bool texturesInitialized = false;
        private Vector2 contentScroll = Vector2.zero;


        private static readonly string[][] Tabs = new string[][]
        {
            new string[] { "Jogador", "Física", "Visuais (ESP)", "Equipamentos", "Cosméticos", "Sessão & Loja", "Sistema" },
            new string[] { "Player", "Physics", "Visuals (ESP)", "Loadout", "Cosmetics", "Lobby & Shop", "System" }
        };

        private string T(string pt, string en)
        {
            return langIndex == 0 ? pt : en;
        }

        private Texture2D MakeTex(int width, int height, Color col)
        {
            Color[] pix = new Color[width * height];
            for (int i = 0; i < pix.Length; i++) pix[i] = col;
            Texture2D res = new Texture2D(width, height);
            res.SetPixels(pix);
            res.Apply();
            return res;
        }

        private static string GetCleanItemName(string originalName)
        {
            if (string.IsNullOrEmpty(originalName)) return "Item";
            string clean;
            if (!itemNameCache.TryGetValue(originalName, out clean))
            {
                clean = originalName.Replace("(Clone)", "")
                                    .Replace("Valuable", "")
                                    .Replace("Wizard", "")
                                    .Replace("Arctic", "")
                                    .Replace("Surplus", "")
                                    .Trim();
                if (string.IsNullOrEmpty(clean)) clean = "Item";
                itemNameCache[originalName] = clean;
            }
            return clean;
        }

        private void Awake()
        {
            CheatMenu[] existingMenus = UnityEngine.Object.FindObjectsOfType<CheatMenu>();
            if (existingMenus != null && existingMenus.Length > 1)
            {
                for (int i = 0; i < existingMenus.Length; i++)
                {
                    if (existingMenus[i] != this && existingMenus[i] != null)
                    {
                        UnityEngine.Object.Destroy(existingMenus[i].gameObject);
                    }
                }
            }

            float w = 730f;
            float h = 530f;
            winRect = new Rect((Screen.width - w) / 2f, (Screen.height - h) / 2f, w, h);

            InitReflection();
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }

        private static void InitReflection()
        {
            if (reflectionScanned) return;

            reflectionScanTimer += Time.deltaTime;
            if (reflectionScanTimer < 2.0f && reflectionScanCount > 0) return;
            reflectionScanTimer = 0f;
            reflectionScanCount++;

            Assembly[] asms = AppDomain.CurrentDomain.GetAssemblies();
            Assembly targetAsm = null;
            for (int a = 0; a < asms.Length; a++)
            {
                if (asms[a].GetName().Name == "Assembly-CSharp")
                {
                    targetAsm = asms[a];
                    break;
                }
            }

            IEnumerable<Assembly> scanList = (targetAsm != null) ? new Assembly[] { targetAsm } : (IEnumerable<Assembly>)asms;

            foreach (Assembly asm in scanList)
            {
                if (cameraAimType == null)
                {
                    Type t = asm.GetType("CameraAim");
                    if (t != null)
                    {
                        cameraAimType = t;
                        cameraAimInstProp = t.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
                        cameraAimInstFld = t.GetField("Instance", BindingFlags.Public | BindingFlags.Static);
                        aimOverrideAimStopFld = t.GetField("overrideAimStop", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        aimOverrideAimStopTimerFld = t.GetField("overrideAimStopTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (inputManagerType == null)
                {
                    Type t = asm.GetType("InputManager");
                    if (t != null)
                    {
                        inputManagerType = t;
                        inputManagerInstFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        imDisableControlsExceptTimerFld = t.GetField("disableControlsExceptTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        imDisableAimingTimerFld = t.GetField("disableAimingTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (cursorType == null)
                {
                    cursorType = asm.GetType("Metater.MetaCursor");
                    if (cursorType != null)
                    {
                        cursorInst = cursorType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
                        cursorUsersList = cursorType.GetProperty("CursorUsers", BindingFlags.Public | BindingFlags.Instance);
                    }
                }
                if (controllerType == null)
                {
                    controllerType = asm.GetType("Metater.MetaPlayerController");
                    if (controllerType != null)
                    {
                        controllerInst = controllerType.GetProperty("Instance", BindingFlags.Public | BindingFlags.Static);
                        controllerBlocker = controllerType.GetField("blocker", BindingFlags.Public | BindingFlags.Instance);
                    }
                }
                if (kolkataPlayerType == null)
                {
                    kolkataPlayerType = asm.GetType("KolkataOsPlayer");
                    if (kolkataPlayerType != null)
                    {
                        kolkataPlayerLocal = kolkataPlayerType.GetProperty("LocalPlayerInstance", BindingFlags.Public | BindingFlags.Static);
                    }
                }
                if (playerInteractorType == null)
                {
                    playerInteractorType = asm.GetType("PlayerInteractor");
                    if (playerInteractorType != null)
                    {
                        playerInteractorLocal = playerInteractorType.GetProperty("LocalPlayerInstance", BindingFlags.Public | BindingFlags.Static);
                    }
                }
                if (photonPlayerControllerType == null)
                {
                    Type t = asm.GetType("PlayerController");
                    if (t != null)
                    {
                        photonPlayerControllerType = t;
                        pcOriginalMoveSpeedFld = t.GetField("playerOriginalMoveSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        pcOriginalSprintSpeedFld = t.GetField("playerOriginalSprintSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        pcOriginalCrouchSpeedFld = t.GetField("playerOriginalCrouchSpeed", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (metaManagerType == null)
                {
                    Type t = asm.GetType("MetaManager");
                    if (t != null)
                    {
                        metaManagerType = t;
                        metaManagerInstanceFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                    }
                }
                if (valuableDirectorType == null)
                {
                    Type t = asm.GetType("ValuableDirector");
                    if (t != null)
                    {
                        valuableDirectorType = t;
                        valuableDirectorInstanceFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        valuableListFld = t.GetField("valuableList", BindingFlags.Public | BindingFlags.Instance);
                    }
                }
                if (dollarValueCurrentFld == null)
                {
                    Type t = asm.GetType("ValuableObject");
                    if (t != null)
                    {
                        dollarValueCurrentFld = t.GetField("dollarValueCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (renderTextureMainType == null)
                {
                    Type t = asm.GetType("RenderTextureMain");
                    if (t != null)
                    {
                        renderTextureMainType = t;
                        renderTextureMainInstanceFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        renderTextureWidthFld = t.GetField("textureWidth", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        renderTextureHeightFld = t.GetField("textureHeight", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (cameraUtilsType == null)
                {
                    Type t = asm.GetType("CameraUtils");
                    if (t != null)
                    {
                        cameraUtilsType = t;
                        cameraUtilsInstanceFld = t.GetField("Instance", BindingFlags.Public | BindingFlags.Static);
                        cameraUtilsMainCameraFld = t.GetField("MainCamera", BindingFlags.Public | BindingFlags.Instance);
                    }
                }
                if (enemyDirectorType == null)
                {
                    Type t = asm.GetType("EnemyDirector");
                    if (t != null)
                    {
                        enemyDirectorType = t;
                        enemyDirectorInstanceFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        enemiesSpawnedFld = t.GetField("enemiesSpawned", BindingFlags.Public | BindingFlags.Instance);
                    }
                }
                if (physGrabObjectType == null)
                {
                    Type t = asm.GetType("PhysGrabObject");
                    if (t != null)
                    {
                        physGrabObjectType = t;
                        physMidPointFld = t.GetField("midPoint", BindingFlags.Public | BindingFlags.Instance);
                        physBoundingBoxFld = t.GetField("boundingBox", BindingFlags.Public | BindingFlags.Instance);
                    }
                }
                if (impactDetectorType == null)
                {
                    Type t = asm.GetType("PhysGrabObjectImpactDetector");
                    if (t != null)
                    {
                        impactDetectorType = t;
                        idIsIndestructibleFld = t.GetField("isIndestructible", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        idDestroyDisableFld = t.GetField("destroyDisable", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        idFragilityFld = t.GetField("fragility", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        idDurabilityFld = t.GetField("durability", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (playerAvatarType == null)
                {
                    Type t = asm.GetType("PlayerAvatar");
                    if (t != null)
                    {
                        playerAvatarType = t;
                        playerAvatarInstFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        playerHealthFld = t.GetField("playerHealth", BindingFlags.Public | BindingFlags.Instance);
                        physGrabberFld = t.GetField("physGrabber", BindingFlags.Public | BindingFlags.Instance);
                        deadSetFld = t.GetField("deadSet", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        playerTumbleFld = t.GetField("tumble", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (playerHealthType == null)
                {
                    Type t = asm.GetType("PlayerHealth");
                    if (t != null)
                    {
                        playerHealthType = t;
                        healthFld = t.GetField("health", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        maxHealthFld = t.GetField("maxHealth", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        healthGodModeFld = t.GetField("godMode", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        invincibleTimerFld = t.GetField("invincibleTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (physGrabberType == null)
                {
                    Type t = asm.GetType("PhysGrabber");
                    if (t != null)
                    {
                        physGrabberType = t;
                        grabStrengthFld = t.GetField("grabStrength", BindingFlags.Public | BindingFlags.Instance);
                        throwStrengthFld = t.GetField("throwStrength", BindingFlags.Public | BindingFlags.Instance);
                        grabRangeFld = t.GetField("grabRange", BindingFlags.Public | BindingFlags.Instance);
                        grabReleaseDistFld = t.GetField("grabReleaseDistance", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        forceMaxFld = t.GetField("forceMax", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        overrideGrabStrengthFld = t.GetField("overrideGrabStrength", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        overrideGrabStrengthTimerFld = t.GetField("overrideGrabStrengthTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        overrideAlwaysGrabbableTimerFld = t.GetField("overrideAlwaysGrabbableTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        grabDisableTimerFld = t.GetField("grabDisableTimer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (chargingStationType == null)
                {
                    Type t = asm.GetType("ChargingStation");
                    if (t != null)
                    {
                        chargingStationType = t;
                        chargingStationInstFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        chargeTotalFld = t.GetField("chargeTotal", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        chargeFloatFld = t.GetField("chargeFloat", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        chargeIntFld = t.GetField("chargeInt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        chargeSegmentCurrentFld = t.GetField("chargeSegmentCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        chargeScaleTargetFld = t.GetField("chargeScaleTarget", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (itemBatteryType == null)
                {
                    Type t = asm.GetType("ItemBattery");
                    if (t != null)
                    {
                        itemBatteryType = t;
                        batteryLifeFld = t.GetField("batteryLife", BindingFlags.Public | BindingFlags.Instance);
                        batteryLifeIntFld = t.GetField("batteryLifeInt", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        batteryBarsFld = t.GetField("batteryBars", BindingFlags.Public | BindingFlags.Instance);
                        drainRateFld = t.GetField("drainRate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (roundDirectorType == null)
                {
                    Type t = asm.GetType("RoundDirector");
                    if (t != null)
                    {
                        roundDirectorType = t;
                        roundDirectorInstFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        rdCurrentHaulFld = t.GetField("currentHaul", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        rdHaulGoalFld = t.GetField("haulGoal", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (extractionPointType == null)
                {
                    Type t = asm.GetType("ExtractionPoint");
                    if (t != null)
                    {
                        extractionPointType = t;
                        epHaulGoalFld = t.GetField("haulGoal", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        epIsLockedFld = t.GetField("isLocked", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        epUpdateLockMethod = t.GetMethod("UpdateLock", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        epShopStationFld = t.GetField("shopStation", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        epIsShopFld = t.GetField("isShop", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        epCurrentStateFld = t.GetField("currentState", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        epExtractionSurplusCompletedFld = t.GetField("extractionSurplusCompleted", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        epIsCompletedRightAwayFld = t.GetField("isCompletedRightAway", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (shopManagerType == null)
                {
                    Type t = asm.GetType("ShopManager");
                    if (t != null)
                    {
                        shopManagerType = t;
                        shopManagerInstFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        smTotalCostFld = t.GetField("totalCost", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        smItemMultiplierFld = t.GetField("itemValueMultiplier", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (itemDeactivatedUntilLevelType == null)
                {
                    Type t = asm.GetType("ItemDeactivatedUntilLevel");
                    if (t != null)
                    {
                        itemDeactivatedUntilLevelType = t;
                        levelToActivateFld = t.GetField("levelToActivate", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
                if (semiFuncType == null)
                {
                    Type t = asm.GetType("SemiFunc");
                    if (t != null)
                    {
                        semiFuncType = t;
                        sfStatSetRunCurrency = t.GetMethod("StatSetRunCurrency", BindingFlags.Public | BindingFlags.Static);
                    }
                }

                if (statsMgrType == null)
                {
                    Type t = asm.GetType("StatsManager");
                    if (t != null)
                    {
                        statsMgrType = t;
                        statsMgrInstFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        statsMgrRunStatsFld = t.GetField("runStats", BindingFlags.Public | BindingFlags.Instance);
                    }
                }

                if (currencyUiType == null)
                {
                    Type t = asm.GetType("CurrencyUI");
                    if (t != null)
                    {
                        currencyUiType = t;
                        currencyUiInstFld = t.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        currencyUiFetchMethod = t.GetMethod("FetchCurrency", BindingFlags.Public | BindingFlags.Instance);
                    }
                }

                if (enemyType == null)
                {
                    Type t = asm.GetType("Enemy");
                    if (t != null)
                    {
                        enemyType = t;
                        enemyNameFld = t.GetField("enemyName", BindingFlags.Public | BindingFlags.Instance);
                        enemyHealthCompFld = t.GetField("Health", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        enCenterTransformFld = t.GetField("CenterTransform", BindingFlags.Public | BindingFlags.Instance);
                    }
                }

                if (healthCurrentFld == null)
                {
                    Type ehType = asm.GetType("EnemyHealth");
                    if (ehType != null)
                    {
                        healthCurrentFld = ehType.GetField("healthCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        healthMaxFld = ehType.GetField("health", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }
                }
            }

            if (extractionPointType != null)
            {
                epPlatformFld = extractionPointType.GetField("platform", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            }

            if (photonPlayerControllerType != null)
            {
                photonPcInstFld = photonPlayerControllerType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                photonPcMoveSpeedFld = photonPlayerControllerType.GetField("MoveSpeed", BindingFlags.Public | BindingFlags.Instance);
                photonPcSprintSpeedFld = photonPlayerControllerType.GetField("SprintSpeed", BindingFlags.Public | BindingFlags.Instance);
                photonPcCrouchSpeedFld = photonPlayerControllerType.GetField("CrouchSpeed", BindingFlags.Public | BindingFlags.Instance);
                photonPcEnergyCurFld = photonPlayerControllerType.GetField("EnergyCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                photonPcEnergyStartFld = photonPlayerControllerType.GetField("EnergyStart", BindingFlags.Public | BindingFlags.Instance);
                photonPcDebugEnergyFld = photonPlayerControllerType.GetField("DebugEnergy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                photonPcJumpCdFld = photonPlayerControllerType.GetField("JumpCooldown", BindingFlags.NonPublic | BindingFlags.Instance);
                photonPcJumpBufferFld = photonPlayerControllerType.GetField("JumpGroundedBuffer", BindingFlags.NonPublic | BindingFlags.Instance);
                photonPcJumpExtraFld = photonPlayerControllerType.GetField("JumpExtraCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                photonPcAvatarScriptFld = photonPlayerControllerType.GetField("playerAvatarScript", BindingFlags.Public | BindingFlags.Instance);
            }

            if (enemyDirectorType != null)
            {
                enemyDirectorDebugNoVisionFld = enemyDirectorType.GetField("debugNoVision", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            }

            if (playerAvatarType != null)
            {
                playerAvatarSteamIDFld = playerAvatarType.GetField("steamID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            }

            if (roundDirectorType != null)
            {
                roundDirectorEpCurFld = roundDirectorType.GetField("extractionPointCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                roundDirectorEpListFld = roundDirectorType.GetField("extractionPointList", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            }

            if ((valuableDirectorType != null && extractionPointType != null && playerAvatarType != null) || reflectionScanCount >= 3)
            {
                reflectionScanned = true;
            }
        }

        private object GetLocalPlayerAvatar()
        {
            localAvatarCacheTimer += Time.deltaTime;
            if (cachedLocalAvatar != null && localAvatarCacheTimer < 0.5f)
            {
                return cachedLocalAvatar;
            }
            localAvatarCacheTimer = 0f;

            try
            {
                if (playerAvatarType != null && playerAvatarInstFld != null)
                {
                    Component av = playerAvatarInstFld.GetValue(null) as Component;
                    if (av != null)
                    {
                        cachedLocalAvatar = av;
                        return av;
                    }
                }
                if (photonPlayerControllerType != null && photonPcInstFld != null && photonPcAvatarScriptFld != null)
                {
                    object pc = photonPcInstFld.GetValue(null);
                    if (pc != null)
                    {
                        Component av = photonPcAvatarScriptFld.GetValue(pc) as Component;
                        if (av != null)
                        {
                            cachedLocalAvatar = av;
                            return av;
                        }
                    }
                }
            }
            catch { }
            return cachedLocalAvatar;
        }

        public void SetPlayerMoney(int amount)
        {
            if (infiniteMoney && amount == lastAppliedMoney) return;
            lastAppliedMoney = amount;

            try
            {
                InitReflection();
                if (sfStatSetRunCurrency != null)
                {
                    moneyArgs[0] = amount;
                    sfStatSetRunCurrency.Invoke(null, moneyArgs);
                }

                if (statsMgrType != null && statsMgrInstFld != null)
                {
                    object sm = statsMgrInstFld.GetValue(null);
                    if (sm != null && statsMgrRunStatsFld != null)
                    {
                        IDictionary dict = statsMgrRunStatsFld.GetValue(sm) as IDictionary;
                        if (dict != null)
                        {
                            dict["currency"] = amount;
                        }
                    }
                }

                if (currencyUiType != null && currencyUiInstFld != null)
                {
                    object cui = currencyUiInstFld.GetValue(null);
                    if (cui != null && currencyUiFetchMethod != null)
                    {
                        currencyUiFetchMethod.Invoke(cui, null);
                    }
                }
            }
            catch { }
        }

        public void HealLocalPlayer()
        {
            try
            {
                InitReflection();
                Component av = GetLocalPlayerAvatar() as Component;
                if (av != null)
                {
                    if (deadSetFld != null) deadSetFld.SetValue(av, false);
                    if (playerHealthFld != null)
                    {
                        object ph = playerHealthFld.GetValue(av);
                        if (ph != null)
                        {
                            if (healthFld != null) healthFld.SetValue(ph, 100);
                            if (maxHealthFld != null) maxHealthFld.SetValue(ph, 100);
                            if (healthGodModeFld != null) healthGodModeFld.SetValue(ph, true);
                            if (invincibleTimerFld != null) invincibleTimerFld.SetValue(ph, 10f);

                            MethodInfo healMethod = ph.GetType().GetMethod("Heal", new Type[] { typeof(int), typeof(bool) });
                            if (healMethod != null)
                            {
                                healMethod.Invoke(ph, new object[] { 100, true });
                            }
                            else
                            {
                                MethodInfo setGreen = ph.GetType().GetMethod("SetMaterialGreen", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                                if (setGreen != null) setGreen.Invoke(ph, null);
                            }
                        }
                    }
                }
            }
            catch { }
        }

        public void TeleportToSafety()
        {
            try
            {
                InitReflection();
                Component av = GetLocalPlayerAvatar() as Component;
                if (av == null) return;

                Vector3 targetPos = Vector3.zero;
                bool foundPos = false;

                Type truckSafetyType = Type.GetType("TruckSafetySpawnPoint, Assembly-CSharp");
                if (truckSafetyType != null)
                {
                    FieldInfo inst = truckSafetyType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                    Component ts = (inst != null) ? inst.GetValue(null) as Component : null;
                    if (ts != null)
                    {
                        targetPos = ts.transform.position;
                        foundPos = true;
                    }
                }

                if (!foundPos && chargingStationType != null)
                {
                    object csInst = (chargingStationInstFld != null) ? chargingStationInstFld.GetValue(null) : null;
                    if (csInst == null) csInst = UnityEngine.Object.FindObjectOfType(chargingStationType);
                    Component csComp = csInst as Component;
                    if (csComp != null)
                    {
                        targetPos = csComp.transform.position + Vector3.up * 1.5f;
                        foundPos = true;
                    }
                }

                if (!foundPos)
                {
                    Type spawnPointType = Type.GetType("SpawnPoint, Assembly-CSharp");
                    if (spawnPointType != null)
                    {
                        Component sp = UnityEngine.Object.FindObjectOfType(spawnPointType) as Component;
                        if (sp != null)
                        {
                            targetPos = sp.transform.position + Vector3.up * 1f;
                            foundPos = true;
                        }
                    }
                }

                if (foundPos)
                {
                    if (playerTumbleFld != null)
                    {
                        object tumble = playerTumbleFld.GetValue(av);
                        if (tumble != null)
                        {
                            FieldInfo pgoFld = tumble.GetType().GetField("physGrabObject", BindingFlags.Public | BindingFlags.Instance);
                            if (pgoFld != null)
                            {
                                object pgo = pgoFld.GetValue(tumble);
                                if (pgo != null)
                                {
                                    MethodInfo tpMethod = pgo.GetType().GetMethod("Teleport", new Type[] { typeof(Vector3), typeof(Quaternion) });
                                    if (tpMethod != null)
                                    {
                                        tpMethod.Invoke(pgo, new object[] { targetPos, av.transform.rotation });
                                    }
                                }
                            }
                        }
                    }

                    av.transform.position = targetPos;

                    FieldInfo playerTransformFld = av.GetType().GetField("playerTransform", BindingFlags.Public | BindingFlags.Instance);
                    if (playerTransformFld != null)
                    {
                        Transform ptr = playerTransformFld.GetValue(av) as Transform;
                        if (ptr != null) ptr.position = targetPos;
                    }

                    Type camPosType = Type.GetType("CameraPosition, Assembly-CSharp");
                    if (camPosType != null)
                    {
                        FieldInfo cpInst = camPosType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                        Component cp = (cpInst != null) ? cpInst.GetValue(null) as Component : null;
                        if (cp != null) cp.transform.position = targetPos;
                    }

                    if (deadSetFld != null) deadSetFld.SetValue(av, false);
                    if (playerHealthFld != null)
                    {
                        object ph = playerHealthFld.GetValue(av);
                        if (ph != null)
                        {
                            if (healthFld != null) healthFld.SetValue(ph, 100);
                            if (maxHealthFld != null) maxHealthFld.SetValue(ph, 100);
                        }
                    }
                }
            }
            catch { }
        }

        public void SpawnEnemyValuable(int size)
        {
            try
            {
                InitReflection();
                Component av = GetLocalPlayerAvatar() as Component;
                Transform tr = (av != null) ? av.transform : (Camera.main != null ? Camera.main.transform : null);
                if (tr == null) return;

                Vector3 spawnPos = tr.position + tr.forward * 1.6f + Vector3.up * 0.4f;

                Type assetMgrType = Type.GetType("AssetManager, Assembly-CSharp");
                if (assetMgrType == null) return;
                FieldInfo instFld = assetMgrType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                object am = (instFld != null) ? instFld.GetValue(null) : null;
                if (am == null) return;

                string fieldName = (size == 0) ? "enemyValuableSmall" : ((size == 1) ? "enemyValuableMedium" : "enemyValuableBig");
                FieldInfo prefFld = assetMgrType.GetField(fieldName, BindingFlags.Public | BindingFlags.Instance);
                GameObject prefab = (prefFld != null) ? prefFld.GetValue(am) as GameObject : null;
                if (prefab == null) return;

                GameObject spawned = null;
                MethodInfo isMpMethod = (semiFuncType != null) ? semiFuncType.GetMethod("IsMultiplayer", BindingFlags.Public | BindingFlags.Static) : null;
                bool isMp = (isMpMethod != null) ? (bool)isMpMethod.Invoke(null, null) : false;

                if (isMp)
                {
                    Type photonNetType = Type.GetType("Photon.Pun.PhotonNetwork, PhotonUnityNetworking");
                    if (photonNetType == null) photonNetType = Type.GetType("PhotonNetwork, Assembly-CSharp");
                    if (photonNetType != null)
                    {
                        MethodInfo instRoom = photonNetType.GetMethod("InstantiateRoomObject", new Type[] { typeof(string), typeof(Vector3), typeof(Quaternion), typeof(byte), typeof(object[]) });
                        if (instRoom != null)
                        {
                            spawned = instRoom.Invoke(null, new object[] { "Valuables/" + prefab.name, spawnPos, Quaternion.identity, (byte)0, null }) as GameObject;
                        }
                    }
                }

                if (spawned == null)
                {
                    spawned = UnityEngine.Object.Instantiate(prefab, spawnPos, Quaternion.identity) as GameObject;
                }
            }
            catch { }
        }

        public void SpawnGameItem(string itemName)
        {
            try
            {
                InitReflection();
                Component av = GetLocalPlayerAvatar() as Component;
                Transform tr = (av != null) ? av.transform : (Camera.main != null ? Camera.main.transform : null);
                if (tr == null) return;

                Vector3 spawnPos = tr.position + tr.forward * 1.6f + Vector3.up * 0.4f;
                Quaternion spawnRot = Quaternion.identity;

                Type statsMgrType = Type.GetType("StatsManager, Assembly-CSharp");
                if (statsMgrType == null) return;
                FieldInfo smInstFld = statsMgrType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                object sm = (smInstFld != null) ? smInstFld.GetValue(null) : null;
                if (sm == null) return;

                FieldInfo itemDictFld = statsMgrType.GetField("itemDictionary", BindingFlags.Public | BindingFlags.Instance);
                IDictionary dict = (itemDictFld != null) ? itemDictFld.GetValue(sm) as IDictionary : null;
                if (dict == null) return;

                object targetItem = null;
                string cleanTarget = itemName.ToLower().Replace("item", "").Trim();

                FieldInfo itemObjNameFld = null;
                PropertyInfo itemObjNameProp = null;

                foreach (DictionaryEntry entry in dict)
                {
                    string key = (entry.Key != null) ? entry.Key.ToString().ToLower() : "";
                    object itemObj = entry.Value;
                    if (itemObj == null) continue;

                    if (itemObjNameFld == null && itemObjNameProp == null)
                    {
                        itemObjNameFld = itemObj.GetType().GetField("itemName", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        itemObjNameProp = itemObj.GetType().GetProperty("name");
                    }

                    string internalName = "";
                    if (itemObjNameFld != null)
                    {
                        object val = itemObjNameFld.GetValue(itemObj);
                        if (val != null) internalName = val.ToString().ToLower();
                    }
                    if (string.IsNullOrEmpty(internalName) && itemObjNameProp != null)
                    {
                        object val = itemObjNameProp.GetValue(itemObj, null);
                        if (val != null) internalName = val.ToString().ToLower();
                    }

                    string cleanKey = key.Replace("item", "").Trim();
                    string cleanInternal = internalName.Replace("item", "").Trim();

                    if (cleanKey == cleanTarget || key == itemName.ToLower() ||
                        cleanInternal == cleanTarget || internalName == itemName.ToLower())
                    {
                        targetItem = itemObj;
                        break;
                    }
                }

                if (targetItem == null)
                {
                    foreach (DictionaryEntry entry in dict)
                    {
                        string key = (entry.Key != null) ? entry.Key.ToString().ToLower() : "";
                        object itemObj = entry.Value;
                        if (itemObj == null) continue;

                        string internalName = "";
                        if (itemObjNameFld != null)
                        {
                            object val = itemObjNameFld.GetValue(itemObj);
                            if (val != null) internalName = val.ToString().ToLower();
                        }
                        if (string.IsNullOrEmpty(internalName) && itemObjNameProp != null)
                        {
                            object val = itemObjNameProp.GetValue(itemObj, null);
                            if (val != null) internalName = val.ToString().ToLower();
                        }

                        string cleanKey = key.Replace("item", "").Trim();
                        string cleanInternal = internalName.Replace("item", "").Trim();

                        if (cleanKey.Contains(cleanTarget) || cleanTarget.Contains(cleanKey) ||
                            cleanInternal.Contains(cleanTarget) || cleanTarget.Contains(cleanInternal) ||
                            (cleanTarget.Contains("bullet") && (cleanKey.Contains("bullet") || cleanInternal.Contains("bullet") || cleanKey.Contains("gun") || cleanInternal.Contains("gun"))))
                        {
                            targetItem = itemObj;
                            break;
                        }
                    }
                }

                if (targetItem == null) return;

                FieldInfo prefabFld = targetItem.GetType().GetField("prefab", BindingFlags.Public | BindingFlags.Instance);
                object prefabRef = (prefabFld != null) ? prefabFld.GetValue(targetItem) : null;
                if (prefabRef == null) return;

                PropertyInfo prefProp = prefabRef.GetType().GetProperty("Prefab");
                GameObject prefab = (prefProp != null) ? prefProp.GetValue(prefabRef, null) as GameObject : null;

                PropertyInfo resPathProp = prefabRef.GetType().GetProperty("ResourcePath");
                string resPath = (resPathProp != null) ? resPathProp.GetValue(prefabRef, null) as string : null;

                GameObject spawned = null;
                MethodInfo isMpMethod = (semiFuncType != null) ? semiFuncType.GetMethod("IsMultiplayer", BindingFlags.Public | BindingFlags.Static) : null;
                bool isMp = (isMpMethod != null) ? (bool)isMpMethod.Invoke(null, null) : false;

                if (isMp && !string.IsNullOrEmpty(resPath))
                {
                    Type photonNetType = Type.GetType("Photon.Pun.PhotonNetwork, PhotonUnityNetworking");
                    if (photonNetType == null) photonNetType = Type.GetType("PhotonNetwork, Assembly-CSharp");
                    if (photonNetType != null)
                    {
                        MethodInfo instRoom = photonNetType.GetMethod("InstantiateRoomObject", new Type[] { typeof(string), typeof(Vector3), typeof(Quaternion), typeof(byte), typeof(object[]) });
                        if (instRoom != null)
                        {
                            spawned = instRoom.Invoke(null, new object[] { resPath, spawnPos, spawnRot, (byte)0, null }) as GameObject;
                        }
                    }
                }

                if (spawned == null && prefab != null)
                {
                    spawned = UnityEngine.Object.Instantiate(prefab, spawnPos, spawnRot) as GameObject;
                }
            }
            catch { }
        }

        public static void ForceUnlockCosmetics()
        {
            try
            {
                InitReflection();
                if (metaManagerType == null || metaManagerInstanceFld == null) return;

                object meta = metaManagerInstanceFld.GetValue(null);
                if (meta == null) return;

                MethodInfo unlockAllMethod = metaManagerType.GetMethod("CosmeticUnlockAll", BindingFlags.Public | BindingFlags.Instance);
                if (unlockAllMethod != null)
                {
                    unlockAllMethod.Invoke(meta, null);
                }

                FieldInfo unlocksField = metaManagerType.GetField("cosmeticUnlocks", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                FieldInfo assetsField = metaManagerType.GetField("cosmeticAssets", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (unlocksField != null && assetsField != null)
                {
                    IList unlocks = unlocksField.GetValue(meta) as IList;
                    IList assets = assetsField.GetValue(meta) as IList;

                    if (unlocks != null && assets != null)
                    {
                        for (int i = 0; i < assets.Count; i++)
                        {
                            if (!unlocks.Contains(i))
                            {
                                unlocks.Add(i);
                            }
                        }
                    }
                }

                MethodInfo saveMethod = metaManagerType.GetMethod("Save", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (saveMethod != null) saveMethod.Invoke(meta, null);
            }
            catch { }
        }

        private void ToggleMenu(bool state)
        {
            isOpen = state;

            try
            {
                InitReflection();

                Cursor.visible = state;
                Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;

                if (cursorInst != null && cursorUsersList != null)
                {
                    object cursor = cursorInst.GetValue(null, null);
                    if (cursor != null)
                    {
                        object list = cursorUsersList.GetValue(cursor, null);
                        if (list != null)
                        {
                            MethodInfo add = list.GetType().GetMethod("Add");
                            MethodInfo rm = list.GetType().GetMethod("Remove");
                            if (state && add != null) add.Invoke(list, new object[] { this });
                            else if (!state && rm != null) rm.Invoke(list, new object[] { this });
                        }
                    }
                }

                if (controllerInst != null && controllerBlocker != null)
                {
                    object ctrl = controllerInst.GetValue(null, null);
                    if (ctrl != null)
                    {
                        object blk = controllerBlocker.GetValue(ctrl);
                        if (blk != null)
                        {
                            PropertyInfo prop = blk.GetType().GetProperty("InputBlockers");
                            object set = prop != null ? prop.GetValue(blk, null) : null;
                            if (set != null)
                            {
                                MethodInfo add = set.GetType().GetMethod("Add");
                                MethodInfo rm = set.GetType().GetMethod("Remove");
                                if (state && add != null) add.Invoke(set, new object[] { this });
                                else if (!state && rm != null) rm.Invoke(set, new object[] { this });
                            }
                        }
                    }
                }

                if (!state && cameraAimType != null)
                {
                    object aimObj = (cameraAimInstProp != null) ? cameraAimInstProp.GetValue(null, null) : null;
                    if (aimObj == null && cameraAimInstFld != null) aimObj = cameraAimInstFld.GetValue(null);
                    if (aimObj != null)
                    {
                        if (aimOverrideAimStopFld != null) aimOverrideAimStopFld.SetValue(aimObj, false);
                        if (aimOverrideAimStopTimerFld != null) aimOverrideAimStopTimerFld.SetValue(aimObj, 0f);
                    }
                }
            }
            catch { }
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus && isOpen)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void OnDisable()
        {
            ToggleMenu(false);
        }

        private void OnDestroy()
        {
            ToggleMenu(false);
            if (fullbrightLight != null)
            {
                try { UnityEngine.Object.Destroy(fullbrightLight.gameObject); } catch { }
                fullbrightLight = null;
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert) || Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.Delete))
            {
                ToggleMenu(!isOpen);
            }

            if (isOpen)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

                if (cameraAimType != null)
                {
                    object aimObj = (cameraAimInstProp != null) ? cameraAimInstProp.GetValue(null, null) : null;
                    if (aimObj == null && cameraAimInstFld != null) aimObj = cameraAimInstFld.GetValue(null);
                    if (aimObj != null)
                    {
                        if (aimOverrideAimStopFld != null) aimOverrideAimStopFld.SetValue(aimObj, true);
                        if (aimOverrideAimStopTimerFld != null) aimOverrideAimStopTimerFld.SetValue(aimObj, 0.25f);
                    }
                }
            }

            // High frequency kinematic updates (per-frame)
            UpdatePlayerMovement();

            if (brightMap && fullbrightLight != null && fullbrightLight.enabled)
            {
                Camera cam = GetActiveCamera();
                if (cam != null)
                {
                    fullbrightLight.transform.position = cam.transform.position + cam.transform.forward * 0.6f;
                }
            }

            // Throttled hacks (10 times per second - 0.1s interval)
            hackTickTimer += Time.deltaTime;
            if (hackTickTimer >= 0.1f)
            {
                hackTickTimer = 0f;
                ApplyThrottledHacks();
            }

            // Periodic slow scans for scene components (once every 1.5s)
            slowScanTimer += Time.deltaTime;
            if (slowScanTimer >= 1.5f)
            {
                slowScanTimer = 0f;
                ApplyPeriodicSlowScans();
            }
        }

        private void UpdatePlayerMovement()
        {
            try
            {
                Component playerAvatar = GetLocalPlayerAvatar() as Component;
                Transform tr = (playerAvatar != null) ? playerAvatar.transform : null;

                if (tr != null && infiniteJumps && Input.GetKeyDown(KeyCode.Space))
                {
                    Rigidbody rb = tr.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.velocity = new Vector3(rb.velocity.x, 7.5f, rb.velocity.z);
                    }
                    else
                    {
                        tr.position += Vector3.up * 1.5f;
                    }
                }
            }
            catch { }
        }

        private void ApplyThrottledHacks()
        {
            try
            {
                if (infiniteMoney)
                {
                    SetPlayerMoney(999999);
                }

                if (photonPlayerControllerType != null && photonPcInstFld != null)
                {
                    object pc = photonPcInstFld.GetValue(null);
                    if (pc != null)
                    {
                        if (unlimitedEnergy)
                        {
                            if (photonPcEnergyStartFld != null && photonPcEnergyCurFld != null)
                            {
                                float startVal = (float)photonPcEnergyStartFld.GetValue(pc);
                                photonPcEnergyCurFld.SetValue(pc, startVal);
                            }
                            if (photonPcDebugEnergyFld != null) photonPcDebugEnergyFld.SetValue(pc, true);
                        }

                        if (infiniteJumps)
                        {
                            if (photonPcJumpCdFld != null) photonPcJumpCdFld.SetValue(pc, 0f);
                            if (photonPcJumpBufferFld != null) photonPcJumpBufferFld.SetValue(pc, 1f);
                            if (photonPcJumpExtraFld != null) photonPcJumpExtraFld.SetValue(pc, 99);
                        }

                        bool isSpeedModified = Mathf.Abs(walkSpeedMultiplier - 1.0f) > 0.05f ||
                                               Mathf.Abs(sprintSpeedMultiplier - 1.0f) > 0.05f ||
                                               Mathf.Abs(crouchSpeedMultiplier - 1.0f) > 0.05f;

                        if (isSpeedModified)
                        {
                            if (cachedBaseMove <= 0.01f && pcOriginalMoveSpeedFld != null) cachedBaseMove = Convert.ToSingle(pcOriginalMoveSpeedFld.GetValue(pc));
                            if (cachedBaseSprint <= 0.01f && pcOriginalSprintSpeedFld != null) cachedBaseSprint = Convert.ToSingle(pcOriginalSprintSpeedFld.GetValue(pc));
                            if (cachedBaseCrouch <= 0.01f && pcOriginalCrouchSpeedFld != null) cachedBaseCrouch = Convert.ToSingle(pcOriginalCrouchSpeedFld.GetValue(pc));

                            float baseMove = (cachedBaseMove > 0.01f) ? cachedBaseMove : 0.5f;
                            float baseSprint = (cachedBaseSprint > 0.01f) ? cachedBaseSprint : 1.0f;
                            float baseCrouch = (cachedBaseCrouch > 0.01f) ? cachedBaseCrouch : 1.0f;

                            if (photonPcMoveSpeedFld != null) photonPcMoveSpeedFld.SetValue(pc, baseMove * walkSpeedMultiplier);
                            if (photonPcSprintSpeedFld != null) photonPcSprintSpeedFld.SetValue(pc, baseSprint * sprintSpeedMultiplier);
                            if (photonPcCrouchSpeedFld != null) photonPcCrouchSpeedFld.SetValue(pc, baseCrouch * crouchSpeedMultiplier);
                        }
                    }
                }

                if (freeShop && shopManagerType != null && shopManagerInstFld != null)
                {
                    object sm = shopManagerInstFld.GetValue(null);
                    if (sm != null)
                    {
                        if (smTotalCostFld != null) smTotalCostFld.SetValue(sm, 0);
                        if (smItemMultiplierFld != null) smItemMultiplierFld.SetValue(sm, 0f);
                    }
                }

                if (freeExtraction)
                {
                    if (roundDirectorType != null && roundDirectorInstFld != null)
                    {
                        object rd = roundDirectorInstFld.GetValue(null);
                        if (rd != null && rdHaulGoalFld != null && rdCurrentHaulFld != null)
                        {
                            int curHaul = Convert.ToInt32(rdCurrentHaulFld.GetValue(rd));
                            rdHaulGoalFld.SetValue(rd, curHaul);
                        }
                    }

                    for (int i = 0; i < cachedExtractionPoints.Count; i++)
                    {
                        Component ep = cachedExtractionPoints[i];
                        if (ep == null) continue;
                        if (epHaulGoalFld != null) epHaulGoalFld.SetValue(ep, 0);
                        if (epIsLockedFld != null) epIsLockedFld.SetValue(ep, false);
                        if (epUpdateLockMethod != null)
                        {
                            try { epUpdateLockMethod.Invoke(ep, new object[] { false }); } catch { }
                        }
                    }
                }

                Component playerAvatar = GetLocalPlayerAvatar() as Component;
                Transform tr = (playerAvatar != null) ? playerAvatar.transform : null;

                if (playerAvatar != null)
                {
                    if (deadSetFld != null && noDamage)
                    {
                        deadSetFld.SetValue(playerAvatar, false);
                    }

                    if (playerHealthFld != null)
                    {
                        object pHealth = playerHealthFld.GetValue(playerAvatar);
                        if (pHealth != null)
                        {
                            if (noDamage)
                            {
                                if (healthGodModeFld != null) healthGodModeFld.SetValue(pHealth, true);
                                if (invincibleTimerFld != null) invincibleTimerFld.SetValue(pHealth, 10f);
                                if (maxHealthFld != null) maxHealthFld.SetValue(pHealth, 100);
                                if (healthFld != null) healthFld.SetValue(pHealth, 100);
                            }
                            else
                            {
                                if (healthGodModeFld != null) healthGodModeFld.SetValue(pHealth, false);
                            }
                        }
                    }

                    if (physGrabberFld != null && superGrab)
                    {
                        object grabber = physGrabberFld.GetValue(playerAvatar);
                        if (grabber != null)
                        {
                            float s = grabForceMultiplier;
                            if (grabStrengthFld != null) grabStrengthFld.SetValue(grabber, 4.0f * s);
                            if (throwStrengthFld != null) throwStrengthFld.SetValue(grabber, 5.0f * s);
                            if (grabRangeFld != null) grabRangeFld.SetValue(grabber, 6.0f + (2.0f * s));
                            if (grabReleaseDistFld != null) grabReleaseDistFld.SetValue(grabber, 12.0f + (3.0f * s));
                            if (forceMaxFld != null) forceMaxFld.SetValue(grabber, 1200f * s);
                            if (overrideGrabStrengthFld != null) overrideGrabStrengthFld.SetValue(grabber, 4.0f * s);
                            if (overrideGrabStrengthTimerFld != null) overrideGrabStrengthTimerFld.SetValue(grabber, 2f);
                            if (overrideAlwaysGrabbableTimerFld != null) overrideAlwaysGrabbableTimerFld.SetValue(grabber, 2f);
                        }
                    }
                }

                if (enemyDirectorType != null && enemyDirectorInstanceFld != null && enemyDirectorDebugNoVisionFld != null)
                {
                    object ed = enemyDirectorInstanceFld.GetValue(null);
                    if (ed != null)
                    {
                        IList nvList = enemyDirectorDebugNoVisionFld.GetValue(ed) as IList;
                        if (nvList != null)
                        {
                            string sid = "local";
                            if (playerAvatar != null && playerAvatarSteamIDFld != null)
                            {
                                object sidVal = playerAvatarSteamIDFld.GetValue(playerAvatar);
                                if (sidVal != null) sid = sidVal.ToString();
                            }
                            if (untouchable)
                            {
                                if (!nvList.Contains(sid)) nvList.Add(sid);
                            }
                            else
                            {
                                while (nvList.Contains(sid)) nvList.Remove(sid);
                            }
                        }
                    }
                }

                if (tr != null && invisibility)
                {
                    Renderer[] renderers = tr.GetComponentsInChildren<Renderer>();
                    if (renderers != null)
                    {
                        for (int i = 0; i < renderers.Length; i++)
                        {
                            if (renderers[i] != null && renderers[i].enabled) renderers[i].enabled = false;
                        }
                    }
                }

                if (tr != null && (infiniteItemBattery || autoChargeHeldItem) && itemBatteryType != null)
                {
                    batteryScanTimer += 0.1f;
                    if (batteryScanTimer >= 0.5f)
                    {
                        batteryScanTimer = 0f;
                        Component[] batteries = tr.GetComponentsInChildren(itemBatteryType);
                        if (batteries != null)
                        {
                            for (int b = 0; b < batteries.Length; b++)
                            {
                                Component ib = batteries[b];
                                if (ib == null) continue;
                                if (batteryBarsFld != null && batteryLifeFld != null && batteryLifeIntFld != null)
                                {
                                    int bars = Convert.ToInt32(batteryBarsFld.GetValue(ib));
                                    batteryLifeFld.SetValue(ib, 100f);
                                    batteryLifeIntFld.SetValue(ib, bars);
                                    if (drainRateFld != null) drainRateFld.SetValue(ib, 0f);
                                }
                            }
                        }
                    }
                }

                if (brightMap)
                {
                    Camera mainCam = GetActiveCamera();
                    if (mainCam != null)
                    {
                        if (fullbrightLight == null)
                        {
                            GameObject lightObj = new GameObject("PatchZone_Fullbright");
                            lightObj.hideFlags = HideFlags.HideAndDontSave;
                            fullbrightLight = lightObj.AddComponent<Light>();
                            fullbrightLight.type = LightType.Point;
                            fullbrightLight.range = 80f;
                            fullbrightLight.intensity = 2.2f;
                            fullbrightLight.color = new Color(0.95f, 0.95f, 1.0f);
                            fullbrightLight.shadows = LightShadows.None;
                        }
                        if (fullbrightLight != null)
                        {
                            fullbrightLight.enabled = true;
                            fullbrightLight.transform.position = mainCam.transform.position + mainCam.transform.forward * 0.6f;
                        }
                    }
                }
                else
                {
                    if (fullbrightLight != null && fullbrightLight.enabled) fullbrightLight.enabled = false;
                }

                if (infiniteStationCharge && chargingStationType != null)
                {
                    object csInst = (chargingStationInstFld != null) ? chargingStationInstFld.GetValue(null) : null;
                    if (csInst != null)
                    {
                        if (chargeTotalFld != null) chargeTotalFld.SetValue(csInst, 100);
                        if (chargeFloatFld != null) chargeFloatFld.SetValue(csInst, 1f);
                        if (chargeIntFld != null) chargeIntFld.SetValue(csInst, 10);
                        if (chargeSegmentCurrentFld != null) chargeSegmentCurrentFld.SetValue(csInst, 40);
                        if (chargeScaleTargetFld != null) chargeScaleTargetFld.SetValue(csInst, 1f);
                    }
                }
            }
            catch { }
        }

        private void ApplyPeriodicSlowScans()
        {
            try
            {
                // Update cached extraction points for ESP (runs every 1.5s instead of in OnGUI!)
                if (extractionPointType != null)
                {
                    cachedExtractionPoints.Clear();

                    if (roundDirectorType != null && roundDirectorInstFld != null && roundDirectorEpCurFld != null)
                    {
                        object rd = roundDirectorInstFld.GetValue(null);
                        if (rd != null)
                        {
                            Component epCur = roundDirectorEpCurFld.GetValue(rd) as Component;
                            if (epCur != null && !cachedExtractionPoints.Contains(epCur)) cachedExtractionPoints.Add(epCur);

                            if (roundDirectorEpListFld != null)
                            {
                                IList list = roundDirectorEpListFld.GetValue(rd) as IList;
                                if (list != null)
                                {
                                    for (int l = 0; l < list.Count; l++)
                                    {
                                        GameObject go = list[l] as GameObject;
                                        if (go != null)
                                        {
                                            Component epComp = go.GetComponent(extractionPointType);
                                            if (epComp != null && !cachedExtractionPoints.Contains(epComp)) cachedExtractionPoints.Add(epComp);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (cachedExtractionPoints.Count == 0)
                    {
                        UnityEngine.Object[] points = UnityEngine.Object.FindObjectsOfType(extractionPointType);
                        if (points != null)
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                Component ep = points[i] as Component;
                                if (ep != null && !cachedExtractionPoints.Contains(ep)) cachedExtractionPoints.Add(ep);
                            }
                        }
                    }
                }

                // Unlock shop items (only on state change)
                if (itemDeactivatedUntilLevelType != null && unlockShopItems != prevUnlockShopItems)
                {
                    prevUnlockShopItems = unlockShopItems;
                    if (unlockShopItems)
                    {
                        UnityEngine.Object[] deacts = UnityEngine.Object.FindObjectsOfType(itemDeactivatedUntilLevelType);
                        if (deacts != null)
                        {
                            for (int i = 0; i < deacts.Length; i++)
                            {
                                Component c = deacts[i] as Component;
                                if (c == null) continue;
                                if (levelToActivateFld != null) levelToActivateFld.SetValue(c, 0);
                                if (c.gameObject != null && !c.gameObject.activeSelf) c.gameObject.SetActive(true);
                            }
                        }
                    }
                }

                // Unbreakable items (only on state change)
                if (impactDetectorType != null && unbreakableItems != prevUnbreakableItems)
                {
                    prevUnbreakableItems = unbreakableItems;
                    UnityEngine.Object[] detectors = UnityEngine.Object.FindObjectsOfType(impactDetectorType);
                    if (detectors != null)
                    {
                        for (int i = 0; i < detectors.Length; i++)
                        {
                            Component det = detectors[i] as Component;
                            if (det == null) continue;
                            if (idIsIndestructibleFld != null) idIsIndestructibleFld.SetValue(det, unbreakableItems);
                            if (idDestroyDisableFld != null) idDestroyDisableFld.SetValue(det, unbreakableItems);
                            if (idFragilityFld != null) idFragilityFld.SetValue(det, unbreakableItems ? 0f : 1f);
                            if (idDurabilityFld != null) idDurabilityFld.SetValue(det, unbreakableItems ? 1000f : 1f);
                        }
                    }
                }
            }
            catch { }
        }

        private void InitTextures()
        {
            if (texturesInitialized) return;

            texBackground = MakeTex(2, 2, new Color(0.047f, 0.051f, 0.067f, 0.98f)); // #0C0D11 Deep Obsidian
            texSidebar = MakeTex(2, 2, new Color(0.063f, 0.071f, 0.094f, 1f));     // #101218
            texHeader = MakeTex(2, 2, new Color(0.055f, 0.063f, 0.082f, 1f));      // #0E1015
            texCard = MakeTex(2, 2, new Color(0.082f, 0.094f, 0.125f, 0.96f));     // #151820
            texCardBorder = MakeTex(2, 2, new Color(0.141f, 0.161f, 0.216f, 1f)); // #242937
            texAccent = MakeTex(2, 2, new Color(0.486f, 0.361f, 0.988f, 1f));     // #7C5CFC Electric Violet
            texAccentCyan = MakeTex(2, 2, new Color(0.0f, 0.824f, 1.0f, 1f));      // #00D2FF Neon Cyan
            texToggleOn = MakeTex(2, 2, new Color(0.063f, 0.725f, 0.506f, 1f));    // #10B981 Emerald ON
            texToggleOff = MakeTex(2, 2, new Color(0.125f, 0.141f, 0.188f, 1f));   // #202430 Dark Slate OFF
            texBtnNormal = MakeTex(2, 2, new Color(0.118f, 0.137f, 0.184f, 1f));   // #1E232F
            texBtnHover = MakeTex(2, 2, new Color(0.165f, 0.192f, 0.259f, 1f));    // #2A3142
            texWhite = MakeTex(2, 2, Color.white);
            texDarkBadge = MakeTex(2, 2, new Color(0.04f, 0.04f, 0.05f, 0.95f));

            styleTitle = new GUIStyle();
            styleTitle.fontSize = 15;
            styleTitle.fontStyle = FontStyle.Bold;
            styleTitle.normal.textColor = Color.white;
            styleTitle.alignment = TextAnchor.MiddleLeft;

            styleSubtitle = new GUIStyle();
            styleSubtitle.fontSize = 10;
            styleSubtitle.fontStyle = FontStyle.Bold;
            styleSubtitle.normal.textColor = new Color(0.486f, 0.361f, 0.988f);
            styleSubtitle.alignment = TextAnchor.MiddleLeft;

            styleTabActive = new GUIStyle();
            styleTabActive.fontSize = 12;
            styleTabActive.fontStyle = FontStyle.Bold;
            styleTabActive.normal.background = MakeTex(2, 2, new Color(0.118f, 0.137f, 0.184f, 0.9f));
            styleTabActive.normal.textColor = new Color(0.0f, 0.824f, 1.0f);
            styleTabActive.padding = new RectOffset(16, 8, 8, 8);
            styleTabActive.alignment = TextAnchor.MiddleLeft;

            styleTabInactive = new GUIStyle();
            styleTabInactive.fontSize = 12;
            styleTabInactive.normal.background = MakeTex(2, 2, Color.clear);
            styleTabInactive.normal.textColor = new Color(0.55f, 0.60f, 0.70f);
            styleTabInactive.hover.textColor = Color.white;
            styleTabInactive.hover.background = MakeTex(2, 2, new Color(0.10f, 0.11f, 0.14f, 0.5f));
            styleTabInactive.padding = new RectOffset(16, 8, 8, 8);
            styleTabInactive.alignment = TextAnchor.MiddleLeft;

            styleHeader = new GUIStyle();
            styleHeader.fontSize = 12;
            styleHeader.fontStyle = FontStyle.Bold;
            styleHeader.normal.textColor = new Color(0.0f, 0.824f, 1.0f);
            styleHeader.alignment = TextAnchor.MiddleLeft;

            styleLabelText = new GUIStyle();
            styleLabelText.fontSize = 12;
            styleLabelText.normal.textColor = new Color(0.88f, 0.90f, 0.95f);
            styleLabelText.alignment = TextAnchor.MiddleLeft;

            styleToggleOn = new GUIStyle(GUI.skin.button);
            styleToggleOn.fontSize = 11;
            styleToggleOn.fontStyle = FontStyle.Bold;
            styleToggleOn.normal.background = texToggleOn;
            styleToggleOn.normal.textColor = Color.white;
            styleToggleOn.hover.background = texToggleOn;
            styleToggleOn.hover.textColor = Color.white;
            styleToggleOn.alignment = TextAnchor.MiddleCenter;

            styleToggleOff = new GUIStyle(GUI.skin.button);
            styleToggleOff.fontSize = 11;
            styleToggleOff.normal.background = texToggleOff;
            styleToggleOff.normal.textColor = new Color(0.55f, 0.60f, 0.70f);
            styleToggleOff.hover.background = MakeTex(2, 2, new Color(0.16f, 0.18f, 0.24f, 1f));
            styleToggleOff.hover.textColor = Color.white;
            styleToggleOff.alignment = TextAnchor.MiddleCenter;

            styleStepperBtn = new GUIStyle(GUI.skin.button);
            styleStepperBtn.fontSize = 13;
            styleStepperBtn.fontStyle = FontStyle.Bold;
            styleStepperBtn.normal.background = texBtnNormal;
            styleStepperBtn.normal.textColor = new Color(0.0f, 0.824f, 1.0f);
            styleStepperBtn.hover.background = texBtnHover;
            styleStepperBtn.hover.textColor = Color.white;
            styleStepperBtn.alignment = TextAnchor.MiddleCenter;

            styleStepperVal = new GUIStyle();
            styleStepperVal.fontSize = 12;
            styleStepperVal.fontStyle = FontStyle.Bold;
            styleStepperVal.normal.textColor = Color.white;
            styleStepperVal.normal.background = MakeTex(2, 2, new Color(0.06f, 0.07f, 0.10f, 0.9f));
            styleStepperVal.alignment = TextAnchor.MiddleCenter;

            styleActionBtn = new GUIStyle(GUI.skin.button);
            styleActionBtn.fontSize = 12;
            styleActionBtn.fontStyle = FontStyle.Bold;
            styleActionBtn.normal.background = texBtnNormal;
            styleActionBtn.normal.textColor = new Color(0.90f, 0.92f, 0.96f);
            styleActionBtn.hover.background = texBtnHover;
            styleActionBtn.hover.textColor = Color.white;
            styleActionBtn.alignment = TextAnchor.MiddleCenter;

            styleCloseBtn = new GUIStyle(GUI.skin.button);
            styleCloseBtn.fontSize = 13;
            styleCloseBtn.fontStyle = FontStyle.Bold;
            styleCloseBtn.normal.background = MakeTex(2, 2, new Color(0.22f, 0.08f, 0.08f, 0.8f));
            styleCloseBtn.normal.textColor = new Color(1f, 0.4f, 0.4f);
            styleCloseBtn.hover.background = MakeTex(2, 2, new Color(0.8f, 0.15f, 0.15f, 0.9f));
            styleCloseBtn.hover.textColor = Color.white;
            styleCloseBtn.alignment = TextAnchor.MiddleCenter;

            espLabelNormal = new GUIStyle();
            espLabelNormal.fontSize = 11;
            espLabelNormal.alignment = TextAnchor.MiddleCenter;
            espLabelNormal.normal.textColor = new Color(0.2f, 1.0f, 0.5f);

            espLabelGold = new GUIStyle();
            espLabelGold.fontSize = 11;
            espLabelGold.fontStyle = FontStyle.Bold;
            espLabelGold.alignment = TextAnchor.MiddleCenter;
            espLabelGold.normal.textColor = new Color(1.0f, 0.85f, 0.25f);

            espEnemyStyle = new GUIStyle();
            espEnemyStyle.fontSize = 11;
            espEnemyStyle.fontStyle = FontStyle.Bold;
            espEnemyStyle.alignment = TextAnchor.MiddleCenter;
            espEnemyStyle.normal.textColor = Color.white;

            espHpStyle = new GUIStyle();
            espHpStyle.fontSize = 10;
            espHpStyle.alignment = TextAnchor.MiddleRight;
            espHpStyle.normal.textColor = new Color(0.85f, 0.90f, 0.85f);

            espExtractionStyle = new GUIStyle();
            espExtractionStyle.fontSize = 12;
            espExtractionStyle.fontStyle = FontStyle.Bold;
            espExtractionStyle.alignment = TextAnchor.MiddleCenter;
            espExtractionStyle.normal.textColor = new Color(0.2f, 0.85f, 1.0f);

            texturesInitialized = true;
        }

        private void OnGUI()
        {
            Event e = Event.current;
            if (e != null && e.type == EventType.KeyDown)
            {
                if (e.keyCode == KeyCode.Insert || e.keyCode == KeyCode.F1 || e.keyCode == KeyCode.Delete)
                {
                    ToggleMenu(!isOpen);
                    e.Use();
                }
            }

            InitTextures();

            if (Event.current != null && Event.current.type == EventType.Repaint)
            {
                GUI.depth = 10;

                if (espValuables)
                {
                    DrawValuablesESP();
                }

                if (espEnemies)
                {
                    DrawEnemiesESP();
                }

                if (espExtraction)
                {
                    DrawExtractionESP();
                }
            }

            if (!isOpen) return;

            GUI.depth = -100;
            winRect = GUI.Window(99991, winRect, DrawModernMenuContent, "", emptyWindow);
        }

        private Camera GetActiveCamera()
        {
            _camCacheTimer += Time.deltaTime;
            if (_cachedCam != null && _cachedCam.isActiveAndEnabled && _camCacheTimer < 0.5f)
            {
                return _cachedCam;
            }
            _camCacheTimer = 0f;

            if (cameraUtilsType != null && cameraUtilsInstanceFld != null && cameraUtilsMainCameraFld != null)
            {
                object inst = cameraUtilsInstanceFld.GetValue(null);
                if (inst != null)
                {
                    Camera c = cameraUtilsMainCameraFld.GetValue(inst) as Camera;
                    if (c != null && c.isActiveAndEnabled)
                    {
                        _cachedCam = c;
                        return c;
                    }
                }
            }

            Camera cam = Camera.main;
            if (cam != null && cam.isActiveAndEnabled)
            {
                _cachedCam = cam;
                return cam;
            }

            if (Camera.current != null && Camera.current.isActiveAndEnabled)
            {
                _cachedCam = Camera.current;
                return Camera.current;
            }

            return _cachedCam;
        }

        private void DrawBoxOutline(Rect r, Color c, float thickness)
        {
            if (texWhite == null) return;
            Color prev = GUI.color;
            GUI.color = c;
            GUI.DrawTexture(new Rect(r.x, r.y, r.width, thickness), texWhite);
            GUI.DrawTexture(new Rect(r.x, r.y + r.height - thickness, r.width, thickness), texWhite);
            GUI.DrawTexture(new Rect(r.x, r.y + thickness, thickness, r.height - (thickness * 2f)), texWhite);
            GUI.DrawTexture(new Rect(r.x + r.width - thickness, r.y + thickness, thickness, r.height - (thickness * 2f)), texWhite);
            GUI.color = prev;
        }

        private void DrawCornerBox(Rect r, Color c, float thickness, float cornerSize)
        {
            if (texWhite == null) return;
            Color prev = GUI.color;
            GUI.color = c;
            cornerSize = Mathf.Min(cornerSize, Mathf.Min(r.width * 0.4f, r.height * 0.4f));

            GUI.DrawTexture(new Rect(r.x, r.y, cornerSize, thickness), texWhite);
            GUI.DrawTexture(new Rect(r.x, r.y, thickness, cornerSize), texWhite);

            GUI.DrawTexture(new Rect(r.x + r.width - cornerSize, r.y, cornerSize, thickness), texWhite);
            GUI.DrawTexture(new Rect(r.x + r.width - thickness, r.y, thickness, cornerSize), texWhite);

            GUI.DrawTexture(new Rect(r.x, r.y + r.height - thickness, cornerSize, thickness), texWhite);
            GUI.DrawTexture(new Rect(r.x, r.y + r.height - cornerSize, thickness, cornerSize), texWhite);

            GUI.DrawTexture(new Rect(r.x + r.width - cornerSize, r.y + r.height - thickness, cornerSize, thickness), texWhite);
            GUI.DrawTexture(new Rect(r.x + r.width - thickness, r.y + r.height - cornerSize, thickness, cornerSize), texWhite);

            GUI.color = prev;
        }

        private void DrawValuablesESP()
        {
            Camera cam = GetActiveCamera();
            if (cam == null) return;

            try
            {
                InitReflection();
                if (valuableDirectorType == null || valuableDirectorInstanceFld == null || valuableListFld == null) return;

                object director = valuableDirectorInstanceFld.GetValue(null);
                if (director == null) return;

                IList list = valuableListFld.GetValue(director) as IList;
                if (list == null || list.Count == 0) return;

                Vector3 camPos = cam.transform.position;
                Vector3 camFwd = cam.transform.forward;

                float scaleX = 1f;
                float scaleY = 1f;
                if (renderTextureMainType != null && renderTextureMainInstanceFld != null && renderTextureWidthFld != null && renderTextureHeightFld != null)
                {
                    object rtInst = renderTextureMainInstanceFld.GetValue(null);
                    if (rtInst != null)
                    {
                        float rw = Convert.ToSingle(renderTextureWidthFld.GetValue(rtInst));
                        float rh = Convert.ToSingle(renderTextureHeightFld.GetValue(rtInst));
                        if (rw > 10f && rh > 10f)
                        {
                            scaleX = (float)Screen.width / rw;
                            scaleY = (float)Screen.height / rh;
                        }
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    MonoBehaviour item = list[i] as MonoBehaviour;
                    if (item == null || item.gameObject == null || !item.gameObject.activeInHierarchy) continue;

                    Vector3 centerPos = item.transform.position;
                    Vector3 diff = centerPos - camPos;
                    if (Vector3.Dot(camFwd, diff) <= 0.15f) continue;

                    float dist = diff.magnitude;
                    if (dist > espMaxDistance || dist < 0.2f) continue;

                    float heightY = 0.5f;
                    if (physGrabObjectType != null)
                    {
                        Component phys = item.GetComponent(physGrabObjectType);
                        if (phys != null)
                        {
                            if (physMidPointFld != null)
                            {
                                object mp = physMidPointFld.GetValue(phys);
                                if (mp is Vector3) centerPos = (Vector3)mp;
                            }
                            if (physBoundingBoxFld != null)
                            {
                                object bb = physBoundingBoxFld.GetValue(phys);
                                if (bb is Vector3) heightY = Mathf.Max(0.25f, ((Vector3)bb).y);
                            }
                        }
                    }

                    float price = 0f;
                    if (dollarValueCurrentFld != null)
                    {
                        object val = dollarValueCurrentFld.GetValue(item);
                        if (val != null) price = Convert.ToSingle(val);
                    }

                    bool isHighValue = (price >= 3000f);
                    if (espOnlyGoldItems && !isHighValue) continue;
                    if (espValuablesMinPrice > 0f && price < espValuablesMinPrice) continue;

                    Vector3 topPos = centerPos + Vector3.up * (heightY * 0.5f);
                    Vector3 btmPos = centerPos - Vector3.up * (heightY * 0.5f);

                    Vector3 screenTop = cam.WorldToScreenPoint(topPos);
                    Vector3 screenBtm = cam.WorldToScreenPoint(btmPos);
                    if (screenTop.z <= 0.2f || screenBtm.z <= 0.2f) continue;

                    float topY = (float)Screen.height - (screenTop.y * scaleY);
                    float btmY = (float)Screen.height - (screenBtm.y * scaleY);
                    float centerX = screenTop.x * scaleX;

                    float boxH = Mathf.Abs(btmY - topY);
                    if (boxH < 14f) boxH = 14f;
                    float boxW = boxH * 0.75f;
                    if (boxW < 14f) boxW = 14f;
                    float boxX = centerX - (boxW * 0.5f);
                    float boxY = Mathf.Min(topY, btmY);

                    if (boxX < -100 || boxX > (float)Screen.width + 100 || boxY < -100 || boxY > (float)Screen.height + 100) continue;

                    Color themeColor = (isHighValue && espGoldBadgeHighValue) ? new Color(1.0f, 0.85f, 0.25f) : new Color(0.2f, 1.0f, 0.45f);

                    if (espBoxValuables)
                    {
                        DrawCornerBox(new Rect(boxX, boxY, boxW, boxH), (isHighValue && espGoldBadgeHighValue) ? new Color(1.0f, 0.85f, 0.25f, 0.9f) : new Color(0.2f, 1.0f, 0.45f, 0.8f), 1.5f, 6f);
                    }

                    string rawName = GetCleanItemName(item.gameObject.name);

                    string labelText = "";
                    if (espShowPrice && price > 0f)
                    {
                        labelText += ((isHighValue && espGoldBadgeHighValue) ? "★ $" : "$") + price.ToString("N0") + " ";
                    }
                    labelText += rawName;
                    if (espShowDistance)
                    {
                        labelText += " [" + dist.ToString("0") + "m]";
                    }

                    float badgeW = labelText.Length * 7.2f + 16f;
                    float badgeH = 18f;
                    float badgeX = centerX - (badgeW * 0.5f);
                    float badgeY = boxY - badgeH - 3f;

                    if (texDarkBadge != null)
                    {
                        GUI.DrawTexture(new Rect(badgeX, badgeY, badgeW, badgeH), texDarkBadge);
                        DrawBoxOutline(new Rect(badgeX, badgeY, badgeW, badgeH), new Color(themeColor.r, themeColor.g, themeColor.b, 0.5f), 1f);
                    }

                    GUI.color = Color.white;
                    GUI.Label(new Rect(badgeX, badgeY, badgeW, badgeH), labelText, (isHighValue && espGoldBadgeHighValue) ? espLabelGold : espLabelNormal);
                }
            }
            catch { }
        }

        private void DrawEnemiesESP()
        {
            Camera cam = GetActiveCamera();
            if (cam == null) return;

            try
            {
                InitReflection();
                if (enemyDirectorType == null || enemyDirectorInstanceFld == null || enemiesSpawnedFld == null) return;

                object director = enemyDirectorInstanceFld.GetValue(null);
                if (director == null) return;

                IList list = enemiesSpawnedFld.GetValue(director) as IList;
                if (list == null || list.Count == 0) return;

                Vector3 camPos = cam.transform.position;
                Vector3 camFwd = cam.transform.forward;

                float scaleX = 1f;
                float scaleY = 1f;
                if (renderTextureMainType != null && renderTextureMainInstanceFld != null && renderTextureWidthFld != null && renderTextureHeightFld != null)
                {
                    object rtInst = renderTextureMainInstanceFld.GetValue(null);
                    if (rtInst != null)
                    {
                        float rw = Convert.ToSingle(renderTextureWidthFld.GetValue(rtInst));
                        float rh = Convert.ToSingle(renderTextureHeightFld.GetValue(rtInst));
                        if (rw > 10f && rh > 10f)
                        {
                            scaleX = (float)Screen.width / rw;
                            scaleY = (float)Screen.height / rh;
                        }
                    }
                }

                for (int i = 0; i < list.Count; i++)
                {
                    Component ep = list[i] as Component;
                    if (ep == null || ep.gameObject == null || !ep.gameObject.activeInHierarchy) continue;

                    if (epEnemyFld == null || epSpawnedFld == null)
                    {
                        Type epType = ep.GetType();
                        if (epEnemyFld == null) epEnemyFld = epType.GetField("Enemy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (epSpawnedFld == null) epSpawnedFld = epType.GetField("Spawned", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    }

                    if (epSpawnedFld != null)
                    {
                        object sp = epSpawnedFld.GetValue(ep);
                        if (sp is bool && !((bool)sp)) continue;
                    }

                    Component enemy = (epEnemyFld != null) ? epEnemyFld.GetValue(ep) as Component : null;
                    if (enemy == null) enemy = ep.GetComponentInChildren<MonoBehaviour>();
                    if (enemy == null || enemy.gameObject == null || !enemy.gameObject.activeInHierarchy) continue;

                    Vector3 centerPos = enemy.transform.position;
                    if (enCenterTransformFld != null)
                    {
                        Transform ct = enCenterTransformFld.GetValue(enemy) as Transform;
                        if (ct != null) centerPos = ct.position;
                    }

                    Vector3 diff = centerPos - camPos;
                    if (Vector3.Dot(camFwd, diff) <= 0.15f) continue;

                    float dist = diff.magnitude;
                    if (dist > espMaxDistance || dist < 0.2f) continue;

                    float enemyHeight = 1.8f;
                    Vector3 topPos = centerPos + Vector3.up * (enemyHeight * 0.5f);
                    Vector3 btmPos = centerPos - Vector3.up * (enemyHeight * 0.5f);

                    Vector3 screenTop = cam.WorldToScreenPoint(topPos);
                    Vector3 screenBtm = cam.WorldToScreenPoint(btmPos);
                    if (screenTop.z <= 0.2f || screenBtm.z <= 0.2f) continue;

                    float topY = (float)Screen.height - (screenTop.y * scaleY);
                    float btmY = (float)Screen.height - (screenBtm.y * scaleY);
                    float centerX = screenTop.x * scaleX;

                    float boxH = Mathf.Abs(btmY - topY);
                    if (boxH < 20f) boxH = 20f;
                    float boxW = boxH * 0.55f;
                    if (boxW < 16f) boxW = 16f;
                    float boxX = centerX - (boxW * 0.5f);
                    float boxY = Mathf.Min(topY, btmY);

                    if (boxX < -100 || boxX > (float)Screen.width + 100 || boxY < -100 || boxY > (float)Screen.height + 100) continue;

                    if (espBoxEnemies)
                    {
                        DrawCornerBox(new Rect(boxX, boxY, boxW, boxH), new Color(1.0f, 0.25f, 0.25f, 0.9f), 1.8f, 7f);
                    }

                    string eName = "Inimigo";
                    if (enemyNameFld != null)
                    {
                        object nVal = enemyNameFld.GetValue(ep);
                        if (nVal != null) eName = nVal.ToString();
                    }
                    else
                    {
                        eName = GetCleanItemName(enemy.gameObject.name);
                    }

                    int curHp = 100;
                    int maxHp = 100;
                    bool hasHealth = false;

                    if (enemyHealthCompFld != null)
                    {
                        Component eh = enemyHealthCompFld.GetValue(enemy) as Component;
                        if (eh != null)
                        {
                            if (healthCurrentFld != null)
                            {
                                curHp = Convert.ToInt32(healthCurrentFld.GetValue(eh));
                                hasHealth = true;
                            }
                            if (healthMaxFld != null)
                            {
                                maxHp = Mathf.Max(1, Convert.ToInt32(healthMaxFld.GetValue(eh)));
                            }
                        }
                    }

                    if (espShowEnemyName)
                    {
                        string topLabel = eName;
                        if (espShowDistance)
                        {
                            topLabel += " [" + dist.ToString("0") + "m]";
                        }

                        GUI.color = Color.white;
                        GUI.Label(new Rect(centerX - 100f, boxY - 18f, 200f, 16f), topLabel, espEnemyStyle);
                    }

                    if (hasHealth && espShowHealthBar)
                    {
                        float barW = 2.5f;
                        float barX = boxX - barW - 6f;
                        float barY = boxY;
                        float barH = boxH;

                        float hpPct = Mathf.Clamp01((float)curHp / (float)maxHp);
                        float fillH = barH * hpPct;
                        float fillY = barY + barH - fillH;

                        Color hpColor = Color.Lerp(new Color(1f, 0.25f, 0.25f), new Color(0.25f, 0.95f, 0.35f), hpPct);

                        if (texWhite != null)
                        {
                            GUI.color = new Color(0.04f, 0.05f, 0.06f, 0.85f);
                            GUI.DrawTexture(new Rect(barX - 1f, barY - 1f, barW + 2f, barH + 2f), texWhite);

                            GUI.color = hpColor;
                            GUI.DrawTexture(new Rect(barX, fillY, barW, fillH), texWhite);
                        }

                        GUI.color = Color.white;
                        GUI.Label(new Rect(barX - 42f, barY - 5f, 38f, 16f), curHp.ToString(), espHpStyle);
                    }
                }
            }
            catch { }
        }

        private void DrawExtractionESP()
        {
            Camera cam = GetActiveCamera();
            if (cam == null) return;

            try
            {
                if (cachedExtractionPoints.Count == 0) return;

                Vector3 camPos = cam.transform.position;
                Vector3 camFwd = cam.transform.forward;

                float scaleX = 1f;
                float scaleY = 1f;
                if (renderTextureMainType != null && renderTextureMainInstanceFld != null && renderTextureWidthFld != null && renderTextureHeightFld != null)
                {
                    object rtInst = renderTextureMainInstanceFld.GetValue(null);
                    if (rtInst != null)
                    {
                        float rw = Convert.ToSingle(renderTextureWidthFld.GetValue(rtInst));
                        float rh = Convert.ToSingle(renderTextureHeightFld.GetValue(rtInst));
                        if (rw > 10f && rh > 10f)
                        {
                            scaleX = (float)Screen.width / rw;
                            scaleY = (float)Screen.height / rh;
                        }
                    }
                }

                for (int i = 0; i < cachedExtractionPoints.Count; i++)
                {
                    Component ep = cachedExtractionPoints[i];
                    if (ep == null || ep.gameObject == null) continue;

                    Vector3 drawPos = ep.transform.position;
                    if (epPlatformFld != null)
                    {
                        Transform pt = epPlatformFld.GetValue(ep) as Transform;
                        if (pt != null) drawPos = pt.position;
                    }

                    Vector3 worldPos = drawPos + Vector3.up * 0.8f;
                    Vector3 diff = worldPos - camPos;
                    if (Vector3.Dot(camFwd, diff) <= 0.01f) continue;

                    float dist = diff.magnitude;
                    if (dist > espMaxDistance) continue;

                    Vector3 screenPos = cam.WorldToScreenPoint(worldPos);
                    if (screenPos.z <= 0.05f) continue;

                    string stateStr = "";
                    if (epCurrentStateFld != null)
                    {
                        object curSt = epCurrentStateFld.GetValue(ep);
                        if (curSt != null) stateStr = curSt.ToString();
                    }

                    bool isComplete = (stateStr == "Complete" || stateStr == "TaxReturn");
                    if (!isComplete && epExtractionSurplusCompletedFld != null)
                    {
                        object val = epExtractionSurplusCompletedFld.GetValue(ep);
                        if (val is bool && ((bool)val)) isComplete = true;
                    }
                    if (!isComplete && epIsCompletedRightAwayFld != null)
                    {
                        object val = epIsCompletedRightAwayFld.GetValue(ep);
                        if (val is bool && ((bool)val)) isComplete = true;
                    }

                    if (espExtractionOnlyIncomplete && isComplete) continue;

                    int quota = 0;
                    if (epHaulGoalFld != null)
                    {
                        object hg = epHaulGoalFld.GetValue(ep);
                        if (hg != null) quota = Convert.ToInt32(hg);
                    }

                    bool isDoorLocked = false;
                    if (epIsLockedFld != null)
                    {
                        object lk = epIsLockedFld.GetValue(ep);
                        if (lk is bool) isDoorLocked = (bool)lk;
                    }

                    if (espExtractionOnlyLocked && !isDoorLocked) continue;

                    string labelText = "⛟ EXTRAÇÃO (VENDER ITENS)";
                    if (isComplete) labelText += " [CONCLUÍDO]";
                    else if (isDoorLocked) labelText += " [BLOQUEADO]";
                    else if (quota <= 0) labelText += " [PRONTO]";
                    else labelText += " [$" + quota.ToString("N0") + "]";

                    if (espShowDistance)
                    {
                        labelText += " • " + dist.ToString("0") + "m";
                    }

                    Color pointColor = isComplete ? new Color(0.5f, 0.5f, 0.5f, 0.8f) :
                                       (isDoorLocked ? new Color(1.0f, 0.25f, 0.25f, 0.9f) :
                                       (quota <= 0 ? new Color(0.2f, 1.0f, 0.45f, 0.9f) : new Color(0.2f, 0.85f, 1.0f, 0.85f)));

                    float screenX = screenPos.x * scaleX;
                    float screenY = (float)Screen.height - (screenPos.y * scaleY);

                    float badgeW = labelText.Length * 7.5f + 16f;
                    float badgeH = 18f;
                    float badgeX = screenX - (badgeW * 0.5f);
                    float badgeY = screenY - 24f;

                    if (texDarkBadge != null)
                    {
                        GUI.DrawTexture(new Rect(badgeX, badgeY, badgeW, badgeH), texDarkBadge);
                        DrawBoxOutline(new Rect(badgeX, badgeY, badgeW, badgeH), pointColor, 1f);
                    }

                    GUI.color = Color.white;
                    GUI.Label(new Rect(badgeX, badgeY, badgeW, badgeH), labelText, espExtractionStyle);

                    DrawCornerBox(new Rect(screenX - 14f, screenY - 14f, 28f, 28f), pointColor, 1.2f, 5f);
                }
            }
            catch { }
        }


        private bool DrawModernToggle(string label, bool val)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, styleLabelText, GUILayout.ExpandWidth(true));

            Color prevColor = GUI.color;
            string stateText = val ? T("ATIVO", "ON") : T("INATIVO", "OFF");
            GUIStyle btnStyle = val ? styleToggleOn : styleToggleOff;

            if (GUILayout.Button(stateText, btnStyle, GUILayout.Width(76), GUILayout.Height(24)))
            {
                val = !val;
            }
            GUI.color = prevColor;
            GUILayout.EndHorizontal();
            GUILayout.Space(6);
            return val;
        }

        private float DrawModernStepper(string label, float val, float step, float min, float max, string format)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(label, styleLabelText, GUILayout.ExpandWidth(true));

            if (GUILayout.Button("-", styleStepperBtn, GUILayout.Width(28), GUILayout.Height(24)))
            {
                val = Mathf.Max(min, val - step);
            }

            GUILayout.Label(val.ToString(format), styleStepperVal, GUILayout.Width(54), GUILayout.Height(24));

            if (GUILayout.Button("+", styleStepperBtn, GUILayout.Width(28), GUILayout.Height(24)))
            {
                val = Mathf.Min(max, val + step);
            }

            GUILayout.EndHorizontal();
            GUILayout.Space(6);
            return val;
        }

        private void DrawModernMenuContent(int id)
        {
            float sideWidth = 175f;
            float headerHeight = 44f;

            // Window Base & Borders
            GUI.DrawTexture(new Rect(0, 0, winRect.width, winRect.height), texBackground);
            GUI.DrawTexture(new Rect(0, 0, sideWidth, winRect.height), texSidebar);
            GUI.DrawTexture(new Rect(0, 0, winRect.width, headerHeight), texHeader);

            // Subtle 1px borders
            GUI.DrawTexture(new Rect(0, headerHeight, winRect.width, 1), texCardBorder);
            GUI.DrawTexture(new Rect(sideWidth, headerHeight, 1, winRect.height - headerHeight), texCardBorder);
            GUI.DrawTexture(new Rect(0, 0, winRect.width, 2), texAccent);

            // Top Header: Title & Badges
            GUI.Label(new Rect(16, 10, 110, 24), "PATCHZONE", styleTitle);
            GUI.Label(new Rect(124, 11, 80, 24), "MODERN", styleSubtitle);

            // Version Pill
            Color prevCol = GUI.color;
            GUI.color = new Color(0.5f, 0.55f, 0.65f, 0.8f);
            GUI.Label(new Rect(205, 13, 140, 20), "[ v2.0 • MONO ]");
            GUI.color = prevCol;

            // Language Selector in Header
            if (GUI.Button(new Rect(winRect.width - 100, 10, 56, 24), langIndex == 0 ? "PT-BR" : "EN-US", styleActionBtn))
            {
                langIndex = (langIndex == 0) ? 1 : 0;
            }

            // Close Button
            if (GUI.Button(new Rect(winRect.width - 36, 10, 26, 24), "✕", styleCloseBtn))
            {
                ToggleMenu(false);
            }

            // Left Sidebar: Modern Tabs
            string[] tabs = Tabs[langIndex];
            string[] tabIcons = TabIcons;
            float tabY = headerHeight + 16f;

            for (int i = 0; i < tabs.Length; i++)
            {
                Rect rTab = new Rect(8, tabY, sideWidth - 16, 36);
                bool isActive = (currentTab == i);

                if (isActive)
                {
                    GUI.DrawTexture(new Rect(8, tabY + 4, 3, 28), texAccentCyan);
                }

                string label = (i < tabIcons.Length ? tabIcons[i] : "") + tabs[i];
                if (GUI.Button(rTab, label, isActive ? styleTabActive : styleTabInactive))
                {
                    currentTab = i;
                    contentScroll = Vector2.zero;
                }
                tabY += 40;
            }

            // Sidebar Footer: Dev credits
            float footerY = winRect.height - 46f;
            GUI.color = new Color(0.40f, 0.50f, 0.65f, 0.9f);
            GUI.Label(new Rect(14, footerY, sideWidth - 20, 18), "PatchZone • Kayke");
            GUI.Label(new Rect(14, footerY + 16, sideWidth - 20, 18), "@PatchZoneBR");
            GUI.color = Color.white;

            // Content Area Container
            float contentX = sideWidth + 14f;
            float contentY = headerHeight + 14f;
            float contentW = winRect.width - sideWidth - 28f;
            float contentH = winRect.height - headerHeight - 28f;

            // Draw Card Background
            GUI.DrawTexture(new Rect(contentX, contentY, contentW, contentH), texCard);
            GUI.DrawTexture(new Rect(contentX, contentY, contentW, 1), texCardBorder);
            GUI.DrawTexture(new Rect(contentX, contentY + contentH - 1, contentW, 1), texCardBorder);
            GUI.DrawTexture(new Rect(contentX, contentY, 1, contentH), texCardBorder);
            GUI.DrawTexture(new Rect(contentX + contentW - 1, contentY, 1, contentH), texCardBorder);

            // Content Area Layout
            Rect areaRect = new Rect(contentX + 14, contentY + 14, contentW - 28, contentH - 28);
            GUILayout.BeginArea(areaRect);
            contentScroll = GUILayout.BeginScrollView(contentScroll, GUILayout.Width(contentW - 28), GUILayout.Height(contentH - 28));

            switch (currentTab)
            {
                case 0:
                    GUILayout.Label(T("CONFIGURAÇÕES DO JOGADOR", "PLAYER MODIFICATIONS"), styleHeader);
                    GUILayout.Space(12);

                    infiniteMoney = DrawModernToggle(T("Dinheiro Máximo ($999K Lock)", "Max Money ($999K Lock)"), infiniteMoney);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(T("+$50.000", "+$50K"), styleActionBtn, GUILayout.Height(28))) SetPlayerMoney(50000);
                    if (GUILayout.Button(T("+$250.000", "+$250K"), styleActionBtn, GUILayout.Height(28))) SetPlayerMoney(250000);
                    if (GUILayout.Button(T("+$1.000.000", "+$1M"), styleActionBtn, GUILayout.Height(28))) SetPlayerMoney(1000000);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);

                    untouchable = DrawModernToggle(T("Ignorar Inimigos (IA Passiva)", "Ignore Enemies (Passive AI)"), untouchable);
                    noDamage = DrawModernToggle(T("Modo Invencível (God Mode)", "Invulnerability (God Mode)"), noDamage);
                    unlimitedEnergy = DrawModernToggle(T("Estamina / Sprint Infinito", "Infinite Stamina / Sprint"), unlimitedEnergy);
                    invisibility = DrawModernToggle(T("Invisibilidade Total", "Full Invisibility"), invisibility);
                    brightMap = DrawModernToggle(T("Iluminação Total do Mapa (Fullbright)", "Fullbright / Clear Vision"), brightMap);

                    superGrab = DrawModernToggle(T("Pegada Forte & Estabilizada (Super Grab)", "Heavy Grab & Stable Physics"), superGrab);
                    if (superGrab)
                    {
                        grabForceMultiplier = DrawModernStepper(T("Potência da Pegada", "Grab Force Multiplier"), grabForceMultiplier, 0.5f, 0.5f, 5.0f, "0.0x");
                    }
                    GUILayout.Space(8);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(T("✚ Restaurar Vida (100%)", "✚ Full Heal (100%)"), styleActionBtn, GUILayout.Height(30)))
                    {
                        HealLocalPlayer();
                    }
                    if (GUILayout.Button(T("⛟ Retornar ao Caminhão", "⛟ Return to Truck"), styleActionBtn, GUILayout.Height(30)))
                    {
                        TeleportToSafety();
                    }
                    GUILayout.EndHorizontal();
                    break;

                case 1:
                    GUILayout.Label(T("FÍSICA & MOVIMENTAÇÃO", "PHYSICS & MOVEMENT"), styleHeader);
                    GUILayout.Space(12);

                    infiniteJumps = DrawModernToggle(T("Pulo Infinito (Air Jump)", "Infinite Air Jump"), infiniteJumps);
                    GUILayout.Space(8);

                    walkSpeedMultiplier = DrawModernStepper(T("Velocidade Andando", "Walk Speed Multiplier"), walkSpeedMultiplier, 0.5f, 0.5f, 10.0f, "0.0x");
                    sprintSpeedMultiplier = DrawModernStepper(T("Velocidade Correndo", "Sprint Speed Multiplier"), sprintSpeedMultiplier, 0.5f, 0.5f, 10.0f, "0.0x");
                    crouchSpeedMultiplier = DrawModernStepper(T("Velocidade Agachado", "Crouch Speed Multiplier"), crouchSpeedMultiplier, 0.5f, 0.5f, 10.0f, "0.0x");

                    GUILayout.Space(12);
                    if (GUILayout.Button(T("Restaurar Velocidades Padrão (1.0x)", "Reset Speeds to Default (1.0x)"), styleActionBtn, GUILayout.Height(30)))
                    {
                        walkSpeedMultiplier = 1.0f;
                        sprintSpeedMultiplier = 1.0f;
                        crouchSpeedMultiplier = 1.0f;
                    }
                    break;

                case 2:
                    GUILayout.Label(T("VISUALIZAÇÃO TÁTICA (ESP)", "TACTICAL VISUALS (ESP)"), styleHeader);
                    GUILayout.Space(10);

                    GUILayout.Label(T("• ITENS & RELÍQUIAS (VALUABLES):", "• LOOT & VALUABLES:"), styleLabelText);
                    GUILayout.Space(4);
                    espValuables = DrawModernToggle(T("ESP de Itens", "Valuables ESP"), espValuables);
                    if (espValuables)
                    {
                        espBoxValuables = DrawModernToggle(T("  └ Caixa Delimitadora (Box)", "  └ Bounding Box"), espBoxValuables);
                        espShowPrice = DrawModernToggle(T("  └ Exibir Valor em Dólar ($)", "  └ Show Cash Value"), espShowPrice);
                        espGoldBadgeHighValue = DrawModernToggle(T("  └ Destaque Dourado (> $3K)", "  └ Gold Highlight (> $3K)"), espGoldBadgeHighValue);
                        espOnlyGoldItems = DrawModernToggle(T("  └ Filtrar Apenas Itens Dourados", "  └ Gold Items Only"), espOnlyGoldItems);
                        espValuablesMinPrice = DrawModernStepper(T("  └ Preço Mínimo", "  └ Min Price Cutoff"), espValuablesMinPrice, 500f, 0f, 5000f, "$0");
                    }

                    GUILayout.Space(12);
                    GUILayout.Label(T("• ENTIDADES & MONSTROS:", "• ENTITIES & MONSTERS:"), styleLabelText);
                    GUILayout.Space(4);
                    espEnemies = DrawModernToggle(T("ESP de Monstros", "Monster ESP"), espEnemies);
                    if (espEnemies)
                    {
                        espBoxEnemies = DrawModernToggle(T("  └ Caixa Delimitadora (Box)", "  └ Bounding Box"), espBoxEnemies);
                        espShowEnemyName = DrawModernToggle(T("  └ Identificação / Nome", "  └ Entity Name"), espShowEnemyName);
                        espShowHealthBar = DrawModernToggle(T("  └ Barra de Vida Dinâmica", "  └ Dynamic Health Bar"), espShowHealthBar);
                    }

                    GUILayout.Space(12);
                    GUILayout.Label(T("• PONTO DE EXTRAÇÃO:", "• EXTRACTION ZONE:"), styleLabelText);
                    GUILayout.Space(4);
                    espExtraction = DrawModernToggle(T("ESP de Extração", "Extraction ESP"), espExtraction);
                    if (espExtraction)
                    {
                        espExtractionOnlyLocked = DrawModernToggle(T("  └ Apenas Bloqueados / Fechados", "  └ Locked Only"), espExtractionOnlyLocked);
                        espExtractionOnlyIncomplete = DrawModernToggle(T("  └ Ocultar após Meta Atingida", "  └ Hide Once Goal Reached"), espExtractionOnlyIncomplete);
                    }

                    GUILayout.Space(12);
                    GUILayout.Label(T("• CONFIGURAÇÃO GERAL:", "• GENERAL CONFIG:"), styleLabelText);
                    GUILayout.Space(4);
                    espShowDistance = DrawModernToggle(T("Exibir Distância em Metros [m]", "Show Distance Indicator [m]"), espShowDistance);
                    espMaxDistance = DrawModernStepper(T("Alcance Máximo", "Max Distance Range"), espMaxDistance, 25f, 25f, 300f, "0m");
                    break;

                case 3:
                    GUILayout.Label(T("GERADOR DE EQUIPAMENTOS & ORBES", "EQUIPMENT & SCRAP SPAWNER"), styleHeader);
                    GUILayout.Space(10);

                    GUILayout.Label(T("• Orbes de Sucata (Valor Extra):", "• Scrap Orbs (Cash Value):"), styleLabelText);
                    GUILayout.Space(4);
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(T("🟣 Orbe Pequena", "🟣 Small Orb"), styleActionBtn, GUILayout.Height(28))) SpawnEnemyValuable(0);
                    if (GUILayout.Button(T("🟣 Orbe Média", "🟣 Medium Orb"), styleActionBtn, GUILayout.Height(28))) SpawnEnemyValuable(1);
                    if (GUILayout.Button(T("🟣 Orbe Grande", "🟣 Big Orb"), styleActionBtn, GUILayout.Height(28))) SpawnEnemyValuable(2);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(12);
                    GUILayout.Label(T("• Armas & Equipamentos de Suporte:", "• Weapons & Support Equipment:"), styleLabelText);
                    GUILayout.Space(4);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🔫 " + T("Rifle Laser", "Laser Rifle"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Gun Laser");
                    if (GUILayout.Button("🔫 " + T("Pistola Balística", "Ballistic Pistol"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Gun Bullet");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("💉 " + T("Tranquilizante", "Tranq Pistol"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Gun Tranq");
                    if (GUILayout.Button("⚡ " + T("Bastão de Choque", "Stun Baton"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Stun Baton");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🔨 " + T("Martelo de Impacto", "Impact Hammer"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Melee Inflatable Hammer");
                    if (GUILayout.Button("💣 " + T("Granada Fragmentação", "Frag Grenade"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Grenade Explosive");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("💥 " + T("Granada Repulsora", "Shockwave Grenade"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Grenade Shockwave");
                    if (GUILayout.Button("⚡ " + T("Granada Choque", "Stun Grenade"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Grenade Stun");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("💣 " + T("Mina Explosiva", "Explosive Mine"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Mine Explosive");
                    if (GUILayout.Button("⚡ " + T("Mina de Choque", "Shock Mine"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Mine Stun");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🚀 " + T("Canhão de Carrinho", "Cart Cannon"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Cart Cannon");
                    if (GUILayout.Button("🔮 " + T("Laser de Carrinho", "Cart Laser"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Cart Laser");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🌀 " + T("Cajado de Vácuo", "Void Staff"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Staff Void");
                    if (GUILayout.Button("💫 " + T("Cajado Gravitacional", "Anti-Gravity Staff"), styleActionBtn, GUILayout.Height(28))) SpawnGameItem("Item Staff Zero Gravity");
                    GUILayout.EndHorizontal();
                    break;

                case 4:
                    GUILayout.Label(T("COSMÉTICOS & SKINS", "COSMETICS & SKINS"), styleHeader);
                    GUILayout.Space(14);

                    if (GUILayout.Button(T("★ Liberar Todo o Guarda-Roupa (Permanente)", "★ Unlock Full Wardrobe (Permanent)"), styleActionBtn, GUILayout.Height(36)))
                    {
                        ForceUnlockCosmetics();
                    }

                    GUILayout.Space(12);
                    GUI.color = new Color(0.65f, 0.70f, 0.80f);
                    GUILayout.Label(T("Desbloqueia e salva permanentemente no seu perfil todos os chapéus, trajes, cores e partes cosméticas sem alterar seu progresso salvo.", "Permanently unlocks and saves all hats, suits, colors and cosmetic items directly to your profile without affecting normal progression."));
                    GUI.color = Color.white;
                    break;

                case 5:
                    GUILayout.Label(T("SESSÃO & LOJA", "LOBBY & SHOP AUTOMATION"), styleHeader);
                    GUILayout.Space(12);

                    unlockShopItems = DrawModernToggle(T("Liberar Todos os Itens da Loja", "Unlock Full Shop Catalog"), unlockShopItems);
                    freeShop = DrawModernToggle(T("Loja 100% Gratuita (Custo $0)", "Free Store Purchases ($0 Cost)"), freeShop);
                    freeExtraction = DrawModernToggle(T("Extração Imediata (Meta Concluída)", "Instant Extraction (Zero Goal)"), freeExtraction);
                    unbreakableItems = DrawModernToggle(T("Itens Inquebráveis (Anti-Impacto)", "Unbreakable Items (Fall Damage Off)"), unbreakableItems);
                    infiniteStationCharge = DrawModernToggle(T("Gerador de Energia Infinito (100%)", "Perpetual Power Station (100%)"), infiniteStationCharge);
                    infiniteItemBattery = DrawModernToggle(T("Bateria Eterna em Equipamentos", "Infinite Battery On All Gear"), infiniteItemBattery);
                    autoChargeHeldItem = DrawModernToggle(T("Recarga Automática no Inventário", "Auto-Recharge Held Equipment"), autoChargeHeldItem);
                    break;

                case 6:
                    GUILayout.Label(T("SISTEMA & CONFIGURAÇÕES", "SYSTEM CONFIGURATION"), styleHeader);
                    GUILayout.Space(14);

                    GUILayout.Label(T("Idioma / Language", "Interface Language"), styleLabelText);
                    GUILayout.Space(4);
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(langIndex == 0 ? "[ Português ]" : "Português", styleActionBtn, GUILayout.Height(28))) langIndex = 0;
                    if (GUILayout.Button(langIndex == 1 ? "[ English ]" : "English", styleActionBtn, GUILayout.Height(28))) langIndex = 1;
                    GUILayout.EndHorizontal();

                    GUILayout.Space(14);
                    if (GUILayout.Button(T("Centralizar Janela na Tela", "Reset Window to Center"), styleActionBtn, GUILayout.Height(30)))
                    {
                        winRect.x = (Screen.width - winRect.width) / 2f;
                        winRect.y = (Screen.height - winRect.height) / 2f;
                    }

                    GUILayout.Space(10);
                    if (GUILayout.Button(T("Descarregar Cheat / Ejetar DLL", "Unload & Eject Menu"), styleCloseBtn, GUILayout.Height(30)))
                    {
                        Loader.Unload();
                    }

                    GUILayout.Space(16);
                    GUILayout.Label(T("CRÉDITOS & SUPORTE", "CREDITS & SUPPORT"), styleHeader);
                    GUILayout.Space(4);
                    GUILayout.Label("PatchZone Modern Framework v2.0", styleLabelText);
                    GUILayout.Label("Desenvolvido por: Kayke", styleLabelText);
                    GUILayout.Label("Discord Oficial: kayke_v1", styleLabelText);
                    GUILayout.Space(8);

                    if (GUILayout.Button("▶ Acessar Canal do YouTube", styleActionBtn, GUILayout.Height(32)))
                    {
                        Application.OpenURL("https://www.youtube.com/@PatchZoneBR");
                    }
                    break;
            }

            GUILayout.EndScrollView();
            GUILayout.EndArea();

            // Window Dragging by top header
            GUI.DragWindow(new Rect(0, 0, winRect.width, headerHeight));
        }

    }
}
