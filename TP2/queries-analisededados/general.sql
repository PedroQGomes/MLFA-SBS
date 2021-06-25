q--queries for data analysis

/* Data Structure
Books
Ratings
Users
*/

-- Connections:  Books - Ratings Inner Join on ISBN
--               Users -- Books Inner Join on UserID

-- Analysis needed to be done:

-User centric:
- Distribution User Rating
- Distribution Rating by Location
-DIstribution Rating by Age


- "           Nr. Books by Location
- "           Nr. Books by Location


-- Book Centric:
- Distribution Years of publication
- Author - Number of Books

-- FUn stuff:
-- Age and Year of Publication: Correlated?
--
-- Location and year of publication - Publisher

--

-- Make tables for querying:
-- User - Rating
-- We need a tabe with

-- UserID Book Rating Location Age
  -- we will get the Distribution ofUser Rating; / Genreal, Age, LOCATION
  -- Nr Books Rated By Location /Age

  Análise ao User

  1) Número de Users
  2) Análise dos top Países com mais users
  3) Análise de Cidade com mais Users
  4) Média de Idades dos Users
  5) Distribuição de Quantidade de Users por faixa etária (%)
  6) Quantos users sem idade registada (%)


  Análise ao Book

  1) Quantos livros existem
  2) Top autores com mais livros
  3) Distribuição de livros por ano de publicação
  4) Editoras com mais livros publicados

  Análise as Avaliações

  1) Utilizadores vs avaliações (gráfico de barras x-> numero de utilizadores y-> quantidade de avaliações) "apenas 2 utilizadores avaliaram 3 livros"
  2) Livros Vs avaliações (gráfico de barras x-> numero de livros y-> quantidade de avaliações) "apenas 1 livros teve 100 avaliações"
  3) Qual é a correlação entre o ano da publicação do livro e a idade dos utilizadores -> será que os mais velhos leem livros mais antigos?
  4) Qual é a correlação entre as melhores avaliações e a idade do utilizador
  5) Qual é a correlação entre as melhores avaliações e o ano de publicação




SELECT u.UserID, u.ISBN, r.Rate, u.Location, u.Age
FROM ratings
INNER JOIN users on u.UserID=r.UserID
