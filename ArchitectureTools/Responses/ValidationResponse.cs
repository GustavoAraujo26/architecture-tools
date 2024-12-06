using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Responses
{
    /// <summary>
    /// Container resposta de validações
    /// </summary>
    public class ValidationResponse
    {
        /// <summary>
        /// Inicializa as propriedades
        /// </summary>
        /// <param name="code">Código da validação</param>
        /// <param name="message">Mesagem</param>
        /// <param name="propertyName">Nome da propriedade</param>
        /// <param name="stackTrace">Caminho da pilha da aplicação</param>
        [JsonConstructor]
        public ValidationResponse(string message, string? code = null, string? propertyName = null, 
            string? stackTrace = null)
        {
            Code = code;
            Message = message;
            PropertyName = propertyName;
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Código
        /// </summary>
        [JsonInclude]
        public string? Code { get; private set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        [JsonInclude]
        public string Message { get; private set; }

        /// <summary>
        /// Nome da propriedade
        /// </summary>
        [JsonInclude]
        public string? PropertyName { get; private set; }

        /// <summary>
        /// Caminho da pilha da aplicação
        /// </summary>
        [JsonInclude]
        public string? StackTrace { get; private set; }

        /// <summary>
        /// Converte resposta para JSON
        /// </summary>
        /// <returns>JSON</returns>
        public override string ToString() => 
            JsonSerializer.Serialize(this);

        /// <summary>
        /// Constrói novo container
        /// </summary>
        /// <param name="code">Código da validação</param>
        /// <param name="message">Mesagem</param>
        /// <param name="propertyName">Nome da propriedade</param>
        /// <param name="stackTrace">Caminho da pilha da aplicação</param>
        /// <returns>Container de resposta de validação</returns>
        public static ValidationResponse Build(string message, string? code = null, string? propertyName = null, 
            string? stackTrace = null) => 
            new ValidationResponse(message, code, propertyName, stackTrace);

        /// <summary>
        /// Deserializa JSON no container resposta
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>Container resposta</returns>
        public static ValidationResponse Deserialize(string json) =>
            JsonSerializer.Deserialize<ValidationResponse>(json);

        /// <summary>
        /// Deserializa JSON em uma lista de containers de resposta
        /// </summary>
        /// <param name="json">JSON</param>
        /// <returns>Lista de containers de resposta</returns>
        public static List<ValidationResponse> DeserializeList(string json) =>
            JsonSerializer.Deserialize<List<ValidationResponse>>(json);
    }
}
