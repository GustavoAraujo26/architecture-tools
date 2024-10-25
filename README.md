# Architecture Tools

> ### Introdução

Projeto para centralizar e expor algumas classes e ferramentas que utilizo com frequência, quando vou criar novos projetos.

Nele centralizo algumas classes implementando alguns design patterns, que auxiliam na criação de microsserviços.

Pacote foi criado no .NET Standard 2.1, para permitir a utilização em versões diferentes do .NET.

> ### Principais funcionalidades

Abaixo, faço uma listagem das principais funcionalidades que podem ser encontrados no pacote.

- [ActionResponse](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Responses/ActionResponse.cs): 

Container que implementa o design pattern "Result". Utilizado para criar respostas para ações/métodos, se baseando no HTTP Status Code para informar se aquela ação foi realizada com sucesso ou falha. Além disso, permite devolver no seu corpo um objeto complexo (uma entidade, value object e afins), bem como structs (DateTime, Guid, integer, dentre outros).

Possui métodos estáticos que auxiliam na sua criação, seja para devolver uma chamada de sucesso ("Ok"), como para devolver chamadas de falha ("BadRequest", "NotFound", "InternalServerError", dentre outros status). Outra possibilidade é converter uma mensagem de resposta de uma chamada HTTP ("HttpResponseMessage") no container em si, já realizando a deserialização do conteúdo.

- [EnvironmentSettings](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Settings/EnvironmentSettings.cs):

Implementa, "de certa forma", o design pattern "Options". Nele é possível realizar a leitura de variáveis de ambiente da aplicação, armazená-las internamente, para que, em momento posterior, o container possa ser recebido via injeção de dependências.

Classe implementada como "Singleton", para que possa ter somente uma instância injetada na aplicação.

A leitura e inicialização das variáveis pode ser feita tanto de forma unitária (passando variável por variável), como também através de uma lista de chaves de variáveis de ambiente.

- [ApiHttpCollection](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/HttpLibrary/ApiHttpCollection.cs):

Ferramenta para centralizar e controlar chamadas HTTP REST para outros serviços. O memso permite registrar diversas API's, com seus endpoints distintos e seus métodos.

Classe implementada como "Singleton", para que possa ter somente uma instância injetada na aplicação.

Para realizar as chamadas, basta acessar um item do ["ApiResource"](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/HttpLibrary/ApiResource.cs), e acionar o método "Call", que espera a classe "ApiEndpointRequest". Através dela, é possível passar os parâmetros a serem utilizados na query da URL, ou enviar classe/objeto a ser transmitido no corpo do projeto.

- [PeriodRange](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Period/PeriodRange.cs):

Struct criada para auxiliar na utilização de "intervalos de período". Nele é possível armazenar um DateTime de início e um de término. Além disso, é possível realizar validação dos dados através do método "Validate".

- [EventMessage](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Event/EventMessage.cs):

Struct para representar mensagem de eventos a serem transmitidos em barramento de eventos (Azure EventBus, RabbitMQ, Kafka, etc).

- [EntityState](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Entities/EntityState.cs):

Struct para centralizar dados de estado e datas de criação/alteração de um objeto.

Possui campo "State", para informar se o objeto está "habilitado" ou "desabilitado". Quando alterado o estado, via métodos "Enable"/"Disable", a data de alteração é alterada automaticamente.

- [BaseEntity](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Entities/BaseEntity.cs):

Classe base para ser herdada, muito por "entidades", para centralizar dados básicos como "Id" e dados do estado do objeto.

- [EnumData](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Enums/EnumData.cs):

Container para obter dados de uma opção de enumerador. Possui como propriedades o valor "inteiro" do item do enumerador, valor "string" do item do enumerador e descrição (quando houver o "decoration" DescriptionAttribute).

- [Page](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Pagination/Page.cs):

Container que controla paginação a ser realizada em uma lista de itens. No mesmo é possível calcular a quantidade de páginas disponíveis (anteriores e próximas). Nas listagens de páginas anteriores e próximas páginas, o limite da lista está como 5 itens.

- [PaginationResponse](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Pagination/PaginationResponse.cs):

Container para retorno de dados paginados. Possui duas propriedades: ["Page"](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Pagination/Page.cs) e "Content" (uma lista de objetos).

- [CryptographyFactory](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Security/CryptographyFactory.cs):

Fábrica para manipulação de criptografia de textos, utilizando SHA256 e AES.

- [Password](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Security/Password.cs):

Struct para manipulação de senhas de usuários, utilizando criptografia o valor inserido.

- [PasswordRules](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Security/PasswordRules.cs):

Classe para realizar validações de integridade de senhas informadas.

- [SecurityKey](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Security/SecurityKey.cs):

Struct de geração de chaves aleatórias de 6 caracteres, muito utilizado em autenticações de múltiplos fatores.

- [ExpirationDate](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Period/ExpirationDate.cs):

Struct para geração de datas de expiração, permitindo realizar comparativos, capturar diferenças entre datas, etc.

> ### Extensões

Foram criadas algumas extensões para auxiliar e agilizar a utilização da biblioteca.

- [GetService](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/DependencyInjectionExtensions.cs): Extensão do IServiceProvider para auxiliar a obtenção de serviços injetados.

- [ConfigureApiCollection](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/DependencyInjectionExtensions.cs): Extensão do IServiceCollection para injetar a classe [ApiHttpCollection](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/HttpLibrary/ApiHttpCollection.cs).

- [GetDescription - Enum](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/EnumExtensions.cs): Extensão de Enum para obter o valor do atributo "Descrição".

- [GetData](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/EnumExtensions.cs): Extensão de Enum para obter os dados do item do enumerador, através do [EnumData](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Enums/EnumData.cs).

- [List](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/EnumExtensions.cs): Extensão de Enum para realizar listagem de todos os itens de um enumerador.

- [ConfigureEnvironmentSettings](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/EnvironmentSettingsExtensions.cs): Extensão do IServiceCollection para realizar a injeção do [EnvironmentSettings](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Settings/EnvironmentSettings.cs).

- [ToAppResponse](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/HttpClientExtensions.cs): Extensão do HTTPResponseMessage para realizar conversão para o [ActionResponse](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Responses/ActionResponse.cs).

- [GetDescription - HTTPStatusCode](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/HttpStatusCodeExtensions.cs): Extensõa do enumerador HttpStatusCode para obter a "descrição" da opção do enumerador ("ReasonPhrase").

- [Truncate - string](https://github.com/GustavoAraujo26/architecture-tools/blob/master/ArchitectureTools/Extensions/StringExtensions.cs): Extensão para "truncar" uma string com base em uma quantidade máxima de caracteres.
