using ArchitectureTools.Enums;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Structs
{
    /// <summary>
    /// Mensagem de evento
    /// </summary>
    /// <typeparam name="T">Tipo do conteúdo da mensagem</typeparam>
    public struct EventMessage<T> where T : struct
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="requestId">ID da requisição</param>
        /// <param name="createdAt">Data de criação</param>
        /// <param name="type">Tipo da mensagem</param>
        /// <param name="content">Conteúdo da mensagem</param>
        /// <param name="eventType">Tipo do evento</param>
        [JsonConstructor]
        public EventMessage(Guid requestId, DateTime createdAt, 
            string type, T content, EventMessageType eventType)
        {
            RequestId = requestId;
            CreatedAt = createdAt;
            Type = type;
            Content = content;
            EventType = eventType;
        }

        /// <summary>
        /// ID da requisição
        /// </summary>
        public Guid RequestId { get; private set; }

        /// <summary>
        /// Data de criação
        /// </summary>
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Tipo da mensagem
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Conteúdo da mensagem
        /// </summary>
        public T Content { get; private set; }

        /// <summary>
        /// Tipo do evento
        /// </summary>
        public EventMessageType EventType { get; private set; }

        /// <summary>
        /// Retorna JSON da mensagem
        /// </summary>
        /// <returns>JSON</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Retorna array de bytes da mensagem
        /// </summary>
        /// <returns>Array de bytes</returns>
        public ReadOnlyMemory<byte> ToBytes() => 
            Encoding.UTF8.GetBytes(ToString());

        /// <summary>
        /// Converte array de bytes na mensagem do evento
        /// </summary>
        /// <param name="bytes">Array de bytes</param>
        /// <returns>Mensagem do evento</returns>
        public static EventMessage<T> FromBytes(byte[] bytes) =>
            FromJson(Encoding.UTF8.GetString(bytes));

        /// <summary>
        /// Converte JSON na mensagem do evento
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>Mensagem do evento</returns>
        public static EventMessage<T> FromJson(string json) =>
            JsonSerializer.Deserialize<EventMessage<T>>(json);
    }
}
