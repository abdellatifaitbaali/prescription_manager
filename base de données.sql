/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE DATABASE IF NOT EXISTS `db` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;
USE `db`;

CREATE TABLE IF NOT EXISTS `analyses` (
  `analyse` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `controle` (
  `idC` int(11) NOT NULL AUTO_INCREMENT,
  `date_Controle` varchar(20) NOT NULL,
  `description` text NOT NULL,
  `date_Controle_Suivant` varchar(20) DEFAULT NULL,
  `traitement` text NOT NULL,
  `analyse` text DEFAULT NULL,
  `idP` int(11) NOT NULL,
  PRIMARY KEY (`idC`),
  KEY `idP` (`idP`),
  CONSTRAINT `controle_ibfk_1` FOREIGN KEY (`idP`) REFERENCES `patient` (`idP`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `login` (
  `username` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `role` varchar(20) NOT NULL,
  PRIMARY KEY (`username`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `mutuelles` (
  `mutuelle` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `patient` (
  `idP` int(11) NOT NULL AUTO_INCREMENT,
  `cin` varchar(20) NOT NULL,
  `nom` varchar(30) NOT NULL,
  `sexe` varchar(20) NOT NULL,
  `num` varchar(12) NOT NULL,
  `age` varchar(10) NOT NULL,
  `ville` varchar(20) NOT NULL,
  `mutuelle` varchar(20) NOT NULL,
  PRIMARY KEY (`idP`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `rendezvous` (
  `idR` int(11) NOT NULL AUTO_INCREMENT,
  `date_Rendez_Vous` varchar(20) NOT NULL,
  `Rendez_Vous` varchar(20) NOT NULL,
  `idP` int(11) NOT NULL,
  PRIMARY KEY (`idR`),
  UNIQUE KEY `UQ_date_time` (`date_Rendez_Vous`,`Rendez_Vous`) USING BTREE,
  KEY `idP` (`idP`),
  CONSTRAINT `rendezvous_ibfk_1` FOREIGN KEY (`idP`) REFERENCES `patient` (`idP`)
) ENGINE=InnoDB AUTO_INCREMENT=50 DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `traitements` (
  `traitement` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE IF NOT EXISTS `types` (
  `type` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
