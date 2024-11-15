using Extensions;
using Feature.DamageApplication.Component;
using Infrastructure.ECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Zenject;

namespace Feature.DamageApplication.System
{
	public sealed class DamageBufferToDamageSystem : IEcsRunSystem
	{
		[Inject] EntityWrapper _other;
		[Inject] EntityWrapper _buffer;
		
		readonly EcsFilterInject<Inc<DamageBuffer>> _damageBufferFilter;
		
		public void Run(IEcsSystems systems)
		{
			foreach (var e in _damageBufferFilter.Value)
			{
				_buffer.SetEntity(e);
				var buffer = _buffer.DamageBuffer();
				foreach (var otherPack in buffer)
				{
					if (otherPack.Unpack(out var otherEntity) == false)
						continue;

					_other.SetEntity(otherEntity);
					_other.AppendDamage(Constant.Damage.c_Default);
				}
			}
		}
	}
}