using ArchitectureTools.Enums;
using System;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Structs
{
    /// <summary>
    /// Estado da entidade
    /// </summary>
    public struct EntityState
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="state">Estado</param>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="updatedAt">Data de alteração</param>
        [JsonConstructor]
        public EntityState(ObjectState state, DateTime createdAt, DateTime updatedAt)
        {
            State = state;
            CreatedAt = createdAt;
            UpdatedAt = createdAt;
        }

        /// <summary>
        /// Estado
        /// </summary>
        public ObjectState State { get; private set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Data de alteração
        /// </summary>
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// Cria novo estado
        /// </summary>
        /// <returns>Estado da entidade</returns>
        public static EntityState Create() =>
            new EntityState(ObjectState.Enabled, DateTime.Now, DateTime.Now);

        /// <summary>
        /// Atualiza data de alteração
        /// </summary>
        public void Update() => UpdatedAt = DateTime.Now;

        /// <summary>
        /// Altera o estado para "habilitado"
        /// </summary>
        public void Enable()
        {
            State = ObjectState.Enabled;
            UpdatedAt = DateTime.Now;
        }

        /// <summary>
        /// Altera o estado para "desabilitado"
        /// </summary>
        public void Disable()
        {
            State = ObjectState.Disabled;
            UpdatedAt = DateTime.Now;
        }
    }
}
