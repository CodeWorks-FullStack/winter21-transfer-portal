CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS teams(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  creatorId VARCHAR(255) NOT NULL,
  name VARCHAR(255) NOT NULL,
  conference VARCHAR(255) NOT NULL,
  division VARCHAR(255) NOT NULL,
  picture VARCHAR(255) NOT NULL,

  FOREIGN KEY(creatorId) REFERENCES accounts(id)
) default charset utf8 COMMENT '';

CREATE TABLE IF NOT EXISTS players(
  id int NOT NULL AUTO_INCREMENT PRIMARY KEY,
  teamId int,
  name VARCHAR(255) NOT NULL,
  picture VARCHAR(255) NOT NULL,
  position VARCHAR(255) NOT NULL,
  class VARCHAR(255) NOT NULL,
  height int NOT NULL,
  weight int NOT NULL,

  FOREIGN KEY(teamId) REFERENCES teams(id)
) default charset utf8 COMMENT '';

SELECT * FROM players;

DROP TABLE teams;
DROP TABLE accounts;
DROP TABLE players;

INSERT INTO players(teamId, name, picture, position, class, height, weight)
VALUES(null, 'LCarpetron DookMarriot', 'https://pbs.twimg.com/profile_images/1361828722375069697/KQmXheag_400x400.jpg', 'TE', 'FR', 6.6, 225);
INSERT INTO players(teamId, name, picture, position, class, height, weight)
VALUES(null, 'Beezer Twelve Washingbeard', 'https://pbs.twimg.com/media/EttDprkXIAEBA2A.jpg', 'WR', 'SO', 6, 185);
INSERT INTO players(teamId, name, picture, position, class, height, weight)
VALUES(null, 'Quattro Quattro', 'https://pbs.twimg.com/profile_images/378800000834036830/a4c01a57b63ba0450c6429c943fd5280_400x400.jpeg', 'DB', 'SO', 6.5, 190);
INSERT INTO players(teamId, name, picture, position, class, height, weight)
VALUES(null, 'DIsiah T Billings Clyde', 'https://i.imgur.com/rlRukMc.jpg', 'OT', 'JR', 6.7, 285);
INSERT INTO players(teamId, name, picture, position, class, height, weight)
VALUES(null, 'Dan Smith', 'https://vidspace.co/poster/5d3b64539e8f025b5da4d386', 'WR', 'SR', 6, 165);



