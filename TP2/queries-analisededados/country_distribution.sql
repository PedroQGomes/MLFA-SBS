-- we need to check all possible locations:
--SELECT sum(t.total)
--FROM
--  (SELECT distinct Country, count(UserID), count(userID)/(SELECT count(*) from users) as total
--   FROM users
--   WHERE Country not like ''
--   GROUP BY 1
--   ORDER BY 2 desc
--   LIMIT 45) as t;

--after dividing (useinf SUBSTRING_INDEX ) Location by Place, City and COuntry we saw taht there where a lot of mistakes. because of this, we only used the country.
--The coutnry has 1k difenret valyes, btu due to the error. we only used the first 46. (96% of all values).

-- what is missing. fromthe 46, evaluate city names.

SELECT users.Country, users.City, count(users.UserID)
FROM
  (SELECT distinct Country as Country, count(UserID)
     FROM users
     WHERE Country not like ''
     GROUP BY 1
     ORDER BY 2 desc
     LIMIT 45) as t
INNER JOIN users on users.Country=t.Country
GROUP BY 1,2
ORDER BY 1,3 ASC;

-- main cities: is main city ot not?  mb is useful for rec.
  -- not going foward on this.


--UPDATING DB to take into account only the countries we want
ALTER TABLE users ADD COLUMN (CountryN VARCHAR(255));
UPDATE users
SET CountryN = SELECT CASE WHEN Country in (SELECT t.SCountry
                                             FROM (SELECT distinct Country as SCountry, count(UserID)
                                                   FROM users
                                                   WHERE Country not like ''
                                                   GROUP BY 1 ORDER BY 2 desc LIMIT 45) as t)
                                             THEN Country ELSE "invalid" END FROM users;
UPDATE users
SET CountryN = Country
WHERE Country in ("%usa%", "%canada%", "%united kingdom%", "%germany%", "%spain%", "%australia%", "%italy%", "%france%", "%portugal%", "%new zealand%", "%netherlands%", "%switzerland%", "%brazil%", "%china%", "%sweden%", "%india%", "%austria%", "%malaysia%", "%argentina%", "%finland%", "%singapore%", "%denmark%", "%belgium%", "%mexico%", "%ireland%", "%philippines%", "%turkey%", "%poland%", "%pakistan%", "%greece%", "%iran%", "%romania%", "%chile%", "%israel%", "%south africa%", "%indonesia%", "%norway%", "%japan%", "%croatia%", "%south korea%", "%nigeria%", "%slovakia%","%czech republic%", "%england%","%russia%");
INSERT INTO users
(CountryN)
SELECT CASE WHEN Country in ( SELECT count(SCountry)
                                             FROM (SELECT distinct LTRIM(RTRIM(Country)) as SCountry, count(*)
                                                   FROM users
                                                   WHERE Country not like ''
                                                   GROUP BY 1 ORDER BY 2 desc LIMIT 45) as t)
                                             THEN Country ELSE "invalid" END FROM users;
-SELECT LTRIM(RTRIM(t.SCountry)),







-- WIP , use the followinf constraint always
LTRIM(RTRIM(Country)) in ("%usa%", "%canada%", "%united kingdom%", "%germany%", "%spain%", "%australia%", "%italy%", "%france%", "%portugal%", "%new zealand%", "%netherlands%", "%switzerland%", "%brazil%", "%china%", "%sweden%", "%india%", "%austria%", "%malaysia%", "%argentina%", "%finland%", "%singapore%", "%denmark%", "%belgium%", "%mexico%", "%ireland%", "%philippines%", "%turkey%", "%poland%", "%pakistan%", "%greece%", "%iran%", "%romania%", "%chile%", "%israel%", "%south africa%", "%indonesia%", "%norway%", "%japan%", "%croatia%", "%south korea%", "%nigeria%", "%slovakia%","%czech republic%", "%england%","%russia%")

----------------------------------------

-- Overview
SELECT distinct CASE WHEN LTRIM(RTRIM(Country)) in ("usa", "canada", "united kingdom", "germany", "spain", "australia", "italy", "france", "portugal", "new zealand", "netherlands", "switzerland", "brazil", "china", "sweden", "india", "austria", "malaysia", "argentina", "finland", "singapore", "denmark", "belgium", "mexico", "ireland", "philippines", "turkey", "poland", "pakistan", "greece", "iran", "romania", "chile", "israel", "south africa", "indonesia", "norway", "japan", "croatia", "south korea", "nigeria", "slovakia","czech republic", "england","russia") THEN LTRIM(RTRIM(Country)) ELSE "invalid" END AS Country, count(users.UserID)
FROM users
GROUP BY 1
ORDER BY 2 ASC;


