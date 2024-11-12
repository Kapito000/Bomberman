using System;

namespace FluentBehaviourTree.Nodes
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
        

        public ActionNode(string name, Func<BehaviourTreeStatus> fn)
        {
            this.name=name;
            this.fn=fn;
        }

        public BehaviourTreeStatus Tick()
        {
            return fn();
        }
    }
}
