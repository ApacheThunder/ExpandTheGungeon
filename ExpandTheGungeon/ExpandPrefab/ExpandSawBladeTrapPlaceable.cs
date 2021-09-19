using Dungeonator;
using UnityEngine;

namespace ExpandTheGungeon.ExpandPrefab {

    public class ExpandSawBladeTrapPlaceable : DungeonPlaceableBehaviour, IPlaceConfigurable {

        public ExpandSawBladeTrapPlaceable() {
            PathNodeAreaSize = new IntVector2(4, 4);
            PathNodeOffset = Vector2.zero;
        }

        public IntVector2 PathNodeAreaSize;
        public Vector2 PathNodeOffset;

        private void Start() { }

        private SerializedPath GenerateRectanglePathInset(Vector2 BasePosition, IntVector2 Dimensions) {
            IntVector2 basePosition = BasePosition.ToIntVector2();

            basePosition += new IntVector2(-1, 0);
            Dimensions += IntVector2.One;
            SerializedPath serializedPath = new SerializedPath(basePosition);
            serializedPath.AddPosition(basePosition + Dimensions.WithY(0));
            serializedPath.AddPosition(basePosition + Dimensions);
            serializedPath.AddPosition(basePosition + Dimensions.WithX(0));
            serializedPath.wrapMode = SerializedPath.SerializedPathWrapMode.Loop;
            SerializedPathNode value = serializedPath.nodes[0];
            value.placement = SerializedPathNode.SerializedNodePlacement.NorthEast;
            serializedPath.nodes[0] = value;
            value = serializedPath.nodes[1];
            value.placement = SerializedPathNode.SerializedNodePlacement.NorthWest;
            serializedPath.nodes[1] = value;
            value = serializedPath.nodes[2];
            value.placement = SerializedPathNode.SerializedNodePlacement.SouthWest;
            serializedPath.nodes[2] = value;
            value = serializedPath.nodes[3];
            value.placement = SerializedPathNode.SerializedNodePlacement.SouthEast;
            serializedPath.nodes[3] = value;
            return serializedPath;
        }

        public void ConfigureOnPlacement(RoomHandler room) {
            gameObject.SetActive(true);
            enabled = true;

            IntVector2 SpawnPosition = (gameObject.transform.PositionVector2().ToIntVector2() - room.area.basePosition);
            Vector2 NodePosition = SpawnPosition.ToVector2();

            SerializedPath SawBladePath = GenerateRectanglePathInset(NodePosition, PathNodeAreaSize);
            room.area.prototypeRoom.paths.Add(SawBladePath);

            DungeonPlaceable m_TrapPlacable = BraveResources.Load<DungeonPlaceable>("RobotDaveTraps", ".asset");

            GameObject sawbladePrefab = null;

            if (m_TrapPlacable) { sawbladePrefab = m_TrapPlacable.variantTiers[0].nonDatabasePlaceable; }
            
            if (sawbladePrefab) {
                GameObject m_PlacedSawBlade = Instantiate(sawbladePrefab, gameObject.transform.position, Quaternion.identity);
                m_PlacedSawBlade.SetActive(false);

                if (m_PlacedSawBlade) {
                    PathMover SawPathMover = m_PlacedSawBlade.GetComponent<PathMover>();
                    if (SawPathMover != null) {
                        SawPathMover.RoomHandler = room;
                        SawPathMover.Path = SawBladePath;
                        SawPathMover.PathStartNode = Random.Range(0, SawBladePath.nodes.Count);
                        SawPathMover.IsUsingAlternateTargets = false;
                        SawPathMover.ForceCornerDelayHack = false;
                        SawPathMover.nodeOffset = PathNodeOffset;
                        m_PlacedSawBlade.SetActive(true);
                    }
                }
            }
        }

        protected override void OnDestroy() { base.OnDestroy(); }
    }
}

