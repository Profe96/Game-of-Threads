-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 11-11-2018 a las 22:57:24
-- Versión del servidor: 10.1.31-MariaDB
-- Versión de PHP: 5.6.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `got_main_database`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `group`
--

CREATE TABLE `group` (
  `ID_Group` int(11) NOT NULL,
  `Valor` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `group_product`
--

CREATE TABLE `group_product` (
  `ID_GP` int(11) NOT NULL,
  `ID_Group` int(11) NOT NULL,
  `ID_Product` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `product`
--

CREATE TABLE `product` (
  `ID_Product` int(11) NOT NULL,
  `ID` varchar(600) NOT NULL,
  `ID_Type` int(11) NOT NULL,
  `Link` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `type_product`
--

CREATE TABLE `type_product` (
  `ID_Type` int(11) NOT NULL,
  `Nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `user`
--

CREATE TABLE `user` (
  `ID` int(11) NOT NULL,
  `ID_Group` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `user_product`
--

CREATE TABLE `user_product` (
  `ID_UP` int(11) NOT NULL,
  `ID_User` int(11) NOT NULL,
  `ID_Product` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `group`
--
ALTER TABLE `group`
  ADD PRIMARY KEY (`ID_Group`);

--
-- Indices de la tabla `group_product`
--
ALTER TABLE `group_product`
  ADD PRIMARY KEY (`ID_GP`);

--
-- Indices de la tabla `type_product`
--
ALTER TABLE `type_product`
  ADD PRIMARY KEY (`ID_Type`);

--
-- Indices de la tabla `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`ID`);

--
-- Indices de la tabla `user_product`
--
ALTER TABLE `user_product`
  ADD PRIMARY KEY (`ID_UP`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `group`
--
ALTER TABLE `group`
  MODIFY `ID_Group` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `group_product`
--
ALTER TABLE `group_product`
  MODIFY `ID_GP` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `type_product`
--
ALTER TABLE `type_product`
  MODIFY `ID_Type` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `user`
--
ALTER TABLE `user`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `user_product`
--
ALTER TABLE `user_product`
  MODIFY `ID_UP` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
