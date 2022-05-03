SELECT "r"."recipeId", "r"."recipeName", "r"."description"
FROM "recipes" AS "r"
WHERE ((
    SELECT COUNT(*)
    FROM "recipeIngredients" AS "r0"
    WHERE "r"."recipeId" = "r0"."recipeId"
    ) = (
    SELECT COUNT(*)
    FROM "recipeIngredients" AS "r1"
        INNER JOIN (
        SELECT "r2"."roomateId", "r2"."ingredientId", "r2"."price", "r2"."quantity", "r2"."quantityUnit"
        FROM "roomateIngredients" AS "r2"
                WHERE "r2"."roomateId" IN (5, 7)
        ) AS "t" ON "r1"."ingredientId" = "t"."ingredientId"
            WHERE "r"."recipeId" = "r1"."recipeId")) OR (((
                    SELECT COUNT(*)
                        FROM "recipeIngredients" AS "r0"
                            WHERE "r"."recipeId" = "r0"."recipeId") IS NULL) AND ((
                                    SELECT COUNT(*)
                                        FROM "recipeIngredients" AS "r1"
                                            INNER JOIN (
                                                SELECT "r2"."roomateId", "r2"."ingredientId", "r2"."price", "r2"."quantity", "r2"."quantityUnit"
                                                    FROM "roomateIngredients" AS "r2"
                                                        WHERE "r2"."roomateId" IN (5, 7)
                                                            ) AS "t" ON "r1"."ingredientId" = "t"."ingredientId"
                                                                WHERE "r"."recipeId" = "r1"."recipeId") IS NULL))