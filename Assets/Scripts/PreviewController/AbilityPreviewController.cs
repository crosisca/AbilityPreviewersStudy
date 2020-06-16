using UnityEngine;

public class AbilityPreviewController : MonoBehaviour
{
    //Assigned by injection
    [SerializeField]
    public Transform champion;

    //Assigned by injection
    [SerializeField]
    protected float maxRange = Mathf.Infinity;

    //Assigned by injection
    [SerializeField]
    protected Vector3 offset;

    //Assigned by injection
    Ability ability;

    public bool IsValid { get; private set; }
    protected Vector3 Origin => champion.position;


    protected Transform target;
    protected Vector3 mouseHitPosition;
    protected Vector3 TargetPosition => target.position;
    protected Quaternion targetRotation;

    

    
    //Assigned on editor
    [SerializeField]
    protected bool canRotate = true;


    

    #region Trajectory
    protected bool showTrajectory;
    protected ParabolaMesh trajectoryParabola;
    #endregion

    public void Setup (Transform champion, Ability ability)
    {
        this.champion = champion;
        this.ability = ability;
    }

    public void Setup(Transform champion, Ability ability, Vector3 offset)
    {
        Setup(champion, ability);

        this.offset = offset;
    }

    public void Setup (Transform champion, Ability ability, float maxRange)
    {
        Setup(champion, ability);

        this.maxRange = maxRange;
    }

    void Awake()
    {
        ability = new Ability();

        Initialize();
    }

    protected virtual void Initialize()
    {
        target = new GameObject("target").transform;
        target.SetParent(transform);

        if (!canRotate)
            transform.rotation = Quaternion.identity;

        trajectoryParabola = GetComponentInChildren<ParabolaMesh>();

        if (trajectoryParabola != null)
        {
            showTrajectory = true;
            trajectoryParabola.Initialize(champion, target);
        }

       
    }

    void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate ()
    {
        mouseHitPosition = Camera.main.ScreenPointToRay(Input.mousePosition).GetIntersectionPoint();

        CalculateTargetLocation();
        CalculateTargetRotation();

        IsValid = !ability.Previewable || ability.IsPreviewPositionValid(TargetPosition);

        if (IsValid)
        {
            SetPosition();
            SetRotation();
            SetScale();
        }
    }
    
    protected virtual void CalculateTargetLocation()
    {

    }

    protected virtual void CalculateTargetRotation()
    {

    }

    protected virtual void SetPosition ()
    {
        transform.position = TargetPosition;
    }

    protected virtual void SetRotation()
    {
        //old mouse mode rotation//transform.rotation = Quaternion.LookRotation(CamerasManager.Current.CurrentActiveCamera.Camera.transform.forward.FlattenY(), Vector3.up); //WHAT????
        if (!canRotate)
            return;

        transform.rotation = targetRotation;
    }

    protected virtual void SetScale ()
    {
        
    }
    
    public virtual void Enable ()
    {
        gameObject.SetActive(true);

        if (showTrajectory)
            trajectoryParabola.gameObject.SetActive(true);
    }


    public virtual void Disable ()
    {
        gameObject.SetActive(false);

        if (showTrajectory)
            trajectoryParabola.gameObject.SetActive(false);
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