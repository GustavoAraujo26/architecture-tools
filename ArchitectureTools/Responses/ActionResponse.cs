using ArchitectureTools.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ArchitectureTools.Responses
{
    /// <summary>
    /// Container de resposta de ação
    /// </summary>
    /// <typeparam name="TValue">Tipo da classe de retorno ou struct</typeparam>
    public struct ActionResponse<TValue>
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="status">Status</param>
        /// <param name="message">Mensagem</param>
        /// <param name="stackTrace">Rastreamento de pilha</param>
        /// <param name="traceId">ID de rastreamento</param>
        /// <param name="content">Conteúdo do container para retorno</param>
        /// <param name="validations">Lista de validações</param>
        [JsonConstructor]
        public ActionResponse(HttpStatusCode status, string message,
            string? stackTrace = null, string? traceId = null, TValue content = default, 
            List<ValidationResponse> validations = null)
        {
            Status = status;
            Message = message;
            StackTrace = stackTrace;
            TraceId = traceId;
            Content = content;

            if (validations is null)
                Validations = new List<ValidationResponse>();
            else
                Validations = validations;
        }

        /// <summary>
        /// Status
        /// </summary>
        [JsonInclude]
        public HttpStatusCode Status { get; private set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        [JsonInclude]
        public string Message { get; private set; }

        /// <summary>
        /// Rastreamento de pilha
        /// </summary>
        [JsonInclude]
        public string? StackTrace { get; private set; }

        /// <summary>
        /// Id de rastreamento
        /// </summary>
        [JsonInclude]
        public string? TraceId { get; private set; }

        /// <summary>
        /// Conteúdo do container para retorno
        /// </summary>
        [JsonInclude]
        public TValue Content { get; private set; }

        /// <summary>
        /// Lista de validações
        /// </summary>
        [JsonInclude]
        public List<ValidationResponse> Validations { get; private set; }

        /// <summary>
        /// Verifica se o status do processamento é de sucesso
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return (int)Status >= 200 && (int)Status <= 299;
            }
        }

        /// <summary>
        /// Verifica se o status do processamento é de erro
        /// </summary>
        public bool IsFailure
        {
            get
            {
                return !IsSuccess;
            }
        }

        /// <summary>
        /// Verifica se o processamento obteve uma falha parcial
        /// </summary>
        public bool PartialFailure
        {
            get
            {
                return (int)Status >= 400 && (int)Status <= 499;
            }
        }

        /// <summary>
        /// Verifica se o processamento obteve uma falha permanente
        /// </summary>
        public bool PermanentFailure
        {
            get
            {
                return (int)Status >= 500 && (int)Status <= 599;
            }
        }

        /// <summary>
        /// Retorna o status HTTP em formato de número
        /// </summary>
        public int StatusNumber
        {
            get
            {
                return (int)Status;
            }
        }

        /// <summary>
        /// Retorna a descrição do status HTTP
        /// </summary>
        public string StatusDescription
        {
            get
            {
                return Status.GetDescription();
            }
        }

        /// <summary>
        /// Retorna JSON do container de resposta
        /// </summary>
        /// <returns>Json de resposta</returns>
        public override string ToString() => JsonSerializer.Serialize(this);

        /// <summary>
        /// Deserializa JSON no container especificado
        /// </summary>
        /// <param name="json">JSON a ser deserializado</param>
        /// <returns>Container deserializado</returns>
        public static ActionResponse<TValue> Deserialize(string json) =>
            JsonSerializer.Deserialize<ActionResponse<TValue>>(json);

        /// <summary>
        /// Constrói container de retorno de sucesso, com possibilidade de repassar mensagem
        /// </summary>
        /// <param name="message">Mensagem de sucesso a ser repassada</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> Ok(string? message = null) =>
            new ActionResponse<TValue>(HttpStatusCode.OK,
                string.IsNullOrEmpty(message) ? HttpStatusCode.OK.ToString() : message);

        /// <summary>
        /// Constrói container de retorno de sucesso, com objeto de retorno
        /// </summary>
        /// <param name="content">Objeto a ser retornado</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> Ok(TValue content) =>
            new ActionResponse<TValue>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), null, null, content);

        /// <summary>
        /// Constrói container de retorno de "requisição ruim", 
        /// com possibilidade de mensagem de retorno e caminho percorrido no sistema
        /// </summary>
        /// <param name="message">Mensagem a ser retornada</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> BadRequest(string? message = null, string? stackTrace = null) =>
            new ActionResponse<TValue>(HttpStatusCode.BadRequest,
                string.IsNullOrEmpty(message) ? HttpStatusCode.BadRequest.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de "recurso não encontrado", 
        /// com possibilidade de mensagem de retorno e caminho percorrido
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> NotFound(string? message = null, string? stackTrace = null) =>
            new ActionResponse<TValue>(HttpStatusCode.NotFound,
                string.IsNullOrEmpty(message) ? HttpStatusCode.NotFound.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de "erro interno do sistema", 
        /// com possibilidade de mensagem de retorno e caminho do sistema
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> InternalError(string? message = null, string? stackTrace = null) =>
            new ActionResponse<TValue>(HttpStatusCode.InternalServerError,
                string.IsNullOrEmpty(message) ? HttpStatusCode.InternalServerError.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de "erro interno do sistema", 
        /// através de uma exceção
        /// </summary>
        /// <param name="exception">Exceção do sistema</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> InternalError(Exception exception) =>
            new ActionResponse<TValue>(HttpStatusCode.InternalServerError, exception.Message, exception.StackTrace);

        /// <summary>
        /// Constrói container de retorno de validação, com possibilidade de mensagem de retorno e caminho do sistema
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> UnprocessableEntity(string? message = null, string? stackTrace = null) =>
            new ActionResponse<TValue>(HttpStatusCode.UnprocessableEntity,
                string.IsNullOrEmpty(message) ? HttpStatusCode.UnprocessableEntity.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de validação, através de lista de mensagens de erro
        /// </summary>
        /// <param name="validationMessages">Lista de mensagens de erro</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> UnprocessableEntity(List<string> validationMessages)
        {
            List<string> errorsFormatted = new List<string>();

            foreach (var message in validationMessages)
                errorsFormatted.Add(message);

            string errorMessage = $"Invalid data! {string.Join(", ", errorsFormatted)}";

            return new ActionResponse<TValue>(HttpStatusCode.UnprocessableEntity, errorMessage);
        }

        /// <summary>
        /// Constrói container de retorno de validação, através de lista de validações
        /// </summary>
        /// <param name="validations">Lista de validações</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> UnprocessableEntity(List<ValidationResponse> validations) =>
            new ActionResponse<TValue>(HttpStatusCode.UnprocessableEntity, null, null, null, default, validations);

        /// <summary>
        /// Constrói container de retorno de "não autorizado", 
        /// com possibilidade de mensagem de retorno e caminho do sistema
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> Unauthorized(string? message = null, string? stackTrace = null) =>
            new ActionResponse<TValue>(HttpStatusCode.Unauthorized,
                string.IsNullOrEmpty(message) ? HttpStatusCode.Unauthorized.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno customizado
        /// </summary>
        /// <param name="status">Status HTTP do processamento</param>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <param name="traceId">ID de rastreamento</param>
        /// <param name="content">Conteúdo do container para retorno</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> Custom(HttpStatusCode status, string? message = null,
            string? stackTrace = null, string? traceId = null, TValue content = default) =>
            new ActionResponse<TValue>(status,
                string.IsNullOrEmpty(message) ? status.ToString() : message,
                stackTrace, traceId, content);

        /// <summary>
        /// Constrói container de retorno com base em uma mensagem de resposta de chamada HTTP
        /// </summary>
        /// <param name="httpResponseMessage">Mensagem de resposta de chamada HTTP</param>
        /// <returns>Container de resposta</returns>
        public static async Task<ActionResponse<TValue>> FromHttpResponse(HttpResponseMessage httpResponseMessage)
        {
            try
            {
                var messageContent = await httpResponseMessage.Content.ReadAsStringAsync();

                if (!httpResponseMessage.IsSuccessStatusCode)
                    return ActionResponse<TValue>.Custom(httpResponseMessage.StatusCode, messageContent);

                var responseContent = JsonSerializer.Deserialize<TValue>(messageContent);
                return ActionResponse<TValue>.Ok(responseContent);
            }
            catch (Exception ex)
            {
                return ActionResponse<TValue>.InternalError(ex);
            }
        }

        /// <summary>
        /// Copia conteúdo de um container
        /// </summary>
        /// <typeparam name="Origin">Tipo do conteúdo do container de origem</typeparam>
        /// <param name="origin">Container de origem</param>
        /// <param name="content">Novo conteúdo</param>
        /// <returns>Container de resposta</returns>
        public static ActionResponse<TValue> Copy<Origin>(ActionResponse<Origin> origin,
            TValue content = default) =>
            new ActionResponse<TValue>(origin.Status, origin.Message, origin.StackTrace,
                origin.TraceId, content, origin.Validations);
    }
}
