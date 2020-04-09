using Cakewalk.IoC.Core;
public class BootStrapper : BaseBootStrapper
{
    public override void Configure(Container _container)
    {
        _container.Register<GameSystem>();
        _container.Register<Weapon>();
        _container.Register<WeaponSet>();
        _container.Register<Skill>();
        _container.Register<SkillSet>();
        _container.Register<SystemInfo>();
        _container.Register<Shop>();
        _container.Register<Deity>();
        _container.Register<WeaponStats>();
        _container.Register<EntityStats>();
        _container.Register<SystemStatus>();
        _container.Register<Item>();
    }
}
