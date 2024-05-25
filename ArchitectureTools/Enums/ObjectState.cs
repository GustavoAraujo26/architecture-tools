using System.ComponentModel;

namespace ArchitectureTools.Enums
{
    /// <summary>
    /// Estado do objeto
    /// </summary>
    public enum ObjectState
    {
        /// <summary>
        /// Habilitado
        /// </summary>
        [Description("Enabled")]
        Enabled = 1,
        /// <summary>
        /// Desabilitado
        /// </summary>
        [Description("Disabled")]
        Disabled = 2
    }
}
