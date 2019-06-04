
-- --------------------------------------------------------
-- 主機:                           127.0.0.1
-- 伺服器版本:                        10.3.12-MariaDB - Source distribution
-- 伺服器操作系統:                      Win64
-- HeidiSQL 版本:                  10.1.0.5464
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- 傾印 test 的資料庫結構
CREATE DATABASE IF NOT EXISTS `test` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `test`;

-- 傾印  表格 test.applicationlog 結構
CREATE TABLE IF NOT EXISTS `applicationlog` (
  `Id` varchar(255) DEFAULT NULL,
  `ReferenceNumber` varchar(255) DEFAULT NULL,
  `ModuleName` varchar(255) DEFAULT NULL,
  `OperatingFunctionName` varchar(255) DEFAULT NULL,
  `OperatingType` varchar(255) DEFAULT NULL,
  `Address` varchar(255) DEFAULT NULL,
  `Operator` varchar(255) DEFAULT NULL,
  `VersionNumber` varchar(255) DEFAULT NULL,
  `BrowserInformation` varchar(255) DEFAULT NULL,
  `Message` varchar(3000) DEFAULT NULL,
  `BeforeData` varchar(3000) DEFAULT NULL,
  `AfterData` varchar(3000) DEFAULT NULL,
  `Timestamp` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- 傾印  表格 test.users 結構
CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) NOT NULL DEFAULT '0',
  `Password` varchar(255) NOT NULL DEFAULT '0',
  `Birthdate` date NOT NULL DEFAULT current_timestamp(),
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- 正在傾印表格  test.users 的資料：~2 rows (大約)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`Id`, `Username`, `Password`, `Birthdate`) VALUES
	(1, 'test', 'test', '1992-01-01'),
	(2, 'kid', 'kid', '2010-01-01');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

GO

--語法錯誤` 附近的語法不正確。
---- --------------------------------------------------------
---- 主機:                           127.0.0.1
---- 伺服器版本:                        10.3.12-MariaDB - Source distribution
---- 伺服器操作系統:                      Win64
---- HeidiSQL 版本:                  10.1.0.5464
---- --------------------------------------------------------
--
--/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
--/*!40101 SET NAMES utf8 */;
--/*!50503 SET NAMES utf8mb4 */;
--/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
--/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
--
--
---- 傾印 test 的資料庫結構
--CREATE DATABASE IF NOT EXISTS `test` /*!40100 DEFAULT CHARACTER SET latin1 */;
--USE `test`;
--
---- 傾印  表格 test.applicationlog 結構
--CREATE TABLE IF NOT EXISTS `applicationlog` (
--  `Id` varchar(255) DEFAULT NULL,
--  `ReferenceNumber` varchar(255) DEFAULT NULL,
--  `ModuleName` varchar(255) DEFAULT NULL,
--  `OperatingFunctionName` varchar(255) DEFAULT NULL,
--  `OperatingType` varchar(255) DEFAULT NULL,
--  `Address` varchar(255) DEFAULT NULL,
--  `Operator` varchar(255) DEFAULT NULL,
--  `VersionNumber` varchar(255) DEFAULT NULL,
--  `BrowserInformation` varchar(255) DEFAULT NULL,
--  `Message` varchar(3000) DEFAULT NULL,
--  `BeforeData` varchar(3000) DEFAULT NULL,
--  `AfterData` varchar(3000) DEFAULT NULL,
--  `Timestamp` datetime DEFAULT NULL
--) ENGINE=InnoDB DEFAULT CHARSET=latin1;
--
---- 傾印  表格 test.users 結構
--CREATE TABLE IF NOT EXISTS `users` (
--  `Id` int(11) NOT NULL AUTO_INCREMENT,
--  `Username` varchar(255) NOT NULL DEFAULT '0',
--  `Password` varchar(255) NOT NULL DEFAULT '0',
--  `Birthdate` date NOT NULL DEFAULT current_timestamp(),
--  PRIMARY KEY (`Id`)
--) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;
--
---- 正在傾印表格  test.users 的資料：~2 rows (大約)
--/*!40000 ALTER TABLE `users` DISABLE KEYS */;
--INSERT INTO `users` (`Id`, `Username`, `Password`, `Birthdate`) VALUES
--	(1, 'test', 'test', '1992-01-01'),
--	(2, 'kid', 'kid', '2010-01-01');
--/*!40000 ALTER TABLE `users` ENABLE KEYS */;
--
--/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
--/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
--/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;



GO
