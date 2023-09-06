using System.Collections.Generic;
using UnityEngine;
using Dungeonator;
using ExpandTheGungeon.ExpandPrefab;


namespace ExpandTheGungeon.ExpandDungeonFlows {

    public class custom_glitchchest_flow {
                        
        public static DungeonFlow Custom_GlitchChest_Flow() {

            DungeonFlow m_CachedFlow = ScriptableObject.CreateInstance<DungeonFlow>();

            DungeonFlowSubtypeRestriction GlitchTestSubTypeRestriction = new DungeonFlowSubtypeRestriction() {
                baseCategoryRestriction = PrototypeDungeonRoom.RoomCategory.NORMAL,
                normalSubcategoryRestriction = PrototypeDungeonRoom.RoomNormalSubCategory.TRAP,
                bossSubcategoryRestriction = PrototypeDungeonRoom.RoomBossSubCategory.FLOOR_BOSS,
                specialSubcategoryRestriction = PrototypeDungeonRoom.RoomSpecialSubCategory.UNSPECIFIED_SPECIAL,
                secretSubcategoryRestriction = PrototypeDungeonRoom.RoomSecretSubCategory.UNSPECIFIED_SECRET,
                maximumRoomsOfSubtype = 1
            };

            DungeonFlowNode GlitchFlowNode_00 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.ENTRANCE,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.tiny_entrance,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                childNodeGuids = new List<string>() { "5160f844-ff79-4d19-b813-38496a344e8e" },
                loopTargetIsOneWay = false,
                guidAsString = "d9be71d3-8d97-48af-8eda-54aa897862be"            
            };

            DungeonFlowNode GlitchFlowNode_01 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.boss_foyer,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "bd36ddc7-e687-4355-a69b-e799c9d857de",
                childNodeGuids = new List<string>() { "a0098d24-7733-4baf-82c0-11ce3e068261" },
                loopTargetIsOneWay = false,
                guidAsString = "036aafaf-a754-4410-94c5-2c4e5139a5bf"
            };

