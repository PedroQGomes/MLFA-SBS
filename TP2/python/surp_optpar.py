from surprise import SVD, KNNBasic
from surprise.model_selection import GridSearchCV, cross_validate
import pandas as pd
import numpy as np


# parameter otimization
def optgrid(dataset, algorithm):
    # cross validation
    cva = 2
    # measures
    meas = ['rmse', 'mae']

    if algorithm == KNNBasic:
        # var k
        k = list(range(1, 300, 50))
        print("iniciate otimizaion " + str(algorithm) + " :")
        param_grid = {'k': k,
                      'sim_options': {'name': ['msd'],
                                      'user_based': [False]}
                      }

    elif algorithm == SVD:
        # var n_factores
        nf = list(range(1, 397, 99))
        ia = [0.010, 0.020, 0.030]
        ne = [10, 20]
        ra = [0.04, 0.05, 0.06, 0.07]
        print("iniciate otimizaion " + str(algorithm) + " :")
        param_grid = {'n_factors': nf, 'n_epochs': ne, 'init_mean': [0], 'init_std_dev': [0],
                      'lr_all': ia, 'reg_all': ra
                      }

    gs = GridSearchCV(algorithm, param_grid, measures=meas, cv=cva)
    gs.fit(dataset)
    # best RMSE score
    print("The best score : ")
    print(gs.best_score['rmse'])
    print("\n")
    print("The best parameters : ")
    print(gs.best_params['rmse'])
    print("\n")
    print("Results : \n")
    print(pd.DataFrame.from_dict(gs.cv_results))
    print("\n")
    return gs.best_params['rmse']


# Create Models
def optimization(dataset, algorithm):
    if algorithm == KNNBasic:
        paramKNN = optgrid(dataset, algorithm)
        so = paramKNN['sim_options']

        sim_options = {'name': so['name'],
                       'user_based': so['user_based']  # compute  similarities between items or users
                       }

        return KNNBasic(sim_options=sim_options, k=paramKNN['k'], min_k=1, verbose=False)

    elif algorithm == SVD:
        paramSVD = optgrid(dataset, algorithm)

        return SVD(n_factors=paramSVD['n_factors'], n_epochs=paramSVD['n_epochs'], init_mean=paramSVD['init_mean'],
                   init_std_dev=paramSVD['init_std_dev'], lr_all=paramSVD['init_mean'],
                   reg_all=paramSVD['reg_all'], verbose=False)


# Cross Validation
def cross_valid(algori, db):
    return cross_validate(algo=algori, data=db, measures=['RMSE', 'MAE'], cv=2, verbose=False)


def best_model(model1, model2, db):
    first_test = cross_valid(model1, db)
    second_test = cross_valid(model2, db)
    if np.mean(first_test['test_rmse']) > np.mean(second_test['test_rmse']):
        print('the best model is: ' + str(model1))
        return model1
    else:
        print('the best model is: ' + str(model2))
        return model2
