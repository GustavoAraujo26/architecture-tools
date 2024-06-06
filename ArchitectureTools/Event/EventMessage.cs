using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Event
{
    /// <summary>
    /// Mensagem de evento
    /// </summary>
    /// <typeparam name="TResult">Tipo do conteúdo da mensagem</typeparam>
    public struct EventMessage<TResult>
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
            string type, TResult content, EventMessageType eventType)
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
        public TResult Content { get; private set; }

        /// <summary>
        /// Tipo do evento
        /// </summary>
        public EventMessageType EventType { get; private set; }

        /// <summary>
        /// Retorna JSON da mensagem
        /// </summary>
        /// <returns>JSON</returns>
        public override string ToString() => JsonSerializer.Serialize(this);

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
        public static EventMessage<TResult> FromBytes(byte[] bytes) =>
            FromJson(Encoding.UTF8.GetString(bytes));

        /// <summary>
        /// Converte JSON na mensagem do evento
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>Mensagem do evento</returns>
        public static EventMessage<TResult> FromJson(string json) =>
            JsonSerializer.Deserialize<EventMessage<TResult>>(json);
    }
}