            DungeonFlowNode GlitchFlowNode_02 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = null,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "036aafaf-a754-4410-94c5-2c4e5139a5bf",
                childNodeGuids = new List<string>() { "f06e0430-437a-481e-9b34-604d145cc77d" },
                loopTargetIsOneWay = false,
                guidAsString = "a0098d24-7733-4baf-82c0-11ce3e068261"
            };

            if (ExpandPrefabs.doublebeholstertable) {
                GlitchFlowNode_02.overrideRoomTable = ExpandPrefabs.doublebeholstertable;
            } else {
                GlitchFlowNode_02.overrideExactRoom = ExpandPrefabs.doublebeholsterroom01;
            }

            DungeonFlowNode GlitchFlowNode_03 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.tiny_exit,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "a0098d24-7733-4baf-82c0-11ce3e068261",
                childNodeGuids = new List<string>(0),
                loopTargetIsOneWay = false,
                guidAsString = "f06e0430-437a-481e-9b34-604d145cc77d"
            };

            DungeonFlowNode GlitchFlowNode_04 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "d9be71d3-8d97-48af-8eda-54aa897862be",
                childNodeGuids = new List<string>() { "0c8ee6c4-31b4-4226-9ddb-90c7eca8f2d3" },
                loopTargetIsOneWay = false,
                guidAsString = "5160f844-ff79-4d19-b813-38496a344e8e"
            };

            DungeonFlowNode GlitchFlowNode_05 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "5160f844-ff79-4d19-b813-38496a344e8e",
                childNodeGuids = new List<string>() { "2439b6f0-b59e-4b46-8521-3195d72748f7" },
                loopTargetIsOneWay = false,
                guidAsString = "0c8ee6c4-31b4-4226-9ddb-90c7eca8f2d3"
            };

            DungeonFlowNode GlitchFlowNode_06 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "2439b6f0-b59e-4b46-8521-3195d72748f7",
                childNodeGuids = new List<string>() { "989ad791-cfc8-4f4e-afc6-fd9512a789b7" },
                loopTargetIsOneWay = false,
                guidAsString = "a919a262-edf3-47e7-aae9-0eb77fa49262"
            };

            DungeonFlowNode GlitchFlowNode_07 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "919a262-edf3-47e7-aae9-0eb77fa49262",
                childNodeGuids = new List<string>() { "b1da2e8a-afeb-41cc-8840-be1c46aa4401", "3a6325a4-d2c0-4b93-a82e-7f09b007e190" },
                loopTargetIsOneWay = false,
                guidAsString = "989ad791-cfc8-4f4e-afc6-fd9512a789b7"
            };


            DungeonFlowNode GlitchFlowNode_08 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "8267eaa8-ed7f-403b-97a6-421d15a21ef3",
                childNodeGuids = new List<string>() { "3956174b-a5ee-4716-b021-889db041a070" },
                loopTargetIsOneWay = false,
                guidAsString = "8b4c640e-b835-4a6b-9326-7b11d856fcde"
            };

            DungeonFlowNode GlitchFlowNode_09 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "8b4c640e-b835-4a6b-9326-7b11d856fcde",
                childNodeGuids = new List<string>() { "bd36ddc7-e687-4355-a69b-e799c9d857de", "31a9f731-24ba-49dd-9086-2f01cb3fcb1d" },
                loopTargetIsOneWay = false,
                guidAsString = "3956174b-a5ee-4716-b021-889db041a070"
            };

            DungeonFlowNode GlitchFlowNode_10 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "3956174b-a5ee-4716-b021-889db041a070",
                childNodeGuids = new List<string>() { "036aafaf-a754-4410-94c5-2c4e5139a5bf", "17f291e0-37c3-4d03-ba6a-b5b534256c07" },
                loopTargetIsOneWay = false,
                guidAsString = "bd36ddc7-e687-4355-a69b-e799c9d857de"
            };

            DungeonFlowNode GlitchFlowNode_11 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "0c8ee6c4-31b4-4226-9ddb-90c7eca8f2d3",
                childNodeGuids = new List<string>() { "dc3ba41b-dc99-42d3-ab9b-088991bc1741", "a919a262-edf3-47e7-aae9-0eb77fa49262" },
                loopTargetIsOneWay = false,
                guidAsString = "2439b6f0-b59e-4b46-8521-3195d72748f7"
            };

            DungeonFlowNode GlitchFlowNode_12 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.reward_room,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "2439b6f0-b59e-4b46-8521-3195d72748f7",
                childNodeGuids = new List<string>() { "55ebfb7d-b617-4da1-853c-209d3bd36f8e" },
                loopTargetIsOneWay = false,
                guidAsString = "dc3ba41b-dc99-42d3-ab9b-088991bc1741"
            };

            DungeonFlowNode GlitchFlowNode_13 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "dc3ba41b-dc99-42d3-ab9b-088991bc1741",
                childNodeGuids = new List<string>(0),
                loopTargetNodeGuid = "0c8ee6c4-31b4-4226-9ddb-90c7eca8f2d3",
                loopTargetIsOneWay = false,
                guidAsString = "55ebfb7d-b617-4da1-853c-209d3bd36f8e"
            };

            DungeonFlowNode GlitchFlowNode_14 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "8267eaa8-ed7f-403b-97a6-421d15a21ef3",
                childNodeGuids = new List<string>() { "44fc3013-6fa2-4436-a0db-1d3b99484703" },
                loopTargetIsOneWay = false,
                guidAsString = "0fbff154-f8cb-4367-a11f-16f5dd56fe4f"
            };

            DungeonFlowNode GlitchFlowNode_15 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "989ad791-cfc8-4f4e-afc6-fd9512a789b7",
                childNodeGuids = new List<string>() { "8267eaa8-ed7f-403b-97a6-421d15a21ef3" },
                loopTargetIsOneWay = false,
                guidAsString = "b1da2e8a-afeb-41cc-8840-be1c46aa4401"
            };

            DungeonFlowNode GlitchFlowNode_16 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.SPECIAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = null,
                overrideRoomTable = ExpandPrefabs.shop_room_table,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "0fbff154-f8cb-4367-a11f-16f5dd56fe4f",
                childNodeGuids = new List<string>(0),
                loopTargetIsOneWay = false,
                guidAsString = "44fc3013-6fa2-4436-a0db-1d3b99484703"
            };


            DungeonFlowNode GlitchFlowNode_17 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "bd36ddc7-e687-4355-a69b-e799c9d857de",
                childNodeGuids = new List<string>() { "56753489-2944-42ed-8c1f-c0daa03417b0" },
                loopTargetIsOneWay = false,
                guidAsString = "17f291e0-37c3-4d03-ba6a-b5b534256c07"
            };

            DungeonFlowNode GlitchFlowNode_18 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "17f291e0-37c3-4d03-ba6a-b5b534256c07",
                childNodeGuids = new List<string>() { "1d489c84-f1b5-431d-bdf3-e61e74cd7f15", "3e0b1ce9-3862-4041-bfa9-bb82474e567a" },
                loopTargetIsOneWay = false,
                guidAsString = "56753489-2944-42ed-8c1f-c0daa03417b0"
            };

            DungeonFlowNode GlitchFlowNode_19 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.NORMAL,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "56753489-2944-42ed-8c1f-c0daa03417b0",
                childNodeGuids = new List<string>() { "9fc6fab9-fe0f-458c-b1a4-e69077243acc" },
                loopTargetIsOneWay = false,
                guidAsString = "1d489c84-f1b5-431d-bdf3-e61e74cd7f15"
            };

            DungeonFlowNode GlitchFlowNode_20 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.CONNECTOR,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                overrideExactRoom = ExpandPrefabs.reward_room,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "1d489c84-f1b5-431d-bdf3-e61e74cd7f15",
                loopTargetNodeGuid = "bd36ddc7-e687-4355-a69b-e799c9d857de",
                loopTargetIsOneWay = true,
                guidAsString = "9fc6fab9-fe0f-458c-b1a4-e69077243acc"
            };

            DungeonFlowNode GlitchFlowNode_21 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.HUB,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.MANDATORY,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "b1da2e8a-afeb-41cc-8840-be1c46aa4401",
                childNodeGuids = new List<string>() { "0fbff154-f8cb-4367-a11f-16f5dd56fe4f", "8b4c640e-b835-4a6b-9326-7b11d856fcde" },
                loopTargetIsOneWay = false,
                guidAsString = "8267eaa8-ed7f-403b-97a6-421d15a21ef3"
            };

            DungeonFlowNode GlitchFlowNode_22 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.SECRET,
                percentChance = 0.196999997f,
                priority = DungeonFlowNode.NodePriority.OPTIONAL,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "989ad791-cfc8-4f4e-afc6-fd9512a789b7",
                childNodeGuids = new List<string>(0),
                loopTargetIsOneWay = false,
                guidAsString = "3a6325a4-d2c0-4b93-a82e-7f09b007e190"
            };

            DungeonFlowNode GlitchFlowNode_23 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.SECRET,
                percentChance = 1f,
                priority = DungeonFlowNode.NodePriority.OPTIONAL,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "3956174b-a5ee-4716-b021-889db041a070",
                childNodeGuids = new List<string>(0),
                loopTargetIsOneWay = false,
                guidAsString = "31a9f731-24ba-49dd-9086-2f01cb3fcb1d"
            };

            DungeonFlowNode GlitchFlowNode_24 = new DungeonFlowNode(m_CachedFlow) {
                isSubchainStandin = false,
                nodeType = DungeonFlowNode.ControlNodeType.ROOM,
                roomCategory = PrototypeDungeonRoom.RoomCategory.SECRET,
                percentChance = 0.291999996f,
                priority = DungeonFlowNode.NodePriority.OPTIONAL,
                capSubchain = false,
                limitedCopiesOfSubchain = false,
                maxCopiesOfSubchain = 1,
                receivesCaps = false,
                isWarpWingEntrance = false,
                handlesOwnWarping = false,
                forcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                loopForcedDoorType = DungeonFlowNode.ForcedDoorType.NONE,
                nodeExpands = false,
                initialChainPrototype = "n",
                minChainLength = 3,
                maxChainLength = 8,
                minChildrenToBuild = 1,
                maxChildrenToBuild = 1,
                canBuildDuplicateChildren = false,
                subchainIdentifiers = new List<string>(0),
                chainRules = new List<ChainRule>(0),
                flow = m_CachedFlow,
                parentNodeGuid = "56753489-2944-42ed-8c1f-c0daa03417b0",
                childNodeGuids = new List<string>(0),
                loopTargetIsOneWay = false,
                guidAsString = "3e0b1ce9-3862-4041-bfa9-bb82474e567a"
            };

            m_CachedFlow.name = "Custom_GlitchChest_Flow";
            m_CachedFlow.fallbackRoomTable = ExpandPrefabs.CustomRoomTable2;
            m_CachedFlow.subtypeRestrictions = new List<DungeonFlowSubtypeRestriction>() { GlitchTestSubTypeRestriction };
            m_CachedFlow.flowInjectionData = new List<ProceduralFlowModifierData>(0);
            m_CachedFlow.sharedInjectionData = new List<SharedInjectionData>(0);

            m_CachedFlow.Initialize();

            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_00, null);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_01, GlitchFlowNode_10);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_02, GlitchFlowNode_01);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_03, GlitchFlowNode_02);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_04, GlitchFlowNode_00);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_05, GlitchFlowNode_04);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_06, GlitchFlowNode_11);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_07, GlitchFlowNode_06);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_08, GlitchFlowNode_21);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_09, GlitchFlowNode_08);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_10, GlitchFlowNode_09);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_11, GlitchFlowNode_05);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_12, GlitchFlowNode_11);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_13, GlitchFlowNode_12);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_14, GlitchFlowNode_21);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_15, GlitchFlowNode_07);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_16, GlitchFlowNode_14);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_17, GlitchFlowNode_10);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_18, GlitchFlowNode_17);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_19, GlitchFlowNode_18);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_20, GlitchFlowNode_19);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_21, GlitchFlowNode_15);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_22, GlitchFlowNode_07);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_23, GlitchFlowNode_09);
            m_CachedFlow.AddNodeToFlow(GlitchFlowNode_24, GlitchFlowNode_18);

            m_CachedFlow.LoopConnectNodes(GlitchFlowNode_13, GlitchFlowNode_05);
            m_CachedFlow.LoopConnectNodes(GlitchFlowNode_20, GlitchFlowNode_10);

            m_CachedFlow.FirstNode = GlitchFlowNode_00;

            return m_CachedFlow;         
        }
    }
}

