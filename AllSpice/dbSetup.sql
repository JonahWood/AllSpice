CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


CREATE TABLE recipes(
  id INT AUTO_INCREMENT PRIMARY KEY,
  title VARCHAR(50) NOT NULL,
  instructions VARCHAR(500) NOT NULL,
  img VARCHAR(500) NOT NULL,
  category VARCHAR(50) NOT NULL,
  creatorId VARCHAR(255) NOT NULL,

  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE recipes;

INSERT INTO recipes
(title, instructions, img, category, creatorId)
VALUES
('Mac n cheese', 'boil water, cook noodles, add cheese and there you go', 'https://codeworks.blob.core.windows.net/public/assets/img/projects/AllSpicePoster.jpg', 'pasts', '64013315bd1dde3ace531c41')