-- phpMyAdmin SQL Dump
-- version 4.7.9
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 16-11-2018 a las 17:55:36
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
-- Estructura Stand-in para la vista `coincidences`
-- (Véase abajo para la vista actual)
--
CREATE TABLE `coincidences` (
);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ebay`
--

CREATE TABLE `ebay` (
  `id` varchar(500) NOT NULL,
  `description` varchar(10000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `groups`
--

CREATE TABLE `groups` (
  `id_group` int(11) NOT NULL,
  `valor` int(11) NOT NULL,
  `description` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `group_products`
--

CREATE TABLE `group_products` (
  `id_group_product` int(11) NOT NULL,
  `id_group` int(11) NOT NULL,
  `id_product` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura Stand-in para la vista `own_filter`
-- (Véase abajo para la vista actual)
--
CREATE TABLE `own_filter` (
`description` varchar(10000)
);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `own_products`
--

CREATE TABLE `own_products` (
  `id_product` int(11) NOT NULL,
  `description` varchar(10000) NOT NULL,
  `id_type` int(11) NOT NULL,
  `id_user` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `products`
--

CREATE TABLE `products` (
  `id` varchar(500) NOT NULL,
  `id_type` int(11) NOT NULL,
  `link` varchar(500) NOT NULL,
  `reference` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `type_products`
--

CREATE TABLE `type_products` (
  `id_type` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `users`
--

CREATE TABLE `users` (
  `Email` varchar(100) NOT NULL,
  `id_group` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `user_products`
--

CREATE TABLE `user_products` (
  `id_user_product` int(11) NOT NULL,
  `id_user` varchar(100) NOT NULL,
  `id_product` varchar(500) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Estructura para la vista `coincidences`
--
DROP TABLE IF EXISTS `coincidences`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `coincidences`  AS  select `products`.`id` AS `id`,`products`.`reference` AS `reference` from ((`products` join `group_products` on((`products`.`id_product` = `group_products`.`id_product`))) join `user_products` on((`products`.`id_product` = `user_products`.`id_product`))) ;

-- --------------------------------------------------------

--
-- Estructura para la vista `own_filter`
--
DROP TABLE IF EXISTS `own_filter`;

CREATE ALGORITHM=UNDEFINED DEFINER=`root`@`localhost` SQL SECURITY DEFINER VIEW `own_filter`  AS  select `own_products`.`description` AS `description` from `own_products` ;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `ebay`
--
ALTER TABLE `ebay`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `groups`
--
ALTER TABLE `groups`
  ADD PRIMARY KEY (`id_group`);

--
-- Indices de la tabla `group_products`
--
ALTER TABLE `group_products`
  ADD PRIMARY KEY (`id_group_product`);

--
-- Indices de la tabla `own_products`
--
ALTER TABLE `own_products`
  ADD PRIMARY KEY (`id_product`);

--
-- Indices de la tabla `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `type_products`
--
ALTER TABLE `type_products`
  ADD PRIMARY KEY (`id_type`);

--
-- Indices de la tabla `user_products`
--
ALTER TABLE `user_products`
  ADD PRIMARY KEY (`id_user_product`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `groups`
--
ALTER TABLE `groups`
  MODIFY `id_group` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `group_products`
--
ALTER TABLE `group_products`
  MODIFY `id_group_product` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `own_products`
--
ALTER TABLE `own_products`
  MODIFY `id_product` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `type_products`
--
ALTER TABLE `type_products`
  MODIFY `id_type` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `user_products`
--
ALTER TABLE `user_products`
  MODIFY `id_user_product` int(11) NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
