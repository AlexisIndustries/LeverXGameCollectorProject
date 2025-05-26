CREATE TABLE "Game" (
  "id" integer PRIMARY KEY,
  "title" varchar,
  "release_date" date,
  "description" text,
  "platform_id" integer,
  "genre_id" integer,
  "developer_id" integer
);

CREATE TABLE "Platform" (
  "id" integer PRIMARY KEY,
  "name" varchar,
  "manufacturer" varchar,
  "release_year" integer
);

CREATE TABLE "Genre" (
  "id" integer PRIMARY KEY,
  "name" varchar,
  "description" text,
  "popularity" varchar
);

CREATE TABLE "Developer" (
  "id" integer PRIMARY KEY,
  "name" varchar,
  "country" varchar,
  "website" varchar,
  "founded" date
);

CREATE TABLE "Review" (
  "id" integer PRIMARY KEY,
  "game_id" integer,
  "reviewer_name" varchar,
  "rating" integer,
  "comment" text,
  "review_date" date
);

COMMENT ON COLUMN "Review"."rating" IS '1-5';

ALTER TABLE "Game" ADD FOREIGN KEY ("platform_id") REFERENCES "Platform" ("id");

ALTER TABLE "Game" ADD FOREIGN KEY ("genre_id") REFERENCES "Genre" ("id");

ALTER TABLE "Game" ADD FOREIGN KEY ("developer_id") REFERENCES "Developer" ("id");

ALTER TABLE "Review" ADD FOREIGN KEY ("game_id") REFERENCES "Game" ("id");
