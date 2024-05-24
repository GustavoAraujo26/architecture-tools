using System.Text.Json.Serialization;

namespace ArchitectureTools.Structs
{
    /// <summary>
    /// Dados de uma opção de enumerador
    /// </summary>
    public struct EnumData
    {
        /// <summary>
        /// Construtor para inicializar as propriedades
        /// </summary>
        /// <param name="intKey">Chave no formato de inteiro</param>
        /// <param name="stringKey">Chave no formato de texto</param>
        /// <param name="description">Descrição</param>
        [JsonConstructor]
        public EnumData(int intKey, string stringKey, string description)
        {
            IntKey = intKey;
            StringKey = stringKey;
            Description = description;
        }

        /// <summary>
        /// Chave no formato de inteiro
        /// </summary>
        public int IntKey { get; private set; }

        /// <summary>
        /// Chave no formato de texto
        /// </summary>
        public string StringKey { get; private set; }

        /// <summary>
        /// Descrição
        /// </summary>
        public string Description { get; private set; }
    }
}
