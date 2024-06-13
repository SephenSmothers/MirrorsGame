using Unity.Netcode;
using UnityEngine;
using TerrorForage.Core.Singleton;

public class PlayerRPC : Singleton<PlayerRPC>
{

    private NetworkVariable<int> PlayersInGameCount = new NetworkVariable<int>();

    public int PlayersInGame
    {
        get
        {
            return PlayersInGameCount.Value;
        }
    }

    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += (id) =>
        {
            if (IsServer)
            {
                Debug.Log($"{id} Just Connected");
                PlayersInGameCount.Value++;
            }
        };

        NetworkManager.Singleton.OnClientDisconnectCallback += (id) =>
        {
            if (IsServer)
            {
                Debug.Log($"{id} Just Disconnected");
                PlayersInGameCount.Value--;
            }
        };
    }


    /*
        [SerializeField]
        FirstPersonController Player;


        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
            {
                Move();
            }
        }

        public void Move()
        {
            SubmitPositionRequestRpc();
        }

        [Rpc(SendTo.Server)]
        void SubmitPositionRequestRpc(RpcParams rpcParams = default)
        {

            Player.Move();
            Player.JumpAndGravity();
            Player.GroundedCheck();
            Player.CameraRotation();
            Position.Value = Player.transform.position;
            //var randomPosition = GetRandomPositionOnPlane();
            //transform.position = randomPosition;
            //Position.Value = randomPosition;
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update()
        {
            transform.position = Position.Value;
        }
    */
}

