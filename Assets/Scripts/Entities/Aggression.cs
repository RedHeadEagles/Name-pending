/// <summary>
/// Defines how this entity reacts toward other entities
/// </summary>
public enum Aggression
{
	/// <summary>
	/// Will not attack unless provoked
	/// </summary>
	Passive,

	/// <summary>
	/// Will not attack
	/// </summary>
	Friendly,
	
	/// <summary>
	/// Will always try to attack
	/// </summary>
	Hostile
}
