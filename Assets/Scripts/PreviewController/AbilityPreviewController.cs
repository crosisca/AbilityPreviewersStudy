using System.Linq;
using UnityEngine;

public class AbilityPreviewController : MonoBehaviour
{
    //Assigned by injection
    [SerializeField]
    public Transform champion;

    //Assigned by injection
    [SerializeField]
    public float maxRange = Mathf.Infinity;

    //Assigned by injection
    [SerializeField]
    public Vector3 offset;

    //Assigned by injection
    Ability ability;

    public bool IsValid { get; private set; }
    public Vector3 Origin => champion.position;

    public Vector3 MouseHitPosition { get; private set; }
    
    //Assigned on editor
    [SerializeField]
    public bool canRotate = true;


    IAbilityPreviewer[] previewers;

    #region Trajectory
    protected bool showTrajectory;
    protected ParabolaMesh trajectoryParabola;
    #endregion

    public void Setup (Transform champion, Ability ability, float maxRange = Mathf.Infinity, Vector3 offset = default)
    {
        this.champion = champion;
        this.ability = ability;
        this.maxRange = maxRange;
        this.offset = offset;
    }
    
    void Awake()
    {
        ability = new Ability();

        Initialize();
    }

    protected virtual void Initialize()
    {
        previewers = GetComponentsInChildren<IAbilityPreviewer>();

        foreach (IAbilityPreviewer abilityPreviewer in previewers)
            abilityPreviewer.Initialize();
    }

    void Update()
    {
        MouseHitPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetIntersectionPoint();

        foreach (IAbilityPreviewer abilityPreviewer in previewers)
        {
            abilityPreviewer.CalculateTargetLocation();
            abilityPreviewer.CalculateTargetRotation();
        }


        foreach (IAbilityPreviewer abilityPreviewer in previewers)
        {
            IsValid = !ability.Previewable || ability.IsPreviewPositionValid(abilityPreviewer.TargetPosition);

            if (!IsValid)
                continue;

            abilityPreviewer.SetPosition();
            abilityPreviewer.SetRotation();
            abilityPreviewer.SetScale();
        }
    }
    
    #region Skydome Specific
    //public virtual void Enable ()
    //{
    //    GameSession.Current.Updater.RegisterUpdate(OnUpdate);
    //    gameObject.SetActive(true);
    //}


    //public virtual void Disable ()
    //{
    //    GameSession.Current?.Updater.UnregisterUpdate(OnUpdate);
    //    gameObject.SetActive(false);
    //}

    //protected void OnSessionEnd ()
    //{
    //    GameSession.Current.OnSessionEnd -= OnSessionEnd;
    //    MatchManager.Current.OnEndMatchGameplayEvent -= OnMatchGameplayEnded;
    //    GameSession.Current.Updater.UnregisterUpdate(OnUpdate);
    //}

    //protected void OnMatchGameplayEnded (Team defeatedTeam)
    //{
    //    GameSession.Current.OnSessionEnd -= OnSessionEnd;
    //    MatchManager.Current.OnEndMatchGameplayEvent -= OnMatchGameplayEnded;
    //    GameSession.Current.Updater.UnregisterUpdate(OnUpdate);
    //}
    #endregion
}