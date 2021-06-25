using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FrontEnd.Models;
using MySqlConnector;

namespace FrontEnd.Data
{
    public class RecomendationsContext
    {
        public string ConnectionString { get; set; }

        public RecomendationsContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }


        public List<Book> getTopWorldBooks()
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT distinct * FROM book INNER JOIN(SELECT * FROM recomendations.topworld ORDER BY topworld.rank ASC LIMIT 20) AS tabela2 ON book.ISBN = tabela2.isbn order by tabela2.rank ASC";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            isbn = reader["ISBN"].ToString(),
                            title = reader["Book-Title"].ToString(),
                            autor = reader["Book-Author"].ToString(),
                            yearRelease = reader["Year-Of-Publication"].ToString(),
                            publisher = reader["Publisher"].ToString(),
                            img_Url_Small = reader["Image-URL-S"].ToString(),
                            img_Url_Medium = reader["Image-URL-M"].ToString(),
                            img_Url_Big = reader["Image-URL-L"].ToString()
                        });
                    }
                }
            }
            return list;
        }



        public List<Book> getTopYearBooks()
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT  distinct * FROM book INNER JOIN(SELECT* FROM recomendations.top_year ORDER BY top_year.rank ASC LIMIT 20) AS tabela2 ON book.ISBN = tabela2.isbn order by tabela2.rank ASC";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            isbn = reader["ISBN"].ToString(),
                            title = reader["Book-Title"].ToString(),
                            autor = reader["Book-Author"].ToString(),
                            yearRelease = reader["Year-Of-Publication"].ToString(),
                            publisher = reader["Publisher"].ToString(),
                            img_Url_Small = reader["Image-URL-S"].ToString(),
                            img_Url_Medium = reader["Image-URL-M"].ToString(),
                            img_Url_Big = reader["Image-URL-L"].ToString()
                        });
                    }
                }
            }
            return list;
        }



        public List<Book> getTopCountryBooks(string country)
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT distinct * FROM book INNER JOIN(SELECT* FROM recomendations.topcountries WHERE topcountries.country = \"" + country + "\") AS tabela2 ON book.ISBN = tabela2.isbn";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            isbn = reader["ISBN"].ToString(),
                            title = reader["Book-Title"].ToString(),
                            autor = reader["Book-Author"].ToString(),
                            yearRelease = reader["Year-Of-Publication"].ToString(),
                            publisher = reader["Publisher"].ToString(),
                            img_Url_Small = reader["Image-URL-S"].ToString(),
                            img_Url_Medium = reader["Image-URL-M"].ToString(),
                            img_Url_Big = reader["Image-URL-L"].ToString()
                        });
                    }
                }
            }
            return list;
        }


        
        // 76499
        public List<Book> getUserRecfBooks(string user)
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT distinct * FROM book INNER JOIN(SELECT * FROM recomendations.recf  WHERE recf.user = \"" + user + "\") AS tabela2 ON book.ISBN = tabela2.book";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            isbn = reader["ISBN"].ToString(),
                            title = reader["Book-Title"].ToString(),
                            autor = reader["Book-Author"].ToString(),
                            yearRelease = reader["Year-Of-Publication"].ToString(),
                            publisher = reader["Publisher"].ToString(),
                            img_Url_Small = reader["Image-URL-S"].ToString(),
                            img_Url_Medium = reader["Image-URL-M"].ToString(),
                            img_Url_Big = reader["Image-URL-L"].ToString()
                        });
                    }
                }
            }
            if(list.Count == 0)
            {
                list = getTopWorldBooks();
            }
            return list;
        }





        public List<Book> getUserBestAuth(string autor)
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT distinct * FROM book INNER JOIN(SELECT * FROM recomendations.author_rank WHERE author = \"" + autor + "\") AS tabela2 ON book.ISBN = tabela2.isbn";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            isbn = reader["ISBN"].ToString(),
                            title = reader["Book-Title"].ToString(),
                            autor = reader["Book-Author"].ToString(),
                            yearRelease = reader["Year-Of-Publication"].ToString(),
                            publisher = reader["Publisher"].ToString(),
                            img_Url_Small = reader["Image-URL-S"].ToString(),
                            img_Url_Medium = reader["Image-URL-M"].ToString(),
                            img_Url_Big = reader["Image-URL-L"].ToString()
                        });
                    }
                }
            }
            if (list.Count == 0)
            {
                list = getBooksOfMostSalesAuthor();
            }
            return list;
        }

        private List<Book> getBooksOfMostSalesAuthor()
        {
            List<Book> list = new List<Book>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT * FROM book INNER JOIN (SELECT author, SUM(sales) AS ALLSales FROM recomendations.author_rank GROUP BY author order by ALLSales DESC LIMIT 1) AS tabela2 ON book.`Book-Author` = tabela2.author LIMIT 20";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Book()
                        {
                            isbn = reader["ISBN"].ToString(),
                            title = reader["Book-Title"].ToString(),
                            autor = reader["Book-Author"].ToString(),
                            yearRelease = reader["Year-Of-Publication"].ToString(),
                            publisher = reader["Publisher"].ToString(),
                            img_Url_Small = reader["Image-URL-S"].ToString(),
                            img_Url_Medium = reader["Image-URL-M"].ToString(),
                            img_Url_Big = reader["Image-URL-L"].ToString()
                        });
                    }
                }
            }
            return list;
        }




       public void addRating(string userid, string isbn, int rating)
        {

            using (MySqlConnection conn = GetConnection())
            {
                string query = "INSERT INTO recomendations.ratings (userid, isbn, rating) VALUES(\"" + userid + "\",\"" + isbn + "\", " + rating + ")";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.ExecuteReader();

            }

        }

        public void updateRating(string userid, string isbn, int rating)
        {

            using (MySqlConnection conn = GetConnection())
            {
                
                string query = "UPDATE recomendations.ratings SET rating = " + rating + " WHERE ratings.userid = \"" + userid + "\" AND ratings.isbn = \"" + isbn +"\"";
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.ExecuteReader();

            }

        }



        public List<Rating> getAllRatings(string userid)
        {
            List<Rating> list = new List<Rating>();

            using (MySqlConnection conn = GetConnection())
            {
                string query = "SELECT * FROM recomendations.ratings WHERE userid = " + userid;
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Rating()
                        {
                            UserID = reader["userid"].ToString(),
                            bookIsbn = reader["isbn"].ToString(),
                            rating = Int32.Parse(reader["rating"].ToString())
                    });
                    }
                }
            }
            return list;

        }




        public User getUser(string userid)
        {
            User user = null;
            try
            {
                using (MySqlConnection conn = GetConnection())
                {
                    string query = "SELECT * FROM user_inf WHERE `User-ID` = " + userid;
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User();
                            user.id = reader["User-ID"].ToString();
                            user.country = reader["Country"].ToString();
                            user.favorit_author = reader["favorite_Author"].ToString();
                        }
                    }
                }

            }
            catch(Exception e)
            {
                return null;
            }
            return user;

        }


    }
}
