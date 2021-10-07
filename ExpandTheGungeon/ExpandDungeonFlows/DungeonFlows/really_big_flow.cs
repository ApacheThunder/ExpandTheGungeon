using Dungeonator;
using System;
using System.Collections.Generic;
using UnityEngine;
using ExpandTheGungeon.ExpandPrefab;


namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class really_big_flow {
        
        public static DungeonFlow Really_Big_Flow() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowSubtypeRestriction TestSubTypeRestriction = new DungeonFlowSubtypeRestriction() {
                baseCategoryRestriction = PrototypeDungeonRoom.RoomCategory.NORMAL,
                normalSubcategoryRestriction = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP,
                bossSubcategoryRestriction = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS,
                specialSubcategoryRestriction = PrototypeDungeonRoom.RoomSpecialSubCategory.UNSPECIFIED_SPECIAL,
                secretSubcategoryRestriction = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET,
                maximumRoomsOfSubtype = 1
            };

            DungeonFlowNode EntranceNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.ENTRANCE,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.big_entrance,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode BossFoyerNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.DragunBossFoyerRoom,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode BossNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.BOSS,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.DraGunRoom01,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode BossExitNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.EXIT,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.DraGunExitRoom,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode BossEndTimesNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.EXIT,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.DraGunEndTimesRoom,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = true,
                handlesOwnWarping = true,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode FirstShopNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.BlacksmithShop,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode SecondShopNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.SPECIAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = null,
                overrideRoomTable = ExpandPrefabs.shop_room_table,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode MiniBossFoyerNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.DragunBossFoyerRoom,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };
            DungeonFlowNode FakebossNode = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.tutorial_fakeboss,
                overrideRoomTable = null,
                capSubchain = false,
                subchainIdentifier = string.Empty,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                subchainIdentifiers = new List<string>(0),
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                chainRules = new List<ChainRule>(0),
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                parentNodeGuid = string.Empty,
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = string.Empty,
                loopTargetIsOneWay = false,
                guidAsString = Guid.NewGuid().ToString(),
            };

            List<DungeonFlowNode> ConnectorNodes = new List<DungeonFlowNode>();
            List<DungeonFlowNode> NormalNodes = new List<DungeonFlowNode>();
            List<DungeonFlowNode> ChestNodes = new List<DungeonFlowNode>();
            
            for (int i = 0; i < 11; i++) {
                ConnectorNodes.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR));
            }            
            for (int i = 0; i < 89; i++) {
                NormalNodes.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.NORMAL));
            }

            ChestNodes.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.REWARD, ExpandPrefabs.reward_room));
            ChestNodes.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room));
            ChestNodes.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room));
            ChestNodes.Add(ExpandDungeonFlow.GenerateDefaultNode(m_CachedFlow, PrototypeDungeonRoom.RoomCategory.CONNECTOR, ExpandPrefabs.reward_room));

            m_CachedFlow.name = "Really_Big_Flow";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.CustomRoomTable2;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>(0);// { TestSubTypeRestriction };
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0); // { ChaosDungeonFlows.BaseSharedInjectionData };
            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(EntranceNode, null);
            m_CachedFlow.AddNodeToFlow(ConnectorNodes[10], EntranceNode);
            m_CachedFlow.AddNodeToFlow(FirstShopNode, ConnectorNodes[10]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[88], ConnectorNodes[10]);

            // First chain of 25 nodes starting at 99 and moving down           
            m_CachedFlow.AddNodeToFlow(NormalNodes[87], NormalNodes[88]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[86], NormalNodes[87]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[85], NormalNodes[86]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[84], NormalNodes[85]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[83], NormalNodes[84]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[82], NormalNodes[83]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[81], NormalNodes[82]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[80], NormalNodes[81]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[79], NormalNodes[80]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[78], NormalNodes[79]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[77], NormalNodes[78]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[76], NormalNodes[77]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[75], NormalNodes[76]);
            // Chest 1
            m_CachedFlow.AddNodeToFlow(ChestNodes[0], NormalNodes[75]);

            //Start of Second Chain
            ConnectorNodes[9].roomCategory = PrototypeDungeonRoom.RoomCategory.HUB;
            m_CachedFlow.AddNodeToFlow(ConnectorNodes[9], NormalNodes[75]);

            m_CachedFlow.AddNodeToFlow(NormalNodes[74], ConnectorNodes[9]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[73], NormalNodes[74]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[72], NormalNodes[73]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[71], NormalNodes[72]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[70], NormalNodes[71]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[69], NormalNodes[70]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[68], NormalNodes[69]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[67], NormalNodes[68]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[66], NormalNodes[67]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[65], NormalNodes[66]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[64], NormalNodes[65]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[63], NormalNodes[64]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[62], NormalNodes[63]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[61], NormalNodes[62]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[60], NormalNodes[61]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[59], NormalNodes[60]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[58], NormalNodes[59]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[57], NormalNodes[58]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[56], NormalNodes[57]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[55], NormalNodes[56]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[54], NormalNodes[55]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[53], NormalNodes[54]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[52], NormalNodes[53]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[51], NormalNodes[52]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[50], NormalNodes[51]);
            // Chest 2
            m_CachedFlow.AddNodeToFlow(ChestNodes[1], NormalNodes[50]);
            m_CachedFlow.AddNodeToFlow(SecondShopNode, ChestNodes[1]);

            //Start of Third Chain
            m_CachedFlow.AddNodeToFlow(NormalNodes[49], ConnectorNodes[9]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[48], NormalNodes[49]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[47], NormalNodes[48]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[46], NormalNodes[47]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[45], NormalNodes[46]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[44], NormalNodes[45]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[43], NormalNodes[44]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[42], NormalNodes[43]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[41], NormalNodes[42]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[40], NormalNodes[41]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[39], NormalNodes[40]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[38], NormalNodes[39]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[37], NormalNodes[38]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[36], NormalNodes[37]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[35], NormalNodes[36]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[34], NormalNodes[35]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[33], NormalNodes[34]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[32], NormalNodes[33]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[31], NormalNodes[32]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[30], NormalNodes[31]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[29], NormalNodes[30]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[28], NormalNodes[29]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[27], NormalNodes[28]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[26], NormalNodes[27]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[25], NormalNodes[26]);
            // Chest 3
            m_CachedFlow.AddNodeToFlow(ChestNodes[2], NormalNodes[25]);
            m_CachedFlow.AddNodeToFlow(MiniBossFoyerNode, ChestNodes[2]);
            m_CachedFlow.AddNodeToFlow(FakebossNode, MiniBossFoyerNode);


            //Start of Fourth Chain
            m_CachedFlow.AddNodeToFlow(NormalNodes[24], ConnectorNodes[9]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[23], NormalNodes[24]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[22], NormalNodes[23]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[21], NormalNodes[22]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[20], NormalNodes[21]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[19], NormalNodes[20]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[18], NormalNodes[19]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[17], NormalNodes[18]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[16], NormalNodes[17]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[15], NormalNodes[16]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[14], NormalNodes[15]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[13], NormalNodes[14]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[12], NormalNodes[13]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[11], NormalNodes[12]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[10], NormalNodes[11]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[9], NormalNodes[10]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[8], NormalNodes[9]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[7], NormalNodes[8]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[6], NormalNodes[7]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[5], NormalNodes[6]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[4], NormalNodes[5]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[3], NormalNodes[4]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[2], NormalNodes[3]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[1], NormalNodes[2]);
            m_CachedFlow.AddNodeToFlow(NormalNodes[0], NormalNodes[1]);
            // Chest 4
            m_CachedFlow.AddNodeToFlow(ChestNodes[3], NormalNodes[0]);

            // Boss
            m_CachedFlow.AddNodeToFlow(BossFoyerNode, ChestNodes[3]);
            m_CachedFlow.AddNodeToFlow(BossNode, BossFoyerNode);
            m_CachedFlow.AddNodeToFlow(BossExitNode, BossNode);
            m_CachedFlow.AddNodeToFlow(BossEndTimesNode, BossExitNode);

            // m_CachedFlow.LoopConnectNodes(ComplexFlowNode_05, ComplexFlowNode_09);

            m_CachedFlow.FirstNode = EntranceNode;

            return m_CachedFlow;
        }
    }
}

