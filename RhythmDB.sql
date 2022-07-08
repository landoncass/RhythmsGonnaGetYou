CREATE TABLE "band" ( 
    "Id" SERIAL PRIMARY KEY,
    "Name" TEXT,
    "CountryOfOrigin" TEXT,
    "NumberOfMembers" INT,
    "Website" TEXT,
    "Style" TEXT,
    "IsSigned" BOOLEAN,
    "ContactName" TEXT, 
    "ContactPhoneNumber" TEXT
 );


INSERT INTO "band" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Radiohead', 'UK', 4, 'Radiohead.com', 'Rock', TRUE, 'Thom Yorke', '965-585-6985')

INSERT INTO "band" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Daft Punk', 'France', 2, 'DaftPunk.com', 'Electronic', TRUE, 'Robot 1', '555-956-6565')

INSERT INTO "band" ("Name", "CountryOfOrigin", "NumberOfMembers", "Website", "Style", "IsSigned", "ContactName", "ContactPhoneNumber")
VALUES ('Sigur Ros', 'Iceland', 5, 'SigurRos.com', 'Post Rock', TRUE, 'Jonsi', '485-965-8565'), ('Jinger', 'Ukraine', 4, 'Jinger.net', 'Metal', FALSE, 'Louie', '985-854-4584'),
('Ernie and the Rubber Ducks', 'Bathtub', 2, 'ErnieDuck.com', 'Hardcore Punk', FALSE, 'Ernie Muppet', '555-555-5555')

SELECT * FROM band;

CREATE TABLE "Album" ( 
    "Id" SERIAL PRIMARY KEY,
    "Title" TEXT,
    "IsExplicit" BOOLEAN,
    "ReleaseDate" DATE
 );
 
 ALTER TABLE "Album" ADD COLUMN "BandID" INTEGER REFERENCES "band"("Id")
 
 INSERT INTO "Album" ("Title", "IsExplicit", "ReleaseDate", "BandID")
 VALUES ('OK Computer', TRUE, '1997-05-21', 2)
         
   
CREATE TABLE "Song" (
    "Id" SERIAL PRIMARY KEY,
    "TrackNumber" INT,
    "Title" TEXT,
    "Duration" INT,
    "AlbumID" INT
  );
  
  ALTER TABLE "Song" ADD COLUMN "Duration" TIME;
  
  INSERT INTO "Song" ("TrackNumber", "Title", "Duration", "AlbumID")
  VALUES (1, 'Airbag', '04:44:00' , 1)
          
  SELECT * FROM "Album";

UPDATE "band" SET "IsSigned" = 'FALSE'
WHERE "Name" = 'Sigur Ros'

UPDATE "band" SET "IsSigned" = 'TRUE'
WHERE "Name" = 'Jinger'

SELECT * FROM "Album" WHERE "BandID" = 2

SELECT * 
FROM "Album" 
JOIN "Song" ON "Album"."Id" = "Song"."AlbumID"
ORDER BY "ReleaseDate"

SELECT *
FROM "band"
WHERE "IsSigned" = TRUE

SELECT *
FROM "band"
WHERE "IsSigned" = FALSE
