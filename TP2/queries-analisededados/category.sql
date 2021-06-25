-- on this one we are going to understand tha validity of the new "Category" and "Language" columns

SELECT CASE WHEN books.Language in ("undefined") then NULL else books.Language end, count(*), count(*)/total.s as percentage
FROM books
CROSS JOIN (SELECT count(ISBN) AS s FROM books) as total
GROUP BY 1
ORDER By 2 DESC
LIMIT 40;

-- category and maturityRate is not valid: 95% was not identified;

-- Language only has 22% not identified, but due to 75% being eng, it does not vary enough to be considered.

--so, taking into account the api requests we made, it was not useful.

