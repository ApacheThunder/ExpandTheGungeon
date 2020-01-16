using System.Collections.Generic;
using UnityEngine;

namespace ExpandTheGungeon.ExpandUtilities {

    public static class FlowHelpers {

		public static void RemoveNodeConnectParentToChildren(DungeonFlowNode node) {
			string parentNodeGuid = node.parentNodeGuid;
			List<string> list = new List<string>(node.childNodeGuids);
			DungeonFlow flow = node.flow;
			node.flow.DeleteNode(node, false);
			foreach (string text in list) {
				bool flag = !string.IsNullOrEmpty(parentNodeGuid) && !string.IsNullOrEmpty(text);
				if (flag) {
					DungeonFlowNode nodeFromGuid = flow.GetNodeFromGuid(parentNodeGuid);
					DungeonFlowNode nodeFromGuid2 = flow.GetNodeFromGuid(text);
					bool flag2 = nodeFromGuid != null && nodeFromGuid2 != null;
					if (flag2) { flow.ConnectNodes(nodeFromGuid, nodeFromGuid2); }
				}
			}
		}

		public static DungeonFlow DuplicateDungeonFlow(DungeonFlow flow) {
			DungeonFlow dungeonFlow = ScriptableObject.CreateInstance<DungeonFlow>();
			dungeonFlow.name = flow.name;
			dungeonFlow.fallbackRoomTable = flow.fallbackRoomTable;
			dungeonFlow.phantomRoomTable = flow.phantomRoomTable;
			dungeonFlow.subtypeRestrictions = flow.subtypeRestrictions;
			dungeonFlow.evolvedRoomTable = flow.evolvedRoomTable;
			ReflectionHelpers.ReflectSetField(typeof(DungeonFlow), "m_firstNodeGuid", ReflectionHelpers.ReflectGetField<string>(typeof(DungeonFlow), "m_firstNodeGuid", flow), dungeonFlow);
			dungeonFlow.flowInjectionData = flow.flowInjectionData;
			dungeonFlow.sharedInjectionData = flow.sharedInjectionData;
			ReflectionHelpers.ReflectSetField(typeof(DungeonFlow), "m_nodeGuids", new List<string>(ReflectionHelpers.ReflectGetField<List<string>>(typeof(DungeonFlow), "m_nodeGuids", flow)), dungeonFlow);
			List<DungeonFlowNode> list = new List<DungeonFlowNode>();
			ReflectionHelpers.ReflectSetField(typeof(DungeonFlow), "m_nodes", list, dungeonFlow);
			foreach (DungeonFlowNode node in flow.AllNodes) {
				DungeonFlowNode dungeonFlowNode = DuplicateDungeonFlowNode(node);
				dungeonFlowNode.flow = dungeonFlow;
				list.Add(dungeonFlowNode);
			}
			return dungeonFlow;
		}

		public static DungeonFlowNode DuplicateDungeonFlowNode(DungeonFlowNode node) {
			DungeonFlowNode dungeonFlowNode = new DungeonFlowNode(node.flow);
			dungeonFlowNode.isSubchainStandin = node.isSubchainStandin;
			dungeonFlowNode.nodeType = node.nodeType;
			dungeonFlowNode.roomCategory = node.roomCategory;
			dungeonFlowNode.percentChance = node.percentChance;
			dungeonFlowNode.priority = node.priority;
			dungeonFlowNode.overrideExactRoom = node.overrideExactRoom;
			dungeonFlowNode.overrideRoomTable = node.overrideRoomTable;
			dungeonFlowNode.capSubchain = node.capSubchain;
			dungeonFlowNode.subchainIdentifier = node.subchainIdentifier;
			dungeonFlowNode.limitedCopiesOfSubchain = node.limitedCopiesOfSubchain;
			dungeonFlowNode.maxCopiesOfSubchain = node.maxCopiesOfSubchain;
			dungeonFlowNode.subchainIdentifiers = new List<string>(node.subchainIdentifiers);
			dungeonFlowNode.receivesCaps = node.receivesCaps;
			dungeonFlowNode.isWarpWingEntrance = node.isWarpWingEntrance;
			dungeonFlowNode.handlesOwnWarping = node.handlesOwnWarping;
			dungeonFlowNode.forcedDoorType = node.forcedDoorType;
			dungeonFlowNode.loopForcedDoorType = node.loopForcedDoorType;
			dungeonFlowNode.nodeExpands = node.nodeExpands;
			dungeonFlowNode.initialChainPrototype = node.initialChainPrototype;
			dungeonFlowNode.chainRules = new List<ChainRule>(node.chainRules.Count);
			foreach (ChainRule chainRule in node.chainRules) {
				dungeonFlowNode.chainRules.Add(new ChainRule { form = chainRule.form, target = chainRule.target, weight = chainRule.weight, mandatory = chainRule.mandatory });
			}
			dungeonFlowNode.minChainLength = node.minChainLength;
			dungeonFlowNode.maxChainLength = node.maxChainLength;
			dungeonFlowNode.minChildrenToBuild = node.minChildrenToBuild;
			dungeonFlowNode.maxChildrenToBuild = node.maxChildrenToBuild;
			dungeonFlowNode.canBuildDuplicateChildren = node.canBuildDuplicateChildren;
			dungeonFlowNode.parentNodeGuid = node.parentNodeGuid;
			dungeonFlowNode.childNodeGuids = new List<string>(node.childNodeGuids);
			dungeonFlowNode.loopTargetNodeGuid = node.loopTargetNodeGuid;
			dungeonFlowNode.loopTargetIsOneWay = node.loopTargetIsOneWay;
			dungeonFlowNode.guidAsString = node.guidAsString;
			dungeonFlowNode.flow = node.flow;
			return dungeonFlowNode;
		}

