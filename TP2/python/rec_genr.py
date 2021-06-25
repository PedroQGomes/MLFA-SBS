import pandas as pd


def rec_gen(model, data_pred):
    col_recfc = [
        'User',
        'Book',
        'Rating'
    ]
    recfc = pd.DataFrame(columns=col_recfc)
    for i in range(len(data_pred)):
        uid = str(data_pred.loc[i, 'User-ID'])
        iid = str(data_pred.loc[i, 'ISBN'])
        predicton = model.predict(uid, iid, verbose=False)
        recfc.loc[-1] = [predicton[0], predicton[1], round(predicton[3], 2)]
        recfc.index = recfc.index + 1
        recfc = recfc.sort_index()
        print(i)
    recfc["rank"] = recfc.groupby("User")["Rating"].rank("first", ascending=False)
    is_rank = recfc["rank"] <= 15
    recfc = recfc[is_rank]
    recfc = recfc.reset_index(drop=True)
    return recfc


def coutry_order(data):
    data["rank"] = data.groupby("Country")["Count"].rank("first", ascending=False)
    is_rank = data["rank"] <= 15
    data = data[is_rank]
    data = data.reset_index(drop=True)
    return data


def ab_order(data):
    data["rank"] = data.groupby("Book-Author")["Count*(Book-Rating)"].rank("first", ascending=False)
    is_rank = data["rank"] <= 15
    is_book = data["Count*(Book-Rating)"] >= 5
    data = data[is_rank]
    data = data[is_book]
    data = data.reset_index(drop=True)
    return data
