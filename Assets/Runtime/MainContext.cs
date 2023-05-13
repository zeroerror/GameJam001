using GameArki.FreeInput;
using UnityEngine;

public class MainContext {

    VFXManager vfxManager;
    public VFXManager VFXManager => vfxManager;

    SFXManager sfxManager;
    public SFXManager SFXManager => sfxManager;

    UIManager uiManager;
    public UIManager UIManager => uiManager;

    public FreeInputCore freeInputCore;

    public RootTemplate rootTemplate;
    public RootRepo rootRepo;
    public RootService rootService;

    GameEntity gameEntity;
    public GameEntity GameEntity => gameEntity;

    public MainContext() {
        this.rootTemplate = new RootTemplate();
        this.rootRepo = new RootRepo();
        this.rootService = new RootService();

        this.gameEntity = new GameEntity();
    }

    public void Inject(VFXManager vfxManager, SFXManager sfxManager, UIManager uiManager, FreeInputCore freeInputCore) {
        this.vfxManager = vfxManager;
        this.sfxManager = sfxManager;
        this.uiManager = uiManager;
        this.freeInputCore = freeInputCore;
    }

}