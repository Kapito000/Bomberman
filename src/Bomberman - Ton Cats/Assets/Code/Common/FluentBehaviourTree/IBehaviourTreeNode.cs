﻿namespace Common.FluentBehaviourTree
{
    /// <summary>
    /// Interface for behaviour tree nodes.
    /// </summary>
    public interface IBehaviourTreeNode
    {
        /// <summary>
        /// Update the time of the behaviour tree.
        /// </summary>
        BehaviourTreeStatus Process();
    }
}
