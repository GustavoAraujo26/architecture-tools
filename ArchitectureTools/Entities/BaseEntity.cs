using System;

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
        /// <param name="state">Estado da entidade</param>
        protected BaseEntity(Guid id, EntityState state)
        {
            Id = id;
            State = state;
        }

        /// <summary>
        /// Identificador da entidade
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Estado da entidade
        /// </summary>
        public EntityState State { get; private set; }
    }
}
