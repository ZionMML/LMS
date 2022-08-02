CREATE DATABASE  IF NOT EXISTS `LMS`;
USE `LMS`;

DROP TABLE IF EXISTS `lms_users`;

CREATE TABLE `lms_users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `user_id` varchar(10),
  `user_name` varchar(40) DEFAULT NULL,
  `password` varchar(20) DEFAULT NULL,
  `company` varchar(40) DEFAULT NULL,
  `team_lead_name` varchar(40) DEFAULT NULL,
  `max_annual_leave` integer DEFAULT 0,
  `annual_leave_balance` integer DEFAULT 0,
  `max_medical_leave` integer DEFAULT 0,
  `medical_leave_balance` integer DEFAULT 0,
  `max_other_leave` integer DEFAULT 0,
  `other_leave_balance` integer DEFAULT 0,
  `created_by` varchar(10) DEFAULT NULL,
  `created_date` DATE,
  `updated_by` varchar(10) DEFAULT NULL,
  `updated_date` DATE,
  `balance_updated_by` varchar(10) DEFAULT NULL,
  `balance_updated_date` DATE,
  PRIMARY KEY (`id`,`user_id`)
) ENGINE=InnoDB AUTO_INCREMENT=1 DEFAULT CHARSET=latin1;

