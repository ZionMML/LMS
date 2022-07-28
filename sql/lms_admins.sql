CREATE DATABASE  IF NOT EXISTS `LMS`;
USE `LMS`;

DROP TABLE IF EXISTS `lms_admins`;

CREATE TABLE `lms_admins` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` varchar(10),
  `user_name` varchar(40) DEFAULT NULL,
  `password` varchar(20) DEFAULT NULL,
  `email_addr` varchar(100) DEFAULT NULL,
  `created_by` varchar(10) DEFAULT NULL,
  `created_date` DATE,
  `updated_by` varchar(10) DEFAULT NULL,
  `updated_date` DATE,
  PRIMARY KEY (`id`,`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

select * from lms_admins;

--
-- Data for table `employee`
--

INSERT INTO `lms_admins` VALUES 
	(1,'H_2022','Harry','password123','harry@gmail.com','SYSTEM',sysdate(),null, null);