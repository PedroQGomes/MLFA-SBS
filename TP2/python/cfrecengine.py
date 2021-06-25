# install extensions
from surprise import Reader
from surprise import Dataset
from surprise import SVD, KNNBasic
import pandas as pd
import mong_acess as ma
import surp_optpar as sop
import rec_genr as rg
from surprise import PredictionImpossible
import mysql.connector as mysql
from rake_nltk import Rake

# DataBase ratings access
print("\ntry access collection ratings...")
try:
    ratings = ma.mong_bx('ratings')
    ratings = ratings.drop(['_id'], axis=1)
except Exception as e:
    print("error accessing database: \n" + str(e) + "\n")
else:
    print("successfully accessed\n")


# Prepare Data
print("\nPrepare Data...")
try:
    reader = Reader(rating_scale=(1, 10))
    data = Dataset.load_from_df(ratings[['User-ID', 'ISBN', 'Book-Rating']],
                                reader)
except Exception as e:
    print("error to prepare data : \n" + str(e) + "\n")
else:
    print("successfully prepared data\n")


# Machine Learning Models
try:
    knn = sop.optimization(data, KNNBasic)
    svd = sop.optimization(data, SVD)
except Exception as e:
    print("error in creation models : \n" + str(e) + "\n")
else:
    print("successfully created models\n")


# Decide Model with cross validation
try:
    print('Choose Best Model...')
    model = sop.best_model(knn, svd, data)
except Exception as e:
    print("error in choose models : \n" + str(e) + "\n")
else:
    print("successfully chose the best model\n")

# Create Recomendations to "Recomendados para si"


# DataBase datapred access
print("\ntry access collection datapred...")
try:
    datapred = ma.mong_bx('datapred')
    datapred = datapred.drop(['_id'], axis=1)
    datapred.set_index(['ISBN'], inplace=True)
    datapred.reset_index(level=['ISBN'], inplace=True)

except Exception as e:
    print("error accessing database: \n" + str(e) + "\n")
else:
    print("successfully accessed\n")

# Generate Recomendations
print("\ngenerate recoemndations of collab filt ...")
try:
    recfc = rg.rec_gen(model, datapred)
except Exception as e:
    print("error to generate recomendations: \n" + str(e) + "\n")
else:
    print("successfully recomendations generated\n")

print(recfc)

# Insert database
print("\ninsert database recf ..")
try:
    ma.mysql_rec("recf", recfc)
except Exception as e:
    print("error insert database recf \n" + str(e) + "\n")
else:
    print("successfully inserted\n")

#Top Country
#DataBase isbn_countriesviews access
print("\ntry access collection isbn_countrieviews...")
try:
    isbn_countryviews = ma.mong_bx('isbn_countryviews')
    isbn_countryviews = isbn_countryviews.drop(['_id'], axis=1)
except Exception as e:
    print("error accessing database: \n" + str(e) + "\n")
else:
    print("successfully accessed\n")

isbn_countryviews = rg.coutry_order(isbn_countryviews)

# Insert database topcountries
print("\ninsert database topcountries ..")
try:
    ma.mysql_rec("topcountries", isbn_countryviews)
except Exception as e:
    print("error insert database isbn_countryviews \n" + str(e) + "\n")
else:
    print("successfully inserted\n")

# Top Author
print("\ntry access collection author_books...")
try:
    author_books = ma.mong_bx('author_books')
    author_books = author_books.drop(['_id'], axis=1)
except Exception as e:
    print("error accessing database: \n" + str(e) + "\n")
else:
    print("successfully accessed\n")


author_books = rg.ab_order(author_books)

# Insert database author_rank
print("\ninsert database author_rank ..")
try:
    ma.mysql_rec("author_rank", author_books)
except Exception as e:
    print("error insert database author_books \n" + str(e) + "\n")
else:
    print("successfully inserted\n")








