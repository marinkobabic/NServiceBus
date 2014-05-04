namespace NServiceBus.MessageMutator
{
    /// <summary>
    /// Mutates incoming messages
    /// </summary>
    public interface IMutateIncomingMessages
    {
        /// <summary>
        /// Mutates the given message right after it has been deseralized
        /// </summary>
        object MutateIncoming(object message);
    }
}