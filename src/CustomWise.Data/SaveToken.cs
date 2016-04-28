namespace CustomWise.Data {
    public struct SaveToken {
        /// <summary>
        /// Gets a value indicating whether [exist save].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exist save]; otherwise, <c>false</c>.
        /// </value>
        public bool ExistSave { get; private set; }
        /// <summary>
        /// Gets a value indicating whether [exit all].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [exit all]; otherwise, <c>false</c>.
        /// </value>
        public bool ExitAll { get; private set; }
        /// <summary>
        /// Sets the exit all.
        /// </summary>
        public void SetExitAll() => ExitAll = ExistSave = true;
        /// <summary>
        /// Creates an empty <see cref="SaveToken"/>.
        /// </summary>
        /// <returns></returns>
        public static SaveToken Empty() => new SaveToken();
    }
}