		/*public static void PrintBossManager() {
			Console.WriteLine(string.Format("{0}.PrintBossManager(): Currently loaded BossManager data below (incomplete output).", typeof(FlowHelpers)));
			Console.WriteLine(string.Format("Prior floor selected boss room: {0}", BossManager.PriorFloorSelectedBossRoom));
			foreach (BossFloorEntry bossFloorEntry in GameManager.Instance.BossManager.BossFloorData) {
				Console.WriteLine(string.Format("BossFloorEntry: {0} '{1}' '{2}'", bossFloorEntry.AssociatedTilesets, bossFloorEntry.Annotation, bossFloorEntry.ToString()));
				foreach (IndividualBossFloorEntry individualBossFloorEntry in bossFloorEntry.Bosses) {
					Console.WriteLine(string.Format(" Individual: '{0}' ({1}) (#prerequisites={2})", individualBossFloorEntry.ToString(), individualBossFloorEntry.BossWeight, individualBossFloorEntry.GlobalBossPrerequisites.Length));
					foreach (WeightedRoom weightedRoom in individualBossFloorEntry.TargetRoomTable.GetCompiledList()) {
						Console.WriteLine(string.Format("  Room: '{0}' (#prerequisites={1})", weightedRoom.room, weightedRoom.additionalPrerequisites.Length));
					}
				}
			}
		}

		public static void PrintFlow(DungeonFlow flow) {
			bool flag = flow == null;
			if (!flag) {
				try {
					Console.WriteLine(string.Format("DungeonFlow: '{0}' '{1}'", flow, flow.name));
					bool flag2 = flow.flowInjectionData != null;
					if (flag2) {
						foreach (ProceduralFlowModifierData arg in flow.flowInjectionData) {
							Console.WriteLine(string.Format(" ProceduralFlowModifierData: {0}", arg));
						}
					}
					bool flag3 = flow.sharedInjectionData != null;
					if (flag3) {
						foreach (SharedInjectionData arg2 in flow.sharedInjectionData) {
							Console.WriteLine(string.Format(" SharedInjectionData: {0}", arg2));
						}
					}
					bool flag4 = flow.phantomRoomTable != null;
					if (flag4) {
						foreach (WeightedRoom weightedRoom in flow.phantomRoomTable.GetCompiledList()) {
							string format = "  PhantomRoom: '{0}' '{1}'";
							object arg3 = weightedRoom;
							PrototypeDungeonRoom room = weightedRoom.room;
							Console.WriteLine(string.Format(format, arg3, (room != null) ? room.name : null));
						}
					}
					bool flag5 = flow.fallbackRoomTable != null;
					if (flag5) {
						foreach (WeightedRoom weightedRoom2 in flow.fallbackRoomTable.GetCompiledList()) {
							string format2 = "  FallbackRoom: '{0}' '{1}'";
							object arg4 = weightedRoom2;
							PrototypeDungeonRoom room2 = weightedRoom2.room;
							Console.WriteLine(string.Format(format2, arg4, (room2 != null) ? room2.name : null));
						}
					}
					foreach (DungeonFlowNode dungeonFlowNode in flow.AllNodes) {
						Console.WriteLine(string.Format(" Flow Node: {0} {1} (iswarpwingentrance={2}) ({3}) (globalboss={4}) (roomcategory={5}) (override={6})", new object[] {
							dungeonFlowNode.priority,
							dungeonFlowNode.guidAsString,
							dungeonFlowNode.isWarpWingEntrance,
							dungeonFlowNode.handlesOwnWarping,
							dungeonFlowNode.UsesGlobalBossData,
							dungeonFlowNode.roomCategory,
							dungeonFlowNode.overrideExactRoom
						}));
						bool flag6 = dungeonFlowNode.overrideRoomTable != null;
						if (flag6) {
							foreach (WeightedRoom weightedRoom3 in dungeonFlowNode.overrideRoomTable.GetCompiledList()) {
								string str = "  Possible Room Table: ";
								PrototypeDungeonRoom room3 = weightedRoom3.room;
								Console.WriteLine(str + ((room3 != null) ? room3.name : null));
							}
						}
						Console.WriteLine("  Parent: " + dungeonFlowNode.parentNodeGuid);
						foreach (string str2 in dungeonFlowNode.childNodeGuids) {
							Console.WriteLine("  Child: " + str2);
						}
						Console.WriteLine(string.Format("  Loop: {0} {1} {2}", dungeonFlowNode.loopForcedDoorType, dungeonFlowNode.loopTargetIsOneWay, dungeonFlowNode.loopTargetNodeGuid));
						Console.WriteLine(string.Format("  Subchain Identifiers: '{0}' #{1} ('{2}')", dungeonFlowNode.subchainIdentifier, dungeonFlowNode.subchainIdentifiers.Count, string.Join("','", dungeonFlowNode.subchainIdentifiers.ToArray())));
						Console.WriteLine(string.Format("  Door Types: {0} {1}", dungeonFlowNode.forcedDoorType, dungeonFlowNode.loopForcedDoorType));
						foreach (ChainRule chainRule in dungeonFlowNode.chainRules) {
							Console.WriteLine(string.Format("  Chain Rule: {0} '{1}' '{2}' {3}", new object[] {
								chainRule.mandatory,
								chainRule.form,
								chainRule.target,
								chainRule.weight
							}));
						}
					}
				} catch (Exception ex) {
					Console.WriteLine("Exception caught and will not cause errors. Just fix the printing code instead!");
					Console.WriteLine(ex.ToString());
				}
			}
		}*/
	}
}

