public class ZoneLeaveTrigger : Trigger
{
	protected override void OnExit(Entity entity)
	{
		entity.ApplyDamage(float.MaxValue);
	}
}
