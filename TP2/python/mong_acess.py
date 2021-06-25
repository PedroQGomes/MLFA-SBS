# database access
import pymongo
import pandas
import mysql.connector as mysql
from sqlalchemy import create_engine
import pymysql


def mong_bx(collections) -> object:
    uri = "mongodb://127.0.0.1:27017"
    client = pymongo.MongoClient(uri)
    database = client['bxdata']
    collection = database[collections]
    cursor = collection.find({})
    return pandas.DataFrame(cursor)


def mysql_rec(table_mysql, table_python):
    db = mysql.connect(
        host="localhost",
        user="root",
        passwd="1234567Ll.",
        database="recomendations"
    )
    cursor = db.cursor()
    if table_mysql == "recf":
        cursor.execute("Truncate Table recf")
        query = """INSERT INTO recf (user, book, predict, ranking, typerec) VALUES (%s, %s, %s, %s, %s)"""
        for i in range(len(table_python)):
            values = (table_python.iloc[i, 0], str(table_python.iloc[i, 1]), float(table_python.iloc[i, 2]),
                      float(table_python.iloc[i, 3]), "Colaborative Filtering")
            cursor.execute(query, values)
            db.commit()
            print(str(i) + " to " + str(len(table_python)))
    elif table_mysql == "topcountries":
        cursor.execute("Truncate Table topcountries")
        query = """INSERT INTO topcountries (isbn, country, count, ranking) VALUES (%s, %s, %s, %s)"""
        for i in range(len(table_python)):
            values = (str(table_python.iloc[i, 0]), str(table_python.iloc[i, 1]), int(table_python.iloc[i, 2]),
                      float(table_python.iloc[i, 3]))
            cursor.execute(query, values)
            db.commit()
            print(str(i) + " to " + str(len(table_python)))
    elif table_mysql == "author_rank":
        cursor.execute("Truncate Table author_rank")
        query = """INSERT INTO author_rank (isbn, author, sales, ranking) VALUES (%s, %s, %s, %s)"""
        for i in range(len(table_python)):
            values = (str(table_python.iloc[i, 0]), str(table_python.iloc[i, 1]), int(table_python.iloc[i, 2]),
                      float(table_python.iloc[i, 3]))
            cursor.execute(query, values)
            db.commit()
            print(str(i) + " to " + str(len(table_python)))
    db.close()


def import_mtsql(table):
    db_connection_str = 'mysql+pymysql://root:1234567Ll.@localhost/recomendations'
    db_connection = create_engine(db_connection_str)
    df = pandas.read_sql("SELECT  ISBN, lower(concat(`Book-Title`,' ', replace(`Book-Author`, ' ', ''),' ',"
                         "`Year-Of-Publication`, ' ',replace(`Publisher`,' ',''))) as words "
                         "FROM " + str(table) + " limit 3000;", con=db_connection)

    return df

