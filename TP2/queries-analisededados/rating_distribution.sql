-- queries to see distribution of rating
SELECT r.Rate, count(*), count(*)/t.total
FROM ratings as r
CROSS JOIN  (SELECT count(*) as total from ratings)  as t
GROUP BY 1
ORDER BY 1;


-- and without the 0 ranking ( read but did not evaluate)
SELECT r.Rate, count(*), count(*)/t.total
FROM ratings as r
CROSS JOIN  (SELECT count(*) as total from ratings where Rate not in (0))  as t
WHERE r.Rate not in (0)
GROUP BY 1
ORDER BY 1;

--of those who evaluate, the majority gives ratings above 7%.
SELECT CASE WHEN r.Rate in (1,2,3,4) then "under_5" else r.Rate end, count(*), count(*)/t.total
FROM ratings as r
CROSS JOIN  (SELECT count(*) as total from ratings where Rate not in (0))  as t
WHERE r.Rate not in (0)
GROUP BY 1
ORDER BY 1;
-- only 4% of all users evaluating evaluate under 5 (1,2,3,4)


WITH t  AS (
  SELECT r.Rate, count(u.UserID) as number
  FROM ratings  as r
  INNER JOIN users as u on u.UserID=r.UserID
  GROUP BY 1
  ORDER BY 1
  LIMIT 11
)
SELECT Rate, number, number/total.s*100 as perc
FROM t
CROSS JOIN (SELECT sum(number) AS s FROM t) as total
group by 1

-- 60% of all evaluations are 0.. error? no evaluation = 0?
  -- other point, its repeating usersID. but not uniforminally: only counting different UsersID in case of different rating.


SELECT ( SELECT count(ratings.UserID)
FROM ratings left join users on ratings.UserID=users.UserID
WHERE users.UserID is not null
) as user_ranked, ( SELECT count(ratings.UserID)
  FROM ratings left join users on ratings.UserID=users.UserID
  WHERE users.UserID is null
) as user_not_ranked;


SELECT sum(CASE WHEN ratings.UserID is null then 1 else 0 end) as total_null, sum(CASE WHEN ratings.UserID is not null then 1 else 0 end) as total_not_null
FROM ratings left join users on ratings.UserID=users.UserID
WHERE users.UserID is not null;



-- We now will look at rating based on the users characteristics, but first we need to have those analysed.
--LOCATION
--Age
--Nr Books Read


SELECT distinct CASE WHEN LTRIM(RTRIM(u.Country)) in ("usa", "canada", "united kingdom", "germany", "spain", "australia", "italy", "france", "portugal", "new zealand", "netherlands", "switzerland", "brazil", "china", "sweden", "india", "austria", "malaysia", "argentina", "finland", "singapore", "denmark", "belgium", "mexico", "ireland", "philippines", "turkey", "poland", "pakistan", "greece", "iran", "romania", "chile", "israel", "south africa", "indonesia", "norway", "japan", "croatia", "south korea", "nigeria", "slovakia","czech republic", "england","russia") THEN LTRIM(RTRIM(Country)) ELSE "invalid" END AS Country
, count(u.UserID), sum(CASE WHEN r.Rate>0 then 1 else 0 end)
FROM ratings  as r
INNER JOIN users as u on u.UserID=r.UserID
GROUp BY 1
ORDER BY 1;

SELECT distinct CASE WHEN u.Age between 14 and 61 then u.Age else "invalid" end  AS Age
, count(u.UserID), sum(CASE WHEN r.Rate>0 then 1 else 0 end)
FROM ratings  as r
INNER JOIN users as u on u.UserID=r.UserID
GROUp BY 1
ORDER BY 1;


SELECT distinct CASE WHEN u.Age between 14 and 24 then "14_24"
                     WHEN u.Age between 25 and 35 then "25_35"
                     WHEN u.Age between 36 and 46 then "36_46"
                     WHEN u.Age between 47 and 61 then "47_61"
                     else "invalid" end  AS Age
, sum(CASE WHEN r.Rate>=7 then 1 else 0 end) as high_rate, count(*) as total, sum(CASE WHEN r.Rate>=7 then 1 else 0 end)/ count(*) as perc
FROM ratings  as r
INNER JOIN users as u on u.UserID=r.UserID
GROUp BY 1
ORDER BY 4;


SELECT distinct CASE WHEN LTRIM(RTRIM(u.Country)) in ("usa", "canada", "united kingdom", "germany", "spain", "australia", "italy", "france", "portugal", "new zealand", "netherlands", "switzerland", "brazil", "china", "sweden", "india", "austria", "malaysia", "argentina", "finland", "singapore", "denmark", "belgium", "mexico", "ireland", "philippines", "turkey", "poland", "pakistan", "greece", "iran", "romania", "chile", "israel", "south africa", "indonesia", "norway", "japan", "croatia", "south korea", "nigeria", "slovakia","czech republic", "england","russia") THEN LTRIM(RTRIM(Country)) ELSE "invalid" END AS Country
, sum(CASE WHEN r.Rate>=7 then 1 else 0 end) as high_rate, count(*) as total, sum(CASE WHEN r.Rate>=7 then 1 else 0 end)/ count(*) as perc
FROM ratings  as r
INNER JOIN users as u on u.UserID=r.UserID
GROUp BY 1
ORDER BY 4;