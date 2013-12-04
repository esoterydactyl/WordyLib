WordyLib
========

Description
WordyLib is a dead simple class library that leverages the HTML Agility Pack to scrape web pages and filter items on a blacklist. A large sample blacklist is provided, containing a large variety of filler, names, non-typographic characters, etc.  The goal of WordyLib is to provide a simple way to retrieve only "keywords" from a website.

WordyLib should remain simple and flexible, and will likely become the basis for larger crawling and analytics projects.  Because of this, all classes and methods in WordyLib are public.  This has the side effect of making them useful in Powershell when imported with "Add-Type".
