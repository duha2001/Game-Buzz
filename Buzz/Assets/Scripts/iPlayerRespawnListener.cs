using UnityEngine;
using System.Collections;

public interface IPlayerRespawnListener
{

    void OnPlayerReSpawninThisCheckpoint(Checkpoint checkpoint, Player player);
}
