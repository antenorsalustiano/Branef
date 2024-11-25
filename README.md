Camada de Apresentação:
Instruções
1.	Desenvolver back-end utilizando Web Api, preferencialmente seguindo o padrão RESTful.
2.	Construir o front-end com Single Page Application. (VueJs, React ou Angular)
3.	Não é necessário se preocupar com o layout nem com boas práticas visuais, html, css, etc.
Domínio da aplicação:
Cadastro de clientes contendo:
●	id
●	Nome da Empresa
●	Porte da empresa
○	O porte da empresa contém 3 valores (pequena, média e grande).
Será necessário desenvolver uma tela de listagem com os clientes cadastrados, uma tela de inclusão com edição e um link de remoção.
ATENÇÃO: Leve em consideração o desenvolvimento em camadas Presentation/Application/Domain, seguindo os princípios do DDD(Domain Driven Design). Será considerado diferencial a aplicação, sem Event Sourcing, do padrão CQRS (Command Query Responsibility Segregation).
Acesso a Dados:
1.	Utilizar um banco de dados relacional (Microsoft® SQL Server, PostgreSQL ou MySql), será considerado um diferencial caso além da utilização do banco relacional houver a utilização do NoSql (MongoDB, DynamoDB ou Cassandra) para projeção dos dados na parte da consulta(CQRS).

2.	No banco de dados relacional, utilizar um framework de mapeamento objeto-relacional - NHibernate ou Entity Framework

