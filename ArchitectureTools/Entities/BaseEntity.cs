using System;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Entities
{
    /// <summary>
    /// Dados básicos de uma entidade
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="id">Identificador da entidade</param>
        /// <param name="stateControl">Estado da entidade</param>
        protected BaseEntity(Guid id, EntityState stateControl)
        {
            Id = id;
            StateControl = stateControl;
        }

        /// <summary>
        /// Identificador da entidade
        /// </summary>
        [JsonInclude]
        public Guid Id { get; private set; }

        /// <summary>
        /// Estado da entidade
        /// </summary>
        [JsonInclude]
        public EntityState StateControl { get; private set; }
    }
}
