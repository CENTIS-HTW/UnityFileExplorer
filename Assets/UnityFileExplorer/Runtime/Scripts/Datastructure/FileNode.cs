using System;
using UnityEngine;

namespace CENTIS.UnityFileExplorer.Datastructure
{
	internal class FileNode : TreeNode, IEquatable<FileNode>
	{
		public UINode UIInstance { get; }

		public FileNode(ExplorerConfiguration config, NodeInformation info, VirtualFolderNode parent)
			: base(config, info, parent)
		{
			UIInstance = GameObject.Instantiate(
				config.FilePrefab,
				config.NodeContainer.transform);
			UIInstance.Initialize(info);
			UIInstance.gameObject.SetActive(false);
			UIInstance.OnSelected += () => Select(this);
			UIInstance.OnDeselected += () => Deselect(this);
			UIInstance.OnActivated += () => Activate(this);
		}

		public bool Equals(FileNode other)
		{
			if (other == null) return false;
			if (other is not FileNode fileNode) return false;
			if (!GetHashCode().Equals(fileNode.GetHashCode())) return false;
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
			if (UIInstance == null) return;

			UIInstance.OnSelected -= () => Select(this);
			UIInstance.OnDeselected -= () => Deselect(this);
			UIInstance.OnActivated -= () => Activate(this);
			GameObject.Destroy(UIInstance.gameObject);
		}

		public override void OnFailedToLoad(ENodeFailedToLoad reason)
		{
			UIInstance.OnFailedToLoad(reason);
		}
	}
}
