/*
MySQL Data Transfer
Source Host: localhost
Source Database: visualsro
Target Host: localhost
Target Database: visualsro
Date: 31.08.2010 21:49:40
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for characters
-- ----------------------------
DROP TABLE IF EXISTS `characters`;
CREATE TABLE `characters` (
  `id` int(10) unsigned NOT NULL,
  `account` int(10) unsigned NOT NULL,
  `name` varchar(45) NOT NULL,
  `chartype` int(10) unsigned NOT NULL DEFAULT '1907',
  `volume` int(10) unsigned NOT NULL DEFAULT '34',
  `level` int(10) unsigned NOT NULL DEFAULT '1',
  `experience` int(10) unsigned NOT NULL DEFAULT '0',
  `strength` int(10) unsigned NOT NULL DEFAULT '20',
  `intelligence` int(10) unsigned NOT NULL DEFAULT '30',
  `attribute` int(10) unsigned NOT NULL DEFAULT '0',
  `hp` int(10) unsigned NOT NULL DEFAULT '200',
  `mp` int(10) unsigned NOT NULL DEFAULT '300',
  `deletion_mark` tinyint(1) NOT NULL DEFAULT '0',
  `deletion_time` datetime NOT NULL DEFAULT '1000-01-01 00:00:00' COMMENT 'Datum',
  `gold` int(10) unsigned NOT NULL DEFAULT '0',
  `sp` int(10) unsigned NOT NULL DEFAULT '0',
  `gm` tinyint(1) NOT NULL DEFAULT '0',
  `xsect` int(10) unsigned NOT NULL DEFAULT '168' COMMENT 'Default loc Jangan',
  `ysect` int(10) unsigned NOT NULL DEFAULT '98',
  `xpos` int(10) unsigned NOT NULL DEFAULT '978',
  `ypos` int(10) unsigned NOT NULL DEFAULT '1097',
  `zpos` int(10) unsigned NOT NULL DEFAULT '40',
  `cur_hp` int(10) unsigned NOT NULL DEFAULT '200',
  `cur_mp` int(10) unsigned NOT NULL DEFAULT '300',
  `min_phyatk` int(10) unsigned NOT NULL DEFAULT '5',
  `max_phyatk` int(10) unsigned NOT NULL DEFAULT '10',
  `min_magatk` int(10) unsigned NOT NULL DEFAULT '5',
  `max_magatk` int(10) unsigned NOT NULL DEFAULT '10',
  `phydef` int(10) unsigned NOT NULL DEFAULT '9',
  `magdef` int(10) unsigned NOT NULL DEFAULT '7',
  `hit` int(10) unsigned NOT NULL DEFAULT '3',
  `parry` int(10) unsigned NOT NULL DEFAULT '5',
  `walkspeed` int(10) unsigned NOT NULL DEFAULT '16',
  `runspeed` int(10) unsigned NOT NULL DEFAULT '50',
  `berserkspeed` int(10) unsigned NOT NULL DEFAULT '100',
  `berserking` tinyint(1) NOT NULL DEFAULT '0',
  `pvp` int(10) unsigned NOT NULL DEFAULT '255',
  `maxitemslots` int(1) NOT NULL DEFAULT '45' COMMENT 'xycvyyxvcxy',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for items
-- ----------------------------
DROP TABLE IF EXISTS `items`;
CREATE TABLE `items` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `itemtype` int(10) unsigned NOT NULL,
  `owner` int(10) NOT NULL,
  `plusvalue` int(10) unsigned NOT NULL DEFAULT '0',
  `slot` int(10) unsigned NOT NULL,
  `quantity` int(10) unsigned NOT NULL DEFAULT '1',
  `durability` int(10) unsigned NOT NULL DEFAULT '30',
  `itemnumber` varchar(10) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=484 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for masteries
-- ----------------------------
DROP TABLE IF EXISTS `masteries`;
CREATE TABLE `masteries` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `owner` int(10) unsigned NOT NULL,
  `mastery` int(10) unsigned NOT NULL,
  `level` int(10) unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=171 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for news
-- ----------------------------
DROP TABLE IF EXISTS `news`;
CREATE TABLE `news` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `head` varchar(30) DEFAULT NULL,
  `text` varchar(250) DEFAULT NULL,
  `day` int(2) DEFAULT NULL,
  `month` int(2) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for quests
-- ----------------------------
DROP TABLE IF EXISTS `quests`;
CREATE TABLE `quests` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `owner` int(10) unsigned NOT NULL,
  `quest` int(10) unsigned NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for servers
-- ----------------------------
DROP TABLE IF EXISTS `servers`;
CREATE TABLE `servers` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(45) NOT NULL,
  `users_current` int(10) unsigned NOT NULL DEFAULT '0',
  `users_max` int(10) unsigned NOT NULL DEFAULT '500',
  `state` int(10) unsigned NOT NULL DEFAULT '1',
  `ip` varchar(45) NOT NULL,
  `port` int(10) unsigned NOT NULL DEFAULT '15000',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `username` varchar(45) NOT NULL,
  `password` varchar(45) NOT NULL,
  `failed_logins` int(10) unsigned NOT NULL DEFAULT '0',
  `banned` int(1) NOT NULL DEFAULT '0',
  `banreason` varchar(255) NOT NULL DEFAULT '...',
  `bantime` varchar(255) NOT NULL DEFAULT '3000-01-01',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `characters` VALUES ('0', '0', 'hhh', '1907', '34', '1', '0', '20', '30', '0', '200', '300', '0', '1000-01-01 00:00:00', '0', '0', '0', '168', '98', '978', '1097', '40', '200', '300', '5', '10', '5', '10', '9', '7', '3', '5', '16', '50', '100', '0', '255', '45');
INSERT INTO `characters` VALUES ('2', '2', 'manneke', '14726', '0', '1', '0', '20', '30', '0', '200', '300', '0', '1000-01-01 00:00:00', '0', '0', '1', '189', '102', '1189', '148', '17', '200', '300', '5', '10', '5', '10', '9', '7', '3', '5', '16', '50', '100', '0', '255', '45');
INSERT INTO `characters` VALUES ('3', '3', 'GoneUp2', '1907', '34', '1', '0', '20', '30', '0', '200', '300', '0', '1000-01-01 00:00:00', '0', '0', '1', '189', '102', '1179', '134', '16', '200', '300', '5', '10', '5', '10', '9', '7', '3', '5', '16', '50', '100', '0', '255', '45');
INSERT INTO `characters` VALUES ('4', '3', 'GoneUp', '1907', '34', '1', '0', '20', '30', '0', '200', '300', '0', '1000-01-01 00:00:00', '0', '0', '1', '188', '94', '695', '671', '0', '200', '300', '5', '10', '5', '10', '9', '7', '3', '5', '16', '50', '100', '0', '255', '45');
INSERT INTO `characters` VALUES ('5', '2', 'm4nn3k3', '1907', '0', '1', '0', '20', '30', '0', '200', '300', '0', '1000-01-01 00:00:00', '0', '0', '1', '188', '94', '695', '696', '0', '200', '300', '5', '10', '5', '10', '9', '7', '3', '5', '16', '50', '100', '0', '255', '45');
INSERT INTO `items` VALUES ('214', '0', '1', '0', '0', '0', '30', 'item0');
INSERT INTO `items` VALUES ('215', '0', '1', '0', '1', '0', '0', 'item1');
INSERT INTO `items` VALUES ('216', '0', '1', '0', '2', '0', '30', 'item2');
INSERT INTO `items` VALUES ('217', '0', '1', '0', '3', '0', '30', 'item3');
INSERT INTO `items` VALUES ('218', '0', '1', '0', '4', '0', '0', 'item4');
INSERT INTO `items` VALUES ('219', '0', '1', '0', '5', '0', '0', 'item5');
INSERT INTO `items` VALUES ('220', '0', '1', '0', '6', '0', '0', 'item6');
INSERT INTO `items` VALUES ('221', '0', '1', '0', '7', '0', '0', 'item7');
INSERT INTO `items` VALUES ('222', '0', '1', '0', '8', '0', '30', 'item8');
INSERT INTO `items` VALUES ('223', '0', '1', '0', '9', '0', '30', 'item9');
INSERT INTO `items` VALUES ('224', '0', '1', '0', '10', '0', '30', 'item10');
INSERT INTO `items` VALUES ('225', '0', '1', '0', '11', '0', '30', 'item11');
INSERT INTO `items` VALUES ('226', '0', '1', '0', '12', '0', '30', 'item12');
INSERT INTO `items` VALUES ('227', '0', '1', '0', '13', '0', '0', 'item13');
INSERT INTO `items` VALUES ('228', '0', '1', '0', '14', '0', '0', 'item14');
INSERT INTO `items` VALUES ('229', '0', '1', '0', '15', '0', '0', 'item15');
INSERT INTO `items` VALUES ('230', '3633', '1', '1', '16', '0', '30', 'item16');
INSERT INTO `items` VALUES ('231', '0', '1', '0', '17', '0', '0', 'item17');
INSERT INTO `items` VALUES ('232', '0', '1', '0', '18', '0', '0', 'item18');
INSERT INTO `items` VALUES ('233', '0', '1', '0', '19', '0', '0', 'item19');
INSERT INTO `items` VALUES ('234', '3643', '1', '2', '20', '0', '30', 'item20');
INSERT INTO `items` VALUES ('235', '0', '1', '0', '21', '0', '0', 'item21');
INSERT INTO `items` VALUES ('236', '0', '1', '0', '22', '0', '0', 'item22');
INSERT INTO `items` VALUES ('237', '0', '1', '0', '23', '0', '0', 'item23');
INSERT INTO `items` VALUES ('238', '0', '1', '0', '24', '0', '0', 'item24');
INSERT INTO `items` VALUES ('239', '0', '1', '0', '25', '0', '0', 'item25');
INSERT INTO `items` VALUES ('240', '0', '1', '0', '26', '0', '0', 'item26');
INSERT INTO `items` VALUES ('241', '0', '1', '0', '27', '0', '0', 'item27');
INSERT INTO `items` VALUES ('242', '0', '1', '0', '28', '0', '0', 'item28');
INSERT INTO `items` VALUES ('243', '0', '1', '0', '29', '0', '30', 'item29');
INSERT INTO `items` VALUES ('244', '0', '1', '0', '30', '0', '30', 'item30');
INSERT INTO `items` VALUES ('245', '0', '1', '0', '31', '0', '30', 'item31');
INSERT INTO `items` VALUES ('246', '0', '1', '0', '32', '0', '0', 'item32');
INSERT INTO `items` VALUES ('247', '0', '1', '0', '33', '0', '30', 'item33');
INSERT INTO `items` VALUES ('248', '0', '1', '0', '34', '0', '0', 'item34');
INSERT INTO `items` VALUES ('249', '0', '1', '0', '35', '0', '30', 'item35');
INSERT INTO `items` VALUES ('250', '0', '1', '0', '36', '0', '0', 'item36');
INSERT INTO `items` VALUES ('251', '0', '1', '0', '37', '0', '0', 'item37');
INSERT INTO `items` VALUES ('252', '0', '1', '0', '38', '0', '0', 'item38');
INSERT INTO `items` VALUES ('253', '0', '1', '0', '39', '0', '0', 'item39');
INSERT INTO `items` VALUES ('254', '3644', '1', '2', '40', '0', '30', 'item40');
INSERT INTO `items` VALUES ('255', '0', '1', '0', '41', '0', '0', 'item41');
INSERT INTO `items` VALUES ('256', '0', '1', '0', '42', '0', '30', 'item42');
INSERT INTO `items` VALUES ('257', '0', '1', '0', '43', '0', '30', 'item43');
INSERT INTO `items` VALUES ('258', '3645', '1', '2', '44', '0', '30', 'item44');
INSERT INTO `items` VALUES ('259', '0', '2', '0', '0', '0', '30', 'item0');
INSERT INTO `items` VALUES ('260', '11465', '2', '2', '1', '0', '30', 'item1');
INSERT INTO `items` VALUES ('261', '0', '2', '0', '2', '0', '30', 'item2');
INSERT INTO `items` VALUES ('262', '0', '2', '0', '3', '0', '30', 'item3');
INSERT INTO `items` VALUES ('263', '11466', '2', '2', '4', '0', '30', 'item4');
INSERT INTO `items` VALUES ('264', '11467', '2', '2', '5', '0', '30', 'item5');
INSERT INTO `items` VALUES ('265', '10735', '2', '1', '6', '0', '30', 'item6');
INSERT INTO `items` VALUES ('266', '0', '2', '0', '7', '0', '30', 'item7');
INSERT INTO `items` VALUES ('267', '0', '2', '0', '8', '0', '30', 'item8');
INSERT INTO `items` VALUES ('268', '0', '2', '0', '9', '0', '30', 'item9');
INSERT INTO `items` VALUES ('269', '0', '2', '0', '10', '0', '30', 'item10');
INSERT INTO `items` VALUES ('270', '0', '2', '0', '11', '0', '30', 'item11');
INSERT INTO `items` VALUES ('271', '0', '2', '0', '12', '0', '30', 'item12');
INSERT INTO `items` VALUES ('272', '0', '2', '0', '13', '0', '30', 'item13');
INSERT INTO `items` VALUES ('273', '0', '2', '0', '14', '0', '30', 'item14');
INSERT INTO `items` VALUES ('274', '0', '2', '0', '15', '0', '30', 'item15');
INSERT INTO `items` VALUES ('275', '0', '2', '0', '16', '0', '30', 'item16');
INSERT INTO `items` VALUES ('276', '0', '2', '0', '17', '0', '30', 'item17');
INSERT INTO `items` VALUES ('277', '0', '2', '0', '18', '0', '30', 'item18');
INSERT INTO `items` VALUES ('278', '0', '2', '0', '19', '0', '30', 'item19');
INSERT INTO `items` VALUES ('279', '0', '2', '0', '20', '0', '30', 'item20');
INSERT INTO `items` VALUES ('280', '0', '2', '0', '21', '0', '30', 'item21');
INSERT INTO `items` VALUES ('281', '0', '2', '0', '22', '0', '30', 'item22');
INSERT INTO `items` VALUES ('282', '0', '2', '0', '23', '0', '30', 'item23');
INSERT INTO `items` VALUES ('283', '0', '2', '0', '24', '0', '30', 'item24');
INSERT INTO `items` VALUES ('284', '0', '2', '0', '25', '0', '30', 'item25');
INSERT INTO `items` VALUES ('285', '0', '2', '0', '26', '0', '30', 'item26');
INSERT INTO `items` VALUES ('286', '0', '2', '0', '27', '0', '30', 'item27');
INSERT INTO `items` VALUES ('287', '0', '2', '0', '28', '0', '0', 'item28');
INSERT INTO `items` VALUES ('288', '0', '2', '0', '29', '0', '30', 'item29');
INSERT INTO `items` VALUES ('289', '0', '2', '0', '30', '0', '30', 'item30');
INSERT INTO `items` VALUES ('290', '0', '2', '0', '31', '0', '30', 'item31');
INSERT INTO `items` VALUES ('291', '0', '2', '0', '32', '0', '0', 'item32');
INSERT INTO `items` VALUES ('292', '0', '2', '0', '33', '0', '30', 'item33');
INSERT INTO `items` VALUES ('293', '0', '2', '0', '34', '0', '30', 'item34');
INSERT INTO `items` VALUES ('294', '0', '2', '0', '35', '0', '30', 'item35');
INSERT INTO `items` VALUES ('295', '0', '2', '0', '36', '0', '30', 'item36');
INSERT INTO `items` VALUES ('296', '0', '2', '0', '37', '0', '30', 'item37');
INSERT INTO `items` VALUES ('297', '0', '2', '0', '38', '0', '30', 'item38');
INSERT INTO `items` VALUES ('298', '0', '2', '0', '39', '0', '30', 'item39');
INSERT INTO `items` VALUES ('299', '0', '2', '0', '40', '0', '30', 'item40');
INSERT INTO `items` VALUES ('300', '0', '2', '0', '41', '0', '30', 'item41');
INSERT INTO `items` VALUES ('301', '0', '2', '0', '42', '0', '30', 'item42');
INSERT INTO `items` VALUES ('302', '0', '2', '0', '43', '0', '30', 'item43');
INSERT INTO `items` VALUES ('303', '0', '2', '0', '44', '0', '30', 'item44');
INSERT INTO `items` VALUES ('304', '0', '2', '0', '0', '0', '30', 'item0');
INSERT INTO `items` VALUES ('305', '11465', '2', '2', '1', '0', '30', 'item1');
INSERT INTO `items` VALUES ('306', '0', '2', '0', '2', '0', '30', 'item2');
INSERT INTO `items` VALUES ('307', '0', '2', '0', '3', '0', '30', 'item3');
INSERT INTO `items` VALUES ('308', '11466', '2', '2', '4', '0', '30', 'item4');
INSERT INTO `items` VALUES ('309', '11467', '2', '2', '5', '0', '30', 'item5');
INSERT INTO `items` VALUES ('310', '10735', '2', '1', '6', '0', '30', 'item6');
INSERT INTO `items` VALUES ('311', '0', '2', '0', '7', '0', '30', 'item7');
INSERT INTO `items` VALUES ('312', '0', '2', '0', '8', '0', '30', 'item8');
INSERT INTO `items` VALUES ('313', '0', '2', '0', '9', '0', '30', 'item9');
INSERT INTO `items` VALUES ('314', '0', '2', '0', '10', '0', '30', 'item10');
INSERT INTO `items` VALUES ('315', '0', '2', '0', '11', '0', '30', 'item11');
INSERT INTO `items` VALUES ('316', '0', '2', '0', '12', '0', '30', 'item12');
INSERT INTO `items` VALUES ('317', '0', '2', '0', '13', '0', '30', 'item13');
INSERT INTO `items` VALUES ('318', '0', '2', '0', '14', '0', '30', 'item14');
INSERT INTO `items` VALUES ('319', '0', '2', '0', '15', '0', '30', 'item15');
INSERT INTO `items` VALUES ('320', '0', '2', '0', '16', '0', '30', 'item16');
INSERT INTO `items` VALUES ('321', '0', '2', '0', '17', '0', '30', 'item17');
INSERT INTO `items` VALUES ('322', '0', '2', '0', '18', '0', '30', 'item18');
INSERT INTO `items` VALUES ('323', '0', '2', '0', '19', '0', '30', 'item19');
INSERT INTO `items` VALUES ('324', '0', '2', '0', '20', '0', '30', 'item20');
INSERT INTO `items` VALUES ('325', '0', '2', '0', '21', '0', '30', 'item21');
INSERT INTO `items` VALUES ('326', '0', '2', '0', '22', '0', '30', 'item22');
INSERT INTO `items` VALUES ('327', '0', '2', '0', '23', '0', '30', 'item23');
INSERT INTO `items` VALUES ('328', '0', '2', '0', '24', '0', '30', 'item24');
INSERT INTO `items` VALUES ('329', '0', '2', '0', '25', '0', '30', 'item25');
INSERT INTO `items` VALUES ('330', '0', '2', '0', '26', '0', '30', 'item26');
INSERT INTO `items` VALUES ('331', '0', '2', '0', '27', '0', '30', 'item27');
INSERT INTO `items` VALUES ('332', '0', '2', '0', '28', '0', '0', 'item28');
INSERT INTO `items` VALUES ('333', '0', '2', '0', '29', '0', '30', 'item29');
INSERT INTO `items` VALUES ('334', '0', '2', '0', '30', '0', '30', 'item30');
INSERT INTO `items` VALUES ('335', '0', '2', '0', '31', '0', '30', 'item31');
INSERT INTO `items` VALUES ('336', '0', '2', '0', '32', '0', '0', 'item32');
INSERT INTO `items` VALUES ('337', '0', '2', '0', '33', '0', '30', 'item33');
INSERT INTO `items` VALUES ('338', '0', '2', '0', '34', '0', '30', 'item34');
INSERT INTO `items` VALUES ('339', '0', '2', '0', '35', '0', '30', 'item35');
INSERT INTO `items` VALUES ('340', '0', '2', '0', '36', '0', '30', 'item36');
INSERT INTO `items` VALUES ('341', '0', '2', '0', '37', '0', '30', 'item37');
INSERT INTO `items` VALUES ('342', '0', '2', '0', '38', '0', '30', 'item38');
INSERT INTO `items` VALUES ('343', '0', '2', '0', '39', '0', '30', 'item39');
INSERT INTO `items` VALUES ('344', '0', '2', '0', '40', '0', '30', 'item40');
INSERT INTO `items` VALUES ('345', '0', '2', '0', '41', '0', '30', 'item41');
INSERT INTO `items` VALUES ('346', '0', '2', '0', '42', '0', '30', 'item42');
INSERT INTO `items` VALUES ('347', '0', '2', '0', '43', '0', '30', 'item43');
INSERT INTO `items` VALUES ('348', '0', '2', '0', '44', '0', '30', 'item44');
INSERT INTO `items` VALUES ('349', '3683', '3', '0', '0', '1', '30', 'item0');
INSERT INTO `items` VALUES ('350', '3643', '3', '1', '1', '0', '30', 'item1');
INSERT INTO `items` VALUES ('351', '0', '3', '0', '2', '0', '30', 'item2');
INSERT INTO `items` VALUES ('352', '0', '3', '0', '3', '0', '30', 'item3');
INSERT INTO `items` VALUES ('353', '3644', '3', '2', '4', '0', '30', 'item4');
INSERT INTO `items` VALUES ('354', '3645', '3', '0', '5', '0', '30', 'item5');
INSERT INTO `items` VALUES ('355', '0', '3', '0', '6', '0', '0', 'item6');
INSERT INTO `items` VALUES ('356', '251', '3', '7', '7', '0', '30', 'item7');
INSERT INTO `items` VALUES ('357', '0', '3', '0', '8', '0', '30', 'item8');
INSERT INTO `items` VALUES ('358', '0', '3', '0', '9', '0', '30', 'item9');
INSERT INTO `items` VALUES ('359', '0', '3', '0', '10', '0', '30', 'item10');
INSERT INTO `items` VALUES ('360', '0', '3', '0', '11', '0', '30', 'item11');
INSERT INTO `items` VALUES ('361', '0', '3', '0', '12', '0', '30', 'item12');
INSERT INTO `items` VALUES ('362', '23279', '3', '0', '13', '1', '30', 'item13');
INSERT INTO `items` VALUES ('363', '23279', '3', '0', '14', '1', '30', 'item14');
INSERT INTO `items` VALUES ('364', '0', '3', '0', '15', '0', '0', 'item15');
INSERT INTO `items` VALUES ('365', '3633', '3', '4', '16', '0', '30', 'item16');
INSERT INTO `items` VALUES ('366', '0', '3', '0', '17', '0', '30', 'item17');
INSERT INTO `items` VALUES ('367', '0', '3', '0', '18', '0', '30', 'item18');
INSERT INTO `items` VALUES ('368', '0', '3', '0', '19', '0', '30', 'item19');
INSERT INTO `items` VALUES ('369', '0', '3', '0', '20', '0', '30', 'item20');
INSERT INTO `items` VALUES ('370', '0', '3', '0', '21', '0', '30', 'item21');
INSERT INTO `items` VALUES ('371', '0', '3', '0', '22', '0', '30', 'item22');
INSERT INTO `items` VALUES ('372', '0', '3', '0', '23', '0', '30', 'item23');
INSERT INTO `items` VALUES ('373', '0', '3', '0', '24', '0', '30', 'item24');
INSERT INTO `items` VALUES ('374', '0', '3', '0', '25', '0', '30', 'item25');
INSERT INTO `items` VALUES ('375', '0', '3', '0', '26', '0', '30', 'item26');
INSERT INTO `items` VALUES ('376', '0', '3', '0', '27', '0', '30', 'item27');
INSERT INTO `items` VALUES ('377', '0', '3', '0', '28', '0', '30', 'item28');
INSERT INTO `items` VALUES ('378', '0', '3', '0', '29', '0', '30', 'item29');
INSERT INTO `items` VALUES ('379', '0', '3', '0', '30', '0', '30', 'item30');
INSERT INTO `items` VALUES ('380', '0', '3', '0', '31', '0', '30', 'item31');
INSERT INTO `items` VALUES ('381', '0', '3', '0', '32', '0', '30', 'item32');
INSERT INTO `items` VALUES ('382', '0', '3', '0', '33', '0', '30', 'item33');
INSERT INTO `items` VALUES ('383', '0', '3', '0', '34', '0', '30', 'item34');
INSERT INTO `items` VALUES ('384', '0', '3', '0', '35', '0', '30', 'item35');
INSERT INTO `items` VALUES ('385', '0', '3', '0', '36', '0', '30', 'item36');
INSERT INTO `items` VALUES ('386', '0', '3', '0', '37', '0', '30', 'item37');
INSERT INTO `items` VALUES ('387', '0', '3', '0', '38', '0', '30', 'item38');
INSERT INTO `items` VALUES ('388', '0', '3', '0', '39', '0', '30', 'item39');
INSERT INTO `items` VALUES ('389', '0', '3', '0', '40', '0', '30', 'item40');
INSERT INTO `items` VALUES ('390', '0', '3', '0', '41', '0', '30', 'item41');
INSERT INTO `items` VALUES ('391', '0', '3', '0', '42', '0', '30', 'item42');
INSERT INTO `items` VALUES ('392', '0', '3', '0', '43', '0', '30', 'item43');
INSERT INTO `items` VALUES ('393', '0', '3', '0', '44', '0', '30', 'item44');
INSERT INTO `items` VALUES ('394', '0', '4', '0', '0', '0', '0', 'item0');
INSERT INTO `items` VALUES ('395', '3643', '4', '2', '1', '0', '30', 'item1');
INSERT INTO `items` VALUES ('396', '0', '4', '0', '2', '0', '30', 'item2');
INSERT INTO `items` VALUES ('397', '0', '4', '0', '3', '0', '30', 'item3');
INSERT INTO `items` VALUES ('398', '3644', '4', '2', '4', '0', '30', 'item4');
INSERT INTO `items` VALUES ('399', '0', '4', '0', '5', '0', '0', 'item5');
INSERT INTO `items` VALUES ('400', '4055', '4', '3', '6', '0', '30', 'item6');
INSERT INTO `items` VALUES ('401', '0', '4', '0', '7', '0', '30', 'item7');
INSERT INTO `items` VALUES ('402', '0', '4', '0', '8', '0', '0', 'item8');
INSERT INTO `items` VALUES ('403', '0', '4', '0', '9', '0', '30', 'item9');
INSERT INTO `items` VALUES ('404', '0', '4', '0', '10', '0', '30', 'item10');
INSERT INTO `items` VALUES ('405', '0', '4', '0', '11', '0', '30', 'item11');
INSERT INTO `items` VALUES ('406', '0', '4', '0', '12', '0', '30', 'item12');
INSERT INTO `items` VALUES ('407', '0', '4', '0', '13', '0', '0', 'item13');
INSERT INTO `items` VALUES ('408', '3635', '4', '3', '14', '0', '30', 'item14');
INSERT INTO `items` VALUES ('409', '0', '4', '0', '15', '0', '0', 'item15');
INSERT INTO `items` VALUES ('410', '0', '4', '0', '16', '0', '0', 'item16');
INSERT INTO `items` VALUES ('411', '0', '4', '0', '17', '0', '0', 'item17');
INSERT INTO `items` VALUES ('412', '0', '4', '0', '18', '0', '0', 'item18');
INSERT INTO `items` VALUES ('413', '0', '4', '0', '19', '0', '0', 'item19');
INSERT INTO `items` VALUES ('414', '0', '4', '0', '20', '0', '0', 'item20');
INSERT INTO `items` VALUES ('415', '0', '4', '0', '21', '0', '30', 'item21');
INSERT INTO `items` VALUES ('416', '0', '4', '0', '22', '0', '30', 'item22');
INSERT INTO `items` VALUES ('417', '0', '4', '0', '23', '0', '0', 'item23');
INSERT INTO `items` VALUES ('418', '0', '4', '0', '24', '0', '0', 'item24');
INSERT INTO `items` VALUES ('419', '0', '4', '0', '25', '0', '30', 'item25');
INSERT INTO `items` VALUES ('420', '0', '4', '0', '26', '0', '30', 'item26');
INSERT INTO `items` VALUES ('421', '0', '4', '0', '27', '0', '0', 'item27');
INSERT INTO `items` VALUES ('422', '0', '4', '0', '28', '0', '30', 'item28');
INSERT INTO `items` VALUES ('423', '0', '4', '0', '29', '0', '30', 'item29');
INSERT INTO `items` VALUES ('424', '0', '4', '0', '30', '0', '30', 'item30');
INSERT INTO `items` VALUES ('425', '0', '4', '0', '31', '0', '0', 'item31');
INSERT INTO `items` VALUES ('426', '0', '4', '0', '32', '0', '0', 'item32');
INSERT INTO `items` VALUES ('427', '0', '4', '0', '33', '0', '30', 'item33');
INSERT INTO `items` VALUES ('428', '0', '4', '0', '34', '0', '30', 'item34');
INSERT INTO `items` VALUES ('429', '0', '4', '0', '35', '0', '30', 'item35');
INSERT INTO `items` VALUES ('430', '0', '4', '0', '36', '0', '30', 'item36');
INSERT INTO `items` VALUES ('431', '0', '4', '0', '37', '0', '30', 'item37');
INSERT INTO `items` VALUES ('432', '0', '4', '0', '38', '0', '30', 'item38');
INSERT INTO `items` VALUES ('433', '0', '4', '0', '39', '0', '0', 'item39');
INSERT INTO `items` VALUES ('434', '0', '4', '0', '40', '0', '30', 'item40');
INSERT INTO `items` VALUES ('435', '0', '4', '0', '41', '0', '30', 'item41');
INSERT INTO `items` VALUES ('436', '0', '4', '0', '42', '0', '30', 'item42');
INSERT INTO `items` VALUES ('437', '0', '4', '0', '43', '0', '30', 'item43');
INSERT INTO `items` VALUES ('438', '0', '4', '0', '44', '0', '0', 'item44');
INSERT INTO `items` VALUES ('439', '0', '5', '0', '0', '0', '0', 'item0');
INSERT INTO `items` VALUES ('440', '3637', '5', '1', '1', '0', '30', 'item1');
INSERT INTO `items` VALUES ('441', '0', '5', '0', '2', '0', '30', 'item2');
INSERT INTO `items` VALUES ('442', '0', '5', '0', '3', '0', '30', 'item3');
INSERT INTO `items` VALUES ('443', '4374', '5', '255', '4', '0', '30', 'item4');
INSERT INTO `items` VALUES ('444', '4446', '5', '255', '5', '0', '30', 'item5');
INSERT INTO `items` VALUES ('445', '0', '5', '3', '6', '0', '0', 'item6');
INSERT INTO `items` VALUES ('446', '0', '5', '0', '7', '0', '30', 'item7');
INSERT INTO `items` VALUES ('447', '0', '5', '0', '8', '0', '30', 'item8');
INSERT INTO `items` VALUES ('448', '0', '5', '0', '9', '0', '30', 'item9');
INSERT INTO `items` VALUES ('449', '0', '5', '0', '10', '0', '30', 'item10');
INSERT INTO `items` VALUES ('450', '0', '5', '0', '11', '0', '30', 'item11');
INSERT INTO `items` VALUES ('451', '0', '5', '0', '12', '0', '30', 'item12');
INSERT INTO `items` VALUES ('452', '3634', '5', '3', '13', '0', '30', 'item13');
INSERT INTO `items` VALUES ('453', '3679', '5', '0', '14', '1', '0', 'item14');
INSERT INTO `items` VALUES ('454', '7492', '5', '0', '15', '1', '30', 'item15');
INSERT INTO `items` VALUES ('455', '0', '5', '0', '16', '0', '0', 'item16');
INSERT INTO `items` VALUES ('456', '0', '5', '0', '17', '0', '0', 'item17');
INSERT INTO `items` VALUES ('457', '0', '5', '0', '18', '0', '30', 'item18');
INSERT INTO `items` VALUES ('458', '0', '5', '0', '19', '0', '0', 'item19');
INSERT INTO `items` VALUES ('459', '0', '5', '0', '20', '0', '0', 'item20');
INSERT INTO `items` VALUES ('460', '0', '5', '0', '21', '0', '30', 'item21');
INSERT INTO `items` VALUES ('461', '0', '5', '0', '22', '0', '30', 'item22');
INSERT INTO `items` VALUES ('462', '0', '5', '0', '23', '0', '30', 'item23');
INSERT INTO `items` VALUES ('463', '0', '5', '0', '24', '0', '30', 'item24');
INSERT INTO `items` VALUES ('464', '0', '5', '0', '25', '0', '30', 'item25');
INSERT INTO `items` VALUES ('465', '0', '5', '0', '26', '0', '30', 'item26');
INSERT INTO `items` VALUES ('466', '0', '5', '0', '27', '0', '30', 'item27');
INSERT INTO `items` VALUES ('467', '0', '5', '0', '28', '0', '0', 'item28');
INSERT INTO `items` VALUES ('468', '0', '5', '0', '29', '0', '30', 'item29');
INSERT INTO `items` VALUES ('469', '0', '5', '0', '30', '0', '30', 'item30');
INSERT INTO `items` VALUES ('470', '0', '5', '0', '31', '0', '0', 'item31');
INSERT INTO `items` VALUES ('471', '0', '5', '0', '32', '0', '0', 'item32');
INSERT INTO `items` VALUES ('472', '0', '5', '0', '33', '0', '0', 'item33');
INSERT INTO `items` VALUES ('473', '0', '5', '0', '34', '0', '30', 'item34');
INSERT INTO `items` VALUES ('474', '0', '5', '0', '35', '0', '30', 'item35');
INSERT INTO `items` VALUES ('475', '0', '5', '0', '36', '0', '30', 'item36');
INSERT INTO `items` VALUES ('476', '0', '5', '0', '37', '0', '30', 'item37');
INSERT INTO `items` VALUES ('477', '0', '5', '0', '38', '0', '30', 'item38');
INSERT INTO `items` VALUES ('478', '0', '5', '0', '39', '0', '30', 'item39');
INSERT INTO `items` VALUES ('479', '0', '5', '0', '40', '0', '30', 'item40');
INSERT INTO `items` VALUES ('480', '0', '5', '0', '41', '0', '30', 'item41');
INSERT INTO `items` VALUES ('481', '0', '5', '0', '42', '0', '30', 'item42');
INSERT INTO `items` VALUES ('482', '0', '5', '0', '43', '0', '0', 'item43');
INSERT INTO `items` VALUES ('483', '0', '5', '0', '44', '0', '30', 'item44');
INSERT INTO `masteries` VALUES ('130', '1', '257', '1');
INSERT INTO `masteries` VALUES ('131', '1', '258', '1');
INSERT INTO `masteries` VALUES ('132', '1', '259', '1');
INSERT INTO `masteries` VALUES ('133', '1', '273', '1');
INSERT INTO `masteries` VALUES ('134', '1', '274', '1');
INSERT INTO `masteries` VALUES ('135', '1', '275', '1');
INSERT INTO `masteries` VALUES ('136', '1', '276', '1');
INSERT INTO `masteries` VALUES ('137', '2', '257', '1');
INSERT INTO `masteries` VALUES ('138', '2', '258', '1');
INSERT INTO `masteries` VALUES ('139', '2', '259', '1');
INSERT INTO `masteries` VALUES ('140', '2', '273', '1');
INSERT INTO `masteries` VALUES ('141', '2', '274', '1');
INSERT INTO `masteries` VALUES ('142', '2', '275', '1');
INSERT INTO `masteries` VALUES ('143', '2', '276', '1');
INSERT INTO `masteries` VALUES ('144', '2', '513', '1');
INSERT INTO `masteries` VALUES ('145', '2', '514', '1');
INSERT INTO `masteries` VALUES ('146', '2', '515', '1');
INSERT INTO `masteries` VALUES ('147', '2', '516', '1');
INSERT INTO `masteries` VALUES ('148', '2', '517', '1');
INSERT INTO `masteries` VALUES ('149', '2', '518', '1');
INSERT INTO `masteries` VALUES ('150', '3', '257', '1');
INSERT INTO `masteries` VALUES ('151', '3', '258', '1');
INSERT INTO `masteries` VALUES ('152', '3', '259', '1');
INSERT INTO `masteries` VALUES ('153', '3', '273', '1');
INSERT INTO `masteries` VALUES ('154', '3', '274', '1');
INSERT INTO `masteries` VALUES ('155', '3', '275', '1');
INSERT INTO `masteries` VALUES ('156', '3', '276', '1');
INSERT INTO `masteries` VALUES ('157', '4', '257', '1');
INSERT INTO `masteries` VALUES ('158', '4', '258', '1');
INSERT INTO `masteries` VALUES ('159', '4', '259', '1');
INSERT INTO `masteries` VALUES ('160', '4', '273', '1');
INSERT INTO `masteries` VALUES ('161', '4', '274', '1');
INSERT INTO `masteries` VALUES ('162', '4', '275', '1');
INSERT INTO `masteries` VALUES ('163', '4', '276', '1');
INSERT INTO `masteries` VALUES ('164', '5', '257', '1');
INSERT INTO `masteries` VALUES ('165', '5', '258', '1');
INSERT INTO `masteries` VALUES ('166', '5', '259', '1');
INSERT INTO `masteries` VALUES ('167', '5', '273', '1');
INSERT INTO `masteries` VALUES ('168', '5', '274', '1');
INSERT INTO `masteries` VALUES ('169', '5', '275', '1');
INSERT INTO `masteries` VALUES ('170', '5', '276', '1');
INSERT INTO `news` VALUES ('1', 'Opening', '<center><font color=red><b>Welcome to the Visual Silkroad Project</b></font></center>', '25', '4');
INSERT INTO `servers` VALUES ('3', 'Local', '0', '500', '1', '127.0.0.1', '15780');
INSERT INTO `servers` VALUES ('4', 'Remote', '0', '500', '1', '84.157.145.56', '15780');
INSERT INTO `users` VALUES ('2', 'test', 'test', '1', '0', '...', '3000-01-02');
INSERT INTO `users` VALUES ('3', 'i', 'i', '1', '0', '...', '3000-01-02');