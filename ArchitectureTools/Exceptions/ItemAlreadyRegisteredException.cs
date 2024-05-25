using System;

namespace ArchitectureTools.Exceptions
{
    /// <summary>
    /// Exceção para item já registrado em um recurso/lista
    /// </summary>
    public sealed class ItemAlreadyRegisteredException : Exception
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="key">Chave</param>
        public ItemAlreadyRegisteredException(string key) : base($"Item {key} already registered!") { }
    }
}
