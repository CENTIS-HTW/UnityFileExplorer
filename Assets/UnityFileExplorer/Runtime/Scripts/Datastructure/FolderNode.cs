using System;
using System.Collections.Generic;
using UnityEngine;

namespace CENTIS.UnityFileExplorer.Datastructure
{
	internal class FolderNode : VirtualFolderNode, IEquatable<FolderNode>
	{
		public UINode UIInstance { get; }

		public FolderNode(ExplorerConfiguration config, NodeInformation info, VirtualFolderNode parent, List<TreeNode> children = null)
			: base(config, info, parent, children)
		{
			UIInstance = GameObject.Instantiate(
				config.FolderPrefab,
				config.NodeContainer.transform);
			UIInstance.Initialize(info);
			UIInstance.gameObject.SetActive(false);
			UIInstance.OnSelected += () => Select(this);
			UIInstance.OnDeselected += () => Deselect(this);
			UIInstance.OnActivated += () => Activate(this);
		}

		public bool Equals(FolderNode other)
		{
			if (other == null) return false;
			if (other is not FolderNode folderNode) return false;
			if (!GetHashCode().Equals(folderNode.GetHashCode())) return false;
			return ToString().Equals(other.ToString());
		}

		public override void Show()
		{
			UIInstance.gameObject.SetActive(true);
		}

		public override void Hide()
		{
			UIInstance.gameObject.SetActive(false);
		}

		public override void Unload()
		{
			base.Unload();

			if (UIInstance != null)
			{
				UIInstance.OnSelected -= () => Select(this);
				UIInstance.OnDeselected -= () => Deselect(this);
				UIInstance.OnActivated -= () => Activate(this);
				GameObject.Destroy(UIInstance.gameObject);
			}
		}

		public override void OnFailedToLoad(ENodeFailedToLoad reason)
		{
			UIInstance.OnFailedToLoad(reason);
		}
	}
}
