-- MySQL dump 10.14  Distrib 5.5.60-MariaDB, for Linux (x86_64)
--
-- Host: 127.0.0.1    Database: fa
-- ------------------------------------------------------
-- Server version	5.5.60-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `fa_app_version`
--

DROP TABLE IF EXISTS `fa_app_version`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_app_version` (
  `ID` int(11) NOT NULL,
  `IS_NEW` decimal(1,0) NOT NULL,
  `TYPE` varchar(20) NOT NULL COMMENT 'ipk\n            apk',
  `REMARK` varchar(1000) DEFAULT NULL,
  `UPDATE_TIME` datetime DEFAULT NULL,
  `UPDATE_URL` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_app_version`
--

LOCK TABLES `fa_app_version` WRITE;
/*!40000 ALTER TABLE `fa_app_version` DISABLE KEYS */;
INSERT INTO `fa_app_version` VALUES (1,0,'20002','1、更新包\r\n2、测试更新1','2018-01-06 00:41:15','http://app.wjbjp.cn/apk/w.apk');
/*!40000 ALTER TABLE `fa_app_version` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_bulletin`
--

DROP TABLE IF EXISTS `fa_bulletin`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_bulletin` (
  `ID` int(11) NOT NULL,
  `TITLE` varchar(255) NOT NULL,
  `PIC` varchar(255) DEFAULT NULL,
  `TYPE_CODE` varchar(50) DEFAULT NULL,
  `CONTENT` text,
  `USER_ID` int(11) DEFAULT NULL,
  `PUBLISHER` varchar(255) NOT NULL,
  `ISSUE_DATE` datetime NOT NULL,
  `IS_SHOW` decimal(1,0) NOT NULL,
  `IS_IMPORT` decimal(1,0) NOT NULL,
  `IS_URGENT` decimal(1,0) NOT NULL,
  `AUTO_PEN` decimal(1,0) NOT NULL,
  `CREATE_TIME` datetime NOT NULL,
  `UPDATE_TIME` datetime NOT NULL,
  `REGION` varchar(10) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_bulletin`
--

LOCK TABLES `fa_bulletin` WRITE;
/*!40000 ALTER TABLE `fa_bulletin` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_bulletin` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_bulletin_file`
--

DROP TABLE IF EXISTS `fa_bulletin_file`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_bulletin_file` (
  `BULLETIN_ID` int(11) NOT NULL,
  `FILE_ID` int(11) NOT NULL,
  PRIMARY KEY (`BULLETIN_ID`,`FILE_ID`),
  KEY `FK_FA_BULLETIN_FILE_REF_FILE` (`FILE_ID`),
  CONSTRAINT `fa_bulletin_file_ibfk_1` FOREIGN KEY (`BULLETIN_ID`) REFERENCES `fa_bulletin` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_bulletin_file_ibfk_2` FOREIGN KEY (`FILE_ID`) REFERENCES `fa_files` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_bulletin_file`
--

LOCK TABLES `fa_bulletin_file` WRITE;
/*!40000 ALTER TABLE `fa_bulletin_file` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_bulletin_file` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_bulletin_log`
--

DROP TABLE IF EXISTS `fa_bulletin_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_bulletin_log` (
  `ID` int(11) NOT NULL,
  `BULLETIN_ID` int(11) NOT NULL,
  `USER_ID` int(11) NOT NULL,
  `LOOK_TIME` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_BULLETIN_LOG_REF_BULLETIN` (`BULLETIN_ID`),
  CONSTRAINT `fa_bulletin_log_ibfk_1` FOREIGN KEY (`BULLETIN_ID`) REFERENCES `fa_bulletin` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_bulletin_log`
--

LOCK TABLES `fa_bulletin_log` WRITE;
/*!40000 ALTER TABLE `fa_bulletin_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_bulletin_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_bulletin_review`
--

DROP TABLE IF EXISTS `fa_bulletin_review`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_bulletin_review` (
  `ID` int(11) NOT NULL,
  `PARENT_ID` int(11) DEFAULT NULL,
  `BULLETIN_ID` int(11) NOT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `CONTENT` text,
  `USER_ID` int(11) NOT NULL,
  `ADD_TIME` datetime NOT NULL,
  `STATUS` varchar(10) NOT NULL COMMENT '正常\n            删除',
  `STATUS_TIME` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_BULLETIN_REVIEW_REF_BULL` (`BULLETIN_ID`),
  KEY `FK_FA_BULLETIN_RE_REF_REVIEW` (`PARENT_ID`),
  CONSTRAINT `fa_bulletin_review_ibfk_1` FOREIGN KEY (`BULLETIN_ID`) REFERENCES `fa_bulletin` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_bulletin_review_ibfk_2` FOREIGN KEY (`PARENT_ID`) REFERENCES `fa_bulletin_review` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_bulletin_review`
--

LOCK TABLES `fa_bulletin_review` WRITE;
/*!40000 ALTER TABLE `fa_bulletin_review` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_bulletin_review` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_bulletin_role`
--

DROP TABLE IF EXISTS `fa_bulletin_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_bulletin_role` (
  `BULLETIN_ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  PRIMARY KEY (`BULLETIN_ID`,`ROLE_ID`),
  KEY `FK_FA_BULLETIN_ROLE_REF_ROLE` (`ROLE_ID`),
  CONSTRAINT `fa_bulletin_role_ibfk_1` FOREIGN KEY (`BULLETIN_ID`) REFERENCES `fa_bulletin` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_bulletin_role_ibfk_2` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_bulletin_role`
--

LOCK TABLES `fa_bulletin_role` WRITE;
/*!40000 ALTER TABLE `fa_bulletin_role` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_bulletin_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_bulletin_type`
--

DROP TABLE IF EXISTS `fa_bulletin_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_bulletin_type` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(80) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_bulletin_type`
--

LOCK TABLES `fa_bulletin_type` WRITE;
/*!40000 ALTER TABLE `fa_bulletin_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_bulletin_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_config`
--

DROP TABLE IF EXISTS `fa_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_config` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TYPE` varchar(10) DEFAULT NULL,
  `CODE` varchar(32) NOT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `VALUE` varchar(300) DEFAULT NULL,
  `REMARK` varchar(500) DEFAULT NULL,
  `REGION` varchar(10) NOT NULL,
  `ADD_USER_ID` int(11) DEFAULT NULL,
  `ADD_TIEM` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_config`
--

LOCK TABLES `fa_config` WRITE;
/*!40000 ALTER TABLE `fa_config` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_config` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_db_server`
--

DROP TABLE IF EXISTS `fa_db_server`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_db_server` (
  `ID` int(11) NOT NULL,
  `DB_TYPE_ID` int(11) NOT NULL,
  `TYPE` varchar(10) NOT NULL COMMENT 'DB2\n            ORACLE',
  `IP` varchar(20) NOT NULL,
  `PORT` int(11) NOT NULL,
  `DBNAME` varchar(20) DEFAULT NULL,
  `DBUID` varchar(20) NOT NULL,
  `PASSWORD` varchar(32) NOT NULL,
  `REMARK` varchar(500) DEFAULT NULL,
  `DB_LINK` varchar(200) DEFAULT NULL,
  `NICKNAME` varchar(32) DEFAULT NULL,
  `TO_PATH_M` varchar(300) DEFAULT NULL,
  `TO_PATH_D` varchar(300) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_DB_SERVER_REF_TYPE` (`DB_TYPE_ID`),
  CONSTRAINT `fa_db_server_ibfk_1` FOREIGN KEY (`DB_TYPE_ID`) REFERENCES `fa_db_server_type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_db_server`
--

LOCK TABLES `fa_db_server` WRITE;
/*!40000 ALTER TABLE `fa_db_server` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_db_server` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_db_server_type`
--

DROP TABLE IF EXISTS `fa_db_server_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_db_server_type` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(20) DEFAULT NULL,
  `REMARK` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_db_server_type`
--

LOCK TABLES `fa_db_server_type` WRITE;
/*!40000 ALTER TABLE `fa_db_server_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_db_server_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_district`
--

DROP TABLE IF EXISTS `fa_district`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_district` (
  `ID` int(11) NOT NULL,
  `PARENT_ID` int(11) DEFAULT NULL,
  `NAME` varchar(255) NOT NULL,
  `CODE` varchar(50) DEFAULT NULL,
  `IN_USE` decimal(1,0) NOT NULL,
  `LEVEL_ID` int(11) NOT NULL COMMENT '1市\n            2区县\n            3片区\n            4网格',
  `ID_PATH` varchar(200) DEFAULT NULL,
  `REGION` varchar(10) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_DISTRICT_REF_DISTRICT` (`PARENT_ID`),
  CONSTRAINT `fa_district_ibfk_1` FOREIGN KEY (`PARENT_ID`) REFERENCES `fa_district` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_district`
--

LOCK TABLES `fa_district` WRITE;
/*!40000 ALTER TABLE `fa_district` DISABLE KEYS */;
INSERT INTO `fa_district` VALUES (1,NULL,'四川','shichuan',0,0,'.','1'),(3,1,'南充','nc',0,1,'.1.','1'),(4,NULL,'贵州','gz',0,0,'.','0'),(5,4,'凯里','kn',0,1,'.4.','0'),(6,3,'test','test',0,2,'.1.3.','1'),(7,6,'test1','test1',0,3,'.1.3.6.','1');
/*!40000 ALTER TABLE `fa_district` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_dynasty`
--

DROP TABLE IF EXISTS `fa_dynasty`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_dynasty` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(20) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_dynasty`
--

LOCK TABLES `fa_dynasty` WRITE;
/*!40000 ALTER TABLE `fa_dynasty` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_dynasty` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_elder`
--

DROP TABLE IF EXISTS `fa_elder`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_elder` (
  `ID` int(11) NOT NULL,
  `FAMILY_ID` int(11) DEFAULT NULL,
  `NAME` varchar(2) NOT NULL,
  `SORT` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_ELDER_REF_FAMILY` (`FAMILY_ID`),
  CONSTRAINT `fa_elder_ibfk_1` FOREIGN KEY (`FAMILY_ID`) REFERENCES `fa_family` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_elder`
--

LOCK TABLES `fa_elder` WRITE;
/*!40000 ALTER TABLE `fa_elder` DISABLE KEYS */;
INSERT INTO `fa_elder` VALUES (1,1,'均',1),(2,1,'庆',2),(3,1,'常',3),(4,NULL,'在',NULL),(5,NULL,'道',NULL),(6,NULL,'景',NULL),(7,NULL,'伯',NULL),(8,NULL,'佰',NULL),(9,NULL,'万',NULL),(10,NULL,'兴',NULL),(11,NULL,'朝',NULL),(12,NULL,'挺',NULL),(13,NULL,'天',NULL),(14,NULL,'学',NULL),(15,NULL,'文',NULL),(16,NULL,'启',NULL),(17,NULL,'光',NULL),(18,NULL,'先',NULL),(19,NULL,'大',NULL),(20,NULL,'大',NULL),(21,NULL,'炳',NULL),(22,NULL,'世',NULL),(23,NULL,'泽',NULL),(24,NULL,'德',NULL),(25,NULL,'茂',NULL),(26,NULL,'定',NULL),(27,NULL,'显',NULL),(28,NULL,'杨',NULL);
/*!40000 ALTER TABLE `fa_elder` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_equipment`
--

DROP TABLE IF EXISTS `fa_equipment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_equipment` (
  `ID` int(11) NOT NULL COMMENT 'ID',
  `NAME` varchar(50) CHARACTER SET utf8 DEFAULT NULL COMMENT 'NAME',
  `PARENT_ID` int(11) DEFAULT NULL,
  `TABLE_TYPE_ID` int(11) NOT NULL,
  `INTRODUCE` varchar(50) CHARACTER SET utf8 NOT NULL,
  `STAUTS` varchar(15) CHARACTER SET utf8 NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_equipment`
--

LOCK TABLES `fa_equipment` WRITE;
/*!40000 ALTER TABLE `fa_equipment` DISABLE KEYS */;
INSERT INTO `fa_equipment` VALUES (2,'软件设备',NULL,0,'软件设备','正常'),(3,'操作系统',2,8,'操作系统','正常'),(4,'开发工具',2,9,'开发工具','正常');
/*!40000 ALTER TABLE `fa_equipment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_event_files`
--

DROP TABLE IF EXISTS `fa_event_files`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_event_files` (
  `EVENT_ID` int(11) NOT NULL,
  `FILES_ID` int(11) NOT NULL,
  PRIMARY KEY (`EVENT_ID`,`FILES_ID`),
  KEY `FK_FA_EVENT_FILES_REF_FILES` (`FILES_ID`),
  CONSTRAINT `fa_event_files_ibfk_1` FOREIGN KEY (`EVENT_ID`) REFERENCES `fa_user_event` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_event_files_ibfk_2` FOREIGN KEY (`FILES_ID`) REFERENCES `fa_files` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_event_files`
--

LOCK TABLES `fa_event_files` WRITE;
/*!40000 ALTER TABLE `fa_event_files` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_event_files` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_export_log`
--

DROP TABLE IF EXISTS `fa_export_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_export_log` (
  `ID` int(11) NOT NULL,
  `USER_ID` int(11) DEFAULT NULL,
  `LOGIN_NAME` varchar(50) DEFAULT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `SQL_CONTENT` text,
  `EXPORT_TIME` datetime DEFAULT NULL,
  `REMARK` varchar(100) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_export_log`
--

LOCK TABLES `fa_export_log` WRITE;
/*!40000 ALTER TABLE `fa_export_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_export_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_family`
--

DROP TABLE IF EXISTS `fa_family`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_family` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(20) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_family`
--

LOCK TABLES `fa_family` WRITE;
/*!40000 ALTER TABLE `fa_family` DISABLE KEYS */;
INSERT INTO `fa_family` VALUES (1,'翁');
/*!40000 ALTER TABLE `fa_family` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_family_books`
--

DROP TABLE IF EXISTS `fa_family_books`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_family_books` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FAMILY_ID` int(11) NOT NULL DEFAULT '1',
  `NAME` varchar(20) NOT NULL DEFAULT '1',
  `TYPE_ID` int(11) NOT NULL DEFAULT '2' COMMENT '1图片，2关系，3Word格式,4EXCEL,5PDF',
  `SORT` int(11) NOT NULL COMMENT '排序',
  `UserID` int(11) NOT NULL COMMENT '用户ID',
  `FileID` int(11) NOT NULL COMMENT '文件ID',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=246 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_family_books`
--

LOCK TABLES `fa_family_books` WRITE;
/*!40000 ALTER TABLE `fa_family_books` DISABLE KEYS */;
INSERT INTO `fa_family_books` VALUES (81,1,'翁庆三',2,81,2,0),(82,1,'翁道德',2,82,1007,0),(83,1,'翁廷玺',2,83,1014,0),(84,1,'翁光纹',2,84,1019,0),(91,1,'翁先纭',2,91,106,0),(92,1,'翁炳琏',2,92,25,0),(93,1,'翁炳瑚',2,93,27,0),(94,1,'翁炳元',2,94,63,0),(95,1,'翁炳禄',2,95,105,0),(96,1,'翁炳富',2,96,109,0),(97,1,'翁炳贵',2,97,110,0),(98,1,'翁炳荣',2,98,111,0),(99,1,'翁炳华',2,99,112,0),(101,1,'翁德锡',2,101,42,0),(102,1,'翁德应',2,102,49,0),(103,1,'翁德学',2,103,52,0),(104,1,'翁德周',2,104,54,0),(105,1,'翁德易',2,105,1073,0),(106,1,'翁德炳',2,106,10,0),(107,1,'翁德惠',2,107,21,0),(108,1,'翁德孝',2,108,74,0),(109,1,'翁德弟',2,109,75,0),(110,1,'翁德忠',2,110,76,0),(111,1,'翁德信',2,111,77,0),(112,1,'翁德智',2,112,78,0),(113,1,'翁德春',2,113,79,0),(114,1,'翁德让',2,114,80,0),(115,1,'翁德超',2,115,91,0),(116,1,'翁德群',2,116,92,0),(117,1,'翁德芳',2,117,93,0),(118,1,'翁德联',2,118,94,0),(119,1,'翁德贵',2,119,95,0),(120,1,'翁德权',2,120,131,0),(121,1,'翁德政',2,121,133,0),(122,1,'翁德茂',2,122,135,0),(123,1,'翁德轩',2,123,136,0),(124,1,'翁德俊',2,124,138,0),(125,1,'翁德玉',2,125,139,0),(126,1,'翁德太',2,126,140,0),(127,1,'翁德汉',2,127,141,0),(128,1,'翁德禄',2,128,143,0),(129,1,'翁德乾',2,129,144,0),(130,1,'翁德亮',2,130,145,0),(131,1,'翁德金',2,131,146,0),(132,1,'翁德荣',2,132,147,0),(133,1,'翁德章',2,133,148,0),(134,1,'翁德书',2,134,149,0),(135,1,'翁德龙',2,135,151,0),(136,1,'翁德凤',2,136,152,0),(137,1,'翁德富',2,137,153,0),(138,1,'翁德义',2,138,154,0),(139,1,'翁德润',2,139,155,0),(140,1,'翁德勇',2,140,156,0),(141,1,'翁定发',2,141,1288,0),(142,1,'翁志华',2,142,1297,0),(143,1,'翁定元',2,143,1367,0),(152,1,'家谱第1页',1,152,0,1),(153,1,'家谱第2页',1,153,0,2),(154,1,'家谱第3页',1,154,0,3),(155,1,'家谱第4页',1,155,0,4),(156,1,'家谱第5页',1,156,0,5),(157,1,'家谱第6页',1,157,0,6),(158,1,'家谱第7页',1,158,0,7),(159,1,'家谱第8页',1,159,0,8),(160,1,'家谱第9页',1,160,0,9),(161,1,'家谱第10页',1,161,0,10),(162,1,'家谱第11页',1,162,0,11),(163,1,'家谱第12页',1,163,0,12),(164,1,'家谱第13页',1,164,0,13),(165,1,'家谱第14页',1,165,0,14),(166,1,'家谱第15页',1,166,0,15),(167,1,'家谱第16页',1,167,0,16),(168,1,'家谱第17页',1,168,0,17),(169,1,'家谱第18页',1,169,0,18),(170,1,'家谱第19页',1,170,0,19),(171,1,'家谱第20页',1,171,0,20),(172,1,'家谱第21页',1,172,0,21),(173,1,'家谱第22页',1,173,0,22),(174,1,'家谱第23页',1,174,0,23),(175,1,'家谱第24页',1,175,0,24),(176,1,'家谱第25页',1,176,0,25),(177,1,'家谱第26页',1,177,0,26),(178,1,'家谱第27页',1,178,0,27),(179,1,'家谱第28页',1,179,0,28),(180,1,'家谱第29页',1,180,0,29),(181,1,'家谱第30页',1,181,0,30),(182,1,'家谱第31页',1,182,0,31),(183,1,'家谱第32页',1,183,0,32),(184,1,'家谱第33页',1,184,0,33),(185,1,'家谱第34页',1,185,0,34),(186,1,'家谱第35页',1,186,0,35),(187,1,'家谱第36页',1,187,0,36),(188,1,'家谱第37页',1,188,0,37),(189,1,'家谱第38页',1,189,0,38),(190,1,'家谱第39页',1,190,0,39),(191,1,'家谱第40页',1,191,0,40),(192,1,'家谱第41页',1,192,0,41),(193,1,'家谱第42页',1,193,0,42),(194,1,'家谱第43页',1,194,0,43),(195,1,'家谱第44页',1,195,0,44),(196,1,'家谱第45页',1,196,0,45),(197,1,'家谱第46页',1,197,0,46),(198,1,'家谱第47页',1,198,0,47),(199,1,'家谱第48页',1,199,0,48),(200,1,'家谱第49页',1,200,0,49),(201,1,'家谱第50页',1,201,0,50),(202,1,'家谱第51页',1,202,0,51),(203,1,'家谱第52页',1,203,0,52),(204,1,'家谱第53页',1,204,0,53),(205,1,'家谱第54页',1,205,0,54),(206,1,'家谱第55页',1,206,0,55),(207,1,'家谱第56页',1,207,0,56),(208,1,'家谱第57页',1,208,0,57),(209,1,'家谱第58页',1,209,0,58),(210,1,'家谱第59页',1,210,0,59),(211,1,'家谱第60页',1,211,0,60),(212,1,'家谱第61页',1,212,0,61),(213,1,'家谱第62页',1,213,0,62),(214,1,'家谱第63页',1,214,0,63),(215,1,'家谱第64页',1,215,0,64),(216,1,'家谱第65页',1,216,0,65),(217,1,'家谱第66页',1,217,0,66),(218,1,'家谱第67页',1,218,0,67),(219,1,'家谱第68页',1,219,0,68),(220,1,'家谱第69页',1,220,0,69),(221,1,'家谱第70页',1,221,0,70),(222,1,'家谱第71页',1,222,0,71),(223,1,'家谱第72页',1,223,0,72),(224,1,'家谱第73页',1,224,0,73),(225,1,'家谱第74页',1,225,0,74),(226,1,'家谱第75页',1,226,0,75),(227,1,'家谱第76页',1,227,0,76),(228,1,'家谱第77页',1,228,0,77),(229,1,'家谱第78页',1,229,0,78),(230,1,'家谱第79页',1,230,0,79),(231,1,'家谱第80页',1,231,0,80),(232,1,'家谱第81页',1,232,0,81),(233,1,'家谱第82页',1,233,0,82),(234,1,'家谱第83页',1,234,0,83),(235,1,'家谱第84页',1,235,0,84),(236,1,'家谱第85页',1,236,0,85),(237,1,'家谱第86页',1,237,0,86),(238,1,'家谱第87页',1,238,0,87),(239,1,'家谱第88页',1,239,0,88),(240,1,'家谱第89页',1,240,0,89),(241,1,'家谱第90页',1,241,0,90),(242,1,'家谱第91页',1,242,0,91),(243,1,'家谱第92页',1,243,0,92),(244,1,'家谱第93页',1,244,0,93),(245,1,'家谱第94页',1,245,0,94);
/*!40000 ALTER TABLE `fa_family_books` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_files`
--

DROP TABLE IF EXISTS `fa_files`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_files` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(255) NOT NULL,
  `PATH` varchar(200) NOT NULL,
  `USER_ID` int(11) DEFAULT NULL,
  `LENGTH` int(11) NOT NULL,
  `UPLOAD_TIME` datetime DEFAULT NULL,
  `REMARK` varchar(2000) DEFAULT NULL,
  `URL` varchar(254) DEFAULT NULL,
  `FILE_TYPE` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_files`
--

LOCK TABLES `fa_files` WRITE;
/*!40000 ALTER TABLE `fa_files` DISABLE KEYS */;
INSERT INTO `fa_files` VALUES (5,'02161831632.jpg','/root/github/FaApi/WebApi/UpFiles/201903/02161831632.jpg',NULL,454591,'2019-03-02 16:20:50',NULL,'api/Public/LookfileByPath/201903/02161831632.jpg','.jpg'),(6,'04025542014.jpg','/root/github/FaApi/WebApi/UpFiles/201903/04025542014.jpg',NULL,503716,'2019-03-04 02:55:53',NULL,'api/Public/LookfileByPath/201903/04025542014.jpg','.jpg'),(7,'29061759195.jpg','/root/github/FaApi/WebApi/UpFiles/201903/29061759195.jpg',NULL,271446,'2019-03-29 06:18:05',NULL,'api/Public/LookfileByPath/201903/29061759195.jpg','.jpg'),(8,'23135435796.jpg','/root/github/FaApi/WebApi/UpFiles/201905/23135435796.jpg',NULL,33418,'2019-05-23 13:54:42',NULL,'api/Public/LookfileByPath/201905/23135435796.jpg','.jpg'),(9,'24002852258.jpg','/root/github/FaApi/WebApi/UpFiles/201905/24002852258.jpg',NULL,38052,'2019-05-24 00:28:59',NULL,'api/Public/LookfileByPath/201905/24002852258.jpg','.jpg'),(10,'24003558911.jpg','/root/github/FaApi/WebApi/UpFiles/201905/24003558911.jpg',NULL,70237,'2019-05-24 00:36:04',NULL,'api/Public/LookfileByPath/201905/24003558911.jpg','.jpg'),(11,'24095920154.jpg','/root/github/FaApi/WebApi/UpFiles/201905/24095920154.jpg',NULL,28248,'2019-05-24 09:59:22',NULL,'api/Public/LookfileByPath/201905/24095920154.jpg','.jpg'),(12,'24112849562.jpg','/root/github/FaApi/WebApi/UpFiles/201905/24112849562.jpg',NULL,10990,'2019-05-24 11:28:54',NULL,'api/Public/LookfileByPath/201905/24112849562.jpg','.jpg'),(13,'24113941571.jpg','/root/github/FaApi/WebApi/UpFiles/201905/24113941571.jpg',NULL,23447,'2019-05-24 11:39:44',NULL,'api/Public/LookfileByPath/201905/24113941571.jpg','.jpg'),(14,'24114155354.jpg','/root/github/FaApi/WebApi/UpFiles/201905/24114155354.jpg',NULL,15883,'2019-05-24 11:41:58',NULL,'api/Public/LookfileByPath/201905/24114155354.jpg','.jpg'),(15,'24115602881.jpg','/root/github/FaApi/WebApi/UpFiles/201905/24115602881.jpg',NULL,34265,'2019-05-24 11:56:05',NULL,'api/Public/LookfileByPath/201905/24115602881.jpg','.jpg'),(16,'25222804023.jpg','/root/github/FaApi/WebApi/UpFiles/201905/25222804023.jpg',NULL,132862,'2019-05-25 22:28:11',NULL,'api/Public/LookfileByPath/201905/25222804023.jpg','.jpg'),(17,'25234617492.jpg','/root/github/FaApi/WebApi/UpFiles/201905/25234617492.jpg',NULL,29106,'2019-05-25 23:46:22',NULL,'api/Public/LookfileByPath/201905/25234617492.jpg','.jpg'),(18,'26000536459.jpg','/root/github/FaApi/WebApi/UpFiles/201905/26000536459.jpg',NULL,33691,'2019-05-26 00:05:39',NULL,'api/Public/LookfileByPath/201905/26000536459.jpg','.jpg'),(19,'28234608543.jpg','/root/github/FaApi/WebApi/UpFiles/201905/28234608543.jpg',NULL,26746,'2019-05-28 23:46:14',NULL,'api/Public/LookfileByPath/201905/28234608543.jpg','.jpg'),(20,'30001519387.jpg','/root/github/FaApi/WebApi/UpFiles/201905/30001519387.jpg',NULL,441713,'2019-05-30 00:15:50',NULL,'api/Public/LookfileByPath/201905/30001519387.jpg','.jpg'),(21,'30002959092.jpg','/root/github/FaApi/WebApi/UpFiles/201905/30002959092.jpg',NULL,21011,'2019-05-30 00:30:02',NULL,'api/Public/LookfileByPath/201905/30002959092.jpg','.jpg'),(22,'30003055716.jpg','/root/github/FaApi/WebApi/UpFiles/201905/30003055716.jpg',NULL,31977,'2019-05-30 00:31:02',NULL,'api/Public/LookfileByPath/201905/30003055716.jpg','.jpg'),(23,'30080409387.jpg','/root/github/FaApi/WebApi/UpFiles/201905/30080409387.jpg',NULL,63678,'2019-05-30 08:04:12',NULL,'api/Public/LookfileByPath/201905/30080409387.jpg','.jpg'),(24,'10114117509.jpg','/root/github/FaApi/WebApi/UpFiles/201906/10114117509.jpg',NULL,44972,'2019-06-10 11:41:21',NULL,'api/Public/LookfileByPath/201906/10114117509.jpg','.jpg'),(25,'11124449109.jpg','/root/github/FaApi/WebApi/UpFiles/201906/11124449109.jpg',NULL,25088,'2019-06-11 12:45:24',NULL,'api/Public/LookfileByPath/201906/11124449109.jpg','.jpg'),(26,'11161712374.jpg','/root/github/FaApi/WebApi/UpFiles/201906/11161712374.jpg',NULL,33367,'2019-06-11 16:17:25',NULL,'api/Public/LookfileByPath/201906/11161712374.jpg','.jpg'),(27,'11161836667.jpg','/root/github/FaApi/WebApi/UpFiles/201906/11161836667.jpg',NULL,44835,'2019-06-11 16:18:40',NULL,'api/Public/LookfileByPath/201906/11161836667.jpg','.jpg'),(28,'11162058949.jpg','/root/github/FaApi/WebApi/UpFiles/201906/11162058949.jpg',NULL,33367,'2019-06-11 16:21:04',NULL,'api/Public/LookfileByPath/201906/11162058949.jpg','.jpg'),(29,'14151552139.jpg','/root/github/FaApi/WebApi/UpFiles/201906/14151552139.jpg',NULL,11926,'2019-06-14 15:16:12',NULL,'api/Public/LookfileByPath/201906/14151552139.jpg','.jpg');
/*!40000 ALTER TABLE `fa_files` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_flow`
--

DROP TABLE IF EXISTS `fa_flow`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_flow` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(100) NOT NULL,
  `FLOW_TYPE` varchar(20) NOT NULL COMMENT '数据外导\n            薪酬结果\n            政策修改',
  `REMARK` varchar(100) DEFAULT NULL,
  `X_Y` varchar(500) DEFAULT NULL COMMENT 'ID,\n            X,\n            Y',
  `REGION` varchar(10) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_flow`
--

LOCK TABLES `fa_flow` WRITE;
/*!40000 ALTER TABLE `fa_flow` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_flow` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_flow_flownode`
--

DROP TABLE IF EXISTS `fa_flow_flownode`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_flow_flownode` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(100) NOT NULL,
  `HANDLE_URL` varchar(200) DEFAULT NULL,
  `SHOW_URL` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_flow_flownode`
--

LOCK TABLES `fa_flow_flownode` WRITE;
/*!40000 ALTER TABLE `fa_flow_flownode` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_flow_flownode` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_flow_flownode_flow`
--

DROP TABLE IF EXISTS `fa_flow_flownode_flow`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_flow_flownode_flow` (
  `ID` int(11) NOT NULL,
  `FLOW_ID` int(11) NOT NULL,
  `FROM_FLOWNODE_ID` int(11) NOT NULL,
  `TO_FLOWNODE_ID` int(11) NOT NULL,
  `HANDLE` decimal(1,0) NOT NULL COMMENT '0:一人处理即可\n            1:所有人必须处理',
  `ASSIGNER` decimal(1,0) NOT NULL COMMENT '0表示指定角色\n            1表示指定人\n            2返回上级\n            3发起人\n            4已处理人',
  `STATUS` varchar(20) DEFAULT NULL,
  `REMARK` varchar(20) DEFAULT NULL,
  `EXPIRE_HOUR` int(11) NOT NULL COMMENT '下一步处理时长',
  PRIMARY KEY (`ID`),
  KEY `FK_FA_FLOWNODE_FLOW_REF_FLOW` (`FLOW_ID`),
  KEY `FK_FA_FLOWNODE_FLOW_REF_NODE` (`FROM_FLOWNODE_ID`),
  CONSTRAINT `fa_flow_flownode_flow_ibfk_1` FOREIGN KEY (`FLOW_ID`) REFERENCES `fa_flow` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_flow_flownode_flow_ibfk_2` FOREIGN KEY (`FROM_FLOWNODE_ID`) REFERENCES `fa_flow_flownode` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_flow_flownode_flow`
--

LOCK TABLES `fa_flow_flownode_flow` WRITE;
/*!40000 ALTER TABLE `fa_flow_flownode_flow` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_flow_flownode_flow` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_flow_flownode_role`
--

DROP TABLE IF EXISTS `fa_flow_flownode_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_flow_flownode_role` (
  `FLOW_ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  PRIMARY KEY (`FLOW_ID`,`ROLE_ID`),
  KEY `FK_FA_FLOW_REF_ROLE` (`ROLE_ID`),
  CONSTRAINT `fa_flow_flownode_role_ibfk_1` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_flow_flownode_role_ibfk_2` FOREIGN KEY (`FLOW_ID`) REFERENCES `fa_flow_flownode_flow` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_flow_flownode_role`
--

LOCK TABLES `fa_flow_flownode_role` WRITE;
/*!40000 ALTER TABLE `fa_flow_flownode_role` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_flow_flownode_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_flow_node`
--

DROP TABLE IF EXISTS `fa_flow_node`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_flow_node` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(100) NOT NULL,
  `HANDLE_URL` varchar(200) DEFAULT NULL,
  `SHOW_URL` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_flow_node`
--

LOCK TABLES `fa_flow_node` WRITE;
/*!40000 ALTER TABLE `fa_flow_node` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_flow_node` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_flow_node_flow`
--

DROP TABLE IF EXISTS `fa_flow_node_flow`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_flow_node_flow` (
  `ID` int(11) NOT NULL,
  `FLOW_ID` int(11) NOT NULL,
  `FROM_NODE_ID` int(11) NOT NULL,
  `TO_NODE_ID` int(11) NOT NULL,
  `HANDLE` decimal(1,0) NOT NULL COMMENT '0:一人处理即可\n            1:所有人必须处理',
  `ASSIGNER` decimal(1,0) NOT NULL COMMENT '0表示指定角色\n            1表示指定人\n            2返回上级\n            3发起人\n            4已处理人',
  `STATUS` varchar(20) DEFAULT NULL,
  `REMARK` varchar(20) DEFAULT NULL,
  `EXPIRE_HOUR` int(11) NOT NULL COMMENT '下一步处理时长',
  PRIMARY KEY (`ID`),
  KEY `FK_FA_FLOWNODE_FLOW_REF_FLOW` (`FLOW_ID`),
  KEY `FK_FA_FLOWNODE_FLOW_REF_NODE` (`FROM_NODE_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_flow_node_flow`
--

LOCK TABLES `fa_flow_node_flow` WRITE;
/*!40000 ALTER TABLE `fa_flow_node_flow` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_flow_node_flow` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_flow_node_role`
--

DROP TABLE IF EXISTS `fa_flow_node_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_flow_node_role` (
  `FLOW_ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  PRIMARY KEY (`FLOW_ID`,`ROLE_ID`),
  KEY `FK_FA_FLOW_REF_ROLE` (`ROLE_ID`),
  CONSTRAINT `fa_flow_node_role_ibfk_1` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_flow_node_role_ibfk_2` FOREIGN KEY (`FLOW_ID`) REFERENCES `fa_flow_node_flow` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_flow_node_role`
--

LOCK TABLES `fa_flow_node_role` WRITE;
/*!40000 ALTER TABLE `fa_flow_node_role` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_flow_node_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_function`
--

DROP TABLE IF EXISTS `fa_function`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_function` (
  `ID` int(11) NOT NULL,
  `REMARK` varchar(100) DEFAULT NULL,
  `FULL_NAME` varchar(100) DEFAULT NULL,
  `NAMESPACE` varchar(100) DEFAULT NULL,
  `CLASS_NAME` varchar(100) DEFAULT NULL,
  `METHOD_NAME` varchar(100) DEFAULT NULL,
  `DLL_NAME` varchar(100) DEFAULT NULL,
  `XML_NOTE` varchar(254) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_function`
--

LOCK TABLES `fa_function` WRITE;
/*!40000 ALTER TABLE `fa_function` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_function` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_log`
--

DROP TABLE IF EXISTS `fa_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_log` (
  `ID` int(11) NOT NULL,
  `ADD_TIME` datetime NOT NULL,
  `MODULE_NAME` varchar(100) NOT NULL,
  `USER_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_log`
--

LOCK TABLES `fa_log` WRITE;
/*!40000 ALTER TABLE `fa_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_login`
--

DROP TABLE IF EXISTS `fa_login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_login` (
  `ID` int(11) NOT NULL,
  `LOGIN_NAME` varchar(20) DEFAULT NULL,
  `PASSWORD` varchar(255) DEFAULT NULL,
  `PHONE_NO` varchar(20) DEFAULT NULL,
  `EMAIL_ADDR` varchar(255) DEFAULT NULL,
  `VERIFY_CODE` varchar(10) DEFAULT NULL,
  `VERIFY_TIME` datetime DEFAULT NULL,
  `IS_LOCKED` int(1) DEFAULT NULL,
  `PASS_UPDATE_DATE` datetime DEFAULT NULL,
  `LOCKED_REASON` varchar(255) DEFAULT NULL,
  `FAIL_COUNT` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_login`
--

LOCK TABLES `fa_login` WRITE;
/*!40000 ALTER TABLE `fa_login` DISABLE KEYS */;
INSERT INTO `fa_login` VALUES (1,'18180770313','e10adc3949ba59abbe56e057f20f883e','18180770313',NULL,'9725','2019-05-29 00:05:26',0,NULL,NULL,0),(33,'17323097208','e10adc3949ba59abbe56e057f20f883e',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(35,'15821125138','20cfc8865c1897a9d1ff650e2fb239e1',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(36,'18227378954','16885ae3b78fff27e0d6978182ce9512',NULL,NULL,'9364','2019-06-12 23:07:46',0,NULL,NULL,0),(37,'15528858269','e10adc3949ba59abbe56e057f20f883e',NULL,NULL,'9023','2019-06-10 21:59:20',0,NULL,NULL,0),(38,'15982623955','880caa9ce87470e19ca61f05e2a18481',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(39,'13408565627','1579e36e94a76caded682b3f3d75dd8b',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(40,'15983756119','de88e3e4ab202d87754078cbb2df6063',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(41,'15882602397','a65daa2d77588f2fb99257b639871940',NULL,NULL,'1173','2019-06-11 08:34:23',0,NULL,NULL,0),(42,'13059328175','c9a8cc38712bd492aa26ed4414c52496',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(43,'15583008111','b947cd326fce120283dcc0b8c1b47e7e',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(44,'18346760350','c7f1f44c6e0a5147a5bd385d0c402321',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(45,'13653087854','549bc1ce8ed1627abd1deff2fb754244',NULL,NULL,'7838','2019-06-12 12:45:20',0,NULL,'用户连续5次错误登陆，帐号锁定。',0),(46,'18318411630','549bc1ce8ed1627abd1deff2fb754244',NULL,NULL,NULL,NULL,0,NULL,NULL,2),(47,'18611992250','89c7cb51eb4fe67691616775ef925903',NULL,NULL,'5625','2019-06-12 19:57:29',0,NULL,NULL,0),(48,'18980698099','b641794049532df690ad865452f173c2',NULL,NULL,'6292','2019-06-12 23:25:38',0,NULL,NULL,0),(49,'18582275208','2f0c96aa640edd76ccf5e3046ba809f0',NULL,NULL,NULL,NULL,0,NULL,NULL,NULL),(50,'15102878560','91b3468c4f22f0ccefb3f83f770ec199',NULL,NULL,NULL,NULL,0,NULL,NULL,0),(51,'13308015598','b774d0ccf93fd6669cee2bb9afc47202',NULL,NULL,'0134','2019-06-23 15:29:27',0,NULL,NULL,0);
/*!40000 ALTER TABLE `fa_login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_login_history`
--

DROP TABLE IF EXISTS `fa_login_history`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_login_history` (
  `ID` int(11) NOT NULL,
  `USER_ID` int(11) DEFAULT NULL,
  `LOGIN_TIME` datetime DEFAULT NULL,
  `LOGIN_HOST` varchar(255) DEFAULT NULL,
  `LOGOUT_TIME` datetime DEFAULT NULL,
  `LOGIN_HISTORY_TYPE` int(11) DEFAULT NULL COMMENT '1:正常登录\n            2:密码错误\n            3:验证码错误\n            4:工号锁定',
  `MESSAGE` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_login_history`
--

LOCK TABLES `fa_login_history` WRITE;
/*!40000 ALTER TABLE `fa_login_history` DISABLE KEYS */;
INSERT INTO `fa_login_history` VALUES (1,1,NULL,'127.0.0.1','2019-01-31 23:51:34',2,'????'),(2,1,NULL,'127.0.0.1','2019-03-02 15:48:04',2,'正常退出'),(3,1,NULL,'127.0.0.1','2019-03-29 07:15:40',2,'正常退出'),(4,1,NULL,'127.0.0.1','2019-05-18 15:25:03',2,'正常退出'),(5,1,NULL,'127.0.0.1','2019-05-18 15:25:15',2,'正常退出'),(6,1,NULL,'::ffff:127.0.0.1','2019-05-23 09:19:06',2,'正常退出'),(7,1,NULL,'::ffff:127.0.0.1','2019-05-24 11:30:08',2,'正常退出'),(8,1,NULL,'::ffff:127.0.0.1','2019-05-25 00:09:45',2,'正常退出'),(9,1,NULL,'::ffff:127.0.0.1','2019-05-25 21:32:02',2,'正常退出'),(10,8,NULL,'::ffff:127.0.0.1','2019-05-25 22:28:41',2,'正常退出'),(11,8,NULL,'::ffff:127.0.0.1','2019-05-25 23:04:28',2,'正常退出'),(12,8,NULL,'::ffff:127.0.0.1','2019-05-25 23:06:47',2,'正常退出'),(13,9,NULL,'::ffff:127.0.0.1','2019-05-26 19:08:31',2,'正常退出'),(14,8,NULL,'::ffff:127.0.0.1','2019-05-26 19:25:56',2,'正常退出'),(15,1,NULL,'::ffff:127.0.0.1','2019-05-27 11:42:59',2,'正常退出'),(16,1,NULL,'::ffff:127.0.0.1','2019-05-29 00:05:04',2,'正常退出'),(17,1,NULL,'::ffff:127.0.0.1','2019-05-29 00:05:09',2,'正常退出'),(18,1,NULL,'::ffff:127.0.0.1','2019-05-29 12:00:36',2,'正常退出'),(19,1,NULL,'::ffff:127.0.0.1','2019-05-30 09:19:31',2,'正常退出'),(20,6,NULL,'::ffff:127.0.0.1','2019-05-31 08:41:06',2,'正常退出'),(21,1,NULL,'::ffff:127.0.0.1','2019-06-10 15:20:21',2,'正常退出'),(22,1,NULL,'::ffff:127.0.0.1','2019-06-10 15:25:32',2,'正常退出'),(23,1418,NULL,'::ffff:127.0.0.1','2019-06-10 21:52:50',2,'正常退出'),(24,1258,NULL,'::ffff:127.0.0.1','2019-06-11 12:36:03',2,'正常退出'),(25,1,NULL,'::ffff:127.0.0.1','2019-06-12 23:00:48',2,'正常退出'),(26,197,NULL,'::ffff:127.0.0.1','2019-06-12 23:03:55',2,'正常退出'),(27,197,NULL,'::ffff:127.0.0.1','2019-06-12 23:04:32',2,'正常退出'),(28,1,NULL,'::ffff:127.0.0.1','2019-06-14 15:08:12',2,'正常退出'),(29,1,NULL,'::ffff:127.0.0.1','2019-06-14 15:08:30',2,'正常退出'),(30,1,NULL,'127.0.0.1','2019-07-27 23:03:29',2,'正常退出');
/*!40000 ALTER TABLE `fa_login_history` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_message`
--

DROP TABLE IF EXISTS `fa_message`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_message` (
  `ID` int(11) NOT NULL,
  `MESSAGE_TYPE_ID` int(11) DEFAULT NULL,
  `KEY_ID` int(11) DEFAULT NULL,
  `TITLE` varchar(100) DEFAULT NULL,
  `CONTENT` varchar(500) DEFAULT NULL,
  `CREATE_TIME` datetime DEFAULT NULL,
  `CREATE_USERNAME` varchar(50) DEFAULT NULL,
  `CREATE_USERID` int(11) DEFAULT NULL,
  `STATUS` varchar(10) DEFAULT NULL COMMENT '正常\n            禁用',
  `PUSH_TYPE` varchar(10) DEFAULT NULL COMMENT '短信\n            手机推送\n            WEB获取\n            智能推送',
  `DISTRICT_ID` int(11) DEFAULT NULL,
  `ALL_ROLE_ID` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_MESSAGE_REF_MESSAGE_TYPE` (`MESSAGE_TYPE_ID`),
  CONSTRAINT `fa_message_ibfk_1` FOREIGN KEY (`MESSAGE_TYPE_ID`) REFERENCES `fa_message_type` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_message`
--

LOCK TABLES `fa_message` WRITE;
/*!40000 ALTER TABLE `fa_message` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_message` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_message_type`
--

DROP TABLE IF EXISTS `fa_message_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_message_type` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `TABLE_NAME` varchar(50) DEFAULT NULL,
  `IS_USE` int(11) DEFAULT NULL,
  `REMARK` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_message_type`
--

LOCK TABLES `fa_message_type` WRITE;
/*!40000 ALTER TABLE `fa_message_type` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_message_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_module`
--

DROP TABLE IF EXISTS `fa_module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_module` (
  `ID` int(11) NOT NULL,
  `PARENT_ID` int(11) DEFAULT NULL,
  `NAME` varchar(60) DEFAULT NULL,
  `LOCATION` varchar(2000) DEFAULT NULL,
  `CODE` varchar(20) DEFAULT NULL,
  `IS_DEBUG` decimal(1,0) NOT NULL,
  `IS_HIDE` decimal(1,0) NOT NULL,
  `SHOW_ORDER` decimal(2,0) NOT NULL,
  `DESCRIPTION` varchar(2000) DEFAULT NULL,
  `IMAGE_URL` varchar(2000) DEFAULT NULL,
  `DESKTOP_ROLE` varchar(200) DEFAULT NULL,
  `W` int(11) DEFAULT NULL,
  `H` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_MODULE_REF_MODULE` (`PARENT_ID`),
  CONSTRAINT `fa_module_ibfk_1` FOREIGN KEY (`PARENT_ID`) REFERENCES `fa_module` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_module`
--

LOCK TABLES `fa_module` WRITE;
/*!40000 ALTER TABLE `fa_module` DISABLE KEYS */;
INSERT INTO `fa_module` VALUES (1,NULL,'系统管理',NULL,'system',1,0,1,NULL,'nb-gear',NULL,NULL,NULL),(2,1,'角色管理','#/pages/query/query?code=role','role',1,0,3,'123','ion-person-stalker','3',1,2),(3,1,'模块管理','#/pages/query/query?code=module','module',0,0,2,NULL,'nb-grid-b-outline',NULL,NULL,NULL),(4,1,'组织结构','#/pages/query/query/district','district',0,1,3,NULL,'ion-network',NULL,NULL,NULL),(5,1,'脚本管理','#/pages/query/query?code=script','script',1,0,6,'脚本管理','nb-snowy-circled',NULL,NULL,NULL),(6,1,'定时任务','#/pages/quartztask/list','quartztask',1,0,6,'定时任务','nb-sunny-circled',NULL,NULL,NULL),(7,NULL,'设备管理',NULL,'Equiment',0,0,2,NULL,'nb-tables',NULL,NULL,NULL),(8,1,'用户管理','#/pages/query/query?code=user','user',0,0,4,NULL,'ion-person',NULL,NULL,NULL),(9,1,'查询管理','#/pages/query/list','query',0,0,1,'222','ion-android-archive',NULL,NULL,NULL),(10,7,'自定义表','#/pages/query/query?code=tableType','tableType',0,0,1,NULL,'nb-layout-centre',NULL,NULL,NULL),(11,7,'设备类型管理','#/pages/query/query?code=equipmentType','equipmentType',1,0,2,'设备类型管理','nb-e-commerce',NULL,NULL,NULL),(12,7,'设备列表','#/pages/equipment/list','equipmentList',1,0,3,'设备列表','nb-list',NULL,NULL,NULL),(13,NULL,'关系图管理',NULL,NULL,0,0,3,NULL,'nb-e-commerce',NULL,NULL,NULL),(14,13,'所有用户','#/pages/query/query?code=faUserInfo','faUserInfo',1,0,1,'所有用户','people',NULL,NULL,NULL),(15,13,'家谱管理','#/pages/query/query?code=faFamilyBooks','FaFamilyBooks',1,0,2,'家谱管理',NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `fa_module` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_oauth`
--

DROP TABLE IF EXISTS `fa_oauth`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_oauth` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `REG_URL` varchar(500) DEFAULT NULL,
  `LOGIN_URL` varchar(500) DEFAULT NULL,
  `REMARK` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_oauth`
--

LOCK TABLES `fa_oauth` WRITE;
/*!40000 ALTER TABLE `fa_oauth` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_oauth` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_oauth_login`
--

DROP TABLE IF EXISTS `fa_oauth_login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_oauth_login` (
  `OAUTH_ID` int(11) NOT NULL,
  `LOGIN_ID` int(11) NOT NULL,
  PRIMARY KEY (`OAUTH_ID`,`LOGIN_ID`),
  KEY `FK_FA_OAUTH_REF_LOGIN` (`LOGIN_ID`),
  CONSTRAINT `fa_oauth_login_ibfk_1` FOREIGN KEY (`LOGIN_ID`) REFERENCES `fa_login` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_oauth_login_ibfk_2` FOREIGN KEY (`OAUTH_ID`) REFERENCES `fa_oauth` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_oauth_login`
--

LOCK TABLES `fa_oauth_login` WRITE;
/*!40000 ALTER TABLE `fa_oauth_login` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_oauth_login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_query`
--

DROP TABLE IF EXISTS `fa_query`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_query` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(50) NOT NULL,
  `CODE` varchar(20) NOT NULL,
  `AUTO_LOAD` decimal(1,0) NOT NULL,
  `PAGE_SIZE` int(11) NOT NULL,
  `SHOW_CHECKBOX` decimal(1,0) NOT NULL,
  `IS_DEBUG` decimal(1,0) NOT NULL,
  `FILTR_LEVEL` decimal(1,0) DEFAULT NULL,
  `DB_SERVER_ID` int(11) DEFAULT NULL,
  `QUERY_CONF` text,
  `QUERY_CFG_JSON` text,
  `IN_PARA_JSON` text,
  `JS_STR` text,
  `ROWS_BTN` text,
  `HEARD_BTN` text,
  `REPORT_SCRIPT` text,
  `CHARTS_CFG` text,
  `CHARTS_TYPE` varchar(50) DEFAULT NULL,
  `FILTR_STR` text,
  `REMARK` text,
  `NEW_DATA` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_query`
--

LOCK TABLES `fa_query` WRITE;
/*!40000 ALTER TABLE `fa_query` DISABLE KEYS */;
INSERT INTO `fa_query` VALUES (1,'查询管理模板','query',1,10,1,1,1,NULL,'select * from fa_query','{\n	\"ID\": {\n		\"title\": \"查询ID\",\n		\"type\": \"number\",\n		\"editable\": false\n	},\n	\"NAME\": {\n		\"title\": \"查询名\",\n		\"tooltip\": \"用于查询\",\n		\"type\": \"string\"\n	},\n	\"CODE\": {\n		\"title\": \"代码\",\n		\"type\": \"string\"\n	},\n	\"AUTO_LOAD\": {\n		\"title\": \"自动加载\",\n		\"defaultValue\": 1,\n		\"type\": \"custom\",\n		\"renderComponent\": \"SmartTableFormatValuePage\",\n		\"onComponentInitFunction\": function(instance) {\n			instance.format = (x) => {\n				return x == 0 ? \"否\" : \"是\"\n			}\n		},\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"1\",\n						\"title\": \"是\"\n					},\n					{\n						\"value\": \"0\",\n						\"title\": \"否\"\n					}\n				]\n			}\n		}\n	},\n	\"PAGE_SIZE\": {\n		\"title\": \"页面大小\",\n		\"type\": \"number\",\n		\"defaultValue\": 10\n	},\n	\"SHOW_CHECKBOX\": {\n		\"title\": \"允许多选\",\n		\"type\": \"custom\",\n		\"renderComponent\": \"SmartTableFormatValuePage\",\n		\"onComponentInitFunction\": function(instance) {\n			instance.format = (x) => {\n				return x == 0 ? \"否\" : \"是\"\n			}\n		},\n		\"defaultValue\": 1,\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"1\",\n						\"title\": \"是\"\n					},\n					{\n						\"value\": \"0\",\n						\"title\": \"否\"\n					}\n				]\n			}\n		}\n	},\n	\"IS_DEBUG\": {\n		\"title\": \"是否隐藏\",\n		\"editable\": false,\n		\"type\": \"custom\",\n		\"renderComponent\": \"SmartTableFormatValuePage\",\n		\"onComponentInitFunction\": function(instance) {\n			instance.format = (x) => {\n				return x == 0 ? \"否\" : \"是\"\n			}\n		},\n		\"defaultValue\": 1,\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"1\",\n						\"title\": \"是\"\n					},\n					{\n						\"value\": \"0\",\n						\"title\": \"否\"\n					}\n				]\n			}\n		}\n	},\n	\"FILTR_LEVEL\": {\n		\"title\": \"过滤层级\",\n		\"editable\": false,\n		\"type\": \"number\",\n		\"defaultValue\": 1\n	},\n	\"DESKTOP_ROLE\": {\n		\"title\": \"是否首页显示\",\n		\"editable\": false,\n		\"type\": \"string\"\n	},\n	\"NEW_DATA\": {\n		\"title\": \"输入的时间\",\n		\"editable\": false,\n		\"type\": \"string\"\n	},\n	\"QUERY_CONF\": {\n		\"title\": \"查询脚本\",\n		\"tooltip\": \"能查出数据的SQL\",\n		\"type\": \"string\",\n		\"inputWidth\": 12,\n		\"isTabs\": true,\n		\"hide\": true,\n		\"editor\": {\n			\"type\": \"textarea\"\n		}\n	},\n	\"QUERY_CFG_JSON\": {\n		\"title\": \"列配置信息\",\n		\"tooltip\": \"title:标题,type:类型,inputWidth:列宽(6/12),editable:是否可编辑,editor:编辑配置,isTabs:tabs显示,tooltip:编辑框提示,hide:是否在列表显示\",\n		\"type\": \"string\",\n		\"isTabs\": true,\n		\"hide\": true,\n		\"inputWidth\": 12,\n		\"editor\": {\n			\"type\": \"textarea\"\n		}\n	},\n\n	\"IN_PARA_JSON\": {\n		\"title\": \"传入的参数\",\n		\"type\": \"string\",\n		\"isTabs\": true,\n		\"hide\": true,\n		\"inputWidth\": 12,\n		\"editor\": {\n			\"type\": \"textarea\"\n		}\n	},\n	\"JS_STR\": {\n		\"title\": \"JS脚本\",\n		\"type\": \"string\",\n		\"isTabs\": true,\n		\"hide\": true,\n		\"inputWidth\": 12,\n		\"editor\": {\n			\"type\": \"textarea\"\n		}\n	},\n	\"ROWS_BTN\": {\n		\"title\": \"行按钮\",\n		\"tooltip\": \"目前只支持两个按钮，一个修改，一个删除\",\n		\"isTabs\": true,\n		\"hide\": true,\n		\"type\": \"string\"\n	},\n	\"HEARD_BTN\": {\n		\"title\": \"表头按钮\",\n        \"tooltip\": \"添加事件：nowThis.Add(\'api地址\');批量执行：nowThis.exec(\'api地址\',\'列名\',\'确认提示信息\')；导出所有事件：nowThis.onExportXls()\",\n		\"isTabs\": true,\n		\"hide\": true,\n		\"type\": \"string\"\n	},\n\n	\"REMARK\": {\n		\"title\": \"备注\",\n		\"isTabs\": true,\n		\"hide\": true,\n		\"type\": \"string\",\n		\"inputWidth\": 12,\n		\"editor\": {\n			\"type\": \"textarea\"\n		}\n	}\n}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"apiUrl\": \"query/save\"\n	},\n	{\n		\"title\": \"删除\",\n		\"class\": \"nb-trash\",\n		\"apiUrl\": \"query/delete\",\n		\"confirmTip\": \"确定要删除吗？\"\n	}\n]','[\n  {\n    \"title\":\"添加\",\n    \"class\":\"nb-plus\",\n    \"click\":\"nowThis.Add(\'query/save\')\"\n  },\n  {\n    \"title\":\"导出\",\n    \"class\":\"ion-archive\",\n    \"click\":\"nowThis.onExportXls()\"\n  }\n  ,\n  {\n    \"title\":\"设置\",\n    \"class\":\"nb-gea\"\n  }\n]\n',NULL,NULL,NULL,NULL,'可以管理系统的模块1',NULL),(2,'查询用户','user',1,10,1,1,1,NULL,'SELECT\n	*, (\n		SELECT\n			group_concat(b.`NAME`)\n		FROM\n			fa_role b,\n			fa_user_role c\n		WHERE\n			b.ID = c.ROLE_ID\n		AND c.USER_ID = a.ID\n	) roleIdList\nFROM\n	fa_user a','{\n	\"ID\": {\n		\"title\": \"用户ID\",\n		\"type\": \"number\",\n		\"editable\": false\n	},\n	\"NAME\": {\n		\"title\": \"姓名\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"LOGIN_NAME\": {\n		\"title\": \"登录名\",\n		\"type\": \"string\",\n		\"editable\": false\n	},\n	\"roleIdList\": {\n		\"title\": \"角色\",\n		\"type\": \"string\",\n		\"editable\": true,\n		\"editor\": {\n			\"type\": \"listAsyn\",\n			\"config\": {\n                                \"api\": \"query/GeListData\",\n                                \"postEnt\": {\"code\":\"role\"},\n				\"hasAllCheckBox\": false,\n				\"maxHeight\": 100\n			}\n		}\n	},\n	\"LOGIN_COUNT\": {\n		\"title\": \"登录次数\",\n		\"type\": \"string\",\n		\"editable\": false\n	},\n	\"LAST_ACTIVE_TIME\": {\n		\"title\": \"最后活动时间\",\n		\"type\": \"string\",\n		\"editable\": false\n	},\n	\"IS_LOCKED\": {\n		\"title\": \"状态\",\n		\"type\": \"string\",\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"1\",\n						\"title\": \"是\"\n					},\n					{\n						\"value\": \"0\",\n						\"title\": \"否\"\n					}\n				]\n			}\n		}\n	}\n}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"apiUrl\": \"User/Save\",\n		\"readUrl\": \"User/Single\"\n	},\n	{\n		\"title\": \"删除\",\n		\"class\": \"nb-trash\",\n		\"apiUrl\": \"UserInfo/Delete\",\n		\"confirmTip\": \"确定要删除该用户吗？\"\n	}\n]','[\n{\n\"title\":\"添加\",\n\"class\":\"nb-plus\",\n\"click\":\"nowThis.Add(\'User/Save\')\"\n},\n{\n\"title\":\"导出\",\n\"class\":\"ion-archive\",\n\"click\":\"nowThis.onExportXls()\"\n}\n]',NULL,NULL,NULL,NULL,'用户的基本情况管理',NULL),(3,'脚本管理','script',1,10,0,0,1,NULL,'SELECT \n   A.ID,\n   A.CODE,\n   A.NAME,\n   A.BODY_TEXT,\n   A.BODY_HASH,\n   A.RUN_WHEN,\n   A.RUN_ARGS,\n   A.RUN_DATA,\n   A.STATUS,\n   A.DISABLE_REASON,\n   A.SERVICE_FLAG,\n   A.REGION,\n   A.IS_GROUP\nFROM  fa_script A','{\n	\"ID\": {\n\n		\"title\": \"ID\",\n		\"type\": \"number\",\n		\"editable\": true\n	},\n	\"CODE\": {\n\n		\"title\": \"代码\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"NAME\": {\n\n		\"title\": \"名称\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"BODY_TEXT\": {\n\n		\"title\": \"任务脚本\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"BODY_HASH\": {\n\n		\"title\": \"脚本哈希值\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"RUN_WHEN\": {\n\n		\"title\": \"时间表达式\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"RUN_ARGS\": {\n\n		\"title\": \"脚本参数\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"RUN_DATA\": {\n\n		\"title\": \"运行时间\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"STATUS\": {\n\n		\"title\": \"状态\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"DISABLE_REASON\": {\n\n		\"title\": \"禁用原因\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"SERVICE_FLAG\": {\n\n		\"title\": \"服务标识\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"REGION\": {\n\n		\"title\": \"REGION\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"IS_GROUP\": {\n\n		\"title\": \"是否是组\",\n		\"type\": \"number\",\n		\"editable\": true\n	}\n}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"apiUrl\": \"Script/Save\",\n		\"readUrl\": \"Script/Single\"\n	},\n	{\n		\"title\": \"删除\",\n		\"class\": \"nb-trash\",\n		\"apiUrl\": \"Script/Delete\",\n		\"confirmTip\": \"确定要删除该用户吗？\"\n	}\n]','[\n{\n\"title\":\"添加\",\n\"class\":\"nb-plus\",\n\"click\":\"nowThis.Add(\'Script/Save\')\"\n},\n{\n\"title\":\"导出\",\n\"class\":\"ion-archive\",\n\"click\":\"nowThis.onExportXls()\"\n}\n]',NULL,NULL,NULL,NULL,'脚本管理',NULL),(4,'自定义表','tableType',1,10,1,1,1,NULL,'select * from fa_table_type','{\n	\"ID\": {\n		\"title\": \"用户ID\",\n		\"type\": \"number\",\n		\"editable\": false\n	},\n	\"NAME\": {\n		\"title\": \"表名\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"TABLE_NAME\": {\n		\"title\": \"数据库中表名\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"INTRODUCE\": {\n		\"title\": \"介绍\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"ADD_TIME\": {\n		\"title\": \"添加时间\",\n		\"type\": \"string\",\n		\"editable\": false\n	},\n	\"STAUTS\": {\n		\"title\": \"状态\",\n		\"type\": \"string\",\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"正常\",\n						\"title\": \"正常\"\n					},\n					{\n						\"value\": \"禁用\",\n						\"title\": \"禁用\"\n					}\n				]\n			}\n		}\n	}\n}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"openModal\": \"TableEditComponent\",\n		\"readUrl\": \"Table/Single\",\n		\"apiUrl\": \"Table/Save\"\n	},\n	{\n		\"title\": \"删除\",\n		\"class\": \"nb-trash\",\n		\"apiUrl\": \"Table/Delete\",\n		\"confirmTip\": \"确定要删除吗？\"\n	}\n]','[{\n		\"title\": \"添加\",\n		\"class\": \"nb-plus\",\n		\"click\": \"nowThis.Add(\'Table/Save\',\'TableEditComponent\')\"\n	},\n	{\n		\"title\": \"批量删除\",\n		\"class\": \"ion-delete\",\n		\"click\": \"nowThis.Exec(\'Table/Delete\',\'ID\',\'删除要删除吗？\')\"\n	},\n	{\n		\"title\": \"导出\",\n		\"class\": \"ion-archive\",\n		\"click\": \"nowThis.onExportXls()\"\n	}\n]',NULL,NULL,NULL,NULL,'自定义表',NULL),(5,'模块管理','module',1,12,1,0,1,NULL,'select * from fa_module','{\n	\"ID\": {\n		\"title\": \"模块ID\",\n		\"defaultValue\": 0,\n		\"type\": \"number\",\n		\"editable\": false\n	},\n	\"PARENT_ID\": {\n		\"title\": \"父节点\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"CODE\": {\n		\"title\": \"模块代码\",\n		\"type\": \"string\"\n	},\n	\"NAME\": {\n		\"title\": \"模块名\",\n		\"type\": \"string\"\n	},\n	\"IMAGE_URL\": {\n		\"title\": \"模块图标\",\n		\"type\": \"string\"\n	},\n	\"LOCATION\": {\n		\"title\": \"连接地址\",\n		\"type\": \"string\"\n	},\n	\"IS_HIDE\": {\n		\"title\": \"是否隐藏\",\n		\"defaultValue\": 0,\n		\"type\": \"number\",\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"1\",\n						\"title\": \"是\"\n					},\n					{\n						\"value\": \"0\",\n						\"title\": \"否\"\n					}\n				]\n			}\n		}\n	},\n	\"IS_DEBUG\": {\n		\"title\": \"是否调试\",\n		\"defaultValue\": 0,\n		\"type\": \"number\"\n	},\n	\"SHOW_ORDER\": {\n		\"title\": \"排序\",\n		\"defaultValue\": 0,\n		\"type\": \"number\"\n	},\n	\"W\": {\n		\"title\": \"宽\",\n		\"editable\": false,\n		\"type\": \"number\"\n	},\n	\"H\": {\n		\"title\": \"高\",\n		\"editable\": false,\n		\"type\": \"number\"\n	},\n	\"DESCRIPTION\": {\n		\"title\": \"备注\",\n		\"type\": \"string\"\n	}\n}',NULL,NULL,'[\n{\n\"title\":\"修改\",\n\"class\":\"nb-edit\",\n\"apiUrl\":\"module/save\"\n},\n{\n\"title\":\"删除\",\n\"class\":\"nb-trash\",\n\"apiUrl\":\"module/delete\",\n\"confirmTip\":\"确定要删除吗？\"\n}\n]','[{\n		\"title\": \"添加\",\n		\"class\": \"nb-plus\",\n		\"click\": \"nowThis.Add(\'module/save\')\"\n	},\n	{\n		\"title\": \"批量删除\",\n		\"class\": \"ion-delete\",\n		\"click\": \"nowThis.Exec(\'module/delete\',\'ID\',\'删除要删除吗？\')\"\n	},\n	{\n		\"title\": \"导出\",\n		\"class\": \"ion-archive\",\n		\"click\": \"nowThis.onExportXls()\"\n	}\n]',NULL,NULL,NULL,NULL,'模块管理',NULL),(6,'角色管理','role',1,10,1,1,1,NULL,'select * from fa_role','{\n  \"ID\": {\n	\"title\": \"角色ID\",\n	\"defaultValue\": 0,\n	\"type\": \"number\",\n	\"editable\": false\n  },\n  \"NAME\": {\n	\"title\": \"角色名\",\n	\"type\": \"string\"\n  },\n  \"REMARK\": {\n	\"title\": \"备注\",\n	\"type\": \"string\"\n  },\n  \"moduleIdStr\": {\n	\"title\": \"所有模块\",\n	\"inputWidth\":\"12\",\n	\"type\": \"treeview\",\n	\"editor\":{\"type\": \"treeview\"}\n  }\n}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"openModal\": \"RoleEditComponent\",\n		\"readUrl\": \"role/single\",\n		\"apiUrl\": \"role/save\"\n	},\n	{\n		\"title\": \"删除\",\n		\"class\": \"nb-trash\",\n		\"apiUrl\": \"role/delete\",\n		\"confirmTip\": \"确定要删除吗？\"\n	}\n]','[{\n		\"title\": \"添加\",\n		\"class\": \"nb-plus\",\n		\"click\": \"nowThis.Add(\'role/save\',\'RoleEditComponent\',{Key:0},\'role/single\')\"\n	},\n	{\n		\"title\": \"批量删除\",\n		\"class\": \"ion-delete\",\n		\"click\": \"nowThis.Exec(\'role/delete\',\'ID\',\'删除要删除吗？\')\"\n	},\n	{\n		\"title\": \"导出\",\n		\"class\": \"nb-archive\",\n		\"click\": \"nowThis.onExportXls()\"\n	}\n]',NULL,NULL,NULL,NULL,'管理角色',NULL),(7,'组织结构','district',1,10,1,1,1,NULL,'SELECT * FROM fa_district','{\n	\"ID\": {\n		\"title\": \"ID\",\n		\"defaultValue\": 0,\n		\"type\": \"number\",\n		\"editable\": false\n	},\n	\"PARENT_ID\": {\n		\"title\": \"节点\",\n		\"type\": \"string\",\n		\"editable\": true,\n		\"editor\": {\n			\"type\": \"dropdown-treeview-select\",\n			\"config\": {\n				\"hasAllCheckBox\": false,\n				\"maxHeight\": 100\n			},\n			\"dataFig\": {\n				\"api\": \"query/query\",\n				\"config\": {\n					\"Key\": \"district\",\n					\"PageIndex\": 1,\n					\"PageSize\": 10\n				}\n			}\n		}\n	},\n	\"CODE\": {\n		\"title\": \"代码\",\n		\"type\": \"string\"\n	},\n	\"NAME\": {\n		\"title\": \"名称\",\n		\"type\": \"string\"\n	},\n	\"IN_USE\": {\n		\"title\": \"是否隐藏\",\n		\"defaultValue\": 0,\n		\"type\": \"custom\",\n		\"renderComponent\": \"SmartTableFormatValuePage\",\n		\"onComponentInitFunction\": function(instance) {\n			instance.format = (x) => {\n				return x == 0 ? \"否\" : \"是\"\n			}\n		},\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"1\",\n						\"title\": \"是\"\n					},\n					{\n						\"value\": \"0\",\n						\"title\": \"否\"\n					}\n				]\n			}\n		}\n\n	},\n	\"ID_PATH\": {\n		\"title\": \"代码\",\n		\"type\": \"string\",\n		\"editable\": false\n	}\n}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"readUrl\": \"district/single\",\n		\"apiUrl\": \"district/save\"\n	},\n	{\n		\"title\": \"删除\",\n		\"class\": \"nb-trash\",\n		\"apiUrl\": \"district/delete\",\n		\"confirmTip\": \"确定要删除吗？\"\n	}\n]\n','[{\n		\"title\": \"添加\",\n		\"class\": \"nb-plus\",\n		\"click\": \"nowThis.Add(\'district/save\')\"\n	},\n	{\n		\"title\": \"批量删除\",\n		\"class\": \"ion-delete\",\n		\"click\": \"nowThis.Exec(\'district/delete\',\'ID\',\'删除要删除吗？\')\"\n	},\n	{\n		\"title\": \"导出\",\n		\"class\": \"nb-archive\",\n		\"click\": \"nowThis.onExportXls()\"\n	}\n]\n',NULL,NULL,NULL,NULL,'组织结构',NULL),(8,'设备类型管理','equipmentType',1,10,1,0,0,NULL,'\nSELECT \n   A.ID,\n   A.NAME,\n   A.PARENT_ID,\n   A.TABLE_TYPE_ID,\n   A.INTRODUCE,\n   A.STAUTS\nFROM fa_equipment A\n','{\n	\"ID\": {\n		\"title\": \"用户ID\",\n		\"type\": \"number\",\n		\"editable\": false\n	},\n	\"NAME\": {\n		\"title\": \"表名\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"PARENT_ID\": {\n		\"title\": \"上级ID\",\n		\"type\": \"number\"\n	},\n	\"TABLE_TYPE_ID\": {\n		\"title\": \"表类型ID\",\n		\"type\": \"number\"\n	},\n	\"INTRODUCE\": {\n		\"title\": \"介绍\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"STAUTS\": {\n		\"title\": \"状态\",\n		\"type\": \"string\",\n		\"editor\": {\n			\"type\": \"list\",\n			\"config\": {\n				\"list\": [{\n						\"value\": \"正常\",\n						\"title\": \"正常\"\n					},\n					{\n						\"value\": \"禁用\",\n						\"title\": \"禁用\"\n					}\n				]\n			}\n		}\n	}\n}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"readUrl\": \"Equipment/Single\",\n		\"apiUrl\": \"Equipment/Save\"\n	},\n	{\n		\"title\": \"删除\",\n		\"class\": \"nb-trash\",\n		\"apiUrl\": \"Equipment/Delete\",\n		\"confirmTip\": \"确定要删除吗？\"\n	}\n]','\n[{\n		\"title\": \"添加\",\n		\"class\": \"nb-plus\",\n		\"click\": \"nowThis.Add(\'Equipment/Save\')\"\n	},\n	{\n		\"title\": \"导出\",\n		\"class\": \"ion-archive\",\n		\"click\": \"nowThis.onExportXls()\"\n	}\n]',NULL,NULL,NULL,NULL,NULL,NULL),(9,'用户管理','faUserInfo',1,20,1,0,0,NULL,'SELECT father.NAME                 FatherName,\n       elder.NAME                  ElderName,\n       elder.SORT                  ElderSort,\n       Couple.NAME                 CoupleName,\n       (SELECT Count(1)\n        FROM   fa_user_info x\n        WHERE  x.FATHER_ID = a.ID) ChildNum,\n       a.ID,\n       a.LEVEL_ID,\n       a.ELDER_ID,\n       a.FATHER_ID,\n       a.COUPLE_ID,\n       a.BIRTHDAY_TIME,\n       a.BIRTHDAY_PLACE,\n       a.IS_LIVE,\n       a.DIED_TIME,\n       a.DIED_PLACE,\n       a.YEARS_TYPE,\n       a.SEX,\n       a.STATUS,\n       a.ALIAS,\n       a.AUTHORITY,\n       a.REMARK,\n       a.EDUCATION,\n       a.INDUSTRY,\n       a.BIRTHDAY_CHINA_YEAR,\n       a.DIED_CHINA_YEAR,\n       a.CREATE_USER_ID,\n       b.NAME,\n       b.LOGIN_NAME,\n       b.ICON_FILES_ID,\n       b.DISTRICT_ID,\n       b.IS_LOCKED,\n       b.CREATE_TIME\nFROM   fa_user_info a\n       LEFT JOIN fa_user b\n              ON a.ID = b.ID\n       LEFT JOIN fa_user father\n              ON father.ID = a.FATHER_ID\n       LEFT JOIN fa_user Couple\n              ON Couple.ID = a.COUPLE_ID\n       LEFT JOIN fa_elder elder\n              ON elder.ID = a.ELDER_ID','{\n	\"ID\": {\n		\"title\": \"ID\",\n		\"type\": \"numberbox\",\n		\"editable\": false\n	},\n	\"NAME\": {\n		\"title\": \"姓名\",\n		\"editable\": false\n	},\n	\"LOGIN_NAME\": {\n		\"title\": \"登录账号\",\n		\"editable\": true\n	},\n	\"IS_LOCKED\": {\n		\"title\": \"锁定\",\n		\"editable\": true\n	},\n\n	\"LEVEL_ID\": {\n		\"title\": \"排行\",\n		\"type\": \"numberbox\",\n		\"editable\": false\n	},\n	\"FAMILY_ID\": {\n		\"title\": \"FAMILY_ID\",\n		\"type\": \"numberbox\",\n		\"editable\": false\n	},\n	\"FatherName\": {\n		\"title\": \"父亲\",\n		\"editable\": false\n	},\n	\"ElderName\": {\n		\"title\": \"辈字\",\n		\"editable\": false\n	},\n	\"CoupleName\": {\n		\"title\": \"配偶\",\n		\"editable\": false\n	},\n	\"ChildNum\": {\n		\"title\": \"子女数\",\n		\"editable\": false\n	},\n	\"ELDER_ID\": {\n		\"title\": \"ELDER_ID\",\n		\"type\": \"numberbox\",\n		\"editable\": false\n	},\n	\"FATHER_ID\": {\n		\"title\": \"父亲ID\",\n		\"type\": \"numberbox\",\n		\"editable\": true\n	},\n	\"COUPLE_ID\": {\n		\"title\": \"配偶ID\",\n		\"type\": \"numberbox\",\n		\"editable\": true\n	},\n	\"BIRTHDAY_TIME\": {\n		\"title\": \"出生日期\",\n		\"type\": \"datetimebox\",\n		\"editable\": true\n	},\n	\"BIRTHDAY_PLACE\": {\n		\"title\": \"出生地点\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"IS_LIVE\": {\n		\"title\": \"是否健在\",\n		\"type\": \"numberbox\",\n		\"editable\": true\n	},\n	\"DIED_TIME\": {\n		\"title\": \"过逝日期\",\n		\"type\": \"datetimebox\",\n		\"editable\": true\n	},\n	\"DIED_PLACE\": {\n		\"title\": \"过逝地点\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"SEX\": {\n		\"title\": \"性别\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"YEARS_TYPE\": {\n		\"title\": \"日期类型\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"STATUS\": {\n		\"title\": \"状态\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"CREATE_USER_NAME\": {\n		\"title\": \"创建用户的姓名\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"CREATE_USER_ID\": {\n		\"title\": \"创建用户ID\",\n		\"type\": \"numberbox\",\n		\"editable\": true\n	},\n	\"UPDATE_TIME\": {\n		\"title\": \"修改时间\",\n		\"type\": \"datetimebox\",\n		\"editable\": true\n	},\n	\"UPDATE_USER_NAME\": {\n		\"title\": \"修改用户的姓名\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"UPDATE_USER_ID\": {\n		\"title\": \"修改用户的ID\",\n		\"type\": \"numberbox\",\n		\"editable\": true\n	},\n	\"ALIAS\": {\n		\"title\": \"外号\",\n		\"type\": \"string\",\n		\"editable\": true\n	},\n	\"AUTHORITY\": {\n		\"title\": \"权限\",\n		\"type\": \"numberbox\",\n		\"editable\": true\n	}}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"readUrl\": \"UserInfo/Single\",\n		\"apiUrl\": \"UserInfo/Save\"\n	}\n]','[]',NULL,NULL,NULL,NULL,'用户管理',NULL),(10,'家谱','faFamilyBooks',1,20,1,1,1,NULL,'\r\nSELECT \r\n   A.ID,\r\n   A.FAMILY_ID,\r\n   A.NAME,\r\n   A.TYPE_ID,\r\n   A.SORT,\r\n   A.UserID,\r\n   A.FileID\r\nFROM FA_FAMILY_BOOKS A\r\n','{\r\n	\"ID\": {\r\n		\"title\": \"ID\",\r\n		\"type\": \"numberbox\",\r\n		\"editable\": true\r\n	},\r\n	\"FAMILY_ID\": {\r\n		\"title\": \"FAMILY_ID\",\r\n		\"type\": \"numberbox\",\r\n		\"editable\": true\r\n	},\r\n	\"NAME\": {\r\n		\"title\": \"NAME\",\r\n		\"type\": \"string\",\r\n		\"editable\": true\r\n	},\r\n	\"TYPE_ID\": {\r\n		\"title\": \"类型\",\r\n		\"type\": \"numberbox\",\r\n		\"editable\": true\r\n	},\r\n	\"SORT\": {\r\n		\"title\": \"排序\",\r\n		\"type\": \"numberbox\",\r\n		\"editable\": true\r\n	},\r\n	\"UserID\": {\r\n		\"title\": \"用户ID\",\r\n		\"type\": \"numberbox\",\r\n		\"editable\": true\r\n	},\r\n	\"FileID\": {\r\n		\"title\": \"文件ID\",\r\n		\"type\": \"numberbox\",\r\n		\"editable\": true\r\n	}}',NULL,NULL,'[{\n		\"title\": \"修改\",\n		\"class\": \"nb-edit\",\n		\"readUrl\": \"FamilyBooks/Single\",\n		\"apiUrl\": \"FamilyBooks/Save\"\n	}\n]','\n[{\n		\"title\": \"添加\",\n		\"class\": \"nb-plus\",\n		\"click\": \"nowThis.Add(\'FamilyBooks/Save\')\"\n	},\n]',NULL,NULL,NULL,NULL,'家谱配置',NULL);
/*!40000 ALTER TABLE `fa_query` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_role`
--

DROP TABLE IF EXISTS `fa_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_role` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(80) DEFAULT NULL,
  `REMARK` varchar(255) DEFAULT NULL,
  `TYPE` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_role`
--

LOCK TABLES `fa_role` WRITE;
/*!40000 ALTER TABLE `fa_role` DISABLE KEYS */;
INSERT INTO `fa_role` VALUES (1,'系统管理','',NULL),(2,'管理员',NULL,NULL),(3,'一般用户',NULL,NULL),(4,'asdf1111','ga222',NULL),(5,'11d','ddd',NULL),(23,'财务人员',NULL,NULL);
/*!40000 ALTER TABLE `fa_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_role_config`
--

DROP TABLE IF EXISTS `fa_role_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_role_config` (
  `ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  `TYPE` varchar(10) DEFAULT NULL,
  `NAME` varchar(50) NOT NULL,
  `VALUE` varchar(300) DEFAULT NULL,
  `REMARK` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_ROLE_CONFIG_REF_ROLE` (`ROLE_ID`),
  CONSTRAINT `fa_role_config_ibfk_1` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_role_config`
--

LOCK TABLES `fa_role_config` WRITE;
/*!40000 ALTER TABLE `fa_role_config` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_role_config` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_role_function`
--

DROP TABLE IF EXISTS `fa_role_function`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_role_function` (
  `FUNCTION_ID` int(11) NOT NULL,
  `ROLE_ID` int(11) NOT NULL,
  PRIMARY KEY (`FUNCTION_ID`,`ROLE_ID`),
  KEY `FK_FA_ROLE_FUNCTION_REF_ROLE` (`ROLE_ID`),
  CONSTRAINT `fa_role_function_ibfk_1` FOREIGN KEY (`FUNCTION_ID`) REFERENCES `fa_function` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_role_function_ibfk_2` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_role_function`
--

LOCK TABLES `fa_role_function` WRITE;
/*!40000 ALTER TABLE `fa_role_function` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_role_function` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_role_module`
--

DROP TABLE IF EXISTS `fa_role_module`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_role_module` (
  `ROLE_ID` int(11) NOT NULL,
  `MODULE_ID` int(11) NOT NULL,
  PRIMARY KEY (`ROLE_ID`,`MODULE_ID`),
  KEY `FK_FA_ROLE_MODULE_REF_MODULE` (`MODULE_ID`),
  CONSTRAINT `fa_role_module_ibfk_1` FOREIGN KEY (`MODULE_ID`) REFERENCES `fa_module` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_role_module_ibfk_2` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_role_module`
--

LOCK TABLES `fa_role_module` WRITE;
/*!40000 ALTER TABLE `fa_role_module` DISABLE KEYS */;
INSERT INTO `fa_role_module` VALUES (1,1),(1,2),(1,3),(1,4),(1,5),(1,6),(1,7),(1,8),(1,9),(1,10),(1,11),(1,12),(1,13),(1,14),(1,15),(2,1),(2,2),(2,3),(2,4),(2,8),(2,9),(4,1),(4,2),(4,4),(4,8),(4,9),(5,1),(5,2),(5,3),(5,5),(5,6),(5,8),(5,9);
/*!40000 ALTER TABLE `fa_role_module` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_role_query_authority`
--

DROP TABLE IF EXISTS `fa_role_query_authority`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_role_query_authority` (
  `ROLE_ID` int(11) NOT NULL,
  `QUERY_ID` int(11) NOT NULL,
  `NO_AUTHORITY` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ROLE_ID`,`QUERY_ID`),
  KEY `FK_FA_ROLE_QUERY_REF_QUERY` (`QUERY_ID`),
  CONSTRAINT `fa_role_query_authority_ibfk_1` FOREIGN KEY (`QUERY_ID`) REFERENCES `fa_query` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_role_query_authority_ibfk_2` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_role_query_authority`
--

LOCK TABLES `fa_role_query_authority` WRITE;
/*!40000 ALTER TABLE `fa_role_query_authority` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_role_query_authority` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_script`
--

DROP TABLE IF EXISTS `fa_script`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_script` (
  `ID` int(11) NOT NULL,
  `CODE` varchar(20) NOT NULL,
  `NAME` varchar(255) NOT NULL,
  `BODY_TEXT` text NOT NULL,
  `BODY_HASH` varchar(255) NOT NULL,
  `RUN_WHEN` varchar(30) DEFAULT NULL,
  `RUN_ARGS` varchar(255) DEFAULT NULL,
  `RUN_DATA` varchar(20) NOT NULL DEFAULT '0',
  `STATUS` varchar(10) DEFAULT NULL COMMENT '正常\n            禁用',
  `DISABLE_REASON` varchar(50) DEFAULT NULL,
  `SERVICE_FLAG` varchar(50) DEFAULT NULL,
  `REGION` varchar(10) DEFAULT NULL,
  `IS_GROUP` decimal(1,0) NOT NULL,
  `GROUP_ID` int(11) DEFAULT NULL,
  `ORDER_INDEX` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_script`
--

LOCK TABLES `fa_script` WRITE;
/*!40000 ALTER TABLE `fa_script` DISABLE KEYS */;
INSERT INTO `fa_script` VALUES (2,'string','string','select * from fa_user','112','0/20 * * * * ?','string','string','正常','string','string',NULL,0,20,10);
/*!40000 ALTER TABLE `fa_script` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_script_group_list`
--

DROP TABLE IF EXISTS `fa_script_group_list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_script_group_list` (
  `SCRIPT_ID` int(11) NOT NULL,
  `GROUP_ID` int(11) NOT NULL,
  `ORDER_INDEX` int(11) NOT NULL,
  PRIMARY KEY (`SCRIPT_ID`,`GROUP_ID`),
  KEY `FK_FA_GROUP_LIST_REF_SCRIPT` (`GROUP_ID`),
  CONSTRAINT `fa_script_group_list_ibfk_1` FOREIGN KEY (`GROUP_ID`) REFERENCES `fa_script` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_script_group_list`
--

LOCK TABLES `fa_script_group_list` WRITE;
/*!40000 ALTER TABLE `fa_script_group_list` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_script_group_list` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_script_task`
--

DROP TABLE IF EXISTS `fa_script_task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_script_task` (
  `ID` int(11) NOT NULL,
  `SCRIPT_ID` int(11) NOT NULL,
  `BODY_TEXT` text NOT NULL,
  `BODY_HASH` varchar(255) NOT NULL,
  `RUN_STATE` varchar(10) NOT NULL DEFAULT '0' COMMENT '0等待\n            1运行\n            2停止',
  `RUN_WHEN` varchar(30) DEFAULT NULL,
  `RUN_ARGS` varchar(255) DEFAULT NULL,
  `RUN_DATA` varchar(20) NOT NULL DEFAULT '0',
  `LOG_TYPE` decimal(1,0) DEFAULT '0',
  `DSL_TYPE` varchar(255) DEFAULT NULL,
  `RETURN_CODE` varchar(10) DEFAULT '0' COMMENT '成功\n            失败',
  `START_TIME` datetime DEFAULT NULL,
  `END_TIME` datetime DEFAULT NULL,
  `DISABLE_DATE` datetime DEFAULT NULL,
  `DISABLE_REASON` varchar(50) DEFAULT NULL,
  `SERVICE_FLAG` varchar(50) DEFAULT NULL,
  `REGION` varchar(10) DEFAULT NULL,
  `GROUP_ID` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_SCRIPT_TASK_REF_SCRIPT` (`SCRIPT_ID`),
  CONSTRAINT `fa_script_task_ibfk_1` FOREIGN KEY (`SCRIPT_ID`) REFERENCES `fa_script` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_script_task`
--

LOCK TABLES `fa_script_task` WRITE;
/*!40000 ALTER TABLE `fa_script_task` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_script_task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_script_task_log`
--

DROP TABLE IF EXISTS `fa_script_task_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_script_task_log` (
  `ID` int(11) NOT NULL,
  `SCRIPT_TASK_ID` int(11) NOT NULL,
  `LOG_TIME` datetime NOT NULL,
  `LOG_TYPE` decimal(1,0) NOT NULL DEFAULT '1' COMMENT '0正常日志\n            1错误日志',
  `MESSAGE` text,
  `SQL_TEXT` text,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_SCRIPT_TASK_LOG_REF_TASK` (`SCRIPT_TASK_ID`),
  CONSTRAINT `fa_script_task_log_ibfk_1` FOREIGN KEY (`SCRIPT_TASK_ID`) REFERENCES `fa_script_task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_script_task_log`
--

LOCK TABLES `fa_script_task_log` WRITE;
/*!40000 ALTER TABLE `fa_script_task_log` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_script_task_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_sms_send`
--

DROP TABLE IF EXISTS `fa_sms_send`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_sms_send` (
  `GUID` char(32) NOT NULL,
  `MESSAGE_ID` int(11) DEFAULT NULL,
  `PHONE_NO` varchar(50) NOT NULL,
  `ADD_TIME` datetime DEFAULT NULL,
  `SEND_TIME` datetime DEFAULT NULL,
  `CONTENT` varchar(500) NOT NULL,
  `STAUTS` varchar(15) DEFAULT NULL,
  `TRY_NUM` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`GUID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_sms_send`
--

LOCK TABLES `fa_sms_send` WRITE;
/*!40000 ALTER TABLE `fa_sms_send` DISABLE KEYS */;
INSERT INTO `fa_sms_send` VALUES ('0d9c282711f84aba834267e3be09c6a9',NULL,'18180770313','2019-05-23 10:11:36',NULL,'1111','成功',0),('0e3e2c87e3e24448b561a5445fd27016',NULL,'13653087854','2019-06-12 12:45:22',NULL,'7838','成功',0),('11021fad5fcf4593b1e5e1fe4082d835',NULL,'18980698099','2019-06-12 23:25:42',NULL,'6292','成功',0),('1362fbd434a14a3daf352c29a928e25a',NULL,'15102878560','2019-06-14 15:10:56',NULL,'9526','成功',0),('18dd80bd4b9d4044a7a2fc98a57983fe',NULL,'18180770310','2019-05-25 00:04:16',NULL,'1111','成功',0),('1c84335476104c5f84c827404482aef7',NULL,'18611992250','2019-06-12 19:57:31',NULL,'5625','成功',0),('2464f735581d41fa88bb5d1302950a85',NULL,'18180770313','2019-05-25 00:10:15',NULL,'1111','成功',0),('37defd26abeb42d4bc8dc61364ae9ed3',NULL,'18180770313','2017-05-11 14:28:17',NULL,'0530','成功',0),('3a77d3c66f604372a08c9817864cabb4',NULL,'18180773011','2019-05-25 23:08:16',NULL,'1111','成功',0),('3d64c5c3c8d94ea486000cdd059688dd',NULL,'15528858269','2019-06-10 11:34:50',NULL,'4199','成功',0),('40a255e6029346db8d1a4b079116ed6d',NULL,'18611992250','2019-06-12 18:39:58',NULL,'4142','成功',0),('434f9cead88f4a9da451979a1037af67',NULL,'13308015598','2019-06-15 11:24:02',NULL,'1490','成功',0),('48a225d551204bf1b8885729c28a2343',NULL,'18180770312','2019-05-25 23:53:06',NULL,'1111','成功',0),('531a9a60889b422c99c93d2f64c9b82d',NULL,'15983756119','2019-06-10 13:47:29',NULL,'7801','成功',0),('53a25da75d3a4361837fc94bd4a2fbe8',NULL,'15821125138','2019-05-30 11:01:10',NULL,'8645','成功',0),('55044acf53c34bb28e6959d62bebe05a',NULL,'15583008111','2019-06-11 15:04:17',NULL,'8280','成功',0),('57effc86d12f4530b0654d38c65ab4c4',NULL,'17323097208','2019-05-30 08:03:34',NULL,'2762','成功',0),('5e17e98d35e845b6a0c87fa3e550b9a4',NULL,'18180770310','2019-05-25 15:08:06',NULL,'1111','成功',0),('5f48c5f647e74527a58f9bd21ace996f',NULL,'18318411630','2019-06-12 12:50:25',NULL,'3787','成功',0),('6153b98908464ca88ee64bb100d446f8',NULL,'13308015598','2019-06-23 15:29:29',NULL,'0134','成功',0),('64de74e3ab07405083538bae9d57f04d',NULL,'13059328175','2019-06-11 12:23:17',NULL,'7695','成功',0),('684fcd10ff174677acf7cf8963cbd383',NULL,'15528858269','2019-06-10 11:40:10',NULL,'5274','成功',0),('6d34a330d6704c5f9ae34b57b07223d3',NULL,'15982623955','2019-06-10 11:38:13',NULL,'6753','成功',0),('6f5faf7773ca4a5f873141445dce78cb',NULL,'18180770310','2019-05-25 01:01:53',NULL,'1111','成功',0),('78b3f26b057044349dcc9bd40c63707f',NULL,'18346760350','2019-06-12 07:09:53',NULL,'4650','成功',0),('7b6aec5341e54b85916a28355fdc7cc3',NULL,'18180770310','2019-05-25 21:45:57',NULL,'1111','成功',0),('7e5033e63be344e5b3133fb13760aa9b',NULL,'18180770313','2019-05-23 00:08:42',NULL,'1111','成功',0),('82bd2778accc46129fd6f5f65baa920b',NULL,'18180770313','2019-05-23 09:28:02',NULL,'1111','成功',0),('871eb0a2d3b946ec8030eb5c6a6dbb68',NULL,'15528858269','2019-06-10 21:59:23',NULL,'9023','成功',0),('89f9e4a7adcc4e5791b8baf17840c2d7',NULL,'15528858269','2019-06-10 11:29:19',NULL,'1041','成功',0),('8fe05d57f77240c39d4bf078fb3e6f99',NULL,'18980698099','2019-06-12 23:19:52',NULL,'3209','成功',0),('906518684f9f424c8d631c26550af723',NULL,'17323097208','2019-05-30 08:00:24',NULL,'0255','成功',0),('9143338ef80d4844a91b1129abef366b',NULL,'18180770313','2019-05-25 23:26:36',NULL,'1111','成功',0),('946b0d91361a4bed81c201121b9d56d8',NULL,'18227378954','2019-06-10 11:15:51',NULL,'0689','成功',0),('94acf99eb9bd49b4b92434eeaecd4622',NULL,'18180770313','2019-05-23 10:08:21',NULL,'1111','成功',0),('95d6cb221bbc45a29a52a02112c6e4de',NULL,'18180770313','2019-05-23 10:09:44',NULL,'1111','成功',0),('a1c09114c6b049c2be5843d737dfc528',NULL,'15528858269','2019-06-10 11:42:08',NULL,'2757','成功',0),('a567961078694494916fe515df0364ff',NULL,'18180770312','2019-05-27 11:43:19',NULL,'1111','成功',0),('a6e4624345244bc790cf214ea68ab65b',NULL,'15881791382','2019-06-16 16:24:12',NULL,'0718','成功',0),('a7bb12983f9f437ea3a30ef0e964aea7',NULL,'15882602397','2019-06-11 08:31:08',NULL,'9143','成功',0),('aa8f1b5c6b6f4552bf55a0e41c739d47',NULL,'13653087854','2019-06-12 12:42:27',NULL,'0801','成功',0),('c6070b5771404379abc52922170f0585',NULL,'18180770313','2019-05-24 11:33:06',NULL,'1111','成功',0),('c6e6d85e99b34796b4e1838bb63a1a63',NULL,'15181569851','2017-05-11 14:41:30',NULL,'0714','成功',0),('d1b8e39f5e1240cab938b340998d4a55',NULL,'18180770313','2019-05-29 00:05:29',NULL,'9725','成功',0),('e09a03f229b94b6ca01ceea4d621edf1',NULL,'18227378954','2019-06-12 23:07:49',NULL,'9364','成功',0),('e2345ed327ad41e59dd658644a37f1d3',NULL,'15528858269','2019-06-10 11:30:47',NULL,'1030','成功',0),('e401539dd47b4ed8b8ffb1eceddbc01c',NULL,'18180770310','2019-05-25 00:12:27',NULL,'1111','成功',0),('e718c5fab98c421f8abeb67f3b469257',NULL,'13653087854','2019-06-12 12:40:28',NULL,'9157','成功',0),('e9030016141349f7877880014d125463',NULL,'15528858269','2019-06-10 21:54:35',NULL,'0410','成功',0),('ec3e51ce1cf94567a80ec3f72400a415',NULL,'15882602397','2019-06-11 08:34:24',NULL,'1173','成功',0),('f21561cdce1b4ecdabe5dec0d7299921',NULL,'13408565627','2019-06-10 13:09:44',NULL,'3482','成功',0),('f3ff35f7bbad4d2195de05901356d896',NULL,'18180770313','2019-05-23 10:24:36',NULL,'1111','成功',0),('feb369c1a5ad42eaafe82fde2757f722',NULL,'18180770313','2019-05-25 21:32:46',NULL,'1111','成功',0);
/*!40000 ALTER TABLE `fa_sms_send` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_table_column`
--

DROP TABLE IF EXISTS `fa_table_column`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_table_column` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `TABLE_TYPE_ID` int(11) NOT NULL,
  `NAME` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '表名',
  `COLUMN_NAME` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '数据库中列名',
  `INTRODUCE` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '介绍',
  `STAUTS` varchar(15) CHARACTER SET utf8 NOT NULL COMMENT '状态',
  `ORDER_INDEX` int(11) NOT NULL,
  `COLUMN_TYPE` varchar(15) CHARACTER SET utf8 NOT NULL COMMENT '列类型',
  `COLUMN_LONG` int(11) DEFAULT NULL,
  `IS_REQUIRED` int(11) DEFAULT NULL,
  `DEFAULT_VALUE` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `COLUMN_TYPE_CFG` varchar(15) CHARACTER SET utf8 DEFAULT NULL,
  `AUTHORITY` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_table_column`
--

LOCK TABLES `fa_table_column` WRITE;
/*!40000 ALTER TABLE `fa_table_column` DISABLE KEYS */;
INSERT INTO `fa_table_column` VALUES (8,8,'分数','sore','1','启用',1,'int',4,4,'1','1',1),(9,8,'姓名','name','1','启用',1,'text',11,1,'1','1',1),(10,9,'说明','remark','1','启用',1,'text',20,1,'1','1',1),(11,9,'名称','rolename','2','启用',1,'text',10,2,'2','2',2);
/*!40000 ALTER TABLE `fa_table_column` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_table_type`
--

DROP TABLE IF EXISTS `fa_table_type`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_table_type` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '表名',
  `TABLE_NAME` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '数据库中表名',
  `INTRODUCE` varchar(50) CHARACTER SET utf8 NOT NULL COMMENT '介绍',
  `ADD_TIME` datetime NOT NULL COMMENT '添加时间',
  `STAUTS` varchar(15) CHARACTER SET utf8 NOT NULL COMMENT '状态',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_table_type`
--

LOCK TABLES `fa_table_type` WRITE;
/*!40000 ALTER TABLE `fa_table_type` DISABLE KEYS */;
INSERT INTO `fa_table_type` VALUES (8,'测试用户','test_user','测试用户','2019-04-03 15:26:00','正常'),(9,'测试资料','test_role','测试资料','2019-04-03 15:33:59','正常');
/*!40000 ALTER TABLE `fa_table_type` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_task`
--

DROP TABLE IF EXISTS `fa_task`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_task` (
  `ID` int(11) NOT NULL,
  `FLOW_ID` int(11) DEFAULT NULL,
  `TASK_NAME` varchar(50) DEFAULT NULL,
  `CREATE_TIME` datetime DEFAULT NULL,
  `CREATE_USER` int(11) DEFAULT NULL,
  `CREATE_USER_NAME` varchar(50) DEFAULT NULL,
  `STATUS` varchar(50) DEFAULT NULL,
  `STATUS_TIME` datetime DEFAULT NULL,
  `REMARK` text,
  `REGION` varchar(10) DEFAULT NULL,
  `KEY_ID` varchar(32) DEFAULT NULL,
  `START_TIME` datetime DEFAULT NULL,
  `END_TIME` datetime DEFAULT NULL,
  `DEAL_TIME` datetime DEFAULT NULL,
  `ROLE_ID_STR` varchar(200) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_FLOW_TASK_REF_FLOW` (`FLOW_ID`),
  CONSTRAINT `fa_task_ibfk_1` FOREIGN KEY (`FLOW_ID`) REFERENCES `fa_flow` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_task`
--

LOCK TABLES `fa_task` WRITE;
/*!40000 ALTER TABLE `fa_task` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_task` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_task_flow`
--

DROP TABLE IF EXISTS `fa_task_flow`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_task_flow` (
  `ID` int(11) NOT NULL,
  `PARENT_ID` int(11) DEFAULT NULL,
  `TASK_ID` int(11) NOT NULL,
  `LEVEL_ID` int(11) DEFAULT NULL,
  `FLOWNODE_ID` int(11) DEFAULT NULL,
  `EQUAL_ID` int(11) DEFAULT NULL,
  `IS_HANDLE` int(11) NOT NULL,
  `NAME` varchar(100) DEFAULT NULL,
  `HANDLE_URL` varchar(200) DEFAULT NULL,
  `SHOW_URL` varchar(200) DEFAULT NULL,
  `EXPIRE_TIME` datetime DEFAULT NULL,
  `START_TIME` datetime NOT NULL,
  `DEAL_STATUS` varchar(50) DEFAULT NULL COMMENT '阶段回复\n            回复',
  `ROLE_ID_STR` varchar(200) DEFAULT NULL,
  `HANDLE_USER_ID` int(11) DEFAULT NULL,
  `DEAL_TIME` datetime DEFAULT NULL,
  `ACCEPT_TIME` datetime DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_TASK_FLOW_REF_TASK` (`TASK_ID`),
  KEY `FK_FA_TASK_FLOW_REF_TASK_FLOW` (`PARENT_ID`),
  CONSTRAINT `fa_task_flow_ibfk_1` FOREIGN KEY (`TASK_ID`) REFERENCES `fa_task` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_task_flow_ibfk_2` FOREIGN KEY (`PARENT_ID`) REFERENCES `fa_task_flow` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_task_flow`
--

LOCK TABLES `fa_task_flow` WRITE;
/*!40000 ALTER TABLE `fa_task_flow` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_task_flow` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_task_flow_handle`
--

DROP TABLE IF EXISTS `fa_task_flow_handle`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_task_flow_handle` (
  `ID` int(11) NOT NULL,
  `TASK_FLOW_ID` int(11) NOT NULL,
  `DEAL_USER_ID` int(11) NOT NULL,
  `DEAL_USER_NAME` varchar(50) NOT NULL,
  `DEAL_TIME` datetime NOT NULL,
  `CONTENT` varchar(2000) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_TASK_FLOW_HANDLE_REF_FLOW` (`TASK_FLOW_ID`),
  CONSTRAINT `fa_task_flow_handle_ibfk_1` FOREIGN KEY (`TASK_FLOW_ID`) REFERENCES `fa_task_flow` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_task_flow_handle`
--

LOCK TABLES `fa_task_flow_handle` WRITE;
/*!40000 ALTER TABLE `fa_task_flow_handle` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_task_flow_handle` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_task_flow_handle_files`
--

DROP TABLE IF EXISTS `fa_task_flow_handle_files`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_task_flow_handle_files` (
  `FLOW_HANDLE_ID` int(11) NOT NULL,
  `FILES_ID` int(11) NOT NULL,
  PRIMARY KEY (`FLOW_HANDLE_ID`,`FILES_ID`),
  KEY `FK_FLOW_HANDLE_REF_FILES` (`FILES_ID`),
  CONSTRAINT `fa_task_flow_handle_files_ibfk_1` FOREIGN KEY (`FILES_ID`) REFERENCES `fa_files` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_task_flow_handle_files_ibfk_2` FOREIGN KEY (`FLOW_HANDLE_ID`) REFERENCES `fa_task_flow_handle` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_task_flow_handle_files`
--

LOCK TABLES `fa_task_flow_handle_files` WRITE;
/*!40000 ALTER TABLE `fa_task_flow_handle_files` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_task_flow_handle_files` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_task_flow_handle_user`
--

DROP TABLE IF EXISTS `fa_task_flow_handle_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_task_flow_handle_user` (
  `TASK_FLOW_ID` int(11) NOT NULL,
  `HANDLE_USER_ID` int(11) NOT NULL,
  PRIMARY KEY (`TASK_FLOW_ID`,`HANDLE_USER_ID`),
  CONSTRAINT `fa_task_flow_handle_user_ibfk_1` FOREIGN KEY (`TASK_FLOW_ID`) REFERENCES `fa_task_flow` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_task_flow_handle_user`
--

LOCK TABLES `fa_task_flow_handle_user` WRITE;
/*!40000 ALTER TABLE `fa_task_flow_handle_user` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_task_flow_handle_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_updata_log`
--

DROP TABLE IF EXISTS `fa_updata_log`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_updata_log` (
  `ID` int(11) NOT NULL,
  `CREATE_TIME` datetime DEFAULT NULL,
  `CREATE_USER_NAME` varchar(50) DEFAULT NULL,
  `CREATE_USER_ID` int(11) DEFAULT NULL,
  `OLD_CONTENT` text,
  `NEW_CONTENT` text,
  `TABLE_NAME` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_updata_log`
--

LOCK TABLES `fa_updata_log` WRITE;
/*!40000 ALTER TABLE `fa_updata_log` DISABLE KEYS */;
INSERT INTO `fa_updata_log` VALUES (1,'2017-05-31 23:48:49','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":89,\"BIRTHDAY_TIME\":\"2017-05-30 07:15:44\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-05-30 07:15:44\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-30 15:15:54\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-30 15:15:54\",\"UPDATE_USER_NAME\":\"admin\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":93,\"ICON_FILES_ID\":93,\"DISTRICT_ID\":93}','{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":89,\"BIRTHDAY_TIME\":\"2017-05-29 23:15:00\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-05-29 23:15:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-30 15:15:54\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-30 15:15:54\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":93,\"ICON_FILES_ID\":93,\"DISTRICT_ID\":93}','FaUserInfo'),(2,'2017-06-01 20:37:15','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":2,\"FATHER_ID\":38,\"BIRTHDAY_TIME\":\"2017-05-30 06:38:46\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-05-30 06:38:46\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-30 14:39:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-30 14:39:00\",\"UPDATE_USER_NAME\":\"admin\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":47,\"ICON_FILES_ID\":47,\"DISTRICT_ID\":47}','{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":2,\"FATHER_ID\":38,\"BIRTHDAY_TIME\":\"2017-05-30 06:38:00\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-05-30 06:38:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-30 14:39:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-30 14:39:00\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":47,\"ICON_FILES_ID\":47,\"DISTRICT_ID\":47}','FaUserInfo'),(3,'2017-06-01 20:39:35','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":74,\"BIRTHDAY_TIME\":\"2017-06-01 12:39:21\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:39:21\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:39:35\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:39:35\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":191,\"ICON_FILES_ID\":191,\"DISTRICT_ID\":191}','FaUserInfo'),(4,'2017-06-01 20:39:48','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":75,\"BIRTHDAY_TIME\":\"2017-06-01 12:39:44\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:39:44\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:39:47\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:39:47\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":192,\"ICON_FILES_ID\":192,\"DISTRICT_ID\":192}','FaUserInfo'),(5,'2017-06-01 20:40:08','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":2,\"FATHER_ID\":75,\"BIRTHDAY_TIME\":\"2017-06-01 12:39:54\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:39:54\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:40:08\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:40:08\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":193,\"ICON_FILES_ID\":193,\"DISTRICT_ID\":193}','FaUserInfo'),(6,'2017-06-01 20:40:21','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":3,\"FATHER_ID\":75,\"BIRTHDAY_TIME\":\"2017-06-01 12:40:14\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:40:14\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:40:21\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:40:21\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":194,\"ICON_FILES_ID\":194,\"DISTRICT_ID\":194}','FaUserInfo'),(7,'2017-06-01 20:40:42','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":76,\"BIRTHDAY_TIME\":\"2017-06-01 12:40:36\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:40:36\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:40:41\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:40:41\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":195,\"ICON_FILES_ID\":195,\"DISTRICT_ID\":195}','FaUserInfo'),(8,'2017-06-01 20:41:06','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":2,\"FATHER_ID\":76,\"BIRTHDAY_TIME\":\"2017-06-01 12:40:50\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:40:50\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:41:05\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:41:05\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":196,\"ICON_FILES_ID\":196,\"DISTRICT_ID\":196}','FaUserInfo'),(9,'2017-06-01 20:41:49','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":3,\"FATHER_ID\":76,\"BIRTHDAY_TIME\":\"2017-06-01 12:41:15\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:41:15\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:41:49\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:41:49\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":197,\"ICON_FILES_ID\":197,\"DISTRICT_ID\":197}','FaUserInfo'),(10,'2017-06-01 20:42:03','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":77,\"BIRTHDAY_TIME\":\"2017-06-01 12:41:58\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:41:58\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:42:02\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:42:02\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":198,\"ICON_FILES_ID\":198,\"DISTRICT_ID\":198}','FaUserInfo'),(11,'2017-06-01 20:42:46','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":78,\"BIRTHDAY_TIME\":\"2017-06-01 12:42:17\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:42:17\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:42:46\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:42:46\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":199,\"ICON_FILES_ID\":199,\"DISTRICT_ID\":199}','FaUserInfo'),(12,'2017-06-01 20:43:27','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":1,\"FATHER_ID\":80,\"BIRTHDAY_TIME\":\"2017-06-01 12:43:20\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:43:20\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:43:26\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:43:26\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":200,\"ICON_FILES_ID\":200,\"DISTRICT_ID\":200}','FaUserInfo'),(13,'2017-06-01 20:43:49','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":2,\"FATHER_ID\":80,\"BIRTHDAY_TIME\":\"2017-06-01 12:43:33\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"2017-06-01 12:43:33\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-01 20:43:48\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-01 20:43:48\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":201,\"ICON_FILES_ID\":201,\"DISTRICT_ID\":201}','FaUserInfo'),(14,'2017-06-07 22:41:07','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"1966-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"admin\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来1\",\"LOGIN_NAME\":\"18180770313\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"1966-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来1\",\"LOGIN_NAME\":\"18180770313\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo'),(15,'2017-06-07 22:41:32','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"1966-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁来\",\"LOGIN_NAME\":\"18180770313\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"1966-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁来\",\"LOGIN_NAME\":\"18180770313\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo'),(16,'2017-06-07 22:42:08','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"1966-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"LOGIN_NAME\":\"18180770313\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"1966-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"LOGIN_NAME\":\"18180770313\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo'),(17,'2017-06-10 23:46:28','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"FATHER_ID\":8,\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-10 23:46:27\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-10 23:46:27\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":206,\"ICON_FILES_ID\":206,\"DISTRICT_ID\":206}','FaUserInfo'),(18,'2017-06-10 23:46:29','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"FATHER_ID\":8,\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-10 23:46:28\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-10 23:46:28\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":207,\"ICON_FILES_ID\":207,\"DISTRICT_ID\":207}','FaUserInfo'),(19,'2017-06-10 23:55:24','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"FATHER_ID\":9,\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-10 23:55:23\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-10 23:55:23\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":208,\"ICON_FILES_ID\":208,\"DISTRICT_ID\":208}','FaUserInfo'),(20,'2017-06-10 23:55:25','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"FATHER_ID\":9,\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-10 23:55:24\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-10 23:55:24\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":209,\"ICON_FILES_ID\":209,\"DISTRICT_ID\":209}','FaUserInfo'),(21,'2017-06-13 00:13:14','翁志来',1,NULL,'{\"FriendList\":[],\"EventList\":[],\"FATHER_ID\":17,\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-06-13 00:13:13\",\"CREATE_USER_NAME\":\"翁志来\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-06-13 00:13:13\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"ID\":210,\"ICON_FILES_ID\":210,\"DISTRICT_ID\":210}','FaUserInfo'),(22,'2017-07-01 23:35:06','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":1.0,\"DIED_TIME\":\"1966-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo'),(23,'2017-07-01 23:35:50','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-03-12 14:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-04-17 00:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo'),(24,'2017-07-08 23:38:34','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-04-17 00:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-12-30 00:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-04-17 00:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-09-29 03:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo'),(25,'2017-07-08 23:38:54','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-04-17 00:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-09-29 03:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-04-17 00:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-09-25 03:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo'),(26,'2017-07-08 23:39:19','翁志来',1,'{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-04-17 00:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-09-25 03:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','{\"FriendList\":[],\"EventList\":[],\"FatherName\":\"翁应中\",\"LEVEL_ID\":4,\"FATHER_ID\":6,\"BIRTHDAY_TIME\":\"1981-04-17 00:00:00\",\"BIRTHDAY_PLACE\":\"四川南充\",\"IS_LIVE\":0.0,\"DIED_TIME\":\"2017-09-24 03:00:00\",\"YEARS_TYPE\":\"阳历\",\"SEX\":\"男\",\"STATUS\":\"正常\",\"CREATE_TIME\":\"2017-05-28 00:00:00\",\"CREATE_USER_NAME\":\"admin\",\"CREATE_USER_ID\":1,\"UPDATE_TIME\":\"2017-05-28 23:15:22\",\"UPDATE_USER_NAME\":\"翁志来\",\"UPDATE_USER_ID\":1,\"RoleAllNameStr\":\"\",\"RoleAllIDStr\":\"\",\"AllModuleIdStr\":\"\",\"UserDistrict\":\"\",\"ID\":1,\"NAME\":\"翁志来\",\"ICON_FILES_ID\":1,\"DISTRICT_ID\":1}','FaUserInfo');
/*!40000 ALTER TABLE `fa_updata_log` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_user`
--

DROP TABLE IF EXISTS `fa_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_user` (
  `ID` int(11) NOT NULL,
  `NAME` varchar(80) DEFAULT NULL,
  `LOGIN_NAME` varchar(20) DEFAULT NULL,
  `ICON_FILES_ID` int(11) DEFAULT NULL,
  `DISTRICT_ID` int(11) NOT NULL,
  `IS_LOCKED` decimal(1,0) DEFAULT NULL,
  `CREATE_TIME` datetime DEFAULT NULL,
  `LOGIN_COUNT` int(11) DEFAULT NULL,
  `LAST_LOGIN_TIME` datetime DEFAULT NULL,
  `LAST_LOGOUT_TIME` datetime DEFAULT NULL,
  `LAST_ACTIVE_TIME` datetime DEFAULT NULL,
  `REMARK` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_USER_REF_DISTRICT` (`DISTRICT_ID`),
  CONSTRAINT `fa_user_ibfk_1` FOREIGN KEY (`DISTRICT_ID`) REFERENCES `fa_district` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_user`
--

LOCK TABLES `fa_user` WRITE;
/*!40000 ALTER TABLE `fa_user` DISABLE KEYS */;
INSERT INTO `fa_user` VALUES (1,'翁志来','18180770313',22,1,0,'2017-05-17 17:49:00',114,'2017-07-31 14:58:00','2019-07-27 23:03:29','2019-07-27 23:03:29','手机。。？'),(2,'翁庆三',NULL,0,1,NULL,'2019-03-29 07:43:21',NULL,NULL,NULL,NULL,NULL),(6,'翁应中','17323097208',23,1,0,'2017-05-17 22:32:00',NULL,'2017-05-17 22:32:00','2019-05-31 08:41:06','2019-05-31 08:41:06',NULL),(8,'翁雅萱',NULL,NULL,1,0,'2017-05-19 00:02:00',NULL,'2017-05-19 00:02:00','2019-05-26 19:25:56','2019-05-26 19:25:56',NULL),(9,'翁雅芸',NULL,NULL,1,0,'2017-05-19 13:43:00',NULL,'2017-05-19 13:43:00','2019-05-26 19:08:31','2019-05-26 19:08:31',NULL),(10,'翁德炳',NULL,NULL,1,0,'2017-05-19 13:55:47',NULL,'2017-05-19 13:55:47',NULL,'2017-05-19 13:55:47',NULL),(11,'翁素梅',NULL,NULL,1,0,'2017-05-19 13:56:15',NULL,'2017-05-19 13:56:15',NULL,'2017-05-19 13:56:15',NULL),(14,'翁秀菊',NULL,NULL,1,0,'2017-05-20 15:38:36',NULL,'2017-05-20 15:38:36',NULL,'2017-05-20 15:38:36',NULL),(15,'翁文秀',NULL,NULL,1,0,'2017-05-20 15:38:59',NULL,'2017-05-20 15:38:59',NULL,'2017-05-20 15:38:59',NULL),(16,'翁应义',NULL,NULL,1,0,'2017-05-23 15:20:40',NULL,'2017-05-23 15:20:40',NULL,'2017-05-23 15:20:40',NULL),(17,'翁自亮',NULL,NULL,1,0,'2017-05-23 15:22:09',NULL,'2017-05-23 15:22:09',NULL,'2017-05-23 15:22:09',NULL),(18,'翁小琼',NULL,NULL,1,0,'2017-05-23 15:22:32',NULL,'2017-05-23 15:22:32',NULL,'2017-05-23 15:22:32',NULL),(19,'翁泽贵',NULL,NULL,1,0,'2017-05-30 14:03:37',NULL,'2017-05-30 14:03:37',NULL,'2017-05-30 14:03:37',NULL),(20,'翁德海',NULL,NULL,1,0,'2017-05-30 14:04:39',NULL,'2017-05-30 14:04:39',NULL,'2017-05-30 14:04:39',NULL),(21,'翁德惠',NULL,NULL,1,0,'2017-05-30 14:05:00',NULL,'2017-05-30 14:05:00',NULL,'2017-05-30 14:05:00',NULL),(22,'翁世开',NULL,NULL,1,0,'2017-05-30 14:07:56',NULL,'2017-05-30 14:07:56',NULL,'2017-05-30 14:07:56',NULL),(23,'翁泽*',NULL,NULL,1,0,'2017-05-30 14:08:51',NULL,'2017-05-30 14:08:51',NULL,'2017-05-30 14:08:51',NULL),(24,'翁泽*',NULL,NULL,1,0,'2017-05-30 14:09:19',NULL,'2017-05-30 14:09:19',NULL,'2017-05-30 14:09:19',NULL),(25,'翁炳琏',NULL,NULL,1,0,'2017-05-30 14:12:53',NULL,'2017-05-30 14:12:53',NULL,'2017-05-30 14:12:53',NULL),(26,'翁大洲',NULL,NULL,1,0,'2017-05-30 14:13:54',NULL,'2017-05-30 14:13:54',NULL,'2017-05-30 14:13:54',NULL),(27,'翁炳瑚',NULL,NULL,1,0,'2017-05-30 14:14:30',NULL,'2017-05-30 14:14:30',NULL,'2017-05-30 14:14:30',NULL),(28,'翁世成',NULL,NULL,1,0,'2017-05-30 14:15:37',NULL,'2017-05-30 14:15:37',NULL,'2017-05-30 14:15:37',NULL),(29,'翁世培',NULL,NULL,1,0,'2017-05-30 14:16:03',NULL,'2017-05-30 14:16:03',NULL,'2017-05-30 14:16:03',NULL),(30,'翁世泽',NULL,NULL,1,0,'2017-05-30 14:16:35',NULL,'2017-05-30 14:16:35',NULL,'2017-05-30 14:16:35',NULL),(34,'翁应刚',NULL,NULL,1,0,'2017-05-30 14:23:51',NULL,'2017-05-30 14:23:51',NULL,'2017-05-30 14:23:51',NULL),(35,'翁应涛',NULL,NULL,1,0,'2017-05-30 14:24:16',NULL,'2017-05-30 14:24:16',NULL,'2017-05-30 14:24:16',NULL),(36,'翁应兵',NULL,NULL,1,0,'2017-05-30 14:24:37',NULL,'2017-05-30 14:24:37',NULL,'2017-05-30 14:24:37',NULL),(37,'翁世龙',NULL,NULL,1,0,'2017-05-30 14:28:39',NULL,'2017-05-30 14:28:39',NULL,'2017-05-30 14:28:39',NULL),(38,'翁世凤',NULL,NULL,1,0,'2017-05-30 14:29:57',NULL,'2017-05-30 14:29:57',NULL,'2017-05-30 14:29:57',NULL),(39,'翁世荣',NULL,NULL,1,0,'2017-05-30 14:30:38',NULL,'2017-05-30 14:30:38',NULL,'2017-05-30 14:30:38',NULL),(40,'翁世贵',NULL,NULL,1,0,'2017-05-30 14:30:53',NULL,'2017-05-30 14:30:53',NULL,'2017-05-30 14:30:53',NULL),(41,'翁泽举',NULL,NULL,1,0,'2017-05-30 14:32:23',NULL,'2017-05-30 14:32:23',NULL,'2017-05-30 14:32:23',NULL),(42,'翁德锡',NULL,NULL,1,0,'2017-05-30 14:34:51',NULL,'2017-05-30 14:34:51',NULL,'2017-05-30 14:34:51',NULL),(43,'翁德金',NULL,NULL,1,0,'2017-05-30 14:35:14',NULL,'2017-05-30 14:35:14',NULL,'2017-05-30 14:35:14',NULL),(44,'翁德银',NULL,NULL,1,0,'2017-05-30 14:35:30',NULL,'2017-05-30 14:35:30',NULL,'2017-05-30 14:35:30',NULL),(45,'翁应友',NULL,NULL,1,0,'2017-05-30 14:35:52',NULL,'2017-05-30 14:35:52',NULL,'2017-05-30 14:35:52',NULL),(46,'翁泽文',NULL,NULL,1,0,'2017-05-30 14:38:14',NULL,'2017-05-30 14:38:14',NULL,'2017-05-30 14:38:14',NULL),(47,'翁泽芝',NULL,NULL,1,0,'2017-05-30 14:39:00',NULL,'2017-05-30 14:39:00',NULL,'2017-05-30 14:39:00',NULL),(48,'翁德财',NULL,NULL,1,0,'2017-05-30 14:39:32',NULL,'2017-05-30 14:39:32',NULL,'2017-05-30 14:39:32',NULL),(49,'翁德应',NULL,NULL,1,0,'2017-05-30 14:40:11',NULL,'2017-05-30 14:40:11',NULL,'2017-05-30 14:40:11',NULL),(50,'翁德福',NULL,NULL,1,0,'2017-05-30 14:40:33',NULL,'2017-05-30 14:40:33',NULL,'2017-05-30 14:40:33',NULL),(51,'翁应清','',NULL,1,0,'2017-05-30 14:41:51',NULL,'2017-05-30 14:41:51',NULL,'2017-05-30 14:41:51',NULL),(52,'翁德学',NULL,NULL,1,0,'2017-05-30 14:42:48',NULL,'2017-05-30 14:42:48',NULL,'2017-05-30 14:42:48',NULL),(54,'翁德周',NULL,NULL,1,0,'2017-05-30 14:43:38',NULL,'2017-05-30 14:43:38',NULL,'2017-05-30 14:43:38',NULL),(55,'翁应玉',NULL,NULL,1,0,'2017-05-30 14:44:03',NULL,'2017-05-30 14:44:03',NULL,'2017-05-30 14:44:03',NULL),(56,'翁应富',NULL,NULL,1,0,'2017-05-30 14:44:32',NULL,'2017-05-30 14:44:32',NULL,'2017-05-30 14:44:32',NULL),(57,'翁应茂',NULL,NULL,1,0,'2017-05-30 14:44:45',NULL,'2017-05-30 14:44:45',NULL,'2017-05-30 14:44:45',NULL),(58,'翁泽松',NULL,NULL,1,0,'2017-05-30 14:50:56',NULL,'2017-05-30 14:50:56',NULL,'2017-05-30 14:50:56',NULL),(59,'翁德成',NULL,NULL,1,0,'2017-05-30 14:51:21',NULL,'2017-05-30 14:51:21',NULL,'2017-05-30 14:51:21',NULL),(60,'翁允伍',NULL,NULL,1,0,'2017-05-30 14:52:53',NULL,'2017-05-30 14:52:53',NULL,'2017-05-30 14:52:53',NULL),(61,'翁大明',NULL,NULL,1,0,'2017-05-30 14:53:31',NULL,'2017-05-30 14:53:31',NULL,'2017-05-30 14:53:31',NULL),(62,'翁大德',NULL,62,1,0,'2017-05-30 14:53:48',NULL,'2017-05-30 14:53:48',NULL,'2017-05-30 14:53:48',NULL),(63,'翁炳元',NULL,NULL,1,0,'2017-05-30 14:55:06',NULL,'2017-05-30 14:55:06',NULL,'2017-05-30 14:55:06',NULL),(68,'翁世金',NULL,NULL,1,0,'2017-05-30 14:58:38',NULL,'2017-05-30 14:58:38',NULL,'2017-05-30 14:58:38',NULL),(69,'翁世坤',NULL,NULL,1,0,'2017-05-30 14:59:10',NULL,'2017-05-30 14:59:10',NULL,'2017-05-30 14:59:10',NULL),(70,'翁泽胜',NULL,NULL,1,0,'2017-05-30 14:59:53',NULL,'2017-05-30 14:59:53',NULL,'2017-05-30 14:59:53',NULL),(71,'翁泽润',NULL,NULL,1,0,'2017-05-30 15:00:10',NULL,'2017-05-30 15:00:10',NULL,'2017-05-30 15:00:10',NULL),(72,'翁泽元',NULL,NULL,1,0,'2017-05-30 15:00:35',NULL,'2017-05-30 15:00:35',NULL,'2017-05-30 15:00:35',NULL),(73,'翁泽于',NULL,NULL,1,0,'2017-05-30 15:01:16',NULL,'2017-05-30 15:01:16',NULL,'2017-05-30 15:01:16',NULL),(74,'翁德孝',NULL,NULL,1,0,'2017-05-30 15:03:40',NULL,'2017-05-30 15:03:40',NULL,'2017-05-30 15:03:40',NULL),(75,'翁德地',NULL,NULL,1,0,'2017-05-30 15:04:30',NULL,'2017-05-30 15:04:30',NULL,'2017-05-30 15:04:30',NULL),(76,'翁德忠',NULL,NULL,1,0,'2017-05-30 15:04:46',NULL,'2017-05-30 15:04:46',NULL,'2017-05-30 15:04:46',NULL),(77,'翁德信',NULL,NULL,1,0,'2017-05-30 15:04:58',NULL,'2017-05-30 15:04:58',NULL,'2017-05-30 15:04:58',NULL),(78,'翁德智',NULL,NULL,1,0,'2017-05-30 15:06:18',NULL,'2017-05-30 15:06:18',NULL,'2017-05-30 15:06:18',NULL),(79,'翁德春',NULL,NULL,1,0,'2017-05-30 15:06:36',NULL,'2017-05-30 15:06:36',NULL,'2017-05-30 15:06:36',NULL),(80,'翁德让',NULL,NULL,1,0,'2017-05-30 15:06:48',NULL,'2017-05-30 15:06:48',NULL,'2017-05-30 15:06:48',NULL),(81,'翁世笃',NULL,NULL,1,0,'2017-05-30 15:11:02',NULL,'2017-05-30 15:11:02',NULL,'2017-05-30 15:11:02',NULL),(82,'翁世忠',NULL,NULL,1,0,'2017-05-30 15:11:18',NULL,'2017-05-30 15:11:18',NULL,'2017-05-30 15:11:18',NULL),(83,'翁世真',NULL,NULL,1,0,'2017-05-30 15:11:38',NULL,'2017-05-30 15:11:38',NULL,'2017-05-30 15:11:38',NULL),(84,'翁世英',NULL,NULL,1,0,'2017-05-30 15:11:51',NULL,'2017-05-30 15:11:51',NULL,'2017-05-30 15:11:51',NULL),(85,'翁世雄',NULL,NULL,1,0,'2017-05-30 15:12:11',NULL,'2017-05-30 15:12:11',NULL,'2017-05-30 15:12:11',NULL),(86,'翁泽儒',NULL,NULL,1,0,'2017-05-30 15:12:49',NULL,'2017-05-30 15:12:49',NULL,'2017-05-30 15:12:49',NULL),(87,'翁泽民',NULL,NULL,1,0,'2017-05-30 15:13:06',NULL,'2017-05-30 15:13:06',NULL,'2017-05-30 15:13:06',NULL),(88,'翁泽扬',NULL,NULL,1,0,'2017-05-30 15:13:19',NULL,'2017-05-30 15:13:19',NULL,'2017-05-30 15:13:19',NULL),(89,'翁泽海',NULL,NULL,1,0,'2017-05-30 15:13:39',NULL,'2017-05-30 15:13:39',NULL,'2017-05-30 15:13:39',NULL),(90,'翁泽四',NULL,NULL,1,0,'2017-05-30 15:14:30',NULL,'2017-05-30 15:14:30',NULL,'2017-05-30 15:14:30',NULL),(91,'翁德超',NULL,NULL,1,0,'2017-05-30 15:15:19',NULL,'2017-05-30 15:15:19',NULL,'2017-05-30 15:15:19',NULL),(92,'翁德群',NULL,NULL,1,0,'2017-05-30 15:15:30',NULL,'2017-05-30 15:15:30',NULL,'2017-05-30 15:15:30',NULL),(93,'翁德芳',NULL,NULL,1,0,'2017-05-30 15:15:54',NULL,'2017-05-30 15:15:54',NULL,'2017-05-30 15:15:54',NULL),(94,'翁德联',NULL,NULL,1,0,'2017-05-30 15:16:15',NULL,'2017-05-30 15:16:15',NULL,'2017-05-30 15:16:15',NULL),(95,'翁德贵',NULL,NULL,1,0,'2017-05-30 15:16:55',NULL,'2017-05-30 15:16:55',NULL,'2017-05-30 15:16:55',NULL),(96,'翁应泽',NULL,NULL,1,0,'2017-05-30 15:18:59',NULL,'2017-05-30 15:18:59',NULL,'2017-05-30 15:18:59',NULL),(97,'翁应良',NULL,NULL,1,0,'2017-05-30 15:19:14',NULL,'2017-05-30 15:19:14',NULL,'2017-05-30 15:19:14',NULL),(98,'翁应军','15882602397',NULL,1,0,'2017-05-30 15:20:06',NULL,'2017-05-30 15:20:06',NULL,'2017-05-30 15:20:06',NULL),(99,'翁应党',NULL,NULL,1,0,'2017-05-30 15:20:20',NULL,'2017-05-30 15:20:20',NULL,'2017-05-30 15:20:20',NULL),(100,'翁应辉',NULL,NULL,1,0,'2017-05-30 15:20:43',NULL,'2017-05-30 15:20:43',NULL,'2017-05-30 15:20:43',NULL),(101,'翁应凯',NULL,NULL,1,0,'2017-05-30 15:22:11',NULL,'2017-05-30 15:22:11',NULL,'2017-05-30 15:22:11',NULL),(102,'翁建国',NULL,NULL,1,0,'2017-05-30 15:22:26',NULL,'2017-05-30 15:22:26',NULL,'2017-05-30 15:22:26',NULL),(103,'翁勇军',NULL,NULL,1,0,'2017-05-30 15:23:13',NULL,'2017-05-30 15:23:13',NULL,'2017-05-30 15:23:13',NULL),(104,'翁(茂)学军',NULL,NULL,1,0,'2017-05-30 15:24:00',NULL,'2017-05-30 15:24:00',NULL,'2017-05-30 15:24:00',NULL),(105,'翁炳禄',NULL,NULL,1,0,'2017-05-30 15:30:03',NULL,'2017-05-30 15:30:03',NULL,'2017-05-30 15:30:03',NULL),(106,'翁先纭',NULL,NULL,1,0,'2017-05-30 15:33:28',NULL,'2017-05-30 15:33:28',NULL,'2017-05-30 15:33:28',NULL),(107,'翁允倡',NULL,NULL,1,0,'2017-05-30 15:34:04',NULL,'2017-05-30 15:34:04',NULL,'2017-05-30 15:34:04',NULL),(108,'翁大秩',NULL,NULL,1,0,'2017-05-30 15:35:30',NULL,'2017-05-30 15:35:30',NULL,'2017-05-30 15:35:30',NULL),(109,'翁炳富',NULL,NULL,1,0,'2017-05-30 15:36:22',NULL,'2017-05-30 15:36:22',NULL,'2017-05-30 15:36:22',NULL),(110,'翁炳贵',NULL,NULL,1,0,'2017-05-30 15:36:38',NULL,'2017-05-30 15:36:38',NULL,'2017-05-30 15:36:38',NULL),(111,'翁炳荣',NULL,NULL,1,0,'2017-05-30 15:36:59',NULL,'2017-05-30 15:36:59',NULL,'2017-05-30 15:36:59',NULL),(112,'翁炳华',NULL,NULL,1,0,'2017-05-30 15:37:24',NULL,'2017-05-30 15:37:24',NULL,'2017-05-30 15:37:24',NULL),(113,'翁世德',NULL,NULL,1,0,'2017-05-30 15:37:58',NULL,'2017-05-30 15:37:58',NULL,'2017-05-30 15:37:58',NULL),(114,'翁世海',NULL,NULL,1,0,'2017-05-30 15:38:16',NULL,'2017-05-30 15:38:16',NULL,'2017-05-30 15:38:16',NULL),(115,'翁世伦',NULL,NULL,1,0,'2017-05-30 15:57:18',NULL,'2017-05-30 15:57:18',NULL,'2017-05-30 15:57:18',NULL),(116,'翁世春',NULL,NULL,1,0,'2017-05-30 15:57:42',NULL,'2017-05-30 15:57:42',NULL,'2017-05-30 15:57:42',NULL),(117,'翁世益',NULL,NULL,1,0,'2017-05-30 15:58:31',NULL,'2017-05-30 15:58:31',NULL,'2017-05-30 15:58:31',NULL),(118,'翁泽兴',NULL,NULL,1,0,'2017-05-30 15:59:29',NULL,'2017-05-30 15:59:29',NULL,'2017-05-30 15:59:29',NULL),(119,'翁泽喜',NULL,NULL,1,0,'2017-05-30 15:59:52',NULL,'2017-05-30 15:59:52',NULL,'2017-05-30 15:59:52',NULL),(120,'翁泽有',NULL,NULL,1,0,'2017-05-30 16:00:14',NULL,'2017-05-30 16:00:14',NULL,'2017-05-30 16:00:14',NULL),(121,'翁泽明',NULL,NULL,1,0,'2017-05-30 16:00:26',NULL,'2017-05-30 16:00:26',NULL,'2017-05-30 16:00:26',NULL),(122,'翁泽和',NULL,NULL,1,0,'2017-05-30 16:00:38',NULL,'2017-05-30 16:00:38',NULL,'2017-05-30 16:00:38',NULL),(123,'翁泽礼',NULL,NULL,1,0,'2017-05-30 16:00:55',NULL,'2017-05-30 16:00:55',NULL,'2017-05-30 16:00:55',NULL),(124,'翁泽义',NULL,NULL,1,0,'2017-05-30 16:01:08',NULL,'2017-05-30 16:01:08',NULL,'2017-05-30 16:01:08',NULL),(125,'翁泽仁',NULL,NULL,1,0,'2017-05-30 16:01:32',NULL,'2017-05-30 16:01:32',NULL,'2017-05-30 16:01:32',NULL),(126,'翁泽清',NULL,NULL,1,0,'2017-05-30 16:01:56',NULL,'2017-05-30 16:01:56',NULL,'2017-05-30 16:01:56',NULL),(127,'翁泽记',NULL,NULL,1,0,'2017-05-30 16:02:23',NULL,'2017-05-30 16:02:23',NULL,'2017-05-30 16:02:23',NULL),(128,'翁泽平',NULL,NULL,1,0,'2017-05-30 16:02:39',NULL,'2017-05-30 16:02:39',NULL,'2017-05-30 16:02:39',NULL),(129,'翁泽安',NULL,NULL,1,0,'2017-05-30 16:02:51',NULL,'2017-05-30 16:02:51',NULL,'2017-05-30 16:02:51',NULL),(130,'翁泽福',NULL,NULL,1,0,'2017-05-30 16:03:09',NULL,'2017-05-30 16:03:09',NULL,'2017-05-30 16:03:09',NULL),(131,'翁德权',NULL,NULL,1,0,'2017-05-30 16:07:49',NULL,'2017-05-30 16:07:49',NULL,'2017-05-30 16:07:49',NULL),(132,'翁德润',NULL,NULL,1,0,'2017-05-30 16:08:44',NULL,'2017-05-30 16:08:44',NULL,'2017-05-30 16:08:44',NULL),(133,'翁德政',NULL,NULL,1,0,'2017-05-30 16:08:59',NULL,'2017-05-30 16:08:59',NULL,'2017-05-30 16:08:59',NULL),(134,'翁德富',NULL,NULL,1,0,'2017-05-30 16:09:20',NULL,'2017-05-30 16:09:20',NULL,'2017-05-30 16:09:20',NULL),(135,'翁德茂',NULL,NULL,1,0,'2017-05-30 16:09:47',NULL,'2017-05-30 16:09:47',NULL,'2017-05-30 16:09:47',NULL),(136,'翁德轩',NULL,NULL,1,0,'2017-05-30 16:13:09',NULL,'2017-05-30 16:13:09',NULL,'2017-05-30 16:13:09',NULL),(137,'翁德元',NULL,NULL,1,0,'2017-05-30 16:13:32',NULL,'2017-05-30 16:13:32',NULL,'2017-05-30 16:13:32',NULL),(138,'翁德俊',NULL,NULL,1,0,'2017-05-30 16:15:06',NULL,'2017-05-30 16:15:06',NULL,'2017-05-30 16:15:06',NULL),(139,'翁德玉',NULL,NULL,1,0,'2017-05-30 16:15:27',NULL,'2017-05-30 16:15:27',NULL,'2017-05-30 16:15:27',NULL),(140,'翁德太',NULL,NULL,1,0,'2017-05-30 16:16:04',NULL,'2017-05-30 16:16:04',NULL,'2017-05-30 16:16:04',NULL),(141,'翁德汉',NULL,NULL,1,0,'2017-05-30 16:16:24',NULL,'2017-05-30 16:16:24',NULL,'2017-05-30 16:16:24',NULL),(142,'翁德福',NULL,NULL,1,0,'2017-05-30 16:16:49',NULL,'2017-05-30 16:16:49',NULL,'2017-05-30 16:16:49',NULL),(143,'翁德禄',NULL,NULL,1,0,'2017-05-30 16:17:07',NULL,'2017-05-30 16:17:07',NULL,'2017-05-30 16:17:07',NULL),(144,'翁德乾',NULL,NULL,1,0,'2017-05-30 16:18:27',NULL,'2017-05-30 16:18:27',NULL,'2017-05-30 16:18:27',NULL),(145,'翁德亮',NULL,NULL,1,0,'2017-05-30 16:18:48',NULL,'2017-05-30 16:18:48',NULL,'2017-05-30 16:18:48',NULL),(146,'翁德金',NULL,NULL,1,0,'2017-05-30 16:19:03',NULL,'2017-05-30 16:19:03',NULL,'2017-05-30 16:19:03',NULL),(147,'翁德荣',NULL,NULL,1,0,'2017-05-30 16:23:30',NULL,'2017-05-30 16:23:30',NULL,'2017-05-30 16:23:30',NULL),(148,'翁德章',NULL,NULL,1,0,'2017-05-30 16:24:00',NULL,'2017-05-30 16:24:00',NULL,'2017-05-30 16:24:00',NULL),(149,'翁德书',NULL,NULL,1,0,'2017-05-30 16:24:35',NULL,'2017-05-30 16:24:35',NULL,'2017-05-30 16:24:35',NULL),(150,'翁德*',NULL,NULL,1,0,'2017-05-30 16:24:48',NULL,'2017-05-30 16:24:48',NULL,'2017-05-30 16:24:48',NULL),(151,'翁德龙',NULL,NULL,1,0,'2017-05-30 16:25:09',NULL,'2017-05-30 16:25:09',NULL,'2017-05-30 16:25:09',NULL),(152,'翁德凤',NULL,NULL,1,0,'2017-05-30 16:25:21',NULL,'2017-05-30 16:25:21',NULL,'2017-05-30 16:25:21',NULL),(153,'翁德富',NULL,NULL,1,0,'2017-05-30 16:25:39',NULL,'2017-05-30 16:25:39',NULL,'2017-05-30 16:25:39',NULL),(154,'翁德义',NULL,NULL,1,0,'2017-05-30 16:26:16',NULL,'2017-05-30 16:26:16',NULL,'2017-05-30 16:26:16',NULL),(155,'翁德润',NULL,NULL,1,0,'2017-05-30 16:26:31',NULL,'2017-05-30 16:26:31',NULL,'2017-05-30 16:26:31',NULL),(156,'翁德勇',NULL,NULL,1,0,'2017-05-30 16:26:43',NULL,'2017-05-30 16:26:43',NULL,'2017-05-30 16:26:43',NULL),(157,'翁茂林',NULL,NULL,1,0,'2017-05-30 16:29:10',NULL,'2017-05-30 16:29:10',NULL,'2017-05-30 16:29:10',NULL),(158,'翁茂兵',NULL,NULL,1,0,'2017-05-30 16:29:35',NULL,'2017-05-30 16:29:35',NULL,'2017-05-30 16:29:35',NULL),(159,'翁应林',NULL,NULL,1,0,'2017-05-30 16:30:19',NULL,'2017-05-30 16:30:19',NULL,'2017-05-30 16:30:19',NULL),(160,'翁应文',NULL,NULL,1,0,'2017-05-30 16:30:34',NULL,'2017-05-30 16:30:34',NULL,'2017-05-30 16:30:34',NULL),(161,'翁贵元',NULL,NULL,1,0,'2017-05-30 16:31:26',NULL,'2017-05-30 16:31:26',NULL,'2017-05-30 16:31:26',NULL),(162,'翁应伯',NULL,NULL,1,0,'2017-05-30 16:32:18',NULL,'2017-05-30 16:32:18',NULL,'2017-05-30 16:32:18',NULL),(163,'翁应科',NULL,NULL,1,0,'2017-05-30 16:34:16',NULL,'2017-05-30 16:34:16',NULL,'2017-05-30 16:34:16',NULL),(164,'翁华平',NULL,NULL,1,0,'2017-05-30 16:34:30',NULL,'2017-05-30 16:34:30',NULL,'2017-05-30 16:34:30',NULL),(165,'翁和平',NULL,NULL,1,0,'2017-05-30 16:34:43',NULL,'2017-05-30 16:34:43',NULL,'2017-05-30 16:34:43',NULL),(166,'翁应光',NULL,NULL,1,0,'2017-05-30 16:35:52',NULL,'2017-05-30 16:35:52',NULL,'2017-05-30 16:35:52',NULL),(167,'翁应明',NULL,NULL,1,0,'2017-05-30 16:36:13',NULL,'2017-05-30 16:36:13',NULL,'2017-05-30 16:36:13',NULL),(168,'翁应顺',NULL,NULL,1,0,'2017-05-30 16:36:28',NULL,'2017-05-30 16:36:28',NULL,'2017-05-30 16:36:28',NULL),(169,'翁应贵',NULL,NULL,1,0,'2017-05-30 16:37:21',NULL,'2017-05-30 16:37:21',NULL,'2017-05-30 16:37:21',NULL),(170,'翁应清',NULL,NULL,1,0,'2017-05-30 16:37:55',NULL,'2017-05-30 16:37:55',NULL,'2017-05-30 16:37:55',NULL),(171,'翁应安',NULL,NULL,1,0,'2017-05-30 16:38:38',NULL,'2017-05-30 16:38:38',NULL,'2017-05-30 16:38:38',NULL),(172,'翁应全',NULL,NULL,1,0,'2017-05-30 16:38:55',NULL,'2017-05-30 16:38:55',NULL,'2017-05-30 16:38:55',NULL),(173,'翁应富',NULL,NULL,1,0,'2017-05-30 16:39:09',NULL,'2017-05-30 16:39:09',NULL,'2017-05-30 16:39:09',NULL),(174,'翁应贵',NULL,174,1,0,'2017-05-30 16:39:32',NULL,'2017-05-30 16:39:32',NULL,'2017-05-30 16:39:32',NULL),(175,'翁应强',NULL,NULL,1,0,'2017-05-30 16:39:45',NULL,'2017-05-30 16:39:45',NULL,'2017-05-30 16:39:45',NULL),(176,'翁应平',NULL,NULL,1,0,'2017-05-30 16:40:02',NULL,'2017-05-30 16:40:02',NULL,'2017-05-30 16:40:02',NULL),(177,'翁应海',NULL,NULL,1,0,'2017-05-30 16:42:27',NULL,'2017-05-30 16:42:27',NULL,'2017-05-30 16:42:27',NULL),(178,'翁应坤',NULL,NULL,1,0,'2017-05-30 16:42:52',NULL,'2017-05-30 16:42:52',NULL,'2017-05-30 16:42:52',NULL),(179,'翁应平',NULL,NULL,1,0,'2017-05-30 16:43:08',NULL,'2017-05-30 16:43:08',NULL,'2017-05-30 16:43:08',NULL),(180,'翁应国',NULL,NULL,1,0,'2017-05-30 16:43:54',NULL,'2017-05-30 16:43:54',NULL,'2017-05-30 16:43:54',NULL),(181,'翁应树',NULL,NULL,1,0,'2017-05-30 16:44:07',NULL,'2017-05-30 16:44:07',NULL,'2017-05-30 16:44:07',NULL),(182,'翁茂国','15821125138',NULL,1,0,'2017-05-30 16:46:04',NULL,'2017-05-30 16:46:04',NULL,'2017-05-30 16:46:04',NULL),(183,'翁茂辉',NULL,NULL,1,0,'2017-05-30 16:46:30',NULL,'2017-05-30 16:46:30',NULL,'2017-05-30 16:46:30',NULL),(184,'翁应全',NULL,NULL,1,0,'2017-05-30 16:46:52',NULL,'2017-05-30 16:46:52',NULL,'2017-05-30 16:46:52',NULL),(185,'翁应华',NULL,NULL,1,0,'2017-05-30 16:47:13',NULL,'2017-05-30 16:47:13',NULL,'2017-05-30 16:47:13',NULL),(186,'翁应和',NULL,NULL,1,0,'2017-05-30 16:48:39',NULL,'2017-05-30 16:48:39',NULL,'2017-05-30 16:48:39',NULL),(187,'翁应志',NULL,NULL,1,0,'2017-05-30 16:49:15',NULL,'2017-05-30 16:49:15',NULL,'2017-05-30 16:49:15',NULL),(188,'翁应康',NULL,NULL,1,0,'2017-05-30 16:49:42',NULL,'2017-05-30 16:49:42',NULL,'2017-05-30 16:49:42',NULL),(189,'翁茂平',NULL,NULL,1,0,'2017-05-30 16:50:25',NULL,'2017-05-30 16:50:25',NULL,'2017-05-30 16:50:25',NULL),(190,'翁茂盛',NULL,NULL,1,0,'2017-05-30 16:51:39',NULL,'2017-05-30 16:51:39',NULL,'2017-05-30 16:51:39',NULL),(191,'翁应益',NULL,NULL,1,0,'2017-06-01 20:39:35',NULL,'2017-06-01 20:39:35',NULL,'2017-06-01 20:39:35',NULL),(192,'翁应团',NULL,NULL,1,0,'2017-06-01 20:39:48',NULL,'2017-06-01 20:39:48',NULL,'2017-06-01 20:39:48',NULL),(193,'翁康林',NULL,NULL,1,0,'2017-06-01 20:40:08',NULL,'2017-06-01 20:40:08',NULL,'2017-06-01 20:40:08',NULL),(194,'翁模林',NULL,NULL,1,0,'2017-06-01 20:40:21',NULL,'2017-06-01 20:40:21',NULL,'2017-06-01 20:40:21',NULL),(195,'翁应炳',NULL,NULL,1,0,'2017-06-01 20:40:41',NULL,'2017-06-01 20:40:41',NULL,'2017-06-01 20:40:41',NULL),(196,'翁应双',NULL,NULL,1,0,'2017-06-01 20:41:06',NULL,'2017-06-01 20:41:06',NULL,'2017-06-01 20:41:06',NULL),(197,'翁波','18227378954',NULL,1,0,'2017-06-01 20:41:49',NULL,'2017-06-01 20:41:49','2019-06-12 23:04:32','2019-06-12 23:04:32',NULL),(198,'翁茂军',NULL,NULL,1,0,'2017-06-01 20:42:03',NULL,'2017-06-01 20:42:03',NULL,'2017-06-01 20:42:03',NULL),(199,'翁应吉',NULL,NULL,1,0,'2017-06-01 20:42:46',NULL,'2017-06-01 20:42:46',NULL,'2017-06-01 20:42:46',NULL),(200,'翁应太',NULL,NULL,1,0,'2017-06-01 20:43:27',NULL,'2017-06-01 20:43:27',NULL,'2017-06-01 20:43:27',NULL),(201,'翁应七',NULL,NULL,1,0,'2017-06-01 20:43:49',NULL,'2017-06-01 20:43:49',NULL,'2017-06-01 20:43:49',NULL),(1000,'翁均甫',NULL,NULL,1,0,'2019-03-29 00:00:00',NULL,NULL,NULL,NULL,NULL),(1002,'翁常一',NULL,0,1,NULL,'2019-03-29 07:49:04',NULL,NULL,NULL,NULL,NULL),(1003,'翁常二',NULL,0,1,NULL,'2019-03-29 07:49:21',NULL,NULL,NULL,NULL,NULL),(1005,'翁和秀',NULL,0,1,NULL,'2019-03-29 07:49:48',NULL,NULL,NULL,NULL,NULL),(1006,'翁必发',NULL,0,1,NULL,'2019-03-29 07:50:31',NULL,NULL,NULL,NULL,NULL),(1007,'翁道德',NULL,0,1,NULL,'2019-03-29 07:50:48',NULL,NULL,NULL,NULL,NULL),(1008,'翁景云',NULL,0,1,NULL,'2019-03-29 07:51:34',NULL,NULL,NULL,NULL,NULL),(1009,'翁伯信',NULL,0,1,NULL,'2019-03-29 07:54:17',NULL,NULL,NULL,NULL,NULL),(1010,'翁万达',NULL,0,1,NULL,'2019-03-29 07:54:35',NULL,NULL,NULL,NULL,NULL),(1011,'翁万逸',NULL,0,1,NULL,'2019-03-29 07:54:59',NULL,NULL,NULL,NULL,NULL),(1012,'翁兴庸',NULL,0,1,NULL,'2019-03-29 07:55:21',NULL,NULL,NULL,NULL,NULL),(1013,'翁朝兰',NULL,0,1,NULL,'2019-03-29 07:56:27',NULL,NULL,NULL,NULL,NULL),(1014,'翁廷玺',NULL,0,1,NULL,'2019-03-29 07:56:52',NULL,NULL,NULL,NULL,NULL),(1015,'翁天祜',NULL,0,1,NULL,'2019-03-29 07:57:03',NULL,NULL,NULL,NULL,NULL),(1016,'翁学良',NULL,0,1,NULL,'2019-03-29 07:58:03',NULL,NULL,NULL,NULL,NULL),(1017,'翁文棱',NULL,0,1,NULL,'2019-03-29 07:58:25',NULL,NULL,NULL,NULL,NULL),(1018,'翁启惠',NULL,0,1,NULL,'2019-03-29 07:59:14',NULL,NULL,NULL,NULL,NULL),(1019,'翁光纹',NULL,0,1,NULL,'2019-03-29 08:23:15',NULL,NULL,NULL,NULL,NULL),(1020,'翁景春',NULL,0,1,NULL,'2019-03-29 13:45:16',NULL,NULL,NULL,NULL,NULL),(1021,'翁和齐',NULL,0,1,NULL,'2019-03-30 11:48:59',NULL,NULL,NULL,NULL,NULL),(1022,'翁和容',NULL,0,1,NULL,'2019-03-30 11:49:17',NULL,NULL,NULL,NULL,NULL),(1023,'翁必玉',NULL,0,1,NULL,'2019-03-30 11:50:06',NULL,NULL,NULL,NULL,NULL),(1024,'翁道盛',NULL,0,1,NULL,'2019-03-30 11:51:16',NULL,NULL,NULL,NULL,NULL),(1025,'翁道秀',NULL,0,1,NULL,'2019-03-30 11:51:45',NULL,NULL,NULL,NULL,NULL),(1026,'翁道器',NULL,0,1,NULL,'2019-03-30 11:52:11',NULL,NULL,NULL,NULL,NULL),(1027,'翁道重',NULL,0,1,NULL,'2019-03-30 11:52:51',NULL,NULL,NULL,NULL,NULL),(1028,'苏氏',NULL,0,1,NULL,'2019-05-18 03:50:21',NULL,NULL,NULL,NULL,NULL),(1029,'黄氏',NULL,0,1,NULL,'2019-05-18 15:40:16',NULL,NULL,NULL,NULL,NULL),(1030,'王氏',NULL,0,1,NULL,'2019-05-18 22:16:03',NULL,NULL,NULL,NULL,NULL),(1031,'刘氏',NULL,0,1,NULL,'2019-05-18 22:34:50',NULL,NULL,NULL,NULL,NULL),(1032,'未详',NULL,0,1,NULL,'2019-05-18 22:37:31',NULL,NULL,NULL,NULL,NULL),(1033,'李氏',NULL,0,1,NULL,'2019-05-18 22:49:51',NULL,NULL,NULL,NULL,NULL),(1034,'李氏',NULL,0,1,NULL,'2019-05-18 22:50:01',NULL,NULL,NULL,NULL,NULL),(1035,'安氏',NULL,0,1,NULL,'2019-05-18 22:51:22',NULL,NULL,NULL,NULL,NULL),(1036,'龙氏',NULL,0,1,NULL,'2019-05-18 22:52:27',NULL,NULL,NULL,NULL,NULL),(1037,'翁德铜',NULL,0,1,NULL,'2019-05-18 22:58:18',NULL,NULL,NULL,NULL,NULL),(1038,'不详',NULL,0,1,NULL,'2019-05-18 22:58:42',NULL,NULL,NULL,NULL,NULL),(1039,'翁德铁',NULL,0,1,NULL,'2019-05-18 22:59:19',NULL,NULL,NULL,NULL,NULL),(1040,'不详',NULL,0,1,NULL,'2019-05-18 22:59:33',NULL,NULL,NULL,NULL,NULL),(1041,'袁应珍',NULL,0,1,NULL,'2019-05-18 23:12:04',NULL,NULL,NULL,NULL,NULL),(1042,'李氏',NULL,0,1,NULL,'2019-05-18 23:13:13',NULL,NULL,NULL,NULL,NULL),(1043,'邓淑清',NULL,0,1,NULL,'2019-05-18 23:15:53',NULL,NULL,NULL,NULL,NULL),(1044,'蒋荣碧',NULL,0,1,NULL,'2019-05-18 23:24:06',NULL,NULL,NULL,NULL,NULL),(1045,'关闰珍',NULL,0,1,NULL,'2019-05-18 23:27:06',NULL,NULL,NULL,NULL,NULL),(1046,'杨琼珍',NULL,0,1,NULL,'2019-05-18 23:28:51',NULL,NULL,NULL,NULL,NULL),(1047,'翁志东',NULL,0,1,NULL,'2019-05-18 23:30:34',NULL,NULL,NULL,NULL,NULL),(1048,'蒋荣碧',NULL,0,1,NULL,'2019-05-18 23:31:29',NULL,NULL,NULL,NULL,NULL),(1049,'翁春林',NULL,0,1,NULL,'2019-05-18 23:32:58',NULL,NULL,NULL,NULL,NULL),(1050,'熊红英',NULL,0,1,NULL,'2019-05-18 23:33:23',NULL,NULL,NULL,NULL,NULL),(1051,'翁方修',NULL,0,1,NULL,'2019-05-18 23:34:24',NULL,NULL,NULL,NULL,NULL),(1052,'翁精英',NULL,0,1,NULL,'2019-05-18 23:35:29',NULL,NULL,NULL,NULL,NULL),(1053,'彭秀容',NULL,0,1,NULL,'2019-05-18 23:36:08',NULL,NULL,NULL,NULL,NULL),(1054,'彭秀容',NULL,0,1,NULL,'2019-05-18 23:36:10',NULL,NULL,NULL,NULL,NULL),(1056,'翁松林','18346760350',0,1,NULL,'2019-05-18 23:37:49',NULL,NULL,NULL,NULL,NULL),(1057,'关润珍',NULL,0,1,NULL,'2019-05-18 23:38:23',NULL,NULL,NULL,NULL,NULL),(1058,'张秀英',NULL,0,1,NULL,'2019-05-18 23:40:13',NULL,NULL,NULL,NULL,NULL),(1059,'翁鹏程',NULL,0,1,NULL,'2019-05-18 23:41:53',NULL,NULL,NULL,NULL,NULL),(1060,'翁鹏宇',NULL,0,1,NULL,'2019-05-18 23:43:02',NULL,NULL,NULL,NULL,NULL),(1061,'徐白英',NULL,0,1,NULL,'2019-05-19 10:58:21',NULL,NULL,NULL,NULL,NULL),(1062,'翁毅',NULL,0,1,NULL,'2019-05-19 10:59:56',NULL,NULL,NULL,NULL,NULL),(1063,'未详',NULL,0,1,NULL,'2019-05-19 11:06:13',NULL,NULL,NULL,NULL,NULL),(1064,'翁泽文',NULL,0,1,NULL,'2019-05-19 11:06:55',NULL,NULL,NULL,NULL,NULL),(1065,'徐氏',NULL,0,1,NULL,'2019-05-19 11:07:37',NULL,NULL,NULL,NULL,NULL),(1066,'翁德映',NULL,0,1,NULL,'2019-05-19 11:09:50',NULL,NULL,NULL,NULL,NULL),(1067,'翁德才',NULL,0,1,NULL,'2019-05-19 11:10:19',NULL,NULL,NULL,NULL,NULL),(1068,'翁德福',NULL,0,1,NULL,'2019-05-19 11:10:47',NULL,NULL,NULL,NULL,NULL),(1069,'未详',NULL,0,1,NULL,'2019-05-19 11:41:19',NULL,NULL,NULL,NULL,NULL),(1070,'未详',NULL,0,1,NULL,'2019-05-19 11:41:23',NULL,NULL,NULL,NULL,NULL),(1071,'翁泽能',NULL,0,1,NULL,'2019-05-19 11:42:01',NULL,NULL,NULL,NULL,NULL),(1072,'翁泽庆',NULL,0,1,NULL,'2019-05-19 11:42:23',NULL,NULL,NULL,NULL,NULL),(1073,'翁德易',NULL,0,1,NULL,'2019-05-19 11:43:23',NULL,NULL,NULL,NULL,NULL),(1074,'纪氏',NULL,0,1,NULL,'2019-05-19 11:43:58',NULL,NULL,NULL,NULL,NULL),(1075,'翁应福',NULL,0,1,NULL,'2019-05-19 11:47:41',NULL,NULL,NULL,NULL,NULL),(1076,'魏氏',NULL,0,1,NULL,'2019-05-19 11:48:15',NULL,NULL,NULL,NULL,NULL),(1077,'翁应寿',NULL,0,1,NULL,'2019-05-19 11:49:24',NULL,NULL,NULL,NULL,NULL),(1078,'李秀清',NULL,0,1,NULL,'2019-05-19 11:51:03',NULL,NULL,NULL,NULL,NULL),(1079,'翁洪华',NULL,0,1,NULL,'2019-05-19 11:52:15',NULL,NULL,NULL,NULL,NULL),(1080,'翁建华',NULL,0,1,NULL,'2019-05-19 11:53:06',NULL,NULL,NULL,NULL,NULL),(1081,'翁昕',NULL,0,1,NULL,'2019-05-19 11:54:37',NULL,NULL,NULL,NULL,NULL),(1082,'翁建军',NULL,0,1,NULL,'2019-05-19 11:55:25',NULL,NULL,NULL,NULL,NULL),(1083,'翁建忠',NULL,0,1,NULL,'2019-05-19 11:56:16',NULL,NULL,NULL,NULL,NULL),(1084,'赵丽琼',NULL,0,1,NULL,'2019-05-19 12:00:13',NULL,NULL,NULL,NULL,NULL),(1085,'翁欢',NULL,0,1,NULL,'2019-05-19 12:00:54',NULL,NULL,NULL,NULL,NULL),(1086,'马文英',NULL,0,1,NULL,'2019-05-19 12:01:32',NULL,NULL,NULL,NULL,NULL),(1087,'若春玲',NULL,0,1,NULL,'2019-05-19 12:03:12',NULL,NULL,NULL,NULL,NULL),(1088,'翁超',NULL,0,1,NULL,'2019-05-19 12:03:56',NULL,NULL,NULL,NULL,NULL),(1089,'杨洁玉',NULL,0,1,NULL,'2019-05-19 12:04:51',NULL,NULL,NULL,NULL,NULL),(1090,'翁佳',NULL,0,1,NULL,'2019-05-19 12:07:12',NULL,NULL,NULL,NULL,NULL),(1091,'袁氏',NULL,0,1,NULL,'2019-05-19 12:08:18',NULL,NULL,NULL,NULL,NULL),(1092,'乔氏',NULL,0,1,NULL,'2019-05-19 12:09:28',NULL,NULL,NULL,NULL,NULL),(1093,'李邦秀',NULL,0,1,NULL,'2019-05-19 12:14:06',NULL,NULL,NULL,NULL,NULL),(1094,'袁应珍',NULL,0,1,NULL,'2019-05-19 12:25:08',NULL,NULL,NULL,NULL,NULL),(1095,'彭素英',NULL,0,1,NULL,'2019-05-19 12:27:57',NULL,NULL,NULL,NULL,NULL),(1096,'许庭碧',NULL,0,1,NULL,'2019-05-19 13:36:57',NULL,NULL,NULL,NULL,NULL),(1097,'许庭碧',NULL,0,1,NULL,'2019-05-19 13:37:18',NULL,NULL,NULL,NULL,NULL),(1098,'李万凤',NULL,0,1,NULL,'2019-05-19 13:43:17',NULL,NULL,NULL,NULL,NULL),(1099,'翁浩然',NULL,0,1,NULL,'2019-05-19 13:44:15',NULL,NULL,NULL,NULL,NULL),(1100,'翁子晰',NULL,0,1,NULL,'2019-05-19 13:44:57',NULL,NULL,NULL,NULL,NULL),(1101,'黄丽',NULL,15,1,NULL,'2019-05-19 13:45:48',NULL,NULL,NULL,NULL,NULL),(1102,'林丽琼',NULL,0,1,NULL,'2019-05-19 13:54:35',NULL,NULL,NULL,NULL,NULL),(1103,'翁黎明',NULL,0,1,NULL,'2019-05-19 13:55:32',NULL,NULL,NULL,NULL,NULL),(1104,'翁梅君',NULL,0,1,NULL,'2019-05-19 13:56:32',NULL,NULL,NULL,NULL,NULL),(1105,'苟红梅',NULL,0,1,NULL,'2019-05-19 13:58:33',NULL,NULL,NULL,NULL,NULL),(1106,'翁敬菠',NULL,0,1,NULL,'2019-05-19 13:59:30',NULL,NULL,NULL,NULL,NULL),(1107,'唐勇军',NULL,0,1,NULL,'2019-05-19 14:01:20',NULL,NULL,NULL,NULL,NULL),(1108,'翁名慧',NULL,0,1,NULL,'2019-05-19 14:02:04',NULL,NULL,NULL,NULL,NULL),(1109,'薛氏',NULL,0,1,NULL,'2019-05-19 14:04:56',NULL,NULL,NULL,NULL,NULL),(1111,'翁世才',NULL,0,1,NULL,'2019-05-19 14:06:51',NULL,NULL,NULL,NULL,NULL),(1112,'翁世友',NULL,0,1,NULL,'2019-05-19 14:07:08',NULL,NULL,NULL,NULL,NULL),(1114,'翁世民',NULL,0,1,NULL,'2019-05-19 14:07:31',NULL,NULL,NULL,NULL,NULL),(1115,'李氏',NULL,0,1,NULL,'2019-05-19 14:11:06',NULL,NULL,NULL,NULL,NULL),(1116,'冯氏',NULL,0,1,NULL,'2019-05-19 14:12:22',NULL,NULL,NULL,NULL,NULL),(1117,'郑氏',NULL,0,1,NULL,'2019-05-19 14:13:28',NULL,NULL,NULL,NULL,NULL),(1118,'邓氏',NULL,0,1,NULL,'2019-05-19 14:15:13',NULL,NULL,NULL,NULL,NULL),(1119,'李氏',NULL,0,1,NULL,'2019-05-19 14:15:39',NULL,NULL,NULL,NULL,NULL),(1120,'龚兴珍',NULL,0,1,NULL,'2019-05-19 14:17:45',NULL,NULL,NULL,NULL,NULL),(1121,'杨桂珍',NULL,0,1,NULL,'2019-05-19 14:20:12',NULL,NULL,NULL,NULL,NULL),(1122,'徐加秀',NULL,0,1,NULL,'2019-05-19 14:22:15',NULL,NULL,NULL,NULL,NULL),(1123,'翁华容',NULL,0,1,NULL,'2019-05-19 14:22:43',NULL,NULL,NULL,NULL,NULL),(1124,'翁华兵','13308015598',0,1,NULL,'2019-05-19 14:23:38',NULL,NULL,NULL,NULL,NULL),(1125,'翁小刚',NULL,0,1,NULL,'2019-05-19 14:24:32',NULL,NULL,NULL,NULL,NULL),(1126,'翁小兰','15583008111',NULL,1,NULL,'2019-05-19 14:25:36',NULL,NULL,NULL,NULL,NULL),(1127,'翁志成',NULL,0,1,NULL,'2019-05-19 14:26:04',NULL,NULL,NULL,NULL,NULL),(1128,'翁志明',NULL,0,1,NULL,'2019-05-19 14:26:19',NULL,NULL,NULL,NULL,NULL),(1129,'文建碧',NULL,0,1,NULL,'2019-05-19 14:28:26',NULL,NULL,NULL,NULL,NULL),(1130,'文建碧',NULL,0,1,NULL,'2019-05-19 14:28:56',NULL,NULL,NULL,NULL,NULL),(1131,'李菊英',NULL,0,1,NULL,'2019-05-19 14:30:34',NULL,NULL,NULL,NULL,NULL),(1132,'谢英',NULL,0,1,NULL,'2019-05-19 14:32:07',NULL,NULL,NULL,NULL,NULL),(1133,'翁俊朗',NULL,0,1,NULL,'2019-05-19 14:35:10',NULL,NULL,NULL,NULL,NULL),(1134,'翁雅琳',NULL,0,1,NULL,'2019-05-19 14:36:26',NULL,NULL,NULL,NULL,NULL),(1135,'翁靖娴',NULL,0,1,NULL,'2019-05-19 14:37:10',NULL,NULL,NULL,NULL,NULL),(1136,'张燕',NULL,0,1,NULL,'2019-05-19 14:37:55',NULL,NULL,NULL,NULL,NULL),(1137,'赵美丽',NULL,0,1,NULL,'2019-05-19 14:43:58',NULL,NULL,NULL,NULL,NULL),(1138,'翁家宝',NULL,0,1,NULL,'2019-05-19 14:44:37',NULL,NULL,NULL,NULL,NULL),(1139,'王琼英',NULL,0,1,NULL,'2019-05-19 15:42:39',NULL,NULL,NULL,NULL,NULL),(1140,'彭碧珍',NULL,0,1,NULL,'2019-05-19 15:44:09',NULL,NULL,NULL,NULL,NULL),(1141,'翁凌燕',NULL,0,1,NULL,'2019-05-19 15:44:42',NULL,NULL,NULL,NULL,NULL),(1142,'翁腾飞',NULL,0,1,NULL,'2019-05-19 15:45:35',NULL,NULL,NULL,NULL,NULL),(1143,'唐秀清',NULL,0,1,NULL,'2019-05-19 15:47:06',NULL,NULL,NULL,NULL,NULL),(1144,'翁清云',NULL,0,1,NULL,'2019-05-19 15:47:39',NULL,NULL,NULL,NULL,NULL),(1145,'翁丽',NULL,0,1,NULL,'2019-05-19 15:47:50',NULL,NULL,NULL,NULL,NULL),(1146,'廖丽琴',NULL,0,1,NULL,'2019-05-19 15:49:26',NULL,NULL,NULL,NULL,NULL),(1147,'翁睿',NULL,0,1,NULL,'2019-05-19 15:50:20',NULL,NULL,NULL,NULL,NULL),(1148,'杨彩芳',NULL,0,1,NULL,'2019-05-19 15:51:37',NULL,NULL,NULL,NULL,NULL),(1149,'彭月英',NULL,0,1,NULL,'2019-05-19 15:53:33',NULL,NULL,NULL,NULL,NULL),(1150,'李小红',NULL,0,1,NULL,'2019-05-19 15:55:05',NULL,NULL,NULL,NULL,NULL),(1151,'翁志远',NULL,0,1,NULL,'2019-05-19 15:55:42',NULL,NULL,NULL,NULL,NULL),(1152,'翁丽萍',NULL,0,1,NULL,'2019-05-19 15:56:15',NULL,NULL,NULL,NULL,NULL),(1153,'徐永珍',NULL,0,1,NULL,'2019-05-19 15:59:57',NULL,NULL,NULL,NULL,NULL),(1154,'唐碧花',NULL,0,1,NULL,'2019-05-19 16:01:33',NULL,NULL,NULL,NULL,NULL),(1155,'翁小伟','15982623955',24,1,NULL,'2019-05-19 16:02:23',NULL,NULL,NULL,NULL,NULL),(1156,'翁小强',NULL,0,1,NULL,'2019-05-19 16:03:06',NULL,NULL,NULL,NULL,NULL),(1157,'王小琴',NULL,0,1,NULL,'2019-05-19 16:04:17',NULL,NULL,NULL,NULL,NULL),(1158,'翁宇轩',NULL,0,1,NULL,'2019-05-19 16:05:12',NULL,NULL,NULL,NULL,NULL),(1159,'邓氏',NULL,0,1,NULL,'2019-05-19 16:06:19',NULL,NULL,NULL,NULL,NULL),(1160,'张氏',NULL,0,1,NULL,'2019-05-19 16:06:55',NULL,NULL,NULL,NULL,NULL),(1161,'陈氏',NULL,0,1,NULL,'2019-05-19 16:07:32',NULL,NULL,NULL,NULL,NULL),(1162,'熊氏',NULL,0,1,NULL,'2019-05-19 16:08:41',NULL,NULL,NULL,NULL,NULL),(1163,'熊氏',NULL,0,1,NULL,'2019-05-19 16:08:44',NULL,NULL,NULL,NULL,NULL),(1164,'张仁英',NULL,0,1,NULL,'2019-05-19 16:10:53',NULL,NULL,NULL,NULL,NULL),(1165,'唐克碧',NULL,0,1,NULL,'2019-05-19 16:12:36',NULL,NULL,NULL,NULL,NULL),(1166,'翁小平',NULL,0,1,NULL,'2019-05-19 16:12:51',NULL,NULL,NULL,NULL,NULL),(1167,'翁万强',NULL,0,1,NULL,'2019-05-19 16:13:09',NULL,NULL,NULL,NULL,NULL),(1168,'彭兰',NULL,0,1,NULL,'2019-05-19 16:15:00',NULL,NULL,NULL,NULL,NULL),(1169,'翁明含',NULL,0,1,NULL,'2019-05-19 16:15:44',NULL,NULL,NULL,NULL,NULL),(1170,'姚敏',NULL,0,1,NULL,'2019-05-19 16:18:01',NULL,NULL,NULL,NULL,NULL),(1171,'翁迷娜',NULL,0,1,NULL,'2019-05-19 16:19:12',NULL,NULL,NULL,NULL,NULL),(1172,'杨守琼',NULL,0,1,NULL,'2019-05-19 16:21:24',NULL,NULL,NULL,NULL,NULL),(1173,'易炳珍',NULL,0,1,NULL,'2019-05-19 16:23:35',NULL,NULL,NULL,NULL,NULL),(1174,'翁志旭',NULL,0,1,NULL,'2019-05-19 16:24:04',NULL,NULL,NULL,NULL,NULL),(1175,'翁志星',NULL,0,1,NULL,'2019-05-19 16:24:22',NULL,NULL,NULL,NULL,NULL),(1176,'苟琼兰',NULL,0,1,NULL,'2019-05-19 16:26:30',NULL,NULL,NULL,NULL,NULL),(1177,'翁志祥',NULL,0,1,NULL,'2019-05-19 16:27:06',NULL,NULL,NULL,NULL,NULL),(1178,'张利君',NULL,0,1,NULL,'2019-05-19 16:29:01',NULL,NULL,NULL,NULL,NULL),(1180,'翁婕',NULL,0,1,NULL,'2019-05-19 16:30:02',NULL,NULL,NULL,NULL,NULL),(1181,'苟国红',NULL,0,1,NULL,'2019-05-19 16:32:10',NULL,NULL,NULL,NULL,NULL),(1182,'翁晨辉',NULL,0,1,NULL,'2019-05-19 16:32:54',NULL,NULL,NULL,NULL,NULL),(1183,'杨',NULL,0,1,NULL,'2019-05-19 16:34:08',NULL,NULL,NULL,NULL,NULL),(1184,'翁悦彤',NULL,0,1,NULL,'2019-05-19 16:34:46',NULL,NULL,NULL,NULL,NULL),(1185,'伏',NULL,0,1,NULL,'2019-05-19 17:01:20',NULL,NULL,NULL,NULL,NULL),(1186,'未详',NULL,0,1,NULL,'2019-05-19 17:02:20',NULL,NULL,NULL,NULL,NULL),(1187,'未详',NULL,0,1,NULL,'2019-05-19 17:02:24',NULL,NULL,NULL,NULL,NULL),(1188,'李氏',NULL,0,1,NULL,'2019-05-19 17:03:48',NULL,NULL,NULL,NULL,NULL),(1189,'邓氏',NULL,0,1,NULL,'2019-05-19 17:06:03',NULL,NULL,NULL,NULL,NULL),(1190,'李氏',NULL,0,1,NULL,'2019-05-19 17:08:35',NULL,NULL,NULL,NULL,NULL),(1191,'王氏',NULL,0,1,NULL,'2019-05-19 17:09:06',NULL,NULL,NULL,NULL,NULL),(1192,'彭氏',NULL,0,1,NULL,'2019-05-19 17:09:33',NULL,NULL,NULL,NULL,NULL),(1193,'彭氏',NULL,0,1,NULL,'2019-05-19 17:09:36',NULL,NULL,NULL,NULL,NULL),(1194,'张氏',NULL,0,1,NULL,'2019-05-19 17:10:28',NULL,NULL,NULL,NULL,NULL),(1195,'唐秀碧',NULL,0,1,NULL,'2019-05-19 17:12:23',NULL,NULL,NULL,NULL,NULL),(1196,'赖永清',NULL,0,1,NULL,'2019-05-19 17:14:25',NULL,NULL,NULL,NULL,NULL),(1197,'钟英秀',NULL,0,1,NULL,'2019-05-19 17:16:04',NULL,NULL,NULL,NULL,NULL),(1198,'翁志平',NULL,0,1,NULL,'2019-05-19 17:16:26',NULL,NULL,NULL,NULL,NULL),(1199,'翁志红',NULL,0,1,NULL,'2019-05-19 17:17:45',NULL,NULL,NULL,NULL,NULL),(1200,'翁攀',NULL,0,1,NULL,'2019-05-19 17:18:10',NULL,NULL,NULL,NULL,NULL),(1201,'邓福英',NULL,0,1,NULL,'2019-05-19 17:19:33',NULL,NULL,NULL,NULL,NULL),(1202,'苟群英',NULL,0,1,NULL,'2019-05-19 17:21:30',NULL,NULL,NULL,NULL,NULL),(1203,'翁彬',NULL,0,1,NULL,'2019-05-19 17:21:46',NULL,NULL,NULL,NULL,NULL),(1204,'江桂花',NULL,0,1,NULL,'2019-05-19 17:22:53',NULL,NULL,NULL,NULL,NULL),(1205,'翁帅',NULL,0,1,NULL,'2019-05-19 17:23:27',NULL,NULL,NULL,NULL,NULL),(1206,'张素华',NULL,0,1,NULL,'2019-05-19 17:26:26',NULL,NULL,NULL,NULL,NULL),(1207,'王秀英',NULL,0,1,NULL,'2019-05-19 17:27:48',NULL,NULL,NULL,NULL,NULL),(1208,'翁军民',NULL,0,1,NULL,'2019-05-19 17:28:12',NULL,NULL,NULL,NULL,NULL),(1209,'翁海英','13408565627',0,1,NULL,'2019-05-19 17:28:28',NULL,NULL,NULL,NULL,NULL),(1210,'周琼兰',NULL,0,1,NULL,'2019-05-19 17:30:27',NULL,NULL,NULL,NULL,NULL),(1211,'翁小菊',NULL,0,1,NULL,'2019-05-19 17:30:57',NULL,NULL,NULL,NULL,NULL),(1212,'翁团结',NULL,0,1,NULL,'2019-05-19 17:31:12',NULL,NULL,NULL,NULL,NULL),(1213,'黄微',NULL,0,1,NULL,'2019-05-19 17:32:48',NULL,NULL,NULL,NULL,NULL),(1214,'翁晟睿',NULL,0,1,NULL,'2019-05-19 17:33:33',NULL,NULL,NULL,NULL,NULL),(1215,'翁悦',NULL,0,1,NULL,'2019-05-19 17:34:14',NULL,NULL,NULL,NULL,NULL),(1216,'翁韵谣',NULL,0,1,NULL,'2019-05-19 17:35:06',NULL,NULL,NULL,NULL,NULL),(1217,'龙思英',NULL,0,1,NULL,'2019-05-19 17:37:49',NULL,NULL,NULL,NULL,NULL),(1218,'胡友珍',NULL,0,1,NULL,'2019-05-19 17:39:10',NULL,NULL,NULL,NULL,NULL),(1219,'虎明珍',NULL,0,1,NULL,'2019-05-19 17:41:40',NULL,NULL,NULL,NULL,NULL),(1220,'虎明珍',NULL,0,1,NULL,'2019-05-19 17:42:00',NULL,NULL,NULL,NULL,NULL),(1221,'虎明珍',NULL,0,1,NULL,'2019-05-19 17:42:19',NULL,NULL,NULL,NULL,NULL),(1222,'翁小平',NULL,0,1,NULL,'2019-05-19 17:43:00',NULL,NULL,NULL,NULL,NULL),(1223,'翁金涛','18611992250',0,1,NULL,'2019-05-19 17:43:38',NULL,NULL,NULL,NULL,NULL),(1224,'翁彪',NULL,0,1,NULL,'2019-05-19 17:44:20',NULL,NULL,NULL,NULL,NULL),(1225,'王家敏',NULL,0,1,NULL,'2019-05-19 17:45:44',NULL,NULL,NULL,NULL,NULL),(1226,'翁素含',NULL,0,1,NULL,'2019-05-19 17:46:27',NULL,NULL,NULL,NULL,NULL),(1227,'翁俊灵',NULL,0,1,NULL,'2019-05-19 17:46:56',NULL,NULL,NULL,NULL,NULL),(1228,'林永珍',NULL,0,1,NULL,'2019-05-20 13:00:51',NULL,NULL,NULL,NULL,NULL),(1229,'邓科艳',NULL,0,1,NULL,'2019-05-20 13:02:41',NULL,NULL,NULL,NULL,NULL),(1230,'翁晨曦',NULL,0,1,NULL,'2019-05-20 13:04:08',NULL,NULL,NULL,NULL,NULL),(1231,'戴建华',NULL,0,1,NULL,'2019-05-20 13:06:32',NULL,NULL,NULL,NULL,NULL),(1232,'翁晨程',NULL,0,1,NULL,'2019-05-20 13:07:08',NULL,NULL,NULL,NULL,NULL),(1233,'翁晨轩',NULL,0,1,NULL,'2019-05-20 13:08:00',NULL,NULL,NULL,NULL,NULL),(1234,'未详',NULL,0,1,NULL,'2019-05-20 13:11:24',NULL,NULL,NULL,NULL,NULL),(1235,'王氏',NULL,0,1,NULL,'2019-05-20 13:14:13',NULL,NULL,NULL,NULL,NULL),(1236,'黄氏',NULL,0,1,NULL,'2019-05-20 13:15:08',NULL,NULL,NULL,NULL,NULL),(1237,'熊氏',NULL,0,1,NULL,'2019-05-20 13:15:42',NULL,NULL,NULL,NULL,NULL),(1238,'杨世珍',NULL,0,1,NULL,'2019-05-20 13:17:43',NULL,NULL,NULL,NULL,NULL),(1239,'张氏',NULL,0,1,NULL,'2019-05-20 13:18:37',NULL,NULL,NULL,NULL,NULL),(1240,'王碧珍',NULL,0,1,NULL,'2019-05-20 13:24:24',NULL,NULL,NULL,NULL,NULL),(1241,'李菊珍',NULL,0,1,NULL,'2019-05-20 13:26:18',NULL,NULL,NULL,NULL,NULL),(1242,'张红波',NULL,0,1,NULL,'2019-05-20 13:28:09',NULL,NULL,NULL,NULL,NULL),(1243,'翁梁景',NULL,0,1,NULL,'2019-05-20 13:29:16',NULL,NULL,NULL,NULL,NULL),(1244,'翁洁洋',NULL,0,1,NULL,'2019-05-20 13:30:56',NULL,NULL,NULL,NULL,NULL),(1245,'翁定崧',NULL,0,1,NULL,'2019-05-20 13:32:42',NULL,NULL,NULL,NULL,NULL),(1246,'翁丹',NULL,0,1,NULL,'2019-05-20 13:33:08',NULL,NULL,NULL,NULL,NULL),(1247,'邓素珍',NULL,0,1,NULL,'2019-05-20 13:34:15',NULL,NULL,NULL,NULL,NULL),(1248,'郑琼珍',NULL,0,1,NULL,'2019-05-20 13:35:37',NULL,NULL,NULL,NULL,NULL),(1249,'纪国珍',NULL,0,1,NULL,'2019-05-20 13:37:30',NULL,NULL,NULL,NULL,NULL),(1250,'翁红珍',NULL,0,1,NULL,'2019-05-20 13:37:59',NULL,NULL,NULL,NULL,NULL),(1251,'翁红兵',NULL,0,1,NULL,'2019-05-20 13:38:28',NULL,NULL,NULL,NULL,NULL),(1252,'薛玉凤',NULL,0,1,NULL,'2019-05-20 13:39:13',NULL,NULL,NULL,NULL,NULL),(1253,'翁显浩',NULL,0,1,NULL,'2019-05-20 13:39:52',NULL,NULL,NULL,NULL,NULL),(1254,'李氏',NULL,0,1,NULL,'2019-05-20 13:42:14',NULL,NULL,NULL,NULL,NULL),(1255,'郑琼花',NULL,0,1,NULL,'2019-05-20 14:02:02',NULL,NULL,NULL,NULL,NULL),(1256,'翁志豪',NULL,0,1,NULL,'2019-05-20 14:03:04',NULL,NULL,NULL,NULL,NULL),(1257,'翁丽珍',NULL,0,1,NULL,'2019-05-20 14:03:23',NULL,NULL,NULL,NULL,NULL),(1258,'翁志辉','13059328175',25,1,NULL,'2019-05-20 14:05:00',NULL,NULL,'2019-06-11 12:36:03','2019-06-11 12:36:03',NULL),(1259,'罗玉慧',NULL,0,1,NULL,'2019-05-20 14:08:02',NULL,NULL,NULL,NULL,NULL),(1260,'周欢',NULL,0,1,NULL,'2019-05-20 14:09:59',NULL,NULL,NULL,NULL,NULL),(1261,'翁显成',NULL,0,1,NULL,'2019-05-20 14:10:35',NULL,NULL,NULL,NULL,NULL),(1262,'翁文兴',NULL,0,1,NULL,'2019-05-20 14:12:26',NULL,NULL,NULL,NULL,NULL),(1263,'张冬梅','18582275208',0,1,NULL,'2019-05-20 14:36:30',NULL,NULL,NULL,NULL,NULL),(1264,'翁晨晨',NULL,0,1,NULL,'2019-05-20 14:37:30',NULL,NULL,NULL,NULL,NULL),(1265,'罗素珍',NULL,0,1,NULL,'2019-05-20 15:26:04',NULL,NULL,NULL,NULL,NULL),(1266,'寇碧华',NULL,0,1,NULL,'2019-05-20 15:28:46',NULL,NULL,NULL,NULL,NULL),(1267,'翁云',NULL,0,1,NULL,'2019-05-20 15:32:37',NULL,NULL,NULL,NULL,NULL),(1268,'王瑶',NULL,0,1,NULL,'2019-05-20 15:33:47',NULL,NULL,NULL,NULL,NULL),(1269,'彭超群',NULL,0,1,NULL,'2019-05-20 15:35:23',NULL,NULL,NULL,NULL,NULL),(1270,'翁禹','15983756119',0,1,NULL,'2019-05-20 15:35:50',NULL,NULL,NULL,NULL,NULL),(1271,'翁梅琳',NULL,0,1,NULL,'2019-05-20 15:36:10',NULL,NULL,NULL,NULL,NULL),(1272,'王月英',NULL,0,1,NULL,'2019-05-20 15:38:16',NULL,NULL,NULL,NULL,NULL),(1273,'翁林',NULL,0,1,NULL,'2019-05-20 15:39:07',NULL,NULL,NULL,NULL,NULL),(1274,'翁敏',NULL,0,1,NULL,'2019-05-20 15:39:20',NULL,NULL,NULL,NULL,NULL),(1275,'魏小丽',NULL,0,1,NULL,'2019-05-20 15:40:52',NULL,NULL,NULL,NULL,NULL),(1276,'翁晨希',NULL,0,1,NULL,'2019-05-20 15:45:12',NULL,NULL,NULL,NULL,NULL),(1277,'翁艺凡',NULL,0,1,NULL,'2019-05-20 15:45:48',NULL,NULL,NULL,NULL,NULL),(1278,'张寇楠',NULL,0,1,NULL,'2019-05-20 15:47:46',NULL,NULL,NULL,NULL,NULL),(1279,'施帮华',NULL,0,1,NULL,'2019-05-20 15:49:55',NULL,NULL,NULL,NULL,NULL),(1280,'未详',NULL,0,1,NULL,'2019-05-20 15:53:00',NULL,NULL,NULL,NULL,NULL),(1281,'未详',NULL,0,1,NULL,'2019-05-20 15:53:52',NULL,NULL,NULL,NULL,NULL),(1282,'李氏',NULL,0,1,NULL,'2019-05-20 15:54:42',NULL,NULL,NULL,NULL,NULL),(1283,'未详',NULL,0,1,NULL,'2019-05-20 15:55:31',NULL,NULL,NULL,NULL,NULL),(1284,'满氏',NULL,0,1,NULL,'2019-05-20 15:56:33',NULL,NULL,NULL,NULL,NULL),(1285,'张兴珍',NULL,0,1,NULL,'2019-05-20 16:00:18',NULL,NULL,NULL,NULL,NULL),(1286,'将氏',NULL,0,1,NULL,'2019-05-20 16:02:07',NULL,NULL,NULL,NULL,NULL),(1287,'翁定云',NULL,0,1,NULL,'2019-05-20 16:04:27',NULL,NULL,NULL,NULL,NULL),(1288,'翁定华',NULL,0,1,NULL,'2019-05-20 16:04:37',NULL,NULL,NULL,NULL,NULL),(1289,'翁定财',NULL,0,1,NULL,'2019-05-20 16:04:46',NULL,NULL,NULL,NULL,NULL),(1290,'未详',NULL,0,1,NULL,'2019-05-20 16:06:01',NULL,NULL,NULL,NULL,NULL),(1291,'杨玉琼',NULL,0,1,NULL,'2019-05-20 16:07:34',NULL,NULL,NULL,NULL,NULL),(1292,'翁显天',NULL,0,1,NULL,'2019-05-20 16:08:04',NULL,NULL,NULL,NULL,NULL),(1293,'翁显明',NULL,0,1,NULL,'2019-05-20 16:08:23',NULL,NULL,NULL,NULL,NULL),(1294,'梁素英',NULL,0,1,NULL,'2019-05-20 16:09:49',NULL,NULL,NULL,NULL,NULL),(1295,'彭登秀',NULL,0,1,NULL,'2019-05-20 16:21:09',NULL,NULL,NULL,NULL,NULL),(1296,'翁志平',NULL,0,1,NULL,'2019-05-20 16:21:29',NULL,NULL,NULL,NULL,NULL),(1297,'翁志华',NULL,0,1,NULL,'2019-05-20 16:21:57',NULL,NULL,NULL,NULL,NULL),(1298,'翁志珍',NULL,0,1,NULL,'2019-05-20 16:22:49',NULL,NULL,NULL,NULL,NULL),(1299,'方淑珍',NULL,0,1,NULL,'2019-05-20 16:24:29',NULL,NULL,NULL,NULL,NULL),(1300,'翁小梅',NULL,0,1,NULL,'2019-05-20 16:24:53',NULL,NULL,NULL,NULL,NULL),(1301,'翁芳',NULL,0,1,NULL,'2019-05-20 16:25:25',NULL,NULL,NULL,NULL,NULL),(1302,'翁鹏程',NULL,0,1,NULL,'2019-05-20 16:25:54',NULL,NULL,NULL,NULL,NULL),(1303,'翁洋',NULL,0,1,NULL,'2019-05-20 16:26:11',NULL,NULL,NULL,NULL,NULL),(1304,'翁小丽',NULL,0,1,NULL,'2019-05-20 16:26:27',NULL,NULL,NULL,NULL,NULL),(1305,'翁明悦',NULL,0,1,NULL,'2019-05-20 16:27:27',NULL,NULL,NULL,NULL,NULL),(1306,'翁明羽',NULL,0,1,NULL,'2019-05-20 16:27:58',NULL,NULL,NULL,NULL,NULL),(1307,'翁乐尔',NULL,0,1,NULL,'2019-05-20 16:28:31',NULL,NULL,NULL,NULL,NULL),(1308,'张旭珍',NULL,0,1,NULL,'2019-05-20 16:29:07',NULL,NULL,NULL,NULL,NULL),(1309,'邓秀娟',NULL,0,1,NULL,'2019-05-20 16:29:51',NULL,NULL,NULL,NULL,NULL),(1310,'杨娟',NULL,0,1,NULL,'2019-05-20 16:30:17',NULL,NULL,NULL,NULL,NULL),(1311,'张素华',NULL,0,1,NULL,'2019-05-20 16:33:59',NULL,NULL,NULL,NULL,NULL),(1312,'翁小毛','18980698099',0,1,NULL,'2019-05-20 16:36:50',NULL,NULL,NULL,NULL,NULL),(1313,'邓存碧',NULL,0,1,NULL,'2019-05-20 16:37:33',NULL,NULL,NULL,NULL,NULL),(1314,'周全珍',NULL,0,1,NULL,'2019-05-20 16:39:41',NULL,NULL,NULL,NULL,NULL),(1315,'翁素碧',NULL,0,1,NULL,'2019-05-20 16:40:35',NULL,NULL,NULL,NULL,NULL),(1316,'翁定珍',NULL,0,1,NULL,'2019-05-20 16:41:00',NULL,NULL,NULL,NULL,NULL),(1317,'袁春',NULL,0,1,NULL,'2019-05-20 16:42:27',NULL,NULL,NULL,NULL,NULL),(1318,'翁倩',NULL,0,1,NULL,'2019-05-20 16:42:47',NULL,NULL,NULL,NULL,NULL),(1319,'李灵芝',NULL,0,1,NULL,'2019-05-20 16:45:09',NULL,NULL,NULL,NULL,NULL),(1320,'翁少天',NULL,0,1,NULL,'2019-05-20 16:45:38',NULL,NULL,NULL,NULL,NULL),(1321,'翁呈贤',NULL,0,1,NULL,'2019-05-20 16:46:20',NULL,NULL,NULL,NULL,NULL),(1322,'满氏',NULL,0,1,NULL,'2019-05-20 16:48:55',NULL,NULL,NULL,NULL,NULL),(1323,'张贵珍',NULL,0,1,NULL,'2019-05-20 16:57:02',NULL,NULL,NULL,NULL,NULL),(1324,'翁定和',NULL,0,1,NULL,'2019-05-20 16:57:29',NULL,NULL,NULL,NULL,NULL),(1325,'翁定元',NULL,0,1,NULL,'2019-05-20 16:58:19',NULL,NULL,NULL,NULL,NULL),(1326,'李文珍',NULL,0,1,NULL,'2019-05-20 16:59:37',NULL,NULL,NULL,NULL,NULL),(1327,'翁显敏',NULL,0,1,NULL,'2019-05-20 16:59:57',NULL,NULL,NULL,NULL,NULL),(1328,'郑素华',NULL,0,1,NULL,'2019-05-20 17:01:42',NULL,NULL,NULL,NULL,NULL),(1329,'翁显杰',NULL,0,1,NULL,'2019-05-20 17:02:14',NULL,NULL,NULL,NULL,NULL),(1330,'翁显奎',NULL,0,1,NULL,'2019-05-20 17:02:43',NULL,NULL,NULL,NULL,NULL),(1331,'唐光清',NULL,0,1,NULL,'2019-05-20 17:05:14',NULL,NULL,NULL,NULL,NULL),(1332,'翁定辉',NULL,0,1,NULL,'2019-05-20 17:05:35',NULL,NULL,NULL,NULL,NULL),(1333,'翁定忠',NULL,0,1,NULL,'2019-05-20 17:05:48',NULL,NULL,NULL,NULL,NULL),(1334,'翁定刚',NULL,0,1,NULL,'2019-05-20 17:06:26',NULL,NULL,NULL,NULL,NULL),(1335,'漆秀清',NULL,0,1,NULL,'2019-05-20 17:08:21',NULL,NULL,NULL,NULL,NULL),(1336,'李世容',NULL,0,1,NULL,'2019-05-20 17:10:13',NULL,NULL,NULL,NULL,NULL),(1337,'翁显林',NULL,0,1,NULL,'2019-05-20 17:10:54',NULL,NULL,NULL,NULL,NULL),(1338,'翁显军',NULL,0,1,NULL,'2019-05-20 17:11:04',NULL,NULL,NULL,NULL,NULL),(1339,'翁显虎',NULL,0,1,NULL,'2019-05-20 17:12:11',NULL,NULL,NULL,NULL,NULL),(1340,'谢薪清',NULL,0,1,NULL,'2019-05-20 17:14:29',NULL,NULL,NULL,NULL,NULL),(1341,'翁显登',NULL,0,1,NULL,'2019-05-20 17:14:45',NULL,NULL,NULL,NULL,NULL),(1342,'张贵珍',NULL,0,1,NULL,'2019-05-20 17:16:47',NULL,NULL,NULL,NULL,NULL),(1343,'翁定国',NULL,0,1,NULL,'2019-05-20 17:16:58',NULL,NULL,NULL,NULL,NULL),(1344,'翁定明',NULL,0,1,NULL,'2019-05-20 17:17:08',NULL,NULL,NULL,NULL,NULL),(1345,'翁定学',NULL,0,1,NULL,'2019-05-20 17:17:19',NULL,NULL,NULL,NULL,NULL),(1346,'李碧珍',NULL,0,1,NULL,'2019-05-20 17:19:01',NULL,NULL,NULL,NULL,NULL),(1347,'翁定斌',NULL,0,1,NULL,'2019-05-20 17:20:08',NULL,NULL,NULL,NULL,NULL),(1348,'张贤容',NULL,0,1,NULL,'2019-05-20 17:21:55',NULL,NULL,NULL,NULL,NULL),(1349,'翁显奇',NULL,0,1,NULL,'2019-05-20 17:22:07',NULL,NULL,NULL,NULL,NULL),(1350,'龙关碧',NULL,0,1,NULL,'2019-05-20 17:23:21',NULL,NULL,NULL,NULL,NULL),(1351,'龙德碧',NULL,0,1,NULL,'2019-05-20 17:25:26',NULL,NULL,NULL,NULL,NULL),(1352,'翁定勇',NULL,0,1,NULL,'2019-05-20 17:25:51',NULL,NULL,NULL,NULL,NULL),(1353,'陈琼容',NULL,0,1,NULL,'2019-05-20 17:27:40',NULL,NULL,NULL,NULL,NULL),(1354,'翁丽霞',NULL,0,1,NULL,'2019-05-20 17:27:58',NULL,NULL,NULL,NULL,NULL),(1355,'翁广容',NULL,0,1,NULL,'2019-05-20 17:28:14',NULL,NULL,NULL,NULL,NULL),(1356,'唐氏',NULL,0,1,NULL,'2019-05-20 17:29:25',NULL,NULL,NULL,NULL,NULL),(1358,'翁明珍',NULL,0,1,NULL,'2019-05-20 22:49:43',NULL,NULL,NULL,NULL,NULL),(1359,'饶顺生',NULL,0,1,NULL,'2019-05-20 22:52:03',NULL,NULL,NULL,NULL,NULL),(1360,'翁杨致远',NULL,0,1,NULL,'2019-05-20 22:52:57',NULL,NULL,NULL,NULL,NULL),(1361,'罗彦惜',NULL,0,1,NULL,'2019-05-20 22:55:24',NULL,NULL,NULL,NULL,NULL),(1362,'翁梓谦',NULL,0,1,NULL,'2019-05-20 22:57:16',NULL,NULL,NULL,NULL,NULL),(1363,'唐氏',NULL,0,1,NULL,'2019-05-20 23:11:50',NULL,NULL,NULL,NULL,NULL),(1364,'翁定超',NULL,0,1,NULL,'2019-05-20 23:12:26',NULL,NULL,NULL,NULL,NULL),(1365,'翁定国',NULL,0,1,NULL,'2019-05-20 23:12:48',NULL,NULL,NULL,NULL,NULL),(1366,'满琼花',NULL,0,1,NULL,'2019-05-20 23:14:56',NULL,NULL,NULL,NULL,NULL),(1367,'翁定元',NULL,0,1,NULL,'2019-05-20 23:16:23',NULL,NULL,NULL,NULL,NULL),(1368,'唐碧容',NULL,0,1,NULL,'2019-05-20 23:17:12',NULL,NULL,NULL,NULL,NULL),(1369,'翁小龙',NULL,0,1,NULL,'2019-05-20 23:17:42',NULL,NULL,NULL,NULL,NULL),(1370,'翁小华',NULL,0,1,NULL,'2019-05-20 23:18:08',NULL,NULL,NULL,NULL,NULL),(1371,'邓仁清',NULL,0,1,NULL,'2019-05-20 23:20:03',NULL,NULL,NULL,NULL,NULL),(1372,'翁显林',NULL,0,1,NULL,'2019-05-20 23:20:24',NULL,NULL,NULL,NULL,NULL),(1373,'翁显浩',NULL,0,1,NULL,'2019-05-20 23:21:01',NULL,NULL,NULL,NULL,NULL),(1374,'魏兰',NULL,0,1,NULL,'2019-05-20 23:23:12',NULL,NULL,NULL,NULL,NULL),(1375,'许玉春',NULL,0,1,NULL,'2019-05-20 23:23:50',NULL,NULL,NULL,NULL,NULL),(1376,'李桂琼',NULL,0,1,NULL,'2019-05-20 23:25:46',NULL,NULL,NULL,NULL,NULL),(1377,'鲁燕',NULL,0,1,NULL,'2019-05-20 23:26:47',NULL,NULL,NULL,NULL,NULL),(1378,'彭德贞',NULL,0,1,NULL,'2019-05-20 23:29:16',NULL,NULL,NULL,NULL,NULL),(1379,'翁应科',NULL,0,1,NULL,'2019-05-20 23:29:54',NULL,NULL,NULL,NULL,NULL),(1380,'曹流珍',NULL,0,1,NULL,'2019-05-20 23:31:53',NULL,NULL,NULL,NULL,NULL),(1382,'翁志勇',NULL,0,1,NULL,'2019-05-20 23:33:28',NULL,NULL,NULL,NULL,NULL),(1383,'翁志云','13653087854',0,1,NULL,'2019-05-20 23:33:37',NULL,NULL,NULL,NULL,NULL),(1384,'曹玉琼',NULL,0,1,NULL,'2019-05-20 23:34:39',NULL,NULL,NULL,NULL,NULL),(1385,'苟万菊',NULL,0,1,NULL,'2019-05-20 23:37:54',NULL,NULL,NULL,NULL,NULL),(1386,'翁臻',NULL,0,1,NULL,'2019-05-20 23:38:48',NULL,NULL,NULL,NULL,NULL),(1387,'翁俊',NULL,0,1,NULL,'2019-05-20 23:39:55',NULL,NULL,NULL,NULL,NULL),(1388,'翁俊杰','18318411630',0,1,NULL,'2019-05-20 23:40:37',NULL,NULL,NULL,NULL,NULL),(1389,'翁瑄遥',NULL,0,1,NULL,'2019-05-20 23:41:34',NULL,NULL,NULL,NULL,NULL),(1390,'漆俊辉',NULL,0,1,NULL,'2019-05-20 23:42:50',NULL,NULL,NULL,NULL,NULL),(1391,'唐氏',NULL,0,1,NULL,'2019-05-20 23:45:08',NULL,NULL,NULL,NULL,NULL),(1392,'张氏',NULL,0,1,NULL,'2019-05-20 23:45:44',NULL,NULL,NULL,NULL,NULL),(1393,'老孝珍',NULL,0,1,NULL,'2019-05-20 23:46:57',NULL,NULL,NULL,NULL,NULL),(1394,'翁琼英',NULL,0,1,NULL,'2019-05-20 23:48:18',NULL,NULL,NULL,NULL,NULL),(1395,'张氏',NULL,0,1,NULL,'2019-05-20 23:49:05',NULL,NULL,NULL,NULL,NULL),(1396,'翁德培',NULL,0,1,NULL,'2019-05-20 23:52:38',NULL,NULL,NULL,NULL,NULL),(1397,'张桂珍',NULL,0,1,NULL,'2019-05-20 23:54:20',NULL,NULL,NULL,NULL,NULL),(1398,'张菊珍',NULL,0,1,NULL,'2019-05-20 23:56:35',NULL,NULL,NULL,NULL,NULL),(1399,'余小兰',NULL,0,1,NULL,'2019-05-20 23:59:52',NULL,NULL,NULL,NULL,NULL),(1400,'翁定凯',NULL,0,1,NULL,'2019-05-21 00:00:22',NULL,NULL,NULL,NULL,NULL),(1401,'翁娱',NULL,0,1,NULL,'2019-05-21 00:01:09',NULL,NULL,NULL,NULL,NULL),(1402,'余小梅',NULL,0,1,NULL,'2019-05-21 00:02:43',NULL,NULL,NULL,NULL,NULL),(1403,'翁定浩',NULL,0,1,NULL,'2019-05-21 00:03:43',NULL,NULL,NULL,NULL,NULL),(1404,'翁欣悦',NULL,0,1,NULL,'2019-05-21 00:04:24',NULL,NULL,NULL,NULL,NULL),(1405,'李培秀',NULL,0,1,NULL,'2019-05-21 09:20:52',NULL,NULL,NULL,NULL,NULL),(1406,'唐群花',NULL,0,1,NULL,'2019-05-21 09:36:19',NULL,NULL,NULL,NULL,NULL),(1407,'王兰美',NULL,0,1,NULL,'2019-05-21 09:37:55',NULL,NULL,NULL,NULL,NULL),(1408,'翁虎',NULL,0,1,NULL,'2019-05-21 09:38:24',NULL,NULL,NULL,NULL,NULL),(1409,'翁雪梅',NULL,0,1,NULL,'2019-05-21 09:38:36',NULL,NULL,NULL,NULL,NULL),(1410,'肖氏',NULL,0,1,NULL,'2019-05-21 09:39:41',NULL,NULL,NULL,NULL,NULL),(1411,'未详',NULL,0,1,NULL,'2019-05-21 09:41:18',NULL,NULL,NULL,NULL,NULL),(1412,'唐氏',NULL,0,1,NULL,'2019-05-21 09:42:02',NULL,NULL,NULL,NULL,NULL),(1413,'唐以秀',NULL,0,1,NULL,'2019-05-21 09:44:58',NULL,NULL,NULL,NULL,NULL),(1414,'翁应碧',NULL,0,1,NULL,'2019-05-21 09:45:17',NULL,NULL,NULL,NULL,NULL),(1415,'邓氏',NULL,0,1,NULL,'2019-05-21 09:46:17',NULL,NULL,NULL,NULL,NULL),(1416,'翁应良',NULL,0,1,NULL,'2019-05-21 09:46:58',NULL,NULL,NULL,NULL,NULL),(1417,'龚玉碧',NULL,0,1,NULL,'2019-05-21 09:48:41',NULL,NULL,NULL,NULL,NULL),(1418,'翁智谦','15528858269',0,1,NULL,'2019-05-21 09:49:05',NULL,NULL,'2019-06-10 21:52:50','2019-06-10 21:52:50',NULL),(1419,'唐以秀',NULL,0,1,NULL,'2019-05-21 09:51:31',NULL,NULL,NULL,NULL,NULL),(1420,'翁涛','15102878560',29,1,NULL,'2019-05-21 09:53:07',NULL,NULL,NULL,NULL,NULL),(1421,'郑仕菊',NULL,0,1,NULL,'2019-05-21 09:54:44',NULL,NULL,NULL,NULL,NULL),(1422,'翁思维',NULL,0,1,NULL,'2019-05-21 09:55:00',NULL,NULL,NULL,NULL,NULL),(1423,'唐兰',NULL,0,1,NULL,'2019-05-21 09:57:27',NULL,NULL,NULL,NULL,NULL),(1424,'翁梓羚',NULL,0,1,NULL,'2019-05-21 09:58:35',NULL,NULL,NULL,NULL,NULL),(1425,'翁清悦',NULL,0,1,NULL,'2019-05-21 09:58:58',NULL,NULL,NULL,NULL,NULL),(1426,'周双梅',NULL,0,1,NULL,'2019-05-21 10:00:08',NULL,NULL,NULL,NULL,NULL),(1427,'翁震博',NULL,0,1,NULL,'2019-05-21 10:00:46',NULL,NULL,NULL,NULL,NULL),(1428,'许氏',NULL,0,1,NULL,'2019-05-21 10:02:45',NULL,NULL,NULL,NULL,NULL),(1429,'袁秀碧',NULL,0,1,NULL,'2019-05-21 10:04:50',NULL,NULL,NULL,NULL,NULL),(1430,'何淑秀',NULL,0,1,NULL,'2019-05-21 10:06:31',NULL,NULL,NULL,NULL,NULL),(1431,'翁茂文',NULL,0,1,NULL,'2019-05-21 10:08:13',NULL,NULL,NULL,NULL,NULL),(1432,'翁荣胜',NULL,0,1,NULL,'2019-05-21 10:08:25',NULL,NULL,NULL,NULL,NULL),(1433,'翁茂平',NULL,0,1,NULL,'2019-05-21 10:08:46',NULL,NULL,NULL,NULL,NULL),(1434,'孙海琼',NULL,0,1,NULL,'2019-05-21 10:10:26',NULL,NULL,NULL,NULL,NULL),(1435,'翁超',NULL,0,1,NULL,'2019-05-21 10:10:45',NULL,NULL,NULL,NULL,NULL),(1436,'王文*',NULL,0,1,NULL,'2019-05-21 10:12:40',NULL,NULL,NULL,NULL,NULL),(1437,'翁添天',NULL,0,1,NULL,'2019-05-21 10:13:28',NULL,NULL,NULL,NULL,NULL),(1438,'张秀杨',NULL,0,1,NULL,'2019-05-21 10:14:44',NULL,NULL,NULL,NULL,NULL),(1439,'翁雨晨',NULL,0,1,NULL,'2019-05-21 10:15:43',NULL,NULL,NULL,NULL,NULL),(1440,'王在先',NULL,0,1,NULL,'2019-05-21 10:18:08',NULL,NULL,NULL,NULL,NULL),(1441,'翁果',NULL,0,1,NULL,'2019-05-21 10:18:48',NULL,NULL,NULL,NULL,NULL),(1442,'翁凌薇',NULL,0,1,NULL,'2019-05-21 10:19:28',NULL,NULL,NULL,NULL,NULL),(1443,'朱国明',NULL,0,1,NULL,'2019-05-21 10:23:57',NULL,NULL,NULL,NULL,NULL),(1444,'李学珍',NULL,0,1,NULL,'2019-05-21 10:25:28',NULL,NULL,NULL,NULL,NULL),(1445,'翁诚',NULL,0,1,NULL,'2019-05-21 10:25:57',NULL,NULL,NULL,NULL,NULL),(1446,'翁涛',NULL,0,1,NULL,'2019-05-21 10:26:26',NULL,NULL,NULL,NULL,NULL),(1447,'高松',NULL,0,1,NULL,'2019-05-21 10:28:02',NULL,NULL,NULL,NULL,NULL),(1448,'翁志成',NULL,0,1,NULL,'2019-05-21 10:28:46',NULL,NULL,NULL,NULL,NULL),(1449,'邓昌容',NULL,0,1,NULL,'2019-05-21 10:30:10',NULL,NULL,NULL,NULL,NULL),(1450,'何春花',NULL,0,1,NULL,'2019-05-21 10:31:58',NULL,NULL,NULL,NULL,NULL),(1451,'翁应权',NULL,0,1,NULL,'2019-05-21 12:35:30',NULL,NULL,NULL,NULL,NULL),(1452,'魏碧珍',NULL,0,1,NULL,'2019-05-21 12:37:34',NULL,NULL,NULL,NULL,NULL),(1453,'翁应骞',NULL,0,1,NULL,'2019-05-21 12:38:50',NULL,NULL,NULL,NULL,NULL),(1454,'翁应碧',NULL,0,1,NULL,'2019-05-21 12:39:40',NULL,NULL,NULL,NULL,NULL),(1455,'翁应琼',NULL,0,1,NULL,'2019-05-21 12:41:38',NULL,NULL,NULL,NULL,NULL),(1456,'穆州',NULL,0,1,NULL,'2019-05-21 12:44:54',NULL,NULL,NULL,NULL,NULL),(1457,'翁志浩',NULL,0,1,NULL,'2019-05-21 12:45:11',NULL,NULL,NULL,NULL,NULL),(1458,'袁清珍',NULL,0,1,NULL,'2019-05-21 12:48:01',NULL,NULL,NULL,NULL,NULL),(1459,'翁应平',NULL,0,1,NULL,'2019-05-21 12:49:08',NULL,NULL,NULL,NULL,NULL),(1460,'翁华平',NULL,0,1,NULL,'2019-05-21 12:51:25',NULL,NULL,NULL,NULL,NULL),(1461,'翁碧莲',NULL,0,1,NULL,'2019-05-21 12:52:30',NULL,NULL,NULL,NULL,NULL),(1462,'闵红英',NULL,0,1,NULL,'2019-05-21 12:55:43',NULL,NULL,NULL,NULL,NULL),(1463,'翁玉莲',NULL,0,1,NULL,'2019-05-21 12:56:54',NULL,NULL,NULL,NULL,NULL),(1464,'翁倩',NULL,0,1,NULL,'2019-05-21 12:57:31',NULL,NULL,NULL,NULL,NULL),(1465,'翁海军','1465',0,1,NULL,'2019-05-31 08:30:23',NULL,NULL,NULL,NULL,NULL),(1466,'翁秀花','',0,1,NULL,'2019-05-31 08:31:32',NULL,NULL,NULL,NULL,NULL),(1467,'翁利群','',0,1,NULL,'2019-05-31 08:34:12',NULL,NULL,NULL,NULL,NULL),(1468,'翁红梅','1468',0,1,NULL,'2019-05-31 08:35:07',NULL,NULL,NULL,NULL,NULL),(1469,'翁腊梅','1469',0,1,NULL,'2019-05-31 08:35:45',NULL,NULL,NULL,NULL,NULL),(1470,'翁建珍','1470',0,1,NULL,'2019-05-31 08:37:03',NULL,NULL,NULL,NULL,NULL),(1471,'翁花菊','1471',0,1,NULL,'2019-06-01 08:36:38',NULL,NULL,NULL,NULL,NULL),(1472,'翁靖','1472',0,1,NULL,'2019-06-01 08:41:10',NULL,NULL,NULL,NULL,NULL),(1473,'陈氏','1473',0,1,NULL,'2019-06-05 08:43:57',NULL,NULL,NULL,NULL,NULL),(1474,'熊红英','1474',0,1,NULL,'2019-06-05 08:55:18',NULL,NULL,NULL,NULL,NULL),(1475,'陈红梅','',0,1,NULL,'2019-06-05 10:49:31',NULL,NULL,NULL,NULL,NULL),(1476,'郑氏','1476',0,1,NULL,'2019-06-06 18:48:57',NULL,NULL,NULL,NULL,NULL),(1477,'张氏','1477',0,1,NULL,'2019-06-06 18:49:23',NULL,NULL,NULL,NULL,NULL),(1478,'翁清珍','',0,1,NULL,'2019-06-10 13:17:28',NULL,NULL,NULL,NULL,NULL),(1479,'翁华珍','1479',0,1,NULL,'2019-06-10 21:26:56',NULL,NULL,NULL,NULL,NULL),(1481,'翁玉华','',0,1,NULL,'2019-06-10 21:41:48',NULL,NULL,NULL,NULL,NULL),(1482,'翁桂花','1482',0,1,NULL,'2019-06-10 21:56:44',NULL,NULL,NULL,NULL,NULL),(1483,'翁素群','1483',0,1,NULL,'2019-06-10 21:57:03',NULL,NULL,NULL,NULL,NULL),(1484,'翁秀碧','1484',0,1,NULL,'2019-06-10 21:57:24',NULL,NULL,NULL,NULL,NULL),(1485,'翁渃汐','',0,1,NULL,'2019-06-11 09:35:14',NULL,NULL,NULL,NULL,NULL),(1486,'翁梓萌','',0,1,NULL,'2019-06-11 09:45:16',NULL,NULL,NULL,NULL,NULL),(1487,'翁微','1487',0,1,NULL,'2019-06-11 10:10:11',NULL,NULL,NULL,NULL,NULL),(1488,'翁素花','1488',0,1,NULL,'2019-06-11 10:58:20',NULL,NULL,NULL,NULL,NULL),(1489,'翁海燕','1489',0,1,NULL,'2019-06-11 11:51:00',NULL,NULL,NULL,NULL,NULL),(1490,'翁红英','1490',0,1,NULL,'2019-06-11 11:51:21',NULL,NULL,NULL,NULL,NULL),(1491,'翁应花','',0,1,NULL,'2019-06-11 11:55:56',NULL,NULL,NULL,NULL,NULL),(1492,'翁桂珍','1492',0,1,NULL,'2019-06-11 11:56:24',NULL,NULL,NULL,NULL,NULL),(1493,'翁能珍','1493',0,1,NULL,'2019-06-11 11:57:26',NULL,NULL,NULL,NULL,NULL),(1494,'翁丽梅','',0,1,NULL,'2019-06-11 12:32:12',NULL,NULL,NULL,NULL,NULL),(1495,'翁应琼','',0,1,NULL,'2019-06-11 12:48:24',NULL,NULL,NULL,NULL,NULL),(1497,'翁小燕','1497',0,1,NULL,'2019-06-11 14:48:41',NULL,NULL,NULL,NULL,NULL),(1498,'翁碧花','1498',0,1,NULL,'2019-06-11 15:04:20',NULL,NULL,NULL,NULL,NULL),(1499,'翁碧荣','1499',0,1,NULL,'2019-06-11 15:04:55',NULL,NULL,NULL,NULL,NULL),(1500,'翁琼珍','1500',0,1,NULL,'2019-06-11 15:05:23',NULL,NULL,NULL,NULL,NULL),(1501,'宋亮','1501',0,1,NULL,'2019-06-11 15:15:14',NULL,NULL,NULL,NULL,NULL),(1502,'翁丽华','1502',0,1,NULL,'2019-06-11 15:22:23',NULL,NULL,NULL,NULL,NULL),(1503,'唐露','1503',0,1,NULL,'2019-06-11 17:40:53',NULL,NULL,NULL,NULL,NULL),(1504,'翁志军','',0,1,NULL,'2019-06-12 13:41:54',NULL,NULL,NULL,NULL,NULL),(1505,'翁志林','1505',0,1,NULL,'2019-06-12 17:10:35',NULL,NULL,NULL,NULL,NULL),(1506,'翁逸晨','',0,1,NULL,'2019-06-12 17:11:09',NULL,NULL,NULL,NULL,NULL),(1507,'翁春梅','1507',0,1,NULL,'2019-06-12 17:51:18',NULL,NULL,NULL,NULL,NULL),(1508,'翁朝斌','1508',0,1,NULL,'2019-06-13 16:10:17',NULL,NULL,NULL,NULL,NULL),(1509,'翁朝斐','1509',0,1,NULL,'2019-06-13 16:11:14',NULL,NULL,NULL,NULL,NULL),(1510,'翁天禄','1510',0,1,NULL,'2019-06-13 16:12:39',NULL,NULL,NULL,NULL,NULL),(1511,'翁天增','1511',0,1,NULL,'2019-06-13 16:13:10',NULL,NULL,NULL,NULL,NULL),(1512,'翁学文','1512',0,1,NULL,'2019-06-13 16:21:37',NULL,NULL,NULL,NULL,NULL),(1513,'翁启懋','',0,1,NULL,'2019-06-13 16:23:14',NULL,NULL,NULL,NULL,NULL),(1514,'翁启愈','1514',0,1,NULL,'2019-06-13 16:24:28',NULL,NULL,NULL,NULL,NULL),(1515,'翁启贞','1515',0,1,NULL,'2019-06-13 16:24:47',NULL,NULL,NULL,NULL,NULL),(1516,'翁启聪','1516',0,1,NULL,'2019-06-13 16:25:06',NULL,NULL,NULL,NULL,NULL),(1517,'翁启思','',0,1,NULL,'2019-06-13 16:25:23',NULL,NULL,NULL,NULL,NULL),(1518,'翁光奎','',0,1,NULL,'2019-06-13 16:27:35',NULL,NULL,NULL,NULL,NULL),(1519,'翁光恒','',0,1,NULL,'2019-06-13 16:28:31',NULL,NULL,NULL,NULL,NULL),(1520,'翁光舞','1520',0,1,NULL,'2019-06-13 16:28:51',NULL,NULL,NULL,NULL,NULL),(1521,'翁先明','1521',0,1,NULL,'2019-06-13 16:30:44',NULL,NULL,NULL,NULL,NULL),(1522,'翁先副','',0,1,NULL,'2019-06-13 16:31:35',NULL,NULL,NULL,NULL,NULL),(1523,'翁先貴','',0,1,NULL,'2019-06-13 16:32:49',NULL,NULL,NULL,NULL,NULL),(1524,'翁琴','1524',0,1,NULL,'2019-06-13 22:36:00',NULL,NULL,NULL,NULL,NULL),(1525,'翁群花','1525',0,1,NULL,'2019-06-14 11:43:27',NULL,NULL,NULL,NULL,NULL),(1526,'翁显斐','1526',0,1,NULL,'2019-07-14 13:27:13',NULL,NULL,NULL,NULL,NULL),(1527,'翁定龙','1527',0,1,NULL,'2019-07-14 13:30:38',NULL,NULL,NULL,NULL,NULL),(1528,'翁冬梅','1528',0,1,NULL,'2019-07-14 13:35:39',NULL,NULL,NULL,NULL,NULL),(1529,'翁小梅','1529',0,1,NULL,'2019-07-14 13:36:06',NULL,NULL,NULL,NULL,NULL),(1530,'李静','1530',0,1,NULL,'2019-07-19 11:56:21',NULL,NULL,NULL,NULL,NULL),(1531,'谢氏','',0,1,NULL,'2019-07-22 12:01:36',NULL,NULL,NULL,NULL,NULL),(1532,'陈氏','1532',0,1,NULL,'2019-07-22 13:41:09',NULL,NULL,NULL,NULL,NULL),(1533,'张氏','',0,1,NULL,'2019-07-22 13:43:08',NULL,NULL,NULL,NULL,NULL),(1534,'李氏','',0,1,NULL,'2019-07-22 13:47:59',NULL,NULL,NULL,NULL,NULL),(1535,'翁菊花','',NULL,1,NULL,'2019-07-27 23:08:39',NULL,NULL,NULL,NULL,NULL),(1536,'翁润花','',0,1,NULL,'2019-07-27 23:11:14',NULL,NULL,NULL,NULL,NULL),(1537,'翁翠','1537',NULL,1,NULL,'2019-07-27 23:15:30',NULL,NULL,NULL,NULL,NULL),(1538,'唐月光','1538',0,1,NULL,'2019-07-28 19:15:27',NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `fa_user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_user_district`
--

DROP TABLE IF EXISTS `fa_user_district`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_user_district` (
  `USER_ID` int(11) NOT NULL,
  `DISTRICT_ID` int(11) NOT NULL,
  PRIMARY KEY (`USER_ID`,`DISTRICT_ID`),
  KEY `FK_FA_USER_DISTRICT_REF_DIST` (`DISTRICT_ID`),
  CONSTRAINT `fa_user_district_ibfk_1` FOREIGN KEY (`DISTRICT_ID`) REFERENCES `fa_district` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_user_district_ibfk_2` FOREIGN KEY (`USER_ID`) REFERENCES `fa_user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_user_district`
--

LOCK TABLES `fa_user_district` WRITE;
/*!40000 ALTER TABLE `fa_user_district` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_user_district` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_user_event`
--

DROP TABLE IF EXISTS `fa_user_event`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_user_event` (
  `ID` int(11) NOT NULL,
  `USER_ID` int(11) DEFAULT NULL,
  `NAME` varchar(50) DEFAULT NULL,
  `HAPPEN_TIME` datetime DEFAULT NULL,
  `CONTENT` varchar(500) DEFAULT NULL,
  `ADDRESS` varchar(500) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_FA_USER_EVENT_REF_USER` (`USER_ID`),
  CONSTRAINT `fa_user_event_ibfk_1` FOREIGN KEY (`USER_ID`) REFERENCES `fa_user_info` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_user_event`
--

LOCK TABLES `fa_user_event` WRITE;
/*!40000 ALTER TABLE `fa_user_event` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_user_event` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_user_file`
--

DROP TABLE IF EXISTS `fa_user_file`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_user_file` (
  `USER_ID` int(11) NOT NULL,
  `FILE_ID` int(11) NOT NULL,
  PRIMARY KEY (`USER_ID`,`FILE_ID`),
  KEY `FK_FA_USER_FILE_REF_FILE` (`FILE_ID`),
  CONSTRAINT `fa_user_file_ibfk_1` FOREIGN KEY (`FILE_ID`) REFERENCES `fa_files` (`ID`),
  CONSTRAINT `fa_user_file_ibfk_2` FOREIGN KEY (`USER_ID`) REFERENCES `fa_user` (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_user_file`
--

LOCK TABLES `fa_user_file` WRITE;
/*!40000 ALTER TABLE `fa_user_file` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_user_file` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_user_friend`
--

DROP TABLE IF EXISTS `fa_user_friend`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_user_friend` (
  `USER_ID` int(11) NOT NULL,
  `FRIEND_ID` int(11) NOT NULL,
  PRIMARY KEY (`USER_ID`,`FRIEND_ID`),
  KEY `FK_FA_FRIEND_REF_USER` (`FRIEND_ID`),
  CONSTRAINT `fa_user_friend_ibfk_1` FOREIGN KEY (`FRIEND_ID`) REFERENCES `fa_user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `fa_user_friend_ibfk_2` FOREIGN KEY (`USER_ID`) REFERENCES `fa_user_info` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_user_friend`
--

LOCK TABLES `fa_user_friend` WRITE;
/*!40000 ALTER TABLE `fa_user_friend` DISABLE KEYS */;
/*!40000 ALTER TABLE `fa_user_friend` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_user_info`
--

DROP TABLE IF EXISTS `fa_user_info`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_user_info` (
  `ID` int(11) NOT NULL,
  `LEVEL_ID` int(11) DEFAULT NULL,
  `FAMILY_ID` int(11) DEFAULT NULL,
  `ELDER_ID` int(11) DEFAULT NULL,
  `LEVEL_NAME` varchar(2) DEFAULT NULL,
  `FATHER_ID` int(11) DEFAULT NULL,
  `MOTHER_ID` int(11) DEFAULT NULL,
  `BIRTHDAY_TIME` datetime DEFAULT NULL,
  `BIRTHDAY_PLACE` varchar(500) DEFAULT NULL,
  `IS_LIVE` decimal(1,0) DEFAULT NULL,
  `DIED_TIME` datetime DEFAULT NULL,
  `DIED_PLACE` varchar(500) DEFAULT NULL,
  `REMARK` varchar(500) DEFAULT NULL,
  `SEX` varchar(2) DEFAULT NULL,
  `YEARS_TYPE` varchar(10) DEFAULT NULL,
  `CONSORT_ID` int(11) DEFAULT NULL,
  `STATUS` varchar(10) NOT NULL DEFAULT '正常',
  `CREATE_TIME` datetime NOT NULL,
  `CREATE_USER_NAME` varchar(50) NOT NULL DEFAULT 'admin',
  `CREATE_USER_ID` int(11) NOT NULL DEFAULT '1',
  `UPDATE_TIME` datetime NOT NULL,
  `UPDATE_USER_NAME` varchar(50) NOT NULL DEFAULT 'admin',
  `UPDATE_USER_ID` int(11) NOT NULL DEFAULT '1',
  `COUPLE_ID` int(11) DEFAULT NULL,
  `ALIAS` varchar(10) DEFAULT NULL,
  `AUTHORITY` int(11) DEFAULT NULL,
  `EDUCATION` varchar(20) DEFAULT NULL,
  `INDUSTRY` varchar(100) DEFAULT NULL,
  `BIRTHDAY_CHINA_YEAR` varchar(50) DEFAULT NULL,
  `DIED_CHINA_YEAR` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_user_info`
--

LOCK TABLES `fa_user_info` WRITE;
/*!40000 ALTER TABLE `fa_user_info` DISABLE KEYS */;
INSERT INTO `fa_user_info` VALUES (1,4,NULL,26,NULL,6,NULL,'1981-03-13 12:00:00','四川南充',1,NULL,NULL,'软件开发','男','阴历',NULL,'正常','2017-05-28 00:00:00','admin',1,'2019-05-30 00:31:02','18180770313',1,1101,'来',7,'四川农业大学','软件开发',NULL,NULL),(2,1,NULL,2,NULL,1000,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:43:21','18180770313',1,'2019-05-24 15:34:14','18180770313',1,NULL,'',4,'','',NULL,NULL),(6,1,1,25,NULL,10,NULL,'1940-02-08 00:32:00','四川南充歧山',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-30 08:04:12','admin',1,'2019-06-05 09:22:53','17323097208',1,1095,NULL,7,NULL,NULL,NULL,NULL),(8,1,1,27,NULL,1,NULL,'2010-04-27 09:00:00','四川仪陇岐山翁家坝',0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-25 23:46:22','admin',1,'2019-05-28 23:50:21','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(9,2,1,27,NULL,1,NULL,'2014-05-10 02:42:00','四川',1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-25 23:53:28','admin',1,'2019-07-27 22:45:39','18180770313',1,NULL,NULL,7,'','',NULL,NULL),(10,2,NULL,24,NULL,19,NULL,'1916-09-24 12:55:00',NULL,0,'2002-12-15 00:00:00','仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-28 00:00:00','admin',1,'2019-07-22 11:47:11','18180770313',1,1094,NULL,7,NULL,NULL,NULL,NULL),(11,3,NULL,26,NULL,6,NULL,'1974-07-26 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2017-05-28 00:00:00','admin',1,'2019-07-19 12:00:14','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(14,1,NULL,26,NULL,6,NULL,'1964-02-06 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2017-05-28 00:00:00','admin',1,'2019-07-14 13:33:21','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(15,2,NULL,26,NULL,6,NULL,'1966-03-05 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2017-05-28 00:00:00','admin',1,'2019-07-19 12:00:05','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(16,2,NULL,25,NULL,10,NULL,'1945-04-06 00:20:00',NULL,1,NULL,NULL,'妻许庭碧，生于一九四九年三月廿三，生子自亮，生女小琼。妻刘慧兰，生庚未详','男','阴历',NULL,'正常','2017-05-28 00:00:00','admin',1,'2019-07-21 00:24:51','18180770313',1,1097,NULL,7,NULL,NULL,NULL,NULL),(17,2,1,26,NULL,16,NULL,'1979-04-27 00:00:00','四川仪陇岐山翁家坝',1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2019-05-23 10:17:30','admin',1,'2019-05-27 18:27:43','18180770313',1,1098,NULL,7,NULL,NULL,NULL,NULL),(18,1,NULL,26,NULL,16,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2017-05-28 00:00:00','admin',1,'2017-05-28 23:15:22','admin',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(19,3,NULL,23,NULL,22,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:03:36','admin',1,'2017-05-30 14:03:36','admin',1,1473,NULL,7,NULL,NULL,NULL,NULL),(20,1,NULL,24,NULL,19,NULL,'1910-08-23 04:04:00',NULL,0,'1974-07-23 10:00:00',NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:04:38','admin',1,'2019-07-20 18:48:24','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(21,3,NULL,24,NULL,19,NULL,'1933-11-13 06:04:00','仪陇翁家坝',1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:05:43','admin',1,'2019-05-19 12:12:52','18180770313',1,1093,NULL,7,NULL,NULL,NULL,NULL),(22,3,NULL,22,NULL,25,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:07:56','admin',1,'2017-05-30 14:07:56','admin',1,1091,NULL,4,NULL,NULL,NULL,NULL),(23,1,NULL,23,NULL,22,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:08:51','admin',1,'2019-05-29 09:22:50','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(24,2,NULL,23,NULL,22,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:09:18','admin',1,'2019-05-29 09:22:59','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(25,2,NULL,21,NULL,26,NULL,'1821-10-01 00:12:00',NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:12:53','admin',1,'2019-05-18 22:49:15','18180770313',1,1034,NULL,4,NULL,NULL,'1820|辛巳|1',NULL),(26,1,NULL,20,NULL,60,NULL,'1803-01-01 05:13:00',NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:13:53','admin',1,'2019-05-18 22:34:15','18180770313',1,1031,NULL,4,NULL,NULL,'1796|癸亥|7',NULL),(27,1,NULL,21,NULL,26,NULL,'1820-06-30 07:14:00',NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:14:30','admin',1,'2019-05-18 22:36:36','18180770313',1,1032,NULL,4,NULL,NULL,'1820|庚辰|0',NULL),(28,1,NULL,22,NULL,25,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:15:37','admin',1,'2019-05-19 11:40:45','18180770313',1,1070,NULL,4,NULL,NULL,NULL,NULL),(29,2,NULL,22,NULL,25,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:16:03','admin',1,'2017-05-30 14:16:03','admin',1,NULL,NULL,4,NULL,NULL,NULL,NULL),(30,4,NULL,22,NULL,25,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:16:34','admin',1,'2019-05-19 14:03:31','18180770313',1,1092,NULL,4,NULL,NULL,NULL,NULL),(34,3,NULL,25,NULL,21,NULL,'1966-05-18 00:23:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:23:50','admin',1,'2019-06-11 15:05:35','18180770313',1,1102,NULL,7,NULL,NULL,NULL,NULL),(35,5,NULL,25,NULL,21,NULL,'1971-07-21 00:24:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:24:15','admin',1,'2019-06-11 15:05:53','18180770313',1,1105,NULL,7,NULL,NULL,NULL,NULL),(36,6,NULL,25,NULL,21,NULL,'1974-07-27 00:24:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:24:37','admin',1,'2019-06-11 15:06:11','18180770313',1,1107,NULL,7,NULL,NULL,NULL,NULL),(37,1,NULL,22,NULL,27,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:28:39','admin',1,'2019-05-18 22:51:05','18180770313',1,1035,NULL,4,NULL,NULL,NULL,NULL),(38,2,NULL,22,NULL,27,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:29:56','admin',1,'2019-05-18 22:52:02','18180770313',1,1036,NULL,4,NULL,NULL,NULL,NULL),(39,3,NULL,22,NULL,27,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:30:37','admin',1,'2019-05-19 11:04:53','18180770313',1,NULL,NULL,4,NULL,NULL,NULL,NULL),(40,4,NULL,22,NULL,27,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:30:52','admin',1,'2019-05-19 11:05:38','18180770313',1,1063,NULL,4,NULL,NULL,NULL,NULL),(41,1,NULL,23,NULL,37,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:32:23','admin',1,'2017-05-30 14:32:23','admin',1,1040,NULL,7,NULL,NULL,NULL,NULL),(42,5,NULL,24,NULL,41,NULL,NULL,NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:34:51','admin',1,'2019-05-18 23:10:55','18180770313',1,1041,NULL,7,NULL,NULL,NULL,NULL),(43,1,NULL,24,NULL,41,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:35:13','admin',1,'2017-05-30 14:35:13','admin',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(44,2,NULL,24,NULL,41,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:35:30','admin',1,'2017-05-30 14:35:30','admin',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(45,1,NULL,25,NULL,42,NULL,'1952-12-03 00:35:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:35:52','admin',1,'2019-06-10 11:12:01','18180770313',1,1048,NULL,7,NULL,NULL,NULL,NULL),(46,1,NULL,23,NULL,38,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:38:13','admin',1,'2017-05-30 14:38:13','admin',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(47,2,NULL,23,NULL,38,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:39:00','admin',1,'2019-07-19 11:58:27','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(48,2,NULL,24,NULL,46,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:39:32','admin',1,'2017-05-30 14:39:32','admin',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(49,1,NULL,24,NULL,46,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:40:11','admin',1,'2017-05-30 14:40:11','admin',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(50,3,NULL,24,NULL,46,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:40:33','admin',1,'2017-05-30 14:40:33','admin',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(51,1,1,25,NULL,49,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2019-05-30 09:38:55','admin',1,'2019-05-30 10:31:54','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(52,1,NULL,24,NULL,47,NULL,NULL,NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:42:48','admin',1,'2019-05-18 23:12:50','18180770313',1,1042,NULL,7,NULL,NULL,NULL,NULL),(54,2,NULL,24,NULL,47,NULL,'1925-06-23 00:43:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:43:37','admin',1,'2019-05-18 23:16:58','18180770313',1,1043,NULL,7,NULL,NULL,NULL,NULL),(55,1,NULL,25,NULL,52,NULL,'1948-04-14 01:43:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:44:03','admin',1,'2019-05-18 23:25:42','18180770313',1,1057,NULL,7,NULL,NULL,NULL,NULL),(56,2,NULL,25,NULL,54,NULL,'1964-07-21 00:44:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:44:32','admin',1,'2019-07-19 11:58:19','18180770313',1,1061,NULL,7,NULL,NULL,NULL,NULL),(57,1,NULL,25,NULL,54,NULL,'1957-06-09 00:44:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:44:45','admin',1,'2019-05-18 23:28:24','18180770313',1,1046,NULL,7,NULL,NULL,NULL,NULL),(58,1,NULL,23,NULL,30,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:50:55','admin',1,'2019-05-19 14:04:12','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(59,1,NULL,24,NULL,58,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 14:51:21','admin',1,'2019-05-19 14:04:00','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(60,1,NULL,19,NULL,106,NULL,'1741-12-06 00:51:00',NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:52:52','admin',1,'2019-05-18 15:42:07','18180770313',1,1029,NULL,4,NULL,NULL,'1736|辛酉|5',NULL),(61,2,NULL,20,NULL,60,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:53:31','admin',1,'2019-05-19 14:04:44','18180770313',1,1109,NULL,4,NULL,NULL,NULL,NULL),(62,3,NULL,20,NULL,60,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:53:47','admin',1,'2017-05-30 14:53:47','admin',1,1185,NULL,4,NULL,NULL,NULL,NULL),(63,1,NULL,21,NULL,61,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:55:05','admin',1,'2019-05-19 14:12:06','18180770313',1,1116,NULL,4,NULL,NULL,NULL,NULL),(68,1,NULL,22,NULL,63,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:58:37','admin',1,'2017-05-30 14:58:37','admin',1,1115,NULL,4,NULL,NULL,NULL,NULL),(69,2,NULL,22,NULL,63,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:59:10','admin',1,'2019-05-19 16:06:08','18180770313',1,1159,NULL,4,NULL,NULL,NULL,NULL),(70,1,NULL,23,NULL,68,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 14:59:52','admin',1,'2019-05-19 14:13:14','18180770313',1,1117,NULL,7,NULL,NULL,NULL,NULL),(71,2,NULL,23,NULL,68,NULL,'1904-10-28 00:59:00',NULL,0,'1990-01-06 00:00:00','仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:00:09','admin',1,'2019-07-20 18:53:27','18180770313',1,1118,NULL,7,NULL,NULL,NULL,NULL),(72,3,NULL,23,NULL,68,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:00:35','admin',1,'2019-05-19 14:15:32','18180770313',1,1119,NULL,7,NULL,NULL,NULL,NULL),(73,1,NULL,23,NULL,69,NULL,'1961-01-01 00:00:00',NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:01:15','admin',1,'2019-05-19 16:10:03','18180770313',1,1164,NULL,7,NULL,NULL,NULL,NULL),(74,1,NULL,24,NULL,70,NULL,'1924-05-07 00:03:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:03:40','admin',1,'2019-05-19 14:16:39','18180770313',1,1120,NULL,7,NULL,NULL,NULL,NULL),(75,2,NULL,24,NULL,70,NULL,'1928-11-20 00:04:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:04:30','admin',1,'2019-06-11 16:51:55','18180770313',1,1121,NULL,7,NULL,NULL,NULL,NULL),(76,3,NULL,24,NULL,70,NULL,'1932-05-29 02:04:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:04:46','admin',1,'2019-06-05 10:18:10','18180770313',1,1139,NULL,7,NULL,NULL,NULL,NULL),(77,4,NULL,24,NULL,70,NULL,'1945-05-01 00:04:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:04:57','admin',1,'2019-05-19 15:52:47','18180770313',1,1149,NULL,7,NULL,NULL,NULL,NULL),(78,1,NULL,24,NULL,71,NULL,'1932-05-07 02:06:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:06:17','admin',1,'2019-06-05 10:29:41','18180770313',1,1153,NULL,7,NULL,NULL,NULL,NULL),(79,1,NULL,24,NULL,73,NULL,'1937-01-06 00:06:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:06:36','admin',1,'2019-05-19 16:11:48','18180770313',1,1165,NULL,7,NULL,NULL,NULL,NULL),(80,2,NULL,24,NULL,73,NULL,'1940-02-16 00:06:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:06:48','admin',1,'2019-05-19 16:20:21','18180770313',1,1172,NULL,7,NULL,NULL,NULL,NULL),(81,1,NULL,22,NULL,105,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:11:02','admin',1,'2019-05-19 17:07:10','18180770313',1,1188,NULL,4,NULL,NULL,NULL,NULL),(82,2,NULL,22,NULL,105,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:11:17','admin',1,'2019-05-19 17:07:02','18180770313',1,1189,NULL,4,NULL,NULL,NULL,NULL),(83,3,NULL,22,NULL,105,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:11:38','admin',1,'2019-05-19 17:06:54','18180770313',1,NULL,NULL,4,NULL,NULL,NULL,NULL),(84,4,NULL,22,NULL,105,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:11:50','admin',1,'2019-05-19 17:07:29','18180770313',1,NULL,NULL,4,NULL,NULL,NULL,NULL),(85,5,NULL,22,NULL,105,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:12:11','admin',1,'2019-05-19 17:07:39','18180770313',1,NULL,NULL,4,NULL,NULL,NULL,NULL),(86,1,NULL,23,NULL,81,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:12:49','admin',1,'2019-05-19 17:08:09','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(87,2,NULL,23,NULL,81,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:13:05','admin',1,'2019-05-19 17:08:27','18180770313',1,1190,NULL,7,NULL,NULL,NULL,NULL),(88,4,NULL,23,NULL,81,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:13:18','admin',1,'2019-05-19 17:08:57','18180770313',1,1191,NULL,7,NULL,NULL,NULL,NULL),(89,5,NULL,23,NULL,81,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:13:39','admin',1,'2019-05-19 17:10:20','18180770313',1,1194,NULL,7,NULL,NULL,NULL,NULL),(90,4,NULL,23,NULL,81,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:14:30','admin',1,'2019-05-19 17:09:22','18180770313',1,1193,NULL,7,NULL,NULL,NULL,NULL),(91,1,NULL,24,NULL,87,NULL,'1932-01-01 00:15:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:15:18','admin',1,'2019-05-19 17:11:44','18180770313',1,1195,NULL,7,NULL,NULL,NULL,NULL),(92,2,NULL,24,NULL,87,NULL,'1937-01-25 00:15:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:15:30','admin',1,'2019-05-19 17:13:20','18180770313',1,1196,NULL,7,NULL,NULL,NULL,NULL),(93,1,NULL,24,NULL,89,NULL,'1926-02-11 00:15:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:15:54','admin',1,'2019-06-10 13:00:16','18180770313',1,1206,NULL,7,NULL,NULL,NULL,NULL),(94,2,NULL,24,NULL,89,NULL,'1934-05-20 00:15:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:16:15','admin',1,'2019-06-10 13:05:36','18180770313',1,1217,NULL,7,NULL,NULL,NULL,NULL),(95,4,NULL,24,NULL,89,NULL,'1943-07-12 00:16:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:16:55','admin',1,'2019-06-14 11:43:52','18180770313',1,1228,NULL,7,NULL,NULL,NULL,NULL),(96,1,NULL,25,NULL,91,NULL,'1955-09-28 00:18:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:18:59','admin',1,'2019-05-19 17:15:15','18180770313',1,1197,NULL,7,NULL,NULL,NULL,NULL),(97,2,NULL,25,NULL,91,NULL,'1962-12-29 00:19:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:19:13','admin',1,'2019-05-19 17:18:55','18180770313',1,1201,NULL,7,NULL,NULL,NULL,NULL),(98,1,1,25,NULL,92,NULL,'1964-06-10 09:20:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2019-06-11 08:31:45','admin',1,'2019-05-19 17:20:54','18180770313',1,1202,NULL,7,NULL,NULL,NULL,NULL),(99,1,NULL,25,NULL,93,NULL,'1952-05-17 00:20:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:20:20','admin',1,'2019-06-05 10:48:29','18180770313',1,1207,NULL,7,NULL,NULL,NULL,NULL),(100,3,NULL,25,NULL,93,NULL,'1962-08-29 00:20:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:20:43','admin',1,'2019-06-11 10:45:30','18180770313',1,1210,NULL,7,NULL,NULL,NULL,NULL),(101,3,NULL,25,NULL,94,NULL,'1963-04-28 07:22:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:22:11','admin',1,'2019-06-11 11:56:40','18180770313',1,1218,NULL,7,NULL,NULL,NULL,NULL),(102,5,NULL,25,NULL,94,NULL,'1973-02-22 00:22:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:22:25','admin',1,'2019-06-11 11:58:02','18180770313',1,1221,NULL,7,NULL,NULL,NULL,NULL),(103,1,NULL,25,NULL,95,NULL,'1966-09-04 00:23:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:23:13','admin',1,'2019-05-20 13:01:53','18180770313',1,1229,NULL,7,NULL,NULL,NULL,NULL),(104,3,NULL,25,NULL,95,NULL,'1974-01-16 00:23:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:23:59','admin',1,'2019-06-12 17:51:30','18180770313',1,1231,NULL,7,NULL,NULL,NULL,NULL),(105,1,NULL,21,NULL,62,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:30:03','admin',1,'2019-05-19 17:02:05','18180770313',1,1187,NULL,4,NULL,NULL,NULL,NULL),(106,4,NULL,18,NULL,1019,NULL,'1755-05-04 01:33:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 15:33:28','admin',1,'2019-05-18 03:37:56','18180770313',1,1028,'荣字',4,NULL,NULL,'1736|丁巳|19',NULL),(107,2,NULL,19,NULL,106,NULL,'1784-10-14 00:33:00',NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:34:04','admin',1,'2019-05-18 22:27:47','18180770313',1,1030,NULL,4,NULL,NULL,'1736|甲辰|48',NULL),(108,1,NULL,20,NULL,107,NULL,'1808-01-15 00:34:00',NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:35:30','admin',1,'2019-05-20 13:10:51','18180770313',1,1234,NULL,4,NULL,NULL,'1796|戊辰|12',NULL),(109,1,NULL,21,NULL,108,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:36:21','admin',1,'2019-05-20 13:14:22','18180770313',1,NULL,NULL,4,NULL,NULL,NULL,NULL),(110,2,NULL,21,NULL,108,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:36:37','admin',1,'2019-05-20 13:14:03','18180770313',1,1235,NULL,4,NULL,NULL,NULL,NULL),(111,3,NULL,21,NULL,108,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:36:58','admin',1,'2017-05-30 15:36:58','admin',1,1280,NULL,4,NULL,NULL,NULL,NULL),(112,4,NULL,21,NULL,108,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:37:24','admin',1,'2019-05-20 15:53:33','18180770313',1,1281,NULL,4,NULL,NULL,NULL,NULL),(113,1,NULL,22,NULL,109,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:37:58','admin',1,'2019-05-20 13:14:58','18180770313',1,1236,NULL,4,NULL,NULL,NULL,NULL),(114,1,NULL,22,NULL,110,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:38:16','admin',1,'2019-05-20 13:15:31','18180770313',1,1237,NULL,4,NULL,NULL,NULL,NULL),(115,1,NULL,22,NULL,111,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:57:18','admin',1,'2019-05-20 15:54:36','18180770313',1,1282,NULL,4,NULL,NULL,NULL,NULL),(116,2,NULL,22,NULL,111,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:57:42','admin',1,'2019-05-20 15:55:17','18180770313',1,1283,NULL,4,NULL,NULL,NULL,NULL),(117,1,NULL,22,NULL,112,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:58:30','admin',1,'2019-05-21 09:39:57','18180770313',1,1410,NULL,4,NULL,NULL,NULL,NULL),(118,1,NULL,23,NULL,113,NULL,'1910-01-01 00:59:00',NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 15:59:29','admin',1,'2019-05-20 13:16:44','18180770313',1,1238,NULL,7,NULL,NULL,NULL,NULL),(119,1,NULL,23,NULL,114,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾','妻张氏生庚未详生二子润、政。妻李氏生庚未详生一子富。妻何氏生庚未详生三子茂、轩、元','男','阴历',NULL,'正常','2017-05-30 15:59:51','admin',1,'2019-07-28 19:35:59','18180770313',1,1239,NULL,7,NULL,NULL,NULL,NULL),(120,1,NULL,23,NULL,115,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:00:13','admin',1,'2019-05-20 15:56:13','18180770313',1,1284,NULL,7,NULL,NULL,NULL,NULL),(121,2,NULL,23,NULL,115,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:00:26','admin',1,'2017-05-30 16:00:26','admin',1,1477,NULL,7,NULL,NULL,NULL,NULL),(122,3,NULL,23,NULL,115,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:00:38','admin',1,'2017-05-30 16:00:38','admin',1,1476,NULL,7,NULL,NULL,NULL,NULL),(123,1,NULL,23,NULL,116,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:00:55','admin',1,'2019-05-20 23:45:16','18180770313',1,1391,NULL,7,NULL,NULL,NULL,NULL),(124,2,NULL,23,NULL,116,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾','娶张氏生庚未详生一子 德培。妻张桂珍生于1912年十月初八生一子 德乾','男','阴历',NULL,'正常','2017-05-30 16:01:07','admin',1,'2019-05-20 23:51:39','18180770313',1,1395,NULL,7,NULL,NULL,NULL,NULL),(125,3,NULL,23,NULL,116,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:01:32','admin',1,'2019-05-20 23:53:26','18180770313',1,1397,NULL,7,NULL,NULL,NULL,NULL),(126,1,NULL,23,NULL,117,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:01:56','admin',1,'2019-05-21 09:41:50','18180770313',1,1412,NULL,7,NULL,NULL,NULL,NULL),(127,2,NULL,23,NULL,117,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:02:23','admin',1,'2019-05-21 09:40:55','18180770313',1,1411,NULL,7,NULL,NULL,NULL,NULL),(128,3,NULL,23,NULL,117,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:02:38','admin',1,'2019-05-21 10:02:50','18180770313',1,1428,NULL,7,NULL,NULL,NULL,NULL),(129,4,NULL,23,NULL,117,NULL,NULL,NULL,1,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:02:51','admin',1,'2019-05-21 10:29:32','18180770313',1,1449,NULL,7,NULL,NULL,NULL,NULL),(130,5,NULL,23,NULL,117,NULL,NULL,NULL,0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2017-05-30 16:03:09','admin',1,'2019-05-21 10:30:40','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(131,1,NULL,24,NULL,118,NULL,'1944-12-29 00:07:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:07:48','admin',1,'2019-05-20 13:23:43','18180770313',1,1240,NULL,7,NULL,NULL,NULL,NULL),(132,1,NULL,24,NULL,119,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:08:44','admin',1,'2019-05-20 13:19:21','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(133,2,NULL,24,NULL,119,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:08:59','admin',1,'2019-05-20 13:22:12','18180770313',1,1247,NULL,7,NULL,NULL,NULL,NULL),(134,3,NULL,24,NULL,119,NULL,NULL,'李氏所生',1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:09:20','admin',1,'2019-05-20 13:22:03','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(135,4,NULL,24,NULL,119,NULL,'1929-03-24 00:00:00','何氏所生',1,NULL,NULL,'妻李氏生生庚未详生子贵元，妻龚兴珍生于1927年7月15日生子应白','男','阴历',NULL,'正常','2017-05-30 16:09:47','admin',1,'2019-05-20 13:59:47','18180770313',1,1254,NULL,7,NULL,NULL,NULL,NULL),(136,4,NULL,24,NULL,119,NULL,'1932-05-07 00:00:00','何氏所生',1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:13:09','admin',1,'2019-05-20 15:25:31','18180770313',1,1265,NULL,7,NULL,NULL,NULL,NULL),(137,5,NULL,24,NULL,119,NULL,'1937-01-01 00:00:00','何氏所生',1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:13:32','admin',1,'2019-05-20 15:51:36','18180770313',1,1279,NULL,7,NULL,NULL,NULL,NULL),(138,1,NULL,24,NULL,120,NULL,'1901-05-30 08:14:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:15:05','admin',1,'2019-05-20 15:59:23','18180770313',1,1285,NULL,7,NULL,NULL,NULL,NULL),(139,2,NULL,24,NULL,120,NULL,'1904-01-01 00:15:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:15:27','admin',1,'2019-05-20 16:48:38','18180770313',1,1322,NULL,7,NULL,NULL,NULL,NULL),(140,1,NULL,24,NULL,121,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:16:04','admin',1,'2019-05-20 17:29:16','18180770313',1,1356,NULL,7,NULL,NULL,NULL,NULL),(141,1,NULL,24,NULL,122,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:16:24','admin',1,'2019-05-20 23:28:02','18180770313',1,1378,NULL,7,NULL,NULL,NULL,NULL),(142,1,NULL,24,NULL,123,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:16:48','admin',1,'2019-05-20 23:45:36','18180770313',1,1392,NULL,7,NULL,NULL,NULL,NULL),(143,2,NULL,24,NULL,123,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:17:07','admin',1,'2019-05-20 23:46:06','18180770313',1,1393,NULL,7,NULL,NULL,NULL,NULL),(144,2,NULL,24,NULL,124,NULL,'1948-05-08 00:00:00',NULL,1,NULL,NULL,'妻张菊珍生于1949年3月18日，生二子国、辉。妻胡中英生于1949年','男','阴历',NULL,'正常','2017-05-30 16:18:26','admin',1,'2019-05-20 23:58:14','18180770313',1,1398,NULL,7,NULL,NULL,NULL,NULL),(145,1,NULL,24,NULL,125,NULL,'1935-12-14 00:18:00',NULL,1,NULL,NULL,'妻李培秀生庚未详生一子 全。妻张孝珍生于1934年2月26日生一子华','男','阴历',NULL,'正常','2017-05-30 16:18:48','admin',1,'2019-05-21 09:28:58','18180770313',1,1405,NULL,7,NULL,NULL,NULL,NULL),(146,2,NULL,24,NULL,125,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:19:03','admin',1,'2019-05-21 09:31:23','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(147,1,NULL,24,NULL,126,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:23:30','admin',1,'2019-05-21 09:44:23','18180770313',1,1413,NULL,7,NULL,NULL,NULL,NULL),(148,2,NULL,24,NULL,127,NULL,'1926-01-01 00:23:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:24:00','admin',1,'2019-05-21 09:45:59','18180770313',1,1415,NULL,7,NULL,NULL,NULL,NULL),(149,3,NULL,24,NULL,127,NULL,'1926-06-08 00:24:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:24:35','admin',1,'2019-05-21 09:50:51','18180770313',1,1419,NULL,7,NULL,NULL,NULL,NULL),(150,1,NULL,24,NULL,127,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:24:47','admin',1,'2019-05-21 09:42:54','18180770313',1,NULL,NULL,7,NULL,NULL,NULL,NULL),(151,1,NULL,24,NULL,128,NULL,'1933-03-08 00:25:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:25:08','admin',1,'2019-05-21 10:05:08','18180770313',1,1429,NULL,7,NULL,NULL,NULL,NULL),(152,2,NULL,24,NULL,128,NULL,'1934-07-04 00:25:00',NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:25:21','admin',1,'2019-05-21 10:06:43','18180770313',1,1430,NULL,7,NULL,NULL,NULL,NULL),(153,3,NULL,24,NULL,128,NULL,'1939-11-19 00:25:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:25:39','admin',1,'2019-05-21 10:23:10','18180770313',1,1443,NULL,7,NULL,NULL,NULL,NULL),(154,1,NULL,24,NULL,129,NULL,'1946-09-16 00:26:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:26:16','admin',1,'2019-05-21 10:31:16','18180770313',1,1450,NULL,7,NULL,NULL,NULL,NULL),(155,2,NULL,24,NULL,129,NULL,'1948-10-24 03:26:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:26:31','admin',1,'2019-05-21 12:36:43','18180770313',1,1452,NULL,7,NULL,NULL,NULL,NULL),(156,3,NULL,24,NULL,129,NULL,'1951-08-15 01:26:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:26:43','admin',1,'2019-05-21 12:46:18','18180770313',1,1458,NULL,7,NULL,NULL,NULL,NULL),(157,1,NULL,25,NULL,131,NULL,'1968-12-27 00:28:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:29:09','admin',1,'2019-05-20 13:31:39','18180770313',1,1242,'王直成',7,NULL,NULL,NULL,NULL),(158,2,NULL,25,NULL,131,NULL,'1969-11-01 00:29:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:29:35','admin',1,'2019-05-20 13:25:16','18180770313',1,1241,NULL,7,NULL,NULL,NULL,NULL),(159,1,NULL,25,NULL,133,NULL,'1941-06-21 00:30:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:30:18','admin',1,'2019-05-20 13:35:17','18180770313',1,1248,NULL,7,NULL,NULL,NULL,NULL),(160,2,NULL,25,NULL,133,NULL,'1951-01-17 00:30:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:30:34','admin',1,'2019-05-20 13:36:16','18180770313',1,1249,NULL,7,NULL,NULL,NULL,NULL),(161,1,NULL,25,NULL,135,NULL,'1951-03-06 00:30:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:31:26','admin',1,'2019-05-20 14:01:17','18180770313',1,1255,NULL,7,NULL,NULL,NULL,NULL),(162,2,NULL,25,NULL,135,NULL,'1964-08-04 00:31:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:32:18','admin',1,'2019-06-10 21:33:23','18180770313',1,1259,NULL,7,NULL,NULL,NULL,NULL),(163,1,NULL,25,NULL,136,NULL,'1954-11-13 08:34:00',NULL,1,NULL,NULL,'寇碧华生于1954年8月23日生一子 云。李万芝生于19601月初一','男','阴历',NULL,'正常','2017-05-30 16:34:16','admin',1,'2019-05-20 15:31:38','18180770313',1,1266,NULL,7,NULL,NULL,NULL,NULL),(164,2,NULL,25,NULL,136,NULL,'1957-05-10 00:34:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:34:29','admin',1,'2019-05-20 15:34:40','18180770313',1,1269,NULL,7,NULL,NULL,NULL,NULL),(165,3,NULL,25,NULL,136,NULL,'1962-09-09 00:34:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:34:43','admin',1,'2019-05-20 15:37:29','18180770313',1,1272,NULL,7,NULL,NULL,NULL,NULL),(166,1,NULL,25,NULL,138,NULL,'1926-09-17 00:35:00',NULL,1,NULL,NULL,'蒋氏生庚未详生一子 云。妻李仲云生庚未详生二子发、财。','男','阴历',NULL,'正常','2017-05-30 16:35:51','admin',1,'2019-05-20 16:03:51','18180770313',1,1286,NULL,7,NULL,NULL,NULL,NULL),(167,2,NULL,25,NULL,138,NULL,'1934-01-01 00:35:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:36:12','admin',1,'2019-05-20 16:20:41','18180770313',1,1295,NULL,7,NULL,NULL,NULL,NULL),(168,3,NULL,25,NULL,138,NULL,'1938-10-22 06:36:00',NULL,1,NULL,NULL,'妻彭以琼，生于一九四三年，生女明珍，，妻张素华，生于一九四三年三月初六，生子小毛','男','阴历',NULL,'正常','2017-05-30 16:36:28','admin',1,'2019-07-28 19:41:55','18180770313',1,1311,NULL,7,NULL,NULL,NULL,NULL),(169,5,NULL,25,NULL,138,NULL,'1944-06-18 08:36:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:37:21','admin',1,'2019-05-20 16:38:46','18180770313',1,1314,NULL,7,NULL,NULL,NULL,NULL),(170,4,NULL,25,NULL,138,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:37:54','admin',1,'2019-05-20 16:37:16','18180770313',1,1313,NULL,7,NULL,NULL,NULL,NULL),(171,1,NULL,25,NULL,139,NULL,'1929-08-19 00:38:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:38:37','admin',1,'2019-05-20 16:56:21','18180770313',1,1323,NULL,7,NULL,NULL,NULL,NULL),(172,2,NULL,25,NULL,139,NULL,'1930-12-30 00:38:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:38:54','admin',1,'2019-07-14 13:24:14','18180770313',1,1331,NULL,7,NULL,NULL,NULL,NULL),(173,3,NULL,25,NULL,139,NULL,'1936-03-16 00:39:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:39:09','admin',1,'2019-05-20 17:15:54','18180770313',1,1342,NULL,7,NULL,NULL,NULL,NULL),(174,4,NULL,25,NULL,139,NULL,'1943-11-10 00:39:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:39:32','admin',1,'2019-05-20 17:18:24','18180770313',1,1346,NULL,7,NULL,NULL,NULL,NULL),(175,6,NULL,25,NULL,139,NULL,'1949-08-12 08:39:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:39:45','admin',1,'2019-05-20 17:24:44','18180770313',1,1351,NULL,7,NULL,NULL,NULL,NULL),(176,5,NULL,25,NULL,139,NULL,'1946-01-11 00:00:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:40:01','admin',1,'2019-05-20 17:23:01','18180770313',1,1350,NULL,7,NULL,NULL,NULL,NULL),(177,1,NULL,25,NULL,140,NULL,'1919-08-17 00:42:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:42:27','admin',1,'2019-05-20 23:10:40','18180770313',1,1363,NULL,7,NULL,NULL,NULL,NULL),(178,2,NULL,25,NULL,140,NULL,'1929-08-20 00:42:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:42:51','admin',1,'2019-05-20 23:13:56','18180770313',1,1366,NULL,7,NULL,NULL,'1911|己巳|18',NULL),(179,3,NULL,25,NULL,140,NULL,NULL,NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:43:08','admin',1,'2019-05-20 23:16:54','18180770313',1,1368,NULL,7,NULL,NULL,NULL,NULL),(180,1,NULL,25,NULL,141,NULL,'1943-11-14 00:43:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:43:53','admin',1,'2019-05-20 23:30:52','18180770313',1,1380,NULL,7,NULL,NULL,NULL,NULL),(181,2,NULL,25,NULL,141,NULL,'1945-05-11 00:43:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:44:06','admin',1,'2019-05-20 23:35:33','18180770313',1,1384,NULL,7,NULL,NULL,NULL,NULL),(182,1,1,25,NULL,144,NULL,'1970-10-01 00:45:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2019-05-30 11:02:00','admin',1,'2019-05-20 23:58:56','18180770313',1,1399,NULL,7,NULL,NULL,NULL,NULL),(183,2,NULL,25,NULL,144,NULL,'1975-04-07 00:46:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:46:29','admin',1,'2019-05-21 00:01:49','18180770313',1,1402,NULL,7,NULL,NULL,NULL,NULL),(184,1,NULL,25,NULL,145,NULL,'1958-09-29 00:46:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:46:52','admin',1,'2019-06-12 17:08:07','18180770313',1,1406,NULL,7,NULL,NULL,NULL,NULL),(185,2,NULL,25,NULL,145,NULL,'1969-06-12 00:47:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:47:12','admin',1,'2019-05-21 09:37:05','18180770313',1,1407,NULL,7,NULL,NULL,NULL,NULL),(186,2,NULL,25,NULL,148,NULL,'1950-08-24 00:48:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:48:39','admin',1,'2019-05-21 09:48:00','18180770313',1,1417,NULL,7,NULL,NULL,NULL,NULL),(187,1,NULL,25,NULL,149,NULL,'1951-04-04 00:49:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:49:14','admin',1,'2019-05-21 09:52:52','18180770313',1,1538,NULL,7,NULL,NULL,NULL,NULL),(188,2,NULL,25,NULL,149,NULL,'1962-05-09 00:49:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:49:42','admin',1,'2019-05-21 09:53:49','18180770313',1,1421,NULL,7,NULL,NULL,NULL,NULL),(189,1,NULL,25,NULL,151,NULL,'1973-07-10 00:50:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:50:24','admin',1,'2019-05-21 10:10:02','18180770313',1,1434,NULL,7,NULL,NULL,NULL,NULL),(190,1,NULL,25,NULL,153,NULL,'1973-12-09 00:51:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-05-30 16:51:39','admin',1,'2019-05-21 10:24:55','18180770313',1,1444,NULL,7,NULL,NULL,NULL,NULL),(191,1,NULL,25,NULL,74,NULL,'1948-09-16 00:39:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:39:35','翁志来',1,'2019-05-19 14:21:25','18180770313',1,1122,NULL,7,NULL,NULL,NULL,NULL),(192,1,NULL,25,NULL,75,NULL,'1952-06-07 00:39:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:39:47','翁志来',1,'2019-06-10 22:20:23','18180770313',1,1130,NULL,7,NULL,NULL,NULL,NULL),(193,5,NULL,25,NULL,75,NULL,'1968-03-27 00:39:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:40:08','翁志来',1,'2019-06-10 22:02:28','18180770313',1,1131,NULL,7,NULL,NULL,NULL,NULL),(194,6,NULL,25,NULL,75,NULL,'1970-12-25 00:40:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:40:21','翁志来',1,'2019-06-10 21:58:24','18180770313',1,1137,NULL,7,NULL,NULL,NULL,NULL),(195,1,NULL,25,NULL,76,NULL,'1956-10-11 00:40:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:40:41','翁志来',1,'2019-06-05 10:24:01','18180770313',1,1140,NULL,7,NULL,NULL,NULL,NULL),(196,2,NULL,25,NULL,76,NULL,'1964-08-17 03:40:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:41:05','翁志来',1,'2019-05-19 15:46:30','18180770313',1,1143,NULL,7,NULL,NULL,NULL,NULL),(197,3,1,25,NULL,76,NULL,'1970-08-25 00:41:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2019-06-10 11:16:28','翁志来',1,'2019-05-19 15:48:45','18180770313',1,1146,NULL,7,NULL,NULL,NULL,NULL),(198,2,NULL,25,NULL,77,NULL,'1973-07-09 00:41:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:42:02','翁志来',1,'2019-06-11 15:22:41','18180770313',1,1150,NULL,7,NULL,NULL,NULL,NULL),(199,1,NULL,25,NULL,78,NULL,'1954-05-04 00:42:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:42:46','翁志来',1,'2019-05-19 16:00:54','18180770313',1,1154,NULL,7,NULL,NULL,NULL,NULL),(200,1,NULL,25,NULL,80,NULL,'1966-06-29 00:43:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:43:26','翁志来',1,'2019-05-19 16:22:52','18180770313',1,1173,NULL,7,NULL,NULL,NULL,NULL),(201,2,NULL,25,NULL,80,NULL,'1969-07-19 04:43:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2017-06-01 20:43:48','翁志来',1,'2019-05-19 16:25:06','18180770313',1,1176,NULL,7,NULL,NULL,NULL,NULL),(1000,3,NULL,1,NULL,NULL,NULL,NULL,NULL,0,NULL,NULL,NULL,'男','阴历',NULL,'正常','2019-03-29 20:04:00','翁志来',1,'2019-03-30 03:09:35','18180770313',1,NULL,NULL,4,NULL,NULL,NULL,NULL),(1002,1,NULL,3,NULL,2,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:49:04','18180770313',1,'2019-03-29 07:49:04','18180770313',1,NULL,'',4,'','',NULL,NULL),(1003,2,NULL,3,NULL,2,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:49:21','18180770313',1,'2019-03-29 07:49:21','18180770313',1,NULL,'',4,'','',NULL,NULL),(1005,1,NULL,4,NULL,1002,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:49:48','18180770313',1,'2019-03-29 07:49:48','18180770313',1,NULL,'',4,'','',NULL,NULL),(1006,2,NULL,5,NULL,1005,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:50:31','18180770313',1,'2019-03-30 11:50:24','18180770313',1,NULL,'',4,'','',NULL,NULL),(1007,2,NULL,6,NULL,1006,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:50:48','18180770313',1,'2019-03-30 11:50:42','18180770313',1,NULL,'',4,'','',NULL,NULL),(1008,1,NULL,7,NULL,1007,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:51:34','18180770313',1,'2019-03-30 09:03:05','18180770313',1,NULL,'',4,'','',NULL,NULL),(1009,1,NULL,8,NULL,1008,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:54:17','18180770313',1,'2019-03-29 07:54:17','18180770313',1,NULL,'',4,'','',NULL,NULL),(1010,2,NULL,9,NULL,1009,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:54:35','18180770313',1,'2019-06-13 16:07:28','18180770313',1,NULL,'',4,'','',NULL,NULL),(1011,1,NULL,9,NULL,1009,NULL,NULL,'',1,NULL,'','通江分支','男','阴历',NULL,'正常','2019-03-29 07:54:59','18180770313',1,'2019-03-30 00:34:41','18180770313',1,NULL,'',4,'','',NULL,NULL),(1012,1,NULL,10,NULL,1010,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:55:21','18180770313',1,'2019-06-13 15:30:31','18180770313',1,NULL,'',4,'','',NULL,NULL),(1013,1,NULL,11,NULL,1012,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:56:27','18180770313',1,'2019-03-29 07:56:27','18180770313',1,NULL,'',4,'','',NULL,NULL),(1014,1,NULL,12,NULL,1013,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:56:52','18180770313',1,'2019-06-13 15:31:11','18180770313',1,NULL,'',4,'','',NULL,NULL),(1015,3,NULL,13,NULL,1014,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-29 07:57:03','18180770313',1,'2019-06-13 16:13:20','18180770313',1,NULL,'',4,'','',NULL,NULL),(1016,2,NULL,14,NULL,1015,NULL,NULL,'',0,NULL,'殁葬居屋后壬山丙向',NULL,'男','阴历',NULL,'正常','2019-03-29 07:58:03','18180770313',1,'2019-07-22 13:40:29','18180770313',1,1532,'元中',4,'','',NULL,NULL),(1017,1,NULL,15,NULL,1016,NULL,NULL,'',0,NULL,'东龟肩辛山乙向','','男','阴历',NULL,'正常','2019-03-29 07:58:25','18180770313',1,'2019-07-22 13:42:20','18180770313',1,1533,'耀仙',4,'','',NULL,NULL),(1018,1,NULL,16,NULL,1017,NULL,NULL,'',0,NULL,'老屋门前下首圹咀辛山乙向',NULL,'男','阴历',NULL,'正常','2019-03-29 07:59:14','18180770313',1,'2019-07-22 13:47:18','18180770313',1,1534,'如周',4,'','',NULL,NULL),(1019,3,NULL,17,NULL,1018,NULL,'1661-08-16 07:00:00','迁居四川川北道顺庆府蓬州上北路长宁里凤凰埧',0,'1781-09-13 03:00:00','居屋后亥山巳向','行三即文字字居亮生于康熙丁亥年八月十六日辰时迁居四川川北道顺庆府蓬州上北路长宁里凤凰埧殁于乾隆辛丑年七月十三日巳时葬居屋后亥山巳向，娶谢氏生于康熙乙未年十一月廿三日酉时，殁于嘉庆己未年十一月廿四日未时葬与夫合茔','男','国号',NULL,'正常','2019-03-29 08:23:15','18180770313',1,'2019-07-22 12:01:08','18180770313',1,1531,'',4,'','','1661|辛丑|0','1736|辛丑|5|undefined'),(1020,2,NULL,7,NULL,1007,NULL,NULL,'',1,NULL,'','巴中分支','男','阴历',NULL,'正常','2019-03-29 13:45:16','18180770313',1,'2019-03-30 09:03:15','18180770313',1,NULL,'',7,'','',NULL,NULL),(1021,2,NULL,4,NULL,1002,NULL,NULL,'',1,NULL,'','奉节分支','男','阴历',NULL,'正常','2019-03-30 11:48:59','18180770313',1,'2019-06-13 16:00:50','18180770313',1,NULL,'',7,'','',NULL,NULL),(1022,3,NULL,4,NULL,1002,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-30 11:49:17','18180770313',1,'2019-06-13 16:01:03','18180770313',1,NULL,'',7,'','',NULL,NULL),(1023,1,NULL,5,NULL,1005,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-30 11:50:06','18180770313',1,'2019-03-30 11:50:06','18180770313',1,NULL,'',7,'','',NULL,NULL),(1024,1,NULL,6,NULL,1006,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-30 11:51:16','18180770313',1,'2019-06-13 16:05:56','18180770313',1,NULL,'',7,'','',NULL,NULL),(1025,3,NULL,6,NULL,1006,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-30 11:51:45','18180770313',1,'2019-03-30 11:51:45','18180770313',1,NULL,'',7,'','',NULL,NULL),(1026,4,NULL,6,NULL,1006,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-30 11:52:11','18180770313',1,'2019-03-30 11:52:35','18180770313',1,NULL,'',7,'','',NULL,NULL),(1027,5,NULL,6,NULL,1006,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-03-30 11:52:51','18180770313',1,'2019-03-30 11:52:51','18180770313',1,NULL,'',7,'','',NULL,NULL),(1028,1,NULL,NULL,NULL,0,NULL,'1756-09-21 03:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 03:50:21','18180770313',1,'2019-05-18 03:51:24','18180770313',1,106,NULL,7,NULL,NULL,'1736|丙子|19',NULL),(1029,1,NULL,NULL,NULL,0,NULL,'1782-06-22 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 15:40:16','18180770313',1,'2019-05-18 15:45:23','18180770313',1,60,NULL,7,NULL,NULL,'1736|壬寅|5',''),(1030,1,NULL,NULL,NULL,0,NULL,'1784-11-15 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:16:03','18180770313',1,'2019-05-18 22:16:23','18180770313',1,107,NULL,7,NULL,NULL,'1736|甲辰|48',NULL),(1031,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:34:50','18180770313',1,'2019-05-18 22:34:50','18180770313',1,26,NULL,7,NULL,NULL,NULL,NULL),(1032,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:37:31','18180770313',1,'2019-05-18 22:37:31','18180770313',1,27,NULL,7,NULL,NULL,NULL,NULL),(1033,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:49:51','18180770313',1,'2019-05-18 22:49:51','18180770313',1,25,NULL,7,NULL,NULL,NULL,NULL),(1034,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:50:01','18180770313',1,'2019-05-18 22:50:01','18180770313',1,25,NULL,7,NULL,NULL,NULL,NULL),(1035,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:51:22','18180770313',1,'2019-05-18 22:51:22','18180770313',1,37,NULL,7,NULL,NULL,NULL,NULL),(1036,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:52:27','18180770313',1,'2019-05-18 22:52:27','18180770313',1,38,NULL,7,NULL,NULL,NULL,NULL),(1037,3,NULL,24,NULL,41,NULL,NULL,'',0,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-18 22:58:18','18180770313',1,'2019-05-18 22:58:18','18180770313',1,NULL,'',7,'','',NULL,NULL),(1038,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:58:42','18180770313',1,'2019-05-18 22:58:42','18180770313',1,41,NULL,7,NULL,NULL,NULL,NULL),(1039,4,NULL,24,NULL,41,NULL,NULL,'',0,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-18 22:59:19','18180770313',1,'2019-06-01 17:44:01','18180770313',1,NULL,'',7,'','',NULL,NULL),(1040,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 22:59:33','18180770313',1,'2019-05-18 22:59:33','18180770313',1,41,NULL,7,NULL,NULL,NULL,NULL),(1041,1,NULL,NULL,NULL,0,NULL,'1922-10-06 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:12:04','18180770313',1,'2019-05-18 23:12:04','18180770313',1,42,NULL,7,NULL,NULL,'',NULL),(1042,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:13:13','18180770313',1,'2019-05-18 23:13:13','18180770313',1,52,NULL,7,NULL,NULL,NULL,NULL),(1043,1,NULL,NULL,NULL,0,NULL,'1932-02-09 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:15:53','18180770313',1,'2019-06-05 08:48:53','18180770313',1,54,NULL,7,NULL,NULL,NULL,NULL),(1044,1,NULL,NULL,NULL,0,NULL,'1955-04-17 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:24:06','18180770313',1,'2019-05-18 23:24:06','18180770313',1,45,NULL,7,NULL,NULL,'',NULL),(1045,1,NULL,NULL,NULL,0,NULL,'1948-04-27 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:27:06','18180770313',1,'2019-05-18 23:27:06','18180770313',1,55,NULL,7,NULL,NULL,'',NULL),(1046,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:28:51','18180770313',1,'2019-05-18 23:28:51','18180770313',1,57,NULL,7,NULL,NULL,NULL,NULL),(1047,1,NULL,26,NULL,45,NULL,'1977-11-11 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-18 23:30:34','18180770313',1,'2019-05-18 23:30:34','18180770313',1,1054,'',7,'','',NULL,NULL),(1048,1,NULL,NULL,NULL,0,NULL,'1954-04-17 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:31:29','18180770313',1,'2019-06-10 11:12:20','18180770313',1,45,NULL,7,NULL,NULL,NULL,NULL),(1049,1,NULL,26,NULL,55,NULL,'1978-07-07 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-18 23:32:58','18180770313',1,'2019-05-18 23:32:58','18180770313',1,1474,'',7,'','',NULL,NULL),(1050,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:33:23','18180770313',1,'2019-05-18 23:33:23','18180770313',1,55,NULL,7,NULL,NULL,NULL,''),(1051,2,NULL,27,NULL,1047,NULL,'2005-11-12 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-18 23:34:24','18180770313',1,'2019-07-27 17:25:45','18180770313',1,NULL,'',7,'','',NULL,NULL),(1052,1,NULL,27,NULL,1047,NULL,'2002-05-11 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-18 23:35:29','18180770313',1,'2019-05-18 23:35:29','18180770313',1,NULL,'',7,'','',NULL,NULL),(1053,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:36:08','18180770313',1,'2019-05-18 23:36:08','18180770313',1,1047,NULL,7,NULL,NULL,NULL,NULL),(1054,1,NULL,NULL,NULL,0,NULL,'1978-11-11 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:36:10','18180770313',1,'2019-06-10 11:18:46','18180770313',1,1047,NULL,7,NULL,NULL,NULL,NULL),(1056,3,1,26,NULL,55,NULL,'1982-01-28 15:00:00','四川省仪陇县岐山翁家坝',1,NULL,'',NULL,'男','阳历',NULL,'正常','2019-06-12 07:10:43','18180770313',1,'2019-06-12 07:27:21','18346760350',1,1058,'',7,'','',NULL,NULL),(1057,1,NULL,NULL,NULL,0,NULL,'1948-04-27 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:38:23','18180770313',1,'2019-06-05 08:56:03','18180770313',1,55,NULL,7,NULL,NULL,NULL,NULL),(1058,2,NULL,NULL,NULL,0,NULL,'1984-01-26 15:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-18 23:40:13','18180770313',1,'2019-06-12 07:23:58','18346760350',1,1056,NULL,7,NULL,NULL,'',NULL),(1059,1,NULL,27,NULL,1049,NULL,'1995-07-15 15:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-18 23:41:53','18180770313',1,'2019-05-18 23:41:53','18180770313',1,NULL,'',7,'','',NULL,NULL),(1060,1,NULL,27,NULL,1056,NULL,'2007-05-04 15:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-18 23:43:02','18180770313',1,'2019-05-18 23:43:02','18180770313',1,NULL,'',7,'','',NULL,NULL),(1061,1,NULL,NULL,NULL,0,NULL,'1966-03-20 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 10:58:21','18180770313',1,'2019-07-19 11:54:24','18180770313',1,56,NULL,7,NULL,NULL,'',NULL),(1062,3,NULL,26,NULL,56,NULL,'1993-03-08 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 10:59:56','18180770313',1,'2019-06-11 11:51:37','18180770313',1,1530,'',7,'','',NULL,NULL),(1063,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 11:06:13','18180770313',1,'2019-05-19 11:06:13','18180770313',1,40,NULL,7,NULL,NULL,NULL,NULL),(1064,1,NULL,23,NULL,40,NULL,NULL,'',0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2019-05-19 11:06:55','18180770313',1,'2019-05-19 11:08:33','18180770313',1,1065,'',7,'','',NULL,NULL),(1065,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 11:07:37','18180770313',1,'2019-05-19 11:08:09','18180770313',1,1064,NULL,7,NULL,NULL,NULL,NULL),(1066,1,NULL,24,NULL,1064,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:09:50','18180770313',1,'2019-05-19 11:09:50','18180770313',1,NULL,'',7,'','',NULL,NULL),(1067,2,NULL,24,NULL,1064,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:10:19','18180770313',1,'2019-05-19 11:10:19','18180770313',1,NULL,'',7,'','',NULL,NULL),(1068,3,NULL,24,NULL,1064,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:10:47','18180770313',1,'2019-05-19 11:10:47','18180770313',1,NULL,'',7,'','',NULL,NULL),(1069,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 11:41:19','18180770313',1,'2019-05-19 11:41:19','18180770313',1,28,NULL,7,NULL,NULL,NULL,NULL),(1070,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 11:41:23','18180770313',1,'2019-05-19 11:41:23','18180770313',1,28,NULL,7,NULL,NULL,NULL,NULL),(1071,1,NULL,23,NULL,28,NULL,NULL,'',0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2019-05-19 11:42:01','18180770313',1,'2019-05-19 11:42:01','18180770313',1,NULL,'',7,'','',NULL,NULL),(1072,1,NULL,23,NULL,29,NULL,NULL,'',0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2019-05-19 11:42:23','18180770313',1,'2019-06-05 08:38:56','18180770313',1,NULL,'',7,'','',NULL,NULL),(1073,1,NULL,24,NULL,1072,NULL,NULL,'',0,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:43:23','18180770313',1,'2019-05-19 11:43:23','18180770313',1,1074,'',7,'','',NULL,NULL),(1074,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 11:43:58','18180770313',1,'2019-05-19 11:43:58','18180770313',1,1073,NULL,7,NULL,NULL,NULL,NULL),(1075,1,NULL,25,NULL,1073,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:47:41','18180770313',1,'2019-06-01 08:43:20','18180770313',1,1076,'',7,'','',NULL,NULL),(1076,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 11:48:15','18180770313',1,'2019-05-19 11:48:15','18180770313',1,1075,NULL,7,NULL,NULL,NULL,NULL),(1077,2,NULL,25,NULL,1073,NULL,'1936-11-11 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:49:24','18180770313',1,'2019-06-05 09:07:06','18180770313',1,1078,'',7,'','',NULL,NULL),(1078,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 11:51:03','18180770313',1,'2019-05-19 11:51:03','18180770313',1,1077,NULL,7,NULL,NULL,NULL,NULL),(1079,1,NULL,26,NULL,1077,NULL,'1962-11-15 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:52:15','18180770313',1,'2019-05-19 11:52:15','18180770313',1,1084,'',7,'','',NULL,NULL),(1080,2,NULL,26,NULL,1077,NULL,'1964-08-14 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:53:06','18180770313',1,'2019-05-19 11:53:06','18180770313',1,1086,'',7,'','',NULL,NULL),(1081,1,NULL,27,NULL,1080,NULL,NULL,'',0,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 11:54:37','18180770313',1,'2019-05-19 12:06:06','18180770313',1,NULL,'',7,'','',NULL,NULL),(1082,3,NULL,26,NULL,1077,NULL,'1966-08-01 03:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:55:25','18180770313',1,'2019-05-19 12:02:42','18180770313',1,1087,'',7,'','',NULL,NULL),(1083,4,NULL,26,NULL,1077,NULL,'1970-07-21 03:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 11:56:16','18180770313',1,'2019-05-19 12:04:22','18180770313',1,1089,'',7,'','',NULL,NULL),(1084,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:00:13','18180770313',1,'2019-05-19 12:00:13','18180770313',1,1079,NULL,7,NULL,NULL,NULL,NULL),(1085,1,NULL,27,NULL,1079,NULL,'1989-08-02 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 12:00:54','18180770313',1,'2019-05-19 12:05:48','18180770313',1,NULL,'',7,'','',NULL,NULL),(1086,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:01:32','18180770313',1,'2019-05-19 12:01:32','18180770313',1,1080,NULL,7,NULL,NULL,NULL,NULL),(1087,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:03:12','18180770313',1,'2019-05-19 12:03:12','18180770313',1,1082,NULL,7,NULL,NULL,NULL,NULL),(1088,1,NULL,27,NULL,1082,NULL,'1993-09-01 04:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 12:03:56','18180770313',1,'2019-05-19 12:03:56','18180770313',1,NULL,'',7,'','',NULL,NULL),(1089,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:04:51','18180770313',1,'2019-05-19 12:04:51','18180770313',1,1083,NULL,7,NULL,NULL,NULL,NULL),(1090,1,NULL,27,NULL,1083,NULL,'1996-07-21 04:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 12:07:12','18180770313',1,'2019-05-19 12:07:12','18180770313',1,NULL,'',7,'','',NULL,NULL),(1091,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:08:18','18180770313',1,'2019-05-19 12:08:18','18180770313',1,22,NULL,7,NULL,NULL,NULL,NULL),(1092,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:09:28','18180770313',1,'2019-05-19 12:09:28','18180770313',1,30,NULL,7,NULL,NULL,NULL,NULL),(1093,1,NULL,NULL,NULL,0,NULL,'1937-06-01 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:14:06','18180770313',1,'2019-05-19 12:14:06','18180770313',1,21,NULL,7,NULL,NULL,'',NULL),(1094,1,NULL,NULL,NULL,0,NULL,'1922-10-06 00:00:00',NULL,0,'1959-11-01 00:00:00',NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:25:08','18180770313',1,'2019-07-20 18:50:21','18180770313',1,10,NULL,7,NULL,NULL,'',NULL),(1095,2,NULL,NULL,NULL,0,NULL,'1944-04-05 00:00:00',NULL,0,'2013-11-18 00:00:00',NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 12:27:57','18180770313',1,'2019-07-20 18:51:17','18180770313',1,6,NULL,7,NULL,NULL,'',''),(1096,1,NULL,NULL,NULL,0,NULL,'1949-03-23 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 13:36:57','18180770313',1,'2019-05-19 13:36:57','18180770313',1,16,NULL,7,NULL,NULL,'',NULL),(1097,1,NULL,NULL,NULL,0,NULL,'1949-03-23 00:00:00',NULL,0,'1994-04-01 00:00:00',NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 13:37:18','18180770313',1,'2019-07-20 18:55:15','18180770313',1,16,NULL,7,NULL,NULL,'',NULL),(1098,1,NULL,NULL,NULL,0,NULL,'1981-07-24 00:00:00','四川广汉',1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 13:43:17','18180770313',1,'2019-05-19 13:43:17','18180770313',1,17,NULL,7,NULL,NULL,'',NULL),(1099,1,NULL,27,NULL,17,NULL,'2007-06-09 05:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 13:44:15','18180770313',1,'2019-05-19 13:50:39','18180770313',1,NULL,'',7,'','',NULL,NULL),(1100,2,NULL,27,NULL,17,NULL,'2016-06-08 05:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 13:44:57','18180770313',1,'2019-05-19 13:51:59','18180770313',1,NULL,'',7,'','',NULL,NULL),(1101,1,NULL,NULL,NULL,0,NULL,'1983-02-05 00:00:00','四川自贡',1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 13:45:48','18180770313',1,'2019-05-24 11:56:05','18180770313',1,1,NULL,7,NULL,NULL,'',NULL),(1102,1,NULL,NULL,NULL,0,NULL,'1969-06-01 05:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 13:54:35','18180770313',1,'2019-05-19 13:54:35','18180770313',1,34,NULL,7,NULL,NULL,'',NULL),(1103,1,NULL,26,NULL,34,NULL,'1989-08-21 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 13:55:32','18180770313',1,'2019-05-19 13:55:32','18180770313',1,NULL,'',7,'','',NULL,NULL),(1104,1,NULL,26,NULL,34,NULL,'1991-09-07 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 13:56:32','18180770313',1,'2019-05-19 13:56:32','18180770313',1,NULL,'',7,'','',NULL,NULL),(1105,1,NULL,NULL,NULL,0,NULL,'1970-01-27 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 13:58:33','18180770313',1,'2019-05-19 13:58:33','18180770313',1,35,NULL,7,NULL,NULL,'',NULL),(1106,1,NULL,26,NULL,35,NULL,'2002-06-09 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 13:59:30','18180770313',1,'2019-07-27 17:31:48','18180770313',1,NULL,'',7,'','',NULL,NULL),(1107,1,NULL,NULL,NULL,0,NULL,'1973-07-10 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:01:20','18180770313',1,'2019-05-19 14:01:20','18180770313',1,36,NULL,7,NULL,NULL,'',NULL),(1108,1,NULL,26,NULL,36,NULL,'1996-05-17 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 14:02:04','18180770313',1,'2019-05-19 14:02:04','18180770313',1,NULL,'',7,'','',NULL,NULL),(1109,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:04:56','18180770313',1,'2019-05-19 14:04:56','18180770313',1,61,NULL,7,NULL,NULL,NULL,NULL),(1111,3,NULL,22,NULL,63,NULL,NULL,'',0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2019-05-19 14:06:51','18180770313',1,'2019-05-19 14:09:56','18180770313',1,1160,'',7,'','',NULL,NULL),(1112,4,NULL,22,NULL,63,NULL,NULL,'',0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2019-05-19 14:07:08','18180770313',1,'2019-05-19 14:10:12','18180770313',1,1161,'',7,'','',NULL,NULL),(1114,5,NULL,22,NULL,63,NULL,NULL,'',0,NULL,'仪陇县白鹤三社坟湾',NULL,'男','阴历',NULL,'正常','2019-05-19 14:07:31','18180770313',1,'2019-05-19 16:08:25','18180770313',1,1163,'',7,'','',NULL,NULL),(1115,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:11:06','18180770313',1,'2019-05-19 14:11:06','18180770313',1,68,NULL,7,NULL,NULL,'',NULL),(1116,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:12:22','18180770313',1,'2019-05-19 14:12:22','18180770313',1,63,NULL,7,NULL,NULL,'',NULL),(1117,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:13:28','18180770313',1,'2019-06-05 10:16:06','18180770313',1,70,NULL,7,NULL,NULL,NULL,NULL),(1118,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:15:13','18180770313',1,'2019-05-19 14:15:13','18180770313',1,71,NULL,7,NULL,NULL,'',NULL),(1119,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:15:39','18180770313',1,'2019-05-19 14:15:39','18180770313',1,72,NULL,7,NULL,NULL,NULL,NULL),(1120,1,NULL,NULL,NULL,0,NULL,'1926-07-14 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:17:45','18180770313',1,'2019-05-19 14:17:45','18180770313',1,74,NULL,7,NULL,NULL,'',NULL),(1121,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:20:12','18180770313',1,'2019-05-19 14:20:12','18180770313',1,75,NULL,7,NULL,NULL,'',NULL),(1122,1,NULL,NULL,NULL,0,NULL,'1952-02-05 03:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:22:15','18180770313',1,'2019-06-15 11:33:45','13308015598',1,191,NULL,7,NULL,NULL,'',NULL),(1123,1,NULL,26,NULL,191,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 14:22:43','18180770313',1,'2019-05-19 14:22:43','18180770313',1,NULL,'',7,'','',NULL,NULL),(1124,2,1,26,NULL,191,NULL,'1979-07-23 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-15 11:24:48','18180770313',1,'2019-06-15 11:50:44','13308015598',1,1132,'',7,'','',NULL,NULL),(1125,2,NULL,26,NULL,192,NULL,'1984-04-13 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 14:24:32','18180770313',1,'2019-06-11 17:29:50','18180770313',1,1136,'',7,'','',NULL,NULL),(1126,1,1,26,NULL,192,NULL,'1982-09-23 21:00:00','岐山乡',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 15:12:13','18180770313',1,'2019-06-12 13:03:48','15583008111',1,1501,'',7,'','',NULL,NULL),(1127,1,NULL,26,NULL,193,NULL,'1994-02-07 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 14:26:04','18180770313',1,'2019-05-19 14:40:35','18180770313',1,NULL,'',7,'','',NULL,NULL),(1128,2,NULL,26,NULL,193,NULL,'1996-02-02 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 14:26:19','18180770313',1,'2019-05-19 14:41:15','18180770313',1,NULL,'',7,'','',NULL,NULL),(1129,1,NULL,NULL,NULL,0,NULL,'1962-12-24 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:28:26','18180770313',1,'2019-05-19 14:28:26','18180770313',1,192,NULL,7,NULL,NULL,'',NULL),(1130,1,NULL,NULL,NULL,0,NULL,'1962-12-25 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:28:56','18180770313',1,'2019-06-11 16:58:50','18180770313',1,192,NULL,7,NULL,NULL,'',NULL),(1131,1,NULL,NULL,NULL,0,NULL,'1970-05-05 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:30:34','18180770313',1,'2019-05-19 14:30:34','18180770313',1,193,NULL,7,NULL,NULL,'',NULL),(1132,1,NULL,NULL,NULL,0,NULL,'1980-11-13 03:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:32:07','18180770313',1,'2019-06-15 11:34:52','13308015598',1,1124,NULL,7,NULL,NULL,'',NULL),(1133,1,NULL,27,NULL,1124,NULL,'2006-01-16 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 14:35:10','18180770313',1,'2019-05-19 14:35:10','18180770313',1,NULL,'',7,'','',NULL,NULL),(1134,1,NULL,27,NULL,1125,NULL,'2009-12-24 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 14:36:26','18180770313',1,'2019-06-11 16:53:20','18180770313',1,NULL,'',7,'','',NULL,NULL),(1135,2,NULL,27,NULL,1125,NULL,'2016-10-21 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 14:37:10','18180770313',1,'2019-06-11 17:00:52','18180770313',1,NULL,'',7,'','',NULL,NULL),(1136,1,NULL,NULL,NULL,0,NULL,'1986-01-21 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:37:55','18180770313',1,'2019-06-11 17:01:27','18180770313',1,1125,NULL,7,NULL,NULL,NULL,NULL),(1137,1,NULL,NULL,NULL,0,NULL,'1980-09-15 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 14:43:58','18180770313',1,'2019-05-19 14:43:58','18180770313',1,194,NULL,7,NULL,NULL,'',NULL),(1138,1,NULL,26,NULL,194,NULL,'2005-12-26 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 14:44:37','18180770313',1,'2019-06-10 21:45:16','18180770313',1,NULL,'',7,'','',NULL,NULL),(1139,1,NULL,NULL,NULL,0,NULL,'1934-10-05 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:42:39','18180770313',1,'2019-05-19 15:42:39','18180770313',1,76,NULL,7,NULL,NULL,'',NULL),(1140,1,NULL,NULL,NULL,0,NULL,'1957-07-16 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:44:09','18180770313',1,'2019-05-19 15:44:09','18180770313',1,195,NULL,7,NULL,NULL,'',NULL),(1141,1,NULL,26,NULL,195,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 15:44:42','18180770313',1,'2019-05-19 15:44:42','18180770313',1,NULL,'',7,'','',NULL,NULL),(1142,2,NULL,26,NULL,195,NULL,'1986-08-10 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 15:45:35','18180770313',1,'2019-05-19 15:45:35','18180770313',1,1148,'',7,'','',NULL,NULL),(1143,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:47:06','18180770313',1,'2019-05-19 15:47:06','18180770313',1,196,NULL,7,NULL,NULL,'',NULL),(1144,1,NULL,26,NULL,196,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 15:47:39','18180770313',1,'2019-05-19 15:47:39','18180770313',1,NULL,'',7,'','',NULL,NULL),(1145,1,NULL,26,NULL,196,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 15:47:50','18180770313',1,'2019-06-05 10:27:17','18180770313',1,NULL,'',7,'','',NULL,NULL),(1146,1,NULL,NULL,NULL,0,NULL,'1973-06-22 07:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:49:26','18180770313',1,'2019-05-19 15:49:26','18180770313',1,197,NULL,7,NULL,NULL,'',NULL),(1147,1,NULL,26,NULL,197,NULL,'1997-03-29 05:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 15:50:20','18180770313',1,'2019-06-12 23:03:37','18227378954',1,NULL,'',7,'','',NULL,NULL),(1148,1,NULL,NULL,NULL,0,NULL,'1987-11-21 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:51:37','18180770313',1,'2019-05-19 15:51:37','18180770313',1,1142,NULL,7,NULL,NULL,'',NULL),(1149,1,NULL,NULL,NULL,0,NULL,'1949-08-14 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:53:33','18180770313',1,'2019-05-19 15:53:33','18180770313',1,77,NULL,7,NULL,NULL,'',NULL),(1150,1,NULL,NULL,NULL,0,NULL,'1976-06-20 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:55:05','18180770313',1,'2019-05-19 15:55:05','18180770313',1,198,NULL,7,NULL,NULL,'',NULL),(1151,1,NULL,26,NULL,198,NULL,'1997-10-23 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 15:55:42','18180770313',1,'2019-05-19 15:55:42','18180770313',1,NULL,'',7,'','',NULL,NULL),(1152,2,NULL,26,NULL,198,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 15:56:15','18180770313',1,'2019-05-19 15:56:15','18180770313',1,NULL,'',7,'','',NULL,NULL),(1153,1,NULL,NULL,NULL,0,NULL,'1930-12-12 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 15:59:57','18180770313',1,'2019-06-05 10:33:27','18180770313',1,78,NULL,7,NULL,NULL,'',NULL),(1154,1,NULL,NULL,NULL,0,NULL,'1955-03-17 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:01:33','18180770313',1,'2019-05-19 16:01:33','18180770313',1,199,NULL,7,NULL,NULL,'',NULL),(1155,1,1,26,NULL,199,NULL,'1982-10-13 00:00:00','四川省南充市仪陇县双胜镇白鹤村3社',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-10 11:39:30','18180770313',1,'2019-06-10 11:41:21','15982623955',1,1157,'',7,'','',NULL,NULL),(1156,1,NULL,26,NULL,199,NULL,'1985-07-28 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:03:06','18180770313',1,'2019-05-19 16:03:06','18180770313',1,1503,'',7,'','',NULL,NULL),(1157,1,NULL,NULL,NULL,0,NULL,'1982-04-27 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:04:17','18180770313',1,'2019-05-19 16:04:17','18180770313',1,1155,NULL,7,NULL,NULL,'',NULL),(1158,1,NULL,27,NULL,1155,NULL,'2012-02-07 00:00:00','四川南充仪陇',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:05:12','18180770313',1,'2019-06-13 12:32:21','15982623955',1,NULL,'',7,'','',NULL,NULL),(1159,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:06:19','18180770313',1,'2019-05-19 16:06:19','18180770313',1,69,NULL,7,NULL,NULL,NULL,NULL),(1160,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:06:55','18180770313',1,'2019-05-19 16:06:55','18180770313',1,1111,NULL,7,NULL,NULL,'',NULL),(1161,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:07:32','18180770313',1,'2019-05-19 16:07:32','18180770313',1,1112,NULL,7,NULL,NULL,'',NULL),(1162,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:08:41','18180770313',1,'2019-05-19 16:08:41','18180770313',1,1114,NULL,7,NULL,NULL,'',NULL),(1163,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:08:44','18180770313',1,'2019-05-19 16:08:44','18180770313',1,1114,NULL,7,NULL,NULL,'',NULL),(1164,1,NULL,NULL,NULL,0,NULL,'1917-01-01 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:10:53','18180770313',1,'2019-05-19 16:10:53','18180770313',1,73,NULL,7,NULL,NULL,'',NULL),(1165,1,NULL,NULL,NULL,0,NULL,'1941-04-04 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:12:36','18180770313',1,'2019-05-19 16:12:36','18180770313',1,79,NULL,7,NULL,NULL,'',NULL),(1166,1,NULL,25,NULL,79,NULL,'1970-05-08 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:12:51','18180770313',1,'2019-05-19 16:14:17','18180770313',1,1168,'',7,'','',NULL,NULL),(1167,1,NULL,25,NULL,79,NULL,'1972-05-08 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:13:09','18180770313',1,'2019-05-19 16:16:33','18180770313',1,1170,'',7,'','',NULL,NULL),(1168,1,NULL,NULL,NULL,0,NULL,'1972-06-19 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:15:00','18180770313',1,'2019-05-19 16:15:00','18180770313',1,1166,NULL,7,NULL,NULL,'',NULL),(1169,1,NULL,26,NULL,1166,NULL,'2007-03-29 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:15:44','18180770313',1,'2019-05-19 16:15:44','18180770313',1,NULL,'',7,'','',NULL,NULL),(1170,1,NULL,NULL,NULL,0,NULL,'1969-09-18 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:18:01','18180770313',1,'2019-05-19 16:18:01','18180770313',1,1167,NULL,7,NULL,NULL,'',NULL),(1171,1,NULL,26,NULL,1167,NULL,'2003-06-18 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 16:19:12','18180770313',1,'2019-05-19 16:19:12','18180770313',1,NULL,'',7,'','',NULL,NULL),(1172,1,NULL,NULL,NULL,0,NULL,'1942-02-12 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:21:24','18180770313',1,'2019-05-19 16:21:24','18180770313',1,80,NULL,7,NULL,NULL,'',NULL),(1173,1,NULL,NULL,NULL,0,NULL,'1967-12-27 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:23:35','18180770313',1,'2019-05-19 16:23:35','18180770313',1,200,NULL,7,NULL,NULL,'',NULL),(1174,1,NULL,26,NULL,200,NULL,'1988-09-29 08:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:24:04','18180770313',1,'2019-05-19 16:28:14','18180770313',1,1178,'',7,'','',NULL,NULL),(1175,2,NULL,26,NULL,200,NULL,'1999-10-01 08:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:24:22','18180770313',1,'2019-05-19 16:31:02','18180770313',1,1181,'',7,'','',NULL,NULL),(1176,1,NULL,NULL,NULL,0,NULL,'1970-02-05 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:26:30','18180770313',1,'2019-05-19 16:26:30','18180770313',1,201,NULL,7,NULL,NULL,'',NULL),(1177,1,NULL,26,NULL,201,NULL,'1992-02-20 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:27:06','18180770313',1,'2019-05-19 16:33:52','18180770313',1,1183,'',7,'','',NULL,NULL),(1178,1,NULL,NULL,NULL,0,NULL,'1991-06-25 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:29:01','18180770313',1,'2019-05-19 16:29:01','18180770313',1,1174,NULL,7,NULL,NULL,'',NULL),(1180,1,NULL,27,NULL,1174,NULL,'2014-09-17 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 16:30:02','18180770313',1,'2019-05-19 16:30:02','18180770313',1,NULL,'',7,'','',NULL,NULL),(1181,1,NULL,NULL,NULL,0,NULL,'1990-08-20 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:32:10','18180770313',1,'2019-05-19 16:32:10','18180770313',1,1175,NULL,7,NULL,NULL,'',NULL),(1182,1,NULL,27,NULL,1175,NULL,'2016-12-18 02:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:32:54','18180770313',1,'2019-05-19 16:32:54','18180770313',1,NULL,'',7,'','',NULL,NULL),(1183,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 16:34:08','18180770313',1,'2019-05-19 16:34:08','18180770313',1,1177,NULL,7,NULL,NULL,'',NULL),(1184,1,NULL,27,NULL,1177,NULL,'2018-01-10 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 16:34:46','18180770313',1,'2019-05-19 16:34:46','18180770313',1,NULL,'',7,'','',NULL,NULL),(1185,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:01:20','18180770313',1,'2019-05-19 17:01:20','18180770313',1,62,NULL,7,NULL,NULL,NULL,NULL),(1186,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:02:20','18180770313',1,'2019-05-19 17:02:20','18180770313',1,105,NULL,7,NULL,NULL,NULL,NULL),(1187,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:02:24','18180770313',1,'2019-05-19 17:02:24','18180770313',1,105,NULL,7,NULL,NULL,NULL,NULL),(1188,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:03:48','18180770313',1,'2019-05-19 17:05:00','18180770313',1,81,NULL,7,NULL,NULL,NULL,NULL),(1189,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:06:03','18180770313',1,'2019-05-19 17:06:03','18180770313',1,82,NULL,7,NULL,NULL,NULL,NULL),(1190,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:08:35','18180770313',1,'2019-05-19 17:08:35','18180770313',1,87,NULL,7,NULL,NULL,NULL,NULL),(1191,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:09:06','18180770313',1,'2019-05-19 17:09:06','18180770313',1,88,NULL,7,NULL,NULL,NULL,NULL),(1192,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:09:33','18180770313',1,'2019-05-19 17:09:33','18180770313',1,90,NULL,7,NULL,NULL,NULL,NULL),(1193,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:09:36','18180770313',1,'2019-05-19 17:09:36','18180770313',1,90,NULL,7,NULL,NULL,NULL,NULL),(1194,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:10:28','18180770313',1,'2019-05-19 17:10:28','18180770313',1,89,NULL,7,NULL,NULL,NULL,NULL),(1195,1,NULL,NULL,NULL,0,NULL,'1936-01-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:12:23','18180770313',1,'2019-05-19 17:12:23','18180770313',1,91,NULL,7,NULL,NULL,'',NULL),(1196,1,NULL,NULL,NULL,0,NULL,'1942-02-14 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:14:25','18180770313',1,'2019-05-19 17:14:25','18180770313',1,92,NULL,7,NULL,NULL,'',NULL),(1197,1,NULL,NULL,NULL,0,NULL,'1957-12-17 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:16:04','18180770313',1,'2019-05-19 17:16:04','18180770313',1,96,NULL,7,NULL,NULL,'',NULL),(1198,1,NULL,26,NULL,96,NULL,'1981-08-11 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:16:26','18180770313',1,'2019-05-19 17:22:32','18180770313',1,1204,'',7,'','',NULL,NULL),(1199,1,NULL,26,NULL,96,NULL,'1987-01-03 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:17:45','18180770313',1,'2019-05-19 17:17:45','18180770313',1,NULL,'',7,'','',NULL,NULL),(1200,1,NULL,26,NULL,97,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:18:10','18180770313',1,'2019-05-19 17:18:10','18180770313',1,NULL,'',7,'','',NULL,NULL),(1201,1,NULL,NULL,NULL,0,NULL,'1966-01-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:19:33','18180770313',1,'2019-06-05 10:43:50','18180770313',1,97,NULL,7,NULL,NULL,'',NULL),(1202,1,NULL,NULL,NULL,0,NULL,'1967-01-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:21:30','18180770313',1,'2019-05-19 17:21:30','18180770313',1,98,NULL,7,NULL,NULL,'',NULL),(1203,1,NULL,26,NULL,98,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:21:46','18180770313',1,'2019-05-19 17:21:46','18180770313',1,NULL,'',7,'','',NULL,NULL),(1204,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:22:53','18180770313',1,'2019-05-19 17:22:53','18180770313',1,1198,NULL,7,NULL,NULL,'',NULL),(1205,1,NULL,27,NULL,1198,NULL,'2005-02-05 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:23:27','18180770313',1,'2019-05-19 17:23:27','18180770313',1,NULL,'',7,'','',NULL,NULL),(1206,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:26:26','18180770313',1,'2019-05-19 17:26:26','18180770313',1,93,NULL,7,NULL,NULL,'',NULL),(1207,1,NULL,NULL,NULL,0,NULL,'1956-01-12 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:27:48','18180770313',1,'2019-05-19 17:27:48','18180770313',1,99,NULL,7,NULL,NULL,'',NULL),(1208,1,NULL,26,NULL,99,NULL,'1979-11-29 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:28:12','18180770313',1,'2019-05-19 17:32:04','18180770313',1,1213,'',7,'','',NULL,NULL),(1209,2,1,26,NULL,99,NULL,'1983-01-21 05:00:00','四川省南充市仪陇县岐山乡白鹤3社',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-10 13:10:03','18180770313',1,'2019-06-10 13:25:47','13408565627',1,NULL,'',7,'','',NULL,NULL),(1210,1,NULL,NULL,NULL,0,NULL,'1966-08-29 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:30:27','18180770313',1,'2019-05-19 17:30:27','18180770313',1,100,NULL,7,NULL,NULL,'',NULL),(1211,1,NULL,26,NULL,100,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 17:30:57','18180770313',1,'2019-05-19 17:30:57','18180770313',1,NULL,'',7,'','',NULL,NULL),(1212,2,NULL,26,NULL,100,NULL,'1989-02-25 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:31:12','18180770313',1,'2019-06-12 22:54:06','18180770313',1,1475,'',7,'','',NULL,NULL),(1213,1,NULL,NULL,NULL,0,NULL,'1986-01-20 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:32:48','18180770313',1,'2019-05-19 17:32:48','18180770313',1,1208,NULL,7,NULL,NULL,'',NULL),(1214,1,NULL,27,NULL,1208,NULL,'2009-11-13 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:33:33','18180770313',1,'2019-05-19 17:33:33','18180770313',1,NULL,'',7,'','',NULL,NULL),(1215,1,NULL,27,NULL,1212,NULL,'2010-11-07 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 17:34:14','18180770313',1,'2019-06-06 18:50:03','18180770313',1,NULL,'',7,'','',NULL,NULL),(1216,1,NULL,27,NULL,1212,NULL,'2013-02-18 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 17:35:06','18180770313',1,'2019-06-06 18:50:17','18180770313',1,NULL,'',7,'','',NULL,NULL),(1217,1,NULL,NULL,NULL,0,NULL,'1935-06-08 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:37:49','18180770313',1,'2019-05-19 17:37:49','18180770313',1,94,NULL,7,NULL,NULL,'',NULL),(1218,1,NULL,NULL,NULL,0,NULL,'1963-05-10 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:39:10','18180770313',1,'2019-05-19 17:39:10','18180770313',1,101,NULL,7,NULL,NULL,'',NULL),(1219,1,NULL,NULL,NULL,0,NULL,'1974-12-30 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:41:40','18180770313',1,'2019-05-19 17:41:40','18180770313',1,102,NULL,7,NULL,NULL,'',NULL),(1220,1,NULL,NULL,NULL,0,NULL,'1974-12-29 09:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:42:00','18180770313',1,'2019-05-19 17:42:00','18180770313',1,102,NULL,7,NULL,NULL,'',NULL),(1221,1,NULL,NULL,NULL,0,NULL,'1974-12-30 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:42:19','18180770313',1,'2019-05-19 17:42:19','18180770313',1,102,NULL,7,NULL,NULL,'',NULL),(1222,1,NULL,26,NULL,101,NULL,'1984-08-19 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:43:00','18180770313',1,'2019-05-19 17:45:06','18180770313',1,1225,'',7,'','',NULL,NULL),(1223,2,1,26,NULL,101,NULL,'1990-04-28 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-12 18:40:34','18180770313',1,'2019-05-19 17:43:38','18180770313',1,NULL,'',7,'','',NULL,NULL),(1224,1,NULL,26,NULL,102,NULL,'1995-08-25 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-19 17:44:20','18180770313',1,'2019-05-19 17:44:20','18180770313',1,NULL,'',7,'','',NULL,NULL),(1225,1,NULL,NULL,NULL,0,NULL,'1988-04-04 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-19 17:45:44','18180770313',1,'2019-05-19 17:45:44','18180770313',1,1222,NULL,7,NULL,NULL,'',NULL),(1226,1,NULL,27,NULL,1222,NULL,'2010-12-04 09:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 17:46:27','18180770313',1,'2019-05-19 17:46:27','18180770313',1,NULL,'',7,'','',NULL,NULL),(1227,1,NULL,27,NULL,1222,NULL,'2015-02-24 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-19 17:46:56','18180770313',1,'2019-05-20 11:03:16','18180770313',1,NULL,'',7,'','',NULL,NULL),(1228,1,NULL,NULL,NULL,0,NULL,'1946-07-09 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:00:51','18180770313',1,'2019-05-20 13:00:51','18180770313',1,95,NULL,7,NULL,NULL,'',NULL),(1229,1,NULL,NULL,NULL,0,NULL,'1969-12-28 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:02:41','18180770313',1,'2019-05-20 13:02:41','18180770313',1,103,NULL,7,NULL,NULL,'',NULL),(1230,1,NULL,26,NULL,103,NULL,'1995-07-26 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 13:04:08','18180770313',1,'2019-05-20 13:04:08','18180770313',1,NULL,'',7,'','',NULL,NULL),(1231,1,NULL,NULL,NULL,0,NULL,'1977-01-15 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:06:32','18180770313',1,'2019-05-20 13:06:32','18180770313',1,104,NULL,7,NULL,NULL,'',NULL),(1232,1,NULL,26,NULL,104,NULL,'2005-08-03 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 13:07:08','18180770313',1,'2019-05-20 13:07:08','18180770313',1,NULL,'',7,'','',NULL,NULL),(1233,2,NULL,26,NULL,104,NULL,'2009-07-04 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 13:08:00','18180770313',1,'2019-05-20 13:08:00','18180770313',1,NULL,'',7,'','',NULL,NULL),(1234,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:11:24','18180770313',1,'2019-05-20 13:11:24','18180770313',1,108,NULL,7,NULL,NULL,'',NULL),(1235,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:14:13','18180770313',1,'2019-05-20 13:14:13','18180770313',1,110,NULL,7,NULL,NULL,NULL,NULL),(1236,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:15:08','18180770313',1,'2019-05-20 13:15:08','18180770313',1,113,NULL,7,NULL,NULL,'',NULL),(1237,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:15:42','18180770313',1,'2019-05-20 13:15:42','18180770313',1,114,NULL,7,NULL,NULL,NULL,NULL),(1238,1,NULL,NULL,NULL,0,NULL,'1912-02-19 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:17:43','18180770313',1,'2019-05-20 13:17:43','18180770313',1,118,NULL,7,NULL,NULL,'',NULL),(1239,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:18:37','18180770313',1,'2019-05-20 13:18:37','18180770313',1,119,NULL,7,NULL,NULL,NULL,NULL),(1240,1,NULL,NULL,NULL,0,NULL,'1947-10-10 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:24:24','18180770313',1,'2019-05-20 13:24:24','18180770313',1,131,NULL,7,NULL,NULL,'',NULL),(1241,1,NULL,NULL,NULL,0,NULL,'1968-07-12 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:26:18','18180770313',1,'2019-05-20 13:26:18','18180770313',1,158,NULL,7,NULL,NULL,'',NULL),(1242,1,NULL,NULL,NULL,0,NULL,'1978-08-28 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:28:09','18180770313',1,'2019-05-20 13:28:09','18180770313',1,157,NULL,7,NULL,NULL,'',NULL),(1243,1,NULL,26,NULL,157,NULL,'2006-06-04 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 13:29:16','18180770313',1,'2019-05-20 13:29:16','18180770313',1,NULL,'',7,'','',NULL,NULL),(1244,2,NULL,26,NULL,157,NULL,'2017-11-30 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 13:30:56','18180770313',1,'2019-05-20 13:31:23','18180770313',1,NULL,'王洁苏',7,'','',NULL,NULL),(1245,3,NULL,26,NULL,158,NULL,'1996-12-12 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 13:32:42','18180770313',1,'2019-07-27 17:43:02','18180770313',1,NULL,'',7,'','',NULL,NULL),(1246,2,NULL,26,NULL,158,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 13:33:08','18180770313',1,'2019-06-01 08:40:02','18180770313',1,NULL,'',7,'','',NULL,NULL),(1247,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:34:15','18180770313',1,'2019-05-20 13:34:15','18180770313',1,133,NULL,7,NULL,NULL,'',NULL),(1248,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:35:37','18180770313',1,'2019-05-20 13:35:37','18180770313',1,159,NULL,7,NULL,NULL,NULL,NULL),(1249,1,NULL,NULL,NULL,0,NULL,'1952-01-17 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:37:30','18180770313',1,'2019-05-20 13:37:30','18180770313',1,160,NULL,7,NULL,NULL,'',NULL),(1250,1,NULL,26,NULL,160,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 13:37:59','18180770313',1,'2019-05-20 13:37:59','18180770313',1,NULL,'',7,'','',NULL,NULL),(1251,2,NULL,26,NULL,160,NULL,'1983-09-21 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 13:38:28','18180770313',1,'2019-05-20 13:38:28','18180770313',1,1252,'',7,'','',NULL,NULL),(1252,1,NULL,NULL,NULL,0,NULL,'1986-02-03 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:39:13','18180770313',1,'2019-05-20 13:39:13','18180770313',1,1251,NULL,7,NULL,NULL,'',NULL),(1253,1,NULL,27,NULL,1251,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 13:39:52','18180770313',1,'2019-05-20 13:39:52','18180770313',1,NULL,'',7,'','',NULL,NULL),(1254,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 13:42:14','18180770313',1,'2019-05-20 13:42:14','18180770313',1,135,NULL,7,NULL,NULL,'',NULL),(1255,1,NULL,NULL,NULL,0,NULL,'1958-05-05 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 14:02:02','18180770313',1,'2019-05-20 14:02:02','18180770313',1,161,NULL,7,NULL,NULL,'',NULL),(1256,1,NULL,26,NULL,161,NULL,'1993-10-15 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 14:03:04','18180770313',1,'2019-05-20 14:08:43','18180770313',1,1260,'',7,'','',NULL,NULL),(1257,1,NULL,26,NULL,161,NULL,'1995-12-29 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 14:03:23','18180770313',1,'2019-05-20 14:14:23','18180770313',1,NULL,'',7,'','',NULL,NULL),(1258,1,1,26,NULL,162,NULL,'1986-10-13 00:00:00','翁家坝',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-11 12:24:25','18180770313',1,'2019-06-13 12:00:45','13059328175',1,1263,'',7,'','',NULL,NULL),(1259,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 14:08:02','18180770313',1,'2019-05-20 14:08:02','18180770313',1,162,NULL,7,NULL,NULL,'',NULL),(1260,1,NULL,NULL,NULL,0,NULL,'1993-10-13 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 14:09:59','18180770313',1,'2019-05-20 14:09:59','18180770313',1,1256,NULL,7,NULL,NULL,'',NULL),(1261,1,NULL,27,NULL,1256,NULL,'2017-01-10 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 14:10:35','18180770313',1,'2019-05-20 14:10:35','18180770313',1,NULL,'',7,'','',NULL,NULL),(1262,2,NULL,27,NULL,1256,NULL,'2015-05-07 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 14:12:26','18180770313',1,'2019-05-20 14:12:26','18180770313',1,NULL,'',7,'','',NULL,NULL),(1263,1,NULL,NULL,NULL,0,NULL,'1988-08-08 04:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 14:36:30','18180770313',1,'2019-06-13 12:03:04','13059328175',1,1258,NULL,7,NULL,NULL,'',NULL),(1264,1,NULL,27,NULL,1258,NULL,'2014-08-22 03:00:00','广东  云浮',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 14:37:30','18180770313',1,'2019-06-13 12:06:15','13059328175',1,NULL,'',7,'','',NULL,NULL),(1265,1,NULL,NULL,NULL,0,NULL,'1933-03-12 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:26:04','18180770313',1,'2019-05-20 15:26:04','18180770313',1,136,NULL,7,NULL,NULL,'',NULL),(1266,1,NULL,NULL,NULL,0,NULL,'1954-09-23 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:28:46','18180770313',1,'2019-05-20 15:28:46','18180770313',1,163,NULL,7,NULL,NULL,'',NULL),(1267,1,NULL,26,NULL,163,NULL,'1983-01-13 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 15:32:37','18180770313',1,'2019-05-20 15:32:37','18180770313',1,1268,'',7,'','',NULL,NULL),(1268,1,NULL,NULL,NULL,0,NULL,'1983-04-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:33:47','18180770313',1,'2019-05-20 15:33:47','18180770313',1,1267,NULL,7,NULL,NULL,'',NULL),(1269,1,NULL,NULL,NULL,0,NULL,'1962-01-06 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:35:23','18180770313',1,'2019-05-20 15:35:23','18180770313',1,164,NULL,7,NULL,NULL,'',NULL),(1270,2,1,26,NULL,164,NULL,'1986-07-09 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-10 13:48:04','18180770313',1,'2019-05-20 15:40:06','18180770313',1,1275,'',7,'','',NULL,NULL),(1271,1,NULL,26,NULL,164,NULL,'1983-04-29 14:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 15:36:10','18180770313',1,'2019-06-11 22:03:20','15983756119',1,NULL,'',7,'','',NULL,NULL),(1272,1,NULL,NULL,NULL,0,NULL,'1965-07-24 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:38:16','18180770313',1,'2019-05-20 15:38:16','18180770313',1,165,NULL,7,NULL,NULL,'',NULL),(1273,1,NULL,26,NULL,165,NULL,'1988-11-23 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 15:39:07','18180770313',1,'2019-05-20 15:47:02','18180770313',1,1278,'',7,'','',NULL,NULL),(1274,2,NULL,26,NULL,165,NULL,'1990-09-02 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 15:39:20','18180770313',1,'2019-05-20 15:50:45','18180770313',1,NULL,'',7,'','',NULL,NULL),(1275,1,NULL,NULL,NULL,0,NULL,'1987-01-09 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:40:52','18180770313',1,'2019-05-20 15:40:52','18180770313',1,1270,NULL,7,NULL,NULL,'',NULL),(1276,1,NULL,27,NULL,1270,NULL,'2011-04-16 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 15:45:12','18180770313',1,'2019-05-20 15:45:12','18180770313',1,NULL,'',7,'','',NULL,NULL),(1277,2,NULL,27,NULL,1270,NULL,'2015-08-06 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 15:45:48','18180770313',1,'2019-05-20 15:46:08','18180770313',1,NULL,'',7,'','',NULL,NULL),(1278,1,NULL,NULL,NULL,0,NULL,'1986-09-23 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:47:46','18180770313',1,'2019-05-20 15:47:46','18180770313',1,1273,NULL,7,NULL,NULL,'',NULL),(1279,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:49:55','18180770313',1,'2019-05-20 15:49:55','18180770313',1,137,NULL,7,NULL,NULL,'',NULL),(1280,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:53:00','18180770313',1,'2019-05-20 15:53:00','18180770313',1,111,NULL,7,NULL,NULL,'',NULL),(1281,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:53:52','18180770313',1,'2019-05-20 15:53:52','18180770313',1,112,NULL,7,NULL,NULL,NULL,NULL),(1282,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:54:42','18180770313',1,'2019-05-20 15:54:42','18180770313',1,115,NULL,7,NULL,NULL,NULL,NULL),(1283,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:55:31','18180770313',1,'2019-05-20 15:55:31','18180770313',1,116,NULL,7,NULL,NULL,NULL,NULL),(1284,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 15:56:33','18180770313',1,'2019-05-20 15:56:33','18180770313',1,120,NULL,7,NULL,NULL,NULL,NULL),(1285,1,NULL,NULL,NULL,0,NULL,'1904-10-13 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:00:18','18180770313',1,'2019-05-20 16:00:18','18180770313',1,138,NULL,7,NULL,NULL,'',NULL),(1286,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:02:07','18180770313',1,'2019-05-20 16:02:07','18180770313',1,166,NULL,7,NULL,NULL,'',NULL),(1287,1,NULL,26,NULL,166,NULL,'1956-05-02 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:04:27','18180770313',1,'2019-05-20 16:05:45','18180770313',1,1290,'',7,'','',NULL,NULL),(1288,2,NULL,26,NULL,166,NULL,'1961-09-23 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:04:37','18180770313',1,'2019-07-27 17:46:15','18180770313',1,1291,'',7,'','',NULL,NULL),(1289,3,NULL,26,NULL,166,NULL,'1975-03-16 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:04:46','18180770313',1,'2019-05-20 16:08:57','18180770313',1,1294,'',7,'','',NULL,NULL),(1290,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:06:01','18180770313',1,'2019-05-20 16:06:01','18180770313',1,1287,NULL,7,NULL,NULL,'',NULL),(1291,1,NULL,NULL,NULL,0,NULL,'1962-10-18 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:07:34','18180770313',1,'2019-05-20 16:07:34','18180770313',1,1288,NULL,7,NULL,NULL,'',NULL),(1292,2,NULL,27,NULL,1288,NULL,'1989-11-02 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:08:04','18180770313',1,'2019-05-20 22:54:06','18180770313',1,1361,'',7,'','',NULL,NULL),(1293,1,NULL,27,NULL,1288,NULL,'1986-10-02 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:08:23','18180770313',1,'2019-05-20 22:51:23','18180770313',1,1359,'',7,'','',NULL,NULL),(1294,1,NULL,NULL,NULL,0,NULL,'1974-03-16 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:09:49','18180770313',1,'2019-05-20 16:09:49','18180770313',1,1289,NULL,7,NULL,NULL,'',NULL),(1295,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:21:09','18180770313',1,'2019-05-20 16:21:09','18180770313',1,167,NULL,7,NULL,NULL,'',NULL),(1296,1,NULL,26,NULL,167,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:21:29','18180770313',1,'2019-05-20 16:21:29','18180770313',1,1299,'',7,'','',NULL,NULL),(1297,2,NULL,26,NULL,167,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:21:57','18180770313',1,'2019-05-20 16:23:20','18180770313',1,1308,'',7,'','',NULL,NULL),(1298,3,NULL,26,NULL,167,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:22:49','18180770313',1,'2019-05-20 16:23:32','18180770313',1,NULL,'',7,'','',NULL,NULL),(1299,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:24:29','18180770313',1,'2019-05-20 16:24:29','18180770313',1,1296,NULL,7,NULL,NULL,'',NULL),(1300,1,NULL,27,NULL,1296,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:24:53','18180770313',1,'2019-05-20 16:24:53','18180770313',1,NULL,'',7,'','',NULL,NULL),(1301,2,NULL,27,NULL,1296,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:25:25','18180770313',1,'2019-05-20 16:25:25','18180770313',1,NULL,'',7,'','',NULL,NULL),(1302,1,NULL,27,NULL,1297,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:25:54','18180770313',1,'2019-05-20 16:25:54','18180770313',1,1309,'',7,'','',NULL,NULL),(1303,1,NULL,27,NULL,1297,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:26:11','18180770313',1,'2019-05-20 16:26:11','18180770313',1,1310,'',7,'','',NULL,NULL),(1304,3,NULL,27,NULL,1297,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:26:27','18180770313',1,'2019-05-20 16:26:27','18180770313',1,NULL,'',7,'','',NULL,NULL),(1305,1,NULL,28,NULL,1302,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:27:27','18180770313',1,'2019-05-20 16:27:27','18180770313',1,NULL,'',7,'','',NULL,NULL),(1306,2,NULL,28,NULL,1302,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:27:58','18180770313',1,'2019-05-20 16:27:58','18180770313',1,NULL,'',7,'','',NULL,NULL),(1307,1,NULL,28,NULL,1303,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:28:31','18180770313',1,'2019-05-20 16:28:31','18180770313',1,NULL,'',7,'','',NULL,NULL),(1308,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:29:07','18180770313',1,'2019-05-20 16:29:07','18180770313',1,1297,NULL,7,NULL,NULL,'',NULL),(1309,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:29:51','18180770313',1,'2019-05-20 16:29:51','18180770313',1,1302,NULL,7,NULL,NULL,'',NULL),(1310,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:30:17','18180770313',1,'2019-05-20 16:30:17','18180770313',1,1303,NULL,7,NULL,NULL,'',NULL),(1311,1,NULL,NULL,NULL,0,NULL,'1943-11-17 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:33:59','18180770313',1,'2019-07-27 17:47:39','18180770313',1,168,NULL,7,NULL,NULL,'',NULL),(1312,2,1,26,NULL,168,NULL,'1983-04-25 18:00:00','四川仪陇',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-12 23:20:50','18180770313',1,'2019-07-27 17:50:55','18180770313',1,1319,'',7,'成都理工大学','自由职业',NULL,NULL),(1313,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:37:33','18180770313',1,'2019-05-20 16:37:33','18180770313',1,170,NULL,7,NULL,NULL,NULL,NULL),(1314,1,NULL,NULL,NULL,0,NULL,'1950-01-22 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:39:41','18180770313',1,'2019-05-20 16:39:41','18180770313',1,169,NULL,7,NULL,NULL,'',NULL),(1315,1,NULL,26,NULL,169,NULL,'1969-11-11 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:40:35','18180770313',1,'2019-05-20 16:40:35','18180770313',1,1317,'',7,'','',NULL,NULL),(1316,1,NULL,26,NULL,169,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:41:00','18180770313',1,'2019-05-20 16:41:00','18180770313',1,NULL,'',7,'','',NULL,NULL),(1317,1,NULL,NULL,NULL,0,NULL,'1970-12-02 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:42:27','18180770313',1,'2019-05-20 16:42:27','18180770313',1,1315,NULL,7,NULL,NULL,'',NULL),(1318,1,NULL,27,NULL,1315,NULL,'1992-10-21 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 16:42:47','18180770313',1,'2019-05-20 16:43:32','18180770313',1,NULL,'',7,'','',NULL,NULL),(1319,1,NULL,NULL,NULL,0,NULL,'1989-10-05 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:45:09','18180770313',1,'2019-05-20 16:45:09','18180770313',1,1312,NULL,7,NULL,NULL,'',NULL),(1320,1,NULL,27,NULL,1312,NULL,'2014-01-06 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:45:38','18180770313',1,'2019-06-18 19:08:12','18980698099',1,NULL,'',7,'','',NULL,NULL),(1321,2,NULL,27,NULL,1312,NULL,'2018-06-06 10:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:46:20','18180770313',1,'2019-06-18 19:09:47','18980698099',1,NULL,'',7,'','',NULL,NULL),(1322,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:48:55','18180770313',1,'2019-05-20 16:48:55','18180770313',1,139,NULL,7,NULL,NULL,'',NULL),(1323,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:57:02','18180770313',1,'2019-05-20 16:57:02','18180770313',1,171,NULL,7,NULL,NULL,'',NULL),(1324,1,NULL,26,NULL,171,NULL,'1968-10-01 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:57:29','18180770313',1,'2019-05-20 16:59:18','18180770313',1,1326,'',7,'','',NULL,NULL),(1325,2,NULL,26,NULL,171,NULL,'1970-08-23 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:58:19','18180770313',1,'2019-05-20 17:01:00','18180770313',1,1328,'',7,'','',NULL,NULL),(1326,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 16:59:37','18180770313',1,'2019-05-20 16:59:37','18180770313',1,1324,NULL,7,NULL,NULL,'',NULL),(1327,1,NULL,27,NULL,1324,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 16:59:57','18180770313',1,'2019-05-20 16:59:57','18180770313',1,NULL,'',7,'','',NULL,NULL),(1328,1,NULL,NULL,NULL,0,NULL,'1974-12-15 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:01:42','18180770313',1,'2019-05-20 17:01:42','18180770313',1,1325,NULL,7,NULL,NULL,'',NULL),(1329,1,NULL,27,NULL,1325,NULL,'1995-07-07 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:02:14','18180770313',1,'2019-05-20 17:02:14','18180770313',1,NULL,'',7,'','',NULL,NULL),(1330,1,NULL,27,NULL,1325,NULL,'2004-10-07 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:02:43','18180770313',1,'2019-05-20 17:02:43','18180770313',1,NULL,'',7,'','',NULL,NULL),(1331,1,NULL,NULL,NULL,0,NULL,'1931-11-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:05:14','18180770313',1,'2019-05-20 17:05:14','18180770313',1,172,NULL,7,NULL,NULL,'',NULL),(1332,1,NULL,26,NULL,172,NULL,'1955-09-05 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:05:35','18180770313',1,'2019-05-20 17:09:44','18180770313',1,1336,'',7,'','',NULL,NULL),(1333,2,NULL,26,NULL,172,NULL,'1965-01-28 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:05:48','18180770313',1,'2019-05-20 17:07:16','18180770313',1,1335,'',7,'','',NULL,NULL),(1334,3,NULL,26,NULL,172,NULL,'1972-11-29 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:06:26','18180770313',1,'2019-05-20 17:13:20','18180770313',1,1340,'',7,'','',NULL,NULL),(1335,1,NULL,NULL,NULL,0,NULL,'1965-11-29 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:08:21','18180770313',1,'2019-05-20 17:08:21','18180770313',1,1333,NULL,7,NULL,NULL,'',NULL),(1336,1,NULL,NULL,NULL,0,NULL,'1955-01-19 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:10:13','18180770313',1,'2019-05-20 17:10:13','18180770313',1,1332,NULL,7,NULL,NULL,'',NULL),(1337,1,NULL,27,NULL,1332,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:10:54','18180770313',1,'2019-05-20 17:10:54','18180770313',1,NULL,'',7,'','',NULL,NULL),(1338,2,NULL,27,NULL,1332,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:11:04','18180770313',1,'2019-05-20 17:11:04','18180770313',1,NULL,'',7,'','',NULL,NULL),(1339,1,NULL,27,NULL,1333,NULL,'1991-10-01 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:12:11','18180770313',1,'2019-05-20 17:12:11','18180770313',1,NULL,'',7,'','',NULL,NULL),(1340,1,NULL,NULL,NULL,0,NULL,'1975-05-19 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:14:29','18180770313',1,'2019-05-20 17:14:29','18180770313',1,1334,NULL,7,NULL,NULL,'',NULL),(1341,1,NULL,27,NULL,1334,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:14:45','18180770313',1,'2019-05-20 17:14:45','18180770313',1,NULL,'',7,'','',NULL,NULL),(1342,1,NULL,NULL,NULL,0,NULL,'1936-01-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:16:47','18180770313',1,'2019-05-20 17:16:47','18180770313',1,173,NULL,7,NULL,NULL,'',NULL),(1343,1,NULL,26,NULL,173,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:16:58','18180770313',1,'2019-05-20 17:16:58','18180770313',1,NULL,'',7,'','',NULL,NULL),(1344,2,NULL,26,NULL,173,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:17:08','18180770313',1,'2019-05-20 17:17:08','18180770313',1,NULL,'',7,'','',NULL,NULL),(1345,3,NULL,26,NULL,173,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:17:19','18180770313',1,'2019-05-20 17:17:19','18180770313',1,NULL,'',7,'','',NULL,NULL),(1346,1,NULL,NULL,NULL,0,NULL,'1944-01-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:19:01','18180770313',1,'2019-05-20 17:19:01','18180770313',1,174,NULL,7,NULL,NULL,'',NULL),(1347,1,NULL,26,NULL,174,NULL,'1990-04-21 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:20:08','18180770313',1,'2019-06-06 20:14:21','18180770313',1,1348,'',7,'','',NULL,NULL),(1348,1,NULL,NULL,NULL,0,NULL,'1989-12-19 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:21:55','18180770313',1,'2019-05-20 17:21:55','18180770313',1,1347,NULL,7,NULL,NULL,'',NULL),(1349,1,NULL,27,NULL,1347,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:22:07','18180770313',1,'2019-05-20 17:22:07','18180770313',1,NULL,'',7,'','',NULL,NULL),(1350,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:23:21','18180770313',1,'2019-05-20 17:23:21','18180770313',1,176,NULL,7,NULL,NULL,'',NULL),(1351,1,NULL,NULL,NULL,0,NULL,'1949-07-08 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:25:26','18180770313',1,'2019-05-20 17:25:26','18180770313',1,175,NULL,7,NULL,NULL,'',NULL),(1352,1,NULL,26,NULL,175,NULL,'1970-01-01 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 17:25:51','18180770313',1,'2019-05-20 17:26:47','18180770313',1,1353,'',7,'','',NULL,NULL),(1353,1,NULL,NULL,NULL,0,NULL,'1970-01-01 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:27:40','18180770313',1,'2019-05-20 17:27:40','18180770313',1,1352,NULL,7,NULL,NULL,'',NULL),(1354,1,NULL,27,NULL,1352,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 17:27:58','18180770313',1,'2019-07-14 13:32:12','18180770313',1,NULL,'',7,'','',NULL,NULL),(1355,2,NULL,27,NULL,1352,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 17:28:14','18180770313',1,'2019-05-20 17:28:14','18180770313',1,NULL,'',7,'','',NULL,NULL),(1356,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 17:29:25','18180770313',1,'2019-05-20 17:29:25','18180770313',1,140,NULL,7,NULL,NULL,NULL,NULL),(1358,1,NULL,26,NULL,168,NULL,'1964-07-27 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 22:49:43','18180770313',1,'2019-07-27 17:48:28','18180770313',1,NULL,'',7,'','',NULL,NULL),(1359,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 22:52:03','18180770313',1,'2019-05-20 22:52:03','18180770313',1,1293,NULL,7,NULL,NULL,'',NULL),(1360,1,NULL,28,NULL,1293,NULL,'2014-11-11 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 22:52:57','18180770313',1,'2019-05-20 23:07:28','18180770313',1,NULL,'',7,'','',NULL,NULL),(1361,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 22:55:24','18180770313',1,'2019-05-20 22:55:24','18180770313',1,1292,NULL,7,NULL,NULL,'',NULL),(1362,1,NULL,28,NULL,1292,NULL,'2009-11-14 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 22:57:16','18180770313',1,'2019-06-06 20:10:30','18180770313',1,NULL,'',7,'','',NULL,NULL),(1363,1,NULL,NULL,NULL,0,NULL,'1919-04-03 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:11:50','18180770313',1,'2019-05-20 23:11:50','18180770313',1,177,NULL,7,NULL,NULL,'1911|己未|8',NULL),(1364,1,NULL,26,NULL,177,NULL,'1963-06-13 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:12:26','18180770313',1,'2019-05-20 23:19:27','18180770313',1,1371,'',7,'','',NULL,NULL),(1365,2,NULL,26,NULL,177,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:12:48','18180770313',1,'2019-05-20 23:12:48','18180770313',1,NULL,'',7,'','',NULL,NULL),(1366,1,NULL,NULL,NULL,0,NULL,'1939-09-28 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:14:56','18180770313',1,'2019-05-20 23:14:56','18180770313',1,178,NULL,7,NULL,NULL,'',NULL),(1367,1,NULL,26,NULL,178,NULL,'1965-01-08 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:16:23','18180770313',1,'2019-05-20 23:16:23','18180770313',1,1376,'',7,'','',NULL,NULL),(1368,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:17:12','18180770313',1,'2019-05-20 23:17:12','18180770313',1,179,NULL,7,NULL,NULL,NULL,NULL),(1369,1,NULL,27,NULL,1367,NULL,'1990-01-01 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:17:42','18180770313',1,'2019-05-20 23:26:24','18180770313',1,1377,'',7,'','',NULL,NULL),(1370,2,NULL,27,NULL,1367,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 23:18:08','18180770313',1,'2019-05-20 23:18:08','18180770313',1,NULL,'',7,'','',NULL,NULL),(1371,1,NULL,NULL,NULL,0,NULL,'1964-04-03 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:20:03','18180770313',1,'2019-05-20 23:20:03','18180770313',1,1364,NULL,7,NULL,NULL,'',NULL),(1372,1,NULL,27,NULL,1364,NULL,'1987-09-24 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:20:24','18180770313',1,'2019-05-20 23:22:14','18180770313',1,1374,'',7,'','',NULL,NULL),(1373,1,NULL,27,NULL,1364,NULL,'1994-01-01 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:21:01','18180770313',1,'2019-05-20 23:21:01','18180770313',1,1375,'',7,'','',NULL,NULL),(1374,1,NULL,NULL,NULL,0,NULL,'1988-04-20 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:23:12','18180770313',1,'2019-05-20 23:23:12','18180770313',1,1372,NULL,7,NULL,NULL,'',NULL),(1375,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:23:50','18180770313',1,'2019-05-20 23:23:50','18180770313',1,1373,NULL,7,NULL,NULL,'',NULL),(1376,1,NULL,NULL,NULL,0,NULL,'1965-07-20 15:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:25:46','18180770313',1,'2019-05-20 23:25:46','18180770313',1,1367,NULL,7,NULL,NULL,'',NULL),(1377,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:26:47','18180770313',1,'2019-05-20 23:26:47','18180770313',1,1369,NULL,7,NULL,NULL,'',NULL),(1378,1,NULL,NULL,NULL,0,NULL,'1924-02-26 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:29:16','18180770313',1,'2019-05-20 23:29:16','18180770313',1,141,NULL,7,NULL,NULL,'',NULL),(1379,3,NULL,25,NULL,141,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:29:54','18180770313',1,'2019-05-20 23:29:54','18180770313',1,NULL,'',7,'','',NULL,NULL),(1380,1,NULL,NULL,NULL,0,NULL,'1939-09-07 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:31:53','18180770313',1,'2019-05-20 23:31:53','18180770313',1,180,NULL,7,NULL,NULL,'',NULL),(1382,1,NULL,26,NULL,181,NULL,'1974-02-14 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:33:28','18180770313',1,'2019-05-20 23:36:47','18180770313',1,1385,'',7,'','',NULL,NULL),(1383,2,1,26,NULL,181,NULL,'1982-09-19 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-12 12:42:51','18180770313',1,'2019-05-20 23:42:10','18180770313',1,1390,'',7,'','',NULL,NULL),(1384,1,NULL,NULL,NULL,0,NULL,'1949-05-26 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:34:39','18180770313',1,'2019-05-20 23:34:39','18180770313',1,181,NULL,7,NULL,NULL,'',NULL),(1385,1,NULL,NULL,NULL,0,NULL,'1977-07-16 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:37:54','18180770313',1,'2019-05-20 23:37:54','18180770313',1,1382,NULL,7,NULL,NULL,'',NULL),(1386,1,NULL,27,NULL,1382,NULL,'2000-10-27 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:38:48','18180770313',1,'2019-06-12 18:36:53','18180770313',1,NULL,'',7,'','',NULL,NULL),(1387,1,NULL,27,NULL,1382,NULL,'1998-01-18 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 23:39:55','18180770313',1,'2019-05-20 23:39:55','18180770313',1,NULL,'',7,'','',NULL,NULL),(1388,1,1,27,NULL,1383,NULL,'2006-05-03 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-12 12:50:50','18180770313',1,'2019-05-20 23:40:37','18180770313',1,NULL,'',7,'','',NULL,NULL),(1389,2,NULL,27,NULL,1383,NULL,'2012-06-29 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 23:41:34','18180770313',1,'2019-05-20 23:41:34','18180770313',1,NULL,'',7,'','',NULL,NULL),(1390,1,NULL,NULL,NULL,0,NULL,'1985-08-29 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:42:50','18180770313',1,'2019-05-20 23:42:50','18180770313',1,1383,NULL,7,NULL,NULL,'',NULL),(1391,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:45:08','18180770313',1,'2019-05-20 23:45:08','18180770313',1,123,NULL,7,NULL,NULL,NULL,NULL),(1392,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:45:44','18180770313',1,'2019-05-20 23:45:44','18180770313',1,142,NULL,7,NULL,NULL,NULL,NULL),(1393,1,NULL,NULL,NULL,0,NULL,'1934-02-26 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:46:57','18180770313',1,'2019-05-20 23:46:57','18180770313',1,143,NULL,7,NULL,NULL,'',NULL),(1394,1,NULL,25,NULL,143,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-20 23:48:18','18180770313',1,'2019-05-20 23:48:18','18180770313',1,NULL,'',7,'','',NULL,NULL),(1395,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:49:05','18180770313',1,'2019-05-20 23:49:05','18180770313',1,124,NULL,7,NULL,NULL,NULL,NULL),(1396,1,NULL,24,NULL,124,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-20 23:52:38','18180770313',1,'2019-05-20 23:52:38','18180770313',1,NULL,'',7,'','',NULL,NULL),(1397,1,NULL,NULL,NULL,0,NULL,'1912-10-08 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:54:20','18180770313',1,'2019-05-20 23:54:20','18180770313',1,125,NULL,7,NULL,NULL,'',NULL),(1398,1,NULL,NULL,NULL,0,NULL,'1949-03-18 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:56:35','18180770313',1,'2019-05-20 23:56:35','18180770313',1,144,NULL,7,NULL,NULL,'',NULL),(1399,1,NULL,NULL,NULL,0,NULL,'1970-05-29 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-20 23:59:52','18180770313',1,'2019-05-20 23:59:52','18180770313',1,182,NULL,7,NULL,NULL,'',NULL),(1400,1,NULL,26,NULL,182,NULL,'1991-05-03 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 00:00:22','18180770313',1,'2019-05-21 00:05:10','18180770313',1,NULL,'',7,'','',NULL,NULL),(1401,2,NULL,26,NULL,182,NULL,'1992-10-29 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 00:01:09','18180770313',1,'2019-05-21 00:01:09','18180770313',1,NULL,'',7,'','',NULL,NULL),(1402,1,NULL,NULL,NULL,0,NULL,'1973-04-14 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 00:02:43','18180770313',1,'2019-05-21 00:02:43','18180770313',1,183,NULL,7,NULL,NULL,'',NULL),(1403,1,NULL,26,NULL,183,NULL,'2000-02-21 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 00:03:43','18180770313',1,'2019-05-21 00:03:43','18180770313',1,NULL,'',7,'','',NULL,NULL),(1404,2,NULL,26,NULL,183,NULL,'2001-10-29 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 00:04:24','18180770313',1,'2019-05-21 00:04:24','18180770313',1,NULL,'',7,'','',NULL,NULL),(1405,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:20:52','18180770313',1,'2019-05-21 09:20:52','18180770313',1,145,NULL,7,NULL,NULL,'',NULL),(1406,1,NULL,NULL,NULL,0,NULL,'1963-11-30 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:36:19','18180770313',1,'2019-06-12 17:08:51','18180770313',1,184,NULL,7,NULL,NULL,'',NULL),(1407,1,NULL,NULL,NULL,0,NULL,'1970-04-10 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:37:55','18180770313',1,'2019-05-21 09:37:55','18180770313',1,185,NULL,7,NULL,NULL,'',NULL),(1408,1,NULL,26,NULL,185,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 09:38:24','18180770313',1,'2019-05-21 09:38:24','18180770313',1,NULL,'',7,'','',NULL,NULL),(1409,1,NULL,26,NULL,185,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 09:38:36','18180770313',1,'2019-05-21 09:38:36','18180770313',1,NULL,'',7,'','',NULL,NULL),(1410,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:39:41','18180770313',1,'2019-05-21 09:39:41','18180770313',1,117,NULL,7,NULL,NULL,NULL,NULL),(1411,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:41:18','18180770313',1,'2019-05-21 09:41:18','18180770313',1,127,NULL,7,NULL,NULL,NULL,NULL),(1412,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:42:02','18180770313',1,'2019-05-21 09:42:02','18180770313',1,126,NULL,7,NULL,NULL,NULL,NULL),(1413,1,NULL,NULL,NULL,0,NULL,'1922-06-02 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:44:58','18180770313',1,'2019-05-21 09:44:58','18180770313',1,147,NULL,7,NULL,NULL,'',NULL),(1414,1,NULL,25,NULL,147,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 09:45:17','18180770313',1,'2019-05-21 09:45:17','18180770313',1,NULL,'',7,'','',NULL,NULL),(1415,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:46:17','18180770313',1,'2019-05-21 09:46:17','18180770313',1,148,NULL,7,NULL,NULL,'',NULL),(1416,1,NULL,25,NULL,148,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 09:46:58','18180770313',1,'2019-05-21 09:46:58','18180770313',1,NULL,'',7,'','',NULL,NULL),(1417,1,NULL,NULL,NULL,0,NULL,'1953-04-04 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:48:41','18180770313',1,'2019-06-10 21:27:32','18180770313',1,186,NULL,7,NULL,NULL,'',NULL),(1418,1,1,26,NULL,186,NULL,'1977-08-24 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-10 11:35:58','18180770313',1,'2019-06-10 21:26:09','18180770313',1,1423,'',7,'','',NULL,NULL),(1419,1,NULL,NULL,NULL,0,NULL,'1922-06-02 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:51:31','18180770313',1,'2019-05-21 09:51:31','18180770313',1,149,NULL,7,NULL,NULL,'',NULL),(1420,2,1,26,NULL,187,NULL,'1987-12-22 15:00:00','',1,NULL,'',NULL,'男','阳历',NULL,'正常','2019-06-14 15:12:14','18180770313',1,'2019-06-14 15:16:12','15102878560',1,1426,'',7,'','',NULL,NULL),(1421,1,NULL,NULL,NULL,0,NULL,'1965-01-02 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:54:44','18180770313',1,'2019-07-27 17:30:25','18180770313',1,188,NULL,7,NULL,NULL,'',NULL),(1422,1,NULL,26,NULL,188,NULL,'1993-03-14 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 09:55:00','18180770313',1,'2019-06-11 15:01:16','18180770313',1,NULL,'',7,'','',NULL,NULL),(1423,1,NULL,NULL,NULL,0,NULL,'1983-11-29 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 09:57:27','18180770313',1,'2019-05-21 09:57:27','18180770313',1,1418,NULL,7,NULL,NULL,'',NULL),(1424,1,NULL,27,NULL,1418,NULL,'2007-02-05 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 09:58:35','18180770313',1,'2019-05-21 09:58:35','18180770313',1,NULL,'',7,'','',NULL,NULL),(1425,1,NULL,27,NULL,1418,NULL,'2012-04-05 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 09:58:58','18180770313',1,'2019-05-21 09:58:58','18180770313',1,NULL,'',7,'','',NULL,NULL),(1426,1,NULL,NULL,NULL,0,NULL,'1991-03-14 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:00:08','18180770313',1,'2019-05-21 10:00:08','18180770313',1,1420,NULL,7,NULL,NULL,'',NULL),(1427,1,NULL,27,NULL,1420,NULL,'2013-03-11 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:00:46','18180770313',1,'2019-05-21 10:00:46','18180770313',1,NULL,'',7,'','',NULL,NULL),(1428,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:02:45','18180770313',1,'2019-05-21 10:02:45','18180770313',1,128,NULL,7,NULL,NULL,NULL,NULL),(1429,1,NULL,NULL,NULL,0,NULL,'1940-03-24 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:04:50','18180770313',1,'2019-05-21 10:04:50','18180770313',1,151,NULL,7,NULL,NULL,'',NULL),(1430,1,NULL,NULL,NULL,0,NULL,'1946-09-10 00:00:00',NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:06:31','18180770313',1,'2019-05-21 10:06:31','18180770313',1,152,NULL,7,NULL,NULL,'',NULL),(1431,1,NULL,25,NULL,152,NULL,'1965-07-10 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:08:13','18180770313',1,'2019-05-21 10:11:22','18180770313',1,1436,'',7,'','',NULL,NULL),(1432,1,NULL,25,NULL,152,NULL,'1968-07-28 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:08:25','18180770313',1,'2019-05-21 10:14:02','18180770313',1,1438,'',7,'','',NULL,NULL),(1433,3,NULL,25,NULL,152,NULL,'1969-12-19 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:08:46','18180770313',1,'2019-05-21 10:27:32','18180770313',1,1447,'',7,'','',NULL,NULL),(1434,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:10:26','18180770313',1,'2019-05-21 10:10:26','18180770313',1,189,NULL,7,NULL,NULL,'',NULL),(1435,2,NULL,26,NULL,189,NULL,'1995-03-14 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:10:45','18180770313',1,'2019-07-27 23:15:42','18180770313',1,1440,'',7,'','',NULL,NULL),(1436,1,NULL,NULL,NULL,0,NULL,'1968-03-25 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:12:40','18180770313',1,'2019-05-21 10:12:40','18180770313',1,1431,NULL,7,NULL,NULL,'',NULL),(1437,1,NULL,26,NULL,1431,NULL,'1990-07-18 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:13:28','18180770313',1,'2019-05-21 10:13:28','18180770313',1,NULL,'',7,'','',NULL,NULL),(1438,1,NULL,NULL,NULL,0,NULL,'1970-04-24 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:14:44','18180770313',1,'2019-05-21 10:14:44','18180770313',1,1432,NULL,7,NULL,NULL,'',NULL),(1439,1,NULL,26,NULL,1432,NULL,'2002-03-25 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 10:15:43','18180770313',1,'2019-05-21 10:15:43','18180770313',1,NULL,'',7,'','',NULL,NULL),(1440,1,NULL,NULL,NULL,0,NULL,'1992-02-02 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:18:08','18180770313',1,'2019-05-21 10:18:08','18180770313',1,1435,NULL,7,NULL,NULL,'',NULL),(1441,1,NULL,27,NULL,1435,NULL,'2016-02-02 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:18:48','18180770313',1,'2019-05-21 10:18:48','18180770313',1,NULL,'',7,'','',NULL,NULL),(1442,2,NULL,27,NULL,1435,NULL,'2018-06-04 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 10:19:28','18180770313',1,'2019-05-21 10:19:28','18180770313',1,NULL,'',7,'','',NULL,NULL),(1443,1,NULL,NULL,NULL,0,NULL,'1931-11-24 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:23:57','18180770313',1,'2019-05-21 10:23:57','18180770313',1,153,NULL,7,NULL,NULL,'',NULL),(1444,1,NULL,NULL,NULL,0,NULL,'1974-12-09 02:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:25:28','18180770313',1,'2019-05-21 10:25:28','18180770313',1,190,NULL,7,NULL,NULL,'',NULL),(1445,1,NULL,26,NULL,190,NULL,'1995-05-22 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:25:57','18180770313',1,'2019-05-21 10:25:57','18180770313',1,NULL,'',7,'','',NULL,NULL),(1446,2,NULL,26,NULL,190,NULL,'2005-07-22 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:26:26','18180770313',1,'2019-05-21 10:26:26','18180770313',1,NULL,'',7,'','',NULL,NULL),(1447,1,NULL,NULL,NULL,0,NULL,'1971-12-19 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:28:02','18180770313',1,'2019-05-21 10:28:02','18180770313',1,1433,NULL,7,NULL,NULL,'',NULL),(1448,1,NULL,26,NULL,1433,NULL,'1995-08-09 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 10:28:46','18180770313',1,'2019-05-21 10:28:46','18180770313',1,NULL,'',7,'','',NULL,NULL),(1449,1,NULL,NULL,NULL,0,NULL,NULL,NULL,0,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:30:10','18180770313',1,'2019-05-21 10:30:10','18180770313',1,129,NULL,7,NULL,NULL,NULL,NULL),(1450,1,NULL,NULL,NULL,0,NULL,'1952-01-18 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 10:31:58','18180770313',1,'2019-05-21 10:31:58','18180770313',1,154,NULL,7,NULL,NULL,'',NULL),(1451,1,NULL,25,NULL,154,NULL,'1976-02-19 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 12:35:30','18180770313',1,'2019-05-21 12:44:34','18180770313',1,1456,'',7,'','',NULL,NULL),(1452,1,NULL,NULL,NULL,0,NULL,'1954-06-06 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 12:37:34','18180770313',1,'2019-05-21 12:37:34','18180770313',1,155,NULL,7,NULL,NULL,'',NULL),(1453,1,NULL,25,NULL,155,NULL,'1989-04-10 16:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 12:38:50','18180770313',1,'2019-05-21 12:38:50','18180770313',1,NULL,'',7,'','',NULL,NULL),(1454,2,NULL,25,NULL,155,NULL,'1976-03-03 10:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 12:39:40','18180770313',1,'2019-05-21 12:43:30','18180770313',1,NULL,'',7,'','',NULL,NULL),(1455,3,NULL,25,NULL,155,NULL,'1986-11-22 12:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 12:41:38','18180770313',1,'2019-05-21 12:41:38','18180770313',1,NULL,'',7,'','',NULL,NULL),(1456,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 12:44:54','18180770313',1,'2019-05-21 12:44:54','18180770313',1,1451,NULL,7,NULL,NULL,'',NULL),(1457,1,NULL,26,NULL,1451,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 12:45:11','18180770313',1,'2019-05-21 12:45:11','18180770313',1,NULL,'',7,'','',NULL,NULL),(1458,1,NULL,NULL,NULL,0,NULL,'1957-03-10 06:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 12:48:01','18180770313',1,'2019-05-21 12:48:01','18180770313',1,156,NULL,7,NULL,NULL,'',NULL),(1459,1,NULL,25,NULL,156,NULL,'1977-09-18 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 12:49:08','18180770313',1,'2019-05-21 12:49:08','18180770313',1,1462,'',7,'','',NULL,NULL),(1460,1,NULL,25,NULL,156,NULL,'1982-06-10 20:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 12:51:25','18180770313',1,'2019-05-21 12:51:25','18180770313',1,NULL,'',7,'','',NULL,NULL),(1461,3,NULL,25,NULL,156,NULL,'1986-09-21 08:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 12:52:30','18180770313',1,'2019-05-21 12:52:30','18180770313',1,NULL,'',7,'','',NULL,NULL),(1462,1,NULL,NULL,NULL,0,NULL,'1985-01-18 01:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-05-21 12:55:43','18180770313',1,'2019-05-21 12:56:02','18180770313',1,1459,NULL,7,NULL,NULL,'',NULL),(1463,1,NULL,26,NULL,1459,NULL,'2005-09-15 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-21 12:56:54','18180770313',1,'2019-05-21 12:56:54','18180770313',1,NULL,'',7,'','',NULL,NULL),(1464,2,NULL,26,NULL,1459,NULL,'2008-04-17 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-21 12:57:31','18180770313',1,'2019-05-21 12:57:31','18180770313',1,NULL,'',7,'','',NULL,NULL),(1465,1,NULL,25,NULL,146,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-31 08:30:23','18180770313',1,'2019-05-31 08:30:23','18180770313',1,NULL,'',0,'','',NULL,NULL),(1466,1,NULL,24,NULL,90,NULL,'1940-11-01 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-31 08:31:32','18180770313',1,'2019-07-28 19:21:22','18180770313',1,NULL,'',0,'','',NULL,NULL),(1467,2,NULL,25,NULL,92,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-31 08:34:12','18180770313',1,'2019-05-31 08:34:45','18180770313',1,NULL,'',0,'','',NULL,NULL),(1468,3,NULL,25,NULL,92,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-05-31 08:35:07','18180770313',1,'2019-05-31 08:35:07','18180770313',1,NULL,'',0,'','',NULL,NULL),(1469,4,NULL,25,NULL,92,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-31 08:35:45','18180770313',1,'2019-05-31 08:35:45','18180770313',1,NULL,'',0,'','',NULL,NULL),(1470,3,NULL,25,NULL,91,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-05-31 08:37:03','18180770313',1,'2019-05-31 08:37:03','18180770313',1,NULL,'',0,'','',NULL,NULL),(1471,2,NULL,26,NULL,55,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-01 08:36:38','18180770313',1,'2019-06-01 08:36:38','18180770313',1,NULL,'',0,'','',NULL,NULL),(1472,1,NULL,26,NULL,158,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-01 08:41:10','18180770313',1,'2019-06-01 08:41:10','18180770313',1,NULL,'',0,'','',NULL,NULL),(1473,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-06-05 08:43:57','18180770313',1,'2019-06-05 08:43:57','18180770313',1,19,NULL,0,NULL,NULL,'',NULL),(1474,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-06-05 08:55:18','18180770313',1,'2019-06-05 08:55:18','18180770313',1,1049,NULL,0,NULL,NULL,'',NULL),(1475,1,NULL,NULL,NULL,0,NULL,'1987-11-16 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-06-05 10:49:31','18180770313',1,'2019-06-12 22:54:45','18180770313',1,1212,NULL,0,NULL,NULL,'',NULL),(1476,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-06-06 18:48:57','18180770313',1,'2019-06-06 18:48:57','18180770313',1,122,NULL,0,NULL,NULL,'',NULL),(1477,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-06-06 18:49:23','18180770313',1,'2019-06-06 18:49:23','18180770313',1,121,NULL,0,NULL,NULL,'',NULL),(1478,2,NULL,26,NULL,45,NULL,'1982-04-28 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-10 13:17:28','18180770313',1,'2019-06-10 13:18:25','18180770313',1,NULL,'',0,'','',NULL,NULL),(1479,2,NULL,26,NULL,186,NULL,'1990-06-20 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-10 21:26:56','18180770313',1,'2019-06-10 21:26:56','18180770313',1,NULL,'',0,'','',NULL,NULL),(1481,2,NULL,26,NULL,162,NULL,'1987-11-12 04:00:00','翁家坝',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-10 21:41:48','18180770313',1,'2019-06-13 12:05:33','13059328175',1,NULL,'',0,'小学','',NULL,NULL),(1482,2,NULL,25,NULL,75,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-10 21:56:44','18180770313',1,'2019-06-10 22:01:34','18180770313',1,NULL,'',0,'','',NULL,NULL),(1483,3,NULL,25,NULL,75,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-10 21:57:03','18180770313',1,'2019-06-11 15:22:55','15583008111',1,NULL,'',0,'','',NULL,NULL),(1484,4,NULL,25,NULL,75,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-10 21:57:24','18180770313',1,'2019-06-10 22:03:07','18180770313',1,NULL,'',0,'','',NULL,NULL),(1485,2,NULL,28,NULL,1369,NULL,'2019-04-02 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 09:35:14','18180770313',1,'2019-06-11 09:47:51','18180770313',1,NULL,'',0,'','',NULL,NULL),(1486,1,NULL,28,NULL,1369,NULL,'2017-08-02 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 09:45:16','18180770313',1,'2019-06-11 09:47:40','18180770313',1,NULL,'',0,'','',NULL,NULL),(1487,2,NULL,26,NULL,98,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 10:10:11','18180770313',1,'2019-06-11 10:10:11','18180770313',1,NULL,'',0,'','',NULL,NULL),(1488,2,NULL,25,NULL,93,NULL,'1957-12-21 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 10:58:20','18180770313',1,'2019-06-11 10:58:20','18180770313',1,NULL,'',0,'','',NULL,NULL),(1489,1,NULL,26,NULL,56,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 11:51:00','18180770313',1,'2019-06-11 11:51:00','18180770313',1,NULL,'',0,'','',NULL,NULL),(1490,2,NULL,26,NULL,56,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 11:51:21','18180770313',1,'2019-06-11 11:51:21','18180770313',1,NULL,'',0,'','',NULL,NULL),(1491,1,NULL,25,NULL,94,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 11:55:56','18180770313',1,'2019-06-11 11:56:53','18180770313',1,NULL,'',0,'','',NULL,NULL),(1492,2,NULL,25,NULL,94,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 11:56:24','18180770313',1,'2019-06-11 11:56:24','18180770313',1,NULL,'',0,'','',NULL,NULL),(1493,4,NULL,25,NULL,94,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 11:57:26','18180770313',1,'2019-06-11 11:57:26','18180770313',1,NULL,'',0,'','',NULL,NULL),(1494,1,NULL,26,NULL,187,NULL,'1976-08-19 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 12:32:12','18180770313',1,'2019-06-11 12:53:28','18180770313',1,NULL,'',0,'','',NULL,NULL),(1495,2,NULL,25,NULL,42,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 12:48:24','18180770313',1,'2019-07-28 19:32:08','18180770313',1,NULL,'',0,'','',NULL,NULL),(1497,2,NULL,26,NULL,188,NULL,'1986-05-02 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 14:48:41','18180770313',1,'2019-06-11 14:48:41','18180770313',1,NULL,'',0,'','',NULL,NULL),(1498,1,NULL,NULL,NULL,21,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 15:04:20','18180770313',1,'2019-06-11 15:04:20','18180770313',1,NULL,'',0,'','',NULL,NULL),(1499,2,NULL,NULL,NULL,21,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 15:04:55','18180770313',1,'2019-06-11 15:04:55','18180770313',1,NULL,'',0,'','',NULL,NULL),(1500,4,NULL,NULL,NULL,21,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 15:05:23','18180770313',1,'2019-06-11 15:05:23','18180770313',1,NULL,'',0,'','',NULL,NULL),(1501,1,NULL,NULL,NULL,0,NULL,'1982-08-16 00:00:00',NULL,1,NULL,NULL,NULL,'男','阴历',NULL,'正常','2019-06-11 15:15:14','15583008111',1,'2019-06-11 17:02:32','18180770313',1,1126,NULL,0,NULL,NULL,'',NULL),(1502,1,NULL,25,NULL,77,NULL,'1971-05-07 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-11 15:22:23','18180770313',1,'2019-06-11 15:22:23','18180770313',1,NULL,'',0,'','',NULL,NULL),(1503,1,NULL,NULL,NULL,0,NULL,'1994-01-18 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-06-11 17:40:53','18180770313',1,'2019-06-11 17:40:53','18180770313',1,1156,NULL,0,NULL,NULL,'',NULL),(1504,1,NULL,26,NULL,184,NULL,'1985-11-29 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-12 13:41:54','18180770313',1,'2019-06-12 17:09:32','18180770313',1,NULL,'',0,'','',NULL,NULL),(1505,2,NULL,26,NULL,184,NULL,'1987-08-24 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-12 17:10:35','18180770313',1,'2019-06-12 17:10:35','18180770313',1,NULL,'',0,'','',NULL,NULL),(1506,1,NULL,27,NULL,1504,NULL,'2015-07-21 00:00:00','',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-12 17:11:09','18180770313',1,'2019-06-12 22:17:47','18180770313',1,NULL,'',0,'','',NULL,NULL),(1507,2,NULL,25,NULL,95,NULL,'1973-12-09 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-12 17:51:18','18180770313',1,'2019-06-12 17:51:18','18180770313',1,NULL,'',0,'','',NULL,NULL),(1508,2,NULL,11,NULL,1012,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:10:17','18180770313',1,'2019-06-13 16:10:17','18180770313',1,NULL,'',0,'','',NULL,NULL),(1509,3,NULL,11,NULL,1012,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:11:14','18180770313',1,'2019-06-13 16:11:14','18180770313',1,NULL,'',0,'','',NULL,NULL),(1510,1,NULL,13,NULL,1014,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:12:39','18180770313',1,'2019-06-13 16:12:39','18180770313',1,NULL,'',0,'','',NULL,NULL),(1511,2,NULL,13,NULL,1014,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:13:10','18180770313',1,'2019-06-13 16:13:10','18180770313',1,NULL,'',0,'','',NULL,NULL),(1512,1,NULL,14,NULL,1015,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:21:37','18180770313',1,'2019-06-13 16:21:37','18180770313',1,NULL,'',0,'','',NULL,NULL),(1513,2,NULL,16,NULL,1017,NULL,NULL,'',1,NULL,'','行二字胜周生殁未详葬下塘咀辛山乙向','男','阴历',NULL,'正常','2019-06-13 16:23:14','18180770313',1,'2019-07-22 13:59:20','18180770313',1,NULL,'',0,'','',NULL,NULL),(1514,3,NULL,16,NULL,1017,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:24:28','18180770313',1,'2019-06-13 16:24:28','18180770313',1,NULL,'',0,'','',NULL,NULL),(1515,4,NULL,16,NULL,1017,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:24:47','18180770313',1,'2019-06-13 16:24:47','18180770313',1,NULL,'',0,'','',NULL,NULL),(1516,5,NULL,16,NULL,1017,NULL,NULL,'',1,NULL,'','行五字能周生殁未详葬四川三台县响水堰娶毛氏生未详殁葬居屋后生子二，汉，月','男','阴历',NULL,'正常','2019-06-13 16:25:06','18180770313',1,'2019-07-22 14:00:44','18180770313',1,NULL,'',0,'','',NULL,NULL),(1517,6,NULL,16,NULL,1017,NULL,NULL,'',1,NULL,'','字能大生于康熙戌辰年六月十五日午时殁于乾隆辛未年正月初九日午时葬居屋背后壬山丙向娶李氏生于康熙丙子年七月初二日辰时殁于乾隆辛卯年六月初八日卯时葬居屋后与夫同茔生子五铭福有传谟','男','阴历',NULL,'正常','2019-06-13 16:25:23','18180770313',1,'2019-07-22 14:01:35','18180770313',1,NULL,'',0,'','',NULL,NULL),(1518,1,NULL,17,NULL,1018,NULL,NULL,'',1,NULL,'','生于乾隆丁巳年五月初六日丑时殁于嘉庆己未年三月初三日辰时葬父茔右同向，娶陈氏，生于乾隆己未年九月十三日巳时，生子二　，锡，受','男','阴历',NULL,'正常','2019-06-13 16:27:35','18180770313',1,'2019-07-22 13:50:41','18180770313',1,NULL,'',0,'','',NULL,NULL),(1519,2,NULL,17,NULL,1018,NULL,NULL,'',1,NULL,'','生于康熙癸未年正月初六日酉时，殁于乾隆丙申年七月十一日辰时，葬四川顺庆府蓬州上兆路辛山乙向，娶金氏，生于康熙四十四年四月初六日巳时，殁于乾隆廿八年十二月十七日亥时葬与夫同茔，生子一 椿','男','阴历',NULL,'正常','2019-06-13 16:28:31','18180770313',1,'2019-07-22 13:38:08','18180770313',1,NULL,'',0,'','',NULL,NULL),(1520,4,NULL,17,NULL,1018,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-06-13 16:28:51','18180770313',1,'2019-06-13 16:28:51','18180770313',1,NULL,'',0,'','',NULL,NULL),(1521,1,NULL,18,NULL,1019,NULL,NULL,'',1,NULL,'','生于乾隆丁巳年五月初六日丑时，殁于嘉庆己未年三月初三日辰时葬父茔右同向，娶陈氏，生于乾隆己未年九月十三日巳时，生子二，锡，受','男','阴历',NULL,'正常','2019-06-13 16:30:44','18180770313',1,'2019-07-22 13:53:46','18180770313',1,NULL,'',0,'','',NULL,NULL),(1522,2,NULL,18,NULL,1019,NULL,NULL,'',1,NULL,'','生于乾隆癸亥年七月初三日酉时，殁葬未详，娶黎氏，生于乾隆乙酉年十月初二日戌时，殁葬未详，生子三 详，服，兴','男','阴历',NULL,'正常','2019-06-13 16:31:35','18180770313',1,'2019-07-22 13:55:22','18180770313',1,NULL,'',0,'','',NULL,NULL),(1523,3,NULL,18,NULL,1019,NULL,NULL,'',1,NULL,'','行三即贵字字无虚生于乾隆壬申年正月廿四日酉时，殁葬未详，娶黄氏，俱未详，生子二　,茂，顺 复娶史氏','男','阴历',NULL,'正常','2019-06-13 16:32:49','18180770313',1,'2019-07-22 13:56:52','18180770313',1,NULL,'',0,'','',NULL,NULL),(1524,2,NULL,26,NULL,201,NULL,'1993-06-30 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-13 22:36:00','18180770313',1,'2019-06-13 22:36:00','18180770313',1,NULL,'',0,'','',NULL,NULL),(1525,3,NULL,24,NULL,89,NULL,'1938-12-27 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-06-14 11:43:27','18180770313',1,'2019-06-14 11:43:27','18180770313',1,NULL,'',0,'','',NULL,NULL),(1526,2,NULL,27,NULL,1333,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-07-14 13:27:13','18180770313',1,'2019-07-14 13:27:13','18180770313',1,NULL,'',0,'','',NULL,NULL),(1527,1,NULL,26,NULL,176,NULL,NULL,'',1,NULL,'',NULL,'男','阴历',NULL,'正常','2019-07-14 13:30:38','18180770313',1,'2019-07-14 13:30:38','18180770313',1,NULL,'',0,'','',NULL,NULL),(1528,3,NULL,25,NULL,131,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-07-14 13:35:39','18180770313',1,'2019-07-14 13:35:39','18180770313',1,NULL,'',0,'','',NULL,NULL),(1529,4,NULL,25,NULL,131,NULL,NULL,'',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-07-14 13:36:06','18180770313',1,'2019-07-14 13:36:06','18180770313',1,NULL,'',0,'','',NULL,NULL),(1530,1,NULL,NULL,NULL,0,NULL,'1999-07-23 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-07-19 11:56:21','18180770313',1,'2019-07-19 11:56:21','18180770313',1,1062,NULL,0,NULL,NULL,'',NULL),(1531,1,NULL,NULL,NULL,0,NULL,'1715-11-23 17:00:00',NULL,0,'2019-11-24 13:00:00','葬与夫合茔','','女','国号',NULL,'正常','2019-07-22 12:01:36','18180770313',1,'2019-07-22 12:06:04','18180770313',1,1019,NULL,0,NULL,NULL,'1661|乙未|54','1796|辛酉|5|undefined'),(1532,1,NULL,NULL,NULL,0,NULL,NULL,'与夫同茔',1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-07-22 13:41:09','18180770313',1,'2019-07-22 13:41:24','18180770313',1,1016,NULL,0,NULL,NULL,'',NULL),(1533,1,NULL,NULL,NULL,0,NULL,NULL,NULL,1,NULL,NULL,'生于顺治丁亥年八月十七日辰时殁于雍正庚戍年十月廿六日午时葬宋家地壬山丙向','女','阴历',NULL,'正常','2019-07-22 13:43:08','18180770313',1,'2019-07-22 13:46:00','18180770313',1,1017,NULL,0,NULL,NULL,'',NULL),(1534,1,NULL,NULL,NULL,0,NULL,NULL,'',0,NULL,'居屋背后丙向',NULL,'女','阴历',NULL,'正常','2019-07-22 13:47:59','18180770313',1,'2019-07-22 13:48:40','18180770313',1,1018,NULL,0,NULL,NULL,'',NULL),(1535,2,NULL,25,NULL,74,NULL,'1952-09-18 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-07-27 23:08:39','18180770313',1,'2019-07-27 23:09:38','18180770313',1,NULL,'',0,'','',NULL,NULL),(1536,3,NULL,25,NULL,74,NULL,'1955-02-09 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-07-27 23:11:14','18180770313',1,'2019-07-27 23:11:59','18180770313',1,NULL,'',0,'','',NULL,NULL),(1537,1,NULL,26,NULL,189,NULL,'1992-08-13 00:00:00','',1,NULL,'',NULL,'女','阴历',NULL,'正常','2019-07-27 23:15:30','18180770313',1,'2019-07-27 23:15:30','18180770313',1,NULL,'',0,'','',NULL,NULL),(1538,1,NULL,NULL,NULL,0,NULL,'1956-05-20 00:00:00',NULL,1,NULL,NULL,NULL,'女','阴历',NULL,'正常','2019-07-28 19:15:27','18180770313',1,'2019-07-28 19:15:27','18180770313',1,187,NULL,0,NULL,NULL,'',NULL);
/*!40000 ALTER TABLE `fa_user_info` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `fa_user_role`
--

DROP TABLE IF EXISTS `fa_user_role`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `fa_user_role` (
  `ROLE_ID` int(11) NOT NULL,
  `USER_ID` int(11) NOT NULL,
  PRIMARY KEY (`ROLE_ID`,`USER_ID`),
  KEY `FK_FA_USER_ROLE_REF_USER` (`USER_ID`),
  CONSTRAINT `FK_FA_USER_ROLE_REF_ROLE` FOREIGN KEY (`ROLE_ID`) REFERENCES `fa_role` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_FA_USER_ROLE_REF_USER` FOREIGN KEY (`USER_ID`) REFERENCES `fa_user` (`ID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `fa_user_role`
--

LOCK TABLES `fa_user_role` WRITE;
/*!40000 ALTER TABLE `fa_user_role` DISABLE KEYS */;
INSERT INTO `fa_user_role` VALUES (1,1),(1,197);
/*!40000 ALTER TABLE `fa_user_role` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sequence`
--

DROP TABLE IF EXISTS `sequence`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sequence` (
  `seq_name` varchar(50) NOT NULL,
  `current_val` int(11) NOT NULL,
  `increment_val` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`seq_name`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 ROW_FORMAT=COMPACT;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sequence`
--

LOCK TABLES `sequence` WRITE;
/*!40000 ALTER TABLE `sequence` DISABLE KEYS */;
INSERT INTO `sequence` VALUES ('fa_equipment',4,1),('fa_files',29,1),('fa_login',51,1),('fa_login_history',30,1),('fa_module',15,1),('fa_query',9,1),('fa_role',5,1),('fa_script',2,1),('fa_script_task',50949,1),('fa_script_task_log',50941,1),('fa_table_column',11,1),('fa_table_type',9,1),('fa_user',1538,1);
/*!40000 ALTER TABLE `sequence` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-07-28 21:18:05
