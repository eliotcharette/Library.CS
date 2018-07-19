-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Jul 20, 2018 at 01:04 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `library`
--

-- --------------------------------------------------------

--
-- Table structure for table `authors`
--

CREATE TABLE `authors` (
  `id` int(11) NOT NULL,
  `author` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `authors`
--

INSERT INTO `authors` (`id`, `author`) VALUES
(1, 'Mike jones'),
(2, 'Marcel Proust'),
(3, 'Cervantes'),
(4, 'James Joyce'),
(5, 'F. Scott Fitzgerald'),
(6, 'Shakespeare'),
(7, 'Leo Tolstoy'),
(8, 'Homer'),
(9, 'Gabriel Garcia Marquez'),
(10, 'JK Rowling'),
(11, 'JRR Tolkein'),
(12, 'Paulo Cuelho'),
(13, 'Mark Twain'),
(14, 'Ernest Hemmingway');

-- --------------------------------------------------------

--
-- Table structure for table `authors_books`
--

CREATE TABLE `authors_books` (
  `id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL,
  `author_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `authors_books`
--

INSERT INTO `authors_books` (`id`, `book_id`, `author_id`) VALUES
(1, 11, 12),
(2, 3, 3),
(3, 5, 6),
(4, 13, 14),
(5, 6, 7),
(7, 4, 5),
(8, 10, 11);

-- --------------------------------------------------------

--
-- Table structure for table `books`
--

CREATE TABLE `books` (
  `id` int(11) NOT NULL,
  `book` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `books`
--

INSERT INTO `books` (`id`, `book`) VALUES
(1, 'Still tippin\''),
(2, 'In Search of Lost Time'),
(3, 'Don Quixote'),
(4, 'The Great Gatsby'),
(5, 'Romeo and Juliet'),
(6, 'War and Peace'),
(7, 'The Odyssey'),
(8, 'One Hundred Years of Solitude'),
(9, 'Harry Potter'),
(10, 'The Hobbit'),
(11, 'The Alchemist'),
(12, 'The Adventures of Huckleberry Finn'),
(13, 'The Old Man and the Sea');

-- --------------------------------------------------------

--
-- Table structure for table `members`
--

CREATE TABLE `members` (
  `id` int(11) NOT NULL,
  `member` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `members`
--

INSERT INTO `members` (`id`, `member`) VALUES
(1, 'Bob'),
(2, 'Bill'),
(3, 'Hank'),
(4, 'BoomHauer'),
(5, 'Dale'),
(6, 'Derek');

-- --------------------------------------------------------

--
-- Table structure for table `members_books`
--

CREATE TABLE `members_books` (
  `id` int(11) NOT NULL,
  `book_id` int(11) NOT NULL,
  `member_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `members_books`
--

INSERT INTO `members_books` (`id`, `book_id`, `member_id`) VALUES
(1, 3, 0),
(2, 5, 0),
(3, 1, 0),
(4, 1, 0),
(5, 1, 0),
(6, 3, 0),
(7, 5, 0),
(8, 9, 0),
(9, 11, 0),
(10, 1, 0),
(11, 7, 0),
(12, 4, 0),
(13, 6, 0),
(14, 3, 0),
(15, 7, 0),
(16, 6, 0),
(17, 1, 0),
(18, 5, 0),
(19, 1, 0),
(20, 1, 0),
(21, 1, 0),
(22, 3, 0),
(23, 1, 1),
(24, 4, 1),
(25, 4, 2),
(26, 4, 3),
(27, 6, 3),
(28, 4, 6),
(29, 11, 6);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `authors`
--
ALTER TABLE `authors`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `authors_books`
--
ALTER TABLE `authors_books`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `books`
--
ALTER TABLE `books`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `members`
--
ALTER TABLE `members`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `members_books`
--
ALTER TABLE `members_books`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `authors`
--
ALTER TABLE `authors`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
--
-- AUTO_INCREMENT for table `authors_books`
--
ALTER TABLE `authors_books`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT for table `books`
--
ALTER TABLE `books`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
--
-- AUTO_INCREMENT for table `members`
--
ALTER TABLE `members`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;
--
-- AUTO_INCREMENT for table `members_books`
--
ALTER TABLE `members_books`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=30;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
