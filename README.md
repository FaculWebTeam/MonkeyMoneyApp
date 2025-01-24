[![Generic badge](https://img.shields.io/badge/STATUS%20DO%20PROJETO-CONCLUIDO-green)](https://shields.io/)

# Projeto de Desenvolvimento Web 2 | MonkiMoney 🐒💸
<p><b>Membros da equipe:</b></p>
<li>Alisson Sousa Ferreira</li>
<li>Lucas Patricio da Silva</li>
<li>Pollyana Fortunato Keller </li>
<li>Rayana Caroline da Silva</li>

<h3>Sobre o projeto</h3>
<p>O MonkiMoney é uma aplicação de Gestão Financeira Pessoal desenvolvida para ajudar os usuários a gerenciar seus gastos e receitas de maneira fácil e eficiente. A plataforma oferece uma visão clara das finanças, permitindo que os usuários façam um controle detalhado de suas transações financeiras.</p>
<p><a href=https://youtu.be/qZWbuT7aLdk>Vídeo demonstrativo de funções</a></p>

<p>Com o MonkiMoney, você pode:</p>

<li><b>Controlar seus Gastos e Receitas:</b> Registre suas transações financeiras, como compras, vendas e recebimentos, ajudando a acompanhar o fluxo de caixa de forma transparente.</li>

<li><b>Estabelecer Metas de Economia:</b> Defina metas financeiras, como economizar uma quantia específica para um objetivo (viagem, emergências, compras, etc.), incentivando a disciplina e o planejamento.</li>

## Recursos utilizados
<h3>Backend - STATUS: 🟢 Concluído </h3>
<li>ASP.NET Core: Framework principal utilizado para construir a API RESTful.</li>
<li>Entity Framework Core: ORM utilizado para interagir com o banco de dados.</li>
<li>SQL Server: Banco de dados utilizado para armazenar as transações financeiras e dados dos usuários.</li>
<li>ASP.NET Core MVC: Para estruturação das rotas e controladores da API.</li>

<h3>Frontend - STATUS: 🟢 Concluído </h3>
<li>Razor Pages: Utilizado para criar páginas dinâmicas, permitindo a interação entre o frontend e o backend de forma simples e eficiente. Permite a construção de páginas HTML com lógica embutida no servidor.</li>
<li>HTML5: A base estrutural das páginas, utilizando as semânticas mais recentes do HTML para garantir acessibilidade e um melhor entendimento do conteúdo para navegadores e mecanismos de busca.</li>
<li>CSS3: Estilização das páginas utilizando propriedades modernas de CSS, como flexbox, grid, e transições, para garantir um design atrativo e responsivo.</li>
<li>Bootstrap 5: Framework CSS popular para criar interfaces modernas e responsivas de maneira rápida. Utilizado para componentes prontos, como botões, formulários, barras de navegação e layout fluído.</li>
<li>JavaScript: Linguagem de programação utilizada para manipulação do DOM, criando interatividade nas páginas, como validação de formulários e atualização dinâmica de conteúdos sem recarregar a página.</li>
<h3>Prototipagem - STATUS: 🟢 Concluído</h3>
<li>Figma</li>
<li><a href="https://www.figma.com/design/cQbVID5jHFaUfLBhSTZzL4/MonkiMoneyApp?node-id=0-1&node-type=canvas&t=dfIQa5Vv7jrGOUjI-0" target="_blank">Acesse o design no Figma</a></li>

## Clone esse repositório
```bash
git clone https://github.com/FaculWebTeam/MonkeyMoneyApp.git
```
## 🔧 Manual de uso
<h3>Inserir uma ConnectionStrings</h3>
<p> Vá até o arquivo "appsettings.json" na raiz do projeto e insira a conexão com o banco dentro da propriedade "DefaultConnection" no array de "ConnectionStrings"</p>
<p>"Server=[inserir o server aqui];Database=MonkiMoneyApp;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"</p>
<h3>Criar as tabelas</h3>
<li>Em ferramentas, no Visual Studio, clique em "Gerenciador de Pacotes do Nuget" e "Console do Gerenciador de Pacotes"</li>
<li>No console, digite o seguinte comando: update-database</li>
<li>Depois, o projeto é para estar rodando! </li>
