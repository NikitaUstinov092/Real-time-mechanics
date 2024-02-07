using Lessons.MetaGame;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<RealtimeSaveLoader>().AsSingle();
        Container.BindInterfacesAndSelfTo<MoneyRewardReceiver>().AsSingle();
        Container.BindInterfacesAndSelfTo<LifeRewardReceiver>().AsSingle();
        Container.BindInterfacesAndSelfTo<WoodRewardReciever>().AsSingle();
    }
}