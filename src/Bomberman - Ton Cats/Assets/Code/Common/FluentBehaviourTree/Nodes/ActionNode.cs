using System;

namespace Common.FluentBehaviourTree.Nodes
{
    /// <summary>
    /// A behaviour tree leaf node for running an action.
    /// </summary>
    public class ActionNode : IBehaviourTreeNode
    {
        /// <summary>
        /// The name of the node.
        /// </summary>
        private string name;

        /// <summary>
        /// Function to invoke for the action.
        /// </summary>
        private Func<BehaviourTreeStatus> fn;
        

        public ActionNode(Func<BehaviourTreeStatus> fn, string name = null)
        {
            this.name=name;
            this.fn=fn;
        }

        public BehaviourTreeStatus Process()
        {
            return fn();
        }
    }
}
