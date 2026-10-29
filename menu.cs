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
        public bool freeShop = false;
        public bool freeExtraction = false;

        public bool espValuables = false;
        public bool espEnemies = true;
        public bool espExtraction = true;
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

        private Texture2D texBackground;
        private Texture2D texSidebar;
        private Texture2D texCard;
        private Texture2D texAccent;
        private Texture2D texWhite;
        private Texture2D texDarkBadge;

        private GUIStyle espLabelNormal;
        private GUIStyle espLabelGold;
        private GUIStyle espEnemyStyle;
        private GUIStyle espHpStyle;
        private GUIStyle espExtractionStyle;
        private bool texturesInitialized = false;
        private Vector2 itemSpawnScroll = Vector2.zero;
        private Vector2 espScroll = Vector2.zero;

        private float periodicHackTimer = 0f;

        private static readonly string[][] Tabs = new string[][]
        {
            new string[] { "Jogador", "Física", "Radar ESP", "Equipamentos", "Cosméticos", "Sessão & Loja", "Sistema" },
            new string[] { "Player", "Physics", "ESP Radar", "Loadout", "Cosmetics", "Lobby & Shop", "System" }
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

        private void Awake()
        {
            float w = 640f;
            float h = 580f;
            winRect = new Rect((Screen.width - w) / 2f, (Screen.height - h) / 2f, w, h);

            InitReflection();
            UnityEngine.Object.DontDestroyOnLoad(gameObject);
        }

        private static void InitReflection()
        {
            if (cursorType != null && metaManagerType != null && valuableDirectorType != null && extractionPointType != null) return;

            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
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
            }
        }

        private object GetLocalPlayerAvatar()
        {
            try
            {
                InitReflection();
                if (playerAvatarType != null && playerAvatarInstFld != null)
                {
                    Component av = playerAvatarInstFld.GetValue(null) as Component;
                    if (av != null) return av;
                }
                if (photonPlayerControllerType != null)
                {
                    FieldInfo instFld = photonPlayerControllerType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                    object pc = (instFld != null) ? instFld.GetValue(null) : null;
                    if (pc != null)
                    {
                        FieldInfo avFld = photonPlayerControllerType.GetField("playerAvatarScript", BindingFlags.Public | BindingFlags.Instance);
                        if (avFld != null)
                        {
                            Component av = avFld.GetValue(pc) as Component;
                            if (av != null) return av;
                        }
                    }
                }
                if (playerAvatarType != null)
                {
                    Component av = UnityEngine.Object.FindObjectOfType(playerAvatarType) as Component;
                    if (av != null) return av;
                }
            }
            catch { }
            return null;
        }

        public void SetPlayerMoney(int amount)
        {
            try
            {
                InitReflection();
                if (sfStatSetRunCurrency != null)
                {
                    sfStatSetRunCurrency.Invoke(null, new object[] { amount });
                }

                Type statsMgrType = Type.GetType("StatsManager, Assembly-CSharp");
                if (statsMgrType != null)
                {
                    FieldInfo instFld = statsMgrType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                    object sm = (instFld != null) ? instFld.GetValue(null) : null;
                    if (sm != null)
                    {
                        FieldInfo runStatsFld = statsMgrType.GetField("runStats", BindingFlags.Public | BindingFlags.Instance);
                        IDictionary dict = (runStatsFld != null) ? runStatsFld.GetValue(sm) as IDictionary : null;
                        if (dict != null)
                        {
                            dict["currency"] = amount;
                        }
                    }
                }

                Type currencyUiType = Type.GetType("CurrencyUI, Assembly-CSharp");
                if (currencyUiType != null)
                {
                    FieldInfo cuiInst = currencyUiType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                    object cui = (cuiInst != null) ? cuiInst.GetValue(null) : null;
                    if (cui != null)
                    {
                        MethodInfo fetch = currencyUiType.GetMethod("FetchCurrency", BindingFlags.Public | BindingFlags.Instance);
                        if (fetch != null) fetch.Invoke(cui, null);
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

            ApplyHacks();
        }

        private void ApplyHacks()
        {
            try
            {
                InitReflection();

                if (infiniteMoney)
                {
                    SetPlayerMoney(999999);
                }

                if (unlockShopItems)
                {
                    if (itemDeactivatedUntilLevelType != null)
                    {
                        UnityEngine.Object[] deacts = UnityEngine.Object.FindObjectsOfType(itemDeactivatedUntilLevelType);
                        if (deacts != null)
                        {
                            for (int i = 0; i < deacts.Length; i++)
                            {
                                Component c = deacts[i] as Component;
                                if (c == null) continue;
                                if (levelToActivateFld != null) levelToActivateFld.SetValue(c, 0);
                                if (c.gameObject != null && !c.gameObject.activeSelf)
                                {
                                    c.gameObject.SetActive(true);
                                }
                            }
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
                        if (rd != null)
                        {
                            if (rdHaulGoalFld != null && rdCurrentHaulFld != null)
                            {
                                int curHaul = Convert.ToInt32(rdCurrentHaulFld.GetValue(rd));
                                rdHaulGoalFld.SetValue(rd, curHaul);
                            }
                        }
                    }

                    if (extractionPointType != null)
                    {
                        UnityEngine.Object[] points = UnityEngine.Object.FindObjectsOfType(extractionPointType);
                        if (points != null)
                        {
                            for (int i = 0; i < points.Length; i++)
                            {
                                Component ep = points[i] as Component;
                                if (ep == null) continue;
                                if (epHaulGoalFld != null) epHaulGoalFld.SetValue(ep, 0);
                                if (epIsLockedFld != null) epIsLockedFld.SetValue(ep, false);
                                if (epUpdateLockMethod != null)
                                {
                                    try { epUpdateLockMethod.Invoke(ep, new object[] { false }); } catch { }
                                }
                            }
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

                    if (physGrabberFld != null)
                    {
                        object grabber = physGrabberFld.GetValue(playerAvatar);
                        if (grabber != null && superGrab)
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

                if (noDamage && playerHealthType != null)
                {
                    UnityEngine.Object[] allHealths = UnityEngine.Object.FindObjectsOfType(playerHealthType);
                    if (allHealths != null)
                    {
                        for (int h = 0; h < allHealths.Length; h++)
                        {
                            Component ph = allHealths[h] as Component;
                            if (ph == null) continue;
                            if (healthGodModeFld != null) healthGodModeFld.SetValue(ph, true);
                            if (invincibleTimerFld != null) invincibleTimerFld.SetValue(ph, 10f);
                            if (healthFld != null) healthFld.SetValue(ph, 100);
                            if (maxHealthFld != null) maxHealthFld.SetValue(ph, 100);
                        }
                    }
                }

                if (enemyDirectorType != null && enemyDirectorInstanceFld != null)
                {
                    object ed = enemyDirectorInstanceFld.GetValue(null);
                    if (ed != null)
                    {
                        FieldInfo nvFld = enemyDirectorType.GetField("debugNoVision", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (nvFld != null)
                        {
                            IList nvList = nvFld.GetValue(ed) as IList;
                            if (nvList != null)
                            {
                                string sid = "local";
                                if (playerAvatar != null)
                                {
                                    FieldInfo sidFld = playerAvatarType.GetField("steamID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                                    if (sidFld != null)
                                    {
                                        object sidVal = sidFld.GetValue(playerAvatar);
                                        if (sidVal != null) sid = sidVal.ToString();
                                    }
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
                }

                Type dchType = Type.GetType("DebugCommandHandler, Assembly-CSharp");
                if (dchType != null)
                {
                    FieldInfo dchInstFld = dchType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                    object dch = (dchInstFld != null) ? dchInstFld.GetValue(null) : null;
                    if (dch != null)
                    {
                        FieldInfo envFld = dchType.GetField("enemyNoVision", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        if (envFld != null)
                        {
                            IList envList = envFld.GetValue(dch) as IList;
                            if (envList != null)
                            {
                                string sid = "local";
                                if (playerAvatar != null)
                                {
                                    FieldInfo sidFld = playerAvatarType.GetField("steamID", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                                    if (sidFld != null)
                                    {
                                        object sidVal = sidFld.GetValue(playerAvatar);
                                        if (sidVal != null) sid = sidVal.ToString();
                                    }
                                }
                                if (untouchable)
                                {
                                    if (!envList.Contains(sid)) envList.Add(sid);
                                }
                                else
                                {
                                    while (envList.Contains(sid)) envList.Remove(sid);
                                }
                            }
                        }
                    }
                }

                if (tr != null)
                {
                    if (invisibility)
                    {
                        Renderer[] renderers = tr.GetComponentsInChildren<Renderer>();
                        if (renderers != null)
                        {
                            for (int i = 0; i < renderers.Length; i++)
                            {
                                if (renderers[i] != null) renderers[i].enabled = false;
                            }
                        }
                    }

                    if (infiniteItemBattery || autoChargeHeldItem)
                    {
                        if (itemBatteryType != null)
                        {
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

                    if (infiniteJumps && Input.GetKeyDown(KeyCode.Space))
                    {
                        Component rb = tr.GetComponent("Rigidbody");
                        if (rb != null)
                        {
                            PropertyInfo velProp = rb.GetType().GetProperty("velocity");
                            if (velProp != null)
                            {
                                Vector3 cur = (Vector3)velProp.GetValue(rb, null);
                                velProp.SetValue(rb, new Vector3(cur.x, 7.5f, cur.z), null);
                            }
                        }
                        else
                        {
                            tr.position += Vector3.up * 1.5f;
                        }
                    }
                }

                if (photonPlayerControllerType != null)
                {
                    FieldInfo instFld = photonPlayerControllerType.GetField("instance", BindingFlags.Public | BindingFlags.Static);
                    object pc = (instFld != null) ? instFld.GetValue(null) : null;
                    if (pc != null)
                    {
                        if (unlimitedEnergy)
                        {
                            FieldInfo energyCur = photonPlayerControllerType.GetField("EnergyCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            FieldInfo energyStart = photonPlayerControllerType.GetField("EnergyStart", BindingFlags.Public | BindingFlags.Instance);
                            float startVal = (energyStart != null) ? (float)energyStart.GetValue(pc) : 100f;
                            if (energyCur != null) energyCur.SetValue(pc, startVal);

                            FieldInfo debugEnergyFld = photonPlayerControllerType.GetField("DebugEnergy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            if (debugEnergyFld != null) debugEnergyFld.SetValue(pc, true);
                        }

                        if (infiniteJumps)
                        {
                            FieldInfo jumpCd = photonPlayerControllerType.GetField("JumpCooldown", BindingFlags.NonPublic | BindingFlags.Instance);
                            if (jumpCd != null) jumpCd.SetValue(pc, 0f);

                            FieldInfo jumpBuffer = photonPlayerControllerType.GetField("JumpGroundedBuffer", BindingFlags.NonPublic | BindingFlags.Instance);
                            if (jumpBuffer != null) jumpBuffer.SetValue(pc, 1f);

                            FieldInfo jumpExtra = photonPlayerControllerType.GetField("JumpExtraCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            if (jumpExtra != null) jumpExtra.SetValue(pc, 99);
                        }

                        bool isSpeedModified = Mathf.Abs(walkSpeedMultiplier - 1.0f) > 0.05f ||
                                               Mathf.Abs(sprintSpeedMultiplier - 1.0f) > 0.05f ||
                                               Mathf.Abs(crouchSpeedMultiplier - 1.0f) > 0.05f;

                        if (isSpeedModified)
                        {
                            float baseMove = (pcOriginalMoveSpeedFld != null) ? Convert.ToSingle(pcOriginalMoveSpeedFld.GetValue(pc)) : 0.5f;
                            float baseSprint = (pcOriginalSprintSpeedFld != null) ? Convert.ToSingle(pcOriginalSprintSpeedFld.GetValue(pc)) : 1.0f;
                            float baseCrouch = (pcOriginalCrouchSpeedFld != null) ? Convert.ToSingle(pcOriginalCrouchSpeedFld.GetValue(pc)) : 1.0f;

                            if (baseMove < 0.01f) baseMove = 0.5f;
                            if (baseSprint < 0.01f) baseSprint = 1.0f;
                            if (baseCrouch < 0.01f) baseCrouch = 1.0f;

                            FieldInfo moveSpeedFld = photonPlayerControllerType.GetField("MoveSpeed", BindingFlags.Public | BindingFlags.Instance);
                            if (moveSpeedFld != null)
                            {
                                moveSpeedFld.SetValue(pc, baseMove * walkSpeedMultiplier);
                            }

                            FieldInfo sprintSpeedFld = photonPlayerControllerType.GetField("SprintSpeed", BindingFlags.Public | BindingFlags.Instance);
                            if (sprintSpeedFld != null)
                            {
                                sprintSpeedFld.SetValue(pc, baseSprint * sprintSpeedMultiplier);
                            }

                            FieldInfo crouchSpeedFld = photonPlayerControllerType.GetField("CrouchSpeed", BindingFlags.Public | BindingFlags.Instance);
                            if (crouchSpeedFld != null)
                            {
                                crouchSpeedFld.SetValue(pc, baseCrouch * crouchSpeedMultiplier);
                            }
                        }
                        else
                        {
                            if (pcOriginalMoveSpeedFld != null)
                            {
                                float origMove = Convert.ToSingle(pcOriginalMoveSpeedFld.GetValue(pc));
                                if (origMove > 0.01f)
                                {
                                    FieldInfo moveSpeedFld = photonPlayerControllerType.GetField("MoveSpeed", BindingFlags.Public | BindingFlags.Instance);
                                    if (moveSpeedFld != null) moveSpeedFld.SetValue(pc, origMove);
                                }
                            }

                            if (pcOriginalSprintSpeedFld != null)
                            {
                                float origSprint = Convert.ToSingle(pcOriginalSprintSpeedFld.GetValue(pc));
                                if (origSprint > 0.01f)
                                {
                                    FieldInfo sprintSpeedFld = photonPlayerControllerType.GetField("SprintSpeed", BindingFlags.Public | BindingFlags.Instance);
                                    if (sprintSpeedFld != null) sprintSpeedFld.SetValue(pc, origSprint);
                                }
                            }

                            if (pcOriginalCrouchSpeedFld != null)
                            {
                                float origCrouch = Convert.ToSingle(pcOriginalCrouchSpeedFld.GetValue(pc));
                                if (origCrouch > 0.01f)
                                {
                                    FieldInfo crouchSpeedFld = photonPlayerControllerType.GetField("CrouchSpeed", BindingFlags.Public | BindingFlags.Instance);
                                    if (crouchSpeedFld != null) crouchSpeedFld.SetValue(pc, origCrouch);
                                }
                            }
                        }
                    }
                }

                if (unlimitedEnergy && controllerInst != null)
                {
                    object ctrl = controllerInst.GetValue(null, null);
                    if (ctrl != null)
                    {
                        FieldInfo bs = ctrl.GetType().GetField("debugBlockSprint", BindingFlags.Public | BindingFlags.Instance);
                        if (bs != null) bs.SetValue(ctrl, false);
                    }
                }

                if (infiniteStationCharge && chargingStationType != null)
                {
                    object csInst = null;
                    if (chargingStationInstFld != null) csInst = chargingStationInstFld.GetValue(null);
                    if (csInst == null) csInst = UnityEngine.Object.FindObjectOfType(chargingStationType);
                    if (csInst != null)
                    {
                        if (chargeTotalFld != null) chargeTotalFld.SetValue(csInst, 100);
                        if (chargeFloatFld != null) chargeFloatFld.SetValue(csInst, 1f);
                        if (chargeIntFld != null) chargeIntFld.SetValue(csInst, 10);
                        if (chargeSegmentCurrentFld != null) chargeSegmentCurrentFld.SetValue(csInst, 40);
                        if (chargeScaleTargetFld != null) chargeScaleTargetFld.SetValue(csInst, 1f);
                    }
                }

                periodicHackTimer += Time.deltaTime;
                if (periodicHackTimer >= 1.0f || (prevUnbreakableItems != unbreakableItems))
                {
                    periodicHackTimer = 0f;
                    prevUnbreakableItems = unbreakableItems;

                    if (impactDetectorType != null)
                    {
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

                    if (infiniteItemBattery && itemBatteryType != null)
                    {
                        UnityEngine.Object[] allBatteries = UnityEngine.Object.FindObjectsOfType(itemBatteryType);
                        if (allBatteries != null)
                        {
                            for (int i = 0; i < allBatteries.Length; i++)
                            {
                                Component ib = allBatteries[i] as Component;
                                if (ib == null) continue;
                                if (batteryBarsFld != null && batteryLifeFld != null && batteryLifeIntFld != null)
                                {
                                    int bars = Convert.ToInt32(batteryBarsFld.GetValue(ib));
                                    float life = Convert.ToSingle(batteryLifeFld.GetValue(ib));
                                    if (life < 99.9f)
                                    {
                                        batteryLifeFld.SetValue(ib, 100f);
                                        batteryLifeIntFld.SetValue(ib, bars);
                                    }
                                    if (drainRateFld != null) drainRateFld.SetValue(ib, 0f);
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void InitTextures()
        {
            if (texturesInitialized) return;

            texBackground = MakeTex(2, 2, new Color(0.10f, 0.11f, 0.14f, 0.98f));
            texSidebar = MakeTex(2, 2, new Color(0.06f, 0.07f, 0.09f, 1f));
            texCard = MakeTex(2, 2, new Color(0.14f, 0.16f, 0.20f, 0.95f));
            texAccent = MakeTex(2, 2, new Color(0.25f, 0.45f, 0.95f, 1f));
            texWhite = MakeTex(2, 2, Color.white);
            texDarkBadge = MakeTex(2, 2, new Color(0.05f, 0.06f, 0.08f, 0.90f));

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

            if (!isOpen) return;

            GUI.depth = -100;
            GUIStyle emptyWindow = new GUIStyle();
            winRect = GUI.Window(99991, winRect, DrawMenuContent, "", emptyWindow);
        }

        private Camera GetActiveCamera()
        {
            if (cameraUtilsType != null && cameraUtilsInstanceFld != null && cameraUtilsMainCameraFld != null)
            {
                object inst = cameraUtilsInstanceFld.GetValue(null);
                if (inst != null)
                {
                    Camera c = cameraUtilsMainCameraFld.GetValue(inst) as Camera;
                    if (c != null && c.isActiveAndEnabled) return c;
                }
            }

            Camera cam = Camera.main;
            if (cam != null && cam.isActiveAndEnabled) return cam;

            foreach (Camera c in Camera.allCameras)
            {
                if (c != null && c.isActiveAndEnabled)
                {
                    if (c.CompareTag("MainCamera") || c.name.ToLower().Contains("player") || c.name.ToLower().Contains("main"))
                    {
                        return c;
                    }
                }
            }

            if (Camera.current != null && Camera.current.isActiveAndEnabled) return Camera.current;

            foreach (Camera c in Camera.allCameras)
            {
                if (c != null && c.isActiveAndEnabled) return c;
            }

            return null;
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

                    Vector3 diff = centerPos - camPos;
                    if (Vector3.Dot(camFwd, diff) <= 0.15f) continue;

                    float dist = diff.magnitude;
                    if (dist > espMaxDistance || dist < 0.2f) continue;

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

                    string rawName = item.gameObject.name;
                    rawName = rawName.Replace("(Clone)", "").Replace("Valuable", "").Replace("Wizard", "").Replace("Arctic", "").Replace("Surplus", "").Trim();
                    if (string.IsNullOrEmpty(rawName)) rawName = "Item";

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

                    Type epType = ep.GetType();
                    FieldInfo enemyFld = epType.GetField("Enemy", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    FieldInfo spawnedFld = epType.GetField("Spawned", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    if (spawnedFld != null)
                    {
                        object sp = spawnedFld.GetValue(ep);
                        if (sp is bool && !((bool)sp)) continue;
                    }

                    Component enemy = (enemyFld != null) ? enemyFld.GetValue(ep) as Component : ep.GetComponentInChildren<MonoBehaviour>();
                    if (enemy == null || enemy.gameObject == null || !enemy.gameObject.activeInHierarchy) continue;

                    Vector3 centerPos = enemy.transform.position;
                    Type enType = enemy.GetType();
                    FieldInfo centerFld = enType.GetField("CenterTransform", BindingFlags.Public | BindingFlags.Instance);
                    if (centerFld != null)
                    {
                        Transform ct = centerFld.GetValue(enemy) as Transform;
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
                    FieldInfo nameFld = epType.GetField("enemyName", BindingFlags.Public | BindingFlags.Instance);
                    if (nameFld != null)
                    {
                        object nVal = nameFld.GetValue(ep);
                        if (nVal != null && !string.IsNullOrEmpty(nVal.ToString())) eName = nVal.ToString();
                    }
                    else
                    {
                        eName = enemy.gameObject.name.Replace("(Clone)", "").Trim();
                    }

                    int curHp = 100;
                    int maxHp = 100;
                    bool hasHealth = false;

                    FieldInfo hpFld = enType.GetField("Health", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (hpFld != null)
                    {
                        Component eh = hpFld.GetValue(enemy) as Component;
                        if (eh != null)
                        {
                            FieldInfo curHpFld = eh.GetType().GetField("healthCurrent", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            FieldInfo maxHpFld = eh.GetType().GetField("health", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                            if (curHpFld != null)
                            {
                                curHp = Convert.ToInt32(curHpFld.GetValue(eh));
                                hasHealth = true;
                            }
                            if (maxHpFld != null)
                            {
                                maxHp = Mathf.Max(1, Convert.ToInt32(maxHpFld.GetValue(eh)));
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
                InitReflection();

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

                List<Vector3> pointPositions = new List<Vector3>();
                List<string> pointLabels = new List<string>();
                List<Color> pointColors = new List<Color>();

                if (extractionPointType != null)
                {
                    UnityEngine.Object[] points = UnityEngine.Object.FindObjectsOfType(extractionPointType);
                    if (points != null)
                    {
                        for (int i = 0; i < points.Length; i++)
                        {
                            Component ep = points[i] as Component;
                            if (ep == null || ep.gameObject == null || !ep.gameObject.activeInHierarchy) continue;

                            bool isShop = false;
                            if (epIsShopFld != null)
                            {
                                object isShopVal = epIsShopFld.GetValue(ep);
                                if (isShopVal is bool) isShop = (bool)isShopVal;
                            }
                            if (!isShop && epShopStationFld != null)
                            {
                                object ss = epShopStationFld.GetValue(ep);
                                if (ss != null) isShop = true;
                            }
                            if (isShop) continue;

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

                            string labelText = "⛟ EXTRAÇÃO";
                            if (isComplete) labelText += " [CONCLUÍDO]";
                            else if (isDoorLocked) labelText += " [BLOQUEADO]";
                            else if (quota <= 0) labelText += " [PRONTO]";
                            else labelText += " [$" + quota.ToString("N0") + "]";

                            Color pointColor = isComplete ? new Color(0.5f, 0.5f, 0.5f, 0.8f) :
                                               (isDoorLocked ? new Color(1.0f, 0.25f, 0.25f, 0.9f) :
                                               (quota <= 0 ? new Color(0.2f, 1.0f, 0.45f, 0.9f) : new Color(0.2f, 0.85f, 1.0f, 0.85f)));

                            pointPositions.Add(ep.transform.position + Vector3.up * 1.0f);
                            pointLabels.Add(labelText);
                            pointColors.Add(pointColor);
                        }
                    }
                }

                Type truckDoorType = Type.GetType("TruckDoor, Assembly-CSharp");
                if (truckDoorType != null)
                {
                    UnityEngine.Object[] doors = UnityEngine.Object.FindObjectsOfType(truckDoorType);
                    if (doors != null)
                    {
                        for (int d = 0; d < doors.Length; d++)
                        {
                            Component td = doors[d] as Component;
                            if (td == null || td.gameObject == null || !td.gameObject.activeInHierarchy) continue;

                            Vector3 doorPos = td.transform.position;
                            Transform doorMeshTr = td.transform.Find("Mesh");
                            if (doorMeshTr != null) doorPos = doorMeshTr.position;

                            bool alreadyHasNear = false;
                            for (int p = 0; p < pointPositions.Count; p++)
                            {
                                if (Vector3.Distance(doorPos, pointPositions[p]) < 4f)
                                {
                                    alreadyHasNear = true;
                                    break;
                                }
                            }

                            if (!alreadyHasNear)
                            {
                                pointPositions.Add(doorPos + Vector3.up * 0.5f);
                                pointLabels.Add("⛟ PORTA CAMINHÃO / SAÍDA");
                                pointColors.Add(new Color(0.2f, 0.85f, 1.0f, 0.9f));
                            }
                        }
                    }
                }

                for (int i = 0; i < pointPositions.Count; i++)
                {
                    Vector3 pos = pointPositions[i];
                    Vector3 diff = pos - camPos;
                    if (Vector3.Dot(camFwd, diff) <= 0.15f) continue;

                    float dist = diff.magnitude;
                    if (dist > espMaxDistance || dist < 0.2f) continue;

                    Vector3 screenPos = cam.WorldToScreenPoint(pos);
                    if (screenPos.z <= 0.2f) continue;

                    float screenX = screenPos.x * scaleX;
                    float screenY = (float)Screen.height - (screenPos.y * scaleY);

                    string labelText = pointLabels[i];
                    if (espShowDistance)
                    {
                        labelText += " • " + dist.ToString("0") + "m";
                    }

                    Color pointColor = pointColors[i];
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

        private void DrawMenuContent(int id)
        {
            float sideWidth = 156f;

            GUI.DrawTexture(new Rect(0, 0, winRect.width, winRect.height), texBackground);
            GUI.DrawTexture(new Rect(0, 0, sideWidth, winRect.height), texSidebar);
            GUI.DrawTexture(new Rect(0, 0, winRect.width, 3), texAccent);

            GUI.color = new Color(0.25f, 0.55f, 1f);
            GUI.Label(new Rect(18, 16, 120, 22), "PATCHZONE");
            GUI.color = new Color(0.5f, 0.55f, 0.65f);
            GUI.Label(new Rect(18, 34, 120, 18), "INTERNAL");

            float creditY = winRect.height - 48f;
            GUI.color = new Color(0.45f, 0.65f, 0.85f, 0.9f);
            GUI.Label(new Rect(16, creditY, sideWidth - 24, 18), "Feito por Kayke");
            GUI.color = new Color(0.35f, 0.42f, 0.5f, 0.85f);
            GUI.Label(new Rect(16, creditY + 16, sideWidth - 24, 18), "@PatchZoneBR");

            string[] tabs = Tabs[langIndex];
            float tabY = 72f;
            for (int i = 0; i < tabs.Length; i++)
            {
                Rect rTab = new Rect(10, tabY, sideWidth - 20, 32);
                bool isActive = (currentTab == i);

                GUI.color = isActive ? new Color(0.4f, 0.65f, 1.0f) : Color.white;
                if (GUI.Button(rTab, (isActive ? "► " : "  ") + tabs[i]))
                {
                    currentTab = i;
                }
                tabY += 36;
            }

            GUI.color = Color.white;
            if (GUI.Button(new Rect(winRect.width - 32, 10, 22, 22), "X"))
            {
                ToggleMenu(false);
            }

            float contentX = sideWidth + 20f;
            float contentY = 24f;
            float contentW = winRect.width - sideWidth - 40f;
            float contentH = winRect.height - 48f;

            GUI.DrawTexture(new Rect(contentX - 8, contentY + 36, contentW + 16, contentH - 40), texCard);

            Rect contentArea = new Rect(contentX, contentY, contentW, contentH);
            GUILayout.BeginArea(contentArea);

            GUI.color = Color.white;

            switch (currentTab)
            {
                case 0:
                    GUILayout.Label(T("JOGADOR", "PLAYER"));
                    GUILayout.Space(14);

                    infiniteMoney = GUILayout.Toggle(infiniteMoney, T(" Dinheiro Máximo ($999K Constante)", " Max Money ($999K Lock)"));
                    GUILayout.Space(6);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(T("+$50.000", "+$50K"), GUILayout.Height(26))) SetPlayerMoney(50000);
                    if (GUILayout.Button(T("+$250.000", "+$250K"), GUILayout.Height(26))) SetPlayerMoney(250000);
                    if (GUILayout.Button(T("+$1.000.000", "+$1M"), GUILayout.Height(26))) SetPlayerMoney(1000000);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(10);

                    untouchable = GUILayout.Toggle(untouchable, T(" Ignorar Inimigos (IA Passiva)", " Ignore Enemies (Passive AI)"));
                    GUILayout.Space(6);

                    noDamage = GUILayout.Toggle(noDamage, T(" Modo Invencível (God Mode)", " Invulnerability (God Mode)"));
                    GUILayout.Space(6);

                    unlimitedEnergy = GUILayout.Toggle(unlimitedEnergy, T(" Estamina / Sprint Infinito", " Infinite Stamina / Sprint"));
                    GUILayout.Space(6);

                    invisibility = GUILayout.Toggle(invisibility, T(" Invisibilidade Total", " Full Invisibility"));
                    GUILayout.Space(6);

                    superGrab = GUILayout.Toggle(superGrab, T(" Pegada Forte & Estabilizada", " Heavy Grab & Stable Physics"));
                    GUILayout.Space(4);

                    if (superGrab)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label(T("Potência: ", "Power: ") + grabForceMultiplier.ToString("0.0") + "x", GUILayout.Width(170));
                        if (GUILayout.Button("-", GUILayout.Width(28), GUILayout.Height(22))) grabForceMultiplier = Mathf.Max(0.5f, grabForceMultiplier - 0.5f);
                        if (GUILayout.Button("+", GUILayout.Width(28), GUILayout.Height(22))) grabForceMultiplier = Mathf.Min(5.0f, grabForceMultiplier + 0.5f);
                        GUILayout.EndHorizontal();
                    }
                    GUILayout.Space(10);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(T("✚ Restaurar Vida (100%)", "✚ Full Heal (100%)"), GUILayout.Height(28)))
                    {
                        HealLocalPlayer();
                    }
                    if (GUILayout.Button(T("⛟ Retornar ao Caminhão", "⛟ Return to Truck"), GUILayout.Height(28)))
                    {
                        TeleportToSafety();
                    }
                    GUILayout.EndHorizontal();
                    break;

                case 1:
                    GUILayout.Label(T("FÍSICA & MOVIMENTAÇÃO", "PHYSICS & MOVEMENT"));
                    GUILayout.Space(16);

                    infiniteJumps = GUILayout.Toggle(infiniteJumps, T(" Pulo Infinito", " Infinite Jump"));
                    GUILayout.Space(12);

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(T("Velocidade Andando", "Walk Speed Multiplier"), GUILayout.Width(200));
                    if (GUILayout.Button("-", GUILayout.Width(28), GUILayout.Height(24))) walkSpeedMultiplier = Mathf.Max(0.5f, walkSpeedMultiplier - 0.5f);
                    GUILayout.Label(" " + walkSpeedMultiplier.ToString("0.#") + "x", GUILayout.Width(45));
                    if (GUILayout.Button("+", GUILayout.Width(28), GUILayout.Height(24))) walkSpeedMultiplier = Mathf.Min(10.0f, walkSpeedMultiplier + 0.5f);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(8);

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(T("Velocidade Correndo", "Sprint Speed Multiplier"), GUILayout.Width(200));
                    if (GUILayout.Button("-", GUILayout.Width(28), GUILayout.Height(24))) sprintSpeedMultiplier = Mathf.Max(0.5f, sprintSpeedMultiplier - 0.5f);
                    GUILayout.Label(" " + sprintSpeedMultiplier.ToString("0.#") + "x", GUILayout.Width(45));
                    if (GUILayout.Button("+", GUILayout.Width(28), GUILayout.Height(24))) sprintSpeedMultiplier = Mathf.Min(10.0f, sprintSpeedMultiplier + 0.5f);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(8);

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(T("Velocidade Agachado", "Crouch Speed Multiplier"), GUILayout.Width(200));
                    if (GUILayout.Button("-", GUILayout.Width(28), GUILayout.Height(24))) crouchSpeedMultiplier = Mathf.Max(0.5f, crouchSpeedMultiplier - 0.5f);
                    GUILayout.Label(" " + crouchSpeedMultiplier.ToString("0.#") + "x", GUILayout.Width(45));
                    if (GUILayout.Button("+", GUILayout.Width(28), GUILayout.Height(24))) crouchSpeedMultiplier = Mathf.Min(10.0f, crouchSpeedMultiplier + 0.5f);
                    GUILayout.EndHorizontal();
                    break;

                case 2:
                    GUILayout.Label(T("VISUALIZAÇÃO TÁTICA (ESP)", "TACTICAL VISUALS (ESP)"));
                    GUILayout.Space(10);

                    espScroll = GUILayout.BeginScrollView(espScroll, GUILayout.Height(380));

                    GUILayout.Label(T("• ITENS & RELÍQUIAS:", "• LOOT & VALUABLES:"));
                    GUILayout.Space(4);
                    espValuables = GUILayout.Toggle(espValuables, T(" ESP de Itens", " Valuables ESP"));
                    if (espValuables)
                    {
                        espBoxValuables = GUILayout.Toggle(espBoxValuables, T("  └ Caixa Delimitadora", "  └ Bounding Box"));
                        espShowPrice = GUILayout.Toggle(espShowPrice, T("  └ Exibir Valor ($)", "  └ Show Cash Value"));
                        espGoldBadgeHighValue = GUILayout.Toggle(espGoldBadgeHighValue, T("  └ Destaque Dourado (> $3K)", "  └ Gold Highlight (> $3K)"));
                        espOnlyGoldItems = GUILayout.Toggle(espOnlyGoldItems, T("  └ Apenas Itens Dourados (Filtrar Resto)", "  └ Gold Items Only"));

                        GUILayout.BeginHorizontal();
                        GUILayout.Label(T("  └ Corte de Preço Mínimo: ", "  └ Minimum Cutoff Price: ") + (espValuablesMinPrice <= 0 ? T("Todos", "All") : "$" + espValuablesMinPrice.ToString("0")));
                        if (GUILayout.Button("-", GUILayout.Width(26))) espValuablesMinPrice = Mathf.Max(0f, espValuablesMinPrice - 500f);
                        if (GUILayout.Button("+", GUILayout.Width(26))) espValuablesMinPrice = Mathf.Min(5000f, espValuablesMinPrice + 500f);
                        GUILayout.EndHorizontal();
                    }

                    GUILayout.Space(10);
                    GUILayout.Label(T("• ENTIDADES & MONSTROS:", "• ENTITIES & MONSTERS:"));
                    GUILayout.Space(4);
                    espEnemies = GUILayout.Toggle(espEnemies, T(" ESP de Monstros", " Monster ESP"));
                    if (espEnemies)
                    {
                        espBoxEnemies = GUILayout.Toggle(espBoxEnemies, T("  └ Caixa Delimitadora", "  └ Bounding Box"));
                        espShowEnemyName = GUILayout.Toggle(espShowEnemyName, T("  └ Identificação / Nome", "  └ Entity Name"));
                        espShowHealthBar = GUILayout.Toggle(espShowHealthBar, T("  └ Barra de Vida Dinâmica", "  └ Dynamic Health Bar"));
                    }

                    GUILayout.Space(10);
                    GUILayout.Label(T("• PONTO DE EXTRAÇÃO:", "• EXTRACTION ZONE:"));
                    GUILayout.Space(4);
                    espExtraction = GUILayout.Toggle(espExtraction, T(" ESP de Extração (Oculto no Lobby)", " Extraction ESP"));
                    if (espExtraction)
                    {
                        espExtractionOnlyIncomplete = GUILayout.Toggle(espExtractionOnlyIncomplete, T("  └ Ocultar após Concluir Meta", "  └ Hide Once Goal Reached"));
                    }

                    GUILayout.Space(10);
                    GUILayout.Label(T("• CONFIGURAÇÃO GERAL:", "• GENERAL CONFIG:"));
                    GUILayout.Space(4);
                    espShowDistance = GUILayout.Toggle(espShowDistance, T(" Exibir Distância em Metros [m]", " Show Distance Indicator [m]"));

                    GUILayout.BeginHorizontal();
                    GUILayout.Label(T(" Alcance Máximo: ", " Max Range: ") + espMaxDistance.ToString("0") + "m");
                    if (GUILayout.Button("-", GUILayout.Width(28))) espMaxDistance = Mathf.Max(25f, espMaxDistance - 25f);
                    if (GUILayout.Button("+", GUILayout.Width(28))) espMaxDistance = Mathf.Min(300f, espMaxDistance + 25f);
                    GUILayout.EndHorizontal();

                    GUILayout.EndScrollView();
                    break;

                case 3:
                    GUILayout.Label(T("GERADOR DE EQUIPAMENTOS", "EQUIPMENT SPAWNER"));
                    GUILayout.Space(10);

                    GUILayout.Label(T("• ORBES COLETÁVEIS (VALOR EXTRA):", "• DROP ORBS (EXTRA SCRAP):"));
                    GUILayout.Space(4);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(T("🟣 Orbe Pequena", "🟣 Small Orb"), GUILayout.Height(28))) SpawnEnemyValuable(0);
                    if (GUILayout.Button(T("🟣 Orbe Média", "🟣 Medium Orb"), GUILayout.Height(28))) SpawnEnemyValuable(1);
                    if (GUILayout.Button(T("🟣 Orbe Grande", "🟣 Big Orb"), GUILayout.Height(28))) SpawnEnemyValuable(2);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(10);
                    GUILayout.Label(T("• ARMAS & ITENS DE SUPORTE:", "• COMBAT & SUPPORT ARSENAL:"));
                    GUILayout.Space(4);

                    itemSpawnScroll = GUILayout.BeginScrollView(itemSpawnScroll, GUILayout.Height(230));

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🔫 " + T("Rifle Laser", "Laser Rifle"), GUILayout.Height(28))) SpawnGameItem("Item Gun Laser");
                    if (GUILayout.Button("🔫 " + T("Pistola Balística", "Ballistic Pistol"), GUILayout.Height(28))) SpawnGameItem("Item Gun Bullet");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("💉 " + T("Tranquilizante", "Tranq Pistol"), GUILayout.Height(28))) SpawnGameItem("Item Gun Tranq");
                    if (GUILayout.Button("⚡ " + T("Bastão de Choque", "Stun Baton"), GUILayout.Height(28))) SpawnGameItem("Item Stun Baton");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🔨 " + T("Martelo de Impacto", "Impact Hammer"), GUILayout.Height(28))) SpawnGameItem("Item Melee Inflatable Hammer");
                    if (GUILayout.Button("💣 " + T("Granada de Fragmentação", "Frag Grenade"), GUILayout.Height(28))) SpawnGameItem("Item Grenade Explosive");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("💥 " + T("Granada Repulsora", "Shockwave Grenade"), GUILayout.Height(28))) SpawnGameItem("Item Grenade Shockwave");
                    if (GUILayout.Button("⚡ " + T("Granada de Atordoamento", "Stun Grenade"), GUILayout.Height(28))) SpawnGameItem("Item Grenade Stun");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("💣 " + T("Mina Explosiva", "Explosive Mine"), GUILayout.Height(28))) SpawnGameItem("Item Mine Explosive");
                    if (GUILayout.Button("⚡ " + T("Mina de Choque", "Shock Mine"), GUILayout.Height(28))) SpawnGameItem("Item Mine Stun");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🚀 " + T("Canhão de Carrinho", "Cart Cannon"), GUILayout.Height(28))) SpawnGameItem("Item Cart Cannon");
                    if (GUILayout.Button("🔮 " + T("Laser de Carrinho", "Cart Laser"), GUILayout.Height(28))) SpawnGameItem("Item Cart Laser");
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("🌀 " + T("Cajado de Vácuo", "Void Staff"), GUILayout.Height(28))) SpawnGameItem("Item Staff Void");
                    if (GUILayout.Button("💫 " + T("Cajado Gravitacional", "Anti-Gravity Staff"), GUILayout.Height(28))) SpawnGameItem("Item Staff Zero Gravity");
                    GUILayout.EndHorizontal();

                    GUILayout.EndScrollView();
                    break;

                case 4:
                    GUILayout.Label(T("COSMÉTICOS & SKINS", "COSMETICS & SKINS"));
                    GUILayout.Space(20);

                    if (GUILayout.Button(T("Liberar Todo o Guarda-Roupa (Permanente)", "Unlock Full Wardrobe (Permanent)"), GUILayout.Height(36)))
                    {
                        ForceUnlockCosmetics();
                    }

                    GUILayout.Space(14);
                    GUI.color = new Color(0.7f, 0.75f, 0.82f);
                    GUILayout.Label(T("Salva no seu perfil do jogo todos os chapéus, trajes, cores e partes cosméticas sem alterar seu progresso.", "Saves all hats, suits, colors and cosmetic items directly to your game profile without altering progression."));
                    GUI.color = Color.white;
                    break;

                case 5:
                    GUILayout.Label(T("SESSÃO & LOJA", "LOBBY & SHOP"));
                    GUILayout.Space(14);

                    unlockShopItems = GUILayout.Toggle(unlockShopItems, T(" Liberar Todos os Itens da Loja", " Unlock Full Shop Catalog"));
                    GUILayout.Space(6);

                    freeShop = GUILayout.Toggle(freeShop, T(" Loja 100% Gratuita (Custo $0)", " Free Store Purchases ($0 Cost)"));
                    GUILayout.Space(6);

                    freeExtraction = GUILayout.Toggle(freeExtraction, T(" Extração Imediata (Meta Concluída)", " Instant Extraction (Zero Goal)"));
                    GUILayout.Space(10);

                    unbreakableItems = GUILayout.Toggle(unbreakableItems, T(" Itens Inquebráveis (Anti-Impacto)", " Unbreakable Items (Fall Damage Off)"));
                    GUILayout.Space(6);

                    infiniteStationCharge = GUILayout.Toggle(infiniteStationCharge, T(" Gerador de Energia Infinito (100%)", " Perpetual Power Station (100%)"));
                    GUILayout.Space(6);

                    infiniteItemBattery = GUILayout.Toggle(infiniteItemBattery, T(" Bateria Eterna em Todos os Equipamentos", " Infinite Battery On All Gear"));
                    GUILayout.Space(6);

                    autoChargeHeldItem = GUILayout.Toggle(autoChargeHeldItem, T(" Recarga Automática no Inventário", " Auto-Recharge Held Equipment"));
                    break;

                case 6:
                    GUILayout.Label(T("SISTEMA", "SYSTEM"));
                    GUILayout.Space(20);

                    GUILayout.Label(T("Idioma / Language", "Interface Language"));
                    GUILayout.Space(6);

                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button(langIndex == 0 ? "[ Português ]" : "Português", GUILayout.Height(30)))
                    {
                        langIndex = 0;
                    }
                    if (GUILayout.Button(langIndex == 1 ? "[ English ]" : "English", GUILayout.Height(30)))
                    {
                        langIndex = 1;
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(15);
                    if (GUILayout.Button(T("Reposicionar Janela ao Centro", "Reset Window to Center"), GUILayout.Height(30)))
                    {
                        winRect.x = (Screen.width - winRect.width) / 2f;
                        winRect.y = (Screen.height - winRect.height) / 2f;
                    }

                    GUILayout.Space(10);
                    if (GUILayout.Button(T("Descarregar Cheat / Ejetar DLL", "Unload & Eject Menu"), GUILayout.Height(30)))
                    {
                        Loader.Unload();
                    }

                    GUILayout.Space(14);
                    GUI.color = new Color(0.2f, 0.4f, 0.8f, 0.5f);
                    GUILayout.Box("", GUILayout.Height(1), GUILayout.ExpandWidth(true));
                    GUILayout.Space(8);

                    GUI.color = new Color(0.35f, 0.75f, 1f);
                    GUILayout.Label("PATCHZONE • CRÉDITOS");
                    GUILayout.Space(4);

                    GUI.color = Color.white;
                    GUILayout.Label("Desenvolvido por: Kayke");
                    GUILayout.Label("Discord Oficial: kayke_v1");
                    GUILayout.Space(10);

                    GUI.color = new Color(1f, 0.35f, 0.35f);
                    if (GUILayout.Button("▶ Acessar Canal do YouTube", GUILayout.Height(36)))
                    {
                        Application.OpenURL("https://www.youtube.com/@PatchZoneBR");
                    }
                    GUI.color = Color.white;
                    break;
            }

            GUILayout.EndArea();

            GUI.DragWindow(new Rect(0, 0, winRect.width, 40));
        }
    }
}
