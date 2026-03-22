# Development

## Q&A

**Q: How do I change how far a sound is heard or how loud it is?**

Each sound command implements the [`ISfxCommand`](Ports/ISfxCommand.cs) interface, which has two properties that control this:

- **`Range`** - The distance in blocks the sound can be heard. This also controls how far the flavor text notification is broadcast to nearby players. Default is `48f`.
- **`Volume`** - A loudness multiplier within that range. `1f` is normal, `2f` is twice as loud. `0.5f` is half as loud.

For example, to make `/growl` quieter and only audible within 12 blocks in [`GrowlCommand.cs`](Commands/GrowlCommand.cs):

```csharp
public float Range => 12f;    // heard within 12 blocks!
public float Volume => 0.6f;  // slightly quieter than normal
```

For reference, some commands already use non-default volumes - [`CluckCommand`](Commands/CluckCommand.cs) uses `Volume => 2f` and [`CrowCommand`](Commands/CrowCommand.cs) uses `Volume => 1.5f` to make those sounds louder.
