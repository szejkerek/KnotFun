using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class SamuraiVisuals
{
    SamuraiVisualsSO visualsSO;
    public int helmetBIndex;
    public int helmetFIndex;
    public int faceIndex;
    public int headIndex;
    public int pantsIndex;

    public SamuraiVisuals(SamuraiVisualsSO visualsSO)
    {
        this.visualsSO = visualsSO;
        Randomize();
    }

    public void Apply(SamuraiRenderers renderer, Character character)
    {
        renderer.Helmet_B.sprite = visualsSO.Helmet_B[helmetBIndex];
        renderer.Helmet_F.sprite = visualsSO.Helmet_F[helmetFIndex];
        renderer.Face_F.sprite = visualsSO.Face_F[faceIndex];
        renderer.Head.sprite = visualsSO.Head[headIndex];
        renderer.Weapon.sprite = new AssetReferenceSprite(character.Weapon.SpriteGuid).LoadAssetAsync<Sprite>().WaitForCompletion();
        renderer.Chestplate.sprite = new AssetReferenceSprite(character.Armor.SpriteGuid).LoadAssetAsync<Sprite>().WaitForCompletion();
        renderer.Pants.sprite = visualsSO.Pants[pantsIndex];
    }

    public void Apply(SamuraiImages images, Character character)
    {
        images.Helmet_B.sprite = visualsSO.Helmet_B[helmetBIndex];
        images.Helmet_F.sprite = visualsSO.Helmet_F[helmetFIndex];
        images.Face_F.sprite = visualsSO.Face_F[faceIndex];
        images.Head.sprite = visualsSO.Head[headIndex];
        images.Weapon.sprite = new AssetReferenceSprite(character.Weapon.SpriteGuid).LoadAssetAsync<Sprite>().WaitForCompletion();
        images.Chestplate.sprite = new AssetReferenceSprite(character.Armor.SpriteGuid).LoadAssetAsync<Sprite>().WaitForCompletion();
        images.Pants.sprite = visualsSO.Pants[pantsIndex];
    }

    public void Randomize()
    {
        faceIndex = Random.Range(0, visualsSO.Face_F.Count);
        headIndex = Random.Range(0, visualsSO.Head.Count);
        pantsIndex = Random.Range(0, visualsSO.Pants.Count);
        helmetBIndex = Random.Range(0, visualsSO.Helmet_B.Count);
        helmetFIndex = Random.Range(0, visualsSO.Helmet_F.Count);
    }
}
