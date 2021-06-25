SELECT

--we saw already that the only location we shoul use is Country: and only the first 45.

SELECT distinct CASE WHEN LTRIM(RTRIM(Country)) in ("usa", "canada", "united kingdom", "germany", "spain", "australia", "italy", "france", "portugal", "new zealand", "netherlands", "switzerland", "brazil", "china", "sweden", "india", "austria", "malaysia", "argentina", "finland", "singapore", "denmark", "belgium", "mexico", "ireland", "philippines", "turkey", "poland", "pakistan", "greece", "iran", "romania", "chile", "israel", "south africa", "indonesia", "norway", "japan", "croatia", "south korea", "nigeria", "slovakia","czech republic", "england","russia") THEN LTRIM(RTRIM(Country)) ELSE "invalid" END AS Country, count(UserID)
FROM users
GROUP BY 1
ORDER BY 2 ASC;

-- Rating given by those users

SELECT distinct CASE WHEN LTRIM(RTRIM(Country)) in ("usa", "canada", "united kingdom", "germany", "spain", "australia", "italy", "france", "portugal", "new zealand", "netherlands", "switzerland", "brazil", "china", "sweden", "india", "austria", "malaysia", "argentina", "finland", "singapore", "denmark", "belgium", "mexico", "ireland", "philippines", "turkey", "poland", "pakistan", "greece", "iran", "romania", "chile", "israel", "south africa", "indonesia", "norway", "japan", "croatia", "south korea", "nigeria", "slovakia","czech republic", "england","russia") THEN LTRIM(RTRIM(Country)) ELSE "invalid" END AS Country
,r.Rate
,count(users.UserID)
FROM users
INNER JOIN ratings as r on users.UserID = r.UserID
GROUP BY 1,2
ORDER BY 1,2,3 ASC;

-- put query on knime to analyse. check if difference on rating by location


-- check age:
SELECT age, count(*), count(*)/ temp1.number
FROM users
CROSS JOIN (SELECT count(*) as number from users) as temp1
GROUP BY 1 ORDER BY 1;
-- 40% of the users does not have an age associated.

SELECT CASE WHEN age between 14 and 61 then 1 else 0 end, count(*), count(*)/ temp1.number
FROM users
CROSS JOIN (SELECT count(*) as number from users) as temp1
GROUP BY 1 ORDER BY 1;


SELECT age, count(*), count(*)/ temp1.number
FROM users
CROSS JOIN (SELECT count(*) as number from users WHERE age is not NULL) as temp1
WHERE age is not NULL
GROUP BY 1 ORDER BY 1;
--taking into account the users who have a age associated, there is not a significant discrepancy