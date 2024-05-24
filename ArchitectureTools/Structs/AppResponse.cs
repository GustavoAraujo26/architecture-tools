using ArchitectureTools.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ArchitectureTools.Structs
{
    /// <summary>
    /// Container de retorno para aplicações, para implementãção do design pattern "Result"
    /// </summary>
    /// <typeparam name="T">Tipo da classe de retorno</typeparam>
    public struct AppResponse<T> where T : class
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="status">Status do processamento (HTTP)</param>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho da execução do processamento</param>
        /// <param name="content">Conteudo a ser retornado</param>
        [JsonConstructor]
        public AppResponse(HttpStatusCode status, string? message = null,
            string? stackTrace = null, T? content = null)
        {
            Status = status;
            Message = message;
            StackTrace = stackTrace;
            Content = content;
        }

        /// <summary>
        /// Status do processamento (HTTP)
        /// </summary>
        public HttpStatusCode Status { get; private set; }

        /// <summary>
        /// Mensagem de retorno
        /// </summary>
        public string? Message { get; private set; }

        /// <summary>
        /// Caminho da execução do processamento
        /// </summary>
        public string? StackTrace { get; private set; }

        /// <summary>
        /// Conteudo a ser retornado
        /// </summary>
        public T? Content { get; private set; }

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
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        /// <summary>
        /// Deserializa JSON no container especificado
        /// </summary>
        /// <typeparam name="T">Tipo do conteudo</typeparam>
        /// <param name="json">JSON a ser deserializado</param>
        /// <returns>Container deserializado</returns>
        public static AppResponse<T> Deserialize(string json) =>
            JsonSerializer.Deserialize<AppResponse<T>>(json);

        /// <summary>
        /// Constrói container de retorno de sucesso, com possibilidade de repassar mensagem
        /// </summary>
        /// <param name="message">Mensagem de sucesso a ser repassada</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> Ok(string? message = null) =>
            new AppResponse<T>(HttpStatusCode.OK,
                string.IsNullOrEmpty(message) ? HttpStatusCode.OK.ToString() : message, null, null);

        /// <summary>
        /// Constrói container de retorno de sucesso, com objeto de retorno
        /// </summary>
        /// <param name="content">Objeto a ser retornado</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> Ok(T content) =>
            new AppResponse<T>(HttpStatusCode.OK, HttpStatusCode.OK.ToString(), null, content);

        /// <summary>
        /// Constrói container de retorno de "requisição ruim", 
        /// com possibilidade de mensagem de retorno e caminho percorrido no sistema
        /// </summary>
        /// <param name="message">Mensagem a ser retornada</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> BadRequest(string? message = null, string? stackTrace = null) =>
            new AppResponse<T>(HttpStatusCode.BadRequest,
                string.IsNullOrEmpty(message) ? HttpStatusCode.BadRequest.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de "recurso não encontrado", 
        /// com possibilidade de mensagem de retorno e caminho percorrido
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> NotFound(string? message = null, string? stackTrace = null) =>
            new AppResponse<T>(HttpStatusCode.NotFound,
                string.IsNullOrEmpty(message) ? HttpStatusCode.NotFound.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de "erro interno do sistema", 
        /// com possibilidade de mensagem de retorno e caminho do sistema
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> InternalError(string? message = null, string? stackTrace = null) =>
            new AppResponse<T>(HttpStatusCode.InternalServerError,
                string.IsNullOrEmpty(message) ? HttpStatusCode.InternalServerError.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de "erro interno do sistema", 
        /// através de uma exceção
        /// </summary>
        /// <param name="exception">Exceção do sistema</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> InternalError(Exception exception) =>
            new AppResponse<T>(HttpStatusCode.InternalServerError, exception.Message, exception.StackTrace);

        /// <summary>
        /// Constrói container de retorno de validação, com possibilidade de mensagem de retorno e caminho do sistema
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> UnprocessableEntity(string? message = null, string? stackTrace = null) =>
            new AppResponse<T>(HttpStatusCode.UnprocessableEntity,
                string.IsNullOrEmpty(message) ? HttpStatusCode.UnprocessableEntity.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno de validação, através de lista de mensagens de erro
        /// </summary>
        /// <param name="validationMessages">Lista de mensagens de erro</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> UnprocessableEntity(List<string> validationMessages)
        {
            List<string> errorsFormatted = new List<string>();

            foreach (var message in validationMessages)
                errorsFormatted.Add(message);

            string errorMessage = $"Invalid data! {string.Join(", ", errorsFormatted)}";

            return new AppResponse<T>(HttpStatusCode.UnprocessableEntity, errorMessage);
        }

        /// <summary>
        /// Constrói container de retorno de "não autorizado", 
        /// com possibilidade de mensagem de retorno e caminho do sistema
        /// </summary>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <returns>Container de resposta</returns>
        public static AppResponse<T> Unauthorized(string? message = null, string? stackTrace = null) =>
            new AppResponse<T>(HttpStatusCode.Unauthorized,
                string.IsNullOrEmpty(message) ? HttpStatusCode.Unauthorized.ToString() : message, stackTrace);

        /// <summary>
        /// Constrói container de retorno customizado
        /// </summary>
        /// <param name="status">Status HTTP do processamento</param>
        /// <param name="message">Mensagem de retorno</param>
        /// <param name="stackTrace">Caminho percorrido no sistema</param>
        /// <param name="content">Conteúdo a ser retornado</param>
        /// <returns></returns>
        public static AppResponse<T> Custom(HttpStatusCode status, string? message = null,
            string? stackTrace = null, T? content = null) =>
            new AppResponse<T>(status,
                string.IsNullOrEmpty(message) ? status.ToString() : message,
                stackTrace, content);

        /// <summary>
        /// Copia conteúdo de um container
        /// </summary>
        /// <typeparam name="Origin">Tipo do conteúdo do container de origem</typeparam>
        /// <param name="origin">Container de origem</param>
        /// <param name="content">Novo conteúdo</param>
        /// <returns></returns>
        public static AppResponse<T> Copy<Origin>(AppResponse<Origin> origin, T? content = null)
            where Origin : class =>
            new AppResponse<T>(origin.Status, origin.Message, origin.StackTrace, content);
    }
}
