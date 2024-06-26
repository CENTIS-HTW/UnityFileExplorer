using System;
using System.Collections.Generic;

namespace CENTIS.UnityFileExplorer.Datastructure
{
	internal class VirtualFolderNode : TreeNode, IEquatable<VirtualFolderNode>
	{
		public List<TreeNode> Children { get; private set; }
		public bool IsFolderLoaded => Children != null;

		public VirtualFolderNode(ExplorerConfiguration config, NodeInformation info, VirtualFolderNode parent, List<TreeNode> children = null)
			: base(config, info, parent)
		{
			Children = children;
		}

		public bool Equals(VirtualFolderNode other)
		{
			if (other == null) return false;
			if (other is not VirtualFolderNode virtualNode) return false;
			if (!GetHashCode().Equals(virtualNode.GetHashCode())) return false;
			return ToString().Equals(other.ToString());
		}

		public override void Show()
		{
		}

		public override void Hide()
		{
		}

		public override void Unload()
		{
			if (Children != null)
			{
				foreach (TreeNode child in Children)
					child.Unload();
				Children = null;
			}
		}

		public override void OnFailedToLoad(ENodeFailedToLoad reason)
		{ 
		}

		public void AddChild(TreeNode child)
		{
			Children ??= new();
			Children.Add(child);
		}

		public void NavigateTo()
		{
			if (Children == null)
				throw new NullReferenceException("Folder with unloaded children was opened!");

			foreach (TreeNode child in Children)
				child.Show();
		}

		public void NavigateFrom()
		{
			if (Children == null)
				throw new NullReferenceException("Folder with unloaded children was opened!");

			foreach (TreeNode child in Children)
				child.Hide();
		}
	}
}
