using UnityEngine;

public class VisualHelper
{
    public static int GetNextIndex(int currentIndex, int count)
    {
        Debug.Log("prev ind " + currentIndex);
        return (currentIndex + 1) % count;
    }

    public static int GetPrevIndex(int currentIndex, int count)
    {
        return (currentIndex - 1 + count) % count;
    }
}


public class DesignGeneral : MonoBehaviour
{
    public SamuraiNames names;
    public SamuraiVisualsSO maleVisuals;
    public Character general;
    [SerializeField] CharacterUIDisplay display;

    [SerializeField] SoundOld buttonPressed = null;
    [SerializeField] SoundOld buttonHover = null;
    private void Start()
    {
        general = SavableDataManager.Instance.data.team.General;
        display.Init(general);
    }

    public void LoadChooseMap()
    {
        ButtonOnClick();
        SceneLoader.Instance.LoadScene(SceneConstants.ChooseLevelScene);
    }

    public void RanomizeName()
    {
        ButtonOnClick();
        general.Name = names.GetRandomName();
        display.Init(general);
    }

    public void NextHelmetF()
    {
        ButtonOnClick();
        general.SamuraiVisuals.helmetFIndex = VisualHelper.GetNextIndex(general.SamuraiVisuals.helmetFIndex, maleVisuals.Helmet_F.Count);
        display.Init(general);
    }

    public void NextHelmetB()
    {
        ButtonOnClick();
        general.SamuraiVisuals.helmetBIndex = VisualHelper.GetNextIndex(general.SamuraiVisuals.helmetBIndex, maleVisuals.Helmet_B.Count);
        display.Init(general);
    }

    public void NextFace()
    {
        ButtonOnClick();
        general.SamuraiVisuals.faceIndex = VisualHelper.GetNextIndex(general.SamuraiVisuals.faceIndex, maleVisuals.Face_F.Count);
        display.Init(general);
    }

    public void NextHead()
    {
        ButtonOnClick();
        general.SamuraiVisuals.headIndex = VisualHelper.GetNextIndex(general.SamuraiVisuals.headIndex, maleVisuals.Head.Count);
        display.Init(general);
    }

    public void NextPants()
    {
        ButtonOnClick();
        general.SamuraiVisuals.pantsIndex = VisualHelper.GetNextIndex(general.SamuraiVisuals.pantsIndex, maleVisuals.Pants.Count);
        display.Init(general);
    }

    // Previous item methods
    public void PrevHelmetF()
    {
        ButtonOnClick();
        general.SamuraiVisuals.helmetFIndex = VisualHelper.GetPrevIndex(general.SamuraiVisuals.helmetFIndex, maleVisuals.Helmet_F.Count);
        display.Init(general);
    }

    public void PrevHelmetB()
    {
        ButtonOnClick();
        general.SamuraiVisuals.helmetBIndex = VisualHelper.GetPrevIndex(general.SamuraiVisuals.helmetBIndex, maleVisuals.Helmet_B.Count);
        display.Init(general);

    }

    public void PrevFace()
    {
        ButtonOnClick();
        general.SamuraiVisuals.faceIndex = VisualHelper.GetPrevIndex(general.SamuraiVisuals.faceIndex, maleVisuals.Face_F.Count);
        display.Init(general);
    }

    public void PrevHead()
    {
        ButtonOnClick();
        general.SamuraiVisuals.headIndex = VisualHelper.GetPrevIndex(general.SamuraiVisuals.headIndex, maleVisuals.Head.Count);
        display.Init(general);
    }

    public void PrevPants()
    {
        ButtonOnClick();
        general.SamuraiVisuals.pantsIndex = VisualHelper.GetPrevIndex(general.SamuraiVisuals.pantsIndex, maleVisuals.Pants.Count);
        display.Init(general);
    }

    public void ButtonOnClick()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonPressed);
    }

    public void ButtonOnHover()
    {
        AudioManagerOld.Instance.PlayGlobal(buttonHover);

    }
}
